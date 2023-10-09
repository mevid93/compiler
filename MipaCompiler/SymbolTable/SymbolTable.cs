using System.Collections.Generic;

namespace MipaCompiler.Symbol
{
    /// <summary>
    /// Class <c>SymbolTable</c> represents the datastructure used to store variable symbols,
    /// function symbols, and procedure symbols. It is used in semantic analysis and code generation.
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
        /// Method <c>RemoveScope</c> removes most recent scope level.
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
                    tmp.PopType();
                    if (next == -1) variables.Remove(tmp);
                    if (next == -1)
                    {
                        tmp.PopType();
                        tmp.PopPointerInfo();
                    }
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
                if (s.GetIdentifier().Equals(identifier) && s.GetCurrentScope() <= currentScope)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method <c>IsFunctionSymbolInTable</c> checks if given function is
        /// already in table.
        /// </summary>
        /// <param name="symbol">function symbol</param>
        /// <returns>true if function in table</returns>
        public bool IsFunctionSymbolInTable(FunctionSymbol symbol)
        {
            foreach(FunctionSymbol f in functions)
            {
                if (f.GetIdentifier().Equals(symbol.GetIdentifier())) return true;
            }

            return false;
        }

        /// <summary>
        /// Method <c>IsProcedureSymbolInTable</c> checks if given procedure is in table.
        /// </summary>
        /// <param name="symbol">procedure symbol</param>
        /// <returns>true if procedure in table</returns>
        public bool IsProcedureSymbolInTable(ProcedureSymbol symbol)
        {
            foreach (ProcedureSymbol p in procedures)
            {
                if (p.GetIdentifier().Equals(symbol.GetIdentifier())) return true;
            }

            return false;
        }

        /// <summary>
        /// Method <c>IsFunctionInTable</c> checks if there exists a function with 
        /// given identifier in table.
        /// </summary>
        /// <param name="identifier">function identifier</param>
        /// <returns>true if function exists</returns>
        public bool IsFunctionInTable(string identifier)
        {
            foreach (FunctionSymbol f in functions)
            {
                if (f.GetIdentifier().Equals(identifier)) return true;
            }

            return false;
        }

        /// <summary>
        /// Method <c>IsProcedureInTable</c> checks if there exists a procedure with 
        /// given identifier in table.
        /// </summary>
        /// <param name="identifier">procedure identifier</param>
        /// <returns>true if procedure exists</returns>
        public bool IsProcedureInTable(string identifier)
        {
            foreach (ProcedureSymbol p in procedures)
            {
                if (p.GetIdentifier().Equals(identifier)) return true;
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
        /// Method <c>ReDeclareVariableSymbol</c> declares variable with new scope and type.
        /// </summary>
        public void ReDeclareVariableSymbol(VariableSymbol updateSymbol)
        {
            foreach(VariableSymbol varSym in variables)
            {
                if (varSym.GetIdentifier().Equals(updateSymbol.GetIdentifier()))
                {
                    // push new scope
                    varSym.PushScope(updateSymbol.GetCurrentScope());

                    // push new type
                    varSym.PushType(updateSymbol.GetSymbolType());

                    // push new pointer info
                    varSym.PushPointerInfo(updateSymbol.IsPointer());
                }
            }
        }

        /// <summary>
        /// Method <c>DeclareFunctionSymbol</c> stores the function symbol to the symbol table.
        /// </summary>
        /// <param name="newSymbol">new function symbol</param>
        public void DeclareFunctionSymbol(FunctionSymbol newSymbol)
        {
            functions.Add(newSymbol);
        }

        /// <summary>
        /// Method <c>DeclareProcedureFunction</c> stores the procedure symbol to symbol table.
        /// </summary>
        /// <param name="newSymbol">new procdure symbol</param>
        public void DeclareProcedureSymbol(ProcedureSymbol newSymbol)
        {
            procedures.Add(newSymbol);
        }

        /// <summary>
        /// Method <c>GetVariableSymbolByIdentifier</c> returns the variable symbol that matches the identifier.
        /// </summary>
        /// <returns>varaible symbol</returns>
        public VariableSymbol GetVariableSymbolByIdentifier(string identifier)
        {
            foreach(VariableSymbol symbol in variables)
            {
                if (symbol.GetIdentifier().Equals(identifier)) return symbol;
            }
            return null;
        }

        /// <summary>
        /// Method <c>GetFunctionSymbolByIdentifierAndArguments</c> returns the function symbol
        /// that matches parameters. If no match was found, null is returned.
        /// </summary>
        /// <param name="identifier">function name</param>
        /// <param name="parameters">function parameters (types)</param>
        /// <returns>function symbol</returns>
        public FunctionSymbol GetFunctionSymbolByIdentifierAndArguments(string identifier, string[] parameters)
        {
            FunctionSymbol f = new FunctionSymbol(identifier, parameters, null);

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
        /// <param name="parameters">procedure parameters (types)</param>
        /// <returns>procedure symbol</returns>
        public ProcedureSymbol GetProcedureSymbolByIdentifierAndArguments(string identifier, string[] parameters)
        {
            ProcedureSymbol p = new ProcedureSymbol(identifier, parameters);

            foreach (ProcedureSymbol symbol in procedures)
            {
                if (symbol.HasSameDefinition(p)) return symbol;
            }

            return null;
        }

        /// <summary>
        /// Method <c>IsVariableSymbolInTableWithCurrentScope</c> checks if variable symbol
        /// is in table and it has current scope as its scope.
        /// </summary>
        public bool IsVariableSymbolInTableWithCurrentScope(string identifier)
        {
            foreach (VariableSymbol s in variables)
            {
                if (s.GetIdentifier().Equals(identifier) && s.GetCurrentScope() == currentScope)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method <c>ResetScope</c> resets the scope of symbol table.
        /// </summary>
        public void ResetScope()
        {
            while (currentScope > 0) RemoveScope();
        }

        /// <summary>
        /// Method <c>GetFunctionSymbolByIdentifier</c> returns the function that matches the
        /// given identifier.
        /// </summary>
        /// <param name="identifier">function identifier</param>
        /// <returns>function symbol</returns>
        public FunctionSymbol GetFunctionSymbolByIdentifier(string identifier)
        {
            foreach(FunctionSymbol f in functions)
            {
                if (f.GetIdentifier().Equals(identifier)) return f;
            }

            return null;
        }

        /// <summary>
        /// Method <c>GetProcedureSymbolByIdentifier</c> returns the procedue that matches the
        /// given identifier.
        /// </summary>
        /// <param name="identifier">procedure identifier</param>
        /// <returns>procedure symbol</returns>
        public ProcedureSymbol GetProcedureSymbolByIdentifier(string identifier)
        {
            foreach (ProcedureSymbol p in procedures)
            {
                if (p.GetIdentifier().Equals(identifier)) return p;
            }

            return null;
        }
    }
}
