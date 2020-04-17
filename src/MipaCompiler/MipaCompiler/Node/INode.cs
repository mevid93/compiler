
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Enum <c>NodeType</c> represents the type of node.
    /// </summary>
    public enum NodeType
    {
        ARRAY_SIZE,         // node for holding array size operation
        ARRAY_INDEX,        // node for holding index of array operation
        ARRAY_TYPE,         // node for array type
        ASSERT,             // node for assert statement
        ASSIGNMENT,         // node for assignment operation
        BLOCK,              // node for code block
        CALL,               // node for calling functions and procedures
        BINARY_EXPRESSION,  // expression node with two child nodes
        FUNCTION,           // function declaration node
        IF_ELSE,            // node for if else statement
        INTEGER,            // node for holding constant integer
        PROCEDURE,          // procedure declaratio node
        PROGRAM,            // program declaration node
        REAL,               // node for holding constant real
        RETURN,             // node for return statement
        SIGN,               // node for defining sign of term
        SIMPLE_TYPE,        // node for simple type
        STRING,             // node for holding constant string
        UNARY_EXPRESSION,   // node for unary expression
        VARIABLE,           // node holding a variable
        VARIABLE_DCL,       // node for variable declaration
        WHILE,              // node for while loop
    }

    /// <summary>
    /// Interface <c>INode</c> is node interface that defines all the methods that are 
    /// required of different nodes in AST.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// Method <c>GetNodeType</c> returns the type of node.
        /// </summary>
        /// <returns>the type of node</returns>
        NodeType GetNodeType();

        /// <summary>
        /// Method <c>PrettyPrint</c> prints the string representation of node.
        /// </summary>
        void PrettyPrint();

        /// <summary>
        /// Method <c>GetRow</c> returns the row in source code that corresponds the node in AST.
        /// </summary>
        int GetRow();

        /// <summary>
        /// Method <c>GetCol</c> returns the column in source code that corresponds the node in AST.
        /// </summary>
        int GetCol();

        /// <summary>
        /// Method <c>GenerateCode</c> generated code corresponding to the node and
        /// stores it to the list of code lines.
        /// </summary>
        /// <param name="codeLines">generated code lines</param>
        void GenerateCode(List<string> codeLines);
    }

}
