using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;

namespace MipaCompilerTests
{
    [TestClass]
    public class ScannerTests
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program1.txt")]
        public void ScannerConstructorWorks()
        {
            string filename = "program1.txt";
            Scanner scanner = new Scanner(filename);
            Assert.IsNotNull(scanner);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program1.txt")]
        public void ScannerPeekNthTokenWorks()
        {
            string filename = "program1.txt";
            Scanner scanner = new Scanner(filename);
            Assert.AreEqual(TokenType.KEYWORD_PROGRAM, scanner.PeekNthToken(1).GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_PROGRAM, scanner.PeekNthToken(1).GetTokenType());
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program1.txt")]
        public void ScanNextTokenWorks1()
        {
            string filename = "program1.txt";
            Scanner scanner = new Scanner(filename);
            // scan and compare
            Assert.AreEqual(TokenType.KEYWORD_PROGRAM, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_READ, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_WHILE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.NOT_EQUALS, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_DO, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_IF, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.GREATER_THAN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_THEN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SUBSTRACTION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_ELSE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SUBSTRACTION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_WRITELN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.DOT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.EOF, scanner.ScanNextToken().GetTokenType());
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program2.txt")]
        public void ScanNextTokenWorks2()
        {
            string filename = "program2.txt";
            Scanner scanner = new Scanner(filename);
            // scan and compare
            Assert.AreEqual(TokenType.KEYWORD_PROGRAM, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_FUNCTION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_IF, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.EQUALS, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_THEN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_RETURN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_ELSE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_RETURN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SUBSTRACTION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SUBSTRACTION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_FUNCTION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_IF, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.EQUALS, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_THEN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_RETURN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_ELSE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_RETURN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SUBSTRACTION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SUBSTRACTION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_WHILE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.LESS_THAN_OR_EQUAL, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_DO, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_WRITELN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_WHILE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.LESS_THAN_OR_EQUAL, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_DO, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_WRITELN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.DOT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.EOF, scanner.ScanNextToken().GetTokenType());

        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program3.txt")]
        public void ScanNextTokenWorks3()
        {
            string filename = "program3.txt";
            Scanner scanner = new Scanner(filename);
            // scan and compare
            Assert.AreEqual(TokenType.KEYWORD_PROGRAM, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_FUNCTION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_ARRAY, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_OF, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_WHILE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.LESS_THAN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.DOT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_SIZE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_DO, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ADDITION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ADDITION, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_RETURN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_PROCEDURE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_ARRAY, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_OF, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_READ, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_WRITELN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.BRACKET_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_WRITELN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_STRING, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.COMMA, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_LEFT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PARENTHIS_RIGHT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.DOT, scanner.ScanNextToken().GetTokenType());

        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\types1.txt")]
        public void ScanNextTokenWorks4()
        {
            string filename = "types1.txt";
            Scanner scanner = new Scanner(filename);
            // scan and compare
            Assert.AreEqual(TokenType.KEYWORD_PROGRAM, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_BEGIN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_BOOLEAN, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_REAL, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_VAR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.SEPARATOR, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_STRING, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.PREDEFINED_TRUE, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_INTEGER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_REAL, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.IDENTIFIER, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.ASSIGNMENT, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.VAL_STRING, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.STATEMENT_END, scanner.ScanNextToken().GetTokenType());

            Assert.AreEqual(TokenType.KEYWORD_END, scanner.ScanNextToken().GetTokenType());
            Assert.AreEqual(TokenType.DOT, scanner.ScanNextToken().GetTokenType());
  
        }
    }
}
