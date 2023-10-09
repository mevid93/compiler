using MipaCompiler.BackEnd;
using MipaCompiler.Symbol;
using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>AssignmentNode</c> represents assingment operation in AST.
    /// </summary>
    public class AssignmentNode : INode
    {
        private readonly int row;               // row in source code
        private readonly int col;               // column in source code
        private readonly INode identifier;      // identifier that is assigned a value
        private readonly INode expression;      // expression for value

        /// <summary>
        /// Constructor <c>AssignmentNode</c> creates new AssignmentNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="identifier">identifier that is assigned value</param>
        /// <param name="expression">expression for value</param>
        public AssignmentNode(int row, int col, INode identifier, INode expression)
        {
            this.row = row;
            this.col = col;
            this.identifier = identifier;
            this.expression = expression;
        }

        /// <summary>
        /// Method <c>GetIdentifier</c> returns the target identifier of assignment operation.
        /// </summary>
        /// <returns>identifier</returns>
        public INode GetIdentifier()
        {
            return identifier;
        }

        /// <summary>
        /// Method <c>GetValueExpression</c> returns the expression for value.
        /// </summary>
        /// <returns>value</returns>
        public INode GetValueExpression()
        {
            return expression;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.ASSIGNMENT;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.ASSIGNMENT}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Identifier: {identifier}");
            Console.WriteLine($"Value expression:");
            if (expression != null) expression.PrettyPrint();
        }

        ////////////////// EVERYTHING AFTER THIS IS FOR CODE GENERATION /////////////////////

        public void GenerateCode(Visitor visitor)
        {
            switch (identifier.GetNodeType())
            {
                case NodeType.VARIABLE:
                    GenerateCodeForVariableAssignment(visitor);
                    break;
                case NodeType.BINARY_EXPRESSION:
                    GenerateCodeForArrayAssignment(visitor);
                    break;
                default:
                    throw new Exception("Unexpected error: Invalid target for assignment operation!");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForVariableAssignment</c> generates code for assignment operation
        /// where target variable is type of integer, real, string or boolean.
        /// </summary>
        private void GenerateCodeForVariableAssignment(Visitor visitor)
        {
            // evaluate expression
            expression.GenerateCode(visitor);

            // get latest temp variable
            string temp = visitor.GetLatestUsedTmpVariable();

            // get target variable name
            VariableNode varNode = (VariableNode)identifier;
            string identifierStr = varNode.GetName();
            
            // check if variable that is assigned a value is pointer
            string prefix = "";
            if (visitor.GetSymbolTable().GetVariableSymbolByIdentifier(identifierStr).IsPointer())
            {
                prefix = "*";
            }

            // check if assigned value is pointer
            string prefix2 = "";
            if (!temp.Equals("false") && !temp.Equals("true"))
            {
                if (visitor.GetSymbolTable().GetVariableSymbolByIdentifier(temp).IsPointer())
                {
                    prefix2 = "*";
                }
            }

            // check the type of variable
            VariableSymbol varSymbol = visitor.GetSymbolTable().GetVariableSymbolByIdentifier($"var_{identifierStr}");

            // string assignment is done with strcpy
            if (varSymbol.GetSymbolType().Equals("string"))
            {
                visitor.AddCodeLine($"strncpy(var_{identifierStr}, {temp}, 255);");
            }
            // if array is assigned to array
            else if (varSymbol.GetSymbolType().Contains("array"))
            {

                // check if size variables are pointers
                string sizeName = "size_" + temp.Replace("var_", "");
                VariableSymbol sizeSymbol = visitor.GetSymbolTable().GetVariableSymbolByIdentifier(sizeName);
                string sizePrefix1 = visitor.GetSymbolTable().GetVariableSymbolByIdentifier($"var_{identifierStr}").IsPointer() ? "*" : "";
                string sizePrefix2 = sizeSymbol.IsPointer() ? "*" : "";

                VariableSymbol tempVariable = visitor.GetSymbolTable().GetVariableSymbolByIdentifier(temp);
                
                Helper.FreeArraysBeforeArrayAssignment($"var_{identifierStr}", varSymbol.GetCurrentScope(), temp, tempVariable.GetCurrentScope(), visitor);

                visitor.AddCodeLine($"{sizePrefix1}size_{identifierStr} = {sizePrefix2}{sizeName};");
                visitor.AddCodeLine($"var_{identifierStr} = {temp};");

                
            }
            // numeric or boolean assignment
            else
            {
                visitor.AddCodeLine($"{prefix}var_{identifierStr} = {prefix2}{temp};");
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForArrayAssignment</c> generates code for assignment operation
        /// where target variable is type of array.
        /// </summary>
        private void GenerateCodeForArrayAssignment(Visitor visitor)
        {
            // evaluate expression
            expression.GenerateCode(visitor);

            // get latest temp variable
            string temp = visitor.GetLatestUsedTmpVariable();

            // get name of the variable that is assigned a value
            BinaryExpressionNode bin = (BinaryExpressionNode)identifier;
            VariableNode varNode = (VariableNode)bin.GetLhs();
            string identifierStr = varNode.GetName();

            // get array index where value is assigned
            bin.GetRhs().GenerateCode(visitor);
            string index = visitor.GetLatestUsedTmpVariable();

            // check if assigned value is pointer
            string prefix2 = "";
            if (!temp.Equals("false") && !temp.Equals("true"))
            {
                if (visitor.GetSymbolTable().GetVariableSymbolByIdentifier(temp).IsPointer())
                {
                    prefix2 = "*";
                }
            }

            // check the type of variable
            VariableSymbol varSymbol = visitor.GetSymbolTable().GetVariableSymbolByIdentifier($"var_{identifierStr}");

            // check if variable is integer array
            if(varSymbol.GetSymbolType().Equals("array[] of integer"))
            {
                visitor.AddCodeLine($"var_{identifierStr}[{index}] = {prefix2}{temp};");
            }
            // check if variable is real array
            else if (varSymbol.GetSymbolType().Equals("array[] of real"))
            {
                visitor.AddCodeLine($"var_{identifierStr}[{index}] = {prefix2}{temp};");
            }
            // check if variable is boolean array
            else if (varSymbol.GetSymbolType().Equals("array[] of boolean"))
            {
                visitor.AddCodeLine($"var_{identifierStr}[{index}] = {prefix2}{temp};");
            }
            // check if variable is string array
            else if (varSymbol.GetSymbolType().Equals("array[] of string"))
            {
                visitor.AddCodeLine($"var_{identifierStr}[{index}] = {temp};");
            }
        }
    }
}
