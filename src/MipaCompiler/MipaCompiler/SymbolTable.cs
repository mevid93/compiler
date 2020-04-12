using System.Collections.Generic;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>SymbolTable</c> represents the datastructure of semantic analysis
    /// where smybols and their status are stored.
    /// </summary>
    public class SymbolTable
    {
        private List<Symbol> symbols;       // list of all symbols
        private int currentScope;           // scope that is under analysis

        /// <summary>
        /// Constructor <c>SymbolTable</c> creates SymbolTable-object.
        /// </summary>
        public SymbolTable()
        {
            symbols = new List<Symbol>();
        }

        /// <summary>
        /// Method <c>AddScope</c> adds new scope level.
        /// </summary>
        public void AddScope()
        {
            currentScope++;
        }

        /// <summary>
        /// Method <c>RemoveScope</c> removes last scope level.
        /// </summary>
        public void RemoveScope()
        {
            // pop scope from all symbols where current scope is top one
            // if was last scope of symbol, remove the symbol
            for(int i = symbols.Count - 1; i >= 0; i--)
            {
                Symbol tmp = symbols[i];
                if(tmp.GetCurrentScope() == currentScope)
                {
                    int next = tmp.PopScope();
                    if (next == -1) symbols.Remove(tmp);
                }
            }

            // remove the last scope
            currentScope--;
        }

        /// <summary>
        /// Method <c>GetCurrentScope</c> returns the current scope of symbol table.
        /// </summary>
        public int GetCurrentScope()
        {
            return currentScope;
        }

        /// <summary>
        /// Method <c>IsSymbolInTable</c> checks if given symbol is in symbol table (stack).
        /// </summary>
        public bool IsSymbolInTable(string identifier)
        {
            foreach (Symbol s in symbols)
            {
                if (s.GetIdentifier().Equals(identifier))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method <c>DeclareSymbol</c> adds the symbol to symbol table.
        /// </summary>
        public void DeclareSymbol(Symbol newSymbol)
        {
            symbols.Add(newSymbol);
        }

        /// <summary>
        /// Method <c>GetSymbolByIdentifier</c> returns the symbol that matches the identifier.
        /// Returns the symbol with highest scope.
        /// If nor symbol is matched, null is returned.
        /// </summary>
        /// <returns>symbol</returns>
        public Symbol GetSymbolByIdentifier(string identifier)
        {
            Symbol symbol = null;

            // find correct symbol from symbol table.
            // same identifier can be used multiple time in different scopes,
            // which is why we have to find the correct one by iterating all symbols.
            // correct one is the one with highest scope
            foreach (Symbol s in symbols)
            {
                if (s.GetIdentifier().Equals(identifier))
                {
                    if (symbol == null)
                    {
                        symbol = s;
                    }
                    else if (s.GetCurrentScope() > symbol.GetCurrentScope())
                    {
                        symbol = s;
                    }
                }
            }

            return symbol;
        }

        /// <summary>
        /// Method <c>UpdateSymbol</c> updates the value of symbol corresponding to given identifier.
        /// This will update the symbol with highest scope in allowed scope range.
        /// </summary>
        /// <param name="identifier">symbol to be updated</param>
        /// <param name="value">new value for symbol</param>
        /// <param name="minScope">minimum scope value that variable must have</param>
        public void UpdateSymbol(string identifier, string value, int minScope)
        {
            Symbol symbol = null;

            // find correct symbol from symbol table.
            // same identifier can be used multiple time in different scopes,
            // which is why we have to find the correct one by iterating all symbols.
            // correct one is the one with highest scope
            foreach (Symbol s in symbols)
            {
                if (s.GetIdentifier().Equals(identifier) && s.GetCurrentScope() >= minScope)
                {
                    if (symbol == null)
                    {
                        symbol = s;
                    }
                    else if(s.GetCurrentScope() > symbol.GetCurrentScope())
                    {
                        symbol = s;
                    }
                }
            }

            // symbol not found
            if (symbol == null) return;

            // symbol was found --> update value
            symbol.SetValue(value);
        }
    }
}
