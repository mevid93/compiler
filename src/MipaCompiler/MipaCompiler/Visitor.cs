using MipaCompiler.Symbol;
using System.Collections.Generic;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>Visitor</c> contains functionality to visit nodes in AST
    /// in order to generate code.
    /// </summary>
    public class Visitor
    {
        private List<string> codeLines;             // generated code lines
        private readonly SymbolTable symbolTable;   // symbol table for storing variable information
        private int temporaryVariableCounter;       // number of latest temporary variable

        /// <summary>
        /// Constructor <c>Visitor</c>
        /// </summary>
        public Visitor()
        {
            codeLines = new List<string>();
            symbolTable = new SymbolTable();
            temporaryVariableCounter = 0;
        }

        /// <summary>
        /// Method <c>GetCodeLines</c> returns the list of generator code lines.
        /// </summary>
        /// <returns>list of code lines</returns>
        public List<string> GetCodeLines()
        {
            return codeLines;
        }

        /// <summary>
        /// Method <c>AddCodeLine</c> adds new code line to list of code lines.
        /// </summary>
        public void AddCodeLine(string codeLine)
        {
            codeLines.Add(codeLine);
        }

        /// <summary>
        /// Method <c>GetSymbolTable</c> returns the symbol table.
        /// </summary>
        /// <returns>symbbol table</returns>
        public SymbolTable GetSymbolTable()
        {
            return symbolTable;
        }
    }
}
