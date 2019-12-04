using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Antlr4.Runtime;

namespace Lab2
{
    public class CellView : DataGridViewTextBoxCell
    {
        public string Expression { get; set; } = "";

        public bool Recalculated { get; set; } = false;

        public HashSet<CellView> Depended { get; set; } = new HashSet<CellView>();

        public HashSet<CellView> Dependencies { get; set; } = new HashSet<CellView>();

        public CellView() : base()
        {
        }

        public int EvaluateExpression(HashSet<string> usedCells)
        {
            if (Expression == "")
            {
                var ex = new Exception();
                ex.Data.Add("Type", "reference to empty cell");
                throw ex;
            }
            if (Recalculated)
            {
                return (int)Value;
            }
            try
            {
                var inputStream = new AntlrInputStream(Expression);
                var lexer = new Lab2GrammarLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(lexer);
                var parser = new Lab2GrammarParser(commonTokenStream);
                parser.RemoveErrorListeners();
                parser.AddErrorListener(new MyParsErrListener());
                var expr = parser.unit();
                int val =
                    (new AntlrVisitor(DataGridView as TableView, usedCells, this))
                    .Visit(expr);
                Recalculated = true;
                return val;
            }
            catch
            {
                throw;
            }
        }

        public override object Clone()
        {
            var clone = base.Clone() as CellView;
            clone.Expression = Expression;
            return clone;
        }
    }
}
