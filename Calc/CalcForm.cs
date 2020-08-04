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
using System.Windows.Forms;

namespace Casasoft.Calc
{
    public partial class CalcForm : Form
    {
        private Calc CalcEngine;
        private bool SecondFunction;

        public CalcForm()
        {
            InitializeComponent();
            CalcEngine = new Calc();
            txtDisplay.Text = CalcEngine.Display.GetText();
            SecondFunction = false;
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

        #region click handlers
        private void CommonClickHandler(int key)
        {
            CalcEngine.EnterKey(Convert.ToByte(key));
            txtDisplay.Text = CalcEngine.Display.GetText();
            if (key != 22) SecondFunction = false;
        }

        private void btn2nd_Click(object sender, EventArgs e)
        {
            SecondFunction = !SecondFunction;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CommonClickHandler(25);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 87 : 1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 88 : 2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 89 : 3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 77 : 4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 78 : 5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 79 : 6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 67 : 7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 68 : 8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 69 : 9);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 97 : 0);
        }

        private void btnCHS_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 99 : 94);
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 98 : 93);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 90 : 95);
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 80 : 85);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 70 : 75);
        }

        private void btnTimes_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 60 : 65);
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 50 : 55);
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 29 : 24);
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 40 : 45);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 58 : 53);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 59 : 54);
        }

        private void btnINV_Click(object sender, EventArgs e)
        {
            CommonClickHandler(22);
        }

        private void btnLn_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 28 : 23);
        }


        private void btnA_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 11 : 16);
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 12 : 17);
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 13 : 18);
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 14 : 19);
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 15 : 10);
        }

        private void btnLRN_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 36 : 31);
        }

        private void btnXT_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 37 : 32);
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 38 : 33);
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 39 : 34);
        }

        private void btnInverse_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 30 : 35);
        }

        private void btnEE_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 57 : 52);
        }

        private void btnlSTO_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 47 : 42);
        }

        private void btnRCL_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 48 : 43);
        }

        private void btnSUM_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 49 : 44);
        }

        private void btnSST_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 46 : 41);
        }

        private void btnBST_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 56 : 51);
        }

        private void btnGTO_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 66 : 61);
        }

        private void btnSBR_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 76 : 71);
        }

        private void btnRST_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 86 : 81);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 96 : 91);
        }
        #endregion

    }
}
