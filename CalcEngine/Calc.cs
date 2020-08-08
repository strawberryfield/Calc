﻿// copyright (c) 2020 Roberto Ceccarelli - CasaSoft
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

namespace Casasoft.Calc
{
    public class Calc
    {
        private enum Operands { Plus, Minus, Times, Div, Exponent }

        private class AritmeticStackItem
        {
            public double Value;
            public Operands Operand;
            public int Priority;

            public AritmeticStackItem(double v, Operands o, int p)
            {
                Value = v;
                Operand = o;
                Priority = p;
            }
        }

        private Stack<AritmeticStackItem> AritmeticStack;
        private int BracketLevel;
        private InputProcessor inputProcessor;

        private bool InverseFunction;
        private enum AngleUnits { Deg, Rad, Grad }
        private AngleUnits TrigMode;
        private double t;

        public DataStorage Memories;
        public List<byte> Steps;
        public Display Display;

        public Calc()
        {
            Memories = new DataStorage();
            Steps = new List<byte>();
            AritmeticStack = new Stack<AritmeticStackItem>();
            BracketLevel = 0;
            InverseFunction = false;
            TrigMode = AngleUnits.Rad;
            t = 0;
            Display = new Display();
            inputProcessor = new InputProcessor(ProcessCommand);
        }

        public void EnterKey(byte key)
        {
            inputProcessor.ProcessKey(key);
        }

        private bool ProcessCommand(byte key, byte[] par)
        {
            if (key <= 9 || key == 93 || key == 94 || key == 24)
            {
                // Numeric input
                Display.EnterKey(key);
            }
            else switch (key)
                {
                    // Clear
                    case 25:
                        Display.Clear();
                        AritmeticStack.Clear();
                        BracketLevel = 0;
                        break;

                    // Equal
                    case 95:
                        while (AritmeticStack.Count > 0)
                        {
                            processStack();
                        }
                        BracketLevel = 0;
                        break;

                    // Operators
                    case 85:
                        processOperator(Operands.Plus, BracketLevel + 1);
                        break;
                    case 75:
                        processOperator(Operands.Minus, BracketLevel + 1);
                        break;
                    case 65:
                        processOperator(Operands.Times, BracketLevel + 2);
                        break;
                    case 55:
                        processOperator(Operands.Div, BracketLevel + 2);
                        break;
                    case 45:
                        processOperator(Operands.Exponent, BracketLevel + 3);
                        break;

                    case 32:
                        double tmp = t;
                        t = Display.GetValue();
                        Display.SetValue(tmp);
                        break;

                    // Brackets
                    case 53:
                        BracketLevel += 10;
                        break;
                    case 54:
                        processStack();
                        BracketLevel -= 10;
                        break;

                    // Angular units
                    case 60:
                        TrigMode = AngleUnits.Deg;
                        break;
                    case 70:
                        TrigMode = AngleUnits.Rad;
                        break;
                    case 80:
                        TrigMode = AngleUnits.Grad;
                        break;

                    //Functions
                    case 23:
                        Display.SetValue(InverseFunction ?
                            Math.Exp(Display.GetValue()) : Math.Log(Display.GetValue()));
                        break;
                    case 28:
                        Display.SetValue(InverseFunction ?
                            Math.Pow(10, Display.GetValue()) : Math.Log(Display.GetValue(), 10));
                        break;
                    case 33:
                        Display.SetValue(Display.GetValue() * Display.GetValue());
                        break;
                    case 34:
                        Display.SetValue(Math.Sqrt(Display.GetValue()));
                        break;
                    case 35:
                        Display.SetValue(1 / Display.GetValue());
                        break;
                    case 50:
                        Display.SetValue(Math.Abs(Display.GetValue()));
                        break;
                    case 59:
                        Display.SetValue(InverseFunction ?
                            Display.GetValue() - Math.Truncate(Display.GetValue()) :
                            Math.Truncate(Display.GetValue()));
                        break;
                    case 89:
                        Display.SetValue(Math.PI);
                        break;

                    case 38:
                        Display.SetValue(InverseFunction ?
                            rad2AngularUnit(Math.Asin(Display.GetValue())) :
                            Math.Sin(AngularUnit2rad(Display.GetValue())));
                        break;
                    case 39:
                        Display.SetValue(InverseFunction ?
                            rad2AngularUnit(Math.Acos(Display.GetValue())) :
                            Math.Cos(AngularUnit2rad(Display.GetValue())));
                        break;
                    case 30:
                        Display.SetValue(InverseFunction ?
                            rad2AngularUnit(Math.Atan(Display.GetValue())) :
                            Math.Tan(AngularUnit2rad(Display.GetValue())));
                        break;

                    // Memories
                    case 47:
                        Memories.CMS();
                        break;

                    default:
                        break;
                }

            // Hadles INV key
            if (key == 22)
            {
                InverseFunction = !InverseFunction;
            }
            else
            {
                InverseFunction = false;
            }
            return true;
        }

        private double AngularUnit2rad(double a) =>
            TrigMode == AngleUnits.Rad ? a :
            a / (TrigMode == AngleUnits.Deg ? 180 : 200) * Math.PI;

        private double rad2AngularUnit(double a) =>
            TrigMode == AngleUnits.Rad ? a :
            a * (TrigMode == AngleUnits.Deg ? 180 : 200) / Math.PI;

        private void processOperator(Operands op, int priority)
        {
            if (AritmeticStack.Count > 0)
            {
                if (AritmeticStack.Peek().Priority >= priority)
                {
                    processStack();
                }
            }
            AritmeticStack.Push(new AritmeticStackItem(Display.GetValue(), op, priority));
            Display.AutoClear();
        }

        private void processStack()
        {
            if (AritmeticStack.Count == 0) return;

            AritmeticStackItem ts = AritmeticStack.Pop();
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
    }
}
