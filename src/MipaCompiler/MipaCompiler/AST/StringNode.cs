﻿using MipaCompiler.Symbol;
using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>StringNode</c> represents constant string in AST.
    /// </summary>
    public class StringNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly string value;      // value of string

        /// <summary>
        /// Constructor <c>StringNode</c> creates new StringNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="value">value of string</param>
        public StringNode(int row, int col, string value)
        {
            this.row = row;
            this.col = col;
            this.value = value;
        }

        /// <summary>
        /// Method <c>GetValue</c> returns the value of string.
        /// </summary>
        /// <returns>value of string</returns>
        public string GetValue()
        {
            return value;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.STRING;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.STRING}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Value: {value}");
        }

        public void GenerateCode(Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // get number for tmp variable
            int number = visitor.GetTmpVariableCounter();
            visitor.IncreaseTmpVariableCounter();

            // define name and type, scope and pointer info
            string name = $"tmp_{number}";
            string type = "string";
            int scope = symTable.GetCurrentScope();
            bool isPointer = false;

            // define new variable symbol
            VariableSymbol varSymbol = new VariableSymbol(name, type, null, scope, isPointer);

            // declare tmp variable to symbol table
            symTable.DeclareVariableSymbol(varSymbol);

            // update latest tmp value to visitor
            visitor.SetLatestTmpVariableName(name);

            // generate code line
            string codeLine = $"char * {name} = \"{value}\";";
            visitor.AddCodeLine(codeLine);
        }
    }
}
