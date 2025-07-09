using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Calculus
{
    public partial class form : Form
    {
        [System.Runtime.InteropServices.DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] val, int size);
        private string temp = String.Empty;
        public form()
        {
            InitializeComponent();
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize"))
            {
                if (key != null)
                {
                    Object o = key.GetValue("AppsUseLightTheme");
                    if (int.Parse(o.ToString()) == 0)
                    {
                        DwmSetWindowAttribute(this.Handle, 20, new[] { 1 }, 4);
                        this.BackColor = Color.FromArgb(255, 25, 25, 25);
                        textBox1.BackColor = Color.FromArgb(255, 19, 19, 19);
                        textBox2.BackColor = Color.FromArgb(255, 19, 19, 19);
                        label1.BackColor = Color.FromArgb(255, 19, 19, 19);
                        toolStrip.BackColor = Color.FromArgb(255, 25, 25, 25);
                        toolStrip.ForeColor = SystemColors.Window;
                        textBox1.ForeColor = SystemColors.Window;
                        label1.ForeColor = SystemColors.Window;
                        textBox2.ForeColor = SystemColors.ControlDark;
                    }
                    else
                    {
                        textBox2.ForeColor = SystemColors.WindowFrame;
                    }
                }
            }
            toolStrip.Renderer = new FixedRenderer();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.TopMost)
            {
                this.TopMost = false;
                toolStripButton3.Text = "Pin";
            }
            else
            {
                this.TopMost = true;
                toolStripButton3.Text = "Unpin";
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "(";
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = ")";
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "7";
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "8";
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "9";
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "rt(2,";
        }
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "^";
        }
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "4";
        }
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "5";
        }
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "6";
        }
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "*";
        }
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "/";
        }
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "1";
        }
        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "2";
        }
        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "3";
        }
        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "-";
        }
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "+";
        }
        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "0";
        }
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = ".";
        }
        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "π";
        }
        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            Solve();
        }
        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "sin(";
        }
        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "cos(";
        }
        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "tan(";
        }
        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "log(10,";
        }
        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "e";
        }
        public static double Evaluate(string ex)
        {
            Stack<double> operands = new Stack<double>();
            Stack<string> operators = new Stack<string>();
            int i = 0;
            while (i < ex.Length)
            {
                if (char.IsWhiteSpace(ex[i]))
                {
                    i++;
                    continue;
                }
                if (char.IsDigit(ex[i]) || ex[i] == '.')
                {
                    string number = String.Empty;
                    while (i < ex.Length && (char.IsDigit(ex[i]) || ex[i] == '.'))
                    {
                        number += ex[i];
                        i++;
                    }
                    operands.Push(double.Parse(number, CultureInfo.GetCultureInfo("en-US")));
                }
                else if (char.IsLetter(ex[i]))
                {
                    string func = String.Empty;
                    while (i < ex.Length && char.IsLetter(ex[i]))
                    {
                        func += ex[i];
                        i++;
                    }
                    operators.Push(func);
                }
                else if (ex[i] == '(')
                {
                    operators.Push("(");
                    i++;
                }
                else if (ex[i] == ',')
                {
                    while (operators.Peek() != "(")
                    {
                        Apply(operands, operators.Pop());
                    }
                    i++;
                }
                else if (ex[i] == ')')
                {
                    while (operators.Peek() != "(")
                    {
                        Apply(operands, operators.Pop());
                    }
                    operators.Pop();
                    if (operators.Count > 0 && (operators.Peek() == "sin" || operators.Peek() == "cos" || operators.Peek() == "tan" || operators.Peek() == "log" || operators.Peek() == "rt"))
                    {
                        Apply(operands, operators.Pop());
                    }

                    i++;
                }
                else if (ex[i] == '+' || ex[i] == '-' || ex[i] == '*' || ex[i] == '/' || ex[i] == '^')
                {
                    string current = ex[i].ToString();
                    while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(current))
                    {
                        Apply(operands, operators.Pop());
                    }
                    operators.Push(current);
                    i++;
                }
                else
                {
                    throw new Exception();
                }
            }
            while (operators.Count > 0)
            {
                Apply(operands, operators.Pop());
            }
            return operands.Pop();
        }
        private static void Apply(Stack<double> operands, string op)
        {
            if (op == "sin" || op == "cos" || op == "tan")
            {
                double b = operands.Pop();
                switch (op)
                {
                    case "sin": operands.Push(Math.Sin(b * Math.PI / 180)); break;
                    case "cos": operands.Push(Math.Cos(b * Math.PI / 180)); break;
                    case "tan": operands.Push(Math.Tan(b * Math.PI / 180)); break;
                    default: throw new Exception();
                }
            }
            else
            {
                double b = operands.Pop();
                double a = operands.Pop();
                switch (op)
                {
                    case "+": operands.Push(a + b); break;
                    case "-": operands.Push(a - b); break;
                    case "*": operands.Push(a * b); break;
                    case "/": operands.Push(a / b); break;
                    case "^": operands.Push(Math.Pow(a, b)); break;
                    case "log": operands.Push(Math.Log(b, a)); break;
                    case "rt": operands.Push(Math.Pow(b, 1 / a)); break;
                    default: throw new Exception();
                }
            }
        }
        private static int Precedence(string op)
        {
            if (op == "+" || op == "-") return 1;
            if (op == "*" || op == "/") return 2;
            if (op == "^") return 3;
            if (op == "sin" || op == "cos" || op == "tan" || op == "log") return 4;
            return 0;
        }
        private void Solve()
        {
            string func = textBox1.Text.Replace("π", Math.PI.ToString(CultureInfo.GetCultureInfo("en-US"))).Replace("e", Math.E.ToString(CultureInfo.GetCultureInfo("en-US"))).Replace(" ", String.Empty);
            for (int i = 0; i < func.Length; i++)
            {
                if (func[i] == '-')
                {
                    if (i == 0 || func[i - 1] == '(')
                    {
                        func = func.Insert(i, "0");
                    }
                }
            }
            try
            {
                double result = Evaluate(func);
                Enter(result.ToString(CultureInfo.GetCultureInfo("en-US")));
            }
            catch (Exception)
            {
                Enter("error");
            }
        }
        private void Enter(string s)
        {
            if (label1.Text == "⏶")
            {
                label1.Text = "⏷";
                (temp, textBox2.Text) = (textBox2.Text, temp);
            }
            if (textBox2.Text != String.Empty)
            {
                textBox2.AppendText(Environment.NewLine + s);
            }
            else
            {
                textBox2.AppendText(s);
            }
            temp = textBox1.Text;
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Solve();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (label1.Text == "⏷")
            {
                label1.Text = "⏶";
                (temp, textBox2.Text) = (textBox2.Text, temp);
            }
            else
            {
                label1.Text = "⏷";
                (temp, textBox2.Text) = (textBox2.Text, temp);
            }
        }
    }
    public class FixedRenderer : ToolStripSystemRenderer
    {
        public FixedRenderer() { }
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
    }
}
