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

        private void GenerateCodeForBinaryExpression(string tmpName, string lhsTmp, string rhsTmp, Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // variable to hold generated code line
            string codeLine = "";

            // evaluate type of left hand side
            string typeLhs = SemanticAnalyzer.EvaluateTypeOfNode(lhs, visitor.GetSymbolTable());
            // evaluate type of right hand side
            string typeRhs = SemanticAnalyzer.EvaluateTypeOfNode(rhs, visitor.GetSymbolTable());

            // check if tmp variables are pointers
            bool lhsIsPointer = symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer();
            bool rhsIsPointer = symTable.GetVariableSymbolByIdentifier(rhsTmp).IsPointer();

            string numericLhsPrefix = lhsIsPointer ? "*" : "";
            string numericRhsPrefix = rhsIsPointer ? "*" : "";

            // variable symbol that holds then new tmp variable
            VariableSymbol varSymbol = null;

            // check the binary operation and generate code line for it
            switch (value)
            {
                case "-":
                case "*":
                case "/":
                    string type = "int";
                    if (typeLhs.Equals("real") || typeRhs.Equals("real")) type = "double";
                    codeLine = $"{type} {tmpName} = {numericLhsPrefix}{lhsTmp} {value} {numericRhsPrefix}{rhsTmp};";
                    if (type == "double")
                    {
                        varSymbol = new VariableSymbol(tmpName, "real", null, symTable.GetCurrentScope());
                    }
                    else
                    {
                        varSymbol = new VariableSymbol(tmpName, "integer", null, symTable.GetCurrentScope());
                    }
                    break;
                case "+":
                    if(typeLhs.Equals("real") || typeLhs.Equals("integer"))
                    {
                        type = "int";
                        if (typeLhs.Equals("real") || typeRhs.Equals("real")) type = "double";
                        codeLine = $"{type} {tmpName} = {numericLhsPrefix}{lhsTmp} {value} {numericRhsPrefix}{rhsTmp};";
                        if (type == "double")
                        {
                            varSymbol = new VariableSymbol(tmpName, "real", null, symTable.GetCurrentScope());
                        }
                        else
                        {
                            varSymbol = new VariableSymbol(tmpName, "integer", null, symTable.GetCurrentScope());
                        }
                    }
                    else if (typeLhs.Equals("string"))
                    {

                    }
                    break;
                case "and":
                case "or":
                    // boolean
                    break;
                case "%":
                    // integer
                    break;
                case "=":
                    if (typeLhs.Equals("boolean") || typeLhs.Equals("integer") || typeRhs.Equals("real"))
                    {
                        codeLine = $"bool {tmpName} = {numericLhsPrefix}{lhsTmp} == {numericRhsPrefix}{rhsTmp};";
                        varSymbol = new VariableSymbol(tmpName, "boolean", null, symTable.GetCurrentScope());
                    }
                    // integer, real, boolean, string, arrays
                    break;
                case "<>":
                    if(typeLhs.Equals("boolean") ||typeLhs.Equals("integer") || typeRhs.Equals("real"))
                    {
                        codeLine = $"bool {tmpName} = {numericLhsPrefix}{lhsTmp} != {numericRhsPrefix}{rhsTmp};";
                        varSymbol = new VariableSymbol(tmpName, "boolean", null, symTable.GetCurrentScope());
                    }
                    // integer, real, boolean, string, arrays
                    break;
                case "<":
                    if (typeLhs.Equals("boolean") || typeLhs.Equals("integer") || typeRhs.Equals("real"))
                    {
                        codeLine = $"bool {tmpName} = {numericLhsPrefix}{lhsTmp} < {numericRhsPrefix}{rhsTmp};";
                        varSymbol = new VariableSymbol(tmpName, "boolean", null, symTable.GetCurrentScope());
                    }
                    // integer, real, boolean, string, arrays
                    break;
                case "<=":
                    if (typeLhs.Equals("boolean") || typeLhs.Equals("integer") || typeRhs.Equals("real"))
                    {
                        codeLine = $"bool {tmpName} = {numericLhsPrefix}{lhsTmp} <= {numericRhsPrefix}{rhsTmp};";
                        varSymbol = new VariableSymbol(tmpName, "boolean", null, symTable.GetCurrentScope());
                    }
                    // integer, real, boolean, string, arrays
                    break;
                case ">":
                    if (typeLhs.Equals("boolean") || typeLhs.Equals("integer") || typeRhs.Equals("real"))
                    {
                        codeLine = $"bool {tmpName} = {numericLhsPrefix}{lhsTmp} > {numericRhsPrefix}{rhsTmp};";
                        varSymbol = new VariableSymbol(tmpName, "boolean", null, symTable.GetCurrentScope());
                    }
                    // integer, real, boolean, string, arrays
                    break;
                case ">=":
                    break;
                case "[]":

                    string simpleType = GetSimpleTypeFromArrayType(typeLhs);
                    string cType = Converter.ConvertSimpleTypeToTargetLanguage(simpleType);

                    string prefix = "&";
                    // if array is pointer --> no prefix
                    if (symTable.GetVariableSymbolByIdentifier(lhsTmp).IsPointer()) prefix = "";

                    codeLine = $"{cType} *{tmpName} = ({cType} *) {prefix}{lhsTmp} + {rhsTmp};";
                    varSymbol = new VariableSymbol(tmpName, cType, null, symTable.GetCurrentScope(), true);
                    
                    /*
                    lhs.GenerateCode(visitor);
                    string type = SemanticAnalyzer.EvaluateTypeOfNode(lhs, visitor.GetSymbolTable());
                    string simpleType = GetSimpleTypeFromArrayType(type);
                    string cType = CodeGenerator.ConvertSimpleTypeToTargetLanguage(simpleType);
                    lhsTmp = visitor.GetLatestUsedTmpVariable();
                    rhs.GenerateCode(visitor);
                    rhsTmp = visitor.GetLatestUsedTmpVariable();
                    counter = visitor.GetTempVariableCounter();
                    visitor.IncreaseTmpVariableCounter();
                    newTmpVariable = "tmp_" + counter;
                    visitor.SetLatestTmpVariableName(newTmpVariable);
                    line = $"{cType} {newTmpVariable} = {lhsTmp}[{rhsTmp}];";
                    */

                    break;
                default:
                    throw new Exception("Unexpected exception... Invalid binary operation!");
            }

            // declare new variable symbol
            symTable.DeclareVariableSymbol(varSymbol);

            // add generated code line to list of code lines
            visitor.AddCodeLine(codeLine);
        }

        /// <summary>
        /// Method <c>GetSimpleTypeFromArrayType</c> extracts simple type from array type.
        /// </summary>
        private string GetSimpleTypeFromArrayType(string type)
        {
            if (type.Contains("integer")) return "integer";
            if (type.Contains("string")) return "string";
            if (type.Contains("boolean")) return "boolean";
            if (type.Contains("real")) return "real";
            throw new Exception("Unexpected exception... Invalid array type!");
        }
    }
}
