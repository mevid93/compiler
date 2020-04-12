
namespace MipaCompiler.Symbol
{
    /// <summary>
    /// Class <c>ProcedureSymbol</c> represents procedure in symbol table.
    /// </summary>
    public class ProcedureSymbol : ISymbol
    {
        private readonly string identifier;     // function name
        private readonly string[] parameters;   // array of parameter types

        /// <summary>
        /// Constructor <c>FunctionSymbol</c> creates new ProcedureSymbol-object.
        /// </summary>
        /// <param name="identifier">name of the procedure</param>
        /// <param name="parameter">parameter types</param>
        public ProcedureSymbol(string identifier, string[] parameters)
        {
            this.identifier = identifier;
            this.parameters = parameters;
        }

        public string[] GetParameterTypes()
        {
            return parameters;
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

    }
}
