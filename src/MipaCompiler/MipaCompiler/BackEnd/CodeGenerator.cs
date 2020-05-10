using MipaCompiler.Node;
using System;
using System.Collections.Generic;
using System.IO;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>CodeGenerator</c> contains functioncality to generate C-program code file.
    /// </summary>
    public class CodeGenerator
    {
        private readonly string outputFilePath;             // output file path
        private readonly INode ast;                         // abstract syntax tree
        private readonly Visitor visitor;                   // visitor for processing ast

        /// <summary>
        /// Constructor <c>CodeGenerator</c> creates new CodeGenerator-object.
        /// </summary>
        /// <param name="outputFilePath">name of the output file</param>
        /// <param name="ast">AST</param>
        public CodeGenerator(string outputFilePath, INode ast)
        {
            visitor = new Visitor();
            this.outputFilePath = outputFilePath;
            this.ast = null;
            if (ast != null && ast.GetNodeType() == NodeType.PROGRAM) this.ast = ast;
        }

        /// <summary>
        /// Method <c>Generate</c> will cenerate a low level C-program file
        /// that corresponds to the AST.
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
            return visitor.GetCodeLines();
        }

        /// <summary>
        /// Method <c>GenerateCodeLines</c> creates C-code lines corresponding to the ast.
        /// </summary>
        private void GenerateCodeLines()
        {
            if (ast == null) return;

            ast.GenerateCode(visitor);
        }

        /// <summary>
        /// Method <c>WriteCodeLinesToFile</c> writes code lines from list to output file.
        /// </summary>
        private void WriteCodeLinesToFile()
        {
            // make sure that ouput file is defined
            if (outputFilePath == null) return;

            // write generated code lines to ouputfile
            File.WriteAllLines(outputFilePath, visitor.GetCodeLines());
        }
        
    }
}
