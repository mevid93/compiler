using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>SizeNode</c> represents size operation of array.
    /// </summary>
    public class ArraySizeNode : INode
    {
        private readonly int row;       // row in source code
        private readonly int col;       // column in source code
        private readonly INode array;   // target array

        /// <summary>
        /// Constructor <c>ArraySizeNode</c> creates new ArraySizeNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="array">array node</param>
        public ArraySizeNode(int row, int col, INode array)
        {
            this.row = row;
            this.col = col;
            this.array = array;
        }

        /// <summary>
        /// Method <c>GetArray</c> returns target array of size operation.
        /// </summary>
        /// <returns>target array node</returns>
        public INode GetArray()
        {
            return array;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.ARRAY_SIZE;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.ARRAY_SIZE}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Array:");
            if (array != null) array.PrettyPrint();
        }
    }
}
