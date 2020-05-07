using MipaCompiler.Symbol;
using System.Collections.Generic;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>Visitor</c> contains functionality to store information about
    /// code generation process while it visits the nodes in AST during code generation.
    /// </summary>
    public class Visitor
    {
        private List<string> codeLines;             // generated code lines
        private readonly SymbolTable symbolTable;   // symbol table for storing variable, procedure and function information
        private int whileCounter;                   // counter for while loop label
        private int ifCounter;                      // counter for if-else structure label
        private string latestTmpVariable;           // latest tmp variable name used
        private int tmpVariableCounter;             // counter for tmp variable
        private bool isMainBlock;                   // is current block main function
        private List<string> allocateArrays;        // list of arrays that are allocated and need to be freed
        private List<string> allocatedStrings;      // list of string that are allocated and need to be freed

        /// <summary>
        /// Constructor <c>Visitor</c> creates new Visitor-object.
        /// </summary>
        public Visitor()
        {
            codeLines = new List<string>();
            symbolTable = new SymbolTable();
            allocateArrays = new List<string>();
            allocatedStrings = new List<string>();
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
        /// Method <c>AddCodeLine</c> stores new code line to list of generated code lines.
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
        /// Method <c>GetWhileCounter</c> returns the next available while loop lable number.
        /// </summary>
        /// <returns>while loop counter value</returns>
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
        /// Method <c>GetTmpVariableCounter</c> returns the next available tmp variable number.
        /// </summary>
        /// <returns>tmp variable counter value</returns>
        public int GetTmpVariableCounter()
        {
            return tmpVariableCounter;
        }

        /// <summary>
        /// Method <c>IncreaseTmpVariableCounter</c> increases the tmp variable counter.
        /// </summary>
        public void IncreaseTmpVariableCounter()
        {
            tmpVariableCounter++;
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
        /// Method <c>GetIfStructureCounter</c> returns the next available if-else lable number.
        /// </summary>
        /// <returns>if structure counter value</returns>
        public int GetIfStructureCounter()
        {
            return ifCounter;
        }

        /// <summary>
        /// Method <c>IncreaseIfStructureCounter</c> increases the if-else label counter.
        /// </summary>
        public void IncreaseIfStructureCounter()
        {
            ifCounter++;
        }

        /// <summary>
        /// Method <c>IsBlockMainFunction</c> returns true if current block is the main function.
        /// </summary>
        /// <returns>true if block is main function</returns>
        public bool IsBlockMainFunction()
        {
            return isMainBlock;
        }

        /// <summary>
        /// Method <c>SetIsBlockMainFunction</c> set the flag that tells if current block
        /// is main function.
        /// </summary>
        /// <param name="isMainBlock">new value</param>
        public void SetIsBlockMainFunction(bool isMainBlock)
        {
            this.isMainBlock = isMainBlock;
        }

        /// <summary>
        /// Method <c>GetAllocatedArrays</c> returns list of allocated arrays that need to
        /// be freed.
        /// </summary>
        /// <returns>list of allocated arrays</returns>
        public List<string> GetAllocatedArrays()
        {
            return allocateArrays;
        }

        /// <summary>
        /// Method <c>AddAllocatedArray</c> inserts new array name to list of allocated arrays.
        /// </summary>
        /// <param name="array">name of new array</param>
        public void AddAllocatedArray(string array)
        {
            allocateArrays.Add(array);
        }
        
        /// <summary>
        /// Method <c>GetAllocatedStrings</c> returns list of allocated strings that need to
        /// be freed.
        /// </summary>
        /// <returns>list of allocated arrays</returns>
        public List<string> GetAllocatedStrings()
        {
            return allocatedStrings;
        }

        /// <summary>
        /// Method <c>AddAllocatedArray</c> inserts new array name to list of allocated arrays.
        /// </summary>
        /// <param name="allocatedString">name of new string</param>
        public void AddAllocatedString(string allocatedString)
        {
            allocatedStrings.Add(allocatedString);
        }
        
    }
}
