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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Casasoft.Calc
{
    [Guid("3555C0AB-FE02-4CC8-88C7-06815BC30CDE"),
    ClassInterface(ClassInterfaceType.None),
    ProgId("Casasoft.CalcFlags")]
    public class Flags : IFlags
    {
        private Dictionary<byte, bool> db;

        public Flags()
        {
            db = new Dictionary<byte, bool>();
        }

        public void Clear() => db.Clear();

        public void Clear(byte f) => db[f] = false;

        public void Set(byte f) => db[f] = true;

        public bool Get(byte f)
        {
            bool ret;
            if (!db.TryGetValue(f, out ret)) ret = false;
            return ret;
        }
    }
}
