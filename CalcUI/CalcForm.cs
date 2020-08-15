// copyright (c) 2020 Roberto Ceccarelli - CasaSoft
// http://strawberryfield.altervista.org 
// 
// This file is part of CasaSoft Calc
// 
// CasaSoft Calc is free software: 
// you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// CasaSoft Calc is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with CasaSoft Calc.  
// If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Casasoft.Calc
{
    [Guid("E20AFEE4-B920-42C4-ADC9-F97246BB552C")]
    [ProgId("Casasoft.Calc")]
    public partial class CalcForm : Form
    {
        public CalcEngine CalcEngine { get; private set; }
        private bool SecondFunction;

        public CalcForm()
        {
            InitializeComponent();
            CalcEngine = new CalcEngine();
            txtDisplay.Text = CalcEngine.Display.GetText();
            SecondFunction = false;
            makeButtons();
        }

        private void makeButtons()
        {
            ButtonsList buttons = new ButtonsList();

            for (int row = 0; row < 9; ++row)
            {
                for (int col = 0; col < 5; ++col)
                {
                    CalcButton b = new CalcButton(buttons.ButtonDefs[row][col]);
                    b.Top = 65 + row * 36;
                    b.Left = 12 + col * 51;
                    Button button = (Button)b.Controls["button"];
                    button.Click += new EventHandler(button_Click);
                    Controls.Add(b);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button ib = (Button)sender;
            CalcButton b = (CalcButton)ib.Parent;

            if (b.FunctionCode == 21)
            {
                SecondFunction = !SecondFunction;
            }
            else if (SecondFunction)
            {
                CommonClickHandler(b.SecondFunctionCode);
            }
            else
            {
                CommonClickHandler(b.FunctionCode);
            }
        }


        private void CommonClickHandler(int key)
        {
            CalcEngine.EnterKey(Convert.ToByte(key));
            txtDisplay.Text = CalcEngine.GetDisplayString();
            if (key != 22) SecondFunction = false;
        }

        private void CalcForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                // Numbers
                case '0':
                    CommonClickHandler(0);
                    break;
                case '1':
                    CommonClickHandler(1);
                    break;
                case '2':
                    CommonClickHandler(2);
                    break;
                case '3':
                    CommonClickHandler(3);
                    break;
                case '4':
                    CommonClickHandler(4);
                    break;
                case '5':
                    CommonClickHandler(5);
                    break;
                case '6':
                    CommonClickHandler(6);
                    break;
                case '7':
                    CommonClickHandler(7);
                    break;
                case '8':
                    CommonClickHandler(8);
                    break;
                case '9':
                    CommonClickHandler(9);
                    break;

                // Operators
                case '+':
                    CommonClickHandler(85);
                    break;
                case '-':
                    CommonClickHandler(75);
                    break;
                case '*':
                    CommonClickHandler(65);
                    break;
                case '/':
                    CommonClickHandler(55);
                    break;
                case '^':
                    CommonClickHandler(45);
                    break;
                case '=':
                    CommonClickHandler(95);
                    break;
                case '(':
                    CommonClickHandler(53);
                    break;
                case ')':
                    CommonClickHandler(54);
                    break;

                // User defined keys
                case 'a':
                    CommonClickHandler(11);
                    break;
                case 'b':
                    CommonClickHandler(12);
                    break;
                case 'c':
                    CommonClickHandler(13);
                    break;
                case 'd':
                    CommonClickHandler(14);
                    break;
                case 'e':
                    CommonClickHandler(15);
                    break;
                case 'A':
                    CommonClickHandler(16);
                    break;
                case 'B':
                    CommonClickHandler(17);
                    break;
                case 'C':
                    CommonClickHandler(18);
                    break;
                case 'D':
                    CommonClickHandler(19);
                    break;
                case 'E':
                    CommonClickHandler(10);
                    break;

                default:
                    break;
            }
        }


    }

}
