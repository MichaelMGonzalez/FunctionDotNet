using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathFunctionParser
{
    public class ExpressionDB
    {
        public static SortedDictionary<string, FunctionType> GetFuncDB()
        {
            SortedDictionary<string, FunctionType> db = new SortedDictionary<string, FunctionType>();
            db["sin"] = FunctionType.Sin;
            db["cos"] = FunctionType.Cos;
            db["tan"] = FunctionType.Tan;
            db["csc"] = FunctionType.Csc;
            db["sec"] = FunctionType.Sec;
            db["cot"] = FunctionType.Cot;
            db["sinh"] = FunctionType.Sinh;
            db["cosh"] = FunctionType.Cosh;
            db["tanh"] = FunctionType.Tanh;
            db["csch"] = FunctionType.Csch;
            db["sech"] = FunctionType.Sech;
            db["coth"] = FunctionType.Coth;
            db["arcsin"] = FunctionType.Arcsin;
            db["arccos"] = FunctionType.Arccos;
            db["arctan"] = FunctionType.Arctan;
            db["arccsc"] = FunctionType.Arccsc;
            db["arcsec"] = FunctionType.Arcsec;
            db["arccot"] = FunctionType.Arccot;
            db["arcsinh"] = FunctionType.Arcsinh;
            db["arccosh"] = FunctionType.Arccosh;
            db["arctanh"] = FunctionType.Arctanh;
            db["arccsch"] = FunctionType.Arccsch;
            db["arcsech"] = FunctionType.Arcsech;
            db["arccoth"] = FunctionType.Arccoth;
            db["ln"] = FunctionType.Ln;
            db["log"] = FunctionType.Log;
            db["floor"] = FunctionType.Floor;
            db["ceiling"] = FunctionType.Ceiling;
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
