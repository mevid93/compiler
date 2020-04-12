using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>AssertNode</c> represents assert statement in AST.
    /// </summary>
    public class AssertNode : ISymbol
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly ISymbol expression;  // boolen condition

        /// <summary>
        /// Constructor <c>AssertNode</c> creates new AssertNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="expression">condition expression</param>
        public AssertNode(int row, int col, ISymbol expression)
        {
            this.row = row;
            this.col = col;
            this.expression = expression;
        }

        /// <summary>
        /// Method <c>GetExpression</c> returns boolean condition expression.
        /// </summary>
        /// <returns>expression</returns>
        public ISymbol GetExpression()
        {
            return expression;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.ASSERT;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.ASSERT}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Expression:");
            if (expression != null) expression.PrettyPrint();
        }
    }
}
