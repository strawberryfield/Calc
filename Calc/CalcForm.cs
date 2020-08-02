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
            CommonClickHandler(1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            CommonClickHandler(2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            CommonClickHandler(3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            CommonClickHandler(4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            CommonClickHandler(5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            CommonClickHandler(6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            CommonClickHandler(7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            CommonClickHandler(8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            CommonClickHandler(9);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            CommonClickHandler(0);
        }

        private void btnCHS_Click(object sender, EventArgs e)
        {
            CommonClickHandler(94);
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            CommonClickHandler(93);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            CommonClickHandler(95);
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            CommonClickHandler(85);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            CommonClickHandler(75);
        }

        private void btnTimes_Click(object sender, EventArgs e)
        {
            CommonClickHandler(65);
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            CommonClickHandler(55);
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            CommonClickHandler(24);
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            CommonClickHandler(45);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            CommonClickHandler(53);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CommonClickHandler(54);
        }

        private void btnINV_Click(object sender, EventArgs e)
        {
            CommonClickHandler(22);
        }

        private void btnLn_Click(object sender, EventArgs e)
        {
            CommonClickHandler(SecondFunction ? 28 : 23);
        }
    }
}
