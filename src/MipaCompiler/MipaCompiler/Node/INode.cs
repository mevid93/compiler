
namespace MipaCompiler.Node
{
    /// <summary>
    /// Enum <c>NodeType</c> represents the type of node.
    /// </summary>
    public enum NodeType
    {
        PROGRAM,            // program declaration node
        FUNCTION,           // function declaration node
        PROCEDURE,          // procedure declaratio node
        BLOCK,              // block declaration node
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
    }

}
