﻿
namespace MipaCompiler
{
    /// <summary>
    /// Enum <c>TokenType</c> indicates the type of token.
    /// </summary>
    public enum TokenType
    {
        ADDITION,               // "+" symbol (sum and concat operation)
        ASSIGNMENT,             // ":=" symbol (assingment operation)
        BRACKET_LEFT,           // "[" symbol (left bracket used with arrays)
        BRACKET_RIGHT,          // "]" symbol (right bracket used with arrays)
        COMMA,                  // "," symbol 
        DIVISION,               // "/" symbol (division operation)
        DOT,                    // "." symbol (dot operation used with arrays)
        EOF,                    // end-of-file indicator
        EQUALS,                 // "=" symbol (equals comparison)
        ERROR,                  // error token for returning error information for parser
        GREATER_THAN,           // ">" symbol (greater than comparison)
        GREATER_THAN_OR_EQUAL,  // ">=" symbol (greater than comparison)
        IDENTIFIER,             // token for variable identifier symbols in source code
        KEYWORD_ARRAY,          // "array" symbol (used when declaring array)
        KEYWORD_ASSERT,         // "assert" symbol (assert function)
        KEYWORD_BEGIN,          // "begin" symbol (keyword)
        KEYWORD_DO,             // "do" symbol (keyword)
        KEYWORD_ELSE,           // "else" symbol (keyword)
        KEYWORD_FUNCTION,       // "function" symbol (keyword)
        KEYWORD_END,            // "end" symbol (keyword)
        KEYWORD_IF,             // "if" symbol (keyword)
        KEYWORD_OF,             // "of" symbol (keyword)
        KEYWORD_PROCEDURE,      // "procedure" symbol (keyword)
        KEYWORD_PROGRAM,        // "program" symbol (keyword)
        KEYWORD_RETURN,         // "return" symbol (keyword)
        KEYWORD_THEN,           // "then" symbol (keyword)
        KEYWORD_VAR,            // "var" symbol (keyword)
        KEYWORD_WHILE,          // "while" symbol (keyword)
        LESS_THAN,              // "<" symbol (less than comparison)
        LESS_THAN_OR_EQUAL,     // "<=" symbol (less than or equal comparison)
        LOGICAL_AND,            // "and" symbol (logical AND operation)
        LOGICAL_NOT,            // "not" symbol (logical NOT operation)
        LOGICAL_OR,             // "or" symbol (logical OR operation)
        MODULO,                 // "%" symbol (modulo operation for integers)
        MULTIPLICATION,         // "*" symbol (multiplication operation)
        NOT_EQUALS,             // "<>" symbol (not equals comparison)
        PARENTHIS_LEFT,         // "(" symbol (left parenthis used in expressions)
        PARENTHIS_RIGHT,        // ")" symbol (right parenthis used in expressions)
        PREDEFINED_BOOLEAN,     // "Boolean" symbol (predefined identifer)
        PREDEFINED_FALSE,       // "false" symbol (predefined identifier)
        PREDEFINED_INTEGER,     // "integer" symbol (predefined identifier)
        PREDEFINED_READ,        // "read" symbol (predefined identifier)
        PREDEFINED_REAL,        // "real" symbol (predefined identifier)
        PREDEFINED_SIZE,        // "size" symbol (predefined identifier)
        PREDEFINED_STRING,      // "string" symbol (predefined identifier)
        PREDEFINED_TRUE,        // "true" symbol (predefined identifier)
        PREDEFINED_WRITELN,     // "writeln" symbol (predefined identifier)
        SEPARATOR,              // ":" symbol (used in function and variable declarations)
        STATEMENT_END,          // ";" symbol (used to end statement)
        SUBSTRACTION,           // "-" symbol (substraction operation)
        VAL_INTEGER,            // token for integer values
        VAL_REAL,               // token for real values
        VAL_STRING              // token for string values
    }

    /// <summary>
    /// Class <c>Token</c> represents a token scanned from the source code.
    /// </summary>
    public class Token
    {
        private readonly TokenType type;    // type of token
        private readonly string value;      // value of token
        private readonly int row;           // row where token exists in source code
        private readonly int col;           // column where token starts in source code

        /// <summary>
        /// Constructor <c>Token</c> creates new Token-object.
        /// <param name="tokenValue">source code symbol</param>
        /// <param name="tokenType">type of token</param>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// </summary>
        public Token(string tokenValue, TokenType tokenType, int row, int col)
        {
            type = tokenType;
            value = tokenValue;
            this.row = row;
            this.col = col;
        }

        /// <summary>
        /// Method <c>GetTokenType</c> returns the type of token.
        /// </summary>
        /// <returns>token type</returns>
        public TokenType GetTokenType()
        {
            return type;
        }

        /// <summary>
        /// Method <c>GetTokenValue</c> returns the value of token. 
        /// This is the symbol in source code.
        /// </summary>
        /// <returns>token value</returns>
        public string GetTokenValue()
        {
            return value;
        }

        /// <summary>
        /// Method <c>GetRow</c> returns the row number where token is located in the source code.
        /// </summary>
        /// <returns>row number</returns>
        public int GetRow()
        {
            return row;
        }

        /// <summary>
        /// Method <c>GetColumn</c> returns the column number where token is located in source code.
        /// </summary>
        /// <returns>column number</returns>
        public int GetColumn()
        {
            return col;
        }

        /// <summary>
        /// Method <c>ToString</c> returns string representation of Token-object.
        /// This is not used by the compiler, but it can be usefull for debugging.
        /// </summary>
        /// <returns>token in string format</returns>
        override
        public string ToString()
        {
            return $"{type}, {value}, Row: {row}, Col: {col}";
        }

        /// <summary>
        /// Static method <c>FindTokenType</c> returns the type of the token for given string.
        /// If string cannot be interpreted as a valid token, then ERROR token is returned.
        /// </summary>
        public static TokenType FindTokenType(string value)
        {
            string valueLower = value.ToLower();

            switch (valueLower)
            {
                case "+":
                    return TokenType.ADDITION;
                case ":=":
                    return TokenType.ASSIGNMENT;
                case "[":
                    return TokenType.BRACKET_LEFT;
                case "]":
                    return TokenType.BRACKET_RIGHT;
                case ",":
                    return TokenType.COMMA;
                case "/":
                    return TokenType.DIVISION;
                case ".":
                    return TokenType.DOT;
                case "=":
                    return TokenType.EQUALS;
                case ">":
                    return TokenType.GREATER_THAN;
                case ">=":
                    return TokenType.GREATER_THAN_OR_EQUAL;
                case "array":
                    return TokenType.KEYWORD_ARRAY;
                case "assert":
                    return TokenType.KEYWORD_ASSERT;
                case "begin":
                    return TokenType.KEYWORD_BEGIN;
                case "do":
                    return TokenType.KEYWORD_DO;
                case "else":
                    return TokenType.KEYWORD_ELSE;
                case "function":
                    return TokenType.KEYWORD_FUNCTION;
                case "end":
                    return TokenType.KEYWORD_END;
                case "if":
                    return TokenType.KEYWORD_IF;
                case "of":
                    return TokenType.KEYWORD_OF;
                case "procedure":
                    return TokenType.KEYWORD_PROCEDURE;
                case "program":
                    return TokenType.KEYWORD_PROGRAM;
                case "return":
                    return TokenType.KEYWORD_RETURN;
                case "then":
                    return TokenType.KEYWORD_THEN;
                case "var":
                    return TokenType.KEYWORD_VAR;
                case "while":
                    return TokenType.KEYWORD_WHILE;
                case "<":
                    return TokenType.LESS_THAN;
                case "<=":
                    return TokenType.LESS_THAN_OR_EQUAL;
                case "and":
                    return TokenType.LOGICAL_AND;
                case "or":
                    return TokenType.LOGICAL_OR;
                case "%":
                    return TokenType.MODULO;
                case "*":
                    return TokenType.MULTIPLICATION;
                case "<>":
                    return TokenType.NOT_EQUALS;
                case "(":
                    return TokenType.PARENTHIS_LEFT;
                case ")":
                    return TokenType.PARENTHIS_RIGHT;
                case "boolean":
                    return TokenType.PREDEFINED_BOOLEAN;
                case "false":
                    return TokenType.PREDEFINED_FALSE;
                case "integer":
                    return TokenType.PREDEFINED_INTEGER;
                case "read":
                    return TokenType.PREDEFINED_READ;
                case "real":
                    return TokenType.PREDEFINED_REAL;
                case "size":
                    return TokenType.PREDEFINED_SIZE;
                case "string":
                    return TokenType.PREDEFINED_STRING;
                case "writeln":
                    return TokenType.PREDEFINED_WRITELN;
                case ":":
                    return TokenType.SEPARATOR;
                case ";":
                    return TokenType.STATEMENT_END;
                case "-":
                    return TokenType.SUBSTRACTION;
                case "true":
                    return TokenType.PREDEFINED_TRUE;
                case "not":
                    return TokenType.LOGICAL_NOT;
                default:
                    return TokenType.ERROR;
            }
        }

        /// <summary>
        /// Static method <c>IsKeyword</c> checks if given string matches keyword token.
        /// </summary>
        /// <param name="value">possible keyword to check</param>
        /// <returns>true if string is a keyword</returns>
        public static bool IsKeyword(string value)
        {
            switch (value)
            {
                case "and":
                case "array":
                case "assert":
                case "begin":
                case "do":
                case "else":
                case "function":
                case "end":
                case "if":
                case "not":
                case "of":
                case "or":
                case "procedure":
                case "program":
                case "return":
                case "then":
                case "var":
                case "while":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Static method <c>IsPredefinedIdentifier</c> checks if the string matches 
        /// predefined identifier token.
        /// </summary>
        /// <param name="value">possible predefined identifier</param>
        /// <returns>true if string matches predefined identifier token</returns>
        public static bool IsPredefinedIdentifier(string value)
        {
            string valueLower = value.ToLower();
            switch (valueLower)
            {
                case "boolean":
                case "false":
                case "integer":
                case "read":
                case "real":
                case "size":
                case "string":
                case "true":
                case "writeln":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Static method <c>IsSingleCharToken</c> checks if the given char is matches
        /// a single char token.
        /// </summary>
        /// <param name="c">possible single char</param>
        /// <returns>true if char matches a single char token</returns>
        public static bool IsSinleCharToken(char c)
        {
            switch (c)
            {
                case '.':
                case ',':
                case '[':
                case ']':
                case '(':
                case ')':
                case '+':
                case '-':
                case '*':
                case '/':
                case '%':
                case ';':
                case '=':
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Static method <c>CanBeIdentifier</c> check if the given token can be
        /// used as identifier.
        /// </summary>
        /// <param name="token">possible identifier token</param>
        /// <returns>true if token can be used as identifier</returns>
        public static bool CanBeIdentifier(Token token)
        {
            switch (token.GetTokenType())
            {
                case TokenType.PREDEFINED_READ:
                case TokenType.IDENTIFIER:
                case TokenType.PREDEFINED_BOOLEAN:
                case TokenType.PREDEFINED_FALSE:
                case TokenType.PREDEFINED_INTEGER:
                case TokenType.PREDEFINED_REAL:
                case TokenType.PREDEFINED_SIZE:
                case TokenType.PREDEFINED_STRING:
                case TokenType.PREDEFINED_TRUE:
                case TokenType.PREDEFINED_WRITELN:
                    return true;
                default:
                    return false;
            }
        }
    }
}
