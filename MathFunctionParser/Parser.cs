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
            Func<double, double> f = x => x;
            Func<double, double, double> originalFunc = node.evalFunc;

            // The following declarations make this function easier to read
            Func<double, double> Ln = x => Math.Log(x);
            Func<double, double> Sqrt = x => Math.Log(x);
            const double one = 1;
            switch(func) {
                case FunctionType.Sin:
                    f = x => Math.Sin(x);
                    break;
                case FunctionType.Cos:
                    f = x => Math.Cos(x);
                    break;
                case FunctionType.Tan:
                    f = x => Math.Tan(x);
                    break;
                case FunctionType.Arcsin:
                    f = x => Math.Asin(x);
                    break;
                case FunctionType.Arccos:
                    f = x => Math.Acos(x);
                    break;
                case FunctionType.Arctan:
                    f = x => Math.Atan(x);
                    break;
                case FunctionType.Csc:
                    f = x => one / Math.Sin(x);
                    break;
                case FunctionType.Sec:
                    f = x => one / Math.Cos(x);
                    break;
                case FunctionType.Cot:
                    f = x => one / Math.Tan(x);
                    break;
                case FunctionType.Arccsc:
                    f = x => Math.Asin(one / x);
                    break;
                case FunctionType.Arcsec:
                    f = x => Math.Acos(one / x);
                    break;
                case FunctionType.Arccot:
                    f = x => Math.Atan(one / x);
                    break;
                case FunctionType.Sinh:
                    f = x => Math.Sinh(x);
                    break;
                case FunctionType.Cosh:
                    f = x => Math.Cosh(x);
                    break;
                case FunctionType.Tanh:
                    f = x => Math.Tanh(x);
                    break;
                case FunctionType.Arcsinh:
                    f = x => Ln(x + Sqrt(x*x+1));
                    break;
                case FunctionType.Arccosh:
                    f = x => Ln(x + Sqrt(x+1)*Sqrt(x-1));
                    break;
                case FunctionType.Arctanh:
                    f = x => (Ln(1+x)-Ln(1-x))/2;
                    break;
                case FunctionType.Csch:
                    f = x => one / Math.Sinh(x);
                    break;
                case FunctionType.Sech:
                    f = x => one / Math.Cosh(x);
                    break;
                case FunctionType.Coth:
                    f = x => one / Math.Tanh(x);
                    break;
                case FunctionType.Arccsch:
                    f = x => Ln(Sqrt(one / (x * x)) + (one / x));
                    break;
                case FunctionType.Arcsech:
                    f = x => Ln(Sqrt(one / x - 1)*Sqrt(one / x + 1)+(one/x));
                    break;
                case FunctionType.Arccoth:
                    f = x => (Ln(1-x)-Ln(1+x))/2;
                    break;
                case FunctionType.Ln:
                    f = Ln;
                    break;
                case FunctionType.Log:
                    f = x => Math.Log10(x);
                    break;
                case FunctionType.Floor:
                    f = x => Math.Floor(x);
                    break;
                case FunctionType.Ceiling:
                    f = x => Math.Ceiling(x);
                    break;
                case FunctionType.Sqrt:
                    f = Sqrt;
                    break;
                case FunctionType.Abs:
                    f = x => Math.Abs(x);
                    break;

            }
            node.evalFunc = (double l, double r) => f(originalFunc(l,r));
            return node;
        }
    }
}
