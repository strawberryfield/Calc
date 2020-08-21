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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Casasoft.Calc
{
    public enum OperatingMode { Interactive, Run, Learn }

    [Guid("6C6029E3-70C3-4D85-9C0F-A49E05A1F2DE"),
    ClassInterface(ClassInterfaceType.None),
    ProgId("Casasoft.CalcEngine")]
    public class CalcEngine : ICalcEngine
    {
        private ArithmeticStack ArithmeticStack;
        private InputProcessor inputProcessor;

        private bool InverseFunction;
        private enum AngleUnits { Deg, Rad, Grad }
        private AngleUnits TrigMode;
        private double t;
        private OperatingMode OperatingMode;

        public DataStorage Memories { get; set; }
        public Display Display { get; set; }
        public Programs Programs { get; private set; }
        public Flags Flags { get; private set; }

        public string About() => "Casasoft Calc";

        public CalcEngine()
        {
            Memories = new DataStorage();
            Programs = new Programs();
            Flags = new Flags();
            Display = new Display();
            ArithmeticStack = new ArithmeticStack(Display);

            InverseFunction = false;
            TrigMode = AngleUnits.Rad;
            t = 0;
            inputProcessor = new InputProcessor(ProcessCommand);
            OperatingMode = OperatingMode.Interactive;
            inputProcessor.OperatingMode = OperatingMode;
            LoadConstantMemory();
        }

        ~CalcEngine()
        {
            SaveConstantMemory();
        }

        public string GetDisplayString()
        {
            switch (OperatingMode)
            {
                case OperatingMode.Interactive:
                    return Display.GetText();
                case OperatingMode.Run:
                    return "Computing";
                case OperatingMode.Learn:
                    return Programs.Current.GetDisplayString();
                default:
                    return string.Empty;
            }
        }

        public void EnterKey(byte key)
        {
            inputProcessor.ProcessKey(key);
        }

        private bool ProcessCommand(byte key, byte[] par)
        {
            if (OperatingMode == OperatingMode.Learn)
                return ProcessCommandLearn(key, par);
            else
                return ProcessCommandExec(key, par);
        }

        private bool ProcessCommandLearn(byte key, byte[] par)
        {
            switch (key)
            {
                case 31:
                    OperatingMode = OperatingMode.Interactive;
                    Programs.Current.FindLabels();
                    break;
                case 41:
                    Programs.Current.GoNext();
                    break;
                case 51:
                    if (Programs.Current.Counter > 0)
                        Programs.Current.BST();
                    break;
                case 46:
                    Programs.Current.INS();
                    break;
                case 56:
                    Programs.Current.DEL();
                    break;
                default:
                    Programs.Current.SetCurrent(key);
                    Programs.Current.GoNext();
                    if (par != null)
                        for (int j = 1; j <= par[0]; ++j)
                        {
                            Programs.Current.SetCurrent(par[j]);
                            Programs.Current.GoNext();
                        }
                    break;
            }
            return true;
        }

        private byte[] CommandsForDisplay =
        {
            0,1,2,3,4,5,6,7,8,9,
            24,93,94,
            52,57,58
        };
        private byte[] InvertibleCommandsForDisplay =
        {
            52,57,58
        };

        private bool ProcessCommandExec(byte key, byte[] par)
        {
            if (CommandsForDisplay.Contains(key))
            {
                // Numeric input
                if (InvertibleCommandsForDisplay.Contains(key) && InverseFunction)
                    key += 100;
                Display.EnterKey(key, par == null ? (byte)0 : par[1]);
            }
            else switch (key)
                {
                    // Clear
                    case 25:
                        Display.Clear();
                        ArithmeticStack.Clear();
                        break;

                    // Equal
                    case 95:
                        ArithmeticStack.Equal();
                        break;

                    // Operators
                    case 85:
                        ArithmeticStack.ProcessOperator(Operands.Plus);
                        break;
                    case 75:
                        ArithmeticStack.ProcessOperator(Operands.Minus);
                        break;
                    case 65:
                        ArithmeticStack.ProcessOperator(Operands.Times);
                        break;
                    case 55:
                        ArithmeticStack.ProcessOperator(Operands.Div);
                        break;
                    case 45:
                        ArithmeticStack.ProcessOperator(Operands.Exponent);
                        break;

                    case 32:
                        double tmp = t;
                        t = Display.GetValue();
                        Display.SetValue(tmp);
                        break;

                    // Brackets
                    case 53:
                        ArithmeticStack.IncreaseBracketLevel();
                        break;
                    case 54:
                        ArithmeticStack.ProcessStack();
                        ArithmeticStack.IncreaseBracketLevel();
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
                    case 69:
                        OP(par[1]);
                        break;
                    case 84:
                        OP(Convert.ToByte(Memories.RCL(par[1])));
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
                    case 42:
                        Memories.STO(par[1], Display.GetValue());
                        break;
                    case 43:
                        Display.SetValue(Memories.RCL(par[1]));
                        break;
                    case 48:
                        Display.SetValue(Memories.EXC(par[1], Display.GetValue()));
                        break;
                    case 44:
                        if (InverseFunction)
                            Memories.INV_SUM(par[1], Display.GetValue());
                        else
                            Memories.SUM(par[1], Display.GetValue());
                        break;
                    case 49:
                        if (InverseFunction)
                            Memories.INV_PRD(par[1], Display.GetValue());
                        else
                            Memories.PRD(par[1], Display.GetValue());
                        break;
                    case 72:
                        Memories.STO_ind(par[1], Display.GetValue());
                        break;
                    case 73:
                        Display.SetValue(Memories.RCL_ind(par[1]));
                        break;
                    case 63:
                        Display.SetValue(Memories.EXC_ind(par[1], Display.GetValue()));
                        break;
                    case 74:
                        if (InverseFunction)
                            Memories.INV_SUM_ind(par[1], Display.GetValue());
                        else
                            Memories.SUM_ind(par[1], Display.GetValue());
                        break;
                    case 64:
                        if (InverseFunction)
                            Memories.INV_PRD_ind(par[1], Display.GetValue());
                        else
                            Memories.PRD_ind(par[1], Display.GetValue());
                        break;

                    // Flags and loops
                    case 86:
                        if (InverseFunction)
                        {
                            if (par[1] == 40)
                                Flags.Clear(Convert.ToByte(Memories.RCL(par[2])));
                            else
                                Flags.Clear(par[1]);
                        }
                        else
                        {
                            if (par[1] == 40)
                                Flags.Set(Convert.ToByte(Memories.RCL(par[2])));
                            else
                                Flags.Set(par[1]);
                        }
                        break;
                    case 87:
                        if (InverseFunction)
                        {
                            if (par[1] == 40)
                            {
                                if (!Flags.Get(Convert.ToByte(Memories.RCL(par[2]))))
                                    ProcessJump(par, 3);
                            }
                            else
                            {
                                if (!Flags.Get(par[1]))
                                    ProcessJump(par, 2);
                            }
                        }
                        else
                        {
                            if (par[1] == 40)
                            {
                                if (Flags.Get(Convert.ToByte(Memories.RCL(par[2]))))
                                    ProcessJump(par, 3);
                            }
                            else
                            {
                                if (Flags.Get(par[1]))
                                    ProcessJump(par, 2);
                            }
                        }
                        break;
                    case 97:
                        int rx = par[1] == 40 ? Convert.ToInt32(Memories.RCL(par[2])) : par[1];
                        if (Memories.RCL(rx) != 0)
                        {
                            if (Memories.RCL(rx) > 0)
                                Memories.Dec(rx);
                            else
                                Memories.Inc(rx);
                        }
                        if (InverseFunction)
                        {
                            if (Memories.RCL(rx) == 0)
                                ProcessJump(par, par[1] == 40 ? 3 : 2);
                        }
                        else
                        {
                            if (Memories.RCL(rx) != 0)
                                ProcessJump(par, par[1] == 40 ? 3 : 2);
                        }
                        break;

                    //Programming
                    case 31:
                        if (OperatingMode == OperatingMode.Interactive)
                        {
                            OperatingMode = OperatingMode.Learn;
                        }
                        break;
                    case 81:
                        Programs.Current.RST();
                        Flags.Clear();
                        break;
                    case 29:
                        if (OperatingMode == OperatingMode.Interactive)
                        {
                            Programs.Current.CP();
                            Flags.Clear();
                        }
                        t = 0;
                        break;
                    case 36:
                        Programs.ActiveProgram = par[1];
                        break;
                    case 62:
                        Programs.ActiveProgram = Convert.ToInt32(Memories.RCL(par[1]));
                        break;
                    case 61:
                        if (par[0] == 1)
                        {
                            Programs.Current.GTO_Label(par[1]);
                        }
                        else
                        {
                            Programs.Current.GTO(par[1] * 100 + par[2]);
                        }
                        break;
                    case 71:
                        ProcessSubroutine(par);
                        break;
                    case 83:
                        Programs.Current.GTO(Convert.ToInt32(Memories.RCL(par[1])));
                        break;
                    case 67:
                        if (InverseFunction)
                        {
                            if (Display.GetValue() != t)
                                ProcessJump(par);
                        }
                        else
                        {
                            if (Display.GetValue() == t)
                                ProcessJump(par);
                        }
                        break;
                    case 77:
                        if (InverseFunction)
                        {
                            if (Display.GetValue() < t)
                                ProcessJump(par);
                        }
                        else
                        {
                            if (Display.GetValue() >= t)
                                ProcessJump(par);
                        }
                        break;
                    case 91:
                        switch (OperatingMode)
                        {
                            case OperatingMode.Interactive:
                                OperatingMode = OperatingMode.Run;
                                Run();
                                break;
                            case OperatingMode.Run:
                                OperatingMode = OperatingMode.Interactive;
                                break;
                            case OperatingMode.Learn:
                                break;
                            default:
                                break;
                        }
                        break;
                    case 92:
                        if (OperatingMode == OperatingMode.Run)
                        {
                            if (Programs.Current.RTN())
                                OperatingMode = OperatingMode.Interactive;
                        }
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                        ProcessSubroutine(key);
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

        private void Run()
        {
            while (OperatingMode == OperatingMode.Run)
            {
                EnterKey(Programs.Current.GetCurrent());
                if (Programs.Current.IsEof())
                    OperatingMode = OperatingMode.Interactive;
                else
                    Programs.Current.SST();
            }
        }

        #region jumps
        private void ProcessJump(byte[] par, int offset = 1, bool isSBR = false)
        {
            int addr = 0;
            if (par[offset] == 40)
            {
                // Indirect jump
                addr = Convert.ToInt32(Memories.RCL(par[offset + 1]));
                if (isSBR)
                    Programs.Current.SBR(addr);
                else
                    Programs.Current.GTO(addr);
            }
            else if (par[offset] < 10)
            {
                // Absolute jump
                addr = par[offset] * 100 + par[offset + 1];
                if (isSBR)
                    Programs.Current.SBR(addr);
                else
                    Programs.Current.GTO(addr);

            }
            else
            {
                // Jump to label
                if (isSBR)
                    Programs.Current.SBR_Label(par[offset]);
                else
                    Programs.Current.GTO_Label(par[offset]);
            }
        }

        private void ProcessSubroutine(byte[] par)
        {
            ProcessJump(par, 1, OperatingMode == OperatingMode.Run);
            if (OperatingMode == OperatingMode.Interactive)
            {
                OperatingMode = OperatingMode.Run;
                Programs.Current.SST();
                Run();
            }

        }

        private void ProcessSubroutine(byte label) => ProcessSubroutine(new byte[] { 1, label });
        #endregion

        #region math utilities
        private double AngularUnit2rad(double a) =>
            TrigMode == AngleUnits.Rad ? a :
            a / (TrigMode == AngleUnits.Deg ? 180 : 200) * Math.PI;

        private double rad2AngularUnit(double a) =>
            TrigMode == AngleUnits.Rad ? a :
            a * (TrigMode == AngleUnits.Deg ? 180 : 200) / Math.PI;

        #endregion

        #region load/save
        public void SaveProgram(string filename) => File.WriteAllText(filename, Programs.Current.Serialize());
        public void SaveProgram(string filename, int n) => File.WriteAllText(filename, Programs[n].Serialize());

        public void LoadProgram(string filename) => Programs.Current.Deserialize(File.ReadAllText(filename));
        public void LoadProgram(string filename, int n) => Programs[n].Deserialize(File.ReadAllText(filename));

        public void SaveMemories(string filename) => File.WriteAllText(filename, Memories.Serialize());
        public void LoadMemories(string filename) => Memories.Deserialize(File.ReadAllText(filename));

        private string ProfilePath()
        {
            string p = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Casasoft\\Calc");
            Directory.CreateDirectory(p);
            return p;
        }

        private string ConstantMemoryFilename() => Path.Combine(ProfilePath(), "ConstantMemory.Txt");

        private void SaveConstantMemory()
        {
            Json.ConstantMemory CM = new Json.ConstantMemory();
            CM.Program = Programs[0].GetForJson();
            CM.Memories = Memories.db;
            CM.Fix = Display.fix;
            CM.Eng = false;
            File.WriteAllText(ConstantMemoryFilename(),
                JsonSerializer.Serialize<Json.ConstantMemory>(CM, Memories.JsonOptions()));
        }

        private void LoadConstantMemory()
        {
            Json.ConstantMemory CM = new Json.ConstantMemory();
            CM = JsonSerializer.Deserialize<Json.ConstantMemory>(
                File.ReadAllText(ConstantMemoryFilename()), Memories.JsonOptions());
            Programs[0].SetFromJson(CM.Program);
            Memories.db = CM.Memories;
            Display.fix = CM.Fix;
        }
        #endregion

        #region built-in functions
        private void OP(byte functionCode)
        {
            switch (functionCode)
            {
                case 10:
                    Display.SetValue(Math.Sign(Display.GetValue()));
                    break;

                case 16:
                case 17:
                    Display.SetValue(999.99);
                    break;

                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                    Memories.Inc(functionCode - 20);
                    break;
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                    Memories.Dec(functionCode - 30);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
