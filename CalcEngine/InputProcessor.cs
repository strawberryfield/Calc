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
using System.Linq;

namespace Casasoft.Calc
{
    public class InputProcessor
    {
        private byte currentCommand;
        private byte[] parameters;
        private Func<byte, byte[], bool> commandProcessor;

        private enum status
        {
            WaitForCommand, WaitForByteParameter, WaitForByteParameterNoInd,
            WaitForSecondDigit, WaitForDigitParameter, WaitForAddress
        }
        private status currentStatus;

        private byte[] CommandsWithoutParameters = {
            11,12,13,14,15,16,17,18,19,10,
            21,22,23,24,25,26,27,28,29,20,
            31,32,33,34,35,37,38,39,30,
            41,45,46,47,
            51,52,53,54,55,56,57,59,50,
            65,66,68,60,
            75,78,79,70,
            81,85,88,89,80,
            91,93,94,95,96,98,99,90
        };

        private byte[] CommandsWithByteParameter = {
            36, 42, 43, 44, 48, 49, 40, 69,
            62, 63, 64, 72, 73, 74, 82, 83, 84
        };

        private Dictionary<byte, byte> IndirectTransform = new Dictionary<byte, byte>()
        {
            { 36, 62 },
            { 42, 72 },
            { 43, 73 },
            { 44, 74 },
            { 48, 63 },
            { 49, 64 },
            { 69, 84 }
       };

        public InputProcessor(Func<byte, byte[], bool> commandProcessor)
        {
            currentStatus = status.WaitForCommand;
            parameters = new byte[5];
            this.commandProcessor = commandProcessor;
        }

        public void ProcessKey(byte key)
        {
            switch (currentStatus)
            {
                case status.WaitForCommand:
                    if (key < 10 || CommandsWithoutParameters.Contains(key))
                    {
                        commandProcessor(key, null);
                    }
                    else if (CommandsWithByteParameter.Contains(key))
                    {
                        currentCommand = key;
                        parameters[0] = 1;
                        parameters[1] = 0;
                        currentStatus = status.WaitForByteParameter;
                    }
                    break;
                case status.WaitForByteParameter:
                    if (key == 40 && IndirectTransform.ContainsKey(currentCommand))
                    {
                        currentCommand = IndirectTransform[currentCommand];
                        currentStatus = status.WaitForByteParameterNoInd;
                    }
                    else
                    {
                        parameters[1] = key;
                        if (key > 9)
                        {
                            commandProcessor(currentCommand, parameters);
                            currentStatus = status.WaitForCommand;
                        }
                        else
                        {
                            currentStatus = status.WaitForSecondDigit;
                        }
                    }
                    break;
                case status.WaitForByteParameterNoInd:
                    parameters[1] = key;
                    if (key > 9)
                    {
                        commandProcessor(currentCommand, parameters);
                        currentStatus = status.WaitForCommand;
                    }
                    else
                    {
                        currentStatus = status.WaitForSecondDigit;
                    }
                    break;
                case status.WaitForSecondDigit:
                    if(key > 9)
                    {
                        commandProcessor(currentCommand, parameters);
                        currentStatus = status.WaitForCommand;
                        ProcessKey(key);
                    }
                    else
                    {
                        parameters[1] = Convert.ToByte(parameters[1] * 10 + key);
                        commandProcessor(currentCommand, parameters);
                        currentStatus = status.WaitForCommand;
                    }
                    break;
                case status.WaitForDigitParameter:
                    break;
                case status.WaitForAddress:
                    break;
                default:
                    break;
            }
        }
    }
}
