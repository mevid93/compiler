using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;

namespace MipaCompilerTests
{
    [TestClass]
    public class SymbolTests
    {

        [TestMethod]
        public void SymbolWorks()
        {
            Symbol symbol = new Symbol("mySymbol", "integer", "23", 1);

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
