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
    [Guid("2F68C8CC-08D1-4A71-B32C-F21FB431E54F"),
        ClassInterface(ClassInterfaceType.None),
        ProgId("Casasoft.CalcData")]
    public class DataStorage : IDataStorage
    {
        private Dictionary<int, double> db;

        public DataStorage()
        {
            db = new Dictionary<int, double>();
        }

        public void CMS() => db.Clear();

        public void STO(int r, double v) => db[r] = v;
 
        public double RCL(int r)
        {
            double ret;
            if (!db.TryGetValue(r, out ret)) ret = 0;
            return ret;
        }

        public void SUM(int r, double v) => STO(r, RCL(r) + v);

        public void INV_SUM(int r, double v) => STO(r, RCL(r) - v);

        public void PRD(int r, double v) => STO(r, RCL(r) * v);

        public void INV_PRD(int r, double v) => STO(r, RCL(r) / v);

        public void Inc(int r) => SUM(r, 1);

        public void Dec(int r) => SUM(r, -1);

        public double EXC(int r, double v)
        {
            double ret = RCL(r);
            STO(r, v);
            return ret;
        }

        #region indirect
        private int getReg(int r) => Convert.ToInt16(RCL(r));

        public void STO_ind(int r, double v) => STO(getReg(r), v);

        public double RCL_ind(int r) => RCL(getReg(r));

        public void SUM_ind(int r, double v) => SUM(getReg(r), v);

        public void INV_SUM_ind(int r, double v) => SUM(getReg(r), -v);

        public void PRD_ind(int r, double v) => PRD(getReg(r), v);

        public void INV_PRD_ind(int r, double v) => INV_PRD(getReg(r), v);

        public double EXC_ind(int r, double v) => EXC(getReg(r), v);
        #endregion
    }
}
