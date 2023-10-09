using MipaCompiler.Symbol;
using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>ProgramNode</c> represents root node in AST.
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

        ////////////////// EVERYTHING AFTER THIS IS FOR CODE GENERATION /////////////////////

        public void GenerateCode(Visitor visitor)
        {
            // include libraries
            InsertIncludes(visitor);

            // insert required typedefs
            InsertTypedefs(visitor);

            // inserts hard coded functions
            InsertHardCodedFunctions(visitor);

            // insert function and procedure forward declarations
            InsertForwardDeclarations(visitor);

            // initialize function symbols to symbol table
            InitFunctionSymbols(visitor);

            // initialize procedure symbols to symbol table
            InitProcedureSymbols(visitor);

            // do actual function and procedure code
            InsertFunctionAndProcedureCode(visitor);

            // insert main function code
            InsertMainFunctionCode(visitor);
        }

        /// <summary>
        /// Method <c>InsertIncludes</c> inserts lines that include required C-standard libaries.
        /// </summary>
        private void InsertIncludes(Visitor visitor)
        {
            // add standard input output libraries + string manipulation library
            visitor.AddCodeLine("#include <stdio.h>");
            visitor.AddCodeLine("#include <string.h>");
            visitor.AddCodeLine("#include <stdlib.h>");
            visitor.AddCodeLine("");
        }

        /// <summary>
        /// Method <c>InsertTypedefs</c> inserts lines that define required typedefs.
        /// </summary>
        private void InsertTypedefs(Visitor visitor)
        {
            // add typedef for boolean
            visitor.AddCodeLine("typedef int bool;");
            visitor.AddCodeLine("#define true 1");
            visitor.AddCodeLine("#define false 0");
            visitor.AddCodeLine("");
        }

        /// <summary>
        /// Method <c>InsertsHardCodedFunctions</c> inserts hard coded functions.
        /// </summary>
        private void InsertHardCodedFunctions(Visitor visitor)
        {
            visitor.AddCodeLine("// hard coded function to allocate string array");
            visitor.AddCodeLine("char ** allocateArrayOfStrings(int * arr_size)");
            visitor.AddCodeLine("{");
            visitor.AddCodeLine("int tmp_a = sizeof(char);");
            visitor.AddCodeLine("int tmp_b = *arr_size * tmp_a;");
            visitor.AddCodeLine("char ** x = malloc(tmp_b);");
            visitor.AddCodeLine("int var_i = 0;");
            visitor.AddCodeLine("while_entry: ;");
            visitor.AddCodeLine("if (var_i >= *arr_size) goto while_exit;");
            visitor.AddCodeLine("{");
            visitor.AddCodeLine("int tmp_c = sizeof(char);");
            visitor.AddCodeLine("int tmp_d = 256 * tmp_c;");
            visitor.AddCodeLine("x[var_i] = malloc(tmp_d);");
            visitor.AddCodeLine("var_i = var_i + 1;");
            visitor.AddCodeLine("}");
            visitor.AddCodeLine("goto while_entry;");
            visitor.AddCodeLine("while_exit: ;");
            visitor.AddCodeLine("return x;");
            visitor.AddCodeLine("}");
            visitor.AddCodeLine("");

            visitor.AddCodeLine("// hard coded function to deallocate string array");
            visitor.AddCodeLine("void deallocateArrayOfStrings(char ** arr, int * arr_size)");
            visitor.AddCodeLine("{");
            visitor.AddCodeLine("int var_i = 0;");
            visitor.AddCodeLine("while_entry: ;");
            visitor.AddCodeLine("if (var_i >= *arr_size) goto while_exit;");
            visitor.AddCodeLine("{");
            visitor.AddCodeLine("free(arr[var_i]);");
            visitor.AddCodeLine("var_i = var_i + 1;");
            visitor.AddCodeLine("}");
            visitor.AddCodeLine("goto while_entry;");
            visitor.AddCodeLine("while_exit: ;");
            visitor.AddCodeLine("free(arr);");
            visitor.AddCodeLine("}");
            visitor.AddCodeLine("");

        }

        /// <summary>
        /// Method <c>InsertForwardDeclarations</c> inserts forward declarations lines
        /// of functions and procedures. Forward declarations are needed for mutual recursion.
        /// </summary>
        private void InsertForwardDeclarations(Visitor visitor)
        {
            visitor.AddCodeLine("// here are forward declarations for functions and procedures (if any exists)");
            if (functions != null && functions.Count > 0)
            {
                foreach (INode function in functions)
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
        }

        /// <summary>
        /// Method <c>InitFunctionSymbols</c> initializes function symbols to symbol table.
        /// </summary>
        private void InitFunctionSymbols(Visitor visitor)
        {
            foreach(FunctionNode f in functions)
            {
                // get function name
                string functionName = f.GetName();

                // for each function parameter --> find parameter type
                // should be integer, string, boolean or array
                List<INode> parameters = f.GetParameters();
                string[] paramTypes = new string[parameters.Count];

                for (int i = 0; i < parameters.Count; i++)
                {
                    VariableNode varNode = (VariableNode)parameters[i];
                    paramTypes[i] = SemanticAnalyzer.EvaluateTypeOfTypeNode(varNode.GetVariableType(), new List<string>(), null);
                }

                // find return type
                string returnType = SemanticAnalyzer.EvaluateTypeOfTypeNode(f.GetReturnType(), new List<string>(), null);

                // create new function symbol
                FunctionSymbol symbol = new FunctionSymbol(functionName, paramTypes, returnType);

                // declare it
                visitor.GetSymbolTable().DeclareFunctionSymbol(symbol);
            }
        }

        /// <summary>
        /// Method <c>InitProcedureSymbols</c> initialized procedure symbols to symbol table.
        /// </summary>
        private void InitProcedureSymbols(Visitor visitor)
        {
            foreach (ProcedureNode p in procedures)
            {
                // get procedure name
                string procedureName = p.GetName();

                // for each procedure parameter --> find parameter type
                // should be integer, string, boolean or array
                List<INode> parameters = p.GetParameters();
                string[] paramTypes = new string[parameters.Count];

                for (int i = 0; i < parameters.Count; i++)
                {
                    VariableNode varNode = (VariableNode)parameters[i];
                    paramTypes[i] = SemanticAnalyzer.EvaluateTypeOfTypeNode(varNode.GetVariableType(), new List<string>(), null);
                }
                
                // create new procedure symbol
                ProcedureSymbol symbol = new ProcedureSymbol(procedureName, paramTypes);

                // declare it
                visitor.GetSymbolTable().DeclareProcedureSymbol(symbol);
            }
        }

        /// <summary>
        /// Method <c>InsertFunctionAndProcedureCode</c> inserts function and procedure code.
        /// </summary>
        private void InsertFunctionAndProcedureCode(Visitor visitor)
        {
            visitor.AddCodeLine("");
            visitor.AddCodeLine("// here are the definitions of functions and procedures (if any exists)");
            if (functions != null && functions.Count > 0)
            {
                foreach (INode function in functions)
                {
                    FunctionNode fNode = (FunctionNode)function;
                    fNode.GenerateCode(visitor);
                    visitor.AddCodeLine("");
                }
            }
            if (procedures != null && procedures.Count > 0)
            {
                foreach (INode procedure in procedures)
                {
                    ProcedureNode pNode = (ProcedureNode)procedure;
                    pNode.GenerateCode(visitor);
                    visitor.AddCodeLine("");
                }
            }
        }

        /// <summary>
        /// Method <c>InsertMainFunctionCode</c> inserts main function code.
        /// </summary>
        private void InsertMainFunctionCode(Visitor visitor)
        {
            visitor.AddCodeLine("");
            visitor.AddCodeLine("// here is the main function");
            visitor.AddCodeLine("int main()");

            // update information that current block is main function
            visitor.SetIsBlockMainFunction(true);

            // generate block code
            mainBlock.GenerateCode(visitor);
        }
    }
}
