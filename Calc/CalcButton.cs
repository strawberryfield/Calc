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

using System.Drawing;
using System.Windows.Forms;

namespace Casasoft.Calc
{
    public partial class CalcButton : UserControl
    {
        public CalcButton()
        {
            InitializeComponent();
        }

        public CalcButton(ButtonDef bd) : this()
        {
            LabelText = bd.AltText;
            ButtonText = bd.Text;
            FunctionCode = bd.FunctionCode;
            SecondFunctionCode = bd.SecondFunctionCode;
            switch (bd.ButtonType)
            {
                case ButtonDef.EButtonType.Normal:
                    ButtonBackColor = Color.SaddleBrown;
                    ButtonForeColor = Color.White;
                    break;
                case ButtonDef.EButtonType.Numeric:
                    ButtonBackColor = Color.White;
                    ButtonForeColor = Color.Black;
                    break;
                case ButtonDef.EButtonType.Operator:
                    ButtonBackColor = Color.Gold;
                    ButtonForeColor = Color.Black;
                    break;
                default:
                    break;
            }
        }

        public string LabelText { get => label.Text; set => label.Text = value; }
        public string ButtonText { get => button.Text; set => button.Text = value; }
        public Color ButtonBackColor { get => button.BackColor; set => button.BackColor = value; }
        public Color ButtonForeColor { get => button.ForeColor; set => button.ForeColor = value; }
        public byte FunctionCode { get; set; }
        public byte SecondFunctionCode { get; set; }
    }
}
