//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from .\Lab2Grammar.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public partial class Lab2GrammarParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		LBRACKET=1, RBRACKET=2, CELLPOS=3, POWER=4, MULTIPLICATION=5, INC=6, DEC=7, 
		DIV=8, ADDITION=9, SUBTRACTION=10, MOD=11, NUM=12, WHITESPACE=13, SYMBOL=14;
	public const int
		RULE_unit = 0, RULE_expr = 1;
	public static readonly string[] ruleNames = {
		"unit", "expr"
	};

	private static readonly string[] _LiteralNames = {
		null, "'('", "')'", null, "'^'", "'*'", "'inc'", "'dec'", "'/'", "'+'", 
		"'-'", "' mod '"
	};
	private static readonly string[] _SymbolicNames = {
		null, "LBRACKET", "RBRACKET", "CELLPOS", "POWER", "MULTIPLICATION", "INC", 
		"DEC", "DIV", "ADDITION", "SUBTRACTION", "MOD", "NUM", "WHITESPACE", "SYMBOL"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Lab2Grammar.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static Lab2GrammarParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public Lab2GrammarParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public Lab2GrammarParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class UnitContext : ParserRuleContext {
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public ITerminalNode Eof() { return GetToken(Lab2GrammarParser.Eof, 0); }
		public UnitContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_unit; } }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterUnit(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitUnit(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitUnit(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public UnitContext unit() {
		UnitContext _localctx = new UnitContext(Context, State);
		EnterRule(_localctx, 0, RULE_unit);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 4; expr(0);
			State = 5; Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExprContext : ParserRuleContext {
		public ExprContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expr; } }
	 
		public ExprContext() { }
		public virtual void CopyFrom(ExprContext context) {
			base.CopyFrom(context);
		}
	}
	public partial class RestContext : ExprContext {
		public ITerminalNode SYMBOL() { return GetToken(Lab2GrammarParser.SYMBOL, 0); }
		public RestContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterRest(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitRest(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitRest(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class DecContext : ExprContext {
		public ITerminalNode DEC() { return GetToken(Lab2GrammarParser.DEC, 0); }
		public ITerminalNode LBRACKET() { return GetToken(Lab2GrammarParser.LBRACKET, 0); }
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public ITerminalNode RBRACKET() { return GetToken(Lab2GrammarParser.RBRACKET, 0); }
		public DecContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterDec(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitDec(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDec(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ModContext : ExprContext {
		public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		public ITerminalNode MOD() { return GetToken(Lab2GrammarParser.MOD, 0); }
		public ModContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterMod(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitMod(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMod(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class BracketsContext : ExprContext {
		public ITerminalNode LBRACKET() { return GetToken(Lab2GrammarParser.LBRACKET, 0); }
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public ITerminalNode RBRACKET() { return GetToken(Lab2GrammarParser.RBRACKET, 0); }
		public BracketsContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterBrackets(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitBrackets(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitBrackets(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class AdditionOrSubtractionContext : ExprContext {
		public IToken operatorToken;
		public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		public ITerminalNode ADDITION() { return GetToken(Lab2GrammarParser.ADDITION, 0); }
		public ITerminalNode SUBTRACTION() { return GetToken(Lab2GrammarParser.SUBTRACTION, 0); }
		public AdditionOrSubtractionContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterAdditionOrSubtraction(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitAdditionOrSubtraction(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAdditionOrSubtraction(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class NumContext : ExprContext {
		public ITerminalNode NUM() { return GetToken(Lab2GrammarParser.NUM, 0); }
		public NumContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterNum(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitNum(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNum(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class MultiplicationOrDivisionContext : ExprContext {
		public IToken operatorToken;
		public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		public ITerminalNode DIV() { return GetToken(Lab2GrammarParser.DIV, 0); }
		public ITerminalNode MULTIPLICATION() { return GetToken(Lab2GrammarParser.MULTIPLICATION, 0); }
		public MultiplicationOrDivisionContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterMultiplicationOrDivision(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitMultiplicationOrDivision(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMultiplicationOrDivision(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class CellPosContext : ExprContext {
		public ITerminalNode CELLPOS() { return GetToken(Lab2GrammarParser.CELLPOS, 0); }
		public CellPosContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterCellPos(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitCellPos(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCellPos(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class PowerContext : ExprContext {
		public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		public ITerminalNode POWER() { return GetToken(Lab2GrammarParser.POWER, 0); }
		public PowerContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterPower(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitPower(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPower(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class NegNumContext : ExprContext {
		public ITerminalNode SUBTRACTION() { return GetToken(Lab2GrammarParser.SUBTRACTION, 0); }
		public ITerminalNode NUM() { return GetToken(Lab2GrammarParser.NUM, 0); }
		public NegNumContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterNegNum(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitNegNum(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNegNum(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class IncContext : ExprContext {
		public ITerminalNode INC() { return GetToken(Lab2GrammarParser.INC, 0); }
		public ITerminalNode LBRACKET() { return GetToken(Lab2GrammarParser.LBRACKET, 0); }
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public ITerminalNode RBRACKET() { return GetToken(Lab2GrammarParser.RBRACKET, 0); }
		public IncContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.EnterInc(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ILab2GrammarListener typedListener = listener as ILab2GrammarListener;
			if (typedListener != null) typedListener.ExitInc(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ILab2GrammarVisitor<TResult> typedVisitor = visitor as ILab2GrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInc(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExprContext expr() {
		return expr(0);
	}

	private ExprContext expr(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		ExprContext _localctx = new ExprContext(Context, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 2;
		EnterRecursionRule(_localctx, 2, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 27;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case SUBTRACTION:
				{
				_localctx = new NegNumContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;

				State = 8; Match(SUBTRACTION);
				State = 9; Match(NUM);
				}
				break;
			case INC:
				{
				_localctx = new IncContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 10; Match(INC);
				State = 11; Match(LBRACKET);
				State = 12; expr(0);
				State = 13; Match(RBRACKET);
				}
				break;
			case DEC:
				{
				_localctx = new DecContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 15; Match(DEC);
				State = 16; Match(LBRACKET);
				State = 17; expr(0);
				State = 18; Match(RBRACKET);
				}
				break;
			case LBRACKET:
				{
				_localctx = new BracketsContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 20; Match(LBRACKET);
				State = 21; expr(0);
				State = 22; Match(RBRACKET);
				}
				break;
			case NUM:
				{
				_localctx = new NumContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 24; Match(NUM);
				}
				break;
			case CELLPOS:
				{
				_localctx = new CellPosContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 25; Match(CELLPOS);
				}
				break;
			case SYMBOL:
				{
				_localctx = new RestContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 26; Match(SYMBOL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			Context.Stop = TokenStream.LT(-1);
			State = 43;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,2,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					State = 41;
					ErrorHandler.Sync(this);
					switch ( Interpreter.AdaptivePredict(TokenStream,1,Context) ) {
					case 1:
						{
						_localctx = new PowerContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 29;
						if (!(Precpred(Context, 7))) throw new FailedPredicateException(this, "Precpred(Context, 7)");
						State = 30; Match(POWER);
						State = 31; expr(8);
						}
						break;
					case 2:
						{
						_localctx = new MultiplicationOrDivisionContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 32;
						if (!(Precpred(Context, 6))) throw new FailedPredicateException(this, "Precpred(Context, 6)");
						State = 33;
						((MultiplicationOrDivisionContext)_localctx).operatorToken = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==MULTIPLICATION || _la==DIV) ) {
							((MultiplicationOrDivisionContext)_localctx).operatorToken = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 34; expr(7);
						}
						break;
					case 3:
						{
						_localctx = new AdditionOrSubtractionContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 35;
						if (!(Precpred(Context, 5))) throw new FailedPredicateException(this, "Precpred(Context, 5)");
						State = 36;
						((AdditionOrSubtractionContext)_localctx).operatorToken = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==ADDITION || _la==SUBTRACTION) ) {
							((AdditionOrSubtractionContext)_localctx).operatorToken = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 37; expr(6);
						}
						break;
					case 4:
						{
						_localctx = new ModContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 38;
						if (!(Precpred(Context, 4))) throw new FailedPredicateException(this, "Precpred(Context, 4)");
						State = 39; Match(MOD);
						State = 40; expr(5);
						}
						break;
					}
					} 
				}
				State = 45;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,2,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 1: return expr_sempred((ExprContext)_localctx, predIndex);
		}
		return true;
	}
	private bool expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0: return Precpred(Context, 7);
		case 1: return Precpred(Context, 6);
		case 2: return Precpred(Context, 5);
		case 3: return Precpred(Context, 4);
		}
		return true;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\x10', '\x31', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x5', '\x3', '\x1E', 
		'\n', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\a', '\x3', ',', '\n', '\x3', '\f', 
		'\x3', '\xE', '\x3', '/', '\v', '\x3', '\x3', '\x3', '\x2', '\x3', '\x4', 
		'\x4', '\x2', '\x4', '\x2', '\x4', '\x4', '\x2', '\a', '\a', '\n', '\n', 
		'\x3', '\x2', '\v', '\f', '\x2', '\x38', '\x2', '\x6', '\x3', '\x2', '\x2', 
		'\x2', '\x4', '\x1D', '\x3', '\x2', '\x2', '\x2', '\x6', '\a', '\x5', 
		'\x4', '\x3', '\x2', '\a', '\b', '\a', '\x2', '\x2', '\x3', '\b', '\x3', 
		'\x3', '\x2', '\x2', '\x2', '\t', '\n', '\b', '\x3', '\x1', '\x2', '\n', 
		'\v', '\a', '\f', '\x2', '\x2', '\v', '\x1E', '\a', '\xE', '\x2', '\x2', 
		'\f', '\r', '\a', '\b', '\x2', '\x2', '\r', '\xE', '\a', '\x3', '\x2', 
		'\x2', '\xE', '\xF', '\x5', '\x4', '\x3', '\x2', '\xF', '\x10', '\a', 
		'\x4', '\x2', '\x2', '\x10', '\x1E', '\x3', '\x2', '\x2', '\x2', '\x11', 
		'\x12', '\a', '\t', '\x2', '\x2', '\x12', '\x13', '\a', '\x3', '\x2', 
		'\x2', '\x13', '\x14', '\x5', '\x4', '\x3', '\x2', '\x14', '\x15', '\a', 
		'\x4', '\x2', '\x2', '\x15', '\x1E', '\x3', '\x2', '\x2', '\x2', '\x16', 
		'\x17', '\a', '\x3', '\x2', '\x2', '\x17', '\x18', '\x5', '\x4', '\x3', 
		'\x2', '\x18', '\x19', '\a', '\x4', '\x2', '\x2', '\x19', '\x1E', '\x3', 
		'\x2', '\x2', '\x2', '\x1A', '\x1E', '\a', '\xE', '\x2', '\x2', '\x1B', 
		'\x1E', '\a', '\x5', '\x2', '\x2', '\x1C', '\x1E', '\a', '\x10', '\x2', 
		'\x2', '\x1D', '\t', '\x3', '\x2', '\x2', '\x2', '\x1D', '\f', '\x3', 
		'\x2', '\x2', '\x2', '\x1D', '\x11', '\x3', '\x2', '\x2', '\x2', '\x1D', 
		'\x16', '\x3', '\x2', '\x2', '\x2', '\x1D', '\x1A', '\x3', '\x2', '\x2', 
		'\x2', '\x1D', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x1D', '\x1C', '\x3', 
		'\x2', '\x2', '\x2', '\x1E', '-', '\x3', '\x2', '\x2', '\x2', '\x1F', 
		' ', '\f', '\t', '\x2', '\x2', ' ', '!', '\a', '\x6', '\x2', '\x2', '!', 
		',', '\x5', '\x4', '\x3', '\n', '\"', '#', '\f', '\b', '\x2', '\x2', '#', 
		'$', '\t', '\x2', '\x2', '\x2', '$', ',', '\x5', '\x4', '\x3', '\t', '%', 
		'&', '\f', '\a', '\x2', '\x2', '&', '\'', '\t', '\x3', '\x2', '\x2', '\'', 
		',', '\x5', '\x4', '\x3', '\b', '(', ')', '\f', '\x6', '\x2', '\x2', ')', 
		'*', '\a', '\r', '\x2', '\x2', '*', ',', '\x5', '\x4', '\x3', '\a', '+', 
		'\x1F', '\x3', '\x2', '\x2', '\x2', '+', '\"', '\x3', '\x2', '\x2', '\x2', 
		'+', '%', '\x3', '\x2', '\x2', '\x2', '+', '(', '\x3', '\x2', '\x2', '\x2', 
		',', '/', '\x3', '\x2', '\x2', '\x2', '-', '+', '\x3', '\x2', '\x2', '\x2', 
		'-', '.', '\x3', '\x2', '\x2', '\x2', '.', '\x5', '\x3', '\x2', '\x2', 
		'\x2', '/', '-', '\x3', '\x2', '\x2', '\x2', '\x5', '\x1D', '+', '-',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
