
namespace MipaCompiler.Symbol
{
    public class ProcedureSymbol
    {
        private readonly string identifier;     // function name
        private readonly string[] args;         // array of argument types

        /// <summary>
        /// Constructor <c>FunctionSymbol</c> creates new ProcedureSymbol-object.
        /// </summary>
        /// <param name="identifier">name of the procedure</param>
        /// <param name="args">argument types</param>
        public ProcedureSymbol(string identifier, string[] args)
        {
            this.identifier = identifier;
            this.args = args;
        }

        /// <summary>
        /// Method <c>GetIdentifier</c> returns the procedure name.
        /// </summary>
        /// <returns>procedure name</returns>
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
        /// Method <c>HasSameDefinition</c> returns true if procedure has
        /// same identifier and argument types array.
        /// </summary>
        /// <returns>true if match</returns>
        public bool HasSameDefinition(ProcedureSymbol f)
        {
            if (!f.GetIdentifier().Equals(identifier)) return false;

            string[] types1 = f.GetArgumentTypes();

            if (types1.Length != args.Length) return false;

            for (int i = 0; i < types1.Length; i++)
            {
                if (!types1[i].Equals(args[i])) return false;
            }

            return true;
        }
    }
}
