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

using Casasoft.Calc.Json;
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

            EventHandler value = ProgramCard_MouseClick;
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

        private void ProgramCard_MouseClick(object sender, EventArgs e)
        {
            TextBox caller = (TextBox)sender;
            ProgramCardEdit edt = new ProgramCardEdit(caller);
            DialogResult res = edt.ShowDialog();
            if (res == DialogResult.OK)
            {
                caller.Text = edt.Value;
            }
        }

        public void SetFromJson(Card json)
        {
            txtProgram.Text = json.Name;
            txtA.Text = json.A;
            txtB.Text = json.B;
            txtC.Text = json.C;
            txtD.Text = json.D;
            txtE.Text = json.E;
            txtA1.Text = json.A1;
            txtB1.Text = json.B1;
            txtC1.Text = json.C1;
            txtD1.Text = json.D1;
            txtE1.Text = json.E1;
        }

        public Card GetForJson()
        {
            Card json = new Card();
            json.Name = txtProgram.Text;
            json.A = txtA.Text;
            json.B = txtB.Text;
            json.C = txtC.Text;
            json.D = txtD.Text;
            json.E = txtE.Text;
            json.A1 = txtA1.Text;
            json.B1 = txtB1.Text;
            json.C1 = txtC1.Text;
            json.D1 = txtD1.Text;
            json.E1 = txtE1.Text;
            return json;
        }
    }
}
