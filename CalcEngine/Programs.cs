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
    [Guid("E914961C-6559-4BAC-AA90-F6CF18C50F54"),
    ClassInterface(ClassInterfaceType.None),
    ProgId("Casasoft.CalcPrograms")]
    public class Programs : IPrograms
    {
        private Dictionary<int, CalcProgram> Progs;

        private int _activeProgram;
        public int ActiveProgram
        {
            get => _activeProgram;
            set
            {
                CurrentProgramChanging(this, new ProgramChangingEventArgs(_activeProgram, value));
                _activeProgram = value;
            }
        }

        public Programs()
        {
            Progs = new Dictionary<int, CalcProgram>();
            Progs.Add(0, new CalcProgram());
            _activeProgram = 0;
        }

        public CalcProgram Current => Progs[_activeProgram];

        public CalcProgram this[int n] { get => Progs[n]; set => Progs[n] = value; }

        public EventHandler<ProgramChangingEventArgs> CurrentProgramChanging;
    }

    public class ProgramChangingEventArgs
    {
        public int OldProgram { get; set; }
        public int NewProgram { get; set; }

        public ProgramChangingEventArgs(int old, int selected)
        {
            OldProgram = old;
            NewProgram = selected;
        }
    }
}
