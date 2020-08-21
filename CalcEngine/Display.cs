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
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Casasoft.Calc
{
    [Guid("5510AF94-04A9-4500-8948-41271BC1FF58"),
        ClassInterface(ClassInterfaceType.None),
        ProgId("Casasoft.CalcDisplay")]
    public class Display : IDisplay
    {
        private string currentText;
        private double currentValue;
        internal int fix;

        private enum EEmode { None, EE, Eng }
        EEmode Emode;

        public Display()
        {
            Clear();
            fix = -1;
            Emode = EEmode.None;
        }

        public void Clear()
        {
            currentText = string.Empty;
            currentValue = 0;
        }

        public void AutoClear()
        {
            currentText = string.Empty;
        }

        public void SetValue(double v)
        {
            currentText = string.Empty;
            currentValue = v;
        }

        public double GetValue() => currentValue;

        public string GetText()
        {
            string format = "{0}";
            if (fix >= 0)
            {
                format = "{0:0." + new string('0', fix) + "}";
            }

            return string.Format(CultureInfo.InvariantCulture, format, currentValue);
        }

        public void EnterKey(byte key) => EnterKey(key, 0);
        public void EnterKey(byte key, byte par)
        {
            switch (key)
            {
                case 24:
                    Clear();
                    break;
                case 94:  // change sign
                    if (!string.IsNullOrWhiteSpace(currentText) && currentText.Substring(0, 1) != "-")
                    {
                        currentText = currentText.Substring(1);
                    }
                    else
                    {
                        currentText = "-" + currentText;
                    }
                    currentValue = -currentValue;
                    break;
                case 93:  // dot
                    if (!currentText.Contains('.'))
                    {
                        currentText += '.';
                    }
                    break;
                case 58:  // Fix
                    fix = par > 8 ? -1 : par;
                    break;
                case 158: // INV Fix
                    fix = -1;
                    break;
                default:
                    if (key <= 9 && (key != 0 || !string.IsNullOrWhiteSpace(currentText)))
                    {
                        currentText += $"{key}";
                    }
                    currentValue = string.IsNullOrWhiteSpace(currentText) ?
                        0 : Convert.ToDouble(currentText, CultureInfo.InvariantCulture);
                    break;
            }
        }

    }
}
