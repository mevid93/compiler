﻿
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

            // do semantic analysis for the functions
            foreach (INode f in prog.GetFunctions())
            {
                CheckFunction(f);
            }

            // from here on --> no values should be returned in return statements
            // only functions should return values
            returnStmntType = "";

            // do semantic analysis for the procedures
            foreach (INode p in prog.GetProcedures())
            {
                CheckProcedure(p);
            }

            // do semantic analysis for the main block,
            // can be null if parsing failed
            INode block = prog.GetMainBlock();
            CheckBlock(block);

        }

        /// <summary>
        /// Method <c>CheckFunction</c> checks semantic constraints for function.
        /// </summary>
        private void CheckFunction(INode node)
        {
            if (node == null) return;

            // convert node to function node
            FunctionNode func = (FunctionNode)node;

            // get function name
            string functionName = func.GetName();

            // for each function parameter --> find parameter type
            // should be integer, string, boolean or array
            List<Node.INode> parameters = func.GetParameters();
            string[] paramTypes = new string[parameters.Count];

            for(int i = 0; i < parameters.Count; i++)
            {
                VariableNode varNode = (VariableNode)parameters[i];
                paramTypes[i] = EvaluateTypeOfTypeNode(varNode.GetVariableType());
            }
            
            // find return type
            string returnType = EvaluateTypeOfTypeNode(func.GetReturnType());
            returnStmntType = returnType;

            // create new function symbol
            FunctionSymbol symbol = new FunctionSymbol(functionName, paramTypes, returnType);

            // check that function symbol does not exist in the symbol table already
            // if it does exist --> report error
            if (symbolTable.IsFunctionSymbolInTable(symbol))
            {
                string functionStr = $"{functionName}({string.Join(", ", paramTypes)})";
                string errorMsg = $"SemanticError::Row {func.GetRow()}::Column {func.GetCol()}::";
                errorMsg += $"Procedure/Function {functionStr} already defined!";
                ReportError(errorMsg);
                return; 
            }

            // no problems detected --> add function symbol to table
            symbolTable.DeclareFunctionSymbol(symbol);

            // check the function code
            CheckBlock(func.GetBlock());
        }

        /// <summary>
        /// Method <c>CheckProcedure</c> checks semantic constraints for procedure.
        /// </summary>
        private void CheckProcedure(INode node)
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
            }

            // create new function symbol
            ProcedureSymbol symbol = new ProcedureSymbol(procedureName, paramTypes);

            // check that porcedure symbol does not exist in the symbol table already
            // if it does exist --> report error
            if (symbolTable.IsProcedureSymbolInTable(symbol))
            {
                string procedureStr = $"{procedureName}({string.Join(", ", paramTypes)})";
                string errorMsg = $"SemanticError::Row {proc.GetRow()}::Column {proc.GetCol()}::";
                errorMsg += $"Procedure /Function {procedureStr} already defined!";
                ReportError(errorMsg);
                return;
            }

            // no problems detected --> add function symbol to table
            symbolTable.DeclareProcedureSymbol(symbol);

            // check the function code
            CheckBlock(proc.GetBlock());

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
                        CheckCall(stmnt);
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

            // remove scope
            symbolTable.RemoveScope();
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

            // TODO
        }

        private void CheckCall(INode node)
        {
            // TODO
        }

        private void CheckIfElse(INode node)
        {
            // TODO
        }

        private void CheckReturn(INode node)
        {
            // TODO
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
            string type = EvaluateTypeOfTypeNode(typeNode);

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

        private void CheckWhile(INode node)
        {
            // TODO
        }

        /// <summary>
        /// Method <c>EvaluateTypeOfTypeNode</c> returns the parameter type
        /// of given typenode. Inputnode should be either SimpleTypeNode or ArrayTypeNode.
        /// </summary>
        private string EvaluateTypeOfTypeNode(INode node)
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
                case NodeType.ARRAY_INDEX:
                    return EvaluateTypeOfArrayIndexNode(node);
                case NodeType.ARRAY_SIZE:
                    return EvaluateTypeOfArraySizeNode(node);
                case NodeType.VARIABLE:
                    return EvaluateTypeOfVariableNode(node);
                case NodeType.INTEGER:
                    return STR_INTEGER;
                case NodeType.STRING:
                    return STR_STRING;
                case NodeType.REAL:
                    return STR_REAL;
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
                if (left.Equals(type) && right.Equals(type))
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
            ArrayIndexNode arrayIndex = (ArrayIndexNode)node;

            // get expression that defines index of array
            INode expression = arrayIndex.GetIndex();

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
            VariableNode array = (VariableNode) arrayIndex.GetArray();

            // get array type node
            INode vartype = array.GetVariableType();
            string arrayType = EvaluateTypeOfTypeNode(vartype);

            return arrayType;
        }

        private string EvaluateTypeOfArraySizeNode(INode node)
        {
            // TODO
            return null;
        }

        private string EvaluateTypeOfVariableNode(INode node)
        {
            // can be array, simple types
            // if variable name is false or true
            // and there exists no variable with name false and true in current scope
            // then return type is boolean --> it is not actually a variable but a boolea value
            // it has not been possible to distinguish two of them before
            
            // TODO
            return null;
        }

    }
}
