using MipaCompiler.BackEnd;
using MipaCompiler.Symbol;
using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// class <c>CallNode</c> represents function, procedure or
    /// predefined function (read, writeln) call in AST.
    /// </summary>
    public class CallNode : INode
    {
        private readonly int row;           // row in source code
        private readonly int col;           // column in source code
        private readonly string id;         // identifier for fucntion or procedure
        private readonly List<INode> args;  // parameters for function or procedure

        /// <summary>
        /// Constructor <c>CallNode</c> creates new CallNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="id">identifier of function or procedure</param>
        /// <param name="args">arguments</param>
        public CallNode(int row, int col, string id, List<INode> args)
        {
            this.row = row;
            this.col = col;
            this.id = id;
            this.args = args;
        }

        /// <summary>
        /// Method <c>GetId</c> returns identifier of function or procedure
        /// that is called.
        /// </summary>
        /// <returns>identifier</returns>
        public string GetId()
        {
            return id;
        }

        /// <summary>
        /// Method <c>GetArguments</c> returns the parameters of the call operation.
        /// </summary>
        /// <returns>arguments</returns>
        public List<INode> GetArguments()
        {
            return args;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.CALL;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.CALL}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Id: {id}");
            Console.WriteLine("Arguments:");
            foreach(INode arg in args)
            {
                arg.PrettyPrint();
            }
        }

        ////////////////// EVERYTHING AFTER THIS IS FOR CODE GENERATION /////////////////////

        public void GenerateCode(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            // check if predfined read procedure
            if (id.Equals("read") && !symTable.IsFunctionInTable(id) && !symTable.IsProcedureInTable(id))
            {
                GenerateCodeForScanf(visitor);
                return;
            }

            // check if predefined writeln procedure
            if (id.Equals("writeln") && !symTable.IsFunctionInTable(id) && !symTable.IsProcedureInTable(id))
            {
                GenerateCodeForPrintf(visitor);
                return;
            }

            // custom procedure or function
            GenerateCodeForCustomFunction(visitor);
        }
        
        /// <summary>
        /// Method <c>GenerateCodeForScanf</c> genereates the code
        /// for predefined read method. In other words, it generates
        /// scanf-statement in C-language.
        /// </summary>
        private void GenerateCodeForScanf(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            
            // scanf-function takes format string as first parameter and
            // pointer arguments as the following arguments.
            // let's define two variables, where one is for format string
            // and the other one is for pointer arguments
            string formatStr = "";
            string pointerArgs = ", ";

            // go through all call arguments and add them to
            // the two string variables defined earlier
            for(int i = 0; i < args.Count; i++)
            {
                // arguments must be separated in scanf-function
                if(i > 0)
                {
                    formatStr += " ";
                    pointerArgs += ", ";
                }

                // get argument type
                string variableType = SemanticAnalyzer.EvaluateTypeOfNode(args[i], symTable);

                // add correct formatting to the format string
                formatStr += GetFormattingForType(variableType);

                // add correct pointer argument to the argument string
                pointerArgs += GetPointerArgumentForScanf(args[i], visitor);
            }

            // create new code line to list of generated code lines
            visitor.AddCodeLine($"scanf(\"{formatStr}\"{pointerArgs});");
        }

        /// <summary>
        /// Method <c>GetFormattingForType</c> returns correct formatting
        /// for given type.
        /// </summary>
        private string GetFormattingForType(string type)
        {
            switch (type)
            {
                case "integer":
                    return "%d";
                case "real":
                    return "%lf";
                case "string":
                    return "%s";
                default:
                    throw new Exception($"Unexpected error: Unsupported variable type!");
            }
        }

        /// <summary>
        /// Method <c>GetPointerArgumentForScanf</c> will return correct argument
        /// for scanf corresponding the given node. Node should be variable
        /// or binary expression (array index).
        /// </summary>
        private string GetPointerArgumentForScanf(INode node, Visitor visitor)
        {
            // check node type
            switch (node.GetNodeType())
            {
                case NodeType.VARIABLE:
                    VariableNode varNode = (VariableNode)node;
                    return GetPointerArgumentFromVariableToScanf(varNode, visitor);
                case NodeType.BINARY_EXPRESSION:
                    BinaryExpressionNode bin = (BinaryExpressionNode)node;
                    return GetPointerArgumentFromArrayIndexToScanf(bin, visitor);
                default:
                    throw new Exception("Unexpected error: Invalid scanf argument type!");
            }
        }

        /// <summary>
        /// Method <c>GetPointerArgumentFromVariableToScanf</c> returns scanf argument
        /// for given variable node.
        /// </summary>
        private string GetPointerArgumentFromVariableToScanf(VariableNode varNode, Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // get argument name
            string varName = varNode.GetName();

            // get variable type
            string varType = SemanticAnalyzer.EvaluateTypeOfVariableNode(varNode, new List<string>(), symTable);

            // check if argument is pointer
            varName = $"var_{varName}";
            VariableSymbol varSymbol = symTable.GetVariableSymbolByIdentifier(varName);
            bool isPointer = varSymbol.IsPointer();

            string prefix = Helper.GetPrefixWhenPointerNeeded(varType, isPointer);
            return $"{prefix}{varName}";
        }

        /// <summary>
        /// Method <c>GetPointerArgumentFromArrayIndexToScanf</c> returns scanf argument
        /// for given binary expression node. BinaryExpression node at scan mean array index access.
        /// </summary>
        private string GetPointerArgumentFromArrayIndexToScanf(BinaryExpressionNode bin, Visitor visitor)
        {
            // get variable name
            VariableNode lhs = (VariableNode)bin.GetLhs();
            string varName = $"var_{lhs.GetName()}";

            // get index variable
            INode rhs = bin.GetRhs();
            rhs.GenerateCode(visitor);
            string tmp = visitor.GetLatestUsedTmpVariable();

            // return argument
            return $"&var_{lhs.GetName()}[{tmp}]";
        }

        /// <summary>
        /// Method <c>GenerateCodeForPrintf</c> generates the code for predefined
        /// writeln.
        /// </summary>
        private void GenerateCodeForPrintf(Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            string format_str = "";
            string print_args = ", ";

            for (int i = 0; i < args.Count; i++)
            {
                // arguments must be separated
                if (i > 0)
                {
                    print_args += ", ";
                }

                // get variable type
                string variableType = SemanticAnalyzer.EvaluateTypeOfNode(args[i], symTable);

                // add correct formatting 
                switch (variableType)
                {
                    case "integer":
                        format_str += "%d";
                        break;
                    case "real":
                        format_str += "%lf";
                        break;
                    case "string":
                        format_str += "%s";
                        break;
                    default:
                        throw new Exception("Unexpected error... unsupported variable type!");
                }

                // get the correct scanf argument
                string argument = GetPrintfArgument(args[i], visitor);
                print_args += argument;
            }
            
            // add printf to list of generated code lines
            string line = $"printf(\"{format_str}\\n\"{print_args});";
            visitor.AddCodeLine(line);
        }

        /// <summary>
        /// Method <c>GetPrintfArgument</c> will return correct argument
        /// for printf corresponding the given node. Node should evaluate
        /// as numeric or string.
        /// </summary>
        private string GetPrintfArgument(INode node, Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // generate code
            node.GenerateCode(visitor);

            // generated code expression result should
            // be accessible at the last tmp variable
            string lastTemp = visitor.GetLatestUsedTmpVariable();

            // check if type of last tmp variable is string
            VariableSymbol varSymbol = symTable.GetVariableSymbolByIdentifier(lastTemp);
            if (varSymbol.GetSymbolType().Equals("string")) return lastTemp;

            // check if last tmp variable is pointer
            bool isPointer = symTable.GetVariableSymbolByIdentifier(lastTemp).IsPointer();
            if (isPointer) lastTemp = $"*{lastTemp}";
            return lastTemp;
        }

        /// <summary>
        /// Method <c>GenerateCodeForCustomFunction</c> generates the code for custom
        /// function.
        /// </summary>
        private void GenerateCodeForCustomFunction(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();
            int scope = symTable.GetCurrentScope();

            // get arguments code that is passed to function or procedure call
            string arguments = GetArgumentsCode(visitor);

            // get function return type (if procedure, then "")
            string returnType = GetCallReturnType(symTable);
            
            // add code line to list of code lines
            if (returnType.Equals(""))
            {
                visitor.AddCodeLine($"procedure_{id}({arguments});");
            }
            else
            {

                // create new temp variable
                int counter = visitor.GetTmpVariableCounter();
                visitor.IncreaseTmpVariableCounter();
                string tmp = $"tmp_{counter}";
                visitor.SetLatestTmpVariableName(tmp);

                // if return type of the called function is array, then the
                // first parameter passed to the function should be return array size
                FunctionSymbol fs = visitor.GetSymbolTable().GetFunctionSymbolByIdentifier(id);
                if (fs.GetReturnType().Contains("array"))
                {
                    if(tmp.Contains("tmp_"))
                    {
                        visitor.AddCodeLine($"int size_{tmp};");
                        symTable.DeclareVariableSymbol(new VariableSymbol($"size_{tmp}", "integer", null, symTable.GetCurrentScope()));
                        arguments = $"&size_{tmp}, " + arguments;

                        // add tmp to memory map
                        int address = visitor.GetArrayAddressCounter();
                        visitor.IncreaseArrayAddressCounter();
                        
                        visitor.GetMemoryMap().AddNewAddress(new MemoryAddress(address, tmp, symTable.GetCurrentScope()));
                    }
                    else
                    {
                        string arrayName = tmp.Substring(4);
                        VariableSymbol arraySize = symTable.GetVariableSymbolByIdentifier($"size_{arrayName}");
                        string prefix = arraySize.IsPointer() ? "": "&";
                        arguments += $"int {prefix}{arraySize}";
                    }
                }

                symTable.DeclareVariableSymbol(new VariableSymbol(tmp, returnType, null, symTable.GetCurrentScope()));
                visitor.AddCodeLine($"{returnType} {tmp} = function_{id}({arguments});");
            }
        }

        /// <summary>
        /// Method <c>GetArgumentsCode</c> returns the arguments code for function or procedure call.
        /// </summary>
        private string GetArgumentsCode(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            // variable to hold generated arguments code
            string code = "";

            // go through all arguments and evalute the type of node
            for (int i = 0; i < args.Count; i++)
            {
                // get argument and generate code from it
                INode argument = args[i];
                argument.GenerateCode(visitor);

                // get last tmp variable --> this is from generated code
                string lastTmp = visitor.GetLatestUsedTmpVariable();

                // evaluate type of node 
                string evaluatedType = SemanticAnalyzer.EvaluateTypeOfNode(argument, visitor.GetSymbolTable());


                // get proper prefix for argument
                bool isPointer = false;
                if(lastTmp.Equals("false") || lastTmp.Equals("true"))
                {
                    int number = visitor.GetTmpVariableCounter();
                    visitor.IncreaseTmpVariableCounter();
                    visitor.AddCodeLine($"bool tmp_{number} = {lastTmp};");
                    visitor.SetLatestTmpVariableName($"tmp_{number}");
                    lastTmp = $"tmp_{number}";
                }
                else
                {
                    isPointer = symTable.GetVariableSymbolByIdentifier(lastTmp).IsPointer();
                }
                string prefix = Helper.GetPrefixWhenPointerNeeded(evaluatedType, isPointer);

                // add code
                code += $"{prefix}{lastTmp}";

                // if argument was array, pass the size as next argument
                if (argument.GetNodeType() == NodeType.VARIABLE && evaluatedType.Contains("array"))
                {
                    VariableNode varNode = (VariableNode)argument;
                    string name = varNode.GetName();

                    code += $", &size_{name}";
                }

                // if other arguments follow, then add comma
                if (i < args.Count - 1) code += ", ";
            }

            return code;
        }

        /// <summary>
        /// Method <c>GetCallReturnType</c> returns the return type of called function
        /// or procedure.
        /// </summary>
        private string GetCallReturnType(SymbolTable symbolTable)
        {
            if (symbolTable.IsProcedureInTable(id)) return "";

            FunctionSymbol fs = symbolTable.GetFunctionSymbolByIdentifier(id);

            string retType = fs.GetReturnType();

            return Helper.ConvertReturnTypeToC(retType);
        }

    }
}
