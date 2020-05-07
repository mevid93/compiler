using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>AssignmentNode</c> represents assingment operation in AST.
    /// </summary>
    public class AssignmentNode : INode
    {
        private readonly int row;               // row in source code
        private readonly int col;               // column in source code
        private readonly string identifier;     // identifier that is assigned a value
        private readonly INode expression;      // expression for value

        /// <summary>
        /// Constructor <c>AssignmentNode</c> creates new AssignmentNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="identifier">identifier that is assigned value</param>
        /// <param name="expression">expression for value</param>
        public AssignmentNode(int row, int col, string identifier, INode expression)
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
        public INode GetValueExpression()
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

        ////////////////// EVERYTHING AFTER THIS IS FOR CODE GENERATION /////////////////////

        public void GenerateCode(Visitor visitor)
        {
            // avaluate expression
            expression.GenerateCode(visitor);

            // get latest temp variable
            string temp = visitor.GetLatestUsedTmpVariable();

            //foreach (string linec in visitor.GetCodeLines()) Console.WriteLine(linec);

            // check if variable that is assigned a value is parameter
            string prefix = "";
            if (visitor.GetSymbolTable().GetVariableSymbolByIdentifier(identifier).IsPointer())
            {
                prefix = "*";
            }

            // check if assigned value is pointer
            string prefix2 = "";
            if (!temp.Equals("false") && !temp.Equals("true"))
            {
                if (visitor.GetSymbolTable().GetVariableSymbolByIdentifier(temp).IsPointer())
                {
                    prefix2 = "*";
                }
            }

            // define the new code line
            string line = $"{prefix}var_{identifier} = {prefix2}{temp};";

            // add new code line to list of generated code lines
            visitor.AddCodeLine(line);
        }
    }
}
