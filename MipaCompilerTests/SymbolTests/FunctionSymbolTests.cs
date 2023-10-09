using MipaCompiler.Symbol;
using Xunit;

namespace MipaCompilerTests.SymbolTests
{
    public class FunctionSymbolTests
    {
        [Fact]
        public void FunctionSymbolWorks()
        {
            string[] args = { "integer", "string", "boolean", "real" };

            FunctionSymbol functionSymbol = new FunctionSymbol("CustomFunction", args, "integer");

            Assert.Equal("CustomFunction", functionSymbol.GetIdentifier());
            Assert.Equal("integer", functionSymbol.GetParameterTypes()[0]);
            Assert.Equal("real", functionSymbol.GetParameterTypes()[3]);
            Assert.Equal("integer", functionSymbol.GetReturnType());
        }

        [Fact]
        public void HasSameDefinitionWorks()
        {
            string[] args = { "integer", "string", "boolean", "real" };
            string[] args1 = { "integer", "string", "boolean", "integer" };
            string[] args2 = {};

            FunctionSymbol functionSymbol = new FunctionSymbol("CustomFunction", args, "integer");
            FunctionSymbol functionSymbol2 = new FunctionSymbol("CustomFunction", args, "real");
            FunctionSymbol functionSymbol3 = new FunctionSymbol("CustomFunction3", args, "integer");
            FunctionSymbol functionSymbol4 = new FunctionSymbol("CustomFunction4", args1, "integer");
            FunctionSymbol functionSymbol5 = new FunctionSymbol("CustomFunction5", args2, "integer");

            Assert.True(functionSymbol.HasSameDefinition(functionSymbol2));
            Assert.False(functionSymbol.HasSameDefinition(functionSymbol3));
            Assert.False(functionSymbol.HasSameDefinition(functionSymbol4));
            Assert.False(functionSymbol.HasSameDefinition(functionSymbol5));
        }
    }
}
