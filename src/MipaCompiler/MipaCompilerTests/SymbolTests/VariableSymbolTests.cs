using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler.Symbol;

namespace MipaCompilerTests.SymbolTests
{
    [TestClass]
    public class VariableSymbolTests
    {

        [TestMethod]
        public void SymbolWorks()
        {
            VariableSymbol symbol = new VariableSymbol("mySymbol", "integer", "23", 1);

            Assert.AreEqual("mySymbol", symbol.GetIdentifier());
            Assert.AreEqual("integer", symbol.GetSymbolType());
            Assert.AreEqual("23", symbol.GetCurrentValue());
            Assert.AreEqual(1, symbol.GetCurrentScope());

            symbol.PushScope(3);
            Assert.AreEqual(3, symbol.GetCurrentScope());

            Assert.AreEqual(1, symbol.PopScope());
            Assert.AreEqual(1, symbol.GetCurrentScope());

            Assert.AreEqual(-1, symbol.PopScope());
            Assert.AreEqual(-1, symbol.PopScope());
            Assert.AreEqual(-1, symbol.GetCurrentScope());
        }

    }
}
