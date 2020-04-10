using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>FunctionNode</c> represents a function definition in AST.
    /// </summary>
    public class FunctionNode : INode
    {
        private readonly int row;                   // row in source code
        private readonly int col;                   // column in source code
        private readonly string name;               // function name
        private readonly string type;               // functiont type
        private readonly INode block;               // block of code
        private readonly List<INode> parameters;    // list of parameters

        /// <summary>
        /// Constructor <c>FunctionNode</c> creates new FunctionNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="name">name of the function</param>
        /// <param name="type">type of the function</param>
        /// <param name="parameters">parameters list</param>
        /// <param name="block">function code block</param>
        public FunctionNode(int row, int col, string name, string type, List<INode> parameters, INode block)
        {
            this.row = row;
            this.col = col;
            this.name = name;
            this.type = type;
            this.parameters = new List<INode>();
            if (parameters != null) this.parameters = parameters;
            this.block = block;
        }

        /// <summary>
        /// Method <c>GetReturnType</c> returns the return type.
        /// </summary>
        /// <returns>type</returns>
        public string GetReturnType()
        {
            return type;
        }

        /// <summary>
        /// Method <c>GetName</c> returns the name of the function.
        /// </summary>
        /// <returns>function name</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Method <c>GetBlock</c> returns the function code block.
        /// </summary>
        /// <returns>code block</returns>
        public INode GetBlock()
        {
            return block;
        }

        /// <summary>
        /// Method <c>GetParameters</c> retruns the list of parameters.
        /// </summary>
        /// <returns>parameters</returns>
        public List<INode> GetParameters()
        {
            return parameters;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.FUNCTION;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.FUNCTION}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Block:");
            if (block != null) block.PrettyPrint();
            Console.WriteLine("Parameters:");
            foreach(INode node in parameters)
            {
                node.PrettyPrint();
            }
        }
    }
}
