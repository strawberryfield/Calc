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

using System.Runtime.InteropServices;

namespace Casasoft.Calc
{
    [Guid("1514ADA7-4158-431F-80EE-945A35ADD80B")]
    public interface IButtonDef
    {
        /// <summary>
        /// Text of the button
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Text of the label
        /// </summary>
        string AltText { get; }

        /// <summary>
        /// Main function code
        /// </summary>
        byte FunctionCode { get; }

        /// <summary>
        /// Second function code (if any)
        /// </summary>
        byte SecondFunctionCode { get; }

        /// <summary>
        /// Button type
        /// </summary>
        /// <remarks>
        /// 0=Normal, 1=Numeric, 2=Operator
        /// </remarks>
        int NButtonType { get; }

    }
}
