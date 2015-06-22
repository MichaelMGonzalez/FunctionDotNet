using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathFunctionParser
{
    public class Evaluator
    {
        // Fields
        private SortedDictionary<string, double> varRealValue;
        internal EvaluatorNode root;

        // Constructor
        public Evaluator( SortedDictionary<string, Token> varDB) {
            varRealValue = new SortedDictionary<string, double>();
            foreach (string key in varDB.Keys)
            {
                varRealValue[key] = 0;
            }
        }

        // Methods
        public SortedDictionary<string, double> GetVarValueMap() {
            return varRealValue; 
        }
        public double Evaluate() { return root.Evaluate(); }
        public void SetVariable(string var, double value)
        {
            varRealValue[var] = value;
        }
        public double EvaluateVariable(string var, double value)
        {
            SetVariable(var, value);
            return Evaluate();
        }
        public int GetNumberOfVars()
        {
            return root.numOfVars;
        }
    }
}
