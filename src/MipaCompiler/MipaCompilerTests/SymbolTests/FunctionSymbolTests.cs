using MipaCompiler.Symbol;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MipaCompilerTests.SymbolTests
{
    [TestClass]
    public class FunctionSymbolTests
    {
        [TestMethod]
        public void FunctionSymbolWorks()
        {
            string[] args = { "integer", "string", "boolean", "real" };

            FunctionSymbol functionSymbol = new FunctionSymbol("CustomFunction", args, "integer");

            Assert.AreEqual("CustomFunction", functionSymbol.GetIdentifier());
            Assert.AreEqual("integer", functionSymbol.GetParameterTypes()[0]);
            Assert.AreEqual("real", functionSymbol.GetParameterTypes()[3]);
            Assert.AreEqual("integer", functionSymbol.GetReturnType());
        }

        [TestMethod]
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

            Assert.IsTrue(functionSymbol.HasSameDefinition(functionSymbol2));
            Assert.IsFalse(functionSymbol.HasSameDefinition(functionSymbol3));
            Assert.IsFalse(functionSymbol.HasSameDefinition(functionSymbol4));
            Assert.IsFalse(functionSymbol.HasSameDefinition(functionSymbol5));
        }
    }
}
