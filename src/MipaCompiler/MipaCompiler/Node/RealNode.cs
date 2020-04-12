using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>RealNode</c> represents constant real value in AST.
    /// </summary>
    public class RealNode : ISymbol
    {
        private readonly int row;       // row in source code
        private readonly int col;       // column in source code
        private readonly string value;  // value of real

        /// <summary>
        /// Constructor <c>RealNode</c> creates new RealNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="value">value in source code</param>
        public RealNode(int row, int col, string value)
        {
            this.row = row;
            this.col = col;
            this.value = value;
        }

        /// <summary>
        /// Method <c>GetValue</c> returns the real value.
        /// </summary>
        /// <returns>value of real</returns>
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
            return NodeType.REAL;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.REAL}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Value: {value}");
        }
    }
}
