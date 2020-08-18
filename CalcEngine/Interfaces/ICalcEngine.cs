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
        /// <summary>
        /// Database of memories
        /// </summary>
        DataStorage Memories { get; set; }

        /// <summary>
        /// Library of programs
        /// </summary>
        Programs Programs { get; }

        /// <summary>
        /// Array of flags
        /// </summary>
        Flags Flags { get; }

        /// <summary>
        /// Display manager
        /// </summary>
        Display Display { get; set; }

        /// <summary>
        /// Processes keys from user interface or program stream
        /// </summary>
        /// <param name="key"></param>
        void EnterKey(byte key);

        /// <summary>
        /// Gets the string to display
        /// </summary>
        /// <returns></returns>
        string GetDisplayString();

        /// <summary>
        /// Only for test
        /// </summary>
        /// <returns></returns>
        string About();

        /// <summary>
        /// Saves active program to disk
        /// </summary>
        /// <param name="filename"></param>
        void SaveProgram(string filename);

        /// <summary>
        /// Saves given program to disk
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="n"></param>
        void SaveProgram(string filename, int n);

        /// <summary>
        /// Loads current program from disk
        /// </summary>
        /// <param name="filename"></param>
        void LoadProgram(string filename);

        /// <summary>
        /// Loads given program from disk
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="n"></param>
        void LoadProgram(string filename, int n);

        /// <summary>
        /// Saves memories' database to disk
        /// </summary>
        /// <param name="filename"></param>
        void SaveMemories(string filename);

        /// <summary>
        /// Loads memories' database from disk
        /// </summary>
        /// <param name="filename"></param>
        void LoadMemories(string filename);
    }
}
