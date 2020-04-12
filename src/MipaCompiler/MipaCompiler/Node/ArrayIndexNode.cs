using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ArrayIndexNode</c> represents array index operation in AST.
    /// </summary>
    public class ArrayIndexNode : ISymbol
    {
        private readonly int row;       // row in source code
        private readonly int col;       // column in source code
        private readonly ISymbol array;   // array in source code
        private readonly ISymbol index;   // index in source code

        /// <summary>
        /// Constructor <c>ArrayIndexNode</c> creates new ArrayIndexNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="array">target array</param>
        /// <param name="index">expression for index</param>
        public ArrayIndexNode(int row, int col, ISymbol array, ISymbol index)
        {
            this.row = row;
            this.col = col;
            this.array = array;
            this.index = index;
        }

        /// <summary>
        /// Method <c>GetArray</c> returns array (VariableNode).
        /// </summary>
        /// <returns>array</returns>
        public ISymbol GetArray()
        {
            return array;
        }

        /// <summary>
        /// Method <c>GetIndex</c> returns expression that defines the index.
        /// </summary>
        /// <returns>index</returns>
        public ISymbol GetIndex()
        {
            return index;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.ARRAY_INDEX;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.ARRAY_INDEX}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine("Array:");
            if (array != null) array.PrettyPrint();
            Console.WriteLine($"Index:");
            if (index != null) index.PrettyPrint();
        }
    }
}
