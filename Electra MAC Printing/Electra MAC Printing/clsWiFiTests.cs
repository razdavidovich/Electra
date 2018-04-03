using Electra_MAC_Printing.classes;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Electra_MAC_Printing
{
    class clsWiFiTests
    {
        private  SerialPort workingPort;
        private string[] strSettingsArray = clsCommon.ReadSingleConfigValue("UnitSettings", "GetSetGeneralSettings", "Settings").Split(',');
        private string  strSettingsTimeOut = clsCommon.ReadSingleConfigValue("TimeOut", "GetSetGeneralSettings", "Settings");
        public clsWiFiTests()
        {
            workingPort = new SerialPort();
            workingPort.BaudRate = 115200;
            workingPort.Parity = Parity.Even;
            workingPort.StopBits = StopBits.One;
            workingPort.ReadTimeout = 1000;
        }
        public enum MB_address
        {
            AIR_CON_STATE = 0X310A,
            SW_VERSION = 0x0030,
            AP_PROD_MODE = 0x413A,
            STA_PROD_MODE = 0x413B,
            RESULT_PROD_MODE = 0x413C,
            MAC = 0x0010,
            IP = 0x4138,
            RSSI = 0x4141
        };
        public bool blnSuccessStatus { get; set; }

        #region "Functionalities for the status update"
        public void ConnectToTheDevice()
        {
            string IP_To_Test = GetIP();
            if (IP_To_Test == "192.168.1.1")
            { //Connected
                blnSuccessStatus = true;
            }
            else {
                blnSuccessStatus = false;
            }
            
        }

        public void StartSTAMode()
        {
            WriteSingleRegister((ushort)MB_address.AP_PROD_MODE, 0x5555);//enter Production Access Point Mode
            string response = ModbusSlaveResponce(8);
            if (response != "")
            {
                blnSuccessStatus = true;
            }
            else {
                blnSuccessStatus = false;
            }
        }
        #endregion

        private string GetIP()
        {

            byte numberOfdataregs = 2;
            byte crc = 2;
            int totalbytesofmessage = 4 + numberOfdataregs * 2 + crc; // 13 bytes

            /* Get Sw Version */

            byte[] OutBuffer = new byte[32];
            byte[] InBuff = new byte[32];
            byte[] a_aInput_swapedDataBytes = new byte[32];

            try
            {
                if (!workingPort.IsOpen)
                {
                    workingPort.Open();
                }

                Modbus_Read_HR((ushort)MB_address.IP, numberOfdataregs);
                Thread.Sleep(300);
                InBuff = ReadPort();
                string IP = "";
                if (CheckCRCFunc(InBuff, (ushort)(totalbytesofmessage - crc)))
                {
                    for (int i = 3; i < 7; i++)
                    {
                        if (i == 6)
                        {
                            IP += InBuff[i].ToString();
                            break;
                        }
                        IP += InBuff[i].ToString() + ".";
                    }
                    return IP;
                }
                else if (InBuff[1] == 0x86)
                {
                    //ExceptionText("- Modbus Exception to Read 2 Regs from 0x4138");
                    return "Exception 0x4138";
                }
            }
            catch (Exception ex)
            {
                //ExceptionFunc(ex);

            }
            return null;

        }
        private void Modbus_Read_HR(UInt16 StartAddress, byte numOfReg)
        {

            byte[] ReqBuffer = new byte[16];

            ReqBuffer[0] = 0x01;
            ReqBuffer[1] = 0x03;
            ReqBuffer[2] = (byte)(StartAddress >> 8);
            ReqBuffer[3] = (byte)StartAddress;
            ReqBuffer[4] = 0x00;
            ReqBuffer[5] = numOfReg;
            ReqBuffer[6] = (byte)CRC16Calculation(ReqBuffer, 6);
            ReqBuffer[7] = (byte)(CRC16Calculation(ReqBuffer, 6) >> 8);

            workingPort.Write(ReqBuffer, 0, 8);

        }
        private void Modbus_Write_SR(UInt16 Address, UInt16 Data)
        {

            byte[] ReqBuffer = new byte[16];

            ReqBuffer[0] = 0x01;
            ReqBuffer[1] = 0x06;
            ReqBuffer[2] = (byte)(Address >> 8);
            ReqBuffer[3] = (byte)Address;
            ReqBuffer[4] = (byte)(Data >> 8);
            ReqBuffer[5] = (byte)Data;
            ReqBuffer[6] = (byte)CRC16Calculation(ReqBuffer, 6);
            ReqBuffer[7] = (byte)(CRC16Calculation(ReqBuffer, 6) >> 8);
            workingPort.Write(ReqBuffer, 0, 8);

        }
        public ushort CRC16Calculation(byte[] a_paBuffer, int a_iSize)
        {
            ushort l_iBufferIndex; // The Bits Index (8bits)
            byte l_iIndex; // The Bits Index (8bits)
            byte l_iData; // The Current data
            ushort d_CrcPolynomial;
            //byte d_CrcPolynomial;
            ushort l_iCrc; // Calculative CRC
            l_iBufferIndex = 0;
            l_iCrc = 0xFFFF;
            d_CrcPolynomial = 0xA001;
            //l_iCrc = 0x0000;
            //d_CrcPolynomial = 0xD5;
            while ((a_iSize--) != 0) // Buffer size
            {
                l_iData = a_paBuffer[l_iBufferIndex++]; // Copy data to local member
                // CRC Loop on 8 bits
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                for (l_iIndex = 0; l_iIndex < 8; l_iIndex++)
                {
                    if (((l_iCrc ^ l_iData) & 0x01) != 0) // XOR and check bit ‘0’
                    {
                        l_iCrc >>= 1; // Shift
                        l_iCrc ^= d_CrcPolynomial;
                    }
                    else
                        l_iCrc >>= 1; // Shift
                    l_iData >>= 1; // Shift
                }
            }
            return l_iCrc; // Return the calculative CRC
        }

        public byte[] ReadPort()
        {
            int curr_byte = 0;
            byte[] buff = new byte[32];

            if (!workingPort.IsOpen)
            {
                workingPort.Open();
            }
            int BytesInPort = workingPort.BytesToRead;

            if (true)//Regular response 
            {
                try
                {
                    workingPort.Read(buff, 0, BytesInPort);
                    workingPort.Close();

                    return buff;
                }
                catch (TimeoutException ex)
                {
                    //form1.ExceptionFunc(ex);
                }
                catch (Exception ex)
                {
                    //form1.ExceptionFunc(ex);
                }
                finally
                {
                    if (workingPort.IsOpen)
                    {
                        workingPort.Close();
                    }
                }
            }
            return buff;
        }
        public bool CheckCRCFunc(byte[] l_aInBuffer, ushort startIndx)
        {
            ushort ReceivedCrC = (ushort)(l_aInBuffer[startIndx] + l_aInBuffer[startIndx + 1] * 256);

            ushort CalcCrC = CRC16Calculation(l_aInBuffer, (startIndx));
            if (ReceivedCrC == CalcCrC)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void WriteSingleRegister(ushort address, ushort value)
        {
            try
            {
                byte numberOfdataregs = 1;
                byte crc = 2;
                int totalbytesofmessage = 4 + numberOfdataregs * 2 + crc; // 13 bytes

                /* Get Sw Version */

                byte[] OutBuffer = new byte[32];
                byte[] InBuff = new byte[32];
                byte[] a_aInput_swapedDataBytes = new byte[32];

                if (!workingPort.IsOpen)
                {
                    workingPort.Open();
                }
                Modbus_Write_SR(address, value);

            }
            catch (Exception exp)
            {
                //ExceptionFunc(exp);
                return;
            }

        }
        private string ModbusSlaveResponce(int length)
        {
            byte[] InBuff = new byte[32];
            InBuff = ReadPort();
            string outstr = BitConverter.ToString(InBuff, 0, length);
            outstr = outstr.Replace('-', ' ');
            return outstr;
        }
    }
}
