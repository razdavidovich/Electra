using Assembly.Software.Utilities;
using Electra_MAC_Printing.classes;
using Electra_MAC_Printing.classes.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWIN = Infragistics.Win;
using IWUG = Infragistics.Win.UltraWinGrid;
using System.IO.Ports;
using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;
using System.Text.RegularExpressions;
using SimpleWifi;
using RestSharp;
using System.Net;

namespace Electra_MAC_Printing
{
    public partial class frmAppWizard : Form
    {

        clsCommon clsCommon = new clsCommon();
        clsVariables clsVariables = new clsVariables();
        clsAppWizardAlign clsAppWizardAlign = new clsAppWizardAlign();
        clsAppWizardBAL clsappWizardBAL = new clsAppWizardBAL();
        //clsSettingsBAL clsSettingsBAL = new clsSettingsBAL();

        private Dictionary<string, object> dicLanguageCaptions;

        string strLanguage = clsCommon.ReadSingleConfigValue("Default", "LanguageCodes", "LanguageSupport");
        private string[] strSettingsArray;

        private bool blnTimerRunning = false;

        public frmAppWizard()
        {
            InitializeComponent();

            /*Settings For Log File path for Debug the Errors.*/
            clsApplicationLogFile.LogFilePath = clsCommon.ReadSingleConfigValue("LogFilePath", "LogSettings", "Settings");
            clsApplicationLogFile.LogFileName = clsCommon.ReadSingleConfigValue("LogFileName", "LogSettings", "Settings");
            clsApplicationLogFile.LogFileExtension = clsCommon.ReadSingleConfigValue("LogFileExtension", "LogSettings", "Settings");
            clsCommon.clsApplicationLogFileWriteLog(null, "Form Login Load : Success");
            clsVariables.variableClearSetDefaultValues();
            LoadLanugageCaptions();
        }

        #region "Modbus"

        private int ConvertRSSI(ushort modbusValue)
        {
                string hexValue = modbusValue.ToString("X").ToUpper().PadLeft(4, '0');
                return Convert.ToInt32(hexValue, 16);

        }

        private string ConvertToIPAddress(ushort[] modbusValues)
        {
            StringBuilder sb = new StringBuilder();

            //Loop the array values
            foreach (ushort value in modbusValues)
            {
                string hexValue = value.ToString("X").ToUpper().PadLeft(4, '0');
                sb.Append(Convert.ToInt32(hexValue.Substring(0, 2), 16).ToString());
                sb.Append(".");
                sb.Append(Convert.ToInt32(hexValue.Substring(2), 16).ToString());
                sb.Append(".");
            }

            //Remove the last "." at the end
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();

        }

        private string ConvertToMACAddress(ushort[] modbusValues)
        {
            StringBuilder sb = new StringBuilder();

            //Setup the Electra prefix
            sb.Append("E");

            //Loop the array values
            foreach (ushort value in modbusValues)
            {
                string hexValue = value.ToString("X").ToUpper().PadLeft(4, '0');
                sb.Append(hexValue.Substring(2));
                sb.Append(hexValue.Substring(0, 2));
            }

            return sb.ToString();

        }

        private string ConvertToSerialNumber(ushort[] modbusValues)
        {
            StringBuilder sb = new StringBuilder();

            //Loop the array values
            foreach (ushort value in modbusValues)
            {
                string hexValue = value.ToString("X").ToUpper().PadLeft(4, '0');
                if (hexValue == "FFFF")
                {
                    return "0000000000";
                }

                sb.Append(HexString2Ascii(hexValue.Substring(2)));
                sb.Append(HexString2Ascii(hexValue.Substring(0, 2)));
            }

            return sb.ToString();

        }

        private string HexString2Ascii(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hexString.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }

        private ushort[] ReadModbusRegisters(byte slaveId, ushort startAddress, ushort numberOfPoints)
        {           
            try
            {
                strSettingsArray = clsCommon.ReadSingleConfigValue("UnitSettings", "GetSetGeneralSettings", "Settings").Split(',');
                string strSettingsTimeOut = clsCommon.ReadSingleConfigValue("TimeOut", "GetSetGeneralSettings", "Settings");
                using (SerialPort port = new SerialPort(strSettingsArray[0]))
                {
                    // configure serial port
                    port.BaudRate = Convert.ToInt32(strSettingsArray[1]);
                    port.DataBits = Convert.ToInt32(strSettingsArray[3]);
                    port.Parity = (Parity)Enum.Parse(typeof(Parity), strSettingsArray[2]);
                    port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), strSettingsArray[4]);
                    port.ReadTimeout = Convert.ToInt32(strSettingsTimeOut);
                    port.Open();

                    // create modbus master
                    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                    // Read the registers
                    var values = master.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);
                    return values;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void WriteModbusRegisters(byte slaveId, ushort writeAddress, ushort value)
        {
            try
            {
                strSettingsArray = clsCommon.ReadSingleConfigValue("UnitSettings", "GetSetGeneralSettings", "Settings").Split(',');
                string strSettingsTimeOut = clsCommon.ReadSingleConfigValue("TimeOut", "GetSetGeneralSettings", "Settings");
                using (SerialPort port = new SerialPort(strSettingsArray[0]))
                {
                    // configure serial port
                    port.BaudRate = Convert.ToInt32(strSettingsArray[1]);
                    port.DataBits = Convert.ToInt32(strSettingsArray[3]);
                    port.Parity = (Parity)Enum.Parse(typeof(Parity), strSettingsArray[2]);
                    port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), strSettingsArray[4]);
                    port.ReadTimeout = Convert.ToInt32(strSettingsTimeOut);
                    port.Open();

                    // create modbus master
                    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                    // Read the registers
                    master.WriteSingleRegister(slaveId, writeAddress, value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void clearData()
        {
            txtUnitSerialNumber.Text = string.Empty;
            txtUnitSerialNumber.BackColor = Color.Red;

            txtunitMacAddress.Text = string.Empty;
            txtunitMacAddress.BackColor = Color.Red;

            BtnRePrint.Hide();

            blnTimerRunning = false;

            Application.DoEvents();
        }

        private void setUnitInformationAndPrint()
        {
            // Set background
            txtUnitSerialNumber.BackColor = Color.Green;
            txtunitMacAddress.BackColor = Color.Green;

            // Print label
            printLabel(txtUnitSerialNumber.Text, txtunitMacAddress.Text);

            // Allow re-print
            BtnRePrint.Show();

            Application.DoEvents();

        }

        private void tmrModbus_Tick(object sender, EventArgs e)
        {
            if (!blnTimerRunning)
            {
                MethodInvoker tmrDelegate = delegate
                {
                    tmrModbusTickWorker();
                };
                tmrDelegate.BeginInvoke(null, null);
            }
        }

        #endregion
        private void tmrModbusTickWorker()
        {
            int i;

            this.BeginInvoke((MethodInvoker)async delegate
            {
                try
                {

                    blnTimerRunning = true;
                    string strPrinterName = clsCommon.ReadSingleConfigValue("PrinterName", "GetSetGeneralSettings", "Settings");
                    string strMACMBAddress = clsCommon.ReadSingleConfigValue("MACAddress", "GetSetGeneralSettings", "Settings");
                    string strSTAIPAddress = clsCommon.ReadSingleConfigValue("STA_IP_Address", "GetSetGeneralSettings", "Settings");
                    string strIPAddressRegEx = clsCommon.ReadSingleConfigValue("IPAddressRegEx", "GetSetGeneralSettings", "Settings");
                    string strSerialNumberAddress = clsCommon.ReadSingleConfigValue("SerialNumberAddress", "GetSetGeneralSettings", "Settings");
                    string strModbusSlaveAddress = clsCommon.ReadSingleConfigValue("ModbusSlaveAddress", "GetSetGeneralSettings", "Settings");

                    // Read Serial number (expecting 6153320000 from test unit)
                    var serialValue = ReadModbusRegisters(Convert.ToByte(strModbusSlaveAddress), Convert.ToUInt16(strSerialNumberAddress, 16), 5);
                    var strSerialNumber = ConvertToSerialNumber(serialValue);

                    // Exit AP & STA mode (for unit recovery in case of any runtime issues)
                    WriteModbusRegisters(Convert.ToByte(strModbusSlaveAddress), (ushort)0x413A, (ushort)0xAAAA);
                    WriteModbusRegisters(Convert.ToByte(strModbusSlaveAddress), (ushort)0x413B, (ushort)0xAAAA);
                    await Task.Delay(3000);

                    if (BtnRePrint.Visible)
                    {
                        blnTimerRunning = false;
                        return;
                    }

                    //===========================================
                    // STA TESTS
                    //===========================================

                    // Read current STA IP address
                    var ipAddressValue = ReadModbusRegisters(Convert.ToByte(strModbusSlaveAddress), Convert.ToUInt16(strSTAIPAddress, 16), 2);
                    var strIPAddress = ConvertToIPAddress(ipAddressValue);

                    if (strIPAddress == "192.168.1.1")
                    {
                        // todo: Notify the UI

                        // Set the unit to STA mode
                        WriteModbusRegisters(Convert.ToByte(strModbusSlaveAddress), (ushort)0x413B, (ushort)0x5555);

                        // Delay execution for up to 12 seconds until the unit recieves an IP address
                        for (i = 0; i < 5; i++)
                        {

                            await Task.Delay(5000);

                            // Read the RSSI value
                            var rssi = ReadModbusRegisters(Convert.ToByte(strModbusSlaveAddress), (ushort)0x4141, 1);

                            // Valdate the RSSI value
                            if (ConvertRSSI(rssi[0]) < 80) { break; }
                        }

                        if (i > 4) { throw new Exception("Invalid RSSI value"); }

                        // Read the STA new IP address
                        ipAddressValue = ReadModbusRegisters(Convert.ToByte(strModbusSlaveAddress), Convert.ToUInt16(strSTAIPAddress, 16), 2);
                        strIPAddress = ConvertToIPAddress(ipAddressValue);

                        // Validate the new IP address                        
                        if (!(Regex.Match(strIPAddress, strIPAddressRegEx).Success)) { throw new Exception("Invalid STA IP Address"); }

                        //Exit STA mode
                        WriteModbusRegisters(Convert.ToByte(strModbusSlaveAddress), (ushort)0x413B, (ushort)0xAAAA);
                        await Task.Delay(5000);                   

                    } else
                    {
                        // Reset the unit address
                        WriteModbusRegisters(Convert.ToByte(strModbusSlaveAddress), (ushort)0x413B, (ushort)0xAAAA);

                        throw new Exception("Invalid Initial IP Address (expected 192.168.1.1)");
                    }

                    //===========================================
                    // AP TESTS
                    //===========================================

                    // todo: Notify the UI

                    // Set the unit to STA mode
                    WriteModbusRegisters(Convert.ToByte(strModbusSlaveAddress), (ushort)0x413A, (ushort)0x5555);

                    // Read MAC ADDRESS (expecting A8 1B 6A 9C 7A 9C from test unit)
                    var macValue = ReadModbusRegisters(Convert.ToByte(strModbusSlaveAddress), Convert.ToUInt16(strMACMBAddress, 16), 3);
                    var strMACAddress = ConvertToMACAddress(macValue);

                    // Notify the UI of the MAC address 

                    // Setup the computer WiFi object
                    var wifi = new Wifi();
                    if (wifi.NoWifiAvailable) { throw new Exception("NO WIFI CARD WAS FOUND"); }

                    var strSSID = clsCommon.ReadSingleConfigValue("APName", "GetSetGeneralSettings", "Settings") + strMACAddress.Substring(7);
                    var strAPPassword = clsCommon.ReadSingleConfigValue("APPassword", "GetSetGeneralSettings", "Settings");

                    var connectedToAP = false;
                    Console.WriteLine("Looking for SSID {0}", strSSID);

                    // Loop and wait for AP to be detected
                    for (i = 0; (i < 11) && !(connectedToAP); i++)
                    {
                        await Task.Delay(5000);
                        IEnumerable<AccessPoint> accessPoints = wifi.GetAccessPoints().OrderByDescending(ap => ap.SignalStrength);

                        foreach (AccessPoint ap in accessPoints)
                            if (ap.Name == strSSID)
                            {
                                AuthRequest authRequest = new AuthRequest(ap)
                                {
                                    Password = strAPPassword
                                };
                                if (!ap.Connect(authRequest)) {
                                    await Task.Delay(3000);
                                } else
                                {
                                    connectedToAP = true;
                                    break;
                                }
                            }
                    }

                    if (i > 10)
                    {
                        throw new Exception("Unable to detect the unit AP");
                    }
                    else
                    {
                        var client = new RestClient("http://192.168.1.1");
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("__SL_P_UTO", "ProdTokenAP");
                        IRestResponse response = client.Execute(request);
                        if (response.StatusCode != HttpStatusCode.NoContent) {
                            throw new Exception(string.Format("Invalid http response code - {0} (expected 204)",response.StatusCode.ToString()));
                        }
                        await Task.Delay(3000);

                        var prod = ReadModbusRegisters(Convert.ToByte(strModbusSlaveAddress), (ushort)0x413C, 1);
                        if (prod[0] != (ushort)0x0055) { throw new Exception("Invalid PROD result (expected 0x0055"); }
                    }

                    // Disconnect from the AP
                    wifi.Disconnect();

                    // Exit AP mode
                    WriteModbusRegisters(Convert.ToByte(strModbusSlaveAddress), (ushort)0x413A, (ushort)0xAAAA);
                    await Task.Delay(1000);

                    if (strMACAddress == "EFFFFFFFFFFFF") { throw new Exception("Invalid MAC address"); }

                    txtunitMacAddress.Text = strMACAddress;
                    txtUnitSerialNumber.Text = strSerialNumber;

                    if (txtunitMacAddress.TextLength > 0)
                    {
                        if (!BtnRePrint.Visible)
                        {
                            if(!string.IsNullOrEmpty(strPrinterName))
                            {
                                setUnitInformationAndPrint();                               
                            }
                            else
                            {
                                clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("PrinterNameNullOrEmptyErrorTextCaption_{0}", strLanguage)]);
                                clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "16");
                                clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("PrinterNameNullOrEmptyErrorTitleCaption_{0}", strLanguage)]);
                                clsCommon.commonGeneralDisplayMessageBox(0);
                            }                          
                        }
                    }
                    blnTimerRunning = false;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0:HH:mm:ss tt} {1}", DateTime.Now, ex.ToString());
                    clearData();
                }
            });

        }

        #region "Print Label"
        private void printLabel(string serialNumber, string unitMACAddress)
        {
            // Get ZPL and printer name from the settings
            string printerName = clsCommon.ReadSingleConfigValue("PrinterName", "GetSetGeneralSettings", "Settings");

            // Setup the ZPL to print
            string zpl = string.Format(clsCommon.ReadSingleConfigValue("ZPL", "GetSetGeneralSettings", "Settings"), serialNumber,unitMACAddress,unitMACAddress.Substring(0,7),unitMACAddress.Substring(7));

            // Print the label
            clsPrintUtility.SendStringToPrinter(printerName, zpl);

            // Svae the print information to the logbook

            SaveLogBookDetails(serialNumber, unitMACAddress);

        }

        #endregion

        #region frmAppWizard_Load
        /****************************************************************************************************
         * NAME         : frmAppWizard_Load                                                                 *
         * DESCRIPTION  : Initial Form load function                                                        *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 08Feb18                                                                          *
         ****************************************************************************************************/
        private void frmAppWizard_Load(object sender, EventArgs e)
        {
            utcAppWizard.Tabs[clsVariables.strFrmAppWizardActiveTabKey].Selected = true;
            //utcAppWizard.Tabs["login"].Selected = true;
            formPageControlsResize();
            HideShowToolBar(0);
            txtEmpNo.Focus();
            txtEmpNo.Text = "";
            LoadControlCaption();
            //Adding_uGridWorkDetails_From_Form_Load();

            // RunStartMarking();
        }
        #endregion

        #region LoadLanugageCaptions
        /****************************************************************************************************
         * NAME         : LoadLanugageCaptions                                                              *
         * DESCRIPTION  : Load Language Captions                                                            *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 24Feb18                                                                           *
         ****************************************************************************************************/
        private void LoadLanugageCaptions()
        {
            try
            {
                string strCaption = clsCommon.ReadSingleConfigValue("Default", "LanguageCodes", "LanguageSupport");
                DataTable dt = (DataTable)clsappWizardBAL.getLanguageCapion(1, strCaption);

                int intRowCount = dt.Rows.Count;
                dicLanguageCaptions = new Dictionary<string, object>();

                if (0 < intRowCount)
                {
                    for (int i = 0; i < intRowCount; i++)
                    {
                        dicLanguageCaptions.Add((string)dt.Rows[i]["vchKey"], (string)dt.Rows[i]["vchTranslation"]);
                    }

                }
            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }
        }
        #endregion

        #region LoadControlCaption
        /****************************************************************************************************
         * NAME         : LoadControlCaption                                                                *
         * DESCRIPTION  : Load Control Captions                                                             *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 24Feb18                                                                           *
         ****************************************************************************************************/
        private void LoadControlCaption()
        {
            lblFormHead.Text = (string)dicLanguageCaptions[string.Format("loginHeaderCaption_{0}", strLanguage)];
            lblHeadlogin.Text = (string)dicLanguageCaptions[string.Format("loginHeaderemployeeCardCaption_{0}", strLanguage)];
            btnLogin.Text = (string)dicLanguageCaptions[string.Format("loginButtonCaption_{0}", strLanguage)];
            lblUnitSerialNumber.Text = (string)dicLanguageCaptions[string.Format("NewBatchUnitSerialNumberCaption_{0}", strLanguage)];
            lblUnitMacAddress.Text = (string)dicLanguageCaptions[string.Format("NewBatchUnitMacAddressCaption_{0}", strLanguage)];
            lblUnitMacAddress.Text = (string)dicLanguageCaptions[string.Format("NewBatchUnitMacAddressCaption_{0}", strLanguage)];
            LBL_LogBook_From.Text = (string)dicLanguageCaptions[string.Format("LBLLogBookFromCaption_{0}", strLanguage)];
            LBL_LogBook_To.Text = (string)dicLanguageCaptions[string.Format("LBLLogBookToCaption_{0}", strLanguage)];
            uBTN_LogBook_Excel.Text = (string)dicLanguageCaptions[string.Format("BtnLogBookExportCaption_{0}", strLanguage)];
            uBTN_LogBook_Filter.Text = (string)dicLanguageCaptions[string.Format("BtnLogBookFilterCaption_{0}", strLanguage)];
            BtnRePrint.Text = (string)dicLanguageCaptions[string.Format("BtnRePrintCaption_{0}", strLanguage)];
            lblErrorMesaage.Text = (string)dicLanguageCaptions[string.Format("LoginLabelErrorMessageCaption_{0}", strLanguage)];

            uToolBarManagerControl.Tools[0].SharedProps.Caption = (string)dicLanguageCaptions[string.Format("ToolBarNewBatchCaption_{0}", strLanguage)];
            uToolBarManagerControl.Tools[1].SharedProps.Caption = (string)dicLanguageCaptions[string.Format("ToolBarEndBatchCaption_{0}", strLanguage)];
            uToolBarManagerControl.Tools[3].SharedProps.Caption = (string)dicLanguageCaptions[string.Format("ToolBarLogbookCaption_{0}", strLanguage)];
            uToolBarManagerControl.Tools[4].SharedProps.Caption = (string)dicLanguageCaptions[string.Format("ToolBarSettingsCaption_{0}", strLanguage)];
            uToolBarManagerControl.Tools[5].SharedProps.Caption = (string)dicLanguageCaptions[string.Format("ToolBarLogOffCaption_{0}", strLanguage)];

        }
        #endregion

        #region frmAppWizard_Resize
        /****************************************************************************************************
         * NAME         : frmAppWizard_Resize                                                                 *
         * DESCRIPTION  : Initial Form load function                                                        *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 08Feb18                                                                          *
         ****************************************************************************************************/
        private void frmAppWizard_Resize(object sender, EventArgs e)
        {
            formPageControlsResize();
        }
        #endregion

        #region formPageControlsResize
        /****************************************************************************************************
         * NAME         : formPageControlsResize                                                            *
         * DESCRIPTION  : Control resize function based on page size and Parent control size                *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 08Feb18                                                                           *
         ****************************************************************************************************/
        private void formPageControlsResize()
        {
            string strAppWizard = null;

            try
            {
                strAppWizard = utcAppWizard.SelectedTab.Key.ToString();
            }
            catch
            {
                strAppWizard = clsVariables.strFrmAppWizardActiveTabKey;
            }
            
            switch (strAppWizard)
            {
                case "login":
                    clsAppWizardAlign.LoginFormResize(pnlLoginMain, pnlLogin, lblFormHead, PictLoginLogo);
                    break;
                case "newbatch":
                    clsAppWizardAlign.newBatchFormResize(panelMainNewBatch, panelLeftNewBatch, panelRightNewBatch);
                    break;               
            }

        }

        #endregion

        #region btnLogin_Click
        /****************************************************************************************************
         * NAME         : clearTools                                                                        *
         * DESCRIPTION  : Click Event call from Login Button control                                        *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string strUserRFID = txtEmpNo.Text.Trim();
                if (!string.IsNullOrEmpty(strUserRFID))
                {
                    bool blnLogin = LoginProcessData(strUserRFID);

                    if (blnLogin)
                    {
                        CheckUserRights();
                        panelFormHeader.Visible = true;
                        HideShowToolBar(0);
                        ProcessPreviousNextCurrentTab(true);
                        Application.DoEvents();
                        tmrModbus.Enabled = true;
                    }
                    else
                    {

                        lblErrorMesaage.Text = "Invalid Employee Number";
                        lblErrorMesaage.Visible = true;
                        txtEmpNo.Focus();
                    }
                }
                else
                {
                    lblErrorMesaage.Text = "Invalid Employee Number";
                    lblErrorMesaage.Visible = true;
                    txtEmpNo.Focus();

                }
            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
                lblErrorMesaage.Text = "Error occured.Please see the log file.";
                lblErrorMesaage.Visible = true;
            }
        }
        #endregion

        #region LoginProcessData
        /****************************************************************************************************
         * NAME         : LoginProcessData                                            *
         * DESCRIPTION  : Get the Login Detail Process                    *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        private bool LoginProcessData(string returnData)
        {
            try
            {
                bool blnLogin = clsappWizardBAL.checkUserLogin(returnData);
                return blnLogin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ProcessPreviousNextCurrentTab
        /****************************************************************************************************
         * NAME         : ProcessPreviousNextCurrentTab                                                     *
         * DESCRIPTION  : To active the current tab                                                         *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb15                                                                           8
         ****************************************************************************************************/
        private void ProcessPreviousNextCurrentTab(bool PreviousNext, string strActiveTabKey = null, string strChangeActiveTab = null)
        {

            /*Variable Declaration*/
            string strTabKey = null;


            if (!string.IsNullOrEmpty(strActiveTabKey))
            {
                strTabKey = strActiveTabKey;
            }
            else
            {
                strTabKey = clsVariables.strFrmAppWizardActiveTabKey;
            }


            if (PreviousNext)
            {
                switch (strTabKey)
                {
                    case "login":
                        utcAppWizard.Tabs["newbatch"].Selected = true;
                        break;
                    case "newbatch":
                        utcAppWizard.Tabs["startbatch"].Selected = true;
                        break;
                    case "startbatch":
                        utcAppWizard.Tabs["endbatch"].Selected = true;
                        break;
                }
            }
            else
            {
                switch (strTabKey)
                {
                    case "endbatch":
                        utcAppWizard.Tabs["startbatch"].Selected = true;
                        break;

                }

            }

            clsVariables.strFrmAppWizardActiveTabKey = utcAppWizard.ActiveTab.Key.ToString();

        }
        #endregion

        #region utcAppWizard_SelectedTabChanged
        /****************************************************************************************************
         * NAME         : utcAppWizard_SelectedTabChanged                                                   *
         * DESCRIPTION  : Tab Selection changed Event call from ultraTab control                            *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        private void utcAppWizard_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            formPageControlsResize();
        }
        #endregion

        #region CheckUserRights
        /****************************************************************************************************
         * NAME         : CheckUserRights                                                                   *
         * DESCRIPTION  : Need to check the users and having rights for the screens.                        *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb18                                                                          *
         ****************************************************************************************************/
        private void CheckUserRights()
        {
            try
            {
                string[] strSplitTexts = clsCommon.ReadSingleConfigValue("adminRights", "UserRoleSettings", "Settings").Split(',');

                foreach (string strCheck in strSplitTexts)
                {
                    if (clsVariables.intLoginRoleID.ToString() == strCheck)
                    {
                        clsVariables.blnToolBarSettingsVisiable = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }
        }
        #endregion

        #region HideShowToolBar
        /****************************************************************************************************
         * NAME         : HideShowToolBar                                                                   *
         * DESCRIPTION  : Hide and Show the Appwizard Tab Tool Bar Settings control.                        *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb2018                                                                           *
         * PARAMETERS:-   None                                                                              *
         ****************************************************************************************************/
        private void HideShowToolBar(int intHideShow = 0)
        {

            for (int i = 0; i < uToolBarManagerControl.Tools.Count; i++)
            {
                if ("settings" == uToolBarManagerControl.Tools[i].Key.ToString())
                {
                    uToolBarManagerControl.Tools[i].SharedProps.Visible = clsVariables.blnToolBarSettingsVisiable;
                }
            }

            switch (intHideShow)
            {
                case 0:
                    uToolBarManagerControl.Tools["newbatch"].SharedProps.Visible = true;
                    uToolBarManagerControl.Tools["newbatch"].SharedProps.Enabled = false;
                    uToolBarManagerControl.Tools["endbatch"].SharedProps.Visible = false;
                    uToolBarManagerControl.Tools["logbook"].SharedProps.Enabled = true;
                    uToolBarManagerControl.Tools["settings"].SharedProps.Enabled = true;
                    uToolBarManagerControl.Tools["logoff"].SharedProps.Enabled = true;
                    break;
                case 1:
                    uToolBarManagerControl.Tools["newbatch"].SharedProps.Visible = true;
                    uToolBarManagerControl.Tools["newbatch"].SharedProps.Enabled = true;
                    uToolBarManagerControl.Tools["endbatch"].SharedProps.Visible = false;
                    uToolBarManagerControl.Tools["logbook"].SharedProps.Enabled = true;
                    uToolBarManagerControl.Tools["settings"].SharedProps.Enabled = true;
                    uToolBarManagerControl.Tools["logoff"].SharedProps.Enabled = true;
                    break;
                case 2:
                    uToolBarManagerControl.Tools["newbatch"].SharedProps.Visible = false;
                    uToolBarManagerControl.Tools["newbatch"].SharedProps.Enabled = false;
                    uToolBarManagerControl.Tools["endbatch"].SharedProps.Visible = true;
                    uToolBarManagerControl.Tools["logbook"].SharedProps.Enabled = false;
                    uToolBarManagerControl.Tools["settings"].SharedProps.Enabled = false;
                    uToolBarManagerControl.Tools["logoff"].SharedProps.Enabled = false;
                    break;
                case 3:
                    uToolBarManagerControl.Tools["newbatch"].SharedProps.Visible = true;
                    uToolBarManagerControl.Tools["newbatch"].SharedProps.Enabled = true;
                    uToolBarManagerControl.Tools["endbatch"].SharedProps.Visible = false;
                    uToolBarManagerControl.Tools["logbook"].SharedProps.Enabled = true;
                    uToolBarManagerControl.Tools["settings"].SharedProps.Enabled = true;
                    uToolBarManagerControl.Tools["logoff"].SharedProps.Enabled = true;
                    break;
                case 4:
                    uToolBarManagerControl.Tools["endbatch"].SharedProps.Enabled = false;
                    break;
                case 5:
                    uToolBarManagerControl.Tools["endbatch"].SharedProps.Enabled = true;
                    break;
            }

        }


        #endregion

        #region txtEmpNo_KeyDown
        /****************************************************************************************************
         * NAME         : txtEmpNo_KeyDown                                                                  *
         * DESCRIPTION  : Click Event call from Textbox Key down Control                                    *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb2018                                                                         *
         * PARAMETERS:-   None                                                                              *
         ****************************************************************************************************/
        private void txtEmpNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();

            }
        }
        #endregion

        #region uToolBarManagerControl_ToolClick
        /****************************************************************************************************
         * NAME         : uToolBarManagerControl_ToolClick                                                *
         * DESCRIPTION  : Click Event call from uToolbarsManagerControl control                             *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb18                                                                          
         ****************************************************************************************************/
        private void uToolBarManagerControl_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            string keyTool = e.Tool.Key;

            switch (keyTool)
            {

                case "logoff":
                    utcAppWizard.Tabs["login"].Selected = true;
                    clearControls();
                    clsVariables.variableClearSetDefaultValues();
                    tmrModbus.Enabled = false;
                    break;

                case "newbatch":
                    utcAppWizard.Tabs["newbatch"].Selected = true;
                    tmrModbus.Enabled = true;
                    break;

                case "settings":
                    tmrModbus.Enabled = false;
                    this.Hide();
                    Application.DoEvents();
                    frmSettings frmSettings = new frmSettings();
                    frmSettings.ShowDialog();
                    this.Show();
                    break;

                case "logbook":
                    tmrModbus.Enabled = false;
                    utcAppWizard.Tabs["logbook"].Selected = true;
                    DTP_LogBookFromDate.Value = DateTime.Now;
                    DTP_LogBookToDate.Value = DateTime.Now;
                    uGrid_LogBookDetails.DataSource = new DataTable();
                    HideShowToolBar(1);
                    break;
            }

            clsVariables.strFrmAppWizardActiveTabKey = utcAppWizard.ActiveTab.Key.ToString();
        }
        #endregion

        #region clearTools
        /****************************************************************************************************
         * NAME         : clearTools                                                                        *
         * DESCRIPTION  : Clear all the Label and Textbox                                                   *
         * WRITTEN BY   : RajaSekar J                                                                      *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        private void clearControls()
        {
            panelFormHeader.Visible = false;
            txtEmpNo.Focus();
            txtEmpNo.Text = "";
            lblErrorMesaage.Visible = false;
        }
        #endregion

        #region uGrid_LogBookDetails_InitializeLayout
        /****************************************************************************************************
         * NAME         : uGrid_LogBookDetails_InitializeLayout                                             *
         * DESCRIPTION  : Initialize Layout Event call from uGrid_LogBookDetails control.                   *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 19Febr2018                                                                         *
         ****************************************************************************************************/
        private void uGrid_LogBookDetails_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            uGrid_LogBookDetails.DisplayLayout.Override.RowSelectors = IWIN.DefaultableBoolean.False;

            foreach (IWUG.UltraGridColumn col in uGrid_LogBookDetails.DisplayLayout.Bands[0].Columns)
            {
                // Here we "turn off" theming for the column header.
                col.Header.Appearance.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
                col.Header.Appearance.BackColor = Color.LightGray;
                col.Header.Appearance.ForeColor = Color.Black;
                col.Header.Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;

                string strColumnName = col.ToString();
                if ("datPrintingDate" == strColumnName)
                {
                    col.Format = "dd-MM-yyyy hh:mm:ss tt";
                }

                col.Header.Caption = (string)dicLanguageCaptions[string.Format("{0}_{1}", col.Key, strLanguage)];

            }

            if ("0" == clsCommon.ReadSingleConfigValue("Default", "LanguageDirection", "LanguageSupport"))
            {
                uGrid_LogBookDetails.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
                uGrid_LogBookDetails.DisplayLayout.Bands[0].Override.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
            }
            else
            {
                uGrid_LogBookDetails.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                uGrid_LogBookDetails.DisplayLayout.Bands[0].Override.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            }
        }
        #endregion

        #region uBTN_LogBook_Filter_Click
        /****************************************************************************************************
         * NAME         : uBTN_LogBook_Filter_Click                                                         *
         * DESCRIPTION  : Click Event call from uBTN_LogBook_Filter_Click control.                          *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 19Febr2018                                                                         *
         ****************************************************************************************************/
        private void uBTN_LogBook_Filter_Click(object sender, EventArgs e)
        {
            DateTime dtFromDate = DateTime.Parse(DTP_LogBookFromDate.Value.ToString());
            DateTime dtToDate = Convert.ToDateTime(DTP_LogBookToDate.Value.ToString());

            uGrid_LogBookDetails.DataSource = clsappWizardBAL.getEletraLogBookDetails(4, dtFromDate, dtToDate);
            uGrid_LogBookDetails.DataBind();
        }
        #endregion

        #region uBTN_LogBook_Excel_Click
        /****************************************************************************************************
         * NAME         : uBTN_LogBook_Excel_Click                                                          *
         * DESCRIPTION  : Export Grid Data to Excel .                                                       *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 19Febr2018                                                                         *
         ****************************************************************************************************/
        private void uBTN_LogBook_Excel_Click(object sender, EventArgs e)
        {
            string[] FolderPath = clsCommon.FileFolderOpenFileDialog();

            try
            {
                if ("True" == FolderPath[0])
                {
                    ultraGridExcelExporter.Export(uGrid_LogBookDetails, FolderPath[1] + "\\ElectraLogBookDetails.xls");

                    clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("ExportExcelTextCaption_{0}", strLanguage)]);
                    clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "64");
                    clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("ExportExcelTtitleCaption_{0}", strLanguage)]);
                    clsCommon.commonGeneralDisplayMessageBox(0);
                }
            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }
        }
        #endregion

        #region SaveLogBookDetails
        /****************************************************************************************************
         * NAME         : SaveLogBookDetails                                                                *
         * DESCRIPTION  : Set the SaveLogBookDetails details.                                               *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 26Feb2018                                                                         *
         ****************************************************************************************************/
        private void SaveLogBookDetails(string serialNumber, string unitMACAddress)
        {
            try
            {                       
                string strStationName = clsCommon.ReadSingleConfigValue("StationName", "GetSetGeneralSettings", "Settings");
                clsappWizardBAL.SetEletraLogBookDetails(2, DateTime.Now, strStationName, clsVariables.intLoginUserID, serialNumber, unitMACAddress);
            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }

        }

        #endregion

        private void BtnRePrint_Click(object sender, EventArgs e)
        {
            printLabel(txtUnitSerialNumber.Text, txtunitMacAddress.Text);
        }
    }

}
