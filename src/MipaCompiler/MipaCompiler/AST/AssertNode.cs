using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>AssertNode</c> represents assert statement in AST.
    /// </summary>
    public class AssertNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly INode expression;  // boolen condition

        /// <summary>
        /// Constructor <c>AssertNode</c> creates new AssertNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="expression">condition expression</param>
        public AssertNode(int row, int col, INode expression)
        {
            this.row = row;
            this.col = col;
            this.expression = expression;
        }

        /// <summary>
        /// Method <c>GetExpression</c> returns boolean condition expression.
        /// </summary>
        /// <returns>expression</returns>
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

        public void GenerateCode(Visitor visitor)
        {
            // create assertion entry
            int number = visitor.GetAssertionCounter();
            visitor.IncreaseAssertionCounter();
            visitor.AddCodeLine($"label_assert_{number}_entry: ;");

            // assertio works so that it checks the condition expression
            // and if the expression does not evaluate as true,
            // an error message is given, and the program will exit

            // generate code from the boolean expression
            expression.GenerateCode(visitor);

            // get boolean expression hoding variable
            string boolVar = visitor.GetLatestUsedTmpVariable();

            // check condition
            visitor.AddCodeLine($"if ({boolVar}) goto label_assert_{number}_exit;");

            // give error message
            int rowInTargetLanguage = visitor.GetCodeLines().Count;
            visitor.AddCodeLine($"printf(\"%s\", \"Assertion error at row {rowInTargetLanguage}... \");");
            visitor.AddCodeLine($"printf(\"%s\\n\", \"Expected {boolVar} to be true!\");");
            visitor.AddCodeLine($"exit(1);");

            visitor.AddCodeLine($"label_assert_{number}_exit: ;");
        }
    }
}
