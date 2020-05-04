using System;
using System.Collections.Generic;

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
            // string does not need to be assigned to temporary variable in C-language.
            // however, we still have to set it as the latest tmp variable.
            visitor.SetLatestTmpVariableName($"\"{value}\"");
        }
    }
}
