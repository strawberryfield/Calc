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
        void CMS();

        void STO(int r, double v);

        double RCL(int r);
  

        void SUM(int r, double v);

        void INV_SUM(int r, double v);

        void PRD(int r, double v);

        void INV_PRD(int r, double v);

        void Inc(int r);

        void Dec(int r);

        double EXC(int r, double v);


        #region indirect
        void STO_ind(int r, double v);

        double RCL_ind(int r);

        void SUM_ind(int r, double v);

        void INV_SUM_ind(int r, double v);

        void PRD_ind(int r, double v);

        void INV_PRD_ind(int r, double v);

        double EXC_ind(int r, double v);
        #endregion
    }
}
