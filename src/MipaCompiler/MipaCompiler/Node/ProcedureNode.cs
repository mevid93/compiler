using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ProcedureNode</c> represents procedure definition in AST.
    /// </summary>
    public class ProcedureNode : ISymbol
    {
        private readonly int row;                   // row in source code   
        private readonly int col;                   // column in source code
        private readonly string name;               // name of the procedure
        private readonly List<ISymbol> parameters;    // list of parameters (optional)
        private readonly ISymbol block;               // code block

        /// <summary>
        /// Constructor <c>ProcedureNode</c> creates new ProcedureNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="name">name of the procedure</param>
        /// <param name="parameters">list of parameters (optional)</param>
        /// <param name="block">block of code</param>
        public ProcedureNode(int row, int col, string name, List<ISymbol> parameters, ISymbol block)
        {
            this.row = row;
            this.col = col;
            this.name = name;
            this.parameters = new List<ISymbol>();
            if (parameters != null) this.parameters = parameters;
            this.block = block;
        }

        /// <summary>
        /// Method <c>GetName</c> returns the name of the procedure.
        /// </summary>
        /// <returns>procedure name</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Method <c>GetParameters</c> returns the list of parameters.
        /// </summary>
        /// <returns>parameters</returns>
        public List<ISymbol> GetParameters()
        {
            return parameters;
        }

        /// <summary>
        /// Method <c>GetBlock</c> returns the procedure code block.
        /// </summary>
        /// <returns>code block</returns>
        public ISymbol GetBlock()
        {
            return block;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.PROCEDURE;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.PROCEDURE}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine("Parameters:");
            foreach (ISymbol node in parameters)
            {
                node.PrettyPrint();
            }
            Console.WriteLine($"Block:");
            if (block != null) block.PrettyPrint();
        }
    }
}
