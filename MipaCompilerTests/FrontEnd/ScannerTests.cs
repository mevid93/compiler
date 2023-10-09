using Xunit;
using MipaCompiler;

namespace MipaCompilerTests
{
    public class ScannerTests
    {
        [Fact]
        public void ScannerConstructorWorks()
        {
            string filename = "../../../SampleFiles/program1.txt";
            Scanner scanner = new Scanner(filename);
            Assert.NotNull(scanner);
        }

        [Fact]
        public void ScannerPeekNthTokenWorks()
        {
            string filename = "../../../SampleFiles/program1.txt";
            Scanner scanner = new Scanner(filename);
            Assert.Equal(TokenType.KEYWORD_PROGRAM, scanner.PeekNthToken(1).GetTokenType());
            Assert.Equal(TokenType.KEYWORD_PROGRAM, scanner.PeekNthToken(1).GetTokenType());
        }

        [Fact]
        public void ScanNextTokenWorks1()
        {
            string filename = "../../../SampleFiles/program1.txt";
            Scanner scanner = new Scanner(filename);
            // scan and compare
            Assert.Equal(TokenType.KEYWORD_PROGRAM, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_READ, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_WHILE, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.NOT_EQUALS, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_DO, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_IF, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.GREATER_THAN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_THEN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SUBSTRACTION, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_ELSE, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SUBSTRACTION, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_WRITELN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.DOT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.EOF, scanner.ScanNextToken().GetTokenType());
        }

      

        [Fact]
        public void ScanNextTokenWorks3()
        {
            string filename = "../../../SampleFiles/program3.txt";
            Scanner scanner = new Scanner(filename);
            // scan and compare
            Assert.Equal(TokenType.KEYWORD_PROGRAM, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.KEYWORD_FUNCTION, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_ARRAY, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_OF, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_WHILE, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.LESS_THAN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.DOT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_SIZE, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_DO, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ADDITION, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ADDITION, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_RETURN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.KEYWORD_PROCEDURE, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_ARRAY, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_OF, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_READ, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_WRITELN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_WRITELN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_STRING, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.DOT, scanner.ScanNextToken().GetTokenType());

        }

        [Fact]
        public void ScanNextTokenWorks4()
        {
            string filename = "../../../SampleFiles/types1.txt";
            Scanner scanner = new Scanner(filename);
            // scan and compare
            Assert.Equal(TokenType.KEYWORD_PROGRAM, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_BOOLEAN, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_REAL, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_STRING, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.PREDEFINED_TRUE, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_REAL, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.VAL_STRING, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.Equal(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.Equal(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
  
        }
    }
}
