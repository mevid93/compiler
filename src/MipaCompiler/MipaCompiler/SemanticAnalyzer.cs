
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
        private readonly Node.ISymbol ast;              // AST representation of the source code
        private bool errorsDetected;                    // flag telling about status of semantic analysis
        private readonly List<string> errors;           // list of all detected errors
        private readonly SymbolTable symbolTable;       // symbol table to store variables

        private string returnStmntType;     // type that should be returned in return statement

        /// <summary>
        /// Constructor <c>SemanticAnalyzer</c> creates new SemanticAnalyzer-object.
        /// </summary>
        /// <param name="ast"></param>
        public SemanticAnalyzer(Node.ISymbol ast)
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


        ///////////////////////////////// ACTUAL SEMANTIC CHECKS /////////////////////////////////


        /// <summary>
        /// Method <c>CheckProgram</c> checks the semantic constraints of program.
        /// </summary>
        private void CheckProgram()
        {
            // convert ast root node to program node
            ProgramNode prog = (ProgramNode)ast;

            // do semantic analysis for the functions
            foreach (Node.ISymbol f in prog.GetFunctions())
            {
                CheckFunction(f);
            }

            // from here on --> no values should be returned in return statements
            // only functions should return values
            returnStmntType = "";

            // do semantic analysis for the procedures
            foreach (Node.ISymbol p in prog.GetProcedures())
            {
                CheckProcedure(p);
            }

            // do semantic analysis for the main block,
            // can be null if parsing failed
            Node.ISymbol block = prog.GetMainBlock();
            if (block != null) CheckBlock(block);

        }

        /// <summary>
        /// Method <c>CheckFunction</c> checks semantic constraints for function.
        /// </summary>
        private void CheckFunction(Node.ISymbol node)
        {
            if (node == null) return;

            // convert node to function node
            FunctionNode func = (FunctionNode)node;

            // get function name
            string functionName = func.GetName();

            // for each function parameter --> find parameter type
            // should be integer, string, boolean or array
            List<Node.ISymbol> parameters = func.GetParameters();
            string[] paramTypes = new string[parameters.Count];

            for(int i = 0; i < parameters.Count; i++)
            {
                paramTypes[i] = EvaluateType(parameters[i]);
            }
            
            // find return type
            string returnType = EvaluateType(func.GetReturnType());
            returnStmntType = returnType;

            // create new function symbol
            FunctionSymbol symbol = new FunctionSymbol(functionName, paramTypes, returnType);

            // check that function symbol does not exist in the symbol table already
            // if it does exist --> report error
            if (symbolTable.IsFunctionSymbolInTable(symbol))
            {
                string functionStr = $"{functionName}({string.Join(", ", paramTypes)})";
                string errorMsg = $"SemanticError::Row {func.GetRow()}::Column {func.GetCol()}::Procedure/Function {functionStr} already defined!";
                Console.WriteLine(errorMsg);
                errors.Add(errorMsg);
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
        private void CheckProcedure(Node.ISymbol node)
        {
            if (node == null) return;

            // convert node to procedure node
            ProcedureNode proc = (ProcedureNode)node;

            // get procedure name
            string procedureName = proc.GetName();

            // for each function parameter --> find parameter type
            // should be integer, string, boolean or array
            List<Node.ISymbol> parameters = proc.GetParameters();
            string[] paramTypes = new string[parameters.Count];

            for (int i = 0; i < parameters.Count; i++)
            {
                paramTypes[i] = EvaluateType(parameters[i]);
            }

            // create new function symbol
            ProcedureSymbol symbol = new ProcedureSymbol(procedureName, paramTypes);

            // check that porcedure symbol does not exist in the symbol table already
            // if it does exist --> report error
            if (symbolTable.IsProcedureSymbolInTable(symbol))
            {
                string procedureStr = $"{procedureName}({string.Join(", ", paramTypes)})";
                string errorMsg = $"SemanticError::Row {proc.GetRow()}::Column {proc.GetCol()}::Procedure/Function {procedureStr} already defined!";
                Console.WriteLine(errorMsg);
                errors.Add(errorMsg);
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
        private void CheckBlock(Node.ISymbol node)
        {
            if (node == null) return;

            // convert node to block node
            BlockNode block = (BlockNode)node;

            // TODO
            
        }

        /// <summary>
        /// Method <c>EvaluateType</c> returns the parameter type
        /// of given input node. Input node should be variable node
        /// </summary>
        private string EvaluateType(Node.ISymbol node)
        {
            if (node.GetNodeType() != NodeType.VARIABLE) return null;

            VariableNode varNode = (VariableNode)node;
            Node.ISymbol type = varNode.GetVariableType();

            if (type == null) return null;

            switch (type.GetNodeType())
            {
                case NodeType.ARRAY_TYPE:
                    ArrayTypeNode at = (ArrayTypeNode)type;
                    string tmp = $"array[] of ";
                    SimpleTypeNode stn = (SimpleTypeNode)at.GetSimpleType();
                    tmp += stn.GetTypeValue();
                    return tmp;
                case NodeType.SIMPLE_TYPE:
                    stn = (SimpleTypeNode)type;
                    return stn.GetTypeValue();
                default:
                    return null;
            }
        }

    }
}
