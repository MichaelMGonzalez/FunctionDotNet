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
        LinkedList<Token> tokenList;

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
            tokenList = lexer.AnalyzeString(expression);
            // TODO: Change return value
            return null;
        }
        /** E -> P + E |
         *       P - E |
         *       P
         */
        private EvaluatorNode E()
        {
            return P();
        }
        /** P -> K * P |
         *       K / P |
         *       K
         */
        private EvaluatorNode P()
        {
            return K();
        }
        /** K -> -K | R */
        private EvaluatorNode K()
        {
            return R();
        }
        /** R -> V ^ V | R */
        private EvaluatorNode R()
        {
            return V();
        }
        /** V -> Constant | Function | (E) */
        private EvaluatorNode V()
        {
            Token next = tokenList.First.Value;
            switch (next.type)
            {
                case TokenType.Constant:
                    double value = 0;
                    try
                    {
                        value = Double.Parse(next.expression);
                    }
                    catch (FormatException e)
                    {
                        string errorMsg = next.expression;
                        errorMsg += " is not a defined variable or constant!";
                        new ParserException(errorMsg);
                    }
                    return new ConstantNode(value);
            }
        }
    }
}
