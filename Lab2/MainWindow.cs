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
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label()
            {
                Left = 50,
                Top = 20,
                Text = text,
                Width = 300
            };
            TextBox textBox = new TextBox()
            {
                Left = 50,
                Top = textLabel.Bottom + 5,
                Width = 300
            };
            Button confirmation = new Button()
            {
                Text = "Submit",
                Left = textBox.Right - 115,
                Width = 100,
                Height = 30,
                Top = textBox.Bottom + 10,
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
        MenuStrip MainMenu;
        ToolStripMenuItem Save, Open;
        TextBox ExpressionInCell;
        Panel Toolbar;
        Button AddRow, AddCol, DelRow, DelCol;
        public MainWindow()
        {
            InitializeComponent();
            AddMainMenu();
            AddToolbar();
            AddTable();
            Resize += MainWindow_Resize;
            FormClosed += MainWindow_FormClosed;
        }
        void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (UpToDate) return;
            var isSave = MessageBox.Show("You have unsaved changes, they will be lost if you" +
                " close the application. Save changes?", "Danger", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (isSave == DialogResult.Yes)
            {
                Save.PerformClick();
            }
        }
        void MainWindow_Resize(object sender, EventArgs e)
        {
            Toolbar.Width = ClientSize.Width - 120;
            AddCol.Location = new Point(
                    Math.Max(330, (Toolbar.Width - 270) / 2), 5);
            DelCol.Location = new Point(AddCol.Bounds.Right + 30, 5);
            AddRow.Location = new Point(DelCol.Bounds.Right + 30, 5);
            DelRow.Location = new Point(AddRow.Bounds.Right + 30, 5);
            Table.Size = new Size(ClientSize.Width - 60,
                    ClientSize.Height - Toolbar.Bottom - 60);
        }
        void AddTable(TableView existing = null)
        {
            if (existing == null)
            {
                Table = new TableView(10, 10)
                {
                    Location = new Point(30, Toolbar.Bottom + 30),
                    Size = new Size(ClientSize.Width - 60,
                        ClientSize.Height - Toolbar.Bottom - 60),
                };
            }
            else
            {
                Table.SuspendLayout();
                Controls.Remove(Table);
                existing.Location = new Point(30, Toolbar.Bottom + 30);
                existing.Size = new Size(ClientSize.Width - 60,
                    ClientSize.Height - Toolbar.Bottom - 60);
                Table = existing;
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
                        MessageBox.Show($"Invalid expression: {ex.Data["Type"]}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Invalid expression",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    changed.Expression = oldExpr;
                    Table.Recalculate(changed);
                }
                UpToDate = false;
            };
            Controls.Add(Table);
            Table.ResumeLayout();
        }
        void AddToolbar()
        {
            Toolbar = new Panel()
            {
                Location = new Point(60, MainMenu.Bottom + 30),
                Size = new Size(ClientSize.Width - 120, 40),
            };
            ExpressionInCell = new TextBox()
            {
                Location = new Point(0, 0),
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
                        MessageBox.Show($"Invalid expression: {ex.Data["Type"]}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Invalid expression",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Table.CurCell.Expression = oldExpr;
                    Table.Recalculate(Table.CurCell);
                }
                UpToDate = false;
            };
            Toolbar.Controls.Add(ExpressionInCell);
            AddButtons();
            Controls.Add(Toolbar);
        }
        void AddButtons()
        {
            AddCol = new Button()
            {
                Text = "Add column",
                Location = new Point(
                    Math.Max(330, (Toolbar.Width - 270) / 2), 5),
                Size = new Size(120, 30),
            };
            AddCol.Click += (s, e) => Table.AddColumns(1);

            DelCol = new Button()
            {
                Text = "Delete column",
                Location = new Point(AddCol.Bounds.Right + 30, 5),
                Size = new Size(120, 30),
            };
            DelCol.Click += (s, e) =>
            {
                string input = Prompt.ShowDialog("Enter title of column to delete:", "");
                if (input == "") return;
                try
                {
                    Table.DeleteColumn(input);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Invalid title of column", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Invalid input", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            AddRow = new Button()
            {
                Text = "Add row",
                Location = new Point(DelCol.Bounds.Right + 30, 5),
                Size = new Size(120, 30),
            };
            AddRow.Click += (s, e) => Table.AddRows(1);

            DelRow = new Button()
            {
                Text = "Delete row",
                Location = new Point(AddRow.Bounds.Right + 30, 5),
                Size = new Size(120, 30),
            };
            DelRow.Click += (s, e) =>
            {
                string input = Prompt.ShowDialog("Enter number of column to delete:", "");
                if (input == "") return;
                try
                {
                    int num = Convert.ToInt32(input);
                    Table.DeleteRow(num);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Invalid number of row", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Invalid input", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            Toolbar.Controls.Add(AddCol);
            Toolbar.Controls.Add(DelCol);
            Toolbar.Controls.Add(AddRow);
            Toolbar.Controls.Add(DelRow);
        }
        void AddMainMenu()
        {
            MainMenu = new MenuStrip()
            {
                BackColor = this.BackColor,
                Font = new Font("Times New Roman", 12)
            };
            var file = new ToolStripMenuItem("File");
            Save = new ToolStripMenuItem("Save to");
            Save.Click += (s, e) =>
            {
                var saveTo = new SaveFileDialog()
                {
                    Filter = "Excelerator files (*.xclr)|*.xclr",
                };
                if (saveTo.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveTo.FileName, Table.Serialize());
                }
                UpToDate = true;
            };
            Open = new ToolStripMenuItem("Open from");
            Open.Click += (s, e) =>
            {
                var openFrom = new OpenFileDialog()
                {
                    Filter = "Excelerator files (*.xclr)|*.xclr",
                };
                if (openFrom.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AddTable(
                            TableView.CreateFromSerialized(
                                File.ReadAllText(openFrom.FileName)));
                    }
                    catch
                    {
                        MessageBox.Show($"Impossible to open: {openFrom.FileName} is not valid" +
                            $" .xclr file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
            file.DropDownItems.Add(Save);
            file.DropDownItems.Add(Open);
            MainMenu.Items.Add(file);
            Controls.Add(MainMenu);
        }
    }
}
