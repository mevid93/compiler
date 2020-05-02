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
        private readonly Visitor visitor;                   // visitor for processing ast

        /// <summary>
        /// Constructor <c>CodeGenerator</c>
        /// </summary>
        /// <param name="outputFilePath"></param>
        /// <param name="ast">abstract syntax tree</param>
        public CodeGenerator(string outputFilePath, INode ast)
        {
            visitor = new Visitor();
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
            if (outputFilePath == null) return;

            // TODO
            // write code lines to ouput file
        }

        ///////////////////////// STATIC GENERAL CODE GENERATION METHODS /////////////////////////

        /// <summary>
        /// Static method <c>ConvertTypeToTargetLanguage</c> convertos Mini-Pascal type
        /// to C-language type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>C-language type</returns>
        public static string ConvertParameterTypeToTargetLanguage(string type)
        {
            switch (type)
            {
                case "string":
                    return "const char *";
                case "real":
                    return "double *";
                case "integer":
                    return "int *";
                case "boolean":
                    return "bool *";
                case "array[] of string":
                    return "char **";
                case "array[] of real":
                    return "double *";
                case "array[] of integer":
                    return "integer *";
                case "array[] of boolean":
                    return "bool *";
                default:
                    throw new Exception("Unexpected error... Invalid type!");
            }
        }

        /// <summary>
        /// Static method <c>ConvertSimpleTypeToTargetLanguage</c> converts simple type to 
        /// C-language equivalent.
        /// </summary>
        /// <param name="simpleType">simple type in Mini-Pascal</param>
        /// <returns>simple type</returns>
        public static string ConvertSimpleTypeToTargetLanguage(string simpleType)
        {
            switch (simpleType)
            {
                case "string":
                    return "const char *";
                case "real":
                    return "double";
                case "integer":
                    return "int";
                case "boolean":
                    return "bool";
                default:
                    throw new Exception("Unexpected error... Invalid simple type!");
            }
        }
    }
}
