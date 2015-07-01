using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Expression
{
    public class Expression
    {
        Stack<char> theStack;       
        List<string> postExpession;        
        public Expression()
        {
           this.theStack = new Stack<char>();
           this.postExpession = new List<string>();
        }

        public double CalcExpression(string expresion, double x, double y)
        {            
            DoTrans(expresion);
            return Calc(x,y);
        }

        private double Calc(double x,double y)
        {
            Stack<double> stack = new Stack<double>();
            double num1, num2, res = 0;
            foreach (string ch in postExpession)
            {
                double temp;
                if (ch == "x" || ch == "X")
                {
                    stack.Push(x);
                    continue;
                }
                if (ch == "y" || ch == "Y")
                {
                    stack.Push(y);
                    continue;
                }
                if (Double.TryParse(ch, out temp))
                    stack.Push(temp);
                else
                {
                    num2 = stack.Pop();
                    num1 = stack.Pop();
                    switch (ch)
                    {
                        case "+":
                            res = num1 + num2;
                            break;
                        case "-":
                            res = num1 - num2;
                            break;
                        case "*":
                            res = num1 * num2;
                            break;
                        case "/":
                            res = num1 / num2;
                            break;                            
                    }
                    stack.Push(res);
                }                    
            }
            return stack.Pop();
        }

        private void DoTrans(string inputString)
        {
            this.postExpession.Clear();
            this.theStack.Clear();
            for (int i = 0; i<inputString.Length; i++)
            {
                char ch = inputString[i];
                switch (ch)
                {
                    case ' ':
                        break;
                    case '+':
                    case '-':
                        GotOper(ch, 1);
                        break;
                    case '*':
                    case '/':
                        GotOper(ch, 2);
                        break;
                    case '(':
                        theStack.Push(ch);
                        break;
                    case ')':
                        GotParen(ch);
                        break;                  
                                           
                    default:
                        if (ch >= '0' && ch <= '9')
                        {
                            string tempNumber = "";
                            while (ch >= '0' && ch <= '9')
                            {
                                tempNumber += ch;
                                if (++i < inputString.Length)
                                    ch = inputString[i];
                                else break;
                            }
                            i--;
                            postExpession.Add(tempNumber);
                        }
                        else
                        {
                            postExpession.Add(ch.ToString());
                        }
                        break;
                }
            }
            while (theStack.Count != 0)
            {
                postExpession.Add(theStack.Pop().ToString());
            }            
        }

        private void GotOper(char op, int prec)
        {
            while (theStack.Count != 0)
            {
                char opTop = theStack.Pop();
                if (opTop == '(')
                {
                    theStack.Push('(');
                    break;
                }
                else 
                {
                    int prec2;
                    if (opTop == '+' || opTop == '-')
                        prec2 = 1;
                    else
                        prec2 = 2;
                    if (prec2 < prec)
                    {
                        theStack.Push(opTop);
                        break;
                    }
                    else
                        postExpession.Add(opTop.ToString());
                }
            }
            theStack.Push(op);
        }

        private void GotParen(char ch)
        {
            while (theStack.Count != 0)
            {
                char chx = theStack.Pop();
                if (chx == '(')
                    break;
                else
                    postExpession.Add(chx.ToString());
            }
        }
    }
}
