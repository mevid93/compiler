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

        ////////////// EVERYTHING AFTER THIS IS FOR CODE GENERATION ////////////////

        public void GenerateCode(Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // check if predfined read procedure
            if (id.Equals("read") && !symTable.IsFunctionInTable(id) && !symTable.IsProcedureInTable(id))
            {
                GenerateCodeForPredefinedRead(visitor);
                return;
            }

            // check if predefined writeln procedure
            if (id.Equals("writeln") && !symTable.IsFunctionInTable(id) && !symTable.IsProcedureInTable(id))
            {
                GenerateCodeForPredefinedWriteln(visitor);
                return;
            }

            // custom procedure or function
            GenerateCodeForCustomFunction(visitor);
        }

        ///////////// METHODS THAT ARE RELATED TO SCANF /////////////////

        /// <summary>
        /// Method <c>GenerateCodeForPredefinedRead</c> genereates the code
        /// for predefined read method.
        /// </summary>
        private void GenerateCodeForPredefinedRead(Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();
            
            // variables to hold scanf format string (first parameter)
            // and readable arguments
            string format_str = "";
            string scan_args = ", ";

            for(int i = 0; i < args.Count; i++)
            {
                // arguments must be separated
                if(i > 0)
                {
                    format_str += " ";
                    scan_args += ", ";
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
                string argument = GetScanfArgument(args[i], visitor);
                scan_args += argument;

            }

            // create new code line to list of generated code lines
            string line = $"scanf(\"{format_str}\"{scan_args});";
            visitor.AddCodeLine(line);
        }

        /// <summary>
        /// Method <c>GetScanfArgument</c> will return correct argument
        /// for scanf corresponding the given node. Node should be variable
        /// or binary expression (array index).
        /// </summary>
        private string GetScanfArgument(INode node, Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            switch (node.GetNodeType())
            {
                case NodeType.VARIABLE:
                    string type = SemanticAnalyzer.EvaluateTypeOfVariableNode(node, new List<string>(), symTable);
                    VariableNode varNode = (VariableNode)node;
                    string name = varNode.GetName();
                    VariableSymbol varSymbol = symTable.GetVariableSymbolByIdentifier(name);
                    string prefix = CodeGenerator.GetPrefixForArgumentByType(type, varSymbol.IsParameter());
                    
                    switch (type)
                    {
                        case "integer":
                        case "real":
                        case "string":
                            return $"{prefix}var_{name}";
                        default:
                            return $"var_{name}";
                    }
                case NodeType.BINARY_EXPRESSION:
                    BinaryExpressionNode bin = (BinaryExpressionNode)node;
                    VariableNode lhs = (VariableNode)bin.GetLhs();
                    INode rhs = bin.GetRhs();
                    rhs.GenerateCode(visitor);
                    string tmp = visitor.GetLatestUsedTmpVariable();
                    return $"var_{lhs.GetName()}[{tmp}]";
                default:
                    throw new Exception("Unexpected error... invalid scanf argument type!");
            }
        }

        ///////////// METHODS THAT ARE RELATED TO PRINTF /////////////////

        /// <summary>
        /// Method <c>GenerateCodeForPredefinedWriteln</c> generates the code for predefined
        /// writeln.
        /// </summary>
        private void GenerateCodeForPredefinedWriteln(Visitor visitor)
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
                    format_str += " ";
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
            string line = $"printf(\"{format_str}\"{print_args});";
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
            return lastTemp;
        }

        ///////////// METHODS THAT ARE RELATED TO CUSTOM FUNCTIONS /////////////////

        /// <summary>
        /// Method <c>GenerateCodeForCustomFunction</c> generates the code for custom
        /// function.
        /// </summary>
        private void GenerateCodeForCustomFunction(Visitor visitor)
        {
            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // get arguments code that is passed to function or procedure call
            string arguments = GetArgumentsCode(visitor);
            
            // create new temp variable
            int counter = visitor.GetTempVariableCounter();
            visitor.IncreaseTempVariableCounter();
            string temp = "tmp_" + counter;
            visitor.SetLatestTmpVariableName(temp);

            // get function return type (if procedure, then "")
            string returnType = GetCallReturnType(symTable);
            
            // add code line to list of code lines
            if (returnType.Equals(""))
            {
                visitor.AddCodeLine($"procedure_{id}({arguments});");
            }
            else
            {
                visitor.AddCodeLine($"{returnType} {temp} = function_{id}({arguments});");
            }
        }

        /// <summary>
        /// Method <c>GetArgumentsCode</c> returns the arguments code for function or procedure call.
        /// </summary>
        private string GetArgumentsCode(Visitor visitor)
        {
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
                bool isParameter = false;
                string prefix = CodeGenerator.GetPrefixForArgumentByType(evaluatedType, isParameter);

                // add code
                code += $"{prefix}{lastTmp}";

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

            return CodeGenerator.ConvertReturnTypeToTargetLanguage(retType);
        }

    }
}
