using MipaCompiler.Symbol;
using Xunit;

namespace MipaCompilerTests.SymbolTests
{
    public class VariableSymbolTests
    {

        [Fact]
        public void SymbolWorks()
        {
            VariableSymbol symbol = new VariableSymbol("mySymbol", "integer", "23", 1);

            Assert.Equal("mySymbol", symbol.GetIdentifier());
            Assert.Equal("integer", symbol.GetSymbolType());
            Assert.Equal("23", symbol.GetCurrentValue());
            Assert.Equal(1, symbol.GetCurrentScope());

            symbol.PushScope(3);
            Assert.Equal(3, symbol.GetCurrentScope());

            Assert.Equal(1, symbol.PopScope());
            Assert.Equal(1, symbol.GetCurrentScope());

            Assert.Equal(-1, symbol.PopScope());
            Assert.Equal(-1, symbol.PopScope());
            Assert.Equal(-1, symbol.GetCurrentScope());
        }

    }
}
