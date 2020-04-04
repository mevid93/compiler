using MipaCompiler.Node;
using System;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>Parser</c> contains functionality to perform the syntax analysis for source code.
    /// It also constructs the abstract syntax tree (AST).
    /// TOP-DOWN parsing by using partial LL(1). There ase some LL(1) violations, which can be handled
    /// by peeking incoming tokens.
    /// </summary>
    public class Parser
    {
        private Scanner scanner;            // scanner object
        private INode ast;                  // AST root node, aka program declaration node
        private Token inputToken;           // current token in input
        private bool errorsDetected;        // flag telling if errors were detected during parsing
        private string lastError;           // variable to hold last reported error message
        private bool errorCurrent;          // flag for current statement error status

        /// <summary>
        /// Constructor <c>Parser</c> creates new Parser-object.
        /// </summary>
        /// <param name="tokenScanner">scanner-object</param>
        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
        }

        /// <summary>
        /// Method <c>Parse</c> starts the syntax analysis and building of AST.
        /// Returns the AST if parsing was succesfull. 
        /// If erros were encountered, then null is returned.
        /// </summary>
        /// <returns>AST</returns>
        public INode Parse()
        {
            ParseProgram();
            return ast;
        }

        /// <summary>
        /// Method <c>ErrorsDetected</c> returns the result of parsing.
        /// </summary>
        /// <returns>true if errors were detected during parsing</returns>
        public bool ErrorsDetected()
        {
            return errorsDetected;
        }

        /// <summary>
        /// Method <c>HandleError</c> handles error situtations. 
        /// When parser encounters errors, then the rest of the statement is skipped and the parser
        /// continues from the next statement. 
        /// </summary>
        private void HandleError()
        {
            // check if errors have already been noticed from current statement/code structure
            if (errorCurrent) return;
            errorCurrent = true;
            
            // different error messages
            string defaultError = $"SyntaxError::Row {inputToken.GetRow()}::Column {inputToken.GetColumn()}::Invalid syntax!";
            string eofError = $"SyntaxError::Row {inputToken.GetRow()}::Column {inputToken.GetColumn()}::Unexpected end of file!";
            
            // print error to user
            if (inputToken.GetTokenType() == TokenType.ERROR)
            {
                if (lastError == null || !lastError.Equals(inputToken.GetTokenValue()))
                {
                    Console.WriteLine(inputToken.GetTokenValue());
                    lastError = inputToken.GetTokenValue();
                }
            }
            else if (inputToken.GetTokenType() == TokenType.EOF)
            {
                if (lastError == null || lastError != eofError)
                {
                    Console.WriteLine(eofError);
                    lastError = eofError;
                }
            }
            else
            {
                if (lastError == null || lastError != defaultError)
                {
                    Console.WriteLine(defaultError);
                    lastError = defaultError;
                }
            }

            // try to move to the end of invalid statement, or end of block, function, procedure or file
            inputToken = scanner.ScanNextToken();
            while (inputToken.GetTokenType() != TokenType.STATEMENT_END
                && inputToken.GetTokenType() != TokenType.EOF
                && inputToken.GetTokenType() != TokenType.KEYWORD_END)
            {
                inputToken = scanner.ScanNextToken();
            }
            errorsDetected = true;
        }

        /// <summary>
        /// Method <c>Match</c> consumes token from input stream if it matches the expected.
        /// Returns the matched token value.
        /// </summary>
        private string Match(TokenType expected)
        {
            if (inputToken.GetTokenType() == expected)
            {
                string value = inputToken.GetTokenValue();
                inputToken = scanner.ScanNextToken();
                return value;
            }
            else
            {
                HandleError();
                return null;
            }
        }

        /// <summary>
        /// Method <c>ParseProgram</c> handles the program declaration parsing.
        /// </summary>
        private void ParseProgram()
        {
            inputToken = scanner.ScanNextToken();
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            Match(TokenType.KEYWORD_PROGRAM);
            string programName = Match(TokenType.IDENTIFIER);
            Match(TokenType.STATEMENT_END);
            if (!errorsDetected)
            {
                ast = new ProgramNode(row, col, programName);
            }
            else
            {
                return;
            }


            while (inputToken.GetTokenType() != TokenType.EOF && inputToken.GetTokenType() != TokenType.ERROR)
            {
                switch (inputToken.GetTokenType())
                {
                    case TokenType.KEYWORD_PROCEDURE:
                        INode procedure = ParseProcedure();
                        if (procedure != null)
                        {
                            ProgramNode pn = (ProgramNode)ast;
                            pn.AddProcedure(procedure);
                        }
                        break;
                    case TokenType.KEYWORD_FUNCTION:
                        INode function = ParseBlock();
                        if (function != null)
                        {
                            ProgramNode pn = (ProgramNode)ast;
                            pn.AddFunction(function);
                        }
                        break;
                    case TokenType.KEYWORD_BEGIN:
                        INode block = ParseBlock();
                        if (block != null)
                        {
                            ProgramNode pn = (ProgramNode)ast;
                            pn.SetMainBlock(block);
                        }
                        Match(TokenType.DOT);
                        Match(TokenType.EOF);
                        return;
                    default:
                        HandleError();
                        return;
                }
            }
        }

        private INode ParseProcedure()
        {
            //TODO
            return null;
        }

        private INode ParseFunction()
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Method <c>ParseBlock</c> handles the parsing of a single block 
        /// ("begin" ... some code ... "end") of code.
        /// </summary>
        private INode ParseBlock()
        {
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            Match(TokenType.KEYWORD_BEGIN);
            BlockNode blockNode = new BlockNode(row, col);

            // each block must have one statement
            INode statement = ParseStatement();
            if (statement != null) blockNode.AddStatement(statement);

            // if statement does not follow with "end" then there should be ";"
            if (inputToken.GetTokenType() != TokenType.KEYWORD_END)
            {
                Match(TokenType.STATEMENT_END);
            }

            // process optional statements
            while (inputToken.GetTokenType() != TokenType.EOF && inputToken.GetTokenType() != TokenType.ERROR && inputToken.GetTokenType() != TokenType.KEYWORD_END)
            {
                statement = ParseStatement();
                if (statement != null) blockNode.AddStatement(statement);
                if (inputToken.GetTokenType() != TokenType.KEYWORD_END)
                {
                    Match(TokenType.STATEMENT_END);
                }
            }

            // end of block
            if (inputToken.GetTokenType() == TokenType.KEYWORD_END)
            {
                Match(TokenType.KEYWORD_END);
                return blockNode;
            }

            return null;
        }

        private INode ParseStatement()
        {
            //TODO
            return null;
        }

    }
}
