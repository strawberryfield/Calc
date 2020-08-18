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
    public enum Operands { Plus, Minus, Times, Div, Exponent }

    [ComVisible(false)]
    public class ArithmeticStack
    {
        private Display Display;

        private class ArithmeticStackItem
        {
            public double Value;
            public Operands Operand;
            public int Priority;

            public ArithmeticStackItem(double v, Operands o, int p)
            {
                Value = v;
                Operand = o;
                Priority = p;
            }
        }

        private Stack<ArithmeticStackItem> stk;
        private int BracketLevel;

        public ArithmeticStack(Display disp)
        {
            Display = disp;
            stk = new Stack<ArithmeticStackItem>();
            BracketLevel = 0;
        }

        public void Clear()
        {
            stk.Clear();
            BracketLevel = 0;
        }

        public void IncreaseBracketLevel() => BracketLevel += 10;
        public void DecreaseBracketLevel() => BracketLevel -= 10;

        public void ProcessOperator(Operands op)
        {
            int priority = BracketLevel;
            switch (op)
            {
                case Operands.Plus:
                case Operands.Minus:
                    priority += 1;
                    break;
                case Operands.Times:
                case Operands.Div:
                    priority += 2;
                    break;
                case Operands.Exponent:
                    priority += 3;
                    break;
                default:
                    break;
            }
            if (stk.Count > 0)
            {
                if (stk.Peek().Priority >= priority)
                {
                    ProcessStack();
                }
            }
            stk.Push(new ArithmeticStackItem(Display.GetValue(), op, priority));
            Display.AutoClear();
        }

        public void ProcessStack()
        {
            if (stk.Count == 0) return;

            ArithmeticStackItem ts = stk.Pop();
            switch (ts.Operand)
            {
                case Operands.Plus:
                    Display.SetValue(ts.Value + Display.GetValue());
                    break;
                case Operands.Minus:
                    Display.SetValue(ts.Value - Display.GetValue());
                    break;
                case Operands.Times:
                    Display.SetValue(ts.Value * Display.GetValue());
                    break;
                case Operands.Div:
                    Display.SetValue(ts.Value / Display.GetValue());
                    break;
                case Operands.Exponent:
                    Display.SetValue(Math.Pow(ts.Value, Display.GetValue()));
                    break;
                default:
                    break;
            }
        }

        public void Equal()
        {
            while (stk.Count > 0)
            {
                ProcessStack();
            }
            BracketLevel = 0;
        }

    }
}
