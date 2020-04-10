using MipaCompiler.Node;
using System;
using System.Collections.Generic;

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
        private bool errorInStatement;      // flag for current statement error status

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
            if (!errorsDetected) ast = new ProgramNode(row, col, programName);
            if (errorsDetected) return;
            
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
            if (statement == null) errorInStatement = false;
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
                if (statement == null) errorInStatement = false;
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

        /// <summary>
        /// Method <c>ParseStatement</c> handles the parsing of a single statement.
        /// </summary>
        private INode ParseStatement()
        {
            switch (inputToken.GetTokenType())
            {
                case TokenType.PREDEFINED_READ:
                    return ParseRead();
                case TokenType.PREDEFINED_WRITELN:
                    return ParseWrite();
                case TokenType.IDENTIFIER:
                case TokenType.PREDEFINED_BOOLEAN:
                case TokenType.PREDEFINED_FALSE:
                case TokenType.PREDEFINED_INTEGER:
                case TokenType.PREDEFINED_REAL:
                case TokenType.PREDEFINED_SIZE:
                case TokenType.PREDEFINED_STRING:
                case TokenType.PREDEFINED_TRUE:
                    Token next1 = scanner.PeekNthToken(1);
                    switch (next1.GetTokenType())
                    {
                        case TokenType.ASSIGNMENT:
                            return ParseAssignment();
                        default:
                            // TODO
                            Match(TokenType.ERROR);
                            return null;
                    }
                case TokenType.KEYWORD_RETURN:
                    // TODO
                    Match(TokenType.KEYWORD_RETURN);
                    return null;
                case TokenType.KEYWORD_BEGIN:
                    // TODO
                    Match(TokenType.KEYWORD_BEGIN);
                    return null;
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
        /// Method <c>ParseAssignment</c> handles the parsing of assignment.
        /// </summary>
        private INode ParseAssignment()
        {
            if (errorInStatement) return null;

            if (!Token.CanBeIdentifier(inputToken)) return null;

            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            string value = inputToken.GetTokenValue();

            inputToken = scanner.ScanNextToken();
            Match(TokenType.ASSIGNMENT);
            if (errorInStatement) return null;

            INode expression = ParseExpression();
            expression = ParseExpressionTail(expression);
            if (errorInStatement) return null;

            return new AssignmentNode(row, col, value, expression);
        }

        /// <summary>
        /// Method <c>ParseIfStatement</c> handles the parsing of if statement.
        /// </summary>
        private INode ParseIfStatement()
        {
            if (errorInStatement) return null;

            int rowIf = inputToken.GetRow();
            int colIf = inputToken.GetColumn();
            Match(TokenType.KEYWORD_IF);
            if (errorInStatement) return null;

            INode condition = ParseExpression();
            condition = ParseExpressionTail(condition);
            if (errorInStatement) return null;

            Match(TokenType.KEYWORD_THEN);
            if (errorInStatement) return null;

            INode thenStatement = ParseStatement();
            if (errorInStatement) return null;

            INode elseStatement = null;
            if (scanner.PeekNthToken(1).GetTokenType() == TokenType.KEYWORD_ELSE)
            {
                Match(TokenType.STATEMENT_END);
                Match(TokenType.KEYWORD_ELSE);
                elseStatement = ParseStatement();
                if (errorInStatement) return null;
            }
            return new IfElseNode(rowIf, colIf, condition, thenStatement, elseStatement);
        }

        /// <summary>
        /// Method <c>ParseWhileStatement</c> handles the parsing of while statement.
        /// </summary>
        private INode ParseWhileStatement()
        {
            if (errorInStatement) return null;

            int row = inputToken.GetRow();
            int column = inputToken.GetColumn();

            Match(TokenType.KEYWORD_WHILE);

            if (errorInStatement) return null;

            INode expr = ParseExpression();
            expr = ParseExpressionTail(expr);
            Match(TokenType.KEYWORD_DO);

            if (errorInStatement) return null;

            INode statement = ParseStatement();

            if (errorInStatement) return null;
            return new WhileNode(row, column, expr, statement);
        }

        /// <summary>
        /// Method <c>ParseWrite</c> handles the parsing of statment that starts with
        /// predefined identifier "writeln".
        /// </summary>
        /// <returns></returns>
        private INode ParseWrite()
        {
            if (inputToken.GetTokenType() != TokenType.PREDEFINED_WRITELN)
            {
                HandleError();
                return null;
            }

            Token first = scanner.PeekNthToken(1);
            Token second = scanner.PeekNthToken(2);

            switch (first.GetTokenType())
            {
                case TokenType.ASSIGNMENT:
                    return ParseAssignment();
                case TokenType.PARENTHIS_LEFT:
                    switch (second.GetTokenType())
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
                            return ParseCall();
                        default:
                            HandleError();
                            return null;
                    }
                default:
                    HandleError();
                    return null;
            }
        }

        /// <summary>
        /// Method <c>ParseRead</c> handles the parsing of statement
        /// that starts with predefined identifier "read".
        /// </summary>
        private INode ParseRead()
        {
            if (inputToken.GetTokenType() != TokenType.PREDEFINED_READ)
            {
                HandleError();
                return null;
            }


            Token first = scanner.PeekNthToken(1);
            Token second = scanner.PeekNthToken(2);
            
            switch (first.GetTokenType())
            {
                case TokenType.ASSIGNMENT:
                    return ParseAssignment();
                case TokenType.PARENTHIS_LEFT:
                    switch (second.GetTokenType())
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
                            return ParseCall();
                        default:
                            HandleError();
                            return null;
                    }
                default:
                    HandleError();
                    return null;
            }
        }

        /// <summary>
        /// Method <c>ParseCall</c> handles the parsing of a call assignment.
        /// </summary>
        private INode ParseCall()
        {
            string id;
            int idRow;
            int idCol;

            if (Token.CanBeIdentifier(inputToken))
            {
                id = inputToken.GetTokenValue();
                idRow = inputToken.GetRow();
                idCol = inputToken.GetColumn();
                inputToken = scanner.ScanNextToken();
            }
            else
            {
                HandleError();
                return null;
            }

            Match(TokenType.PARENTHIS_LEFT);
            if (errorInStatement) return null;

            INode args = ParseArguments();
            if (errorInStatement) return null;

            Match(TokenType.PARENTHIS_RIGHT);
            if (errorInStatement) return null;

            return new CallNode(idRow, idCol, id, args);
        }

        /// <summary>
        /// Method <c>ParseArguments</c> handles the pargins of arguments
        /// for function or procedure call.
        /// </summary>
        private INode ParseArguments()
        {
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            List<INode> expressions = new List<INode>();

            while (!errorInStatement && inputToken.GetTokenType() != TokenType.PARENTHIS_RIGHT)
            {
                INode expression = ParseExpression();
                expression = ParseExpressionTail(expression);
                if(!errorInStatement)
                {
                    expressions.Add(expression);
                    if(inputToken.GetTokenType() == TokenType.COMMA)
                    {
                        Match(TokenType.COMMA);
                    }
                }
            }

            if (errorInStatement) return null;

            return new ArgumentsNode(row, col, expressions);
        }

        /// <summary>
        /// Method <c>ParseVariableDeclaration</c> handles the parsing of a variable declaration.
        /// </summary>
        private INode ParseVariableDeclaration()
        {
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();
            Match(TokenType.KEYWORD_VAR);
            List<string> ids = new List<string>();

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
                        ids.Add(inputToken.GetTokenValue());
                        inputToken = scanner.ScanNextToken();
                        break;
                    default:
                        HandleError();
                        return null;
                }

                // check if multiple variables are declared
                if (inputToken.GetTokenType() != TokenType.COMMA)
                {
                    break;
                }
                Match(TokenType.COMMA);
            }

            if (errorInStatement) return null;
            Match(TokenType.SEPARATOR);
            if (errorInStatement) return null;

            string type;
            // parse the type of variable/variables
            switch (inputToken.GetTokenType())
            {
                case TokenType.PREDEFINED_INTEGER:
                case TokenType.PREDEFINED_REAL:
                case TokenType.PREDEFINED_BOOLEAN:
                case TokenType.PREDEFINED_STRING:
                    type = inputToken.GetTokenValue().ToLower();
                    inputToken = scanner.ScanNextToken();
                    break;
                case TokenType.KEYWORD_ARRAY:
                    // TODO
                default:
                    HandleError();
                    return null;
            }

            VariableDclNode varDclNode = new VariableDclNode(row, col, type);
            foreach(string name in ids)
            {
                varDclNode.AddVariableName(name);
            }
            return varDclNode;
        }

        /// <summary>
        /// Method <c>ParseExpression</c> handles the parsing of expression (front).
        /// </summary>
        private INode ParseExpression()
        {
            if (errorInStatement) return null;
            INode node = ParseSimpleExpression();
            node = ParseSimpleExpressionTail(node);
            return node;
        }

        /// <summary>
        /// Method <c>ParseExpressionTail</c> handles the parsing of expression tail.
        /// </summary>
        private INode ParseExpressionTail(INode rhs)
        {
            if (errorInStatement) return null;
            switch (inputToken.GetTokenType())
            {
                case TokenType.EQUALS:
                case TokenType.NOT_EQUALS:
                case TokenType.LESS_THAN:
                case TokenType.LESS_THAN_OR_EQUAL:
                case TokenType.GREATER_THAN_OR_EQUAL:
                case TokenType.GREATER_THAN:
                    int row = inputToken.GetRow();
                    int col = inputToken.GetColumn();
                    string value = inputToken.GetTokenValue();

                    inputToken = scanner.ScanNextToken();

                    INode lhs = ParseSimpleExpression();
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
        private INode ParseSimpleExpression()
        {
            if (errorInStatement) return null;

            bool isNegative = false;
            bool hasSign = true;
            int row = inputToken.GetRow();
            int col = inputToken.GetColumn();

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

            INode term = ParseTerm();
            term = ParseTermTail(term);

            if (errorInStatement) return null;

            if (hasSign)
            {
                return new SignNode(row, col, isNegative, term);
            }
            return term;
        }

        /// <summary>
        /// Method <c>ParseSimpleExpressionTail</c> handles the parsing of simple expression tail.
        /// </summary>
        private INode ParseSimpleExpressionTail(INode node)
        {
            if (errorInStatement) return null;

            switch (inputToken.GetTokenType())
            {
                case TokenType.ADDITION:
                case TokenType.SUBSTRACTION:
                case TokenType.LOGICAL_OR:
                    int row = inputToken.GetRow();
                    int col = inputToken.GetColumn();
                    string value = inputToken.GetTokenValue();

                    inputToken = scanner.ScanNextToken();

                    INode term = ParseTerm();
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
        private INode ParseTerm()
        {
            if (errorInStatement) return null;

            INode factor = ParseFactor();
            factor = ParseFactorTail(factor);

            if (errorInStatement) return null;

            return factor;
        }

        /// <summary>
        ///  Method <c>ParseTermTail</c> handles the parsing of a term tail.
        /// </summary>
        private INode ParseTermTail(INode node)
        {
            if (errorInStatement) return null;

            switch (inputToken.GetTokenType())
            {
                case TokenType.MULTIPLICATION:
                case TokenType.DIVISION:
                case TokenType.MODULO:
                case TokenType.LOGICAL_AND:
                    int row = inputToken.GetRow();
                    int col = inputToken.GetColumn();
                    string value = inputToken.GetTokenValue();

                    inputToken = scanner.ScanNextToken();

                    INode factor = ParseFactor();
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
        private INode ParseFactor()
        {
            if (errorInStatement) return null;

            Token peek1 = scanner.PeekNthToken(1);

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
                    switch (peek1.GetTokenType())
                    {
                        case TokenType.PARENTHIS_LEFT:
                            return ParseCall();
                        case TokenType.BRACKET_LEFT:
                            int row = inputToken.GetRow();
                            int col = inputToken.GetColumn();
                            string name = inputToken.GetTokenValue();
                            INode variable = new VariableNode(row, col, name, null);

                            inputToken = scanner.ScanNextToken();
                            row = inputToken.GetRow();
                            col = inputToken.GetColumn();
                            Match(TokenType.BRACKET_LEFT);
                            INode expr = ParseExpression();
                            expr = ParseExpressionTail(expr);
                            Match(TokenType.BRACKET_RIGHT);

                            if (errorInStatement) return null;

                            return new ArrayIndexNode(row, col, variable, expr);
                        default:
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
                    INode expression = ParseExpression();
                    expression = ParseExpressionTail(expression);
                    Match(TokenType.PARENTHIS_RIGHT);
                    if (errorInStatement) return null;
                    return expression;
                case TokenType.LOGICAL_NOT:
                    Match(TokenType.LOGICAL_NOT);
                    INode factor = ParseFactor();
                    factor = ParseFactorTail(factor);
                    return factor;
                default:
                    HandleError();
                    return null;
            }
        }

        /// <summary>
        /// Method <c>ParseFactorTail</c> handles the parsing of factor tail.
        /// </summary>
        private INode ParseFactorTail(INode node)
        {
            if (errorInStatement) return null;

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
        private INode ParseLiteral()
        {
            if (errorInStatement) return null;

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
                    return new StringNode(row, col, value);
                default:
                    HandleError();
                    return null;
            }
        }
    }
}
