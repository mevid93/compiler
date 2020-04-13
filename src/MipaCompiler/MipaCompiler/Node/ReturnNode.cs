using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ReturnNode</c> represents return statement in AST.
    /// </summary>
    public class ReturnNode : INode
    {
        private readonly int row;               // row in source code
        private readonly int col;               // column in source code
        private readonly INode expression;      // returned value expression (optional)

        /// <summary>
        /// Constructor <c>ReturnNode</c> creates new ReturnNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="expression">expression for return value (optional)</param>
        public ReturnNode(int row, int col, INode expression)
        {
            this.row = row;
            this.col = col;
            this.expression = expression;
        }

        /// <summary>
        /// Method <c>GetExpression</c> returns the return value expression.
        /// </summary>
        /// <returns>return value expression</returns>
        public INode GetExpression()
        {
            return expression;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.RETURN;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.RETURN}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine("Expression:");
            if (expression != null) expression.PrettyPrint();
        }
    }
}
