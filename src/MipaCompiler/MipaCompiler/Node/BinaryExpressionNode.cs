using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>BinaryExpressionNode</c> represents binary expression in AST.
    /// </summary>
    public class BinaryExpressionNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly string value;      // value of operation
        private readonly INode lhs;         // left hand side of expression
        private readonly INode rhs;         // right hand side of expression

        /// <summary>
        /// Constructor <c>BinaryExpressionNode</c> creates new BinaryExpressionNode.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="operation"></param>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public BinaryExpressionNode(int row, int col, string operation, INode lhs, INode rhs)
        {
            this.row = row;
            this.col = col;
            value = operation;
            this.lhs = lhs;
            this.rhs = rhs;
        }

        /// <summary>
        /// Method <c>GetOperation</c> returns the binary operation symbol.
        /// </summary>
        /// <returns>binary operation</returns>
        public string GetOperation()
        {
            return value;
        }

        /// <summary>
        /// Method <c>GetLhs</c> returns the left hand side of operation.
        /// </summary>
        /// <returns>lhs</returns>
        public INode GetLhs()
        {
            return lhs;
        }

        /// <summary>
        /// Method <c>GetRhs</c> returns the right hand side of operation.
        /// </summary>
        /// <returns>rhs</returns>
        public INode GetRhs()
        {
            return rhs;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.BINARY_EXPRESSION;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.BINARY_EXPRESSION}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Operation: {value}");
            Console.WriteLine($"Left hand side:");
            if (lhs != null) lhs.PrettyPrint();
            Console.WriteLine($"Right hand side:");
            if (rhs != null) rhs.PrettyPrint();
        }
    }
}
