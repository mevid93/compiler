using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>SignNode</c> represents sign of term in AST.
    /// </summary>
    public class SignNode : INode
    {
        private readonly int row;               // row in source code
        private readonly int col;               // column in source code
        private readonly bool isNegative;       // is sign negative
        private readonly INode term;            // target term of sign

        /// <summary>
        /// Constructor <c>SignNode</c> creates new SignNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="isNegative">boolean value for negation</param>
        /// <param name="term">target term of the sign</param>
        public SignNode(int row, int col, bool isNegative, INode term)
        {
            this.row = row;
            this.col = col;
            this.isNegative = isNegative;
            this.term = term;
        }

        /// <summary>
        /// Method <c>IsNegativeSign</c> returns true if sign is negative.
        /// </summary>
        /// <returns>true if negative</returns>
        public bool IsNegativeSign()
        {
            return isNegative;
        }

        /// <summary>
        /// Method <c>GetTerm</c> returns term that is affected be sign.
        /// </summary>
        /// <returns>term</returns>
        public INode GetTerm()
        {
            return term;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.SIGN;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.SIGN}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Is negative: {isNegative}");
            Console.WriteLine($"Term: ");
            if (term != null) term.PrettyPrint();
        }

        public void GenerateCode(Visitor visitor)
        {
            // TODO
        }
    }
}
