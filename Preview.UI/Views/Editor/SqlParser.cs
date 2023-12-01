using CUE4Parse.Utils;

using Irony.Parsing;

using Xylia.Preview.Data;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Views.Editor;

[Language("MySQL", "1.0", "SQL")]
public class SqlGrammar : Grammar
{
	public SqlGrammar() : base(false)
	{
		//SQL is case insensitive
		//Terminals
		var comment = new CommentTerminal("comment", "/*", "*/");
		var lineComment = new CommentTerminal("line_comment", "--", "\n", "\r\n");
		NonGrammarTerminals.Add(comment);
		NonGrammarTerminals.Add(lineComment);
		var number = new NumberLiteral("number");
		var string_literal = new StringLiteral("string", "'", StringOptions.AllowsDoubledQuote);
		var Id_simple = TerminalFactory.CreateSqlExtIdentifier(this, "id_simple"); //covers normal identifiers (abc) and quoted id's ([abc d], "abc d")
		var comma = ToTerm(",");
		var dot = ToTerm(".");
		var CREATE = ToTerm("CREATE");
		var NULL = ToTerm("NULL");
		var NOT = ToTerm("NOT");
		var UNIQUE = ToTerm("UNIQUE");
		var WITH = ToTerm("WITH");
		var TABLE = ToTerm("TABLE");
		var ALTER = ToTerm("ALTER");
		var ADD = ToTerm("ADD");
		var COLUMN = ToTerm("COLUMN");
		var DROP = ToTerm("DROP");
		var CONSTRAINT = ToTerm("CONSTRAINT");
		var INDEX = ToTerm("INDEX");
		var ON = ToTerm("ON");
		var KEY = ToTerm("KEY");
		var PRIMARY = ToTerm("PRIMARY");
		var INSERT = ToTerm("INSERT");
		var INTO = ToTerm("INTO");
		var UPDATE = ToTerm("UPDATE");
		var SET = ToTerm("SET");
		var VALUES = ToTerm("VALUES");
		var DELETE = ToTerm("DELETE");
		var SELECT = ToTerm("SELECT");
		var FROM = ToTerm("FROM");
		var AS = ToTerm("AS");
		var COUNT = ToTerm("COUNT");
		var JOIN = ToTerm("JOIN");
		var BY = ToTerm("BY");

		//Non-terminals
		var Id = new NonTerminal("Id");
		var stmt = new NonTerminal("stmt");
		var createTableStmt = new NonTerminal("createTableStmt");
		var createIndexStmt = new NonTerminal("createIndexStmt");
		var alterStmt = new NonTerminal("alterStmt");
		var dropTableStmt = new NonTerminal("dropTableStmt");
		var dropIndexStmt = new NonTerminal("dropIndexStmt");
		var selectStmt = new NonTerminal("selectStmt");
		var insertStmt = new NonTerminal("insertStmt");
		var updateStmt = new NonTerminal("updateStmt");
		var deleteStmt = new NonTerminal("deleteStmt");
		var fieldDef = new NonTerminal("fieldDef");
		var fieldDefList = new NonTerminal("fieldDefList");
		var nullSpecOpt = new NonTerminal("nullSpecOpt");
		var typeName = new NonTerminal("typeName");
		var typeSpec = new NonTerminal("typeSpec");
		var typeParamsOpt = new NonTerminal("typeParams");
		var constraintDef = new NonTerminal("constraintDef");
		var constraintListOpt = new NonTerminal("constraintListOpt");
		var constraintTypeOpt = new NonTerminal("constraintTypeOpt");
		var idlist = new NonTerminal("idlist");
		var idlistPar = new NonTerminal("idlistPar");
		var uniqueOpt = new NonTerminal("uniqueOpt");
		var orderList = new NonTerminal("orderList");
		var orderMember = new NonTerminal("orderMember");
		var orderDirOpt = new NonTerminal("orderDirOpt");
		var withClauseOpt = new NonTerminal("withClauseOpt");
		var alterCmd = new NonTerminal("alterCmd");
		var insertData = new NonTerminal("insertData");
		var intoOpt = new NonTerminal("intoOpt");
		var assignList = new NonTerminal("assignList");
		var whereClauseOpt = new NonTerminal("whereClauseOpt");
		var assignment = new NonTerminal("assignment");
		var expression = new NonTerminal("expression");
		var exprList = new NonTerminal("exprList");
		var selRestrOpt = new NonTerminal("selRestrOpt");
		var selList = new NonTerminal("selList");
		var intoClauseOpt = new NonTerminal("intoClauseOpt");
		var fromClauseOpt = new NonTerminal("fromClauseOpt");
		var groupClauseOpt = new NonTerminal("groupClauseOpt");
		var havingClauseOpt = new NonTerminal("havingClauseOpt");
		var orderClauseOpt = new NonTerminal("orderClauseOpt");
		var columnItemList = new NonTerminal("columnItemList");
		var columnItem = new NonTerminal("columnItem");
		var columnSource = new NonTerminal("columnSource");
		var asOpt = new NonTerminal("asOpt");
		var aliasOpt = new NonTerminal("aliasOpt");
		var aggregate = new NonTerminal("aggregate");
		var aggregateArg = new NonTerminal("aggregateArg");
		var aggregateName = new NonTerminal("aggregateName");
		var tuple = new NonTerminal("tuple");
		var joinChainOpt = new NonTerminal("joinChainOpt");
		var joinKindOpt = new NonTerminal("joinKindOpt");
		var term = new NonTerminal("term");
		var unExpr = new NonTerminal("unExpr");
		var unOp = new NonTerminal("unOp");
		var binExpr = new NonTerminal("binExpr");
		var binOp = new NonTerminal("binOp");
		var betweenExpr = new NonTerminal("betweenExpr");
		var inExpr = new NonTerminal("inExpr");
		var parSelectStmt = new NonTerminal("parSelectStmt");
		var notOpt = new NonTerminal("notOpt");
		var funCall = new NonTerminal("funCall");
		var stmtLine = new NonTerminal("stmtLine");
		var semiOpt = new NonTerminal("semiOpt");
		var stmtList = new NonTerminal("stmtList");
		var funArgs = new NonTerminal("funArgs");
		var inStmt = new NonTerminal("inStmt");

		var LimitClauseOpt = new NonTerminal("limitClauseOpt");


		//BNF Rules
		this.Root = stmtList;
		stmtLine.Rule = stmt + semiOpt;
		semiOpt.Rule = Empty | ";";
		stmtList.Rule = MakePlusRule(stmtList, stmtLine);

		//ID
		Id.Rule = MakePlusRule(Id, dot, Id_simple);

		stmt.Rule = createTableStmt | createIndexStmt | alterStmt
				  | dropTableStmt | dropIndexStmt
				  | selectStmt | insertStmt | updateStmt | deleteStmt
				  | "GO";
		//Create table
		createTableStmt.Rule = CREATE + TABLE + Id + "(" + fieldDefList + ")" + constraintListOpt;
		fieldDefList.Rule = MakePlusRule(fieldDefList, comma, fieldDef);
		fieldDef.Rule = Id + typeName + typeParamsOpt + nullSpecOpt;
		nullSpecOpt.Rule = NULL | NOT + NULL | Empty;
		typeName.Rule = ToTerm("BIT") | "DATE" | "TIME" | "TIMESTAMP" | "DECIMAL" | "REAL" | "FLOAT" | "SMALLINT" | "INTEGER"
									 | "INTERVAL" | "CHARACTER"
									 // MS SQL types:  
									 | "DATETIME" | "INT" | "DOUBLE" | "CHAR" | "NCHAR" | "VARCHAR" | "NVARCHAR"
									 | "IMAGE" | "TEXT" | "NTEXT";
		typeParamsOpt.Rule = "(" + number + ")" | "(" + number + comma + number + ")" | Empty;
		constraintDef.Rule = CONSTRAINT + Id + constraintTypeOpt;
		constraintListOpt.Rule = MakeStarRule(constraintListOpt, constraintDef);
		constraintTypeOpt.Rule = PRIMARY + KEY + idlistPar | UNIQUE + idlistPar | NOT + NULL + idlistPar
							   | "Foreign" + KEY + idlistPar + "References" + Id + idlistPar;
		idlistPar.Rule = "(" + idlist + ")";
		idlist.Rule = MakePlusRule(idlist, comma, Id);

		//Create Index
		createIndexStmt.Rule = CREATE + uniqueOpt + INDEX + Id + ON + Id + orderList + withClauseOpt;
		uniqueOpt.Rule = Empty | UNIQUE;
		orderList.Rule = MakePlusRule(orderList, comma, orderMember);
		orderMember.Rule = Id + orderDirOpt;
		orderDirOpt.Rule = Empty | "ASC" | "DESC";
		withClauseOpt.Rule = Empty | WITH + PRIMARY | WITH + "Disallow" + NULL | WITH + "Ignore" + NULL;

		//Alter 
		alterStmt.Rule = ALTER + TABLE + Id + alterCmd;
		alterCmd.Rule = ADD + COLUMN + fieldDefList + constraintListOpt
					  | ADD + constraintDef
					  | DROP + COLUMN + Id
					  | DROP + CONSTRAINT + Id;

		//Drop stmts
		dropTableStmt.Rule = DROP + TABLE + Id;
		dropIndexStmt.Rule = DROP + INDEX + Id + ON + Id;

		//Insert stmt
		insertStmt.Rule = INSERT + intoOpt + Id + idlistPar + insertData;
		insertData.Rule = selectStmt | VALUES + "(" + exprList + ")";
		intoOpt.Rule = Empty | INTO; //Into is optional in MSSQL

		//Update stmt
		updateStmt.Rule = UPDATE + Id + SET + assignList + whereClauseOpt;
		assignList.Rule = MakePlusRule(assignList, comma, assignment);
		assignment.Rule = Id + "=" + expression;

		//Delete stmt
		deleteStmt.Rule = DELETE + FROM + Id + whereClauseOpt;

		//Select stmt
		selectStmt.Rule = SELECT + selRestrOpt + selList + intoClauseOpt + fromClauseOpt + whereClauseOpt +
						  groupClauseOpt + havingClauseOpt + orderClauseOpt + LimitClauseOpt;
		selRestrOpt.Rule = Empty | "ALL" | "DISTINCT";
		selList.Rule = columnItemList | "*";
		columnItemList.Rule = MakePlusRule(columnItemList, comma, columnItem);
		columnItem.Rule = columnSource + aliasOpt;
		aliasOpt.Rule = Empty | asOpt + Id;
		asOpt.Rule = Empty | AS;
		columnSource.Rule = aggregate | Id;
		aggregate.Rule = aggregateName + "(" + aggregateArg + ")";
		aggregateArg.Rule = expression | "*";
		aggregateName.Rule = COUNT | "Avg" | "Min" | "Max" | "StDev" | "StDevP" | "Sum" | "Var" | "VarP";
		intoClauseOpt.Rule = Empty | INTO + Id;
		fromClauseOpt.Rule = Empty | FROM + idlist + joinChainOpt;
		joinChainOpt.Rule = Empty | joinKindOpt + JOIN + idlist + ON + Id + "=" + Id;
		joinKindOpt.Rule = Empty | "INNER" | "LEFT" | "RIGHT";
		whereClauseOpt.Rule = Empty | "WHERE" + expression;
		groupClauseOpt.Rule = Empty | "GROUP" + BY + idlist;
		havingClauseOpt.Rule = Empty | "HAVING" + expression;
		orderClauseOpt.Rule = Empty | "ORDER" + BY + orderList;
		LimitClauseOpt.Rule = Empty | "LIMIT" + number;


		//Expression
		exprList.Rule = MakePlusRule(exprList, comma, expression);
		expression.Rule = term | unExpr | binExpr;// | betweenExpr; //-- BETWEEN doesn't work - yet; brings a few parsing conflicts 
		term.Rule = Id | string_literal | number | funCall | tuple | parSelectStmt;// | inStmt;
		tuple.Rule = "(" + exprList + ")";
		parSelectStmt.Rule = "(" + selectStmt + ")";
		unExpr.Rule = unOp + term;
		unOp.Rule = NOT | "+" | "-" | "~";
		binExpr.Rule = expression + binOp + expression;
		binOp.Rule = ToTerm("+") | "-" | "*" | "/" | "%" //arithmetic
				   | "&" | "|" | "^"                     //bit
				   | "=" | ">" | "<" | ">=" | "<=" | "<>" | "!=" | "!<" | "!>"
				   | "AND" | "OR" | "LIKE" | NOT + "LIKE" | "IN" | NOT + "IN";
		betweenExpr.Rule = expression + notOpt + "BETWEEN" + expression + "AND" + expression;
		notOpt.Rule = Empty | NOT;
		//funCall covers some psedo-operators and special forms like ANY(...), SOME(...), ALL(...), EXISTS(...), IN(...)
		funCall.Rule = Id + "(" + funArgs + ")";
		funArgs.Rule = selectStmt | exprList;
		inStmt.Rule = expression + "IN" + "(" + exprList + ")";

		//Operators
		RegisterOperators(10, "*", "/", "%");
		RegisterOperators(9, "+", "-");
		RegisterOperators(8, "=", ">", "<", ">=", "<=", "<>", "!=", "!<", "!>", "LIKE", "IN");
		RegisterOperators(7, "^", "&", "|");
		RegisterOperators(6, NOT);
		RegisterOperators(5, "AND");
		RegisterOperators(4, "OR");

		MarkPunctuation(",", "(", ")");
		MarkPunctuation(asOpt, semiOpt);
		//Note: we cannot declare binOp as transient because it includes operators "NOT LIKE", "NOT IN" consisting of two tokens. 
		// Transient non-terminals cannot have more than one non-punctuation child nodes.
		// Instead, we set flag InheritPrecedence on binOp , so that it inherits precedence value from it's children, and this precedence is used
		// in conflict resolution when binOp node is sitting on the stack
		base.MarkTransient(stmt, term, asOpt, aliasOpt, stmtLine, expression, unOp, tuple);
		binOp.SetFlag(TermFlags.InheritPrecedence);
	}
}

public static class IronyExtension
{
	public static ParseTreeNode SelectNode(this ParseTreeNode node, string path) => SelectNodes(node, path).FirstOrDefault();

	public static IEnumerable<ParseTreeNode> SelectNodes(this ParseTreeNode node, string path)
	{
		// xpath
		path = path.Remove(0, 1);
		var current = path.SubstringBefore('/');
		var next = path.SubstringAfter(current).Trim();

		// select
		var temp = node.ChildNodes.Where(x => current == "*" || x.Term.Name == current);
		if (string.IsNullOrWhiteSpace(next)) return temp;
		else return temp.SelectMany(x => SelectNodes(x, next));
	}

	public static bool IsEmpty(this ParseTreeNode node) => node.ChildNodes.Count == 0;

	/// <summary>
	/// return token text
	/// </summary>
	public static string GetToken(this ParseTreeNode node) => node.Token.Text?.Trim('"');
}


public sealed class SqlParser
{
	private static readonly Parser parser = new(new LanguageData(new SqlGrammar()));
	public BnsDatabase Database { internal set; get; }

	public string[] Fields;
	public Record[] Source;

	public void Execute(string command)
	{
		Source = null;

		var tree = parser.Parse(command);
		var error = tree.ParserMessages.Where(m => m.Level == Irony.ErrorLevel.Error).Select(m => new Exception(m.Message));
		if (error.Any()) throw new AggregateException(error);

		// statement
		var stmt = tree.Root.ChildNodes[0];
		switch (stmt.Term.Name)
		{
			case "selectStmt": Select(stmt); break;
		}
	}


	private void Select(ParseTreeNode node)
	{
		var Name = node.SelectNode("/fromClauseOpt/idlist/Id/*").GetToken();
		var fields = node.SelectNodes("/selList/columnItemList/columnItem/columnSource/Id/*").Select(x => x.GetToken()).ToList();
		var limitNum = (int?)node.SelectNode("/limitClauseOpt/number")?.Token.Value;
		var WhereClause = node.SelectNode("/whereClauseOpt");


		#region Source
		if (Database is null) return;

		var table = Database.Provider.Tables[Name];
		if (table is null) return;

		IEnumerable<Record> source = table.Records;
		if (!WhereClause.IsEmpty()) source = source.Where(record => CheckWhere(record, WhereClause));
		if (limitNum != null) source = source.Take(limitNum.Value);

		this.Source = source.ToArray();
		#endregion

		#region Field
		if (!fields.Any() && Source.Length > 0)
		{
			var firstType = source.First().SubclassType;
			var IsMulti = source.FirstOrDefault(x => firstType != x.SubclassType) != null;

			fields = (IsMulti ? table.Definition.ElRecord : source.First().ElDefinition).ExpandedAttributes.Select(o => o.Name).ToList();
		}

		if (table.Definition.ElRecord.Subtables.Count > 1)
			fields.Insert(0, "type");

		Fields = fields.ToArray();
		#endregion
	}

	private static bool CheckWhere(Record record, ParseTreeNode expr)
	{
		var Op = expr.SelectNode("/binOp/*")?.Token.Text;
		var binExpr = expr.SelectNodes("/binExpr");
		if (binExpr.Any())
		{
			var result = binExpr.Select(x => CheckWhere(record, x));

			if (Op is null || Op == "and" || Op == "&") return result.All(x => x == true);
			else if (Op == "or" || Op == "|") return result.FirstOrDefault(x => x == true) != false;
		}
		else
		{
			var Id = expr.SelectNodes("/Id/*").Select(x => x.GetToken()).ToArray();
			if (Id.Length > 1)
			{
				var key = Id[0];
				var value = Id[1];
				var _value = record.Attributes[key];

				// TODO: String.Equal
				// TODO: like
				if (Op == "=" || Op == "==") return _value == value;  
				else if (Op == "!=" || Op == "<>") return _value != value;
			}
			else
			{
				var key = Id[0];
				var value = (int)expr.SelectNode("/number").Token.Value;
				long _value = 0;

				var obj = record.Attributes.Get(key);
				if (obj is int i) _value = i;
				else if (obj is sbyte b) _value = b;
				else if (obj is short s) _value = s;
				else if (obj is long l) _value = l;
				else return false;


				if (Op == "=" || Op == "==") return _value == value;
				else if (Op == "!=" || Op == "<>") return _value != value;
				else if (Op == "<") return _value < value;
				else if (Op == ">") return _value > value;
				else if (Op == "<=") return _value <= value;
				else if (Op == ">=") return _value >= value;
			}
		}

		throw new NotImplementedException("Invalid operator: " + Op);
	}
}