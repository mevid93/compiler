using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ProgramDclNode</c> is root node for the AST.
    /// When program declaration is parsed, it should be the root of AST.
    /// </summary>
    public class BlockNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private List<INode> statements;     // statements inside the block

        /// <summary>
        /// Constructor <c>BlockNode</c> creates new BlockNode-object.
        /// </summary>
        public BlockNode(int row, int col)
        {
            this.row = row;
            this.col = col;
            statements = new List<INode>();
        }

        /// <summary>
        /// Method <c>AddStatement</c> adds new statement node in statements list.
        /// </summary>
        public void AddStatement(INode statement)
        {
            statements.Add(statement);
        }

        /// <summary>
        /// Method <c>GetStatements</c> 
        /// </summary>
        /// <returns></returns>
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
    }
}
