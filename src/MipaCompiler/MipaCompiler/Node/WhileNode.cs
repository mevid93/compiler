using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>WhileNode</c> represents while statement in AST.
    /// </summary>
    public class WhileNode : INode
    {
        private readonly int row;                   // row in source code
        private readonly int col;                   // column in source code
        private readonly INode boolExpression;      // boolean epression (condition for loop)
        private readonly INode statement;           // looped statement

        /// <summary>
        /// Constructor <c>WhileNode</c> creates new WhileNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="boolExpression">boolean expression</param>
        /// <param name="statement">looped statement</param>
        public WhileNode(int row, int col, INode boolExpression, INode statement)
        {
            this.row = row;
            this.col = col;
            this.boolExpression = boolExpression;
            this.statement = statement;
        }

        /// <summary>
        /// Method <c>GetBooleanExpression</c> returns the condition expression of while loop.
        /// </summary>
        /// <returns>boolean expression</returns>
        public INode GetBooleanExpression()
        {
            return boolExpression;
        }

        /// <summary>
        /// Method <c>GetStatement</c> returns the looped statement.
        /// </summary>
        /// <returns>statement</returns>
        public INode GetStatement()
        {
            return statement;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.WHILE;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.WHILE}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Boolean expression:");
            if (boolExpression != null) boolExpression.PrettyPrint();
            Console.WriteLine($"Statement:");
            if (statement != null) statement.PrettyPrint();
        }
    }
}
