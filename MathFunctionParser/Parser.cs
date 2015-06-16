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
        SortedDictionary<string, Token> varEvalDB;
        SortedDictionary<string, Token> constDB;
        Lexer lexer;
        Evaluator evaluator; 
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
        public Evaluator Parse(string expression)
        {
            tokenList = lexer.AnalyzeString(expression);
            evaluator = new Evaluator(E());
            return evaluator;
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
                case TokenType.Variable:
                    string name = next.expression;
                    return new VariableNode(name, evaluator.GetVarValueMap);
                case TokenType.Function:
                    LinkedList<Token> oldList = tokenList;
                    tokenList = next.subList;
                    EvaluatorNode returnVal = Function(E(), next.function);
                    tokenList = oldList;
                    return returnVal;
            }
            return null;
        }
        private EvaluatorNode Function(EvaluatorNode node, FunctionType func)
        {
            Func<double, double> function = x => x;
            Func<double, double, double> originalFunc = node.evalFunc;
            switch(func) {
                case FunctionType.Sin:
                    function = x => Math.Sin(x);
                    break;
                case FunctionType.Cos:
                    function = x => Math.Cos(x);
                    break;
                case FunctionType.Tan:
                    function = x => Math.Tan(x);
                    break;
                case FunctionType.Arcsin:
                    function = x => Math.Asin(x);
                    break;
                case FunctionType.Arccos:
                    function = x => Math.Acos(x);
                    break;
                case FunctionType.Arctan:
                    function = x => Math.Atan(x);
                    break;
                case FunctionType.Sinh:
                    function = x => Math.Sinh(x);
                    break;
                case FunctionType.Cosh:
                    function = x => Math.Cosh(x);
                    break;
                case FunctionType.Tanh:
                    function = x => Math.Tanh(x);
                    break;
                case FunctionType.Arcsinh:
                    function = x => Math.Log(x + Math.Sqrt(x*x+1));
                    break;
                case FunctionType.Arccosh:
                    function = x => Math.Log(x + Math.Sqrt(x+1)*Math.Sqrt(x-1));
                    break;
                case FunctionType.Arctanh:
                    function = x => (Math.Log(1+x)-Math.Log(1-x))/2;
                    break;
            }
            node.evalFunc = (double l, double r) => function(originalFunc(l,r));
            
        }
    }
}
