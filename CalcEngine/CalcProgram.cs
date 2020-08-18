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
using System.Text;

namespace Casasoft.Calc
{
    [Guid("B52566CE-9681-40FF-9B61-C88DB453972C"),
    ClassInterface(ClassInterfaceType.None),
    ProgId("Casasoft.CalcProgram")]
    public class CalcProgram : ICalcProgram
    {
        private List<byte> Steps;
        public int Counter { get; private set; }
        private Stack<int> Returns;

        public CalcProgram()
        {
            Steps = new List<byte>();
            Steps.Add(0);
            Returns = new Stack<int>();
            Counter = 0;
            Labels = new Dictionary<byte, int>();
        }

        public void CP()
        {
            Steps.Clear();
            Returns.Clear();
            Counter = 0;
        }

        public void GTO(int step) => Counter = step;

        public void GTO_Label(byte label)
        {
            int addr;
            if(Labels.TryGetValue(label, out addr))
            {
                GTO(addr);
            }
        }

        public void RST() => Counter = 0;

        public void SST() => Counter++;

        public void BST() => Counter--;

        public void SBR(int step)
        {
            Returns.Push(Counter);
            GTO(step);
        }

        public void SBR_Label(byte label)
        {
            int addr;
            if (Labels.TryGetValue(label, out addr))
            {
                SBR(addr);
            }
        }
        public bool RTN()
        {
            bool stop = false;
            if (Returns.Count > 0)
                GTO(Returns.Pop());
            else
                stop = true;

            return stop;
        }

        public void INS() => Steps.Insert(Counter, 0);

        public void DEL() => Steps.RemoveAt(Counter);

        public byte GetCurrent() => Steps[Counter];

        public void SetCurrent(byte step) => Steps[Counter] = step;

        public string GetDisplayString() => $"{Counter:000} {GetCurrent():00} ";

        public bool IsEof() => Counter == Steps.Count - 1;

        public void Add() => Steps.Add(0);

        public void GoNext()
        {
            if (IsEof())
                Add();
            SST();
        }

        #region labels
        public Dictionary<byte, int> Labels { get; private set; }

        public void FindLabels()
        {
            Labels.Clear();
            InputProcessor inputProcessor = new InputProcessor(TestLabel);
            int oldCounter = Counter;
            for(Counter = 0; Counter < Steps.Count; ++Counter)
            {
                inputProcessor.ProcessKey(Steps[Counter]);
            }
            Counter = oldCounter;
        }

        private bool TestLabel(byte key, byte[] par)
        {
            if(key == 76)
            {
                Labels.Add(par[1], Counter);
            }
            return true;
        }
        #endregion

        #region load/save
        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();
            for(int j=0; j < Steps.Count; ++j)
            {
                sb.Append($"{Steps[j]:00},");
            }
            return sb.ToString();
        }

        public void Deserialize(string s)
        {
            Steps.Clear();
            string[] codes = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string st in codes)
            {
                Steps.Add(byte.Parse(st));
            }
            FindLabels();
        }
        #endregion
    }
}
