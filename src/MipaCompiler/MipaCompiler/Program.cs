using System;
using System.IO;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>Program</c> is the Driver-class for the compiler.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Static method <c>Main</c> starts the Mini-Pascal compiler.
        /// </summary>
        /// <param name="args">input parameters (source code file)</param>
        static int Main(string[] args)
        {
            // user must provide path to source code file as input parameter
            if (args.Length == 0)
            {
                Console.WriteLine($"IOError::Please provide a path to Mini-Pascal source file!");
                Console.WriteLine("Usage: <compiler.exe> <sourcecode.txt> [<targetcode.c>]");
                return -1;
            }

            // check that input source code file exists
            string sourceFilePath = args[0];
            if (!File.Exists(sourceFilePath))
            {
                Console.WriteLine($"IOError::Invalid source code file. File not found!");
                return -1;
            }

            // file exists, so set default output file name according to it
            string outputFilePath = Path.ChangeExtension(sourceFilePath, ".c");

            // if outputfile name/path was given, check that the extension is .c
            if (args.Length == 2)
            {
                string output = args[1];
                string extension = Path.GetExtension(output);
                if (extension != null && extension.Equals(".c"))
                {
                    outputFilePath = args[1];
                }
                else
                {
                    Console.WriteLine($"IOError::Extension of the target code file is not valid (must be \".c\")!");
                    return -1;
                }
            }


            // create Scanner-object for lexical analysis
            Scanner scanner = new Scanner(sourceFilePath);

            // create Parser-object for syntax analysis
            Parser parser = new Parser(scanner);

            // syntax analysis and create AST intermediate representation
            // List<INode> ast = parser.Parse();

            // semantic analysis
            // Semantix semalys = new Semantix(ast);
            // semalys.CheckConstraints();

            // check that no errors were detected in source code
            // if (parser.NoErrorsDetected() && semalys.NoErrorsDetected())
            // {
                // code generation ...
                // TODO!
            // }

            return 0;
        }
    }
}
