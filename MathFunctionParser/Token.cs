using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathFunctionParser
{
    public enum TokenType
    {
        Function,
        Operator,
        Variable,
        Constant
    }
    public enum FunctionType
    {
        Sin,
        Cos,
        Tan,
        Sec,
        Csc,
        Cot,
        Arcsin,
        Arccos,
        Arctan,
        Arcsec,
        Arccsc,
        Arccot,
        Sinh,
        Cosh,
        Tanh,
        Sech,
        Csch,
        Coth,
        Arccosh,
        Arcsinh,
        Arctanh,
        Arcsech,
        Arccsch,
        Arccoth,
        Log,
        Ln,
        Floor,
        Ceiling,
        Sqrt,
        Abs,
        RegularExpression
    }
    /** This class represents a token to be read by the expression parser */
    public class Token
    {
        public string expression;
        public TokenType type;
        public FunctionType function;
        public LinkedList<Token> subList;
        public Token(FunctionType func)
        {
            this.function = func;
            this.type = TokenType.Function;
            subList = new LinkedList<Token>();
        }
        public Token(TokenType type, string expression)
        {
            this.type = type;
            this.expression = expression;
            this.function = FunctionType.RegularExpression;
        }
        public override string ToString()
        {
            string retVal = "";
            if (type == TokenType.Function)
            {
                if (function != FunctionType.RegularExpression)
                    retVal += function.ToString();
                retVal += "(";
                foreach (Token t in subList)
                    retVal += t.ToString();
                retVal += ")";
                return retVal;

            }
            return expression;
        }
    }
}
