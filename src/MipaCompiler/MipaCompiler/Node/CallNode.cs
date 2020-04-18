using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// class <c>CallNode</c> represents function, procedure or
    /// predefined function (read, writeln) call in AST.
    /// </summary>
    public class CallNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly string id;         // identifier for fucntion or procedure
        private readonly List<INode> args;  // parameters for function or procedure

        /// <summary>
        /// Constructor <c>CallNode</c> creates new CallNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="id">identifier of function or procedure</param>
        /// <param name="args">arguments</param>
        public CallNode(int row, int col, string id, List<INode> args)
        {
            this.row = row;
            this.col = col;
            this.id = id;
            this.args = args;
        }

        /// <summary>
        /// Method <c>GetId</c> returns identifier of function or procedure
        /// that is called.
        /// </summary>
        /// <returns>identifier</returns>
        public string GetId()
        {
            return id;
        }

        /// <summary>
        /// Method <c>GetArguments</c> returns the parameters of the call operation.
        /// </summary>
        /// <returns>arguments</returns>
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
            return NodeType.CALL;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.CALL}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Id: {id}");
            Console.WriteLine("Arguments:");
            foreach(INode arg in args)
            {
                arg.PrettyPrint();
            }
        }

        public string GenerateCode(List<string> codeLines)
        {
            throw new NotImplementedException();
        }
    }
}
