﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunctionParser
{
    /**
     * This class will anaylze a string for mathematical expressions and 
     * produces a queue of tokens for the parser to handle.
     */
    class Lexer
    {
        /** Fields */

        // DBs
        private SortedDictionary<string, Token> funcDB;
        private SortedDictionary<string, Token> constDB;
        private SortedDictionary<string, Token> varDB;
        private SortedDictionary<char, Token> opDB;
        // The white space trimmed expression to anaylze
        private string expression;
        // The lower case varient of the variable expression declared above
        private string expressionLC;

        /** Constructors */

        // Default constructor
        // Sets the DB dictionaries to default DBs
        public Lexer() 
            : this(ExpressionDB.GetDefaultVarDB(), ExpressionDB.GetDefaultConstDB())
        {
            funcDB = ExpressionDB.GetFuncDB();
            constDB = ExpressionDB.GetDefaultConstDB();
        }
        // This constructor allows the use of custom variables and constants
        public Lexer(SortedDictionary<string, Token> varDB,
                     SortedDictionary<string, Token> constDB)
        {
            this.varDB = varDB;
            this.constDB = constDB;
        }

    }
}