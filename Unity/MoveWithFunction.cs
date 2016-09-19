using UnityEngine;
using System.Collections.Generic;
using MathFunctionParser;


public class MoveWithFunction : MonoBehaviour {

    Parser xFuncParser, yFuncParser;
    SortedDictionary<string, Token> db;
    Evaluator xEvaluator, yEvaluator;

    public string xFuncString = "sin(t)";
    public string yFuncString = "cos(t)";


	void Start () {

        db = ExpressionDB.GetDefaultVarDB(); 
        db["t"] = new Token(TokenType.Variable, "t");
        xFuncParser = new Parser(db, ExpressionDB.GetDefaultConstDB());
        yFuncParser = new Parser(db, ExpressionDB.GetDefaultConstDB());
        xEvaluator = xFuncParser.Parse(xFuncString);
        yEvaluator = yFuncParser.Parse(yFuncString);
	}
	
	void Update () {
        xEvaluator.SetVariable("t", Time.time);
        yEvaluator.SetVariable("t", Time.time);
        double x = xEvaluator.Evaluate();
        double y = yEvaluator.Evaluate();
        transform.position = new Vector3((float)x, (float)y);
	}
}
