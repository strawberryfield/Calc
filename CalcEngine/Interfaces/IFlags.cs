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
    [Guid("C8169041-4557-4AE1-B464-FBE0561F270A")]
    public interface IFlags
    {
        /// <summary>
        /// Clears all flags
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears the flag
        /// </summary>
        /// <param name="f"></param>
        void Clear(byte f);

        /// <summary>
        /// Sets the flag
        /// </summary>
        /// <param name="f"></param>
        void Set(byte f);

        /// <summary>
        /// Gets the value of the flag
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        bool Get(byte f);
    }
}
