using MipaCompiler.Node;
using System;
using System.Collections.Generic;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>CodeGenerator</c> contains functioncality to generate program code file.
    /// </summary>
    public class CodeGenerator
    {
        private readonly string outputFilePath;             // output file path
        private readonly INode ast;                         // abstract syntax tree
        private readonly List<string> codeLines;            // lines of code

        /// <summary>
        /// Constructor <c>CodeGenerator</c>
        /// </summary>
        /// <param name="outputFilePath"></param>
        /// <param name="ast">abstract syntax tree</param>
        public CodeGenerator(string outputFilePath, INode ast)
        {
            codeLines = new List<string>();
            this.outputFilePath = outputFilePath;
            this.ast = null;
            if (ast != null || ast.GetNodeType() == NodeType.PROGRAM) this.ast = ast;
        }

        /// <summary>
        /// Method <c>Generate</c> will cenerate a output file
        /// that corresponds to the ast. Output file will contain low level C-code.
        /// </summary>
        public void Generate()
        {
            // check that ast is not null
            if (ast == null)
            {
                throw new Exception("Unexpected error: Abstract syntax tree is null!");
            }

            if (outputFilePath == null)
            {
                throw new Exception("Unexpected error: Output file path is null!");
            }

            // abstract syntax tree and output file path exists --> generate code
            GenerateCodeLines();

            // write code lines to output file
            WriteCodeLinesToFile();
        }

        /// <summary>
        /// Method <c>GetCodeLindes</c> return list of generated code lines.
        /// </summary>
        /// <returns>C-code lines</returns>
        public List<string> GetCodeLines()
        {
            return codeLines;
        }

        /// <summary>
        /// Method <c>GenerateCodeLines</c> creates C-code lines corresponding to the ast.
        /// </summary>
        private void GenerateCodeLines()
        {
            if (ast == null) return;

            ast.GenerateCode(codeLines);
        }

        /// <summary>
        /// Method <c>WriteCodeLinesToFile</c> writes code lines from list to output file.
        /// </summary>
        private void WriteCodeLinesToFile()
        {
            if (outputFilePath == null) return;

            // TODO
            // write code lines to ouput file
        }

    }
}
