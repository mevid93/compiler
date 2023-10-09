using Xunit;
using MipaCompiler;

namespace MipaCompilerTests
{
    public class TokenTests
    {
        [Fact]
        public void ConstructorWorks()
        {
            Token token = new Token("myValue", TokenType.VAL_STRING, 23, 11);
            Assert.Equal("myValue", token.GetTokenValue());
            Assert.Equal(TokenType.VAL_STRING, token.GetTokenType());
            Assert.Equal(23, token.GetRow());
            Assert.Equal(11, token.GetColumn());
        }

        [Fact]
        public void ToStringWorks()
        {
            Token token = new Token("Hello, World!", TokenType.VAL_STRING, 10, 10);
            string value = $"{TokenType.VAL_STRING}, Hello, World!, Row: 10, Col: 10";
            Assert.Equal(value, token.ToString());
        }

        [Fact]
        public void FindTokenTypeWorksWithUpperValidToken()
        {
            Assert.Equal(TokenType.PREDEFINED_BOOLEAN, Token.FindTokenType("BoOlEaN"));
            Assert.Equal(TokenType.ASSIGNMENT, Token.FindTokenType(":="));
            Assert.Equal(TokenType.PREDEFINED_REAL, Token.FindTokenType("real"));
        }

        [Fact]
        public void FindTokenTypeWorksWithInvalidToken()
        {
            Assert.Equal(TokenType.ERROR, Token.FindTokenType("MEME"));
            Assert.Equal(TokenType.ERROR, Token.FindTokenType(":=="));
            Assert.Equal(TokenType.ERROR, Token.FindTokenType("--"));
        }

        [Fact]
        public void IsKeywordWorksWithKeyword()
        {
            Assert.True(Token.IsKeyword("begin"));
            Assert.True(Token.IsKeyword("program"));
            Assert.True(Token.IsKeyword("procedure"));
            Assert.True(Token.IsKeyword("function"));
        }

        [Fact]
        public void IsKeywordWorksWithNonKeyword()
        {
            Assert.False(Token.IsKeyword("false"));
            Assert.False(Token.IsKeyword("identifier"));
            Assert.False(Token.IsKeyword(";"));
        }

        [Fact]
        public void IsPredefinedIdentifierWorksWithPredefinedIdentifier()
        {
            Assert.True(Token.IsPredefinedIdentifier("false"));
            Assert.True(Token.IsPredefinedIdentifier("true"));
            Assert.True(Token.IsPredefinedIdentifier("integer"));
            Assert.True(Token.IsPredefinedIdentifier("BOOLEAN"));
        }

        [Fact]
        public void IsPredefinedIdentifierWorksWithNonPredefinedIdentifier()
        {
            Assert.False(Token.IsPredefinedIdentifier("begin"));
            Assert.False(Token.IsPredefinedIdentifier("program"));
            Assert.False(Token.IsPredefinedIdentifier("procedure"));
            Assert.False(Token.IsPredefinedIdentifier("function"));
        }

        [Fact]
        public void IsSingleCharTokenWorksWithSingleCharToken()
        {
            Assert.True(Token.IsSinleCharToken(','));
            Assert.True(Token.IsSinleCharToken('.'));
            Assert.True(Token.IsSinleCharToken('['));
            Assert.True(Token.IsSinleCharToken(';'));
        }

        [Fact]
        public void IsSingleCharTokenWorksWithNonSingleCharToken()
        {
            Assert.False(Token.IsSinleCharToken('!'));
            Assert.False(Token.IsSinleCharToken(':'));
            Assert.False(Token.IsSinleCharToken('<'));
            Assert.False(Token.IsSinleCharToken('>'));
        }

        [Fact]
        public void CanBeUsedAsIdentifierWorks()
        {
            Token valid = new Token("yes", TokenType.IDENTIFIER, 1, 1);
            Token valid2 = new Token("yes2", TokenType.PREDEFINED_READ, 1, 1);
            Token notValid = new Token("no", TokenType.ADDITION, 1, 1);
            Assert.True(Token.CanBeIdentifier(valid));
            Assert.True(Token.CanBeIdentifier(valid2));
            Assert.False(Token.CanBeIdentifier(notValid));
        }
    }
}
