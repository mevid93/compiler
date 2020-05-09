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

        ////////////////// EVERYTHING AFTER THIS IS FOR CODE GENERATION /////////////////////

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
            SymbolTable symTable = visitor.GetSymbolTable();

            // get simple type
            SimpleTypeNode stn = (SimpleTypeNode)type;
            string valueType = stn.GetTypeValue();

            // get simple type in C-language
            string varType = Helper.ConvertDeclarationTypeToC(valueType);

            // declare each variable to newline (easier to read)
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

                // if string --> allocate memory buffer
                if (valueType.Equals("string"))
                {
                    varName += " = malloc(256 * sizeof(char))";
                    visitor.AddAllocatedString($"var_{name}");
                }

                // define variable that holds the new generated code line
                string codeLine = $"{varType} {varName};";

                // add generated code line to list of code lines
                visitor.AddCodeLine(codeLine);
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForArrayType</c> handles the code genearation when
        /// variable to be declared is an array.
        /// </summary>
        private void GenerateCodeForArrayType(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            // first find out the simple type of array
            ArrayTypeNode arr = (ArrayTypeNode)type;
            SimpleTypeNode stn = (SimpleTypeNode)arr.GetSimpleType();
            string simpleType = stn.GetTypeValue();

            // get actual node type
            string nodeType = SemanticAnalyzer.EvaluateTypeOfTypeNode(type, new List<string>(), symTable);

            // get declaration type
            string varType = Helper.ConvertDeclarationTypeToC(nodeType);

            // generate code for array size --> only if it is given
            string tmp_size = null;
            if (arr.GetSizeExpression() != null)
            {
                arr.GetSizeExpression().GenerateCode(visitor);

                // get tmp variable that holds the size of the array
                tmp_size = visitor.GetLatestUsedTmpVariable();
            }

            // define list where all names of the new size variables are stored
            List<string> size_name = new List<string>();

            // declare each variable in a new line (easier to read)
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

                string allocationType = Helper.ConvertTypeToMallocTypeInC(nodeType);

                if(tmp_size != null)
                {
                    // check if size is pointer
                    string prefix = "";
                    if (symTable.GetVariableSymbolByIdentifier(tmp_size).IsPointer()) prefix = "*";

                    // allocate memory for array
                    string codeLine = $"{varType} {varName} = malloc({prefix}{tmp_size} * sizeof({allocationType}";
                    int address = visitor.GetArrayAddressCounter();
                    visitor.IncreaseArrayAddressCounter();
                    visitor.GetMemoryMap().AddNewAddress(new MemoryAddress(address, varName, symTable.GetCurrentScope()));

                    // each string is has fixed buffer size
                    if (simpleType == "string") codeLine += Helper.GetStringBufferSize();
                    codeLine += "));";

                    // add generated code line to list of code lines
                    visitor.AddCodeLine(codeLine);
                }
                else
                {
                    // empty array initialized
                    visitor.AddCodeLine($"{varType} {varName} = malloc(0 * sizeof({allocationType}));");
                    int address = visitor.GetArrayAddressCounter();
                    visitor.IncreaseArrayAddressCounter();
                    visitor.GetMemoryMap().AddNewAddress(new MemoryAddress(address, varName, symTable.GetCurrentScope()));
                }

            }

            // declare size variables
            foreach (string size in size_name)
            {
                VariableSymbol varSymbol = new VariableSymbol(size, "integer", null, symTable.GetCurrentScope());
                symTable.DeclareVariableSymbol(varSymbol);

                // check if size is pointer
                string prefix = "";
                if(tmp_size != null)
                {
                    if (symTable.GetVariableSymbolByIdentifier(tmp_size).IsPointer()) prefix = "*";
                }
                else
                {
                    tmp_size = "0";
                }

                string codeLine = $"int {size} = {prefix}{tmp_size};";
                visitor.AddCodeLine(codeLine);
            }
        }
    }
}
