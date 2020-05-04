﻿using MipaCompiler.Symbol;
using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>FunctionNode</c> represents a function definition in AST.
    /// </summary>
    public class FunctionNode : INode
    {
        private readonly int row;                   // row in source code
        private readonly int col;                   // column in source code
        private readonly string name;               // function name
        private readonly INode type;                // function type
        private readonly INode block;               // block of code
        private readonly List<INode> parameters;    // list of parameters

        /// <summary>
        /// Constructor <c>FunctionNode</c> creates new FunctionNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="name">name of the function</param>
        /// <param name="type">type of the function</param>
        /// <param name="parameters">parameters list</param>
        /// <param name="block">function code block</param>
        public FunctionNode(int row, int col, string name, INode type, List<INode> parameters, INode block)
        {
            this.row = row;
            this.col = col;
            this.name = name;
            this.type = type;
            this.parameters = new List<INode>();
            if (parameters != null) this.parameters = parameters;
            this.block = block;
        }

        /// <summary>
        /// Method <c>GetReturnType</c> returns the return type.
        /// </summary>
        /// <returns>type</returns>
        public INode GetReturnType()
        {
            return type;
        }

        /// <summary>
        /// Method <c>GetName</c> returns the name of the function.
        /// </summary>
        /// <returns>function name</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Method <c>GetBlock</c> returns the function code block.
        /// </summary>
        /// <returns>code block</returns>
        public INode GetBlock()
        {
            return block;
        }

        /// <summary>
        /// Method <c>GetParameters</c> retruns the list of parameters.
        /// </summary>
        /// <returns>parameters</returns>
        public List<INode> GetParameters()
        {
            return parameters;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.FUNCTION;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.FUNCTION}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine("Type:");
            if (type != null) type.PrettyPrint();
            Console.WriteLine("Parameters:");
            foreach (INode node in parameters)
            {
                node.PrettyPrint();
            }
            Console.WriteLine($"Block:");
            if (block != null) block.PrettyPrint();
        }

        /// <summary>
        /// Method <c>GenerateForwardDeclaration</c> returns function forward declaration
        /// in C-language.
        /// </summary>
        /// <returns>function forward declaration</returns>
        public string GenerateForwardDeclaration()
        {
            // variable to hold forward declaration
            string dcl = "";

            // get return type --> no need to give symbol table
            string resultType = SemanticAnalyzer.EvaluateTypeOfTypeNode(type, new List<string>(), null);

            // convert return type into c-code
            dcl += CodeGenerator.ConvertReturnTypeToTargetLanguage(resultType) + " ";

            // name of the function
            dcl += "function_" + name;

            int arrayCounter = 0;

            // add parameters
            dcl += "(";
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
                    dcl += $", int * size_{arrayCounter}";
                    arrayCounter++;
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
            DeclareFunctionParameterVariables(symTable);

            // get function forward declaration
            string forwardDeclaration = GenerateForwardDeclaration();

            // forward declaration will contain ";" at the end. remove it
            string firstLine = forwardDeclaration.Remove(forwardDeclaration.Length - 1);

            // add first line of code to list of generated code lines
            visitor.AddCodeLine(firstLine);

            // generate function block code
            block.GenerateCode(visitor);
        }

        /// <summary>
        /// Method <c>DeclareFunctionParameterVariables</c> declares parameter variables to symbol table.
        /// </summary>
        private void DeclareFunctionParameterVariables(SymbolTable symTable)
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
            }
        }

    }
}
