using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>IntegerNode</c> represents constant integer in AST.
    /// </summary>
    public class IntegerNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly string value;      // value in source code

        /// <summary>
        /// Constructor <c>IntegerNode</c> creates new IntegerNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="value">integer value</param>
        public IntegerNode(int row, int col, string value)
        {
            this.row = row;
            this.col = col;
            this.value = value;
        }

        /// <summary>
        /// Method <c>GetValue</c> returns the integer value.
        /// </summary>
        /// <returns>value of integer</returns>
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
            return NodeType.INTEGER;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.INTEGER}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Value: {value}");
        }

        public void GenerateCode(Visitor visitor)
        {
            // constant integer value does not need to be assigned
            // to temporary variable. However, we still has to set
            // it as the latest tmp variable 
            visitor.SetLatestTmpVariableName(value);
        }
    }
}
