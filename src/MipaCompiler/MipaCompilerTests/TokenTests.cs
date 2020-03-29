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

        [TestMethod]
        public void IsKeywordWorksWithKeyword()
        {
            Assert.IsTrue(Token.IsKeyword("begin"));
            Assert.IsTrue(Token.IsKeyword("program"));
            Assert.IsTrue(Token.IsKeyword("procedure"));
            Assert.IsTrue(Token.IsKeyword("function"));
        }

        [TestMethod]
        public void IsKeywordWorksWithNonKeyword()
        {
            Assert.IsFalse(Token.IsKeyword("false"));
            Assert.IsFalse(Token.IsKeyword("identifier"));
            Assert.IsFalse(Token.IsKeyword(";"));
        }

        [TestMethod]
        public void IsPredefinedIdentifierWorksWithPredefinedIdentifier()
        {
            Assert.IsTrue(Token.IsPredefinedIdentifier("false"));
            Assert.IsTrue(Token.IsPredefinedIdentifier("true"));
            Assert.IsTrue(Token.IsPredefinedIdentifier("integer"));
            Assert.IsTrue(Token.IsPredefinedIdentifier("BOOLEAN"));
        }

        [TestMethod]
        public void IsPredefinedIdentifierWorksWithNonPredefinedIdentifier()
        {
            Assert.IsFalse(Token.IsPredefinedIdentifier("begin"));
            Assert.IsFalse(Token.IsPredefinedIdentifier("program"));
            Assert.IsFalse(Token.IsPredefinedIdentifier("procedure"));
            Assert.IsFalse(Token.IsPredefinedIdentifier("function"));
        }

        [TestMethod]
        public void IsSingleCharTokenWorksWithSingleCharToken()
        {
            Assert.IsTrue(Token.IsSinleCharToken(','));
            Assert.IsTrue(Token.IsSinleCharToken('.'));
            Assert.IsTrue(Token.IsSinleCharToken('['));
            Assert.IsTrue(Token.IsSinleCharToken(';'));
        }

        [TestMethod]
        public void IsSingleCharTokenWorksWithNonSingleCharToken()
        {
            Assert.IsFalse(Token.IsSinleCharToken('!'));
            Assert.IsFalse(Token.IsSinleCharToken(':'));
            Assert.IsFalse(Token.IsSinleCharToken('<'));
            Assert.IsFalse(Token.IsSinleCharToken('>'));
        }
    }
}
