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

using System.CodeDom;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Casasoft.Calc
{
    [Guid("E914961C-6559-4BAC-AA90-F6CF18C50F54"),
    ClassInterface(ClassInterfaceType.None),
    ProgId("Casasoft.CalcPrograms")]
    public class Programs : IPrograms
    {
        private Dictionary<int, CalcProgram> Progs;
        public int ActiveProgram { get; set; }

        public Programs()
        {
            Progs = new Dictionary<int, CalcProgram>();
            Progs.Add(0, new CalcProgram());
            ActiveProgram = 0;
        }

        public CalcProgram Current { get => Progs[ActiveProgram]; }
        
        public CalcProgram this[int n] { get => Progs[n]; set => Progs[n] = value; }
    }
}
