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
            : this(ExpressionDB.GetDefaultVarDB(),
                   ExpressionDB.GetDefaultConstDB()){}
        // This constructor allows the use of custom variables and constants
        public Lexer(SortedDictionary<string, Token> varDB,
                     SortedDictionary<string, Token> constDB)
        {
            this.varDB = varDB;
            this.constDB = constDB;
            funcDB = ExpressionDB.GetFuncDB();
            opDB = ExpressionDB.GetOperatorDB();
        }

        /** Methods */

        // Returns a token queue to parse
        public LinkedList<Token> AnalyzeString(string expression)
        {
            LinkedList<Token> tokenList= new LinkedList<Token>();
            this.expression = RemoveAllWhiteSpace(expression);
            expressionLC = this.expression.ToLower();
            int startIndex = 0;
            while (startIndex < expressionLC.Length)
            {
                startIndex = AnalyzeString(startIndex, expressionLC.Length, tokenList);
                Console.WriteLine("Expression: " + expression);
                Console.WriteLine("StartIndex: " + startIndex);
            }
            Console.WriteLine();
            return tokenList;
        }
        // Helper method to public AnalyzeString
        // Analyzes from start (inclusive) to end (exclusive)
        private int AnalyzeString(int start, int end, LinkedList<Token> list)
        {
            Console.WriteLine(expression);
            Console.WriteLine("Analyzing from " + start + " until " + end);
            // Token to add to the list
            Token tok = null;
            string subString;
            int numberOfSubExpressions = CountBrackets(start, end);
            int retVal = end;
            // Case: Function Analysis
            // If true, there might be a function to analyze
            if (retVal == end && numberOfSubExpressions > 0)
            {
                int leftBracketIdx = expressionLC.IndexOf('(' , start);
                int rightBracketIdx = IndexOfClosingBracket(leftBracketIdx, end);
                // The from the start index to the index of '('
                subString = expressionLC.Substring(start, leftBracketIdx-start);
                Console.WriteLine("The subString before '(' is " + subString);
                // If true, then there's a function to analyze
                if (leftBracketIdx == start || funcDB.ContainsKey(subString))
                {
                    if (leftBracketIdx == start)
                        tok = new Token(FunctionType.RegularExpression);
                    else
                        tok = new Token(funcDB[subString]);
                    subString = expression.Substring(leftBracketIdx + 1, rightBracketIdx-leftBracketIdx-1);
                    Lexer subLex = new Lexer();
                    tok.subList = subLex.AnalyzeString(subString);
                    list.AddLast(tok);
                    return (rightBracketIdx + 1);
                    //return rightBracketIdx + 1;
                }
            }
            int indexOfNextOp = IndexOfNextOp(start, end);
            // Case: Operators Analysis 
            // If the index of the next operator isn't -1, then the string 
            // before that operator should be some number
            if (retVal == end && indexOfNextOp != -1)
            {
                // Recursively call this function on the preceding string
                if( start != indexOfNextOp )
                    AnalyzeString(start, indexOfNextOp, list);
                // Add the operator to the list
                list.AddLast(opDB[expression[indexOfNextOp]]);
                Console.WriteLine("Index of next Operator: " + indexOfNextOp);
                return (indexOfNextOp + 1);
            }
            // At this point, we assume this substring is some plain number, 
            // constant, or a variable. We define the substring key to verify
            subString = expression.Substring(start, end - start);
            Console.WriteLine("Number Substring: " + subString);
            // Case: Variable Analysis
            if (varDB.ContainsKey(subString))
            {
                list.AddLast(varDB[subString]);
            }
            // Case: Constant Analysis
            else if (constDB.ContainsKey(subString))
            {
                list.AddLast(constDB[subString]);
            }
            // Case: Plain Number
            // It might not be a plain number (random string) but the parser
            // will throw out such input
            else
            {
                list.AddLast(new Token(TokenType.Constant, subString));
            }
            return retVal;
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
                throw new LexerException(errType, leftCounter.ToString() + " " + rightCounter.ToString());
            }
            return leftCounter;
        }
        /** This function returns the index of the next ')' bracket matching 
         *  the ')' bracket indexed at start. It searches the range 
         *  start(inclusive) to end(exclusive).
         */
        private int IndexOfClosingBracket(int start, int end)
        {
            int bracketCounter = 0;
            int i = start;
            for (; i < end; i++)
            {
                switch (expression[i])
                {
                    case '(':
                        bracketCounter++;
                        break;
                    case ')':
                        if (--bracketCounter == 0)
                            return i;
                        break;
                }

            }
            return i;
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
