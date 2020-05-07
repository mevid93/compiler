using MipaCompiler.BackEnd;
using MipaCompiler.Symbol;
using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>UnaryExpressionNode</c> represents unary expression in AST.
    /// </summary>
    public class UnaryExpressionNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly string oper;       // operator symbol (not, size)
        private readonly INode expression;  // target expression of unary operation

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
            switch (oper)
            {
                case "not":
                    GenerateCodeForNotOperator(visitor);
                    break;
                case "size":
                    GenerateCodeForSizeOperator(visitor);
                    break;
                default:
                    throw new Exception("Unexpected error: Invalid unary operaror!");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForSizeOperator</c> generates code where operator is
        /// "size".
        /// </summary>
        private void GenerateCodeForSizeOperator(Visitor visitor)
        {
            // size operator is only meant to be used with arrays.
            // this means that the variable is array.
            // retrieve the identifier of that array.
            VariableNode varNode = (VariableNode)expression;
            string identifier = varNode.GetName();

            // now we have the name, so let's find its size from the symbol table
            string sizeName = $"size_{identifier}";


            // update latest tmp variable information in visitor
            visitor.SetLatestTmpVariableName(sizeName);
        }

        /// <summary>
        /// Method <c>GenerateCodeForNotOperator</c> generates code for not operator.
        /// </summary>
        private void GenerateCodeForNotOperator(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            // generate code for the expression
            expression.GenerateCode(visitor);

            // get last tmp variable which should be a boolean value
            string tmpLast = visitor.GetLatestUsedTmpVariable();

            // get tmp counter
            int number = visitor.GetTmpVariableCounter();
            visitor.IncreaseTmpVariableCounter();

            // check if assigned variable is pointer
            bool isPointer = symTable.GetVariableSymbolByIdentifier(tmpLast).IsPointer();
            string prefix = Converter.GetPrefixWhenPointerNotNeeded("boolean", isPointer);

            // create new tmp variable for not operator
            string line = $"bool tmp_{number} = !{prefix}{tmpLast};";
            visitor.AddCodeLine(line);

            // update last used tmp variable
            visitor.SetLatestTmpVariableName($"tmp_{number}");

            // declare new variable
            symTable.DeclareVariableSymbol(new VariableSymbol($"tmp_{number}", "boolean", null, symTable.GetCurrentScope()));
        }
    }
}
