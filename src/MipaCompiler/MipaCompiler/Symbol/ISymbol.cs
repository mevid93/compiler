﻿
namespace MipaCompiler.Symbol
{
    /// <summary>
    /// Interface <c>ISymbol</c> defines methods that each symbol must define.
    /// </summary>
    public interface ISymbol
    {
        /// <summary>
        /// Method <c>GetParameterTypes</c> returns an array of parameter types.
        /// </summary>
        /// <returns>parameter types</returns>
        string[] GetParameterTypes();

        /// <summary>
        /// Method <c>HasSameDefinition</c> checks if two ISymbol instances have same
        /// definition. In other words, it checks if procedures or functions have
        /// identical declarations.
        /// </summary>
        /// <param name="symbol">symbol to compare</param>
        /// <returns>true if identical</returns>
        bool HasSameDefinition(ISymbol symbol);

        /// <summary>
        /// Method <c>GetIdentifier</c> returns symbol identifier.
        /// </summary>
        /// <returns>symbol identifier</returns>
        string GetIdentifier();
    }
}
