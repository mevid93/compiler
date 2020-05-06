using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>VariableNode</c> represents variable in AST. It is also
    /// used to store parameter and boolean values.
    /// </summary>
    public class VariableNode : INode
    {
        private readonly int row;       // row in source code
        private readonly int col;       // column in source code
        private readonly INode type;    // type of variable
        private readonly string name;   // variable name

        /// <summary>
        /// Constructor <c>VariableNode</c> creates new VariableNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="name">name of the variable</param>
        /// <param name="type">type of the variable (optional)</param>
        public VariableNode(int row, int col, string name, INode type)
        {
            this.row = row;
            this.col = col;
            this.name = name;
            this.type = type;
        }

        /// <summary>
        /// Method <c>GetName</c> returns the name of the variable.
        /// </summary>
        /// <returns>variable name</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Method <c>GetVariableType</c> returns the type of the variable.
        /// </summary>
        /// ´<returns>variable type</returns>
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
            return NodeType.VARIABLE;
        }

        public int GetRow()
        {
            return row;
        }

        public void PrettyPrint()
        {
            Console.WriteLine($"NodeType: {NodeType.VARIABLE}");
            Console.WriteLine($"Row: {row}, Column: {col}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Type:");
            if (type != null) type.PrettyPrint();
        }

        public void GenerateCode(Visitor visitor)
        {
            // ecah variable should have an alternative variable symbol
            // in symbol table with name "var_" + name
            string varName = $"var_{name}";

            // variable does not need to be assigned to temporary variable.
            // however, we still have to set it as the latest tmp variable.
            visitor.SetLatestTmpVariableName(varName);
        }
    }
}
