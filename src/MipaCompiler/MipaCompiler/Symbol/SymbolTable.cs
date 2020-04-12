using System.Collections.Generic;

namespace MipaCompiler.Symbol
{
    /// <summary>
    /// Class <c>SymbolTable</c> represents the datastructure of semantic analysis
    /// where smybols and their status are stored. Functions and procedures are
    /// also stored into symbol table.
    /// </summary>
    public class SymbolTable
    {
        private List<VariableSymbol> variables;     // list of all variable symbols
        private List<FunctionSymbol> functions;     // list of function symbols
        private List<ProcedureSymbol> procedures;   // list of procedure symbols
        private int currentScope;                   // scope that is under analysis

        /// <summary>
        /// Constructor <c>SymbolTable</c> creates SymbolTable-object.
        /// </summary>
        public SymbolTable()
        {
            variables = new List<VariableSymbol>();
            functions = new List<FunctionSymbol>();
            procedures = new List<ProcedureSymbol>();
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
            for(int i = variables.Count - 1; i >= 0; i--)
            {
                VariableSymbol tmp = variables[i];
                if(tmp.GetCurrentScope() == currentScope)
                {
                    int next = tmp.PopScope();
                    if (next == -1) variables.Remove(tmp);
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
        /// Method <c>IsSymbolInTable</c> checks if given variable is in table.
        /// </summary>
        public bool IsVariableSymbolInTable(string identifier)
        {
            foreach (VariableSymbol s in variables)
            {
                if (s.GetIdentifier().Equals(identifier))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method <c>IsFunctionSymbolInTable</c> checks if given function is in table.
        /// Both function name and arguments must match.
        /// </summary>
        /// <returns>true if function in table</returns>
        public bool IsFunctionSymbolInTable(FunctionSymbol symbol)
        {
            foreach(FunctionSymbol f in functions)
            {
                if (f.HasSameDefinition(symbol)) return true;
            }

            return false;
        }

        /// <summary>
        /// Method <c>IsProcedureSymbolInTable</c> checks if given procedure is in table.
        /// </summary>
        /// <param name="procedureName">name of the procedure</param>
        /// <param name="args">arguments for procedure</param>
        /// <returns>true if procedure in table</returns>
        public bool IsProcedureSymbolInTable(ProcedureSymbol symbol)
        {
            foreach (ProcedureSymbol p in procedures)
            {
                if (p.HasSameDefinition(symbol)) return true;
            }

            return false;
        }

        /// <summary>
        /// Method <c>DeclareSymbol</c> adds the symbol to symbol table.
        /// </summary>
        /// <param name="newSymbol">new symbol</param>
        public void DeclareVariableSymbol(VariableSymbol newSymbol)
        {
            variables.Add(newSymbol);
        }

        /// <summary>
        /// Method <c>DeclareFunctionSymbol</c> adds the symbol to symbol table.
        /// </summary>
        /// <param name="newSymbol">new symbol</param>
        public void DeclareFunctionSymbol(FunctionSymbol newSymbol)
        {
            functions.Add(newSymbol);
        }

        /// <summary>
        /// Method <c>DeclareProcedureFunction</c> adds the symbol to symbol table.
        /// </summary>
        /// <param name="newSymbol">new symbol</param>
        public void DeclareProcedureSymbol(ProcedureSymbol newSymbol)
        {
            procedures.Add(newSymbol);
        }

        /// <summary>
        /// Method <c>GetVariableSymbolByIdentifier</c> returns the variable symbol that matches the identifier.
        /// Returns the symbol with highest scope. If nor symbol is matched, null is returned.
        /// </summary>
        /// <returns>varaible symbol</returns>
        public VariableSymbol GetVariableSymbolByIdentifier(string identifier)
        {
            VariableSymbol symbol = null;

            // find correct symbol from symbol table.
            // same identifier can be used multiple time in different scopes,
            // which is why we have to find the correct one by iterating all symbols.
            // correct one is the one with highest scope
            foreach (VariableSymbol s in variables)
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
        /// Method <c>GetFunctionSymbolByIdentifierAndArguments</c> returns the function symbol
        /// that matches parameters. If no match was found, null is returned.
        /// </summary>
        /// <param name="identifier">function name</param>
        /// <param name="args">function arguments (types)</param>
        /// <returns>function symbol</returns>
        public FunctionSymbol GetFunctionSymbolByIdentifierAndArguments(string identifier, string[] args)
        {
            FunctionSymbol f = new FunctionSymbol(identifier, args, null);

            foreach(FunctionSymbol symbol in functions)
            {
                if (symbol.HasSameDefinition(f)) return symbol;
            }

            return null;
        }

        /// <summary>
        /// Method <c>GetProcedureSymbolByIdentifierAndArguments</c> returns the procedure symbol
        /// that matches parameters. If no match was found, null is returned.
        /// </summary>
        /// <param name="identifier">procedure name</param>
        /// <param name="args">procedure arguments (types)</param>
        /// <returns>function symbol</returns>
        public ProcedureSymbol GetProcedureSymbolByIdentifierAndArguments(string identifier, string[] args)
        {
            ProcedureSymbol p = new ProcedureSymbol(identifier, args);

            foreach (ProcedureSymbol symbol in procedures)
            {
                if (symbol.HasSameDefinition(p)) return symbol;
            }

            return null;
        }

        /// <summary>
        /// Method <c>UpdateSymbol</c> updates the value of variable symbol corresponding to given identifier.
        /// This will update the symbol with highest scope in allowed scope range.
        /// </summary>
        /// <param name="identifier">variable symbol to be updated</param>
        /// <param name="value">new value for symbol</param>
        /// <param name="minScope">minimum scope value that variable must have</param>
        public void UpdateVariableSymbol(string identifier, string value, int minScope)
        {
            VariableSymbol symbol = null;

            // find correct symbol from symbol table.
            // same identifier can be used multiple time in different scopes,
            // which is why we have to find the correct one by iterating all symbols.
            // correct one is the one with highest scope
            foreach (VariableSymbol s in variables)
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
