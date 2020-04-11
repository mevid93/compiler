using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ArrayTypeNode</c> represents array type in AST.
    /// </summary>
    public class ArrayTypeNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly INode size;        // size expression (optional)
        private readonly INode simpleType;  // type of values in array

        /// <summary>
        /// Constructror <c>ArrayTypeNode</c> creates new ArrayTypeNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="size">size expression optional</param>
        /// <param name="simpleType">type of values in array</param>
        public ArrayTypeNode(int row, int col, INode size, INode simpleType)
        {
            this.row = row;
            this.col = col;
            this.size = size;
            this.simpleType = simpleType;
        }

        /// <summary>
        /// Method <c>GetSizeExpression</c> returns the size of array.
        /// </summary>
        /// <returns>array size expression</returns>
        public INode GetSizeExpression()
        {
            return size;
        }

        /// <summary>
        /// Method <c>GetVariableType</c> returns the type of values stored in array.
        /// </summary>
        /// <returns>type of values in array</returns>
        public INode GetSimpleType()
        {
            return simpleType;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.ARRAY_TYPE;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.ARRAY_TYPE}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Size expression:");
            if (size != null) size.PrettyPrint();
            Console.WriteLine($"Simple type:");
            if (simpleType != null) simpleType.PrettyPrint();
        }
    }
}
