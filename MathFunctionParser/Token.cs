using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunctionParser
{
    enum TokenType
    {
        Function,
        Operator,
        Variable,
        Constant
    }
    enum FunctionType
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
        RegularExpression
    }
    /** This class represents a token to be read by the expression parser */
    class Token
    {
        public string expression;
        public TokenType type;
        public FunctionType function;
        public Token(FunctionType func)
        {
            this.function = func;
            this.type = TokenType.Function;
        }
        public Token(TokenType type, string expression)
        {
            this.type = type;
            this.expression = expression;
            this.function = FunctionType.RegularExpression;
        }
    }
}
