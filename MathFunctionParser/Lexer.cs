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

        /** Methods */

        // Returns a token queue to parse
        public Queue<Token> AnalyzeString(string expression)
        {
            this.expression = RemoveAllWhiteSpace(expression);
            expressionLC = this.expression.ToLower();
            return AnalyzeString(0, expressionLC.Length);
        }
        // Helper method to public AnalyzeString
        // Analyzes from start (inclusive) to end (exclusive)
        private Queue<Token> AnalyzeString(int start, int end)
        {
            return null;
        }
        /** This function counts the number of bracket pairs for a definable 
         *  substring region. It looks from start(inclusive) to end(exclusive)
         *  This function will throw an exception if the expression has not 
         *  been defined or if the number of left brackets is not equal to the
         *  number of right brackets
         */
        private int CountBrackets(int start, int end)
        {
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
            if( leftCounter != rightCounter )
            {
                // Throw exception
            }
            return leftCounter;
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
