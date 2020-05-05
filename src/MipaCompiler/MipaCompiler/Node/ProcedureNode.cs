using MipaCompiler.Symbol;
using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ProcedureNode</c> represents procedure definition in AST.
    /// </summary>
    public class ProcedureNode : INode
    {
        private readonly int row;                   // row in source code   
        private readonly int col;                   // column in source code
        private readonly string name;               // name of the procedure
        private readonly List<INode> parameters;    // list of parameters (optional)
        private readonly INode block;               // code block

        /// <summary>
        /// Constructor <c>ProcedureNode</c> creates new ProcedureNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="name">name of the procedure</param>
        /// <param name="parameters">list of parameters (optional)</param>
        /// <param name="block">block of code</param>
        public ProcedureNode(int row, int col, string name, List<INode> parameters, INode block)
        {
            this.row = row;
            this.col = col;
            this.name = name;
            this.parameters = new List<INode>();
            if (parameters != null) this.parameters = parameters;
            this.block = block;
        }

        /// <summary>
        /// Method <c>GetName</c> returns the name of the procedure.
        /// </summary>
        /// <returns>procedure name</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Method <c>GetParameters</c> returns the list of parameters.
        /// </summary>
        /// <returns>parameters</returns>
        public List<INode> GetParameters()
        {
            return parameters;
        }

        /// <summary>
        /// Method <c>GetBlock</c> returns the procedure code block.
        /// </summary>
        /// <returns>code block</returns>
        public INode GetBlock()
        {
            return block;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.PROCEDURE;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.PROCEDURE}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine("Parameters:");
            foreach (INode node in parameters)
            {
                node.PrettyPrint();
            }
            Console.WriteLine($"Block:");
            if (block != null) block.PrettyPrint();
        }

        /// <summary>
        /// Method <c>GenerateForwardDeclaration</c> returns  procedure
        /// forward declaration in C-language.
        /// </summary>
        /// <returns>procedure forward declaration</returns>
        public string GenerateForwardDeclaration()
        {
            // variable to hold forward declaration
            string dcl = $"void procedure_{name}(";

            // add parameters
            for (int i = 0; i < parameters.Count; i++)
            {
                INode node = parameters[i];
                VariableNode variableNode = (VariableNode)node;
                string varType = SemanticAnalyzer.EvaluateTypeOfTypeNode(variableNode.GetVariableType(), new List<string>(), null);
                string cVarType = CodeGenerator.ConvertParameterTypeToTargetLanguage(varType);
                dcl += cVarType + " ";
                string varName = "var_" + variableNode.GetName();
                dcl += varName;

                // if was array, pass the array size as next parameter (always)
                if (varType.Contains("array"))
                {
                    dcl += $", int * size_{variableNode.GetName()}";
                }

                if (i < parameters.Count - 1) dcl += ", ";
            }
            dcl += ");";

            // return declaration
            return dcl;
        }

        public void GenerateCode(Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // declare variables that are passed as parameters to function
            DeclareProcedureParameterVariables(symTable);

            // get procedure forward declaration
            string forwardDeclaration = GenerateForwardDeclaration();

            // forward declaration will contain ";" at the end. remove it
            string firstLine = forwardDeclaration.Remove(forwardDeclaration.Length - 1);

            // add first line of code to list of generated code lines
            visitor.AddCodeLine(firstLine);

            // generate procedure block code
            block.GenerateCode(visitor);
        }

        /// <summary>
        /// Method <c>DeclareProcedureParameterVariables</c> declares parameter variables to symbol table.
        /// </summary>
        private void DeclareProcedureParameterVariables(SymbolTable symTable)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                INode node = parameters[i];
                VariableNode variableNode = (VariableNode)node;

                string varType = SemanticAnalyzer.EvaluateTypeOfTypeNode(variableNode.GetVariableType(), new List<string>(), null);
                string name = variableNode.GetName();
                int scope = symTable.GetCurrentScope() + 1; // take scope increase into account 

                VariableSymbol varSymbol = new VariableSymbol(name, varType, null, scope, true);
                symTable.DeclareVariableSymbol(varSymbol);

                // if was array, declare the array size as next parameter (always)
                if (varType.Contains("array"))
                {
                    name = $"size_{variableNode.GetName()}";
                    varType = "integer";
                    varSymbol = new VariableSymbol(name, varType, null, scope, true);
                    symTable.DeclareVariableSymbol(varSymbol);
                }
            }
        }


    }
}
