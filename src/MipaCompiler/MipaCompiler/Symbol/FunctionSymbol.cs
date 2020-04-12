
namespace MipaCompiler.Symbol
{
    /// <summary>
    /// Class <c>FunctionSymbol</c> represents function in SymbolTable.
    /// </summary>
    public class FunctionSymbol
    {
        private readonly string identifier;     // function name
        private readonly string[] args;         // array of argument types
        private readonly string returnType;     // return value type

        /// <summary>
        /// Constructor <c>FunctionSymbol</c> creates new FunctionSymbol-object.
        /// </summary>
        /// <param name="identifier">name of the function</param>
        /// <param name="args">argument types</param>
        /// <param name="returnType">return type</param>
        public FunctionSymbol(string identifier, string[] args, string returnType)
        {
            this.identifier = identifier;
            this.args = args;
            this.returnType = returnType;
        }

        /// <summary>
        /// Method <c>GetIdentifier</c> returns the function name.
        /// </summary>
        /// <returns>function name</returns>
        public string GetIdentifier()
        {
            return identifier;
        }

        /// <summary>
        /// Method <c>GetArgumentTypes</c> returns an array of argument types.
        /// </summary>
        /// <returns>argument types</returns>
        public string[] GetArgumentTypes()
        {
            return args;
        }

        /// <summary>
        /// Method <c>GetReturnType</c> returns the return type of function.
        /// </summary>
        /// <returns>return type</returns>
        public string GetReturnType()
        {
            return returnType;
        }

        /// <summary>
        /// Method <c>HasSameDefinition</c> returns true if function has
        /// same identifier and argument types array.
        /// </summary>
        /// <returns>true if match</returns>
        public bool HasSameDefinition(FunctionSymbol f)
        {
            if (!f.GetIdentifier().Equals(identifier)) return false;

            string[] types1 = f.GetArgumentTypes();

            if (types1.Length != args.Length) return false;
            
            for(int i = 0; i < types1.Length; i++)
            {
                if (!types1[i].Equals(args[i])) return false;
            }

            return true;
        }
    }
}
