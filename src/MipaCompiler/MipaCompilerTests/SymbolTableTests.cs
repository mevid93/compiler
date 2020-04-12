using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;

namespace MipaCompilerTests
{
    [TestClass]
    public class SymbolTableTests
    {
        [TestMethod]
        public void SymbolTableWorks()
        {
            SymbolTable symbolTable = new SymbolTable();
            Assert.IsTrue(symbolTable.GetCurrentScope() == 0);

            symbolTable.DeclareSymbol(new Symbol("symbol1", "integer", "26", 0));
            Assert.IsTrue(symbolTable.IsSymbolInTable("symbol1"));
            Assert.IsFalse(symbolTable.IsSymbolInTable("symbol2"));

            Assert.AreEqual("symbol1", symbolTable.GetSymbolByIdentifier("symbol1").GetIdentifier());
            symbolTable.AddScope();

            symbolTable.DeclareSymbol(new Symbol("symbol1", "string", "hello world", symbolTable.GetCurrentScope()));
            Assert.IsTrue(symbolTable.IsSymbolInTable("symbol1"));
            string value = symbolTable.GetSymbolByIdentifier("symbol1").GetCurrentValue();
            Assert.AreEqual("hello world", value);

            symbolTable.RemoveScope();
            Assert.IsTrue(symbolTable.IsSymbolInTable("symbol1"));
            value = symbolTable.GetSymbolByIdentifier("symbol1").GetCurrentValue();
            Assert.AreEqual("26", value);
        }
    }
}
