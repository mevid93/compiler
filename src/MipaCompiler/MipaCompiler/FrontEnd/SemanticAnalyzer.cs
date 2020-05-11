
using MipaCompiler.Node;
using MipaCompiler.Symbol;
using System;
using System.Collections.Generic;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>SemanticAnalyzer</c> holds functionality to perform semantic analysis for
    /// intermediate representation of source code. In other words, it takes
    /// AST as input, checks semantic constraints and reports any errors it finds.
    /// </summary>
    public class SemanticAnalyzer
    {
        private readonly INode ast;                     // AST representation of the source code
        private readonly List<string> errors;           // list of all detected errors
        private readonly SymbolTable symbolTable;       // symbol table to store variables, functions and procedures
        private string returnStmntType;                 // type of return value expected

        /// <summary>
        /// Constructor <c>SemanticAnalyzer</c> creates new SemanticAnalyzer-object.
        /// </summary>
        /// <param name="ast">AST</param>
        public SemanticAnalyzer(INode ast)
        {
            this.ast = ast;
            errors = new List<string>();
            symbolTable = new SymbolTable();
        }

        /// <summary>
        /// Method <c>ErrorsDetected</c> returns true if errors were detected
        /// during semantic analysis.
        /// </summary>
        /// <returns>true if errors were detected</returns>
        public bool ErrosDetected()
        {
            return errors.Count > 0;
        }

        /// <summary>
        /// Method <c>GetDetectedErrors</c> returns the list of detected semantic errors.
        /// </summary>
        /// <returns>list of errors</returns>
        public List<string> GetDetectedErrors()
        {
            return errors;
        }

        /// <summary>
        /// Method <c>CheckConstraints</c> checks the semantic constraints of the source code.
        /// </summary>
        public void CheckConstraints()
        {
            // check that code actually exists
            if (ast == null)
            {
                errors.Add($"SemanticError::Row ?::Column ?::No code to analyze!");
                return;
            }
            CheckProgram();
        }

        ///////////////////////////////// ACTUAL SEMANTIC CHECKS /////////////////////////////////

        // different types that are used to describe the type of variables and values
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
        /// and adds it to the symbol table.
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
                paramTypes[i] = EvaluateTypeOfTypeNode(varNode.GetVariableType(), errors, symbolTable);

                // declare variable to symbol table
                VariableSymbol sym = new VariableSymbol(varNode.GetName(), paramTypes[i], null, symbolTable.GetCurrentScope());
                symbolTable.DeclareVariableSymbol(sym);
            }

            // find return type
            string returnType = EvaluateTypeOfTypeNode(func.GetReturnType(), errors, symbolTable);
            returnStmntType = returnType;

            // create new function symbol
            FunctionSymbol symbol = new FunctionSymbol(functionName, paramTypes, returnType);
            // create similar procedure symbol
            ProcedureSymbol psymbol = new ProcedureSymbol(functionName, paramTypes);

            // check that function symbol does not exist in the symbol table already
            // if it does exist --> report error
            if (symbolTable.IsFunctionSymbolInTable(symbol) || symbolTable.IsProcedureSymbolInTable(psymbol))
            {
                string errorMsg = $"Name {functionName} already defined in this scope!";
                ReportError(func.GetRow(), func.GetCol(), errors, errorMsg);
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
                paramTypes[i] = EvaluateTypeOfTypeNode(varNode.GetVariableType(), errors, symbolTable);

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
                string errorMsg = $"Name {procedureName} already defined in this scope!";
                ReportError(proc.GetRow(), proc.GetCol(), errors, errorMsg);
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
        /// Method <c>CheckBlock</c> checks semantic constraints for code block.
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
            foreach (INode stmnt in statements)
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
                    EvaluateTypeOfCallNode(stmnt, errors, symbolTable);
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
            string returnType = EvaluateTypeOfExpressionNode(expr, errors, symbolTable);

            // check that type is boolean, if not --> report
            if (returnType == null || !returnType.Equals(STR_BOOLEAN))
            {
                string errorMsg = "Expected assertion expression to be type of boolean!";
                ReportError(expr.GetRow(), expr.GetCol(), errors, errorMsg);
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

            // assignment node can have bionary expression or array
            INode identifierNode = assign.GetIdentifier();

            // get identifier name of assign target
            string identifier = null;
            if(identifierNode.GetNodeType() == NodeType.VARIABLE)
            {
                VariableNode varNode = (VariableNode)identifierNode;
                identifier = varNode.GetName();
            }
            else
            {
                BinaryExpressionNode bin = (BinaryExpressionNode)identifierNode;
                VariableNode varNode = (VariableNode)bin.GetLhs();
                identifier = varNode.GetName();
            }

            INode expr = assign.GetValueExpression();
            string type = EvaluateTypeOfExpressionNode(expr, errors, symbolTable);

            // check that variable has been declared
            if (!symbolTable.IsVariableSymbolInTable(identifier))
            {
                // variable is not in table --> report error
                string errorMsg = $"Variable {identifier} not declared in this scope!";
                ReportError(assign.GetRow(), assign.GetCol(), errors, errorMsg);
                return;
            }

            // check that the type of assignment expression matches the variable
            string symtype = EvaluateTypeOfExpressionNode(identifierNode, errors, symbolTable);

            // if assignment target is real and assigned value is int --> ok
            if(type != null && symtype != null && symtype.Equals("real") && type.Equals("integer"))
            {
                return;
            }

            if (type == null || !symtype.Equals(type))
            {
                // was not correct --> report error
                string errorMsg = $"Cannot implicitly convert type {type} to {symtype}!";
                ReportError(expr.GetRow(), expr.GetCol(), errors, errorMsg);
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

            string type = EvaluateTypeOfExpressionNode(condition, errors, symbolTable);

            if (type != null && !type.Equals(STR_BOOLEAN))
            {
                // condition not boolean expression
                string errorMsg = $"Cannot implicitly convert type {type} to boolean!";
                ReportError(condition.GetRow(), condition.GetCol(), errors, errorMsg);
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

            string type = EvaluateTypeOfExpressionNode(expr, errors, symbolTable);

            if (type != null && !type.Equals(returnStmntType))
            {
                string errorMsg = $"Cannot implicitly convert type {type} to {returnStmntType}!";
                ReportError(expr.GetRow(), expr.GetCol(), errors, errorMsg);
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
            string type = EvaluateTypeOfTypeNode(typeNode, errors, symbolTable);

            // for each variable --> check that they have not been declared in current scope
            // if not --> then declare it
            foreach (INode n in variables)
            {
                // get variable name
                VariableNode varNode = (VariableNode)n;
                string varName = varNode.GetName();

                // check that variable does not already exist in current scope
                if (symbolTable.IsVariableSymbolInTableWithCurrentScope(varName))
                {
                    // already exist --> report error
                    string errorMsg = $"Variable {varName} already declared in this scope!";
                    ReportError(varDcl.GetRow(), varDcl.GetCol(), errors, errorMsg);
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

            string type = EvaluateTypeOfExpressionNode(expr, errors, symbolTable);

            if (type == null || !type.Equals(STR_BOOLEAN))
            {
                // report error
                string errorMsg = $"Cannot implicitly convert type {type} to boolean!";
                ReportError(expr.GetRow(), expr.GetCol(), errors, errorMsg);
            }

            CheckStatement(stmnt);
        }

        ///////////////////////////////// STATIC TYPE EVALUATION METHODS  /////////////////////////////////

        /// <summary>
        /// Static method <c>ReportError</c> prints error message to user and adds it to
        /// the list of reported errors.
        /// </summary>
        private static void ReportError(int row, int col, List<string> errors, string errorMsg)
        {
            string message = $"SemanticError::Row {row}::Column {col}::" + errorMsg;
            Console.WriteLine(message);
            errors.Add(message);
        }

        /// <summary>
        /// Static method <c>EvaluateTypeOfTypeNode</c> returns the type of given typenode. 
        /// Inputnode should be either SimpleTypeNode or ArrayTypeNode. Returns null in case
        /// errors were detected.
        /// </summary>
        /// <param name="node">typenode</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <param name="isInit">if array initialization type</param>
        /// <returns>type in string representation</returns>
        public static string EvaluateTypeOfTypeNode(INode node, List<string> errors, SymbolTable symbolTable)
        {
            if (node == null) return null;

            switch (node.GetNodeType())
            {
                case NodeType.ARRAY_TYPE:
                    ArrayTypeNode at = (ArrayTypeNode)node;
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
        /// Static method <c>EvaluateTypeOfExpressionNode</c> returns the type 
        /// of result from given expression node. Returns null if type if errors are detected.
        /// </summary>
        /// <param name="node">expression node</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>type of expression result</returns>
        public static string EvaluateTypeOfExpressionNode(INode node, List<string> errors, SymbolTable symbolTable)
        {
            if (node == null) return null;

            switch (node.GetNodeType())
            {
                case NodeType.SIGN:
                    return EvaluateTypeOfSignNode(node, errors, symbolTable);
                case NodeType.UNARY_EXPRESSION:
                    return EvaluateTypeOfUnaryExpressionNode(node, errors, symbolTable);
                case NodeType.BINARY_EXPRESSION:
                    return EvaluateTypeOfBinaryExpressionNode(node, errors, symbolTable);
                case NodeType.VARIABLE:
                    return EvaluateTypeOfVariableNode(node, errors, symbolTable);
                case NodeType.INTEGER:
                    return STR_INTEGER;
                case NodeType.STRING:
                    return STR_STRING;
                case NodeType.REAL:
                    return STR_REAL;
                case NodeType.CALL:
                    return EvaluateTypeOfCallNode(node, errors, symbolTable);
                default:
                    // something wrong with the AST
                    throw new InvalidOperationException($"Unexpected NodeType {node.GetNodeType()} as expression!");
            }
        }

        /// <summary>
        /// Static method <c>EvaluateTypeOfSignNode</c> returns the result type of given sign node.
        /// If type is valid, "integer" or "real" is returned. If errors are detected, null is returned.
        /// </summary>
        /// <param name="node">sign node</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>result type</returns>
        public static string EvaluateTypeOfSignNode(INode node, List<string> errors, SymbolTable symbolTable)
        {
            if (node == null) return null;

            // convert to sign node
            SignNode signNode = (SignNode)node;

            // get term
            INode term = signNode.GetTerm();

            // get type of term
            string type = EvaluateTypeOfExpressionNode(term, errors, symbolTable);

            // check if valid type
            if (type.Equals(STR_INTEGER) || type.Equals(STR_REAL)) return type;

            // was not valid type --> report
            string errorMsg = $"Cannot implicitly convert type {type} to integer or real!";
            ReportError(term.GetRow(), term.GetCol(), errors, errorMsg);
            return null;
        }

        /// <summary>
        /// Static method <c>EvaluateTypeOfUnaryExpressionNode</c> returns the result type 
        /// of unary expression node. If errors are detected, returns null.
        /// </summary>
        /// <param name="node">unary expression node</param>
        /// <param name="errors">list of errors detected</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>result type</returns>
        public static string EvaluateTypeOfUnaryExpressionNode(INode node, List<string> errors, SymbolTable symbolTable)
        {
            if (node == null) return null;

            // convert node to unary expression node
            UnaryExpressionNode unary = (UnaryExpressionNode)node;

            // get expression
            INode expr = unary.GetExpression();

            // get type of expression
            string type = EvaluateTypeOfExpressionNode(expr, errors, symbolTable);

            switch (unary.GetOperator())
            {
                case "not":
                    // check that type is boolean
                    if (type.Equals(STR_BOOLEAN)) return type;

                    // was not correct type --> report error
                    string errorMsg = $"Cannot implicitly convert type {type} to boolean!";
                    ReportError(node.GetRow(), node.GetCol(), errors, errorMsg);
                    return null;
                case "size":
                    // check that type is array
                    if (type.Contains("array")) return STR_INTEGER;
                    
                    // was not correct type --> report error
                    errorMsg = $"Size operation cannot be used with type {type}!";
                    ReportError(node.GetRow(), node.GetCol(), errors, errorMsg);
                    return null;
                default:
                    // something wrong with the AST
                    throw new InvalidOperationException($"Unsupported unary operation {unary.GetOperator()}!");
            }
        }

        /// <summary>
        /// Static method <c>EvaluateTypeOfBinaryExpressionNode</c> returns the result type 
        /// of binaery expression node. If errors are detected, null is returned.
        /// </summary>
        /// <param name="node">binary expression node</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>result type</returns>
        public static string EvaluateTypeOfBinaryExpressionNode(INode node, List<string> errors, SymbolTable symbolTable)
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
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes1, null, errors, symbolTable);
                case "%":
                    string[] supportedTypes2 = { STR_INTEGER };
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes2, null, errors, symbolTable);
                case "+":
                    string[] supportedTypes3 = { STR_INTEGER, STR_REAL, STR_STRING };
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes3, null, errors, symbolTable);
                case "-":
                case "*":
                case "/":
                    string[] supportedTypes4 = { STR_INTEGER, STR_REAL };
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes4, null, errors, symbolTable);
                case "=":
                case "<>":
                case "<":
                case "<=":
                case ">=":
                case ">":
                    string[] supportedTypes5 = { STR_INTEGER, STR_REAL, STR_STRING, STR_BOOLEAN };
                    return EvaluateTypeOfBinaryOperationForSupportedTypes(node, supportedTypes5, STR_BOOLEAN, errors, symbolTable);
                case "[]":
                    return EvaluateTypeOfArrayIndexNode(node, errors, symbolTable);
                default:
                    // something wrong with the AST
                    throw new InvalidOperationException($"Unsupported binary operator {op}!");
            }
        }

        /// <summary>
        /// Static method <c>EvaluateTypeOfBinaryOperationForSupportedTypes</c> performs type check
        /// for binary operation. Returns result type if it is in supported types list.
        /// Otherwise null is returned.
        /// </summary>
        /// <param name="node">binary expression node</param>
        /// <param name="supportedTypes">list of supported result types</param>
        /// <param name="overrideReturnType">override return type</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>type</returns>
        public static string EvaluateTypeOfBinaryOperationForSupportedTypes(INode node, string[] supportedTypes, string overrideReturnType, List<string> errors, SymbolTable symbolTable)
        {
            if (node == null) return null;

            // convert to binary expression node
            BinaryExpressionNode bin = (BinaryExpressionNode)node;

            INode lhs = bin.GetLhs();
            INode rhs = bin.GetRhs();

            string left = EvaluateTypeOfExpressionNode(lhs, errors, symbolTable);
            string right = EvaluateTypeOfExpressionNode(rhs, errors, symbolTable);

            if (left == null ||right == null) return null;

            // if left and right have same type and that type is among allowed types --> then ok
            foreach (string type in supportedTypes)
            {
                if (left != null && left.Equals(type) && right != null && right.Equals(type))
                {
                    if (overrideReturnType != null) return overrideReturnType;
                    return type;
                }
            }
            
            // there are also cases where left and right do not have same type but still valid
            // for example, it is ok to sum integer and real
            if ((left.Equals(STR_INTEGER) || left.Equals(STR_REAL)) && (right.Equals(STR_INTEGER) || right.Equals(STR_REAL)))
            {
                bool realIsSupported = false;
                bool integerIsSupported = false;
                foreach(string type in supportedTypes)
                {
                    if (type == "integer") integerIsSupported = true;
                    if (type == "real") realIsSupported = true;
                }

                if (realIsSupported && integerIsSupported) return "real";
            }

            // was not correct --> report error
            string errorMsg = $"Operation {bin.GetOperation()} not supported between types {left} and {right}";
            ReportError(bin.GetRow(), bin.GetCol(), errors, errorMsg);
            return null;
        }

        /// <summary>
        /// Static method <c>EvaluateTypeOfArrayIndexNode</c> checks the result type 
        /// of array index node. It will return the simple type of array, and check that 
        /// index expression evaluates as integer. If errors are detected, null is returned.
        /// </summary>
        /// <param name="node">binary expression node</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>type</returns>
        public static string EvaluateTypeOfArrayIndexNode(INode node, List<string> errors, SymbolTable symbolTable)
        {
            if (node == null) return null;
            
            // convert to arrayindex node
            BinaryExpressionNode arrayIndex = (BinaryExpressionNode)node;

            // get expression that defines index of array
            INode expression = arrayIndex.GetRhs();

            if(expression != null)
            {
                // evaluate type of index expression
                string type = EvaluateTypeOfExpressionNode(expression, errors, symbolTable);

                // check if type was not integer
                if (type == null || !type.Equals(STR_INTEGER))
                {
                    // was not correct --> report error
                    string errorMsg = $"Cannot implicitly convert type {type} to integer!";
                    ReportError(expression.GetRow(), expression.GetCol(), errors, errorMsg);
                }
            }

            // get array
            VariableNode array = (VariableNode)arrayIndex.GetLhs();

            string arrayType = EvaluateTypeOfVariableNode(array, errors, symbolTable);

            if (arrayType != null && arrayType.Equals("array[] of " + STR_INTEGER)) return STR_INTEGER;
            if (arrayType != null && arrayType.Equals("array[] of " + STR_REAL)) return STR_REAL;
            if (arrayType != null && arrayType.Equals("array[] of " + STR_STRING)) return STR_STRING;
            if (arrayType != null && arrayType.Equals("array[] of " + STR_BOOLEAN)) return STR_BOOLEAN;
            return null;
        }

        /// <summary>
        /// Static method <c>EvaluateTypeOfVariableNode</c> checks the result type of variable node.
        /// Returns null in case of errors. If variable name is "true" or "false" and they
        /// do not exist in current scope, then it is assumed variable is actually 
        /// a boolean type (true or false).
        /// </summary>
        /// <param name="node">variable node</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>result type</returns>
        public static string EvaluateTypeOfVariableNode(INode node, List<string> errors, SymbolTable symbolTable)
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
            string errorMsg = $"Variable {identifier} not declared in this scope!";
            ReportError(node.GetRow(), node.GetCol(), errors, errorMsg);
            return null;
        }

        /// <summary>
        /// Static method <c>EvaluateTypeOfCallNode</c> peforms semantic analysis on call node
        /// and returns the returns the type of return value. If errors are detected, null is returned.
        /// </summary>
        /// <param name="node">call node</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>type</returns>
        public static string EvaluateTypeOfCallNode(INode node, List<string> errors, SymbolTable symbolTable)
        {
            if (node == null) return null;

            // conver to call node
            CallNode callNode = (CallNode)node;

            // get name of the function or procedure that is called
            string identifier = callNode.GetId();

            List<INode> arguments = callNode.GetArguments();
            string[] parameters = new string[arguments.Count];
            for (int i = 0; i < arguments.Count; i++)
            {
                string type = EvaluateTypeOfExpressionNode(arguments[i], errors, symbolTable);
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
            if (functionNameExists && !procedureNameExists) return CheckFunctionCall(node, functionInTable, identifier, parameters, errors, symbolTable);
            // is a procedure
            if (!functionNameExists && procedureNameExists) return CheckProcedureCall(node, procedureInTable, identifier, parameters, errors, symbolTable);
            // can be both function or procedure call
            if (functionNameExists && procedureNameExists) return CheckProcedureFunctionCall(node, functionInTable, procedureInTable, identifier, parameters, errors, symbolTable);
            // is predefined read procedure call
            if (identifier.Equals("read")) return CheckReadCall(node, errors, symbolTable);
            // is predefined writeln procedure call
            if (identifier.Equals("writeln")) return CheckWritelnCall(node, errors, symbolTable);

            // is not a valid function or procedure name --> report error
            string errorMsg = $"SemanticError::Row {node.GetRow()}::Column {node.GetCol()}::";
            errorMsg += $"Procedure/Function {identifier} not declared in this scope!";
            ReportError(node.GetRow(), node.GetCol(), errors, errorMsg);
            return null;
        }

        /// <summary>
        /// Static method <c>CheckFunctionCall</c> checks that function call parameters are correct.
        /// Returns function return type.
        /// </summary>
        /// <param name="node">function call node</param>
        /// <param name="functionInTable">does function exist in table</param>
        /// <param name="identifier">function name</param>
        /// <param name="parameters">parameter list</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>return type of the function return value</returns>
        public static string CheckFunctionCall(INode node, bool functionInTable, string identifier, string[] parameters, List<string> errors, SymbolTable symbolTable)
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

                string errorMsg = $"Procedure/Function arguments are invalid!";
                ReportError(node.GetRow(), node.GetCol(), errors, errorMsg);

                return similar.GetReturnType();
            }
        }

        /// <summary>
        /// Static method <c>CheckProcedureCall</c> checks that the procedure call parameters are correct.
        /// </summary>
        /// <param name="node">procedure call node</param>
        /// <param name="procedureInTable">procedure in table</param>
        /// <param name="identifier">procedure name</param>
        /// <param name="parameters">parameters</param>
        /// <param name="errors">list of detected erros</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>null</returns>
        public static string CheckProcedureCall(INode node, bool procedureInTable, string identifier, string[] parameters, List<string> errors, SymbolTable symbolTable)
        {
            if (procedureInTable)
            {
                ProcedureSymbol psymbol = symbolTable.GetProcedureSymbolByIdentifierAndArguments(identifier, parameters);
            }
            else
            {
                // incorrect arguments --> report error
                ProcedureSymbol similar = symbolTable.GetMostSimilarProcedureSymbol(identifier, parameters);

                string errorMsg = $"Procedure/Function arguments are invalid!";
                ReportError(node.GetRow(), node.GetCol(), errors, errorMsg);
            }
            return null;
        }

        /// <summary>
        /// Static method <c>CheckReadCall</c> checks that the parameters of read call are correct type.
        /// Returns null.
        /// </summary>
        /// <param name="node">read call node</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>null</returns>
        public static string CheckReadCall(INode node, List<string> errors, SymbolTable symbolTable)
        {
            if (node == null) return null;

            CallNode callNode = (CallNode)node;

            // check that all paramateres are correct type and declared in scope
            foreach (INode p in callNode.GetArguments())
            {
                VariableNode varNode = null;
                if (p.GetNodeType() != NodeType.VARIABLE && p.GetNodeType() != NodeType.BINARY_EXPRESSION)
                {
                    string notVariable = "Argument must be a variable!";
                    ReportError(p.GetRow(), p.GetCol(), errors, notVariable);
                    continue;
                }
                else if (p.GetNodeType() == NodeType.BINARY_EXPRESSION)
                {
                    BinaryExpressionNode bin = (BinaryExpressionNode)p;
                    INode lhs = bin.GetLhs();
                    if (lhs.GetNodeType() != NodeType.VARIABLE)
                    {
                        string notVariable = "Argument must be a variable!";
                        ReportError(p.GetRow(), p.GetCol(), errors, notVariable);
                        continue;
                    }
                    varNode = (VariableNode)lhs;
                }
                else if (p.GetNodeType() == NodeType.VARIABLE)
                {
                    varNode = (VariableNode)p;
                }

                string identifier = varNode.GetName();
                if (!symbolTable.IsVariableSymbolInTable(identifier))
                {
                    string notDcl = $"Variable {identifier} does not exist in this scope!";
                    ReportError(p.GetRow(), p.GetCol(), errors, notDcl);
                    continue;
                }

                INode nodeType = varNode.GetVariableType();
                string type = EvaluateTypeOfTypeNode(nodeType, errors, symbolTable);

                if (type != null && type == STR_BOOLEAN)
                {
                    string wrongtype = "Argument type cannot be boolean!";
                    ReportError(p.GetRow(), p.GetCol(), errors, wrongtype);
                    continue;
                }

            }

            return null;
        }

        /// <summary>
        /// Static method <c>CheckWritelnCall</c> checks that the parameters are correct type and declared.
        /// </summary>
        /// <param name="node">write call node</param>
        /// <param name="errors">list of detected errors</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>null</returns>
        public static string CheckWritelnCall(INode node, List<string> errors, SymbolTable symbolTable)
        {
            if (node == null) return null;

            CallNode callNode = (CallNode)node;

            // check that all paramateres are correct type and declared in scope
            foreach (INode p in callNode.GetArguments())
            {
                switch (p.GetNodeType())
                {
                    case NodeType.VARIABLE:
                        string varType = EvaluateTypeOfVariableNode(p, errors, symbolTable);
                        if (varType != null && varType.Equals(STR_BOOLEAN))
                        {
                            string errorVar = "Argument must be numeric or string!";
                            ReportError(p.GetRow(), p.GetCol(), errors, errorVar);
                        }
                        break;
                    case NodeType.STRING:
                    case NodeType.INTEGER:
                    case NodeType.REAL:
                        break;
                    case NodeType.BINARY_EXPRESSION:
                        string binaryType = EvaluateTypeOfBinaryExpressionNode(p, errors, symbolTable);
                        if (binaryType != null && binaryType.Equals(STR_BOOLEAN))
                        {
                            string errorBin = "Argument must be numeric or string!";
                            ReportError(p.GetRow(), p.GetCol(), errors, errorBin);
                        }
                        break;
                    case NodeType.CALL:
                        CallNode c = (CallNode)p;
                        string rettype = EvaluateTypeOfCallNode(c, errors, symbolTable);
                        if (rettype != null && rettype.Equals(STR_BOOLEAN))
                        {
                            string errorCall = "Argument must be numeric or string!";
                            ReportError(p.GetRow(), p.GetCol(), errors, errorCall);
                        }
                        break;
                    case NodeType.UNARY_EXPRESSION:
                        string typeUnary = EvaluateTypeOfUnaryExpressionNode(p, errors, symbolTable);
                        if (typeUnary != null && !typeUnary.Equals(STR_INTEGER))
                        {
                            string errorUnary = "Argument must be numeric or string!";
                            ReportError(p.GetRow(), p.GetCol(), errors, errorUnary);
                        }
                        break;
                    default:
                        string errorDefault = "Argument must be numeric or string!";
                        ReportError(p.GetRow(), p.GetCol(), errors, errorDefault);
                        break;
                }

            }

            return null;
        }

        /// <summary>
        /// Static method <c>CheckProcedureFunctionCall</c> checks the arguments of
        /// the function/procedure that is called.
        /// </summary>
        /// <param name="node">function call node</param>
        /// <param name="functionInTable">is function in table</param>
        /// <param name="procedureInTable">is procedure in table</param>
        /// <param name="identifier">function/procedure name</param>
        /// <param name="parameters">parameters</param>
        /// <param name="errors">list of detected erros</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>function return type</returns>
        public static string CheckProcedureFunctionCall(INode node, bool functionInTable, bool procedureInTable, string identifier, string[] parameters, List<string> errors, SymbolTable symbolTable)
        {
            if (functionInTable)
            {
                FunctionSymbol f = symbolTable.GetFunctionSymbolByIdentifierAndArguments(identifier, parameters);
                return f.GetReturnType();
            }
            else if (procedureInTable)
            {
                return null;
            }
            else
            {
                string errorMsg = "Procedure/Function arguments are invalid!";
                ReportError(node.GetRow(), node.GetCol(), errors, errorMsg);

                FunctionSymbol f = symbolTable.GetMostSimilarFunctionSymbol(identifier, parameters);
                return f.GetReturnType();
            }

        }

        /// <summary>
        /// Method <c>EvaluateTypeOfNode</c> evaluates the type of given node.
        /// </summary>
        /// <param name="node">node to be evaluated</param>
        /// <param name="symbolTable">symbol table</param>
        /// <returns>type</returns>
        public static string EvaluateTypeOfNode(INode node, SymbolTable symbolTable)
        {
            List<string> errors = new List<string>();
            string evaluatedType = null;

            switch (node.GetNodeType())
            {
                case NodeType.BINARY_EXPRESSION:
                    evaluatedType = EvaluateTypeOfBinaryExpressionNode(node, errors, symbolTable);
                    break;
                case NodeType.CALL:
                    evaluatedType = EvaluateTypeOfCallNode(node, errors, symbolTable);
                    break;
                case NodeType.VARIABLE:
                    evaluatedType = EvaluateTypeOfVariableNode(node, errors, symbolTable);
                    break;
                case NodeType.STRING:
                    return STR_STRING;
                case NodeType.INTEGER:
                    return STR_INTEGER;
                case NodeType.REAL:
                    return STR_REAL;
                case NodeType.UNARY_EXPRESSION:
                    return EvaluateTypeOfUnaryExpressionNode(node, errors, symbolTable);
                default:
                    throw new Exception($"Unexpected error... nodetype {node.GetNodeType()} not supported!");
            }

            return evaluatedType;
        }
    }
}
