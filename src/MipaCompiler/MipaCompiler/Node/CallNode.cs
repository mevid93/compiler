using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// class <c>CallNode</c> represents function, procedure or
    /// predefined function (read, writeln) call in AST.
    /// </summary>
    public class CallNode : INode
    {
        private readonly int row;       // row in source code
        private readonly int col;       // column in source code
        private readonly string id;     // identifier for fucntion or procedure
        private readonly INode args;    // parameters for function or procedure

        /// <summary>
        /// Constructor <c>CallNode</c> creates new CallNode-object.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="id"></param>
        /// <param name="args"></param>
        public CallNode(int row, int col, string id, INode args)
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
        /// <returns>parameters</returns>
        public INode GetArguments()
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
            if (args != null) args.PrettyPrint();
        }
    }
}
