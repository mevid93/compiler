
using MipaCompiler.Node;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>Semantix</c> holds functionality to do semantic analysis for
    /// intermediate representation of source code. In other words, it takes
    /// AST as input, checks semantic constraints and reports any errors it finds.
    /// </summary>
    public class SemanticAnalyzer
    {
        private readonly INode ast;     // AST representation of the source code
        private bool errorsDetected;    // flag telling about status of semantic analysis

        /// <summary>
        /// Constructor <c>SemanticAnalyzer</c> creates new SemanticAnalyzer-object.
        /// </summary>
        /// <param name="ast"></param>
        public SemanticAnalyzer(INode ast)
        {
            this.ast = ast;
        }

        /// <summary>
        /// Method <c>ErrorsDetected</c> returns result of analysis.
        /// </summary>
        /// <returns>true if errors were detected</returns>
        public bool ErrosDetected()
        {
            return errorsDetected;
        }

    }
}
