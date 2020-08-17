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
    [Guid("7E640F3E-6775-4182-95BC-0B728C749F93")]
    public interface IPrograms
    {
        /// <summary>
        /// Current program id
        /// </summary>
        int ActiveProgram { get; set; }

        /// <summary>
        /// Current program
        /// </summary>
        CalcProgram Current { get; }

        /// <summary>
        /// Program by id
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        CalcProgram this[int n] { get; set; }
    }
}
