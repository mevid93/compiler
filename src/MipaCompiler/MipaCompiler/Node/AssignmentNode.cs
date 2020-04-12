using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>AssignmentNode</c> represents assingment operation in AST.
    /// </summary>
    public class AssignmentNode : ISymbol
    {
        private readonly int row;               // row in source code
        private readonly int col;               // column in source code
        private readonly string identifier;     // identifier that is assigned a value
        private readonly ISymbol expression;      // expression for value

        /// <summary>
        /// Constructor <c>AssignmentNode</c> creates new AssignmentNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="identifier">identifier that is assigned value</param>
        /// <param name="expression">expression for value</param>
        public AssignmentNode(int row, int col, string identifier, ISymbol expression)
        {
            this.row = row;
            this.col = col;
            this.identifier = identifier;
            this.expression = expression;
        }

        /// <summary>
        /// Method <c>GetIdentifier</c> returns the target identifier of assignment operation.
        /// </summary>
        /// <returns>identifier</returns>
        public string GetIdentifier()
        {
            return identifier;
        }

        /// <summary>
        /// Method <c>GetValueExpression</c> returns the expression for value.
        /// </summary>
        /// <returns>value</returns>
        public ISymbol GetValueExpression()
        {
            return expression;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.ASSIGNMENT;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.ASSIGNMENT}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Identifier: {identifier}");
            Console.WriteLine($"Value expression:");
            if (expression != null) expression.PrettyPrint();
        }
    }
}
