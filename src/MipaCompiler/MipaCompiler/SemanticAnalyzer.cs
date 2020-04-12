
using MipaCompiler.Node;
using System.Collections.Generic;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>Semantix</c> holds functionality to do semantic analysis for
    /// intermediate representation of source code. In other words, it takes
    /// AST as input, checks semantic constraints and reports any errors it finds.
    /// </summary>
    public class SemanticAnalyzer
    {
        private readonly INode ast;                 // AST representation of the source code
        private bool errorsDetected;                // flag telling about status of semantic analysis
        private readonly List<string> errors;       // list of all detected errors
        private readonly SymbolTable symbolTable;   // symbol table to store variables

        /// <summary>
        /// Constructor <c>SemanticAnalyzer</c> creates new SemanticAnalyzer-object.
        /// </summary>
        /// <param name="ast"></param>
        public SemanticAnalyzer(INode ast)
        {
            this.ast = ast;
            errors = new List<string>();
            symbolTable = new SymbolTable();
        }

        /// <summary>
        /// Method <c>ErrorsDetected</c> returns result of analysis.
        /// </summary>
        /// <returns>true if errors were detected</returns>
        public bool ErrosDetected()
        {
            return errorsDetected;
        }

        /// <summary>
        /// Method <c>GetDetectedErrors</c> returns the list of detected errors.
        /// </summary>
        /// <returns>list of errors</returns>
        public List<string> GetDetectedErrors()
        {
            return errors;
        }

        /// <summary>
        /// Method <c>CheckConstraints</c> checks the semantic constraints of source code.
        /// </summary>
        public void CheckConstraints()
        {
            // check that code actually exists
            if (ast == null)
            {
                errorsDetected = true;
                errors.Add($"SemanticError::Row ?::Column ?::No code to analyze!");
                return;
            }
            CheckProgram();
        }


        ///////////////////////////////// ACTUAL SEMANTIC CHECKS /////////////////////////////////


        /// <summary>
        /// Method <c>CheckProgram</c> checks the semantic constraints of program.
        /// </summary>
        private void CheckProgram()
        {
            // convert ast root node to program node
            ProgramNode prog = (ProgramNode)ast;

            // do semantic analysis for the functions
            foreach (INode f in prog.GetFunctions())
            {
                CheckFunction(f);
            }

            // do semantic analysis for the procedures
            foreach(INode p in prog.GetProcedures())
            {
                CheckProcedure(p);
            }

            // do semantic analysis for the main block,
            // can be null if parsing failed
            INode block = prog.GetMainBlock();
            if (block != null) CheckBlock(block);

        }

        private void CheckFunction(INode node)
        {
            // convert node to function node
            FunctionNode func = (FunctionNode)node;

            // TODO
        }

        private void CheckProcedure(INode node)
        {
            // convert node to procedure node
            ProcedureNode proc = (ProcedureNode)node;

            // TODO
        }

        private void CheckBlock(INode node)
        {
            // convert node to block node
            BlockNode block = (BlockNode)node;

            // TODO
        }
    }
}
