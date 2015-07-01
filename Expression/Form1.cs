using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Expression
{
    public partial class Form1 : Form
    {
        Expression expression;
        public Form1()
        {
            InitializeComponent();
            expression = new Expression();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            tbResult.Text = expression.CalcExpression(tbFunction.Text, Double.Parse(tbX.Text), Double.Parse(tbY.Text)).ToString();
        }
    }
}
