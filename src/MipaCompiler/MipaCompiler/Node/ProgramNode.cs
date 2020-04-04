using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ProgramDclNode</c> is root node for the AST.
    /// When program declaration is parsed, it should be the root of AST.
    /// </summary>
    public class ProgramNode : INode
    {
        private readonly int col;               // column in source code
        private readonly int row;               // row in source code
        private readonly string name;           // name of the program
        private List<INode> functions;          // function declarations
        private List<INode> procedures;         // procedure declarations
        private INode mainBlock;                // main code

        /// <summary>
        /// Constructor <c>ProgramDclNode</c> creates new ProgramDclNode-object.
        /// </summary>
        public ProgramNode(int row, int col, string name)
        {
            this.row = row;
            this.col = col;
            this.name = name;
            functions = new List<INode>();
            procedures = new List<INode>();
        }

        /// <summary>
        /// Method <c>AddFunction</c> adds function to list of functions.
        /// </summary>
        /// <param name="functionNode"></param>
        public void AddFunction(INode functionNode)
        {
            functions.Add(functionNode);
        }

        /// <summary>
        /// Method <c>GetFunctions</c> returns the functions in program.
        /// </summary>
        /// <returns>list of functions</returns>
        public List<INode> GetFunctions()
        {
            return functions;
        }

        /// <summary>
        /// Method <c>AddProcedure</c> adds procedure to list of procedures.
        /// </summary>
        /// <param name="procedureNode"></param>
        public void AddProcedure(INode procedureNode)
        {
            procedures.Add(procedureNode);
        }

        /// <summary>
        /// Method <c>GetProcedures</c> returns the procedures in program.
        /// </summary>
        /// <returns>list of procedures</returns>
        public List<INode> GetProcedures()
        {
            return procedures;
        }

        /// <summary>
        /// Method <c>SetMainBlock</c> sets the given node as main block.
        /// </summary>
        /// <param name="block"></param>
        public void SetMainBlock(INode block)
        {
            mainBlock = block;
        }

        /// <summary>
        /// Method <c>GetMainBlock</c> returns the main block node.
        /// </summary>
        /// <returns>main block</returns>
        public INode GetMainBlock()
        {
            return mainBlock;
        }

        /// <summary>
        /// Method <c>GetProgramName</c> returns the name of the program.
        /// </summary>
        /// <returns>program name</returns>
        public string GetProgramName()
        {
            return name;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.PROGRAM;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.PROGRAM}");
            Console.WriteLine($"Program name: {name}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine("Functions:");
            foreach (INode node in functions)
            {
                node.PrettyPrint();
            }
            Console.WriteLine("Procedures:");
            foreach (INode node in procedures)
            {
                node.PrettyPrint();
            }
            Console.WriteLine("Main block:");
            if (mainBlock != null) mainBlock.PrettyPrint();
        }
    }
}
