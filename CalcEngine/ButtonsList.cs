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

namespace Casasoft.Calc
{
    public class ButtonsList
    {
        public ButtonDef[][] ButtonDefs { get; private set; }

        public ButtonsList()
        {
            ButtonDefs = new ButtonDef[][]
            {
                new ButtonDef[]
                   {
                      new ButtonDef("A", "A´", 11),
                      new ButtonDef("B", "B´", 12),
                      new ButtonDef("C", "C´", 13),
                      new ButtonDef("D", "D´", 14),
                      new ButtonDef("E", "E´", 15),
                   },
                new ButtonDef[]
                   {
                      new ButtonDef("2nd", "", 21, 21, ButtonDef.EButtonType.Operator),
                      new ButtonDef("INV", "", 22, 22, ButtonDef.EButtonType.Normal),
                      new ButtonDef("lnx", "log", 23),
                      new ButtonDef("CE", "CP", 24),
                      new ButtonDef("CLR", "", 25, 25, ButtonDef.EButtonType.Operator),
                   },
                new ButtonDef[]
                   {
                      new ButtonDef("LRN", "Pgm", 31),
                      new ButtonDef("x⇌t", "P→R", 32),
                      new ButtonDef("x²", "sin", 33),
                      new ButtonDef("√x", "cos", 34),
                      new ButtonDef("1/x", "tan", 35),
                   },
                new ButtonDef[]
                   {
                      new ButtonDef("SST", "Ins", 41),
                      new ButtonDef("STO", "CMs", 42),
                      new ButtonDef("RCL", "Exc", 43),
                      new ButtonDef("SUM", "Prd", 44),
                      new ButtonDef("yˣ", "Ind", 45),
                   },
                new ButtonDef[]
                   {
                      new ButtonDef("BST", "Del", 51),
                      new ButtonDef("EE", "Eng", 52),
                      new ButtonDef("(", "Fix", 53),
                      new ButtonDef(")", "Int", 54),
                      new ButtonDef("÷", "|x|", 55, 50, ButtonDef.EButtonType.Operator),
                   },
                new ButtonDef[]
                   {
                      new ButtonDef("GTO", "Pause", 61),
                      new ButtonDef("7", "x=t", 7, 67, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("8", "Nop", 8, 68, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("9", "Op", 9, 69, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("×", "Deg", 65, 60, ButtonDef.EButtonType.Operator),
                   },
                new ButtonDef[]
                   {
                      new ButtonDef("SBR", "Lbl", 71),
                      new ButtonDef("4", "x≥t", 4, 77, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("5", "∑+", 5, 78, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("6", "x̅", 6, 79, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("-", "Rad", 75, 70, ButtonDef.EButtonType.Operator),
                   },
                new ButtonDef[]
                   {
                      new ButtonDef("RST", "St flg", 81),
                      new ButtonDef("1", "If flg", 1, 87, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("2", "D.MS", 2, 88, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("3", "π", 3, 89, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("+", "Grad", 85, 80, ButtonDef.EButtonType.Operator),
                   },
                new ButtonDef[]
                   {
                      new ButtonDef("R/S", "Write", 91),
                      new ButtonDef("0", "Dsz", 0, 97, ButtonDef.EButtonType.Numeric),
                      new ButtonDef(".", "Adv", 93, 98, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("+/-", "Prt", 94, 99, ButtonDef.EButtonType.Numeric),
                      new ButtonDef("=", "List", 95, 90, ButtonDef.EButtonType.Operator),
                   },
            };
        }
    }
}
