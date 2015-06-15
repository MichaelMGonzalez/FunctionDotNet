using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunctionParser
{
    class Evaluator
    {
        // Fields
        private SortedDictionary<string, double> varRealValue;
        private EvaluatorNode root;

        // Constructor
        public Evaluator(EvaluatorNode root) {
            this.root = root;
            varRealValue = new SortedDictionary<string, double>();
        }

        // Methods
        public SortedDictionary<string, double> GetVarValueMap;
    }
}
