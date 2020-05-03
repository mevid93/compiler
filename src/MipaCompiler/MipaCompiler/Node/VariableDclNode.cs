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

        public void GenerateCode(Visitor visitor)
        {
            // variable to hold new generated code line
            string line = "";

            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // simple type assignment
            if (type.GetNodeType() == NodeType.SIMPLE_TYPE)
            {
                SimpleTypeNode stn = (SimpleTypeNode)type;
                string valueType = stn.GetTypeValue();
                string varType = CodeGenerator.ConvertSimpleTypeToTargetLanguage(valueType);

                line += varType + " ";

                for(int i = 0; i < variables.Count; i++)
                {
                    VariableNode varNode = (VariableNode)variables[i];
                    string name = varNode.GetName();

                    // symbol table update
                    if (symTable.IsVariableSymbolInTable(name))
                    {
                        symTable.ReDeclareVariableSymbol(new VariableSymbol(name, valueType, null, symTable.GetCurrentScope()));
                    }
                    else
                    {
                        symTable.DeclareVariableSymbol(new VariableSymbol(name, valueType, null, symTable.GetCurrentScope()));
                    }

                    name = "var_" + name;
                    line += name;

                    if (i < variables.Count - 1) line += ", ";
                }

            }

            // array type
            if (type.GetNodeType() == NodeType.ARRAY_TYPE)
            {
                ArrayTypeNode arr = (ArrayTypeNode)type;
                SimpleTypeNode stn = (SimpleTypeNode)arr.GetSimpleType();
                string valueType = stn.GetTypeValue();
                //varType = CodeGenerator.ConvertReturnTypeToTargetLanguage(valueType);
            }

            // add end of statement 
            line += ";";

            // add generated code line to list of code lines
            visitor.AddCodeLine(line);
        }
    }
}
