using MipaCompiler.Node;
using System;
using System.Collections.Generic;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>Parser</c> contains functionality to perform the syntax analysis for source code.
    /// It also constructs the abstract syntax tree (AST).
    /// TOP-DOWN parsing by using partial LL(1). There ase some LL(1) violations, which are handled
    /// by peeking incoming tokens.
    /// </summary>
    public class Parser
    {
        private Scanner scanner;            // scanner object
        private ISymbol ast;                  // AST root node, aka program declaration node
        private Token inputToken;           // current token in input
        private bool errorsDetected;        // flag telling if errors were detected during parsing
        private string lastError;           // variable to hold last reported error message
        private bool errorInStatement;      // flag for current statement error status

        /// <summary>
        /// Constructor <c>Parser</c> creates new Parser-object.
        /// </summary>
        /// <param name="tokenScanner">scanner-object</param>
        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
            ast = null;
        }

        /// <summary>
        /// Method <c>Parse</c> starts the syntax analysis and AST building.
        /// Returns the AST if parsing was succesfull. AST can be null if
        /// errors were detected.
        /// </summary>
        /// <returns>AST</returns>
        public ISymbol Parse()
        {
            ParseProgram();
            return ast;
        }

        /// <summary>
        /// Method <c>ErrorsDetected</c> returns true if errors were detected
        /// during parsing.
        /// </summary>
        /// <returns>true if errors were detected</returns>
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
            if (errorInStatement) return;
            errorInStatement = true;

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
        /// Method <c>Match</c> consumes token from input stream if it matches the expected
        /// token given as a parameter. It also returns the value of the matched token.
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


        /////////////////////////// ACTUAL PARSING METHODS  ///////////////////////////


        /// <summary>
        /// Method <c>ParseProgram</c> handles the program declaration parsing.
        /// </summary>
        private void ParseProgram()
        {
            // parse program keyword
            inputToken = scanner.ScanNextToken();
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            Match(TokenType.KEYWORD_PROGRAM);

            // parse program name
            if (!Token.CanBeIdentifier(inputToken))
            {
                HandleError();
                return;
            }
            string programName = inputToken.GetTokenValue();
            inputToken = scanner.ScanNextToken();

            // parse statement end
            Match(TokenType.STATEMENT_END);
            if (errorInStatement) return;

            List<ISymbol> procedures = new List<ISymbol>();
            List<ISymbol> functions = new List<ISymbol>();
            ISymbol block = null;

            // parse functions, procedures and main block
            while (inputToken.GetTokenType() != TokenType.EOF && inputToken.GetTokenType() != TokenType.ERROR)
            {
                errorInStatement = false;
                bool exitWhile = false;

                switch (inputToken.GetTokenType())
                {
                    case TokenType.KEYWORD_PROCEDURE:
                        ISymbol procedure = ParseProcedure();
                        if (!errorInStatement) procedures.Add(procedure);
                        break;

                    case TokenType.KEYWORD_FUNCTION:
                        ISymbol function = ParseFunction();
                        if (!errorInStatement) functions.Add(function);
                        break;

                    case TokenType.KEYWORD_BEGIN:
                        block = ParseBlock();
                        Match(TokenType.DOT);
                        Match(TokenType.EOF);
                        if (errorInStatement) return;
                        exitWhile = true;
                        break;

                    default:
                        HandleError();
                        return;
                }

                if (exitWhile) break;
            }

            // create program node to AST if no errors were detected
            ast = new ProgramNode(row, col, programName, procedures, functions, block);
        }

        /// <summary>
        /// Method <c>ParseProcedure</c> handles the parsing of single procedure.
        /// </summary>
        private ISymbol ParseProcedure()
        {
            // if error has been detected already --> return
            if (errorInStatement) return null;

            // parse procedure keyword
            int rowProcedure = inputToken.GetRow();
            int colProcedure = inputToken.GetColumn();
            Match(TokenType.KEYWORD_PROCEDURE);
            if (errorInStatement) return null;

            // parse procedure identifier
            if (!Token.CanBeIdentifier(inputToken))
            {
                HandleError();
                return null;
            }
            string procedureName = inputToken.GetTokenValue();
            inputToken = scanner.ScanNextToken();

            // parse left parenthis
            Match(TokenType.PARENTHIS_LEFT);

            // parse parameters (optional)
            List<ISymbol> parameters = new List<ISymbol>();
            if (inputToken.GetTokenType() != TokenType.PARENTHIS_RIGHT) parameters = ParseParameters();

            // parse right parenthis
            Match(TokenType.PARENTHIS_RIGHT);

            // parse end of statement
            Match(TokenType.STATEMENT_END);
            if (errorInStatement) return null;

            // parse code block
            ISymbol block = ParseBlock();

            // parse end of statement
            Match(TokenType.STATEMENT_END);
            if (errorInStatement) return null;

            return new ProcedureNode(rowProcedure, colProcedure, procedureName, parameters, block);
        }

        /// <summary>
        /// Method <c>ParseFunction</c> handles the parsing of single function.
        /// </summary>
        private ISymbol ParseFunction()
        {
            // if errors have been detected already --> return
            if (errorInStatement) return null;

            // parse function keyword
            int rowFunction = inputToken.GetRow();
            int colFunction = inputToken.GetColumn();
            Match(TokenType.KEYWORD_FUNCTION);
            if (errorInStatement) return null;

            // parse function identifier
            if (!Token.CanBeIdentifier(inputToken))
            {
                HandleError();
                return null;
            }
            string functionName = inputToken.GetTokenValue();
            inputToken = scanner.ScanNextToken();

            // parse left parenthis
            Match(TokenType.PARENTHIS_LEFT);

            // parse parameters
            List<ISymbol> parameters = new List<ISymbol>();
            if (inputToken.GetTokenType() != TokenType.PARENTHIS_RIGHT) parameters = ParseParameters();

            // parse right parenthis
            Match(TokenType.PARENTHIS_RIGHT);

            // parse separator
            Match(TokenType.SEPARATOR);

            // parse function return type
            ISymbol returnType = ParseType();

            // parse end of statement
            Match(TokenType.STATEMENT_END);
            if (errorInStatement) return null;

            // parse code block
            ISymbol block = ParseBlock();

            // parse end of statement
            Match(TokenType.STATEMENT_END);
            if (errorInStatement) return null;

            return new FunctionNode(rowFunction, colFunction, functionName, returnType, parameters, block);
        }

        /// <summary>
        /// Method <c>ParseBlock</c> handles the parsing of a single block. 
        /// Block starts with keyword "begin" and ends with keyword "end".
        /// </summary>
        private ISymbol ParseBlock()
        {
            // parse begin keyword
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            Match(TokenType.KEYWORD_BEGIN);

            List<ISymbol> statements = new List<ISymbol>();

            // each block must have one statement
            ISymbol statement = ParseStatement();
            if (statement == null) errorInStatement = false;
            if (statement != null) statements.Add(statement);

            // if statement does not follow with "end" then there should be ";"
            if (inputToken.GetTokenType() != TokenType.KEYWORD_END) Match(TokenType.STATEMENT_END);

            // process optional statements
            while (inputToken.GetTokenType() != TokenType.EOF
                && inputToken.GetTokenType() != TokenType.ERROR
                && inputToken.GetTokenType() != TokenType.KEYWORD_END)
            {
                statement = ParseStatement();
                if (statement != null) statements.Add(statement);
                if (statement == null) errorInStatement = false;
                if (inputToken.GetTokenType() != TokenType.KEYWORD_END)
                {
                    Match(TokenType.STATEMENT_END);
                }
            }

            // parse end of block
            if (inputToken.GetTokenType() == TokenType.KEYWORD_END)
            {
                Match(TokenType.KEYWORD_END);
                return new BlockNode(row, col, statements);
            }

            return null;
        }

        /// <summary>
        /// Method <c>ParseStatement</c> handles the parsing of a single statement.
        /// </summary>
        private ISymbol ParseStatement()
        {
            // if errors have been detected already --> return
            if (errorInStatement) return null;

            // decide what to parse based on inputToken
            switch (inputToken.GetTokenType())
            {
                case TokenType.KEYWORD_ASSERT:
                    return ParseAssert();
                case TokenType.PREDEFINED_READ:
                case TokenType.PREDEFINED_WRITELN:
                case TokenType.IDENTIFIER:
                case TokenType.PREDEFINED_BOOLEAN:
                case TokenType.PREDEFINED_FALSE:
                case TokenType.PREDEFINED_INTEGER:
                case TokenType.PREDEFINED_REAL:
                case TokenType.PREDEFINED_SIZE:
                case TokenType.PREDEFINED_STRING:
                case TokenType.PREDEFINED_TRUE:
                    switch (scanner.PeekNthToken(1).GetTokenType())
                    {
                        case TokenType.ASSIGNMENT:
                            return ParseAssignment();
                        case TokenType.PARENTHIS_LEFT:
                            return ParseCall();
                        default:
                            HandleError();
                            return null;
                    }
                case TokenType.KEYWORD_RETURN:
                    return ParseReturnStatement();
                case TokenType.KEYWORD_BEGIN:
                    return ParseBlock();
                case TokenType.KEYWORD_IF:
                    return ParseIfStatement();
                case TokenType.KEYWORD_WHILE:
                    return ParseWhileStatement();
                case TokenType.KEYWORD_VAR:
                    return ParseVariableDeclaration();
                default:
                    HandleError();
                    return null;
            }
        }

        /// <summary>
        /// Method <c>ParseAssert</c> handles the parsing of assert statement.
        /// </summary>
        private ISymbol ParseAssert()
        {
            // if errors already detected --> return
            if (errorInStatement) return null;

            // parse assert keyword
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            Match(TokenType.PARENTHIS_LEFT);
            if (errorInStatement) return null;

            // parse boolean expression
            ISymbol expression = ParseExpression();
            expression = ParseExpressionTail(expression);
            if (errorInStatement) return null;

            return new AssertNode(row, col, expression);
        }

        /// <summary>
        /// Method <c>ParseType</c> handles the parsing of type.
        /// </summary>
        private ISymbol ParseType()
        {
            // if error already detected --> return
            if (errorInStatement) return null;

            // decide what kind of type is parsed
            switch (inputToken.GetTokenType())
            {
                case TokenType.PREDEFINED_INTEGER:
                case TokenType.PREDEFINED_BOOLEAN:
                case TokenType.PREDEFINED_STRING:
                case TokenType.PREDEFINED_REAL:
                    return ParseSimpleType();
                case TokenType.KEYWORD_ARRAY:
                    return ParseArrayType();
                default:
                    HandleError();
                    return null;
            }
        }

        /// <summary>
        /// Method <c>ParseSimpleType</c> handles the parsing of simple type.
        /// </summary>
        private ISymbol ParseSimpleType()
        {
            // if error already detected --> return
            if (errorInStatement) return null;

            // check that is simple type
            switch (inputToken.GetTokenType())
            {
                case TokenType.PREDEFINED_INTEGER:
                case TokenType.PREDEFINED_BOOLEAN:
                case TokenType.PREDEFINED_STRING:
                case TokenType.PREDEFINED_REAL:
                    break;
                default:
                    HandleError();
                    return null;
            }

            // parse simple type
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            string value = inputToken.GetTokenValue();
            inputToken = scanner.ScanNextToken();

            return new SimpleTypeNode(row, col, value);
        }

        /// <summary>
        /// Method <c>ParseArrayType</c> handles the parsing of an array type.
        /// </summary>
        private ISymbol ParseArrayType()
        {
            // if error already detected --> return
            if (errorInStatement) return null;

            // parse array keyword
            int row = inputToken.GetRow();
            int column = inputToken.GetColumn();
            Match(TokenType.KEYWORD_ARRAY);

            // parse left bracket
            Match(TokenType.BRACKET_LEFT);
            if (errorInStatement) return null;

            // parse optional expression
            ISymbol expression = null;
            if (inputToken.GetTokenType() != TokenType.BRACKET_RIGHT)
            {
                expression = ParseExpression();
                expression = ParseExpressionTail(expression);
            }

            // parse right bracked
            Match(TokenType.BRACKET_RIGHT);

            // parse of keyword
            Match(TokenType.KEYWORD_OF);
            if (errorInStatement) return null;

            // parse simple type
            ISymbol simpleType = ParseSimpleType();
            if (errorInStatement) return null;

            return new ArrayTypeNode(row, column, expression, simpleType);
        }

        /// <summary>
        /// Method <c>ParseParameters</c> handles the parsing of parameters in procedure
        /// or function definition.
        /// </summary>
        private List<ISymbol> ParseParameters()
        {
            // if errors already detected --> return null
            if (errorInStatement) return null;

            List<ISymbol> parameters = new List<ISymbol>();

            // parse all parameters
            while (!errorInStatement)
            {
                // parse optional var keyword
                if (inputToken.GetTokenType() == TokenType.KEYWORD_VAR) Match(TokenType.KEYWORD_VAR);

                // parse identifier
                if (!Token.CanBeIdentifier(inputToken))
                {
                    HandleError();
                    return null;
                }
                int rowP = inputToken.GetRow();
                int colP = inputToken.GetColumn();
                string identifier = inputToken.GetTokenValue();
                inputToken = scanner.ScanNextToken();

                // parse separator
                Match(TokenType.SEPARATOR);

                // parse type
                ISymbol type = ParseType();
                if (errorInStatement) return null;

                parameters.Add(new VariableNode(rowP, colP, identifier, type));

                // parse optional comma
                if (inputToken.GetTokenType() == TokenType.COMMA)
                {
                    Match(TokenType.COMMA);
                }
                else
                {
                    break;
                } 
                if (inputToken.GetTokenType() != TokenType.KEYWORD_VAR && !Token.CanBeIdentifier(inputToken)) break;
            }

            // if no errors were detected --> return parameters
            if (errorInStatement) return null;
            return parameters;
        }

        /// <summary>
        /// Method <c>ParseReturnStatement</c> handles the parsing of return statement.
        /// </summary>
        private ISymbol ParseReturnStatement()
        {
            // if errors already detected
            if (errorInStatement) return null;

            // parse return keyword
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            Match(TokenType.KEYWORD_RETURN);
            if (errorInStatement) return null;

            // parse optional expression
            ISymbol expr = null;
            if (inputToken.GetTokenType() != TokenType.STATEMENT_END 
                && inputToken.GetTokenType() != TokenType.KEYWORD_END
                && inputToken.GetTokenType() != TokenType.KEYWORD_ELSE)
            {
                expr = ParseExpression();
                expr = ParseExpressionTail(expr);
            }
            if (errorInStatement) return null;

            return new ReturnNode(row, col, expr);
        }

        /// <summary>
        /// Method <c>ParseAssignment</c> handles the parsing of assignment.
        /// </summary>
        private ISymbol ParseAssignment()
        {
            // if error already detected --> return null
            if (errorInStatement) return null;

            // parse identifier
            if (!Token.CanBeIdentifier(inputToken))
            {
                HandleError();
                return null;
            }
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            string value = inputToken.GetTokenValue();
            inputToken = scanner.ScanNextToken();

            // parse assignment operator
            Match(TokenType.ASSIGNMENT);
            if (errorInStatement) return null;

            // parse expression
            ISymbol expression = ParseExpression();
            expression = ParseExpressionTail(expression);
            if (errorInStatement) return null;

            return new AssignmentNode(row, col, value, expression);
        }

        /// <summary>
        /// Method <c>ParseIfStatement</c> handles the parsing of if statement.
        /// </summary>
        private ISymbol ParseIfStatement()
        {
            // if error already detected --> return
            if (errorInStatement) return null;

            // parse if keyword
            int rowIf = inputToken.GetRow();
            int colIf = inputToken.GetColumn();
            Match(TokenType.KEYWORD_IF);
            if (errorInStatement) return null;

            // parse condition expression
            ISymbol condition = ParseExpression();
            condition = ParseExpressionTail(condition);
            if (errorInStatement) return null;

            // parse then keyword
            Match(TokenType.KEYWORD_THEN);
            if (errorInStatement) return null;

            // parse the true condition statement
            ISymbol thenStatement = ParseStatement();
            if (errorInStatement) return null;

            // parse the false condition statement (optional)
            // statement can have ';' or it can be without it
            ISymbol elseStatement = null;
            if (inputToken.GetTokenType() == TokenType.KEYWORD_ELSE
                || scanner.PeekNthToken(1).GetTokenType() == TokenType.KEYWORD_ELSE)
            {
                // parse optional statement end
                if (inputToken.GetTokenType() == TokenType.STATEMENT_END) Match(TokenType.STATEMENT_END);

                // parse else keyword
                Match(TokenType.KEYWORD_ELSE);

                // parse else statement
                elseStatement = ParseStatement();
                if (errorInStatement) return null;
            }
            return new IfElseNode(rowIf, colIf, condition, thenStatement, elseStatement);
        }

        /// <summary>
        /// Method <c>ParseWhileStatement</c> handles the parsing of while statement.
        /// </summary>
        private ISymbol ParseWhileStatement()
        {
            // if errors already detected --> return null
            if (errorInStatement) return null;

            // parse while keyword
            int row = inputToken.GetRow();
            int column = inputToken.GetColumn();
            Match(TokenType.KEYWORD_WHILE);
            if (errorInStatement) return null;

            // parse expression
            ISymbol expr = ParseExpression();
            expr = ParseExpressionTail(expr);
            if (errorInStatement) return null;

            // parse do keyword
            Match(TokenType.KEYWORD_DO);
            if (errorInStatement) return null;

            // parse statement
            ISymbol statement = ParseStatement();
            if (errorInStatement) return null;

            return new WhileNode(row, column, expr, statement);
        }

        /// <summary>
        /// Method <c>ParseCall</c> handles the parsing of a call assignment.
        /// </summary>
        private ISymbol ParseCall()
        {
            // if errors already detected --> return 
            if (errorInStatement) return null;

            // check that input token can be identifier and parse it
            if (!Token.CanBeIdentifier(inputToken))
            {
                HandleError();
                return null;
            }
            string id = inputToken.GetTokenValue();
            int idRow = inputToken.GetRow();
            int idCol = inputToken.GetColumn();
            inputToken = scanner.ScanNextToken();
            
            // parse left parenthis
            Match(TokenType.PARENTHIS_LEFT);
            if (errorInStatement) return null;

            // parse arguments (optional)
            List<ISymbol> args = ParseArguments();
            if (errorInStatement) return null;

            // parse right parentis
            Match(TokenType.PARENTHIS_RIGHT);
            if (errorInStatement) return null;

            return new CallNode(idRow, idCol, id, args);
        }

        /// <summary>
        /// Method <c>ParseArguments</c> handles the parsing of arguments
        /// for function or procedure call.
        /// </summary>
        private List<ISymbol> ParseArguments()
        {
            // if errors already detected --> return
            if (errorInStatement) return null;

            List<ISymbol> args = new List<ISymbol>();

            // while can be argument --> parse it
            while (!errorInStatement && inputToken.GetTokenType() != TokenType.PARENTHIS_RIGHT)
            {
                // parse expression
                ISymbol expression = ParseExpression();
                expression = ParseExpressionTail(expression);
                if (errorInStatement) return null;

                // add expression to arguments
                args.Add(expression);
                if (inputToken.GetTokenType() == TokenType.COMMA) Match(TokenType.COMMA);

            }

            // if no errors were detected return arguments
            if (errorInStatement) return null;
            return args;
        }

        /// <summary>
        /// Method <c>ParseVariableDeclaration</c> handles the parsing of a variable declaration.
        /// </summary>
        private ISymbol ParseVariableDeclaration()
        {
            // if errors already detected --> return
            if (errorInStatement) return null;

            // parse var keyword
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            Match(TokenType.KEYWORD_VAR);

            List<ISymbol> variables = new List<ISymbol>();

            // parse all variable names
            while (!errorInStatement)
            {
                switch (inputToken.GetTokenType())
                {
                    case TokenType.IDENTIFIER:
                    case TokenType.PREDEFINED_BOOLEAN:
                    case TokenType.PREDEFINED_FALSE:
                    case TokenType.PREDEFINED_INTEGER:
                    case TokenType.PREDEFINED_READ:
                    case TokenType.PREDEFINED_REAL:
                    case TokenType.PREDEFINED_SIZE:
                    case TokenType.PREDEFINED_STRING:
                    case TokenType.PREDEFINED_TRUE:
                    case TokenType.PREDEFINED_WRITELN:
                        int rowV = inputToken.GetRow();
                        int colV = inputToken.GetColumn();
                        string val = inputToken.GetTokenValue();
                        variables.Add(new VariableNode(rowV, colV, val, null));
                        inputToken = scanner.ScanNextToken();
                        break;
                    default:
                        HandleError();
                        return null;
                }

                // check if multiple variables are declared
                if (inputToken.GetTokenType() != TokenType.COMMA) break;
                Match(TokenType.COMMA);
            }
            if (errorInStatement) return null;

            // parse separator 
            Match(TokenType.SEPARATOR);
            if (errorInStatement) return null;

            // parse type
            ISymbol type = ParseType();
            if (errorInStatement) return null;

            return new VariableDclNode(row, col, variables, type);
        }

        /// <summary>
        /// Method <c>ParseExpression</c> handles the parsing of expression (front).
        /// </summary>
        private ISymbol ParseExpression()
        {
            // if errors already detected --> return null
            if (errorInStatement) return null;

            // parse simple expression
            ISymbol node = ParseSimpleExpression();
            node = ParseSimpleExpressionTail(node);
            if (errorInStatement) return null;

            return node;
        }

        /// <summary>
        /// Method <c>ParseExpressionTail</c> handles the parsing of expression tail.
        /// </summary>
        private ISymbol ParseExpressionTail(ISymbol rhs)
        {
            // if errors already detected --> return
            if (errorInStatement) return null;

            // check if has relational operator as next token
            switch (inputToken.GetTokenType())
            {
                case TokenType.EQUALS:
                case TokenType.NOT_EQUALS:
                case TokenType.LESS_THAN:
                case TokenType.LESS_THAN_OR_EQUAL:
                case TokenType.GREATER_THAN_OR_EQUAL:
                case TokenType.GREATER_THAN:
                    // parse token
                    int row = inputToken.GetRow();
                    int col = inputToken.GetColumn();
                    string value = inputToken.GetTokenValue();
                    inputToken = scanner.ScanNextToken();

                    // parse right hand side expression
                    ISymbol lhs = ParseSimpleExpression();
                    lhs = ParseSimpleExpressionTail(lhs);
                    if (errorInStatement) return null;

                    return new BinaryExpressionNode(row, col, value, rhs, lhs);
                default:
                    return rhs;
            }
        }

        /// <summary>
        /// Method <c>ParseSimpleExpression</c> handles the parsing of simple expression (front).
        /// </summary>
        private ISymbol ParseSimpleExpression()
        {
            // if errors already detected --> return
            if (errorInStatement) return null;

            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();

            // check if negation (or positive sign that can be ignored)
            bool isNegative = false;
            bool hasSign = true;
            switch (inputToken.GetTokenType())
            {
                case TokenType.ADDITION:
                    Match(TokenType.ADDITION);
                    break;
                case TokenType.SUBSTRACTION:
                    isNegative = true;
                    Match(TokenType.SUBSTRACTION);
                    break;
                default:
                    hasSign = false;
                    break;
            }

            // parse term
            ISymbol term = ParseTerm();
            term = ParseTermTail(term);
            if (errorInStatement) return null;

            if (hasSign) return new SignNode(row, col, isNegative, term);
            return term;
        }

        /// <summary>
        /// Method <c>ParseSimpleExpressionTail</c> handles the parsing of simple expression tail.
        /// </summary>
        private ISymbol ParseSimpleExpressionTail(ISymbol node)
        {
            // if errors already detected --> return
            if (errorInStatement) return null;

            // check if logical operator as next token
            switch (inputToken.GetTokenType())
            {
                case TokenType.ADDITION:
                case TokenType.SUBSTRACTION:
                case TokenType.LOGICAL_OR:
                    // parse operator
                    int row = inputToken.GetRow();
                    int col = inputToken.GetColumn();
                    string value = inputToken.GetTokenValue();
                    inputToken = scanner.ScanNextToken();

                    // parse right hand side of expression
                    ISymbol term = ParseTerm();
                    term = ParseTermTail(term);
                    if (errorInStatement) return null;

                    return new BinaryExpressionNode(row, col, value, node, term);
                default:
                    return node;
            }
        }

        /// <summary>
        /// Method <c>ParseTerm</c> handles the parsing of single term (front).
        /// </summary>
        private ISymbol ParseTerm()
        {
            // if errors already detected --> return
            if (errorInStatement) return null;

            // parse factor
            ISymbol factor = ParseFactor();
            factor = ParseFactorTail(factor);
            if (errorInStatement) return null;

            return factor;
        }

        /// <summary>
        ///  Method <c>ParseTermTail</c> handles the parsing of a term tail.
        /// </summary>
        private ISymbol ParseTermTail(ISymbol node)
        {
            // if errors already detected --> return
            if (errorInStatement) return null;

            // get token type
            switch (inputToken.GetTokenType())
            {
                case TokenType.MULTIPLICATION:
                case TokenType.DIVISION:
                case TokenType.MODULO:
                case TokenType.LOGICAL_AND:
                    // parse token
                    int row = inputToken.GetRow();
                    int col = inputToken.GetColumn();
                    string value = inputToken.GetTokenValue();
                    inputToken = scanner.ScanNextToken();

                    // parse factor
                    ISymbol factor = ParseFactor();
                    factor = ParseFactorTail(factor);
                    if (errorInStatement) return null;

                    return new BinaryExpressionNode(row, col, value, node, factor);
                default:
                    return node;
            }
        }

        /// <summary>
        /// Method <c>ParseFactor</c> handles the parsing of factor (front).
        /// </summary>
        private ISymbol ParseFactor()
        {
            // if errors already detected --> return null
            if (errorInStatement) return null;

            // parse --> must peek because of LL(1) violations
            switch (inputToken.GetTokenType())
            {
                case TokenType.IDENTIFIER:
                case TokenType.PREDEFINED_READ:
                case TokenType.PREDEFINED_BOOLEAN:
                case TokenType.PREDEFINED_FALSE:
                case TokenType.PREDEFINED_INTEGER:
                case TokenType.PREDEFINED_REAL:
                case TokenType.PREDEFINED_SIZE:
                case TokenType.PREDEFINED_STRING:
                case TokenType.PREDEFINED_TRUE:
                case TokenType.PREDEFINED_WRITELN:
                    switch (scanner.PeekNthToken(1).GetTokenType())
                    {
                        case TokenType.PARENTHIS_LEFT:
                            return ParseCall();
                        case TokenType.BRACKET_LEFT:
                            // parse identifier
                            int row = inputToken.GetRow();
                            int col = inputToken.GetColumn();
                            string name = inputToken.GetTokenValue();
                            ISymbol variable = new VariableNode(row, col, name, null);

                            // parse peeked left bracket
                            inputToken = scanner.ScanNextToken();
                            row = inputToken.GetRow();
                            col = inputToken.GetColumn();
                            Match(TokenType.BRACKET_LEFT);

                            // parse expression
                            ISymbol expr = ParseExpression();
                            expr = ParseExpressionTail(expr);

                            // parse bracket right
                            Match(TokenType.BRACKET_RIGHT);
                            if (errorInStatement) return null;
                            return new ArrayIndexNode(row, col, variable, expr);
                        default:
                            // parse variable node
                            row = inputToken.GetRow();
                            col = inputToken.GetColumn();
                            name = inputToken.GetTokenValue();
                            inputToken = scanner.ScanNextToken();
                            return new VariableNode(row, col, name, null);
                    }
                case TokenType.VAL_INTEGER:
                case TokenType.VAL_REAL:
                case TokenType.VAL_STRING:
                    return ParseLiteral();
                case TokenType.PARENTHIS_LEFT:
                    Match(TokenType.PARENTHIS_LEFT);
                    ISymbol expression = ParseExpression();
                    expression = ParseExpressionTail(expression);
                    Match(TokenType.PARENTHIS_RIGHT);
                    if (errorInStatement) return null;
                    return expression;
                case TokenType.LOGICAL_NOT:
                    // parse token
                    int rowL = inputToken.GetRow();
                    int colL = inputToken.GetColumn();
                    string oper = Match(TokenType.LOGICAL_NOT);

                    // parse expression
                    ISymbol factor = ParseFactor();
                    factor = ParseFactorTail(factor);
                    if (errorInStatement) return null;

                    return new UnaryExpressionNode(rowL, colL, oper, factor);
                default:
                    HandleError();
                    return null;
            }
        }

        /// <summary>
        /// Method <c>ParseFactorTail</c> handles the parsing of factor tail.
        /// </summary>
        private ISymbol ParseFactorTail(ISymbol node)
        {
            // if errors already detected --> return
            if (errorInStatement) return null;

            // check if size operation for arrays
            switch (inputToken.GetTokenType())
            {
                case TokenType.DOT:
                    int row = inputToken.GetRow();
                    int col = inputToken.GetColumn();
                    Match(TokenType.DOT);
                    Match(TokenType.PREDEFINED_SIZE);
                    if (errorInStatement) return null;
                    return new ArraySizeNode(row, col, node);
                default:
                    return node;
            }
        }

        /// <summary>
        /// Method <c>ParseLiteral</c> handles the parsing of literal.
        /// </summary>
        private ISymbol ParseLiteral()
        {
            // if errors already detected --> return null
            if (errorInStatement) return null;

            // parse integer, real, or string literal
            switch (inputToken.GetTokenType())
            {
                case TokenType.VAL_INTEGER:
                    int row = inputToken.GetRow();
                    int col = inputToken.GetColumn();
                    string value = inputToken.GetTokenValue();
                    Match(TokenType.VAL_INTEGER);
                    return new IntegerNode(row, col, value);

                case TokenType.VAL_REAL:
                    row = inputToken.GetRow();
                    col = inputToken.GetColumn();
                    value = inputToken.GetTokenValue();
                    Match(TokenType.VAL_REAL);
                    return new RealNode(row, col, value);

                case TokenType.VAL_STRING:
                    row = inputToken.GetRow();
                    col = inputToken.GetColumn();
                    value = inputToken.GetTokenValue();
                    Match(TokenType.VAL_STRING);
                    return new StringNode(row, col, value);

                default:
                    HandleError();
                    return null;
            }
        }
    }
}
