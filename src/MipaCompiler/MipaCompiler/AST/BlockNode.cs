using MipaCompiler.BackEnd;
using MipaCompiler.Symbol;
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
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // code block increases scope
            symTable.AddScope();

            // check if current block is main function
            bool main = visitor.IsBlockMainFunction();
            visitor.SetIsBlockMainFunction(false);

            // add start of block
            visitor.AddCodeLine("{");

            // will generate code of the statements inside the block
            foreach(INode statement in statements)
            {
                statement.GenerateCode(visitor);
            }

            // free allocated strings
            Helper.FreeAllocatedStrings(visitor, false);

            // free allocated arrays
            visitor.FreeArraysBeforeExitingScope(visitor.GetSymbolTable().GetCurrentScope() - 1);

            // if block is main function --> should return 0
            if (main) visitor.AddCodeLine("return 0;");

            // add end of block
            visitor.AddCodeLine("}");

            // decrease scope when exiting block
            symTable.RemoveScope();
        }
    }
}
