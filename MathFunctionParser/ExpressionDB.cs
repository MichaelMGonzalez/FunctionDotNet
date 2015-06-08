using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunctionParser
{
    public class ExpressionDB
    {
        public static SortedDictionary<string, Token> GetFuncDB()
        {
            SortedDictionary<string, Token> db = new SortedDictionary<string, Token>();
            db["sin"] = new Token(FunctionType.Sin);
            db["cos"] = new Token(FunctionType.Cos);
            db["tan"] = new Token(FunctionType.Tan);
            db["csc"] = new Token(FunctionType.Csc);
            db["sec"] = new Token(FunctionType.Sec);
            db["cot"] = new Token(FunctionType.Cot);
            db["sinh"] = new Token(FunctionType.Sinh);
            db["cosh"] = new Token(FunctionType.Cosh);
            db["tanh"] = new Token(FunctionType.Tanh);
            db["csch"] = new Token(FunctionType.Csch);
            db["sech"] = new Token(FunctionType.Sech);
            db["coth"] = new Token(FunctionType.Coth);
            db["arcsin"] = new Token(FunctionType.Arcsin);
            db["arccos"] = new Token(FunctionType.Arccos);
            db["arctan"] = new Token(FunctionType.Arctan);
            db["arccsc"] = new Token(FunctionType.Arccsc);
            db["arcsec"] = new Token(FunctionType.Arcsec);
            db["arccot"] = new Token(FunctionType.Arccot);
            db["arcsinh"] = new Token(FunctionType.Arcsinh);
            db["arccosh"] = new Token(FunctionType.Arccosh);
            db["arctanh"] = new Token(FunctionType.Arctanh);
            db["arccsch"] = new Token(FunctionType.Arccsch);
            db["arcsech"] = new Token(FunctionType.Arcsech);
            db["arccoth"] = new Token(FunctionType.Arccoth);
            db["ln"] = new Token(FunctionType.Ln);
            db["log"] = new Token(FunctionType.Log);
            db["floor"] = new Token(FunctionType.Floor);
            db["ceiling"] = new Token(FunctionType.Ceiling);
            return db;
        } 
        public static SortedDictionary<char, Token> GetOperatorDB()
        {
            SortedDictionary<char, Token> db = new SortedDictionary<char, Token>();
            db['+'] = new Token(TokenType.Operator, "+");
            db['-'] = new Token(TokenType.Operator, "-");
            db['*'] = new Token(TokenType.Operator, "*");
            db['/'] = new Token(TokenType.Operator, "/");
            db['^'] = new Token(TokenType.Operator, "^");
            return db;
        } 
        public static SortedDictionary<string, Token> GetDefaultVarDB()
        {
            SortedDictionary<string, Token> db = new SortedDictionary<string, Token>();
            db["x"] = new Token(TokenType.Variable, "x");
            db["y"] = new Token(TokenType.Variable, "y");
            return db;
        } 
        public static SortedDictionary<string, Token> GetDefaultConstDB()
        {
            SortedDictionary<string, Token> db = new SortedDictionary<string, Token>();
            db["pi"] = new Token(TokenType.Constant, "3.14159");
            db["phi"] = new Token(TokenType.Constant, "1.61803");
            db["e"] = new Token(TokenType.Constant, "2.718281");
            return db;
        } 
    }
}
