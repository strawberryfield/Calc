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
using System.Windows.Forms;

namespace Casasoft.Calc
{
    [ComVisible(false)]
    public partial class ProgramCard : UserControl
    {
        public ProgramCard()
        {
            InitializeComponent();
        }  

        public event EventHandler ClickLabel
        {
            add
            {
                txtA.Click += value;
                txtB.Click += value;
                txtC.Click += value;
                txtD.Click += value;
                txtE.Click += value;
                txtA1.Click += value;
                txtB1.Click += value;
                txtC1.Click += value;
                txtD1.Click += value;
                txtE1.Click += value;
                txtProgram.Click += value;
            }
            remove
            {
                txtA.Click -= value;
                txtB.Click -= value;
                txtC.Click -= value;
                txtD.Click -= value;
                txtE.Click -= value;
                txtA1.Click -= value;
                txtB1.Click -= value;
                txtC1.Click -= value;
                txtD1.Click -= value;
                txtE1.Click -= value;
                txtProgram.Click -= value;
            }
        }
    }
}
