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

namespace Casasoft.Calc
{
    public class ButtonDef
    {
        public string Text { get; set; }
        public string AltText { get; set; }
        public byte FunctionCode { get; set; }
        public byte SecondFunctionCode { get; set; }
        public enum EButtonType { Normal, Numeric, Operator }
        public EButtonType ButtonType { get; set; }
        public int NButtonType { get => (int)ButtonType; }

        public ButtonDef(string text, string altText, byte fcode, byte f2code, EButtonType type)
        {
            Text = text;
            AltText = altText;
            FunctionCode = fcode;
            SecondFunctionCode = f2code;
            ButtonType = type;
        }

        public ButtonDef(string text, string altText, byte fcode) :
            this(text, altText, fcode, Convert.ToByte(fcode % 10 == 5 ? fcode - 5 : fcode + 5), EButtonType.Normal)
        { }
    }
}
