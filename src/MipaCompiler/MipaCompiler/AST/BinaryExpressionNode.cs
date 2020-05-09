using MipaCompiler.BackEnd;
using MipaCompiler.Symbol;
using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>BinaryExpressionNode</c> represents binary expression in AST.
    /// </summary>
    public class BinaryExpressionNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly string value;      // value of operation
        private readonly INode lhs;         // left hand side of expression
        private readonly INode rhs;         // right hand side of expression

        /// <summary>
        /// Constructor <c>BinaryExpressionNode</c> creates new BinaryExpressionNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="operation">operator symbol</param>
        /// <param name="lhs">left hand side of operation</param>
        /// <param name="rhs">right hand side of operation</param>
        public BinaryExpressionNode(int row, int col, string operation, INode lhs, INode rhs)
        {
            this.row = row;
            this.col = col;
            value = operation;
            this.lhs = lhs;
            this.rhs = rhs;
        }

        /// <summary>
        /// Method <c>GetOperation</c> returns the binary operation symbol.
        /// </summary>
        /// <returns>binary operation</returns>
        public string GetOperation()
        {
            return value;
        }

        /// <summary>
        /// Method <c>GetLhs</c> returns the left hand side of operation.
        /// </summary>
        /// <returns>lhs</returns>
        public INode GetLhs()
        {
            return lhs;
        }

        /// <summary>
        /// Method <c>GetRhs</c> returns the right hand side of operation.
        /// </summary>
        /// <returns>rhs</returns>
        public INode GetRhs()
        {
            return rhs;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.BINARY_EXPRESSION;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.BINARY_EXPRESSION}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Operation: {value}");
            Console.WriteLine($"Left hand side:");
            if (lhs != null) lhs.PrettyPrint();
            Console.WriteLine($"Right hand side:");
            if (rhs != null) rhs.PrettyPrint();
        }

        ////////////////// EVERYTHING AFTER THIS IS FOR CODE GENERATION /////////////////////

        public void GenerateCode(Visitor visitor)
        {
            // generate code for left hand side
            lhs.GenerateCode(visitor);
            // retrieve name of the latest tmp variable
            string lhsTmp = visitor.GetLatestUsedTmpVariable();

            // generate code for right hand side
            rhs.GenerateCode(visitor);
            // retrieve name of the latest tmp variable
            string rhsTmp = visitor.GetLatestUsedTmpVariable();

            // get number for new latest tmp variable
            int number = visitor.GetTmpVariableCounter();
            visitor.IncreaseTmpVariableCounter();

            // define the name of the new tmp variable
            string tmpName = $"tmp_{number}";

            // update information about latest used tmp variable to visitor
            visitor.SetLatestTmpVariableName(tmpName);

            // generate the code line
            GenerateCodeForBinaryExpression(tmpName, lhsTmp, rhsTmp, visitor);
        }

        /// <summary>
        /// Method <c>GenerateCodeForBinaryExpression</c> generates the code line for binary operation.
        /// </summary>
        private void GenerateCodeForBinaryExpression(string tmpName, string lhsTmp, string rhsTmp, Visitor visitor)
        {

            // evaluate type of left hand side
            string typeLhs = SemanticAnalyzer.EvaluateTypeOfNode(lhs, visitor.GetSymbolTable());
            // evaluate type of right hand side
            string typeRhs = SemanticAnalyzer.EvaluateTypeOfNode(rhs, visitor.GetSymbolTable());

            // check the binary operation and generate code line for it
            switch (value)
            {
                case "-":
                    GenerateCodeForSubstractionOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "*":
                    GenerateCodeForMultiplicationOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "/":
                    GenerateCodeForDivisionOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "+":
                    GenerateCodeForAdditionOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "%":
                    GenerateCodeForModuloOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "and":
                    GenerateCodeForLogicalAndOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "or":
                    GenerateCodeForLogicalOrOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "=":
                    GenerateCodeForEqualOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "<>":
                    GenerateCodeForNotEqualOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "<":
                    GenerateCodeForLessThanOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "<=":
                    GenerateCodeForLessThanOrEqualOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case ">":
                    GenerateCodeForLargerThanOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case ">=":
                    GenerateCodeForLargerThanOrEqualOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                case "[]":
                    GenerateCodeForArrayIndexOperation(tmpName, lhsTmp, rhsTmp, typeLhs, typeRhs, visitor);
                    break;
                default:
                    throw new Exception("Unexpected exception: Invalid binary operation!");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForSubstractionOperation</c> generates code for substraction operation.
        /// </summary>
        private void GenerateCodeForSubstractionOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "double";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "real", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} - {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "int";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "integer", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} - {prefixRhs}{rhsTmp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForMultiplicationOperation</c> generates code for multiplication operation.
        /// </summary>
        private void GenerateCodeForMultiplicationOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "double";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "real", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} * {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "int";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "integer", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} * {prefixRhs}{rhsTmp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForDivisionOperation</c> generates code for division operation.
        /// </summary>
        private void GenerateCodeForDivisionOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "double";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "real", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} / {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "int";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "integer", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} / {prefixRhs}{rhsTmp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForAdditionOperation</c> generates code for addition operation.
        /// </summary>
        private void GenerateCodeForAdditionOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "double";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "real", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} + {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "int";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "integer", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} + {prefixRhs}{rhsTmp};");
            }
            // string case
            else if(lhsType.Equals("string") || rhsType.Equals("string"))
            {
                string type = "char *";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "string", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNeeded("string", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNeeded("string", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = malloc(256 * sizeof(char));");
                visitor.AddCodeLine($"strcpy({tmpName}, {prefixLhs}{lhsTmp});");
                visitor.AddCodeLine($"strcat({tmpName}, {prefixRhs}{rhsTmp});");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForModuloOperation</c> generates code for modulo operation.
        /// </summary>
        private void GenerateCodeForModuloOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            string type = "int";
            symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "integer", null, scope));
            string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
            string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
            visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} % {prefixRhs}{rhsTmp};");
        }

        /// <summary>
        /// Method <c>GenerateCodeForLogicalAndOperation</c> generates code for logical AND operation.
        /// </summary>
        private void GenerateCodeForLogicalAndOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            string type = "bool";
            symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
            string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", lhsIsPointer);
            string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", rhsIsPointer);
            visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} && {prefixRhs}{rhsTmp};");
        }

        /// <summary>
        /// Method <c>GenerateCodeForLogicalOrOperation</c> generates code for logical OR operation.
        /// </summary>
        private void GenerateCodeForLogicalOrOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            string type = "bool";
            symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
            string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", lhsIsPointer);
            string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", rhsIsPointer);
            visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} || {prefixRhs}{rhsTmp};");
        }

        /// <summary>
        /// Method <c>GenerateCodeForEqualOperation</c> generates code for equal operation.
        /// </summary>
        private void GenerateCodeForEqualOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} == {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} == {prefixRhs}{rhsTmp};");
            }
            // string case
            else if (lhsType.Equals("string") || rhsType.Equals("string"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNeeded("string", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNeeded("string", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = 0 == strcmp({prefixLhs}{lhsTmp}, {prefixRhs}{rhsTmp});");
            }
            // boolean case
            else if (lhsType.Equals("boolean") || rhsType.Equals("boolean"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} == {prefixRhs}{rhsTmp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForNotEqualOperation</c> generates code for not equal operation.
        /// </summary>
        private void GenerateCodeForNotEqualOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} != {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} != {prefixRhs}{rhsTmp};");
            }
            // string case
            else if (lhsType.Equals("string") || rhsType.Equals("string"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNeeded("string", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNeeded("string", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = 0 != strcmp({prefixLhs}{lhsTmp}, {prefixRhs}{rhsTmp});");
            }
            // boolean case
            else if (lhsType.Equals("boolean") || rhsType.Equals("boolean"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} != {prefixRhs}{rhsTmp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForLessThanOperation</c> generates code for less than operation.
        /// </summary>
        private void GenerateCodeForLessThanOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} < {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} < {prefixRhs}{rhsTmp};");
            }
            // string case
            else if (lhsType.Equals("string") || rhsType.Equals("string"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNeeded("string", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNeeded("string", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = 0 > strcmp({prefixLhs}{lhsTmp}, {prefixRhs}{rhsTmp});");
            }
            // boolean case
            else if (lhsType.Equals("boolean") || rhsType.Equals("boolean"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} < {prefixRhs}{rhsTmp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForLessThanOrEqualOperation</c> generates code for less than or equal operation.
        /// </summary>
        private void GenerateCodeForLessThanOrEqualOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} <= {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} <= {prefixRhs}{rhsTmp};");
            }
            // string case
            else if (lhsType.Equals("string") || rhsType.Equals("string"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNeeded("string", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNeeded("string", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = 0 >= strcmp({prefixLhs}{lhsTmp}, {prefixRhs}{rhsTmp});");
            }
            // boolean case
            else if (lhsType.Equals("boolean") || rhsType.Equals("boolean"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} <= {prefixRhs}{rhsTmp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForLargerThanOperation</c> generates code for larger than operation.
        /// </summary>
        private void GenerateCodeForLargerThanOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} > {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} > {prefixRhs}{rhsTmp};");
            }
            // string case
            else if (lhsType.Equals("string") || rhsType.Equals("string"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNeeded("string", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNeeded("string", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = 0 < strcmp({prefixLhs}{lhsTmp}, {prefixRhs}{rhsTmp});");
            }
            // boolean case
            else if (lhsType.Equals("boolean") || rhsType.Equals("boolean"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} > {prefixRhs}{rhsTmp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForLargerThanOrEqualOperation</c> generates code for larget than or 
        /// equal operation.
        /// </summary>
        private void GenerateCodeForLargerThanOrEqualOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = visitor.GetSymbolTable().GetCurrentScope();
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            // real case
            if (lhsType.Equals("real") || rhsType.Equals("real"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("real", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("real", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} >= {prefixRhs}{rhsTmp};");
            }
            // integer case
            else if (lhsType.Equals("integer") || rhsType.Equals("integer"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("integer", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("integer", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} >= {prefixRhs}{rhsTmp};");
            }
            // string case
            else if (lhsType.Equals("string") || rhsType.Equals("string"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNeeded("string", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNeeded("string", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = 0 <= strcmp({prefixLhs}{lhsTmp}, {prefixRhs}{rhsTmp});");
            }
            // boolean case
            else if (lhsType.Equals("boolean") || rhsType.Equals("boolean"))
            {
                string type = "bool";
                symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "boolean", null, scope));
                string prefixLhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", lhsIsPointer);
                string prefixRhs = Helper.GetPrefixWhenPointerNotNeeded("boolean", rhsIsPointer);
                visitor.AddCodeLine($"{type} {tmpName} = {prefixLhs}{lhsTmp} >= {prefixRhs}{rhsTmp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForArrayIndexOperation</c> generates code for array index operation.
        /// </summary>
        private void GenerateCodeForArrayIndexOperation(string tmpName, string lhsTmp, string rhsTmp, string lhsType, string rhsType, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            // get array type
            VariableSymbol variableSymbol = symTable.GetVariableSymbolByIdentifier(lhsTmp);
            string type = variableSymbol.GetSymbolType();
            string cType = Helper.GetElementTypeFromArrayTypeInC(type);

            string line = $"{cType} * {tmpName} = &{lhsTmp}[{rhsTmp}];";
            visitor.AddCodeLine(line);

            symTable.DeclareVariableSymbol(new VariableSymbol(tmpName, "integer", null, symTable.GetCurrentScope(), true));
        }
    }
}
