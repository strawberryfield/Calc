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
    [Guid("B33ECB95-0809-4739-888E-25B5F06E6E04")]
    public interface ICalcProgram
    {
        int Counter { get; }
        void CP();
        void RST();
        void SST();
        void BST();
        void SBR(int step);
        void RTN();
        void INS();
        void DEL();
        byte GetCurrent();
        void SetCurrent(byte step);
        string GetDisplayString();
        bool IsEof();
        void Put(byte b);
        void Add();
        void GoNext();
    }
}
