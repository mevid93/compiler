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

        public string GenerateCode(List<string> codeLines)
        {
            throw new NotImplementedException();
        }
    }
}
