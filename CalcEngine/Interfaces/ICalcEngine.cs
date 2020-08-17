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
    [Guid("63B56927-BA70-4CD4-9369-5A813CDF6CF9")]
    public interface ICalcEngine
    {
        DataStorage Memories { get; set; }
        Display Display { get; set; }
        void EnterKey(byte key);
        string GetDisplayString();
        string About();

        void SaveProgram(string filename);
        void SaveProgram(string filename, int n);

        void LoadProgram(string filename);
        void LoadProgram(string filename, int n);

        void SaveMemories(string filename);
        void LoadMemories(string filename);

    }
}
