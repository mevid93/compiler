using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ProgramDclNode</c> represents root node in AST.
    /// </summary>
    public class ProgramNode : ISymbol
    {
        private readonly int col;                   // column in source code
        private readonly int row;                   // row in source code
        private readonly string name;               // name of the program
        private readonly List<ISymbol> functions;     // function declarations
        private readonly List<ISymbol> procedures;    // procedure declarations
        private ISymbol mainBlock;                    // main code

        /// <summary>
        ///  Constructor <c>ProgramDclNode</c> creates new ProgramDclNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="name">name of the program</param>
        /// <param name="procedures">program procedures</param>
        /// <param name="functions">program functions</param>
        /// <param name="mainBlock">main code block</param>
        public ProgramNode(int row, int col, string name, List<ISymbol> procedures, List<ISymbol> functions, ISymbol mainBlock)
        {
            this.row = row;
            this.col = col;
            this.name = name;
            this.functions = new List<ISymbol>();
            this.procedures = new List<ISymbol>();
            if (functions != null) this.functions = functions;
            if (procedures != null) this.procedures = procedures;
            this.mainBlock = mainBlock;
        }

        /// <summary>
        /// Method <c>GetFunctions</c> returns the functions in program.
        /// </summary>
        /// <returns>list of functions</returns>
        public List<ISymbol> GetFunctions()
        {
            return functions;
        }

        /// <summary>
        /// Method <c>GetProcedures</c> returns the procedures in program.
        /// </summary>
        /// <returns>list of procedures</returns>
        public List<ISymbol> GetProcedures()
        {
            return procedures;
        }

        /// <summary>
        /// Method <c>GetMainBlock</c> returns the main block node.
        /// </summary>
        /// <returns>main block</returns>
        public ISymbol GetMainBlock()
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
            foreach (ISymbol node in functions)
            {
                node.PrettyPrint();
            }
            Console.WriteLine("Procedures:");
            foreach (ISymbol node in procedures)
            {
                node.PrettyPrint();
            }
            Console.WriteLine("Main block:");
            if (mainBlock != null) mainBlock.PrettyPrint();
        }
    }
}
