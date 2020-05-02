using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ProgramDclNode</c> represents root node in AST.
    /// </summary>
    public class ProgramNode : INode
    {
        private readonly int col;                   // column in source code
        private readonly int row;                   // row in source code
        private readonly string name;               // name of the program
        private readonly List<INode> functions;     // function declarations
        private readonly List<INode> procedures;    // procedure declarations
        private INode mainBlock;                    // main code

        /// <summary>
        ///  Constructor <c>ProgramDclNode</c> creates new ProgramDclNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="name">name of the program</param>
        /// <param name="procedures">program procedures</param>
        /// <param name="functions">program functions</param>
        /// <param name="mainBlock">main code block</param>
        public ProgramNode(int row, int col, string name, List<INode> procedures, List<INode> functions, INode mainBlock)
        {
            this.row = row;
            this.col = col;
            this.name = name;
            this.functions = new List<INode>();
            this.procedures = new List<INode>();
            if (functions != null) this.functions = functions;
            if (procedures != null) this.procedures = procedures;
            this.mainBlock = mainBlock;
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
        /// Method <c>GetProcedures</c> returns the procedures in program.
        /// </summary>
        /// <returns>list of procedures</returns>
        public List<INode> GetProcedures()
        {
            return procedures;
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

        public void GenerateCode(Visitor visitor)
        {
            // add standard input output libraries
            visitor.AddCodeLine("#include <stdio.h>");
            visitor.AddCodeLine("");

            // add typedef for boolean
            visitor.AddCodeLine("typedef int bool;");
            visitor.AddCodeLine("#define true 1");
            visitor.AddCodeLine("#define false 0");
            visitor.AddCodeLine("");

            // do function and procedure forward declaration (for mutual recursion)
            visitor.AddCodeLine("// here are forward declarations for functions and procedures (if any exists)");
            if (functions != null && functions.Count > 0)
            {
                foreach(INode function in functions)
                {
                    FunctionNode fNode = (FunctionNode)function;
                    string line = fNode.GenerateForwardDeclaration();
                    visitor.AddCodeLine(line);
                }
            }
            if (procedures != null && procedures.Count > 0)
            {
                foreach (INode procedure in procedures)
                {
                    ProcedureNode pNode = (ProcedureNode)procedure;
                    string line = pNode.GenerateForwardDeclaration();
                    visitor.AddCodeLine(line);
                }
            }


            // do actual function and procedure code
            visitor.AddCodeLine("");
            visitor.AddCodeLine("// here are the definitions of functions and procedures (if any exists)");
            if (functions != null && functions.Count > 0)
            {

            }
            if (procedures != null && procedures.Count > 0)
            {

            }

            // main block
            visitor.AddCodeLine("");
            visitor.AddCodeLine("// here is the main function");
            visitor.AddCodeLine("int main()");
            visitor.AddCodeLine("{");

            mainBlock.GenerateCode(visitor);

            visitor.AddCodeLine("return 0;");
            visitor.AddCodeLine("}");
        }
    }
}
