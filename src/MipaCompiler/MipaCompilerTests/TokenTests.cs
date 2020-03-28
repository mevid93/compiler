using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;

namespace MipaCompilerTests
{
    [TestClass]
    public class TokenTests
    {
        [TestMethod]
        public void ConstructorWorks()
        {
            Token token = new Token("myValue", TokenType.VAL_STRING, 23, 11);
            Assert.AreEqual("myValue", token.GetTokenValue());
            Assert.AreEqual(TokenType.VAL_STRING, token.GetTokenType());
            Assert.AreEqual(23, token.GetRow());
            Assert.AreEqual(11, token.GetColumn());
        }

        [TestMethod]
        public void ToStringWorks()
        {
            Token token = new Token("Hello, World!", TokenType.VAL_STRING, 10, 10);
            string value = $"{TokenType.VAL_STRING}, Hello, World!, Row: 10, Col: 10";
            Assert.AreEqual(value, token.ToString());
        }

        [TestMethod]
        public void FindTokenTypeWorksWithUpperValidToken()
        {
            Assert.AreEqual(TokenType.PREDEFINED_BOOLEAN, Token.FindTokenType("BoOlEaN"));
            Assert.AreEqual(TokenType.ASSIGNMENT, Token.FindTokenType(":="));
            Assert.AreEqual(TokenType.PREDEFINED_REAL, Token.FindTokenType("real"));
        }

        [TestMethod]
        public void FindTokenTypeWorksWithInvalidToken()
        {
            Assert.AreEqual(TokenType.ERROR, Token.FindTokenType("MEME"));
            Assert.AreEqual(TokenType.ERROR, Token.FindTokenType(":=="));
            Assert.AreEqual(TokenType.ERROR, Token.FindTokenType("--"));
        }
    }
}
