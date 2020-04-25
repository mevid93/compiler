using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>UnaryExpressionNode</c> represents unary expression in AST.
    /// </summary>
    public class UnaryExpressionNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly string oper;       // operator symbol
        private readonly INode expression;  // terget expression of unary operation

        /// <summary>
        /// Constructor <c>UnaryExpressionNode</c> creates new UnaryExpressionNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="oper">unary operator symbol</param>
        /// <param name="expression">target expression</param>
        public UnaryExpressionNode(int row, int col, string oper, INode expression)
        {
            this.row = row;
            this.col = col;
            this.oper = oper;
            this.expression = expression;
        }

        /// <summary>
        /// Method <c>GetOperator</c> returns the unary operator symbol.
        /// </summary>
        /// <returns>unary operator symbol</returns>
        public string GetOperator()
        {
            return oper;
        }

        /// <summary>
        /// Method <c>GetExpression</c> returns the unary target expression.
        /// </summary>
        /// <returns>target expression</returns>
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
            return NodeType.UNARY_EXPRESSION;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.UNARY_EXPRESSION}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Operator: {oper}");
            Console.WriteLine($"Expression:");
            if (expression != null) expression.PrettyPrint();
        }

        public void GenerateCode(Visitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
