using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>BlockNode</c> represents code block in AST.
    /// </summary>
    public class BlockNode : INode
    {
        private readonly int row;                   // row in source code
        private readonly int col;                   // column in source code
        private readonly List<INode> statements;    // statements inside the block

        /// <summary>
        /// Constructor <c>BlockNode</c> creates new BlockNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="statements">statements inside the block</param>
        public BlockNode(int row, int col, List<INode> statements)
        {
            this.row = row;
            this.col = col;
            this.statements = new List<INode>();
            if (statements != null) this.statements = statements;
        }

        /// <summary>
        /// Method <c>GetStatements</c> returns the list of statements inside the block.
        /// </summary>
        /// <returns>statements</returns>
        public List<INode> GetStatements()
        {
            return statements;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.BLOCK;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.BLOCK}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine("Statements:");
            foreach(INode node in statements)
            {
                node.PrettyPrint();
            }
        }

        public void GenerateCode(Visitor visitor)
        {
            // will generate code of the statements inside the block
            foreach(INode statement in statements)
            {
                statement.GenerateCode(visitor);
            }
        }
    }
}
