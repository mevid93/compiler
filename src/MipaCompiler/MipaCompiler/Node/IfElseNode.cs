using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>IfElseNode</c> represents if-else statement in AST.
    /// </summary>
    public class IfElseNode : ISymbol
    {
        private readonly int row;               // row in source code
        private readonly int col;               // column in source code
        private readonly ISymbol condition;       // condition of if statement
        private readonly ISymbol thenStatement;   // statement if condition is true
        private readonly ISymbol elseStatement;   // statement if condition is false (optional)

        /// <summary>
        /// Constructor <c>IfElseNode</c> creates new IfElseNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="condition">condition of then statement</param>
        /// <param name="thenStatement">statement to execute if condition is true</param>
        /// <param name="elseStatement">statement to execute if condition if false (optional)</param>
        public IfElseNode(int row, int col, ISymbol condition, ISymbol thenStatement, ISymbol elseStatement)
        {
            this.row = row;
            this.col = col;
            this.condition = condition;
            this.thenStatement = thenStatement;
            this.elseStatement = elseStatement;
        }

        /// <summary>
        /// Method <c>GetCondition</c> returns the condition expression for then statement.
        /// </summary>
        /// <returns>condition expression</returns>
        public ISymbol GetCondition()
        {
            return condition;
        }

        /// <summary>
        /// Method <c>GetThenStatement</c> returns the "then" statement.
        /// </summary>
        /// <returns>then statement</returns>
        public ISymbol GetThenStatement()
        {
            return thenStatement;
        }

        /// <summary>
        /// Method <c>GetElseStatement</c> returns the "else" statement.
        /// </summary>
        /// <returns>else statement</returns>
        public ISymbol GetElseStatement()
        {
            return elseStatement;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.IF_ELSE;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.IF_ELSE}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine("Condition:");
            if (condition != null) condition.PrettyPrint();
            Console.WriteLine("Then statement:");
            if (thenStatement != null) thenStatement.PrettyPrint();
            Console.WriteLine("Else statement:");
            if (elseStatement != null) elseStatement.PrettyPrint();
        }
    }
}
