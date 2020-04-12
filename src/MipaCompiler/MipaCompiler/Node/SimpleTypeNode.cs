using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>SimpleTypeNode</c> represents simple type in AST.
    /// </summary>
    public class SimpleTypeNode : ISymbol
    {
        private readonly int row;       // row in source code
        private readonly int col;       // column in source code
        private readonly string type;   // value of type

        /// <summary>
        /// Constructor <c>SimpleTypeNode</c> creates new SimpleTypeNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="type">type value</param>
        public SimpleTypeNode(int row, int col, string type)
        {
            this.row = row;
            this.col = col;
            this.type = type;
        }

        /// <summary>
        /// Method <c>GetTypeValue</c> returns the value of type.
        /// </summary>
        /// <returns>type value</returns>
        public string GetTypeValue()
        {
            return type;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.SIMPLE_TYPE;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.SIMPLE_TYPE}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Type: {type}");
        }
    }
}
