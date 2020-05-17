using MipaCompiler.BackEnd;
using MipaCompiler.Symbol;
using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>VariableDclNode</c> represents variable declaration statement in AST.
    /// </summary>
    public class VariableDclNode : INode
    {
        private readonly int row;               // row in source code
        private readonly int col;               // column in source code
        private readonly List<INode> variables; // names of variables
        private readonly INode type;            // type of the variables

        /// <summary>
        /// Constructor <c>VariableDclNode</c> creates new VariableDclNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="variables">variables to declare</param>
        /// <param name="type">type of variable</param>
        public VariableDclNode(int row, int col, List<INode> variables, INode type)
        {
            this.row = row;
            this.col = col;
            this.type = type;
            this.variables = new List<INode>();
            if (variables != null) this.variables = variables;
        }

        /// <summary>
        /// Method <c>GetVariables</c> returns the list of variables.
        /// </summary>
        /// <returns>list of variable</returns>
        public List<INode> GetVariables()
        {
            return variables;
        }

        /// <summary>
        /// Method <c>GetVariableType</c> returns type of variables.
        /// </summary>
        /// <returns>type</returns>
        public INode GetVariableType()
        {
            return type;
        }

        public int GetCol()
        {
            return col;
        }

        public NodeType GetNodeType()
        {
            return NodeType.VARIABLE_DCL;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.VARIABLE_DCL}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            foreach (INode node in variables)
            {
                VariableNode varNode = (VariableNode)node;
                Console.WriteLine($"Name: {varNode.GetName()}");
            }
            Console.WriteLine($"Type: {type}");
        }

        ////////////////// EVERYTHING AFTER THIS IS FOR CODE GENERATION /////////////////////

        public void GenerateCode(Visitor visitor)
        {
            // simple type assignment
            if (type.GetNodeType() == NodeType.SIMPLE_TYPE)
            {
                GenerateCodeForSimpleType(visitor);
            }

            // array type
            if (type.GetNodeType() == NodeType.ARRAY_TYPE)
            {
                GenerateCodeForArrayType(visitor);
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForSimpleType</c> handles the code generation when
        /// type of the variable to be declared is simple type.
        /// </summary>
        private void GenerateCodeForSimpleType(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            // get simple type
            SimpleTypeNode stn = (SimpleTypeNode)type;
            string simpleType = stn.GetTypeValue();

            // get simple type in C-language
            string cType = Helper.ConvertDeclarationTypeToC(simpleType);

            // declare each variable to newline (easier to read)
            for (int i = 0; i < variables.Count; i++)
            {
                VariableNode varNode = (VariableNode)variables[i];
                string name = varNode.GetName();
                string nameInC = $"var_{name}";

                // symbol table update
                if (symTable.IsVariableSymbolInTable(name))
                {
                    symTable.ReDeclareVariableSymbol(new VariableSymbol(name, simpleType, null, symTable.GetCurrentScope()));
                    symTable.ReDeclareVariableSymbol(new VariableSymbol(nameInC, simpleType, null, symTable.GetCurrentScope()));
                }
                else
                {
                    symTable.DeclareVariableSymbol(new VariableSymbol(name, simpleType, null, symTable.GetCurrentScope()));
                    symTable.DeclareVariableSymbol(new VariableSymbol(nameInC, simpleType, null, symTable.GetCurrentScope()));
                }

                // generate code lines
                if (simpleType.Equals("string"))
                {
                    // if string --> allocate memory buffer
                    int number1 = visitor.GetTmpVariableCounter();
                    visitor.IncreaseTmpVariableCounter();
                    visitor.AddCodeLine($"int tmp_{number1} = sizeof(char);");

                    int number2 = visitor.GetTmpVariableCounter();
                    visitor.IncreaseTmpVariableCounter();
                    visitor.AddCodeLine($"int tmp_{number2} = 256 * tmp_{number1};");

                    visitor.AddCodeLine($"{cType} {nameInC} = malloc(tmp_{number2});");
                }
                else
                {
                    // boolean, integer, or real
                    string codeLine = $"{cType} {nameInC};";
                    visitor.AddCodeLine(codeLine);
                }
            }
        }

        /// <summary>
        /// Method <c>GenerateCodeForArrayType</c> handles the code genearation when
        /// variable to be declared is an array.
        /// </summary>
        private void GenerateCodeForArrayType(Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            // first find out the simple type of array
            ArrayTypeNode arr = (ArrayTypeNode)type;
            SimpleTypeNode stn = (SimpleTypeNode)arr.GetSimpleType();
            string simpleType = stn.GetTypeValue();

            // get actual node type
            string nodeType = SemanticAnalyzer.EvaluateTypeOfTypeNode(type, new List<string>(), symTable);

            // get declaration type in C-language
            string cType = Helper.ConvertDeclarationTypeToC(nodeType);

            // generate code for array size --> only if it is given
            string tmpSize = GetArraySize(arr, visitor);

            // define list where all names of the new size variables are stored
            List<string> sizeNames = new List<string>();

            // declare each variable in a new line (easier to read)
            for (int i = 0; i < variables.Count; i++)
            {
                VariableNode varNode = (VariableNode)variables[i];
                string name = varNode.GetName();
                string varName = $"var_{name}";

                // symbol table update
                if (symTable.IsVariableSymbolInTable(name))
                {
                    symTable.ReDeclareVariableSymbol(new VariableSymbol(name, nodeType, null, symTable.GetCurrentScope()));
                    symTable.ReDeclareVariableSymbol(new VariableSymbol(varName, nodeType, null, symTable.GetCurrentScope()));
                }
                else
                {
                    symTable.DeclareVariableSymbol(new VariableSymbol(name, nodeType, null, symTable.GetCurrentScope()));
                    symTable.DeclareVariableSymbol(new VariableSymbol(varName, nodeType, null, symTable.GetCurrentScope()));
                }

                // add new size variable name to list (one for each array)
                sizeNames.Add($"size_{name}");

                string allocationType = Helper.ConvertTypeToMallocTypeInC(nodeType);

                if(tmpSize != null)
                {
                    // check if size is pointer
                    AllocateNonEmptyArray(simpleType, allocationType, cType, varName, tmpSize, visitor);
                }
                else
                {
                    // empty array initialized
                    AllocateEmptyArray(simpleType, allocationType, cType, varName, visitor);
                }

            }

            // declare size variables
            DeclareArraySizeVariables(sizeNames, tmpSize, visitor);
        }

        /// <summary>
        /// Method <c>GetArraySize</c> returns variable name holding array size.
        /// </summary>
        /// <returns></returns>
        private string GetArraySize(ArrayTypeNode arr, Visitor visitor)
        {
            string tmpSize = null;
            if (arr.GetSizeExpression() != null)
            {
                arr.GetSizeExpression().GenerateCode(visitor);

                // get tmp variable that holds the size of the array
                tmpSize = visitor.GetLatestUsedTmpVariable();
            }
            return tmpSize;
        }

        /// <summary>
        /// Method <c>AllocateNonEmptyArray</c> generates code for non empty array allocation.
        /// </summary>
        private void AllocateNonEmptyArray(string simpleType, string allocationType, string cType, string varName, string tmpSize, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            string prefix = "";
            if (symTable.GetVariableSymbolByIdentifier(tmpSize).IsPointer()) prefix = "*";

            int number = visitor.GetTmpVariableCounter();
            visitor.IncreaseTmpVariableCounter();
            visitor.AddCodeLine($"int tmp_{number} = sizeof({allocationType});");

            int number2 = visitor.GetTmpVariableCounter();
            visitor.IncreaseTmpVariableCounter();
            visitor.AddCodeLine($"int tmp_{number2} = {prefix}{tmpSize} * tmp_{number};");

            // allocate memory for array
            string codeLine = $"{cType} {varName} = malloc(tmp_{number2});";

            int address = visitor.GetArrayAddressCounter();
            visitor.IncreaseArrayAddressCounter();
            visitor.GetMemoryMap().AddNewAddress(new MemoryAddress(address, varName, symTable.GetCurrentScope()));

            // each string arrays are allocated by callind hard coded function
            prefix = prefix.Equals("*") ? "" : "&";
            if (simpleType == "string") codeLine = $"{cType} {varName} = allocateArrayOfStrings({prefix}{tmpSize});";

            // add generated code line to list of code lines
            visitor.AddCodeLine(codeLine);
        }

        /// <summary>
        /// Method <c>AllocateEmptyArray</c> generates code for empty array declaration.
        /// </summary>
        private void AllocateEmptyArray(string simpleType, string allocationType, string cType, string varName, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            if (simpleType.Equals("string"))
            {
                int number = visitor.GetTmpVariableCounter();
                visitor.IncreaseTmpVariableCounter();
                visitor.AddCodeLine($"int tmp_{number} = 0;");
                visitor.AddCodeLine($"{cType} {varName} = allocateArrayOfStrings(&tmp_{number});");
            }
            else
            {
                int number = visitor.GetTmpVariableCounter();
                visitor.IncreaseTmpVariableCounter();
                visitor.AddCodeLine($"int tmp_{number} = sizeof({allocationType});");

                int number2 = visitor.GetTmpVariableCounter();
                visitor.IncreaseTmpVariableCounter();
                visitor.AddCodeLine($"int tmp_{number2} = 0 * tmp_{number};");
                visitor.AddCodeLine($"{cType} {varName} = malloc(tmp_{number2});");
            }

            int address = visitor.GetArrayAddressCounter();
            visitor.IncreaseArrayAddressCounter();
            visitor.GetMemoryMap().AddNewAddress(new MemoryAddress(address, varName, symTable.GetCurrentScope()));
        }

        /// <summary>
        /// Method <c>DeclareArraySizeVariables</c> generates code for array size variables.
        /// </summary>
        private void DeclareArraySizeVariables(List<string> sizeNames, string tmpSize, Visitor visitor)
        {
            SymbolTable symTable = visitor.GetSymbolTable();

            foreach (string size in sizeNames)
            {
                VariableSymbol varSymbol = new VariableSymbol(size, "integer", null, symTable.GetCurrentScope());
                symTable.DeclareVariableSymbol(varSymbol);

                // check if size is pointer
                string prefix = "";
                if (tmpSize != null)
                {
                    if (symTable.GetVariableSymbolByIdentifier(tmpSize).IsPointer()) prefix = "*";
                }
                else
                {
                    tmpSize = "0";
                }

                string codeLine = $"int {size} = {prefix}{tmpSize};";
                visitor.AddCodeLine(codeLine);
            }
        }
    }
}
