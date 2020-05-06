using MipaCompiler.BackEnd;
using MipaCompiler.Symbol;
using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>VariableDclNode</c> represents variable declaration statement in AST.
    /// </summary>
    public class VariableDclNode : INode
    {
        private readonly int row;               // row in source code
        private readonly int col;               // column in source code
        private readonly List<INode> variables; // names of variables
        private readonly INode type;            // type of the variables

        /// <summary>
        /// Constructor <c>VariableDclNode</c> creates new VariableDclNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="variables">variables to declare</param>
        /// <param name="type">type of variable</param>
        public VariableDclNode(int row, int col, List<INode> variables, INode type)
        {
            this.row = row;
            this.col = col;
            this.type = type;
            this.variables = new List<INode>();
            if (variables != null) this.variables = variables;
        }

        /// <summary>
        /// Method <c>GetVariables</c> returns the list of variables.
        /// </summary>
        /// <returns>list of variable</returns>
        public List<INode> GetVariables()
        {
            return variables;
        }

        /// <summary>
        /// Method <c>GetVariableType</c> returns type of variables.
        /// </summary>
        /// <returns>type</returns>
        public INode GetVariableType()
        {
            return type;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.VARIABLE_DCL;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.VARIABLE_DCL}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            foreach (INode node in variables)
            {
                VariableNode varNode = (VariableNode)node;
                Console.WriteLine($"Name: {varNode.GetName()}");
            }
            Console.WriteLine($"Type: {type}");
        }

        //////////////// EVERYTHING AFTER THIS IS RELATED TO CODE GENERATION /////////////////////////

        public void GenerateCode(Visitor visitor)
        {
            // simple type assignment
            if (type.GetNodeType() == NodeType.SIMPLE_TYPE)
            {
                GenerateCodeForSimpleType(visitor);
            }

            // array type
            if (type.GetNodeType() == NodeType.ARRAY_TYPE)
            {
                GenerateCodeForArrayType(visitor);
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForSimpleType</c> handles the code generation when
        /// type of the variable to be declared is simple type.
        /// </summary>
        private void GenerateCodeForSimpleType(Visitor visitor)
        {
            // get symbol table from visitor
            SymbolTable symTable = visitor.GetSymbolTable();

            // get simple type
            SimpleTypeNode stn = (SimpleTypeNode)type;
            string valueType = stn.GetTypeValue();

            // get simple type in C-language
            string varType = Converter.ConvertSimpleTypeToTargetLanguage(valueType);

            // define variable that holds the new generated code line
            string codeLine = varType + " ";

            for (int i = 0; i < variables.Count; i++)
            {
                VariableNode varNode = (VariableNode)variables[i];
                string name = varNode.GetName();
                string varName = $"var_{name}";

                // symbol table update
                if (symTable.IsVariableSymbolInTable(name))
                {
                    symTable.ReDeclareVariableSymbol(new VariableSymbol(name, valueType, null, symTable.GetCurrentScope()));
                    symTable.ReDeclareVariableSymbol(new VariableSymbol(varName, valueType, null, symTable.GetCurrentScope()));
                }
                else
                {
                    symTable.DeclareVariableSymbol(new VariableSymbol(name, valueType, null, symTable.GetCurrentScope()));
                    symTable.DeclareVariableSymbol(new VariableSymbol(varName, valueType, null, symTable.GetCurrentScope()));
                }

                // each string can be max 256 chars long
                if (valueType == "string") varName += "[256]";

                codeLine += varName;

                if (i < variables.Count - 1) codeLine += ", ";
            }

            // add end of statement 
            codeLine += ";";

            // add generated code line to list of code lines
            visitor.AddCodeLine(codeLine);
        }

        /// <summary>
        /// Method <c>GenerateCodeForArrayType</c> handles the code genearation when
        /// variable to be declared is an array.
        /// </summary>
        private void GenerateCodeForArrayType(Visitor visitor)
        {
            // get symbol table from visitor
            SymbolTable symTable = visitor.GetSymbolTable();

            // first find out the simple type of array
            ArrayTypeNode arr = (ArrayTypeNode)type;
            SimpleTypeNode stn = (SimpleTypeNode)arr.GetSimpleType();
            string simpleType = stn.GetTypeValue();

            // get actual node type
            string nodeType = SemanticAnalyzer.EvaluateTypeOfTypeNode(type, new List<string>(), symTable);

            // get simple type in C-languge
            string varType = Converter.ConvertSimpleTypeToTargetLanguage(simpleType);

            // create variable that holds the new code line
            string codeLine = varType + " ";

            // generate code for array size
            arr.GetSizeExpression().GenerateCode(visitor);

            // get tmp variable that holds the size of the array
            string tmp_size = visitor.GetLatestUsedTmpVariable();

            // define list where all names of the new size variables are stored
            List<string> size_name = new List<string>();

            // declare each variable
            for (int i = 0; i < variables.Count; i++)
            {
                VariableNode varNode = (VariableNode)variables[i];
                string name = varNode.GetName();
                string varName = $"var_{name}";

                // symbol table update
                if (symTable.IsVariableSymbolInTable(name))
                {
                    symTable.ReDeclareVariableSymbol(new VariableSymbol(name, nodeType, null, symTable.GetCurrentScope()));
                    symTable.ReDeclareVariableSymbol(new VariableSymbol(varName, nodeType, null, symTable.GetCurrentScope()));
                }
                else
                {
                    symTable.DeclareVariableSymbol(new VariableSymbol(name, nodeType, null, symTable.GetCurrentScope()));
                    symTable.DeclareVariableSymbol(new VariableSymbol(varName, nodeType, null, symTable.GetCurrentScope()));
                }

                // add new size variable name to list (one for each array)
                size_name.Add($"size_{name}");

                codeLine += $"{varName}[{tmp_size}]";

                // each string is max 256 char long
                if (simpleType == "string") codeLine += "[256]";

                if (i < variables.Count - 1) codeLine += ", ";
            }

            // add end of statement 
            codeLine += ";";

            // add generated code line to list of code lines
            visitor.AddCodeLine(codeLine);

            // declare size variables
            foreach (string size in size_name)
            {
                VariableSymbol varSymbol = new VariableSymbol(size, "integer", null, symTable.GetCurrentScope());
                symTable.DeclareVariableSymbol(varSymbol);
                codeLine = $"int {size} = {tmp_size};";
                visitor.AddCodeLine(codeLine);
            }
        }
    }
}
