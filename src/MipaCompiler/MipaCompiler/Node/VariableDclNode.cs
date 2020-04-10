using System;
using System.Collections.Generic;

namespace MipaCompiler.Node
{
    /// <summary>
    /// Class <c>VariableDclNode</c> is a node where one or multiple
    /// variables are declared.
    /// </summary>
    public class VariableDclNode : INode
    {
        private readonly int row;       // row in source code
        private readonly int col;       // column in source code
        private List<string> names;     // names of variables
        private readonly INode type;    // type of the variables

        /// <summary>
        /// Constructor <c>VariableDclNode</c> creates new VariableDclNode-object.
        /// </summary>
        /// <param name="row">row in source code</param>
        /// <param name="col">column in source code</param>
        /// <param name="type">type of variable</param>
        public VariableDclNode(int row, int col, INode type)
        {
            this.row = row;
            this.col = col;
            this.type = type;
            names = new List<string>();
        }

        /// <summary>
        /// Method <c>AddVariableName</c> adds new variable to list of variables to be declared.
        /// </summary>
        /// <param name="name"></param>
        public void AddVariableName(string name)
        {
            names.Add(name);
        }

        /// <summary>
        /// Method <c>GetVariableNames</c> returns the list of variable names.
        /// </summary>
        /// <returns>list of variable names</returns>
        public List<string> GetVariableNames()
        {
            return names;
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
            foreach (string name in names)
            {
                Console.WriteLine($"Name: {name}");
            }
            Console.WriteLine($"Type: {type}");
        }
    }
}
