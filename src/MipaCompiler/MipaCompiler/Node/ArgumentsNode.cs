using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ArgumentsNode</c> represents parameters for function or
    /// procedure in AST.
    /// </summary>
    public class ArgumentsNode : INode
    {
        private readonly int row;               // row in source code
        private readonly int col;               // column in source code
        private readonly List<INode> args;      // argument expression nodes

        /// <summary>
        /// Constructor <c>ArgumentsNode</c> creates new ArgumentsNode-object.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="args"></param>
        public ArgumentsNode(int row, int col, List<INode> args)
        {
            this.row = row;
            this.col = col;
            this.args = args;
        }

        /// <summary>
        /// Method <c>GetArguments</c> returns the list of parameters.
        /// </summary>
        /// <returns>parameters list</returns>
        public List<INode> GetArguments()
        {
            return args;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.ARGUMENTS;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.ARGUMENTS}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Arguments:");
            foreach(INode node in args)
            {
                node.PrettyPrint();
            }
        }
    }
}
