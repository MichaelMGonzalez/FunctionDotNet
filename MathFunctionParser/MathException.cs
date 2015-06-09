using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunctionParser
{
    public class LexerException : Exception
    {
        public enum Type
        {
            BracketCountMismatch,
            ExpressionNotDefined
        }
        public Type type;
        public LexerException(Type type) : base (GetErrMessage(type))
        {
            this.type = type;
        }
        public LexerException(Type type, string moreInfo) : base (GetErrMessage(type) + moreInfo)
        {
            this.type = type;
        }
        private static string GetErrMessage(Type type)
        {
            string errMsg = "";
            switch (type)
            {
                case Type.BracketCountMismatch:
                    errMsg += "The number of left parenthesis '(' does not ";
                    errMsg += "match the number of right parenthesis ')'!";
                    break;
                    
            }
            return errMsg;
        }
    }
}
