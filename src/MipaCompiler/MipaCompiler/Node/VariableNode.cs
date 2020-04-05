using System;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>VariableNode</c> is variable holding node.
    /// When variables are parsed, variable node should be added to AST.
    /// Arrays are special and have their own node.
    /// </summary>
    public class VariableNode : INode
    {
        private readonly int row;       // row in source code
        private readonly int col;       // column in source code
        private readonly string type;   // type of variable (int, real, bool, string)
        private readonly string name;   // variable name

        /// <summary>
        /// Constructor <c>VariableNode</c> creates new VariableNode-object.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public VariableNode(int row, int col, string name, string type)
        {
            this.row = row;
            this.col = col;
            this.name = name;
            this.type = type;
        }

        /// <summary>
        /// Method <c>GetName</c> returns the name of the variable.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Method <c>GetSimpleType</c> returns the type of the variable.
        /// VariableNode is only used for simple types (int, real, bool, string).
        /// </summary>
        public string GetSimpleType()
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
            Console.WriteLine($"Type: {type}");
            Console.WriteLine($"Name: {name}");
        }
    }
}
