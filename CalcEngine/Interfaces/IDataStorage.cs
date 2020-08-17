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
    [Guid("F5104AA9-41B6-4CC9-BF4E-FFAF8692C6D5")]
    public interface IDataStorage
    {
        /// <summary>
        /// Clears all registers
        /// </summary>
        void CMS();

        /// <summary>
        /// Stores value in given register
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v"></param>
        void STO(int r, double v);
        
        /// <summary>
        /// Recalls value from register 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        double RCL(int r);

        /// <summary>
        /// Sums the given value to the register
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v"></param>
        void SUM(int r, double v);

        /// <summary>
        /// Subtracts the given value from the register
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v"></param>
        void INV_SUM(int r, double v);

        /// <summary>
        /// Multiplies the register value by the given value
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v"></param>
        void PRD(int r, double v);

        /// <summary>
        /// Divide the register value by the given value
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v"></param>
        void INV_PRD(int r, double v);

        /// <summary>
        /// Increase the register by 1
        /// </summary>
        /// <param name="r"></param>
        void Inc(int r);

        /// <summary>
        /// Decrease the register by 1
        /// </summary>
        /// <param name="r"></param>
        void Dec(int r);

        /// <summary>
        /// Store the given value to the regsiter and returns the old register value
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v"></param>
        /// <returns>Old register value</returns>
        double EXC(int r, double v);


        #region indirect
        /// <summary>
        /// Store the given value to the register pointed by passed register
        /// </summary>
        /// <param name="r">register containing the pointer</param>
        /// <param name="v"></param>
        void STO_ind(int r, double v);

        /// <summary>
        /// Recalls the value from the register pointed by passed register
        /// </summary>
        /// <param name="r">register containing the pointer</param>
        double RCL_ind(int r);

        /// <summary>
        /// Sums the given value to the pointed register
        /// </summary>
        /// <param name="r">register containing the pointer</param>
        /// <param name="v"></param>
        void SUM_ind(int r, double v);

        /// <summary>
        /// Subtracts the given value from the pointed register
        /// </summary>
        /// <param name="r">register containing the pointer</param>
        /// <param name="v"></param>
        void INV_SUM_ind(int r, double v);

        /// <summary>
        /// Multiplies the pointed register value by the given value
        /// </summary>
        /// <param name="r">register containing the pointer</param>
        /// <param name="v"></param>
        void PRD_ind(int r, double v);

        /// <summary>
        /// Divide the pointed register value by the given value
        /// </summary>
        /// <param name="r">register containing the pointer</param>
        /// <param name="v"></param>
        void INV_PRD_ind(int r, double v);

        /// <summary>
        /// Store the given value to the register pointed by given register and returns the old pointed register value
        /// </summary>
        /// <param name="r">register containing the pointer</param>
        /// <param name="v"></param>
        /// <returns>Old register value</returns>
        double EXC_ind(int r, double v);
        #endregion

        /// <summary>
        /// Serializes the database for save
        /// </summary>
        /// <returns></returns>
        string Serialize();

        /// <summary>
        /// Loads the database from the string
        /// </summary>
        /// <param name="s"></param>
        void Deserialize(string s);
    }
}
