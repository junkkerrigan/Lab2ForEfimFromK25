using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4;
using Antlr4.Runtime;

namespace Lab2
{
    public static class console
    {
        public static void log(object obj)
        {
            Debug.WriteLine(obj);
        }
    }
    // mod div
    // inc dec
    // power
    // java -jar antlr-4.7.2-complete.jar -Dlanguage=CSharp.\Lab2Grammar.g4 -visitor 
    public static class Dialog
    {
        public static string Show(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 600,
                Height = 400,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label()
            {
                Left = 70,
                Top = 90,
                Text = text,
                Width = 400
            };
            RichTextBox textBox = new RichTextBox()
            {
                Left = 70,
                Top = textLabel.Bottom + 30,
                Width = 400,
                Height = 40,
                Font = new Font("Times New Roman", 16),
            };
            Button confirmation = new Button()
            {
                Text = "Submit",
                Left = textBox.Right - 110,
                Width = 90,
                Height = 40,
                Top = textBox.Bottom + 30,
                DialogResult = DialogResult.OK
            };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }

    public static class Sys26
    {
        public static string NumToSys26(int num)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string title = "";
            //if (num <= 0) throw new ArgumentException();
            while (num > 0)
            {
                if (num % 26 == 0)
                {
                    num /= 26;
                    num--;
                    title = "Z" + title;
                }
                else
                {
                    title = alphabet[num % 26 - 1] + title;
                    num /= 26;
                }
            }
            return title;
        }
        public static int Sys26ToNum(string inSys26)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int num = 0, pow = 1;
            for (int i = inSys26.Length - 1; i >= 0; i--)
            {
                num += (alphabet.IndexOf(inSys26[i]) + 1) * pow;
                pow *= 26;
            }
            return num;
        }
    }

    public partial class MainWindow : Form
    {
        public TableView Table { get; set; }
        bool UpToDate = true;
        TextBox ExpressionInCell;
        Panel MenuPanel;
        Label Rows, Cols;
        Button AddRow, AddCol, DelRow, DelCol, Save, Open;
        public MainWindow()
        {
            InitializeComponent();
            AddMenuPanel();
            AddTable();
            Resize += MainWindow_SizeChanged;
            FormClosing += MainWindow_FormClosing;
        }
        void AddMenuPanel()
        {
            MenuPanel = new Panel()
            {
                Location = new Point(60, 60),
                Size = new Size(ClientSize.Width - 120, 80),
            };
            AddFileButtons();
            ExpressionInCell = new TextBox()
            {
                Location = new Point(Open.Right + 50, 0),
                Font = new Font("Times New Roman", 16),
                Width = 300,
            };
            ExpressionInCell.Leave += (s, e) =>
            {
                if (Table.CurCell == null) return;
                string oldExpr = Table.CurCell.Expression;
                Table.CurCell.Expression = ExpressionInCell.Text;
                try
                {
                    Table.Recalculate(Table.CurCell);
                }
                catch (Exception ex)
                {
                    if (ex.Data.Contains("Type"))
                    {
                        MessageBox.Show($"Bad expression: {ex.Data["Type"]}", "Error");
                    }
                    else
                    {
                        MessageBox.Show($"Bad expression", "Error");
                    }
                    Table.CurCell.Expression = oldExpr;
                    Table.Recalculate(Table.CurCell);
                }
                UpToDate = false;
            };
            MenuPanel.Controls.Add(ExpressionInCell);
            AddTableButtons();
            Controls.Add(MenuPanel);
        }
        void AddTable(TableView table = null)
        {
            if (table == null)
            {
                Table = new TableView(30, 20)
                {
                    Location = new Point(30, MenuPanel.Bottom + 30),
                    Size = new Size(ClientSize.Width - 60,
                        ClientSize.Height - MenuPanel.Bottom - 60),
                };
            }
            else
            {
                Table.SuspendLayout();
                Controls.Remove(Table);
                table.Location = new Point(30, MenuPanel.Bottom + 30);
                table.Size = new Size(ClientSize.Width - 60,
                    ClientSize.Height - MenuPanel.Bottom - 60);
                Table = table;
            }

            Table.CellEnter += (s, e) =>
            {
                CellView selected = Table.Cell(e.RowIndex, e.ColumnIndex) as CellView;
                ExpressionInCell.Text = Table.Cell(e.RowIndex, e.ColumnIndex).Expression;
            };
            Table.CellBeginEdit += (s, e) =>
            {
                try
                {
                    Table.CurCell.Value = ExpressionInCell.Text =
                        Table.Cell(e.RowIndex, e.ColumnIndex).Expression;
                }
                catch (IndexOutOfRangeException)
                {
                }
            };
            Table.CellEndEdit += (s, e) =>
            {
                CellView changed = Table.Cell(e.RowIndex, e.ColumnIndex);
                string oldExpr = changed.Expression;
                changed.Expression = (string)changed.Value;
                try
                {
                    Table.Recalculate(changed);
                }
                catch (Exception ex)
                {
                    if (ex.Data.Contains("Type"))
                    {
                        MessageBox.Show($"Bad expression: {ex.Data["Type"]}", "Error");

                    }
                    else
                    {
                        MessageBox.Show($"Bad expression", "Error");

                    }
                    changed.Expression = oldExpr;
                    Table.Recalculate(changed);
                }
                UpToDate = false;
            };
            Controls.Add(Table);
            Table.ResumeLayout();
        }
        void AddFileButtons()
        {
            Save = new Button()
            {
                Location = new Point(0, 5),
                Size = new Size(80, 30),
                Text = "Save",
                Font = new Font("Times New Roman", 12),
            };
            Save.Click += (s, e) =>
            {
                var saveTo = new SaveFileDialog()
                {
                    Filter = "Text files (*.txt)|*.txt",
                };
                if (saveTo.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveTo.FileName, Table.ToFile());
                }
                UpToDate = true;
            };
            Open = new Button()
            {
                Location = new Point(Save.Right + 15, 5),
                Size = new Size(80, 30),
                Text = "Open",
                Font = new Font("Times New Roman", 12),
            };
            Open.Click += (s, e) =>
            {
                var openFrom = new OpenFileDialog()
                {
                    Filter = "Text files (*.txt)|*.txt",
                };
                if (openFrom.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AddTable(
                            TableView.FillFromFile(
                                File.ReadAllText(openFrom.FileName)));
                    }
                    catch
                    {
                        MessageBox.Show($"Bad file: {openFrom.FileName}", "Error");
                    }
                }
            };
            MenuPanel.Controls.Add(Save);
            MenuPanel.Controls.Add(Open);
        }
        void AddTableButtons()
        {
            Rows = new Label()
            {
                Location = new Point(
                    Math.Max(580, (MenuPanel.Width - 300) / 2), 10),
                Text = "Rows:",
                Font = new Font("Times New Roman", 12),
                Size = new Size(70, 30),
            };
            AddRow = new Button()
            {
                Text = "+",
                Location = new Point(Rows.Right, 5),
                Size = new Size(30, 30),
            };
            AddRow.Click += (s, e) => Table.AddRows(1);
            DelRow = new Button()
            {
                Text = "-",
                Location = new Point(AddRow.Right + 10, 5),
                Size = new Size(30, 30),
            };
            DelRow.Click += (s, e) =>
            {
                string input = Dialog.Show("Enter number of row to delete:", "");
                if (input == "") return;
                try
                {
                    int num = Convert.ToInt32(input);
                    Table.DeleteRow(num);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Bad number of row", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Bad input", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            Cols = new Label()
            {
                Location = new Point(DelRow.Right + 40, 10),
                Text = "Columns:",
                Font = new Font("Times New Roman", 12),
                Size = new Size(100, 30),
            };
            AddCol = new Button()
            {
                Text = "+",
                Location = new Point(Cols.Right, 5),
                Size = new Size(30, 30),
            };
            AddCol.Click += (s, e) => Table.AddColumns(1);
            DelCol = new Button()
            {
                Text = "-",
                Location = new Point(AddCol.Right + 10, 5),
                Size = new Size(30, 30),
            };
            DelCol.Click += (s, e) =>
            {
                string input = Dialog.Show("Enter name of column to delete:", "");
                if (input == "") return;
                try
                {
                    Table.DeleteColumn(input);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Bad name of column", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Bad input", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            MenuPanel.Controls.Add(Rows);
            MenuPanel.Controls.Add(AddRow);
            MenuPanel.Controls.Add(DelRow);
            MenuPanel.Controls.Add(Cols);
            MenuPanel.Controls.Add(AddCol);
            MenuPanel.Controls.Add(DelCol);
        }
        void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            MenuPanel.Width = ClientSize.Width - 120;
            Rows.Location = new Point(Math.Max(330, (MenuPanel.Width - 300) / 2), 10);
            AddRow.Location = new Point(Rows.Right, 5);
            DelRow.Location = new Point(AddRow.Right + 10, 5);
            Cols.Location = new Point(DelRow.Right + 40, 10);
            AddCol.Location = new Point(Cols.Right, 5);
            DelCol.Location = new Point(AddCol.Right + 10, 5);
            Table.Size = new Size(ClientSize.Width - 60,
                    ClientSize.Height - MenuPanel.Bottom - 60);
        }
        void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UpToDate) return;
            var isSave = MessageBox.Show("Save changes?", "", MessageBoxButtons.YesNoCancel);
            if (isSave == DialogResult.Yes)
            {
                Save.PerformClick();
            }
            else if (isSave == DialogResult.Cancel) e.Cancel = true;
        }
    }
}
