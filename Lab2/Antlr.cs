using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime.Misc;

namespace Lab2
{
    public class AntlrVisitor : Lab2GrammarBaseVisitor<int>
    {
        CellView Evaluating;
        TableView Table;
        HashSet<string> UsedCells;
        public AntlrVisitor(TableView table, HashSet<string> usedCells, CellView evaluating) : base()
        {
            Table = table;
            Evaluating = evaluating;
            UsedCells = usedCells;
            console.log($"Evaluating: {evaluating}");
            console.log("Dependecies:");
            foreach (var d in evaluating.Dependencies)
            {
                console.log(d);
            }
            console.log("Depended:");
            foreach (var d in evaluating.Depended)
            {
                console.log(d);
            }
        }
        public override int VisitUnit(Lab2GrammarParser.UnitContext context)
        {
            try
            {
                console.log(context.GetText());
                int ans = Visit(context.expr());
                console.log($"{ans}");
                return ans;
            }
            catch
            {
                throw;
            }
        }
        public override int VisitNum([NotNull] Lab2GrammarParser.NumContext context)
        {
            int ans = int.Parse(context.GetText());
            console.log($"num {ans}");
            return ans;
        }
        public override int VisitAdditionOrSubtraction(Lab2GrammarParser.AdditionOrSubtractionContext context)
        {
            int l = LeftOperand(context), r = RightOperand(context);
            int ans = 0;
            if (context.operatorToken.Type == Lab2GrammarLexer.ADDITION)
            {
                ans = l + r;
                console.log($"plus {ans}");
            }
            else if (context.operatorToken.Type == Lab2GrammarLexer.SUBTRACTION)
            {
                ans = l - r;
                console.log($"minus {ans}");
            }
            return ans;
        }
        public override int VisitMultiplicationOrDivision(Lab2GrammarParser.MultiplicationOrDivisionContext context)
        {
            int l = LeftOperand(context), r = RightOperand(context);
            int ans = 0;
            if (context.operatorToken.Type == Lab2GrammarLexer.MULTIPLICATION)
            {
                ans = l * r;
                console.log($"mult {ans}");
            }
            else if (context.operatorToken.Type == Lab2GrammarLexer.DIV)
            {
                if (r == 0)
                {
                    var ex = new Exception();
                    ex.Data.Add("Type", "division by 0");
                    throw ex;
                }
                ans = l / r;
                console.log($"div {ans}");
            }
            return ans;
        }
        public override int VisitMod([NotNull] Lab2GrammarParser.ModContext context)
        {
            var l = LeftOperand(context);
            var r = RightOperand(context);
            if (r == 0)
            {
                var ex = new DivideByZeroException();
                ex.Data.Add("Type", "modulo by zero");
                throw ex;
            }
            int ans = l % r;
            console.log($"mod {ans}");
            return ans;
        }
        public override int VisitPower([NotNull] Lab2GrammarParser.PowerContext context)
        {
            var l = LeftOperand(context);
            var r = RightOperand(context);
            if (r < 0)
            {
                var ex = new ArgumentOutOfRangeException();
                ex.Data.Add("Type", "negative power");
                throw ex;
            }
            if (r == 0 && l == 0)
            {
                var ex = new ArgumentOutOfRangeException();
                ex.Data.Add("Type", "0^0");
                throw ex;
            }
            int ans = (int)Math.Pow(l, r);
            console.log($"pow {ans}");
            return ans;
        }
        public override int VisitBrackets([NotNull] Lab2GrammarParser.BracketsContext context)
        {
            int ans = Visit(context.expr());
            console.log($"par {ans}");
            return ans;
        }
        public override int VisitNegNum([NotNull] Lab2GrammarParser.NegNumContext context)
        {
            int ans = int.Parse(context.GetText());
            console.log($"neg {ans}");
            return ans;
        }
        public override int VisitCellPos([NotNull] Lab2GrammarParser.CellPosContext context)
        {
            try
            {
                string cellPosition = context.GetText();
                if (UsedCells.Contains(cellPosition))
                {
                    var ex = new Exception();
                    ex.Data.Add("Type", "reference loop");
                    throw ex;
                }
                UsedCells.Add(cellPosition);
                int titleLen = 0;
                while (65 <= (int)cellPosition[titleLen] && (int)cellPosition[titleLen] <= 90)
                    titleLen++;
                int colNum = Converter.ColumnTitleToNumber(cellPosition.Substring(0, titleLen)) - 1;
                int rowNum = int.Parse(cellPosition.Substring(titleLen)) - 1;
                CellView cell = Table.GetCell(rowNum, colNum);
                cell.Depended.Add(Evaluating);
                Evaluating.Dependencies.Add(cell);
                int ans = cell.EvaluateExpression(UsedCells);
                UsedCells.Remove(cellPosition);
                console.log($"{cellPosition} {ans}");
                return ans;
            }
            catch
            {
                throw;
            }
        }
        public override int VisitRest([NotNull] Lab2GrammarParser.RestContext context)
        {
            throw new Exception();
        }
        public override int VisitInc([NotNull] Lab2GrammarParser.IncContext context)
        {
            int ans = Visit(context.expr()) + 1;
            console.log($"inc {ans}");
            return ans;
        }
        public override int VisitDec([NotNull] Lab2GrammarParser.DecContext context)
        {
            int ans = Visit(context.expr()) - 1;
            console.log($"dic {ans}");
            return ans;
        }
        private int LeftOperand(Lab2GrammarParser.ExprContext context)
        {
            return Visit(context.GetRuleContext<Lab2GrammarParser.ExprContext>(0));
        }
        private int RightOperand(Lab2GrammarParser.ExprContext context)
        {
            return Visit(context.GetRuleContext<Lab2GrammarParser.ExprContext>(1));
        }
    }
}
