using MipaCompiler.Symbol;
using Xunit;

namespace MipaCompilerTests.SymbolTests
{
    public class ProcedureSymbolTests
    {
        [Fact]
        public void ProcedureSymbolWorks()
        {
            string[] args = { "integer", "string", "boolean", "real" };

            ProcedureSymbol functionSymbol = new ProcedureSymbol("CustomProcedure", args);

            Assert.Equal("CustomProcedure", functionSymbol.GetIdentifier());
            Assert.Equal("integer", functionSymbol.GetParameterTypes()[0]);
            Assert.Equal("real", functionSymbol.GetParameterTypes()[3]);
        }

        [Fact]
        public void HasSameDefinitionWorks()
        {
            string[] args = { "integer", "string", "boolean", "real" };
            string[] args1 = { "integer", "string", "boolean", "integer" };
            string[] args2 = { };

            ProcedureSymbol procedureSymbol = new ProcedureSymbol("CustomFunction", args);
            ProcedureSymbol procedureSymbol2 = new ProcedureSymbol("CustomFunction", args);
            ProcedureSymbol procedureSymbol3 = new ProcedureSymbol("CustomFunction3", args);
            ProcedureSymbol procedureSymbol4 = new ProcedureSymbol("CustomFunction4", args1);
            ProcedureSymbol procedureSymbol5 = new ProcedureSymbol("CustomFunction5", args2);

            Assert.True(procedureSymbol.HasSameDefinition(procedureSymbol2));
            Assert.False(procedureSymbol.HasSameDefinition(procedureSymbol3));
            Assert.False(procedureSymbol.HasSameDefinition(procedureSymbol4));
            Assert.False(procedureSymbol.HasSameDefinition(procedureSymbol5));
        }
    }
}
