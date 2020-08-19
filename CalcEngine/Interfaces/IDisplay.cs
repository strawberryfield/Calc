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

namespace Casasoft.Calc
{
    [Guid("6850B9D4-97E5-4FBA-8C8E-A07EECBC11DD")]
    public interface IDisplay
    {
        /// <summary>
        /// Clears the display
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears the display on next numeric key pressed
        /// </summary>
        void AutoClear();

        /// <summary>
        /// Sets the display value
        /// </summary>
        /// <param name="v"></param>
        void SetValue(double v);

        /// <summary>
        /// Gets numeric value of the display
        /// </summary>
        /// <returns></returns>
        double GetValue();

        /// <summary>
        /// Gets a formatted string of the current value 
        /// </summary>
        /// <returns></returns>
        string GetText();

        /// <summary>
        /// Processes keystroke
        /// </summary>
        /// <param name="key"></param>
        void EnterKey(byte key, byte par);

        /// <summary>
        /// Processes keystroke
        /// </summary>
        /// <param name="key"></param>
        void EnterKey(byte key);

    }
}
