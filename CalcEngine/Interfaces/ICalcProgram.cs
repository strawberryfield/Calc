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

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Casasoft.Calc
{
    [Guid("B33ECB95-0809-4739-888E-25B5F06E6E04")]
    public interface ICalcProgram
    {
        /// <summary>
        /// Program counter
        /// </summary>
        int Counter { get; }

        /// <summary>
        /// Clears the program
        /// </summary>
        void CP();

        /// <summary>
        /// Moves program counter to 0 and clears return stack
        /// </summary>
        void RST();

        /// <summary>
        /// Step forward
        /// </summary>
        void SST();

        /// <summary>
        /// Step backward
        /// </summary>
        void BST();

        /// <summary>
        /// Go to step
        /// </summary>
        /// <param name="step"></param>
        void GTO(int step);

        /// <summary>
        /// Go to label
        /// </summary>
        /// <param name="label">byte value of the opcode</param>
        void GTO_Label(byte label);

        /// <summary>
        /// Go to step and save return point
        /// </summary>
        /// <param name="step"></param>
        void SBR(int step);

        /// <summary>
        /// Returns to saved point
        /// </summary>
        void RTN();

        /// <summary>
        /// Inserts a program step
        /// </summary>
        void INS();

        /// <summary>
        /// Deletes the current program step
        /// </summary>
        void DEL();

        /// <summary>
        /// Gets current step's opcode
        /// </summary>
        /// <returns></returns>
        byte GetCurrent();

        /// <summary>
        /// Sets the value at current step
        /// </summary>
        /// <param name="step"></param>
        void SetCurrent(byte step);

        /// <summary>
        /// Gets the strin to display
        /// </summary>
        /// <returns></returns>
        string GetDisplayString();

        /// <summary>
        /// True if at the end of the program
        /// </summary>
        /// <returns></returns>
        bool IsEof();
        
        /// <summary>
        /// Adds an empty step
        /// </summary>
        void Add();

        /// <summary>
        /// Goes to next step and add it if missing
        /// </summary>
        void GoNext();

        /// <summary>
        /// List of labels
        /// </summary>
        Dictionary<byte, int> Labels { get; }

        /// <summary>
        /// finds labels declarations in the program
        /// </summary>
        void FindLabels();

        /// <summary>
        /// Serializes program for save
        /// </summary>
        /// <returns></returns>
        string Serialize();

        /// <summary>
        /// Deserialize program from string
        /// </summary>
        /// <param name="s"></param>
        void Deserialize(string s);
    }
}
