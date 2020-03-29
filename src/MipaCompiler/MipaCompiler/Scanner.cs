using System;
using System.IO;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>Scanner</c> contains functionality to scan tokens from source code file.
    /// Scans and groups successive characters in souce code into tokens.
    /// </summary>
    public class Scanner
    {
        private bool processingCommentblock;            // flag telling if processing multiline comment block {* *}
        private int rowNum;                             // row of source code that is processed
        private int colNum;                             // column of source code that is processed
        private string[] lines;                         // source code lines

        /// <summary>
        /// Constructor <c>Scanner</c> creates new Scanner-object.
        /// </summary>
        /// <param name="filepath">path to Mini-Pascal source code file</param>
        public Scanner(string filepath)
        {
            processingCommentblock = false;
            lines = File.ReadAllLines(filepath);
        }

        /// <summary>
        /// Method <c>Scan</c> scans the next token from input source code.
        /// </summary>
        /// <returns>next token</returns>
        public Token ScanNextToken()
        {
            for (int r = rowNum; r < lines.Length; r++)
            {
                string line = lines[r];
                for (int c = colNum; c < line.Length; c++)
                {

                    // check if currently processing comment block
                    // then we have to check if there is end of comment block
                    if (processingCommentblock)
                    {
                        if (line[c] == '*' && c < line.Length - 1 && line[c + 1] == '}')
                        {
                            c++;
                            processingCommentblock = false;
                        }
                        continue;
                    }


                    // check if the next character is whitespace
                    if (char.IsWhiteSpace(line[c])) continue;


                    // check if start of a multiline comment
                    if (line[c] == '{' && c < line.Length - 1 && line[c + 1] == '*')
                    {
                        processingCommentblock = true;
                        c++;
                        continue;
                    }


                    // check if next character is single characer token
                    if (Token.IsSinleCharToken(line[c]))
                    {
                        colNum = c + 1;
                        return new Token(line[c].ToString(), Token.FindTokenType(line[c].ToString()), r + 1, c + 1);
                    }


                    // check if the character is ':' --> two valid cases
                    if (line[c] == ':')
                    {
                        if (c < line.Length - 1 && line[c + 1] == '=')
                        {
                            colNum = c + 2;
                            return new Token(":=", TokenType.ASSIGNMENT, r + 1, c + 1);
                        }
                        colNum = c + 1;
                        return new Token(":", TokenType.SEPARATOR, r + 1, c + 1);
                    }


                    // check if the character is '<' --> three valid cases
                    if (line[c] == '<')
                    {
                        if (c < line.Length - 1 && line[c + 1] == '>')
                        {
                            colNum = c + 2;
                            return new Token("<>", TokenType.NOT_EQUALS, r + 1, c + 1);
                        }
                        else if (c < line.Length - 1 && line[c + 1] == '=')
                        {
                            colNum = c + 2;
                            return new Token("<=", TokenType.LESS_THAN_OR_EQUAL, r + 1, c + 1);
                        }
                        colNum = c + 1;
                        return new Token("<", TokenType.LESS_THAN, r + 1, c + 1);
                    }


                    // check if the character is '>' --> two valid cases
                    if (line[c] == '>')
                    {
                        if (c < line.Length - 1 && line[c + 1] == '=')
                        {
                            colNum = c + 2;
                            return new Token(">=", TokenType.GREATER_THAN_OR_EQUAL, r + 1, c + 1);
                        }
                        colNum = c + 1;
                        return new Token(">", TokenType.GREATER_THAN, r + 1, c + 1);
                    }


                    /*

                  

                    // check if character is " --> start of string
                    if (line[c] == '"')
                    {
                        int startCol = c + 1;
                        string value = "";
                        bool ended = false;
                        c++;
                        while (c < line.Length)
                        {
                            if (line[c] == '"')
                            {
                                ended = true;
                                break;
                            }
                            if (line[c] == '\\' && c < line.Length && line[c + 1] == 'n')
                            {
                                c++;
                                value += "\n";
                            }
                            else if (line[c] == '\\' && c < line.Length && line[c + 1] == '"')
                            {
                                c++;
                                value += "\"";
                            }
                            else
                            {
                                value += line[c];
                            }
                            c++;
                        }
                        if (ended)
                        {
                            colNum = c + 1;
                            return new Token(value, TokenType.VAL_STRING, r + 1, startCol);
                        }
                        colNum = c - 1;
                        string errorSlash = $"LexicalError::Row {r + 1}::Column {c + 1}::Expected '\"'!";
                        return new Token(errorSlash, TokenType.ERROR, r + 1, c + 1);
                    }*/

                    // check if character is digit --> start of number
                    if (char.IsDigit(line[c]))
                    {
                        int startCol = c + 1;
                        string number = "" + line[c];
                        while (c + 1 < line.Length && char.IsDigit(line[c + 1]))
                        {
                            number += line[c + 1];
                            c++;
                        }
                        colNum = c + 1;
                        if (c < line.Length && line[c] == '.')
                        {
                            number += ".";
                            c++;
                            if(c < line.Length && !char.IsDigit(line[c])) {
                                string errorDigit = $"LexicalError::Row {r + 1}::Column {c + 1}::Expected a digit!";
                                colNum = c + 1;
                                return new Token(errorDigit, TokenType.ERROR, r + 1, c + 1);
                            }
                            while (c < line.Length && char.IsDigit(line[c]))
                            {
                                number += line[c];
                                c++;
                            }
                            colNum = c + 1;
                            return new Token(number, TokenType.VAL_REAL, r + 1, startCol);
                        }
                        return new Token(number, TokenType.VAL_INTEGER, r + 1, startCol);
                    }


                    // check if next character is alphabet --> start of identifier or keyword
                    if (IsAlphabet(line[c]))
                    {
                        int startCol = c + 1;
                        string letters = "" + line[c];
                        while (c + 1 < line.Length && (char.IsDigit(line[c + 1]) || IsAlphabet(line[c + 1]) || line[c + 1] == '_'))
                        {
                            letters += line[c + 1];
                            c++;
                        }
                        colNum = c + 1;
                        if (Token.IsKeyword(letters)) return new Token(letters, Token.FindTokenType(letters), r + 1, startCol);
                        if (Token.IsPredefinedIdentifier(letters)) return new Token(letters, Token.FindTokenType(letters), r + 1, startCol);
                        return new Token(letters, TokenType.IDENTIFIER, r + 1, startCol);
                    }


                    // could not scan the content into valid token
                    string error = $"LexicalError::Row {r + 1}::Column {c + 1}::Illegal character!";
                    colNum = c + 1;
                    return new Token(error, TokenType.ERROR, r + 1, c + 1);
                }
                rowNum++;
                colNum = 0;
            }

            // all rows have been scanned already --> end of file
            return new Token("EOF", TokenType.EOF, rowNum + 1, colNum + 1);
        }

        /// <summary>
        /// Method <c>IsAlphabet</c> checks if character is alphabet character (a-z, A-Z).
        /// </summary>
        private bool IsAlphabet(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
    }
}
