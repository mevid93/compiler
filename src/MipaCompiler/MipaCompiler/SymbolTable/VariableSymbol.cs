﻿
using System.Collections.Generic;

namespace MipaCompiler.Symbol
{
    /// <summary>
    /// Class <c>Symbol</c> represents single variable in the symbol table.
    /// </summary>
    public class VariableSymbol : ISymbol
    {
        private readonly string identifier;         // variable symbol  (identifier)
        private Stack<string> types;                // variable type  (string representation)
        private string currentValue;                // value that symbols is currently holding
        private Stack<int> scopes;                  // scope of variable (lower means wider scope)
        private Stack<bool> isPointer;              // is variable a pointer in scope

        /// <summary>
        /// Constructor <c>Symbol</c> creates new Symbol-object.
        /// </summary>
        /// <param name="identifier">identifier of symbol</param>
        /// <param name="type">type os symbol (string representation)</param>
        /// <param name="currentValue">value of symbol</param>
        /// <param name="scope">scope of symbol</param>
        /// <param name="isPointer">is variable a pointer in C-language</param>
        public VariableSymbol(string identifier, string type, string currentValue, int scope, bool isPointer=false)
        {
            this.identifier = identifier;
            types = new Stack<string>();
            types.Push(type);
            this.currentValue = currentValue;
            scopes = new Stack<int>();
            scopes.Push(scope);
            this.isPointer = new Stack<bool>();
            this.isPointer.Push(isPointer);
        }

        /// <summary>
        /// Method <c>GetSymbolType</c> returns the type of value the symbol in string representation.
        /// </summary>
        /// <returns>type of symbol</returns>
        public string GetSymbolType()
        {
            if (types.Count == 0) return null;
            return types.Peek();
        }

        /// <summary>
        /// Method <c>GetCurrentValue</c> returns the value the symbol is currently holding.
        /// </summary>
        /// <returns>value of symbol</returns>
        public string GetCurrentValue()
        {
            return currentValue;
        }

        /// <summary>
        /// Method <c>SetValue</c> sets the value of symbol.
        /// </summary>
        public void SetValue(string newValue)
        {
            currentValue = newValue;
        }

        /// <summary>
        /// Method <c>GetCurrentScope</c> returns the current scope of symbol.
        /// </summary>
        /// <returns>scope of symbol</returns>
        public int GetCurrentScope()
        {
            if (scopes.Count == 0) return -1;
            return scopes.Peek();
        }

        /// <summary>
        /// Method <c>PushScope</c> pushes new scope to scope stack.
        /// </summary>
        public void PushScope(int scope)
        {
            scopes.Push(scope);
        }

        /// <summary>
        /// Method <c>PopScope</c> pops scope from the scope stack and
        /// returns the value of next scope. If no more scopes are
        /// available, -1 is returned.
        /// </summary>
        /// <returns>scope after pop</returns>
        public int PopScope()
        {
            if (scopes.Count == 0) return -1;
            scopes.Pop();
            if (scopes.Count == 0) return -1;
            return scopes.Peek();
        }

        /// <summary>
        /// Method <c>PopType</c> pops type from the types stack and
        /// returns the value of next type. If no more types are
        /// available, -1 is returned.
        /// </summary>
        /// <returns>type after pop</returns>
        public string PopType()
        {
            if (types.Count == 0) return null;
            types.Pop();
            if (types.Count == 0) return null;
            return types.Peek();
        }

        /// <summary>
        /// Method <c>PushScope</c> pushes new scope to scope stack.
        /// </summary>
        public void PushType(string type)
        {
            types.Push(type);
        }

        public string[] GetParameterTypes()
        {
            return types.ToArray();
        }

        public bool HasSameDefinition(ISymbol symbol)
        {
            // NOT NEEDED FOR VARIABLE SYMBOLS
            // AT LEST FOR CURRENT IMPLEMENTATION
            throw new System.NotImplementedException();
        }

        public string GetIdentifier()
        {
            return identifier;
        }

        /// <summary>
        /// Method <c>IsPointer</c> returns true if variable is a pointer in C-language.
        /// </summary>
        /// <returns>true if variable is pointer</returns>
        public bool IsPointer()
        {
            return isPointer.Peek();
        }

        /// <summary>
        /// Method <c>PushPointerInfo</c> stores new pointer info to stack.
        /// </summary>
        /// <param name="info">pointer info</param>
        public void PushPointerInfo(bool info)
        {
            isPointer.Push(info);
        }

        /// <summary>
        /// Method <c>PopPointerInfo</c> removes latest pointer info from stack.
        /// </summary>
        public void PopPointerInfo()
        {
            isPointer.Pop();
        }
  
    }
}
