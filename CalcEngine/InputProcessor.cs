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
using System.Linq;
using System.Runtime.InteropServices;

namespace Casasoft.Calc
{
    [ComVisible(false)]
    public class InputProcessor
    {
        private byte currentCommand;
        private byte[] parameters;
        private Func<byte, byte[], bool> commandProcessor;
        internal OperatingMode OperatingMode;
        private bool AfterINV;

        private enum status
        {
            WaitForCommand, WaitForByteParameter, WaitForByteParameterNoInd,
            WaitForSecondDigit, WaitForDigitParameter, WaitForAddress,
            WaitForLabel
        }
        private status currentStatus;

        private byte[] CommandsWithoutParameters =
        {
            11,12,13,14,15,16,17,18,19,10,
            21,23,24,25,26,27,28,29,20,
            31,32,33,34,35,37,38,39,30,
            41,45,46,47,
            51,52,53,54,55,56,57,59,50,
            65,66,68,60,
            75,78,79,70,
            81,85,88,89,80,
            91,92,93,94,95,96,98,99,90
        };

        private byte[] CommandsWithByteParameter =
        {
            36, 42, 43, 44, 48, 49, 40, 69,
            62, 63, 64, 72, 73, 74, 82, 83, 84
        };

        private byte[] JumpCommands =
        {
            61, 71, 67, 77
        };

        private byte[] CommandsWithSingleDigitParameter =
        {
            86, 87, 97, 58
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
            OperatingMode = OperatingMode.Interactive;
            AfterINV = false;
        }

        public void ProcessKey(byte key)
        {
            switch (currentStatus)
            {
                case status.WaitForCommand:
                    // Transforms INV SBR to RTN
                    if (AfterINV)
                    {
                        if (key == 71)
                        {
                            key = 92;
                        }
                        else
                        {
                            commandProcessor(22, null);
                            if(key == 58)
                            {
                                // INV Fix has no parameters
                                commandProcessor(key, new byte[] { 1, 0 });
                                return;
                            }
                        }
                        AfterINV = false;
                    }

                    // Other commands
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
                    else if (key == 76)
                    {
                        currentCommand = key;
                        parameters[0] = 1;
                        parameters[1] = 0;
                        currentStatus = status.WaitForLabel;
                    }
                    else if (JumpCommands.Contains(key))
                    {
                        currentCommand = key;
                        parameters[0] = 1;
                        currentStatus = status.WaitForAddress;
                    }
                    else if (CommandsWithSingleDigitParameter.Contains(key))
                    {
                        currentCommand = key;
                        parameters[0] = 1;
                        parameters[1] = 0;
                        currentStatus = status.WaitForDigitParameter;
                    }
                    else if (key == 22)
                    {
                        AfterINV = true;
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
                        parameters[parameters[0]] = key;
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
                    parameters[parameters[0]] = key;
                    if (key > 9)
                    {
                        HandleSecondParameter();
                    }
                    else
                    {
                        currentStatus = status.WaitForSecondDigit;
                    }
                    break;
                case status.WaitForSecondDigit:
                    if (key > 9)
                    {
                        commandProcessor(currentCommand, parameters);
                        currentStatus = status.WaitForCommand;
                        ProcessKey(key);
                    }
                    else
                    {
                        parameters[parameters[0]] = Convert.ToByte(parameters[parameters[0]] * 10 + key);
                        HandleSecondParameter();
                    }
                    break;
                case status.WaitForDigitParameter:
                    if (key == 40)
                    {
                        parameters[0] = 2;
                        parameters[1] = key;
                        currentStatus = status.WaitForByteParameterNoInd;
                    }
                    else
                    {
                        parameters[0] = 1;
                        parameters[1] = key;
                        HandleSecondParameter();
                    }
                    break;
                case status.WaitForAddress:
                    if (key == 40)
                    {
                        if (currentCommand == 61)
                        {
                            currentCommand = 83;
                        }
                        else
                        {
                            parameters[parameters[0]] = key;
                            parameters[0]++;
                        }
                        currentStatus = status.WaitForByteParameterNoInd;
                    }
                    else if (key < 10)
                    {
                        parameters[parameters[0]] = key;
                        parameters[0]++;
                        currentStatus = status.WaitForByteParameterNoInd;
                    }
                    else
                    {
                        parameters[parameters[0]] = key;
                        commandProcessor(currentCommand, parameters);
                        currentStatus = status.WaitForCommand;
                    }
                    break;
                case status.WaitForLabel:
                    parameters[1] = key;
                    commandProcessor(currentCommand, parameters);
                    currentStatus = status.WaitForCommand;
                    break;
                default:
                    break;
            }
        }

        private void HandleSecondParameter()
        {
            if ((currentCommand == 87 || currentCommand == 97) && parameters[0] <= 2)
            {
                parameters[0]++;
                currentStatus = status.WaitForAddress;
            }
            else
            {
                commandProcessor(currentCommand, parameters);
                currentStatus = status.WaitForCommand;
            }

        }
    }
}
