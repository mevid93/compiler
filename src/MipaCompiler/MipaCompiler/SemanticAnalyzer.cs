
using MipaCompiler.Node;
using MipaCompiler.Symbol;
using System;
using System.Collections.Generic;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>Semantix</c> holds functionality to do semantic analysis for
    /// intermediate representation of source code. In other words, it takes
    /// AST as input, checks semantic constraints and reports any errors it finds.
    /// </summary>
    public class SemanticAnalyzer
    {
        private readonly INode ast;                     // AST representation of the source code
        private bool errorsDetected;                    // flag telling about status of semantic analysis
        private readonly List<string> errors;           // list of all detected errors
        private readonly SymbolTable symbolTable;       // symbol table to store variables

        private string returnStmntType;

        /// <summary>
        /// Constructor <c>SemanticAnalyzer</c> creates new SemanticAnalyzer-object.
        /// </summary>
        /// <param name="ast"></param>
        public SemanticAnalyzer(INode ast)
        {
            this.ast = ast;
            errors = new List<string>();
            symbolTable = new SymbolTable();
        }

        /// <summary>
        /// Method <c>ErrorsDetected</c> returns result of analysis.
        /// </summary>
        /// <returns>true if errors were detected</returns>
        public bool ErrosDetected()
        {
            return errorsDetected;
        }

        /// <summary>
        /// Method <c>GetDetectedErrors</c> returns the list of detected errors.
        /// </summary>
        /// <returns>list of errors</returns>
        public List<string> GetDetectedErrors()
        {
            return errors;
        }

        /// <summary>
        /// Method <c>CheckConstraints</c> checks the semantic constraints of source code.
        /// </summary>
        public void CheckConstraints()
        {
            // check that code actually exists
            if (ast == null)
            {
                errorsDetected = true;
                errors.Add($"SemanticError::Row ?::Column ?::No code to analyze!");
                return;
            }
            CheckProgram();
        }

        /// <summary>
        /// Method <c>ReportError</c> print error message to user and adds it to
        /// the list of reported errors.
        /// </summary>
        private void ReportError(string errorMsg)
        {
            Console.WriteLine(errorMsg);
            errors.Add(errorMsg);
            errorsDetected = true;
        }

        ///////////////////////////////// ACTUAL SEMANTIC CHECKS /////////////////////////////////

        public static string STR_BOOLEAN = "boolean";
        public static string STR_INTEGER = "integer";
        public static string STR_REAL = "real";
        public static string STR_STRING = "string";

        /// <summary>
        /// Method <c>CheckProgram</c> checks the semantic constraints of program.
        /// </summary>
        private void CheckProgram()
        {
            // convert ast root node to program node
            ProgramNode prog = (ProgramNode)ast;

            // before the actual checking, get all function
            // and procedure declarations to symbol table
            foreach (INode f in prog.GetFunctions()) InitFunctionToSymbolTable(f);
            foreach (INode p in prog.GetProcedures()) InitProceduresToSymbolTable(p);

            // do semantic analysis for the functions
            foreach (INode f in prog.GetFunctions()) CheckFunction(f);

            // from here on --> no values should be returned in return statements
            // only functions should return values
            returnStmntType = "";

            // do semantic analysis for the procedures
            foreach (INode p in prog.GetProcedures()) CheckProcedure(p);

            // do semantic analysis for the main block,
            // can be null if parsing failed
            INode block = prog.GetMainBlock();
            CheckBlock(block);

        }

        /// <summary>
        /// Method <c>InitFunctionToSymbolTable</c> checks function definition
        /// and adds it to the symbolt table.
        /// </summary>
        /// <param name="node">function node</param>
        private void InitFunctionToSymbolTable(INode node)
        {
            if (node == null) return;

            // convert node to function node
            FunctionNode func = (FunctionNode)node;

            // get function name
            string functionName = func.GetName();

            // for each function parameter --> find parameter type
            // should be integer, string, boolean or array
            List<INode> parameters = func.GetParameters();
            string[] paramTypes = new string[parameters.Count];

            for (int i = 0; i < parameters.Count; i++)
            {
                VariableNode varNode = (VariableNode)parameters[i];
                paramTypes[i] = EvaluateTypeOfTypeNode(varNode.GetVariableType());

                // declare variable to symbol table
                VariableSymbol sym = new VariableSymbol(varNode.GetName(), paramTypes[i], null, symbolTable.GetCurrentScope());
                symbolTable.DeclareVariableSymbol(sym);
            }

            // find return type
            string returnType = EvaluateTypeOfTypeNode(func.GetReturnType());
            returnStmntType = returnType;

            // create new function symbol
            FunctionSymbol symbol = new FunctionSymbol(functionName, paramTypes, returnType);
            // create similar procedure symbol
            ProcedureSymbol psymbol = new ProcedureSymbol(functionName, paramTypes);

            // check that function symbol does not exist in the symbol table already
            // if it does exist --> report error
            if (symbolTable.IsFunctionSymbolInTable(symbol) || symbolTable.IsProcedureSymbolInTable(psymbol))
            {
                string functionStr = $"{functionName}({string.Join(", ", paramTypes)})";
                string errorMsg = $"SemanticError::Row {func.GetRow()}::Column {func.GetCol()}::";
                errorMsg += $"Name {functionStr} already defined in this scope!";
                ReportError(errorMsg);
            }
            else
            {
                // no problems detected --> add function symbol to table
                symbolTable.DeclareFunctionSymbol(symbol);
            }
        }

        /// <summary>
        /// Method <c>InitProceduresToSymbolTable</c> checks procedure definition and
        /// adds it to the symbol table.
        /// </summary>
        /// <param name="node">procedure node</param>
        private void InitProceduresToSymbolTable(INode node)
        {
            if (node == null) return;

            // convert node to procedure node
            ProcedureNode proc = (ProcedureNode)node;

            // get procedure name
            string procedureName = proc.GetName();

            // for each function parameter --> find parameter type
            // should be integer, string, boolean or array
            List<INode> parameters = proc.GetParameters();
            string[] paramTypes = new string[parameters.Count];

            for (int i = 0; i < parameters.Count; i++)
            {
                VariableNode varNode = (VariableNode)parameters[i];
                paramTypes[i] = EvaluateTypeOfTypeNode(varNode.GetVariableType());

                // declare variable to symbol table
                VariableSymbol sym = new VariableSymbol(varNode.GetName(), paramTypes[i], null, symbolTable.GetCurrentScope());
                symbolTable.DeclareVariableSymbol(sym);
            }

            // create new procedure symbol
            ProcedureSymbol symbol = new ProcedureSymbol(procedureName, paramTypes);
            // create similar function symbol
            FunctionSymbol fsymbol = new FunctionSymbol(procedureName, paramTypes, null);

            // check that porcedure symbol does not exist in the symbol table already
            // if it does exist --> report error
            if (symbolTable.IsProcedureSymbolInTable(symbol) || symbolTable.IsFunctionSymbolInTable(fsymbol))
            {
                string procedureStr = $"{procedureName}({string.Join(", ", paramTypes)})";
                string errorMsg = $"SemanticError::Row {proc.GetRow()}::Column {proc.GetCol()}::";
                errorMsg += $"Procedure /Function {procedureStr} already defined!";
                ReportError(errorMsg);
            }
            else
            {
                // no problems detected --> add function symbol to table
                symbolTable.DeclareProcedureSymbol(symbol);
            }
        }

        /// <summary>
        /// Method <c>CheckFunction</c> checks semantic constraints for function code block.
        /// </summary>
        private void CheckFunction(INode node)
        {
            if (node == null) return;

            symbolTable.AddScope();

            // convert node to function node
            FunctionNode func = (FunctionNode)node;

            // check the function code
            CheckBlock(func.GetBlock());

            symbolTable.RemoveScope();
        }

        /// <summary>
        /// Method <c>CheckProcedure</c> checks semantic constraints for procedure code block.
        /// </summary>
        private void CheckProcedure(INode node)
        {
            if (node == null) return;

            symbolTable.AddScope();

            // convert node to procedure node
            ProcedureNode proc = (ProcedureNode)node;

            // check the function code
            CheckBlock(proc.GetBlock());

            symbolTable.RemoveScope();

        }

        /// <summary>
        /// Method <c>CheckBlock</c> checks semantic constraints for block.
        /// </summary>
        private void CheckBlock(INode node)
        {
            if (node == null) return;

            // increase scope
            symbolTable.AddScope();

            // convert node to block node
            BlockNode block = (BlockNode)node;

            // get list of statements inside block
            List<INode> statements = block.GetStatements();

            // check semantics for each statement
            foreach(INode stmnt in statements)
            {
                CheckStatement(stmnt);
            }

            // remove scope
            symbolTable.RemoveScope();
        }

        /// <summary>
        /// Method <c>CheckStatement</c> checks semantic constraints for statement.
        /// </summary>
        private void CheckStatement(INode stmnt)
        {
            if (stmnt == null) return;

            switch (stmnt.GetNodeType())
            {
                case NodeType.ASSERT:
                    CheckAssert(stmnt);
                    break;
                case NodeType.ASSIGNMENT:
                    CheckAssignment(stmnt);
                    break;
                case NodeType.BLOCK:
                    CheckBlock(stmnt);
                    break;
                case NodeType.CALL:
                    EvaluateTypeOfCallNode(stmnt);
                    break;
                case NodeType.IF_ELSE:
                    CheckIfElse(stmnt);
                    break;
                case NodeType.RETURN:
                    CheckReturn(stmnt);
                    break;
                case NodeType.VARIABLE_DCL:
                    CheckVariableDcl(stmnt);
                    break;
                case NodeType.WHILE:
                    CheckWhile(stmnt);
                    break;
                default:
                    // something wrong with the AST
                    throw new InvalidOperationException($"Unexpected NodeType {stmnt.GetNodeType()} as block statement!");
            }
        }

        /// <summary>
        /// Method <c>CheckAssert</c> checks semantic constraints for assert statement.
        /// </summary>
        private void CheckAssert(INode node)
        {
            if (node == null) return;

            // convert inputnode to assernode
            AssertNode assert = (AssertNode)node;

            // get expression node
            INode expr = assert.GetExpression();

            // evaluate the result type of expression
            string returnType = EvaluateTypeOfExpressionNode(expr);

            // check that type is boolean, if not --> report
            if (returnType == null || !returnType.Equals(STR_BOOLEAN))
            {
                string errorMsg = $"SemanticError::Row {expr.GetRow()}::Column {expr.GetCol()}::";
                errorMsg += "Expected assertion expression to be type of boolean!";
                ReportError(errorMsg);
            }
        }

        /// <summary>
        /// Method <c>CheckAssignment</c> checks semantic constraints for assignment statement.
        /// </summary>
        private void CheckAssignment(INode node)
        {
            if (node == null) return;

            // convert assignment node
            AssignmentNode assign = (AssignmentNode)node;

            string identifier = assign.GetIdentifier();

            INode expr = assign.GetValueExpression();
            string type = EvaluateTypeOfExpressionNode(expr);

            // check that variable has been declared
            if (!symbolTable.IsVariableSymbolInTable(identifier))
            {
                // variable is not in table --> report error
                string errorMsg = $"SemanticError::Row {assign.GetRow()}::Column {assign.GetCol()}::";
                errorMsg += $"Variable {identifier} not declared in this scope!";
                ReportError(errorMsg);
                return;
            }

            // check that the type of assignment expression matches the variable
            VariableSymbol symbol = symbolTable.GetVariableSymbolByIdentifier(identifier);
            string symtype = symbol.GetSymbolType();

            if(type == null || !symtype.Equals(type))
            {
                // was not correct --> report error
                string errorMsg = $"SemanticError::Row {expr.GetRow()}::Column {expr.GetCol()}::";
                errorMsg += $"Cannot implicitly convert type {type} to {symtype}!";
                ReportError(errorMsg);
            }
        }

        /// <summary>
        /// Method <c>CheckIfElse</c> checks semantic constraints for if-else structure.
        /// </summary>
        private void CheckIfElse(INode node)
        {
            if (node == null) return;

            // convert to if-else node
            IfElseNode ifNode = (IfElseNode)node;

            INode condition = ifNode.GetCondition();

            string type = EvaluateTypeOfExpressionNode(condition);

            if (type != null && !type.Equals(STR_BOOLEAN)){
                // condition not boolean expression
                string errorMsg = $"SemanticError::Row {condition.GetRow()}::Column {condition.GetCol()}::";
                errorMsg += $"Cannot implicitly convert type {type} to boolean!";
                ReportError(errorMsg);
            }

            INode thenStatement = ifNode.GetThenStatement();
            INode elseStatement = ifNode.GetElseStatement();

            CheckStatement(thenStatement);
            CheckStatement(elseStatement);
        }

        /// <summary>
        /// Method <c>CheckReturn</c> checks the semantic constrains of return statement.
        /// </summary>
        private void CheckReturn(INode node)
        {
            if (node == null) return;

            // convert to return node
            ReturnNode retNode = (ReturnNode)node;

            if (returnStmntType == null || returnStmntType == "") return;

            // check that the return value corresponds to the return type
            // of current function
            INode expr = retNode.GetExpression();

            string type = EvaluateTypeOfExpressionNode(expr);

            if(type != null && !type.Equals(returnStmntType))
            {
                string errorMsg = $"SemanticError::Row {expr.GetRow()}::Column {expr.GetCol()}::";
                errorMsg += $"Cannot implicitly convert type {type} to {returnStmntType}!";
                ReportError(errorMsg);
            }
        }

        /// <summary>
        /// Method <c>CheckVariableDcl</c> checks semantic constraints for variable declaration.
        /// </summary>
        private void CheckVariableDcl(INode node)
        {
            if (node == null) return;

            // convert to variable declaration node
            VariableDclNode varDcl = (VariableDclNode)node;

            // get list of variables
            List<INode> variables = varDcl.GetVariables();

            // get type of variables
            INode typeNode = varDcl.GetVariableType();
            string type = EvaluateTypeOfTypeNode(typeNode, true);

            // for each variable --> check that they have not been declared in current scope
            // if not --> then declare it
            foreach(INode n in variables)
            {
                // get variable name
                VariableNode varNode = (VariableNode)n;
                string varName = varNode.GetName();

                // check that variable does not already exist in current scope
                if (symbolTable.IsVariableSymbolInTableWithCurrentScope(varName))
                {
                    // already exist --> report error
                    string errorMsg = $"SemanticError::Row {varDcl.GetRow()}::Column {varDcl.GetCol()}::";
                    errorMsg += $"Variable {varName} already declared in this scope!";
                    ReportError(errorMsg);
                }
                else
                {
                    // check if variable exists in table
                    if (symbolTable.IsVariableSymbolInTable(varName))
                    {
                        // exists in earlier scope --> redefine
                        int scope = symbolTable.GetCurrentScope();
                        symbolTable.ReDeclareVariableSymbol(new VariableSymbol(varName, type, null, scope));
                    }
                    else
                    {
                        // does not exist in table --> declare
                        int scope = symbolTable.GetCurrentScope();
                        symbolTable.DeclareVariableSymbol(new VariableSymbol(varName, type, null, scope));
                    }
                }
            }
        }

        /// <summary>
        /// Method <c>CheckWhile</c> checks semantic constraints for while loop.
        /// </summary>
        private void CheckWhile(INode node)
        {
            if (node == null) return;

            // conver to while loop node
            WhileNode whileNode = (WhileNode)node;

            INode expr = whileNode.GetBooleanExpression();
            INode stmnt = whileNode.GetStatement();

            string type = EvaluateTypeOfExpressionNode(expr);

            if(type == null || !type.Equals(STR_BOOLEAN))
            {
                // report error
                string errorMsg = $"SemanticError::Row {expr.GetRow()}::Column {expr.GetCol()}::";
                errorMsg += $"Cannot implicitly convert type {type} to boolean!";
                ReportError(errorMsg);
            }

            CheckStatement(stmnt);
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfTypeNode</c> returns the parameter type
        /// of given typenode. Inputnode should be either SimpleTypeNode or ArrayTypeNode.
        /// </summary>
        private string EvaluateTypeOfTypeNode(INode node, bool isInit = false)
        {
            if (node == null) return null;

            switch (node.GetNodeType())
            {
                case NodeType.ARRAY_TYPE:
                    ArrayTypeNode at = (ArrayTypeNode)node;
                    if (isInit)
                    {
                        // array should have integer as size argument
                        INode expr = at.GetSizeExpression();
                        string type = EvaluateTypeOfExpressionNode(expr);

                        if (type == null || !type.Equals(STR_INTEGER))
                        {
                            // report error
                            string errorMsg = $"SemanticError::Row {at.GetRow()}::Column {at.GetCol()}::";
                            errorMsg += $"Array must have integer as size argument!";
                            ReportError(errorMsg);
                        } 
                    }
                    string tmp = $"array[] of ";
                    SimpleTypeNode stn = (SimpleTypeNode)at.GetSimpleType();
                    tmp += stn.GetTypeValue();
                    return tmp;
                case NodeType.SIMPLE_TYPE:
                    stn = (SimpleTypeNode)node;
                    return stn.GetTypeValue();
                default:
                    // something wrong with the AST
                    throw new InvalidOperationException($"Unexpected NodeType {node.GetNodeType()} as type!");
            }
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfExpressionNode</c> returns the type 
        /// of given expression node.
        /// </summary>
        private string EvaluateTypeOfExpressionNode(INode node)
        {
            if (node == null) return null;

            switch (node.GetNodeType())
            {
                case NodeType.SIGN:
                    return EvaluateTypeOfSignNode(node);
                case NodeType.UNARY_EXPRESSION:
                    return EvaluateTypeOfUnaryExpressionNode(node);
                case NodeType.BINARY_EXPRESSION:
                    return EvaluateTypeOfBinaryExpressionNode(node);
                case NodeType.VARIABLE:
                    return EvaluateTypeOfVariableNode(node);
                case NodeType.INTEGER:
                    return STR_INTEGER;
                case NodeType.STRING:
                    return STR_STRING;
                case NodeType.REAL:
                    return STR_REAL;
                case NodeType.CALL:
                    return EvaluateTypeOfCallNode(node);
                default:
                    // something wrong with the AST
                    throw new InvalidOperationException($"Unexpected NodeType {node.GetNodeType()} as expression!");
            }
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfSignNode</c> returns the type of given sign node.
        /// If type is valid, "integer" or "real" is returned. If type is invalid,
        /// null is returned.
        /// </summary>
        private string EvaluateTypeOfSignNode(INode node)
        {
            if (node == null) return null;

            // convert to sign node
            SignNode signNode = (SignNode)node;

            // get term
            INode term = signNode.GetTerm();

            // get type of term
            string type = EvaluateTypeOfExpressionNode(term);

            // check if valid type
            if (type.Equals(STR_INTEGER) || type.Equals(STR_REAL)) return type;

            // was not valid type --> report
            string errorMsg = $"SemanticError::Row {term.GetRow()}::Column {term.GetCol()}::";
            errorMsg += $"Cannot implicitly convert type {type} to integer or real!";
            ReportError(errorMsg);
            return null;
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfUnaryExpressionNode</c> returns the type of unary expression node.
        /// </summary>
        private string EvaluateTypeOfUnaryExpressionNode(INode node)
        {
            if (node == null) return null;

            // convert node to unary expression node
            UnaryExpressionNode unary = (UnaryExpressionNode)node;

            // get expression
            INode expr = unary.GetExpression();

            // get type of expression
            string type = EvaluateTypeOfExpressionNode(expr);

            switch (unary.GetOperator())
            {
                case "not":
                    // check that type is boolean
                    if (type.Equals(STR_BOOLEAN)) return type;
                    
                    // was not correct type --> report error
                    string errorMsg = $"SemanticError::Row {node.GetRow()}::Column {node.GetCol()}::";
                    errorMsg += $"Cannot implicitly convert type {type} to boolean!";
                    ReportError(errorMsg);
                    return null;
                case "size":
                    // check that type is integer "array[] of integer"
                    if (type.Equals("array[] of " + STR_INTEGER)) return STR_INTEGER;

                    // was not correct type --> report error
                    errorMsg = $"SemanticError::Row {node.GetRow()}::Column {node.GetCol()}::";
                    errorMsg += $"Cannot implicitly convert type {type} to integer!";
                    ReportError(errorMsg);
                    return null;
                default:
                    // something wrong with the AST
                    throw new InvalidOperationException($"Unsupported unary operation {unary.GetOperator()}!");
            }
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfBinaryExpressionNode</c> returns the type of 
        /// binaery expression node.
        /// </summary>
        private string EvaluateTypeOfBinaryExpressionNode(INode node)
        {
            if (node == null) return null;

            // convert to binary expression node
            BinaryExpressionNode bin = (BinaryExpressionNode)node;

            // get operation
            string op = bin.GetOperation();

            switch (op)
            {
                case "and":
                case "or":
                    string[] supportedTypes1 = { STR_BOOLEAN };
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes1, null);
                case "%":
                    string[] supportedTypes2 = { STR_INTEGER };
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes2, null);
                case "+":
                    string[] supportedTypes3 = { STR_INTEGER, STR_REAL, STR_STRING };
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes3, null);
                case "-":
                case "*":
                case "/":
                    string[] supportedTypes4 = { STR_INTEGER, STR_REAL };
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes4, null);
                case "=":
                case "<>":
                case "<":
                case "<=":
                case ">=":
                case ">":
                    string[] supportedTypes5 = { STR_INTEGER, STR_REAL, STR_STRING, STR_BOOLEAN };
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes5, STR_BOOLEAN);
                case "[]":
                    return EvaluateTypeOfArrayIndexNode(node);
                default:
                    // something wrong with the AST
                    throw new InvalidOperationException($"Unsupported binary operator {op}!");
            }
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfBinaryOperationForSupportedTypes</c> performs type check
        /// for binary operation. Returns type if it is in supported types list.
        /// Otherwise null is returned.
        private string EvaluateTypeOfBinaryOperationForSupportedTypes(INode node, string[] supportedTypes, string overrideReturnType)
        {
            if (node == null) return null;

            // convert to binary expression node
            BinaryExpressionNode bin = (BinaryExpressionNode)node;

            INode lhs = bin.GetLhs();
            INode rhs = bin.GetRhs();

            string left = EvaluateTypeOfExpressionNode(lhs);
            string right = EvaluateTypeOfExpressionNode(rhs);

            // if correct 
            foreach(string type in supportedTypes)
            {
                if (left != null && left.Equals(type) && right != null && right.Equals(type))
                {
                    if (overrideReturnType != null) return overrideReturnType;
                    return type;
                } 
            }

            // was not correct --> report error
            string errorMsg = $"SemanticError::Row {bin.GetRow()}::Column {bin.GetCol()}::";
            errorMsg += $"Operation {bin.GetOperation()} not supported between types {left} and {right}";
            ReportError(errorMsg);
            return null;
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfArrayIndexNode</c> check the type of array index node.
        /// It will return the simple type of array, and check that index expression
        /// evaluates as integer.
        /// </summary>
        private string EvaluateTypeOfArrayIndexNode(INode node)
        {
            if (node == null) return null;

            // convert to arrayindex node
            BinaryExpressionNode arrayIndex = (BinaryExpressionNode)node;

            // get expression that defines index of array
            INode expression = arrayIndex.GetRhs();

            // evaluate type of index expression
            string type = EvaluateTypeOfExpressionNode(expression);

            // check if type was not integer
            if (!type.Equals(STR_INTEGER))
            {
                // was not correct --> report error
                string errorMsg = $"SemanticError::Row {expression.GetRow()}::Column {expression.GetCol()}::";
                errorMsg += $"Cannot implicitly convert type {type} to integer!";
                ReportError(errorMsg);
            }

            // get array
            VariableNode array = (VariableNode) arrayIndex.GetLhs();

            string arrayType = EvaluateTypeOfVariableNode(array);

            if(arrayType != null && arrayType.Equals("array[] of " + STR_INTEGER)) return STR_INTEGER;
            if (arrayType != null && arrayType.Equals("array[] of " + STR_REAL)) return STR_REAL;
            if (arrayType != null && arrayType.Equals("array[] of " + STR_STRING)) return STR_STRING;
            if (arrayType != null && arrayType.Equals("array[] of " + STR_BOOLEAN)) return STR_BOOLEAN;
            return null;
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfVariableNode</c> checks the type of varaible node.
        /// Returns null in case of errors. if variable name is "true" or "false" and they
        /// do not exist in current scope, then it is assumed variable is actually 
        /// a boolean type (true or false).
        /// </summary>
        private string EvaluateTypeOfVariableNode(INode node)
        {
            if (node == null) return null;

            // convert to variable node
            VariableNode varNode = (VariableNode)node;
            string identifier = varNode.GetName();

            if (symbolTable.IsVariableSymbolInTable(identifier))
            {
                VariableSymbol varSym = symbolTable.GetVariableSymbolByIdentifier(identifier);
                return varSym.GetSymbolType();
            }

            // identifier is not in table --> check if true or false
            if (identifier.Equals("true") || identifier.Equals("false"))
            {
                return STR_BOOLEAN;
            }

            // variable is not in table --> report error
            string errorMsg = $"SemanticError::Row {node.GetRow()}::Column {node.GetCol()}::";
            errorMsg += $"Variable {identifier} not declared in this scope!";
            ReportError(errorMsg);
            return null;
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfCallNode</c> peforms semantic analysis on call node
        /// and returns the returns the type of return value.
        /// </summary>
        private string EvaluateTypeOfCallNode(INode node)
        {
            if (node == null) return null;

            // conver to call node
            CallNode callNode = (CallNode)node;

            // get name of the function or procedure that is called
            string identifier = callNode.GetId();

            List<INode> arguments = callNode.GetArguments();
            string[] parameters = new string[arguments.Count];
            for(int i = 0; i < arguments.Count; i++)
            {
                string type = EvaluateTypeOfExpressionNode(arguments[i]);
                if (type != null) parameters[i] = type;
            }

            // check if function with same name exists in table
            bool functionNameExists = symbolTable.IsFunctionInTable(identifier);

            // check if procedure with same name exists in table
            bool procedureNameExists = symbolTable.IsProcedureInTable(identifier);

            // could be a function call
            FunctionSymbol fs = new FunctionSymbol(identifier, parameters, null);
            bool functionInTable = symbolTable.IsFunctionSymbolInTable(fs);

            // could be a procedure call
            ProcedureSymbol ps = new ProcedureSymbol(identifier, parameters);
            bool procedureInTable = symbolTable.IsProcedureSymbolInTable(ps);

            // is a function
            if(functionNameExists && !procedureNameExists) return CheckFunctionCall(node, functionInTable, identifier, parameters);
            // is a procedure
            if (!functionNameExists && procedureNameExists) return CheckProcedureCall();
            // can be both function or procedure call
            if (functionNameExists && procedureNameExists) return CheckProcedureFunctionCall();
            // is predefined read procedure call
            if (identifier.Equals("read")) return CheckReadCall();
            // is predefined writeln procedure call
            if (identifier.Equals("writeln")) return CheckWritelnCall();
            
            // is not a valid function or procedure name --> report error
            string errorMsg = $"SemanticError::Row {node.GetRow()}::Column {node.GetCol()}::";
            errorMsg += $"Procedure/Function {identifier} not declared in this scope!";
            ReportError(errorMsg);
            return null;
        }

        /// <summary>
        /// Method <c>CheckFunctionCall</c> checks that function call parameters are correct.
        /// </summary>
        /// <param name="functionInTable"></param>
        /// <param name="identifier"></param>
        /// <param name="parameters"></param>
        /// <returns>return type of most similar function definition</returns>
        private string CheckFunctionCall(INode node, bool functionInTable, string identifier, string[] parameters)
        {
            if (functionInTable)
            {
                FunctionSymbol fsymbol = symbolTable.GetFunctionSymbolByIdentifierAndArguments(identifier, parameters);
                return fsymbol.GetReturnType();
            }
            else
            {
                // incorrect arguments --> report error
                FunctionSymbol similar = symbolTable.GetMostSimilarFunctionSymbol(identifier, parameters);

                string errorMsg = $"SemanticError::Row {node.GetRow()}::Column {node.GetCol()}::";
                errorMsg += $"Procedure/Function arguments are invalid!";
                ReportError(errorMsg);

                return similar.GetReturnType();
            }
        }

        private string CheckProcedureCall()
        {
            /*
            if (procedureInTable)
            {
                // correct call --> return type

                return null;
            }
            else
            {
                // incorrect arguments --> report error

                return null;
            }*/
            return null;
        }

        private string CheckReadCall()
        {
            // TODO
            return null;
        }

        private string CheckWritelnCall()
        {
            // TODO
            return null;
        }

        private string CheckProcedureFunctionCall()
        {
            /*
            if (functionInTable)
            {
                // correct arguments for function call

                return null;
            }
            else if (procedureInTable)
            {
                // incorrect arguments for function call

                return null;
            }
            else
            {
                // wrong arguments --> report error

                return null;
            }
            */
            return null;
        }

    }
}
