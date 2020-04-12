
namespace MipaCompiler.Symbol
{
    /// <summary>
    /// Class <c>FunctionSymbol</c> represents function in SymbolTable.
    /// </summary>
    public class FunctionSymbol : ISymbol
    {
        private readonly string identifier;     // function name
        private readonly string[] parameters;   // array of parameters types
        private readonly string returnType;     // return value type

        /// <summary>
        /// Constructor <c>FunctionSymbol</c> creates new FunctionSymbol-object.
        /// </summary>
        /// <param name="identifier">name of the function</param>
        /// <param name="parameters">parameter types</param>
        /// <param name="returnType">return type</param>
        public FunctionSymbol(string identifier, string[] parameters, string returnType)
        {
            this.identifier = identifier;
            this.parameters = parameters;
            this.returnType = returnType;
        }

        /// <summary>
        /// Method <c>GetReturnType</c> returns the return type of function.
        /// </summary>
        /// <returns>return type</returns>
        public string GetReturnType()
        {
            return returnType;
        }

        public bool HasSameDefinition(ISymbol symbol)
        {
            if (symbol.GetType() == typeof(VariableSymbol)) return false;

            if (!symbol.GetIdentifier().Equals(identifier)) return false;

            string[] types1 = symbol.GetParameterTypes();

            if (types1.Length != parameters.Length) return false;

            for (int i = 0; i < types1.Length; i++)
            {
                if (!types1[i].Equals(parameters[i])) return false;
            }

            return true;
        }

        public string GetIdentifier()
        {
            return identifier;
        }

        public string[] GetParameterTypes()
        {
            return parameters;
        }

    }
}
