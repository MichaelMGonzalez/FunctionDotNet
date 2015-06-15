using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunctionParser
{
    /** This class will parse a string containing a mathmatical function and 
     *  return an Expression Tree which can evaluate the function.
     */
    class Parser
    {
        // Fields
        SortedDictionary<string, Token> varDB;
        SortedDictionary<string, Token> constDB;
        Lexer lexer;

        // Constructors
        public Parser() 
            : this(ExpressionDB.GetDefaultVarDB(), ExpressionDB.GetDefaultConstDB()){}
        public Parser(SortedDictionary<string, Token> varDB,
                      SortedDictionary<string, Token> constDB)
        {
            this.varDB = varDB;
            this.constDB = constDB;
            lexer = new Lexer(varDB, constDB);
        }

        // Methods
        public EvaluatorNode Parse(string expression)
        {
            // TODO: Change return value
            LinkedList<Token> tokenList = lexer.AnalyzeString(expression);
            return null;
        }
        /** E -> P + E |
         *       P - E |
         *       P
         */
        public EvaluatorNode E()
        {
            return P();
        }
        /** P -> K * P |
         *       K / P |
         *       K
         */
        public EvaluatorNode P()
        {
            return K();
        }
        /** K -> -K | R */
        public EvaluatorNode K()
        {
            return R();
        }
        /** R -> V ^ V | R */
        public EvaluatorNode R()
        {
            return V();
        }
        /** V -> Constant | Function | (E) */
        public EvaluatorNode V()
        {
        }
    }
}
