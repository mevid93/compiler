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

        /// <summary>
        /// Method <c>GenerateCodeForPredefinedRead</c> genereates the code
        /// for predefined read method.
        /// </summary>
        private void GenerateCodeForPredefinedRead(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            string line = "scanf(";

            string str = "";
            string var = ", ";

            bool first = true;
            foreach (INode argument in args)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    str += " ";
                    var += ", ";
                }
                VariableNode varNode = (VariableNode)argument;
                string varName = varNode.GetName();
                VariableSymbol varSymbol = symTable.GetVariableSymbolByIdentifier(varName);
                string varType = varSymbol.GetSymbolType();

                if (varType == "integer")
                {
                    str += "%d";
                    var += "&var_" + varName;
                }
            }

            line += "\"" + str + "\"" + var;

            line += ");";
            visitor.AddCodeLine(line);
        }

        /// <summary>
        /// Method <c>GenerateCodeForPredefinedWriteln</c> generates the code for predefined
        /// writeln.
        /// </summary>
        private void GenerateCodeForPredefinedWriteln(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            string line = "printf(";

            string str = "";
            string var = ", ";

            bool first = true;
            foreach (INode argument in args)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    str += " ";
                    var += ", ";
                }

                switch (argument.GetNodeType())
                {
                    case NodeType.VARIABLE:
                        VariableNode varNode = (VariableNode)argument;
                        string varName = varNode.GetName();
                        VariableSymbol varSymbol = symTable.GetVariableSymbolByIdentifier(varName);
                        string varType = varSymbol.GetSymbolType();

                        if (varType == "integer")
                        {
                            str += "%d";
                            var += "var_" + varName;
                        }
                        break;
                    case NodeType.CALL:
                        CallNode callNode = (CallNode)argument;
                        string id = callNode.GetId();
                        FunctionSymbol f = symTable.GetFunctionSymbolByIdentifier(id);
                        string retType = f.GetReturnType();
                        callNode.GenerateCode(visitor);
                        string lastTmp = visitor.GetLatestUsedTmpVariable();
                       
                        if(retType == "integer")
                        {
                            str += "%d";
                            var += lastTmp;
                        }
                        break;
                    default:
                        throw new Exception("Unexpected error... unsupported argument type!");
                }

            }

            line += "\"" + str + "\"" + var;

            line += ");";
            visitor.AddCodeLine(line);
            return;
        }

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
