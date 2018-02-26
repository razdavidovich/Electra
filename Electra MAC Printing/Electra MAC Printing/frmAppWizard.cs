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
using System.Text;

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
            using (SerialPort port = new SerialPort("COM3"))
            {
                // configure serial port
                port.BaudRate = 115200;
                port.DataBits = 8;
                port.Parity = Parity.Even;
                port.StopBits = StopBits.One;
                port.ReadTimeout = 1000;
                port.Open();

                // create modbus master
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                // Read the registers
                var values = master.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);
                return values;
            }
        }

        private void clearData()
        {

            txtUnitSerialNumber.Text = string.Empty;
            txtUnitSerialNumber.BackColor = Color.Red;

            txtunitMacAddress.Text = string.Empty;
            txtunitMacAddress.BackColor = Color.Red;

            BtnRePrint.Hide();
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

        }

        private void tmrModbus_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!BtnRePrint.Visible)
                {
                    // Read Serial number (expecting 6153320000 from test unit)
                    var serialValue = ReadModbusRegisters(2, 0x00, 5);
                    txtUnitSerialNumber.Text = ConvertToSerialNumber(serialValue);

                    // Read MAC ADDRESS (expecting A8 1B 6A 9C 7A 9C from test unit)
                    var macValue = ReadModbusRegisters(2, 0x10, 3);
                    txtunitMacAddress.Text = ConvertToMACAddress(macValue);

                    if (txtunitMacAddress.TextLength > 0)
                    {
                        setUnitInformationAndPrint();
                    }
                }
            }
            catch (Exception ex)
            {
                clearControls();
                Console.WriteLine(ex.Message);
            }

        }

        #endregion

        #region "Print Label"
        private void printLabel(string serialNumber, string unitMACAddress)
        {
            // Get ZPL and printer name from the settings
            string printerName = clsCommon.ReadSingleConfigValue("PrinterName", "GetSetGeneralSettings", "Settings");

            // Setup the ZPL to print
            string zpl = string.Format(clsCommon.ReadSingleConfigValue("ZPL", "GetSetGeneralSettings", "Settings"), serialNumber,unitMACAddress,unitMACAddress.Substring(0,7),unitMACAddress.Substring(6));

            // Print the label
            clsPrintUtility.SendStringToPrinter(printerName, zpl);

            // Svae the print information to the logbook

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
         * DESCRIPTION  : Load Control Captions                                                            *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 24Feb18                                                                           *
         ****************************************************************************************************/
        private void LoadControlCaption()
        {
            string strLanguage = clsCommon.ReadSingleConfigValue("Default", "LanguageCodes", "LanguageSupport");

            lblFormHead.Text = (string)dicLanguageCaptions[string.Format("loginHeaderCaption_{0}", strLanguage)];
            lblHeadlogin.Text = (string)dicLanguageCaptions[string.Format("loginHeaderemployeeCardCaption_{0}", strLanguage)];
            lblEmpNo.Text = (string)dicLanguageCaptions[string.Format("loginEmployeeNumberCaption_{0}", strLanguage)];
            btnLogin.Text = (string)dicLanguageCaptions[string.Format("loginButtonCaption_{0}", strLanguage)];
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

            //clsAppWizardAlign.panelSharedHeaderDetailsLabelResize(panelSharedHeader, lblShared, panelsharedImage);

            switch (strAppWizard)
            {
                case "login":
                    clsAppWizardAlign.LoginFormResize(pnlLoginMain, pnlLogin, lblFormHead, PictLoginLogo);
                    break;
                case "newbatch":
                    clsAppWizardAlign.newBatchFormResize(panelMainNewBatch, panelLeftNewBatch, panelRightNewBatch);
                    break;
                case "startbatch":
                   // clsAppWizardAlign.panelStartBatchLabelResize(panelStartBatch, lblBarCode, lblStartMarking, txtBarCode, lblStartBatchErroMessage, panelStartBatchHeader);
                    break;
                case "endbatch":

                    //clsAppWizardAlign.panelMarkingLabelResize(panelMarking, LBL_MarkingText, btnMarkingCancel, LBL_BTN_MarkingText);
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
                   // ClearNewBatchControls();
                    clsVariables.variableClearSetDefaultValues();
                    break;

                case "endbatch":
                    utcAppWizard.Tabs["newbatch"].Selected = true;
                    //ClearNewBatchControls();
                    break;

                case "newbatch":
                    utcAppWizard.Tabs["newbatch"].Selected = true;
                    //ClearNewBatchControls();
                    break;

                case "settings":
                    this.Hide();
                    Application.DoEvents();
                    frmSettings frmSettings = new frmSettings();
                    frmSettings.ShowDialog();
                    this.Show();
                    break;

                case "logbook":
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
                if ("datMarkingDate" == strColumnName)
                {
                    col.Format = "dd-MM-yyyy hh:mm:ss tt";
                }

                col.Header.Caption = clsCommon.ReadSingleConfigValue(strColumnName, "uGrid_LogBookDetails_HeaderCaption", "Settings");

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
                    ultraGridExcelExporter.Export(uGrid_LogBookDetails, FolderPath[1] + "\\MarkerLogBookDetails.xls");
                    clsCommon.commonGeneralDisplayMessageBox(1);

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
         * DATE         : 21Feb2018                                                                         *
         ****************************************************************************************************/
        private void SaveLogBookDetails()
        {
            try
            {
                string[] strParameterValue = new string[4];
                //int GridRows = ultraBarCodeGrid.Rows.Count;

                //if (0 < GridRows)
                //{
                //    for (int i = 0; i < GridRows; i++)
                //    {
                //        strParameterValue[i] = string.Format("{0} => {1}", ultraBarCodeGrid.Rows[i].Cells[3].Value.ToString(), ultraBarCodeGrid.Rows[i].Cells[4].Value.ToString());
                //    }
                //}
                string strMachineName = clsCommon.ReadSingleConfigValue("MachineNumber", "GetSetGeneralSettings", "Settings");
                clsappWizardBAL.SetEletraLogBookDetails(2, DateTime.Now, strMachineName, clsVariables.intLoginUserID, clsVariablesWorkOrderDetails.intPartNumberID, clsVariablesWorkOrderDetails.strVLMName, strParameterValue[0], strParameterValue[1], strParameterValue[2], strParameterValue[3], 0);
            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }

        }
        #endregion


    }

}
