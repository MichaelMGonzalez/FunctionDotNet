using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunctionParser
{
    /**
     * This class will analyze a string for mathematical expressions and 
     * produces a queue of tokens for the parser to handle.
     */
    class Lexer
    {
        /** Fields */

        // DBs
        private SortedDictionary<string, FunctionType> funcDB;
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

        /** Methods */

        // Returns a token queue to parse
        public LinkedList<Token> AnalyzeString(string expression)
        {
            LinkedList<Token> tokenList= new LinkedList<Token>();
            this.expression = RemoveAllWhiteSpace(expression);
            expressionLC = this.expression.ToLower();
            AnalyzeString(0, expressionLC.Length, tokenList);
            return tokenList;
        }
        // Helper method to public AnalyzeString
        // Analyzes from start (inclusive) to end (exclusive)
        private void AnalyzeString(int start, int end, LinkedList<Token> list)
        {
            // Token to add to the list
            Token tok = null;
            string funcSS;
            int numberOfSubExpressions = CountBrackets(start, end);
            // Case: Function Analysis
            // If true, there might be a function to analyze
            if (numberOfSubExpressions > 0)
            {
                int leftBracketIdx = expressionLC.IndexOf('(' , start);
                int rightBracketIdx = expressionLC.IndexOf(')' , start);
                funcSS = expressionLC.Substring(start, leftBracketIdx);
                // If true, then there's a function to analyze
                if (leftBracketIdx == start || funcDB.ContainsKey(funcSS))
                {
                    if (leftBracketIdx == start)
                    {
                        tok = new Token(FunctionType.RegularExpression);
                    }
                    else
                        tok = new Token(funcDB[funcSS]);
                    AnalyzeString(leftBracketIdx + 1, end, tok.subList);
                    list.AddLast(tok);
                    
                }
            }
            int indexOfNextOp = IndexOfNextOp(start, end);
        }
        /** This function counts the number of bracket pairs for a definable 
         *  substring region. It looks from start(inclusive) to end(exclusive)
         *  This function will throw an exception if the expression has not 
         *  been defined or if the number of left brackets is not equal to the
         *  number of right brackets
         */
        private int CountBrackets(int start, int end)
        {
            // If an exception happen, then it's stored here
            LexerException.Type errType;
            // Throw an exception if expression isn't set
            if(expression == null)
            {
                errType = LexerException.Type.ExpressionNotDefined;
                throw new LexerException(errType);
            }
            // Keeps track of the number of left and right brackets
            int leftCounter = 0;
            int rightCounter = 0;
            // Count the number of bracket pairs
            for (int i = start; i < end; i++)
            {
                switch( expression[i] )
                {
                    case '(':
                        leftCounter++;
                        break;
                    case ')':
                        rightCounter++;
                        break;
                }
            }
            // Throws exception
            if( leftCounter != rightCounter )
            {
                errType = LexerException.Type.BracketCountMismatch;
                throw new LexerException(errType);
            }
            return leftCounter;
        }
        /** This function returns the index of the next operator or -1 if one
         *  cannot be found. It searchs expression from start(inclusive) to 
         *  end(exclusive). 
         */
        private int IndexOfNextOp(int start, int end)
        {
            // Index
            for (int i = start; i < end; i++ )
            {
                if (opDB.ContainsKey(expression[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public string RemoveAllWhiteSpace(string expression)
        {
            // Value to return
            string retVal = string.Copy(expression).Trim();
            // Index of the white space
            int i = retVal.IndexOf(' ');
            // Temporary storage of substrings
            string leftString, rightString;
            // While there are white spaces, remove them
            while (i != -1)
            {
                leftString = retVal.Substring(0, i);
                rightString = retVal.Substring(i + 1, retVal.Length - i - 1);
                retVal = (leftString + rightString).Trim();
                i = retVal.IndexOf(' ');
            }
            return retVal;
        }
    }
}
