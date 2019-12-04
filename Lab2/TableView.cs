using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Antlr4.Runtime;

namespace Lab2
{
    public class TableView : DataGridView
    {
        int N = 0;
        int M = 0;

        public CellView SelectedCell
        {
            get
            {
                if (SelectedCells.Count == 0) return null;
                return SelectedCells[SelectedCells.Count - 1] as CellView;
            }
        }

        public TableView() : this(0, 0)
        {
        }

        public TableView(int n, int m) : base()
        {
            AllowUserToAddRows = false;
            MultiSelect = false;
            AddColumn(m);
            AddRow(n);
            RowHeadersWidth = 60;
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void AddRow(int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                N++;
                DataGridViewRow row = new DataGridViewRow();
                row.HeaderCell.Value = N.ToString();
                Rows.Add(row);
            }
        }

        public void AddColumn(int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                M++;
                DataGridViewColumn col = new DataGridViewColumn(new CellView());
                col.HeaderCell.Value = Converter.NumberToColumnTitle(M);
                Columns.Add(col);
            }
        }

        private void ResetDepended(CellView cur)
        {
            cur.Value = cur.Expression = "";
            foreach (var d in cur.Depended)
            {
                ResetDepended(d);
            }
            cur.Depended.Clear();
            cur.Dependencies.Clear();
        }

        public void DeleteRow(int idx)
        {
            idx--;
            bool canDelete = true;
            for (int i = 0; i < M; i++)
            {
                var cell = GetCell(idx, i);
                foreach (var d in cell.Depended)
                {
                    if (d.RowIndex == idx) continue;
                    canDelete = false;
                    break;
                }
            }
            if (canDelete)
            {
                ShiftRowTitles(idx);
                Rows.RemoveAt(idx);
                N--;
                return;
            }
            var isDelete = MessageBox.Show("If you delete this row, some cells" +
                " will be cleared. Delete this row?", "Danger",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (isDelete == DialogResult.No) return;
            try
            {
                for (int i = 0; i < M; i++)
                {
                    var cell = GetCell(idx, i);
                    ResetDepended(cell);
                }
                ShiftRowTitles(idx);
                Rows.RemoveAt(idx);
                N--;
            }
            catch (Exception ex)
            {
                console.log($"in DelRow: {ex.Message}");
            }
        }

        private void ShiftRowTitles(int to)
        {
            for (int i = N - 1; i > to; i--)
            {
                Rows[i].HeaderCell.Value = Rows[i - 1].HeaderCell.Value;
            }
        }

        public void DeleteColumn(string title)
        {
            int idx = Converter.ColumnTitleToNumber(title);
            idx--;
            bool canDelete = true;
            for (int i = 0; i < N; i++)
            {
                var cell = GetCell(i, idx);
                foreach (var d in cell.Depended)
                {
                    if (d.ColumnIndex == idx) continue;
                    canDelete = false;
                    break;
                }
            }
            if (canDelete)
            {
                ShiftColTitles(idx);
                Columns.RemoveAt(idx);
                M--;
                return;
            }
            var isDelete = MessageBox.Show("If you delete this column, some cells" +
                " will be cleared. Delete this column?", "Danger",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (isDelete == DialogResult.No) return;
            try
            {
                for (int i = 0; i < N; i++)
                {
                    var cell = GetCell(i, idx);
                    ResetDepended(cell);
                }
                ShiftColTitles(idx);
                Columns.RemoveAt(idx);
                M--;
            }
            catch (Exception ex)
            {
                console.log($"in DelCol: {ex.Message}");
            }
        }

        private void ShiftColTitles(int to)
        {
            for (int i = M - 1; i > to; i--)
            {
                Columns[i].HeaderCell.Value = Columns[i - 1].HeaderCell.Value;
            }
        }

        public CellView GetCell(int r, int c)
        {
            return Rows[r].Cells[c] as CellView;
        }

        private void ResetRecalculated()
        {
            foreach (DataGridViewRow r in Rows)
            {
                foreach (CellView c in r.Cells)
                {
                    c.Recalculated = false;
                }
            }
        }

        public string GetExpressionInCell(int r, int c)
        {
            if (r < 0 || c < 0) throw new IndexOutOfRangeException();
            return (Rows[r].Cells[c] as CellView).Expression;
        }

        public TableView Clone()
        {
            TableView clone = new TableView(N, M);
            for (int i = 0; i < Rows.Count; i++)
            {
                int Index = 0;
                foreach (CellView cell in Rows[i].Cells)
                {
                    clone.Rows[i].Cells[Index] = cell.Clone() as CellView;
                    Index++;
                }
            }
            return clone;
        }

        public string Serialize()
        {
            string data = $"{N} {M}\n";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    data += GetCell(i, j).Expression + '\n';
                }
            }
            return data;
        }

        public static TableView CreateFromSerialized(string data)
        {
            try
            {
                var reader = new StringReader(data);
                var sizes = reader.ReadLine();
                var n = int.Parse(sizes.Substring(0, sizes.IndexOf(' ')));
                var m = int.Parse(sizes.Substring(sizes.IndexOf(' ') + 1));
                console.log($"created table with sizes {n} and {m}");
                TableView table = new TableView(n, m);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        table.GetCell(i, j).Expression = reader.ReadLine();
                        console.log($"in cell ({i},{j}) now {table.GetCell(i, j).Expression}");
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        table.Recalculate(table.GetCell(i, j));
                        console.log($"value of ({i},{j}) now {table.GetCell(i, j).Value}");
                    }
                }
                return table;
            }
            catch
            {
                throw;
            }
        }

        private void CalculateCell(CellView cell)
        {
            try
            {
                var inputStream = new AntlrInputStream(cell.Expression);
                var lexer = new Lab2GrammarLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(lexer);
                var parser = new Lab2GrammarParser(commonTokenStream);
                parser.RemoveErrorListeners();
                parser.AddErrorListener(new MyParsErrListener());
                var expr = parser.unit();
                cell.Value =
                    (new AntlrVisitor(this, new HashSet<string>(), cell))
                    .Visit(expr);
            }
            catch
            {
                throw;
            }
            cell.Recalculated = true;
            foreach (var dep in cell.Depended)
            {
                CalculateCell(dep);
            }
        }

        public void Recalculate(CellView changed)
        {
            ResetRecalculated();
            foreach (var d in changed.Dependencies)
            {
                d.Depended.Remove(changed);
            }
            changed.Dependencies.Clear();
            if (string.IsNullOrWhiteSpace(changed.Expression))
            {
                ResetDepended(changed);
                return;
            }
            try
            {
                CalculateCell(changed);
            }
            catch
            {
                throw;
            }
        }
    }
}
