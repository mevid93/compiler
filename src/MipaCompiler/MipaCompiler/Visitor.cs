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
        private int whileCounter;                   // counter of latest while loop label
        private int ifCounter;                      // counter of if-else structure label
        private string latestTmpVariable;           // latest temporary variable name used
        private int tempVariableCounter;            // counter for latest temporary variable

        /// <summary>
        /// Constructor <c>Visitor</c>
        /// </summary>
        public Visitor()
        {
            codeLines = new List<string>();
            symbolTable = new SymbolTable();
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

        /// <summary>
        /// Method <c>GetWhileCounter</c> returns the latest while loop label counter.
        /// </summary>
        /// <returns>latest while loop label counter</returns>
        public int GetWhileCounter()
        {
            return whileCounter;
        }

        /// <summary>
        /// Method <c>IncreaseWhileCounter</c> increases the while loop label counter.
        /// </summary>
        public void IncreaseWhileCounter()
        {
            whileCounter += 1;
        }

        /// <summary>
        /// Method <c>GetTempVariableCounter</c> returns the counter for latest temporary variable.
        /// </summary>
        /// <returns>temp variable counter</returns>
        public int GetTempVariableCounter()
        {
            return tempVariableCounter;
        }

        /// <summary>
        /// Method <c>IncreaseTempVariableCounter</c> increases the temp variable counter.
        /// </summary>
        public void IncreaseTempVariableCounter()
        {
            tempVariableCounter++;
        }

        /// <summary>
        /// Method <c>GetLatestUsedTmpVariable</c> returns the name of the latest tmp variable used.
        /// </summary>
        /// <returns>tmp variable name</returns>
        public string GetLatestUsedTmpVariable()
        {
            return latestTmpVariable;
        }

        /// <summary>
        /// Method <c>SetLatestTmpVariableName</c> sets the name of the latest tmp variable.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public void SetLatestTmpVariableName(string name)
        {
            latestTmpVariable = name;
        }

        /// <summary>
        /// Method <c>GetIfStructureCounter</c> returns the counter of if structure labels.
        /// </summary>
        /// <returns>if strucutre counter</returns>
        public int GetIfStructureCounter()
        {
            return ifCounter;
        }

        /// <summary>
        /// Method <c>IncreaseIfStructureCounter</c> increases the if structure label counter.
        /// </summary>
        public void IncreaseIfStructureCounter()
        {
            ifCounter++;
        }
    }
}
