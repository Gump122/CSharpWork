using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test02
{
    public partial class Form1 : Form
    {
        bool isAppend; //控制数字输入
        string preOper = null; //前一位运算符
        double result; //运算结果
        bool Useequal = false; //判断等号是否使用
        bool Usedot = false; //判断小数点是否使用
        bool UseNegative = false; //判断负数符号是否使用

        //textbox1为输入框
        //textbox2为运算式显示框

        public Form1() //主窗体
        {
            InitializeComponent();
        }

        private void NumClicked(object sender, EventArgs e) //数字按钮click函数
        {

            string num = ((Button)sender).Text;
            Textdisplay(num);
            NumInput(num);

        }

        private void Textdisplay(string num)   //第一行的运算式的显示函数
        {
            this.textBox2.Text += num;
        }

        private void NumInput(string num)   //数字输入
        {
            if (Useequal == true)
            {
                this.textBox2.Text = num;
                Useequal = false;
            }
            if (isAppend)
            {
                this.textBox1.Text += num;
            }
            else
            {
                this.textBox1.Text = num;
            }


            isAppend = true;
            this.textBox1.Focus();
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
        }

        private void DoubleClicked(object sender, EventArgs e)  //双目运算符click函数
        {
            string curOper = ((Button)sender).Text;
            if (this.textBox2.Text.Last() == '+' || this.textBox2.Text.Last() == '-' ||
                this.textBox2.Text.Last() == '*' || this.textBox2.Text.Last() == '/')
            {
                this.textBox2.Text = this.textBox2.Text.Remove(this.textBox2.Text.Length - 1, 1) + curOper;
                preOper = curOper;
            }
            else
            {
                Textdisplay(curOper);
                DoubleOperInput(curOper);
            }

            this.textBox1.Focus();
        }

        private void DoubleOperInput(string curOper)     //双目运算符输入
        {
            if (Useequal == true)
                Useequal = false;
            if (preOper == null)
            {
                preOper = curOper;
                this.result = double.Parse(this.textBox1.Text);
            }
            else
            {
                double curNum = double.Parse(this.textBox1.Text);
                switch (preOper)
                {
                    case "+": this.result += curNum; break;
                    case "-": this.result -= curNum; break;
                    case "*": this.result *= curNum; break;
                    case "/": this.result /= curNum; break;

                }
                this.textBox1.Text = result.ToString();
                preOper = curOper;
            }
            isAppend = false;
            Usedot = false;
            UseNegative = false;
        }

        private void SingleClick(object sender, EventArgs e)   单目运算符click函数
        {
            if (Useequal == true)
                Useequal = false;
            double curNum = double.Parse(this.textBox1.Text);
        string curOper = ((Button)sender).Text;
        Textdisplay(curOper);
            switch (curOper)
            {
                case "sqrt": curNum=Math.Sqrt(curNum); break;
                case "1/x": curNum=1/curNum; break;
                case "x^2": curNum=curNum* curNum; break;
                case "sin": curNum = Math.Sin(Math.PI* (curNum / 180)); break;

            }
            this.textBox1.Text = curNum.ToString();
            Usedot = false;
            UseNegative = false;
            isAppend = false;
            this.textBox1.Focus();
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
        }

    private void DotClick(object sender, EventArgs e)    //小数点click函数
    {
        string curOper = ((Button)sender).Text;
        DotInput(curOper);
    }

    private void DotInput(string curOper)       //小数点输入
    {
        if (Usedot == false)
        {
            if (Useequal == true)
            {
                this.textBox2.Text = "0" + curOper;
                this.textBox1.Text = "0" + curOper;
                Useequal = false;
                isAppend = true;
            }
            else if (this.textBox1.Text == "")
            {
                this.textBox2.Text = "0" + curOper;
                this.textBox1.Text = "0" + curOper;
                isAppend = true;
            }
            else if (!char.IsDigit(this.textBox2.Text.Last()))
            {
                this.textBox2.Text += "0" + curOper;
                this.textBox1.Text = "0" + curOper;
                isAppend = true;
            }
            else
            {
                this.textBox1.Text += curOper;
                this.textBox2.Text += curOper;
            }
            Usedot = true;
        }

        this.textBox1.Focus();
    }

    private void EqualClick(object sender, EventArgs e)   //等于按钮click函数
    {
        DoubleClicked(btnEquals, null);
        this.textBox2.Text = result.ToString();
        preOper = null;
        Useequal = true;
        Usedot = false;
        UseNegative = false;
        this.textBox1.Focus();
    }

    private void BackClick(object sender, EventArgs e)   //退格click函数
    {
        if (isAppend && this.textBox1.Text != "")
        {
            this.textBox1.Text = this.textBox1.Text.Remove(this.textBox1.Text.Length - 1, 1);
            this.textBox2.Text = this.textBox2.Text.Remove(this.textBox2.Text.Length - 1, 1);
        }
        this.textBox1.Focus();
    }

    private void ClearClick(object sender, EventArgs e)  //清除
    {
        this.textBox1.Text = "";
        this.textBox2.Text = "";
        preOper = null;
        Usedot = false;
        Useequal = false;
        UseNegative = false;
        this.textBox1.Focus();
    }


    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)  //键盘输入
    {
        e.Handled = true;
        if (char.IsDigit(e.KeyChar))
        {
            Textdisplay(e.KeyChar.ToString());
            NumInput(e.KeyChar.ToString());
        }
        else if (e.KeyChar == '.')
        {
            DotInput(e.KeyChar.ToString());
        }
        else if (e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar == '*' || e.KeyChar == '/')
        {
            Textdisplay(e.KeyChar.ToString());
            DoubleOperInput(e.KeyChar.ToString());
        }
        else if (e.KeyChar == 8)  //退格符的ascii码为8
        {
            BackClick(null, null);
        }
        else if (e.KeyChar == 13)  //等于符的ascii码为13
        {
            EqualClick(null, null);
        }
        this.textBox1.SelectionStart = this.textBox1.Text.Length;
    }

    private void Negative(object sender, EventArgs e)  //负数click函数
    {
        if (UseNegative == false)
        {
            if (this.textBox1.Text == "")
            {
                this.textBox1.Text = "-";
                this.textBox2.Text = "(-)";
                isAppend = true;
            }
            else if (Useequal == true)
            {
                this.textBox1.Text = "-";
                this.textBox2.Text = "(-)";
                Useequal = false;
                isAppend = true;
            }
            else if (!char.IsDigit(this.textBox2.Text.Last()))
            {
                this.textBox2.Text += "(-)";
                this.textBox1.Text = "-";
                isAppend = true;
            }

        }

        UseNegative = true;
    }
}
}
