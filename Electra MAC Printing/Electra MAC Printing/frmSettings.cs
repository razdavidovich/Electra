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

using System.Drawing.Printing;


namespace Electra_MAC_Printing
{
    public partial class frmSettings : Form
    {
        clsCommon clsCommon = new clsCommon();
        clsAppWizardAlign clsAppWizardAlign = new clsAppWizardAlign();
       // clsSerialPort clsSerialPort = new clsSerialPort();
        clsSettingsBAL clsSettingsBAL = new clsSettingsBAL();
        clsCommonBAL clsCommonBAL = new clsCommonBAL();
        clsAppWizardBAL clsappWizardBAL = new clsAppWizardBAL();

        /*Variable Declaration*/
        private int intuGrid_Users_DeleteRowId = 0;

        string strLanguage = clsCommon.ReadSingleConfigValue("Default", "LanguageCodes", "LanguageSupport");

        private Dictionary<string, object> dicLanguageCaptions;

        public frmSettings()
        {
            InitializeComponent();
            LoadLanugageCaptions();
        }


        #region frmSettings_Load
        /****************************************************************************************************
         * NAME         : frmSettings_Load                                                                  *
         * DESCRIPTION  : Initial Form load function                                                        *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        private void frmSettings_Load(object sender, EventArgs e)
        {
            utcAppSettingsWizard.Tabs["general"].Selected = true;

            uGrid_Users_Form_Load();


            Common_COMPORT_DataBinding(uCBO_GS_StripeCom);
            Common_BaudRate_DataBinding(uCBO_GS_StripeBaudRate);
            Common_Parity_DataBinding(uCBO_GS_StripeParity);
            Common_DataBits_DataBinding(uCBO_GS_StripeDataBits);
            Common_StopBits_DataBinding(uCBO_GS_StripeStopBits);

           

            /*General Settings*/
            GeneralSettings();
            LoadControlCaption();
        }
        #endregion

        private void GetPrinter()
        {
            try
            {
                string strPrinterName = clsCommon.ReadSingleConfigValue("PrinterName", "GetSetGeneralSettings", "Settings");
                int index = 0;
                int intselectedIndex = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("ValueText");
                dt.Columns.Add("DisplayText");
                DataRow row = dt.NewRow();
                row[0] = "";
                row[1] = "None";
                dt.Rows.Add(row);
                index++;
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    row = dt.NewRow();
                    row[0] = printer;
                    row[1] = printer;
                    dt.Rows.Add(row);
                   
                    if (strPrinterName == printer)
                    {
                       intselectedIndex = index;
                    }
                    index++;
                }

                UltraPrinter.ValueMember = "ValueText";
                UltraPrinter.DisplayMember = "DisplayText";
                UltraPrinter.DataSource = dt;
                UltraPrinter.DataBind();
                UltraPrinter.SelectedIndex = intselectedIndex;
                if(intselectedIndex == 0)
                {
                  clsCommon.SaveConfigSettingsValue("PrinterName", "GetSetGeneralSettings", "Settings", "");                           
                }
               // UltraPrinter.Value = strPrinterName;

            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }
        }

        #region Common_COMPORT_DataBinding
        /****************************************************************************************************
         * NAME         : Common_COMPORT_DataBinding                                                        *
         * DESCRIPTION  : Initial ComboEditorBox datasource load when the form load funcrion .              *
         * WRITTEN BY   : PrabakaranG                                                                       *
         * DATE         : 20Mar15                                                                           *
         ****************************************************************************************************/
        private void Common_COMPORT_DataBinding(Infragistics.Win.UltraWinEditors.UltraComboEditor UCE_COMPort_Control)
        {
            DataTable dt = clsCommon.getSerialPort();

            UCE_COMPort_Control.ValueMember = "ValueText";
            UCE_COMPort_Control.DisplayMember = "DisplayText";
            UCE_COMPort_Control.DataSource = dt;
            UCE_COMPort_Control.DataBind();
            UCE_COMPort_Control.SelectedIndex = 0;
        }
        #endregion

        #region Common_BaudRate_DataBinding
        /****************************************************************************************************
         * NAME         : Common_BaudRate_DataBinding                                                       *
         * DESCRIPTION  : Initial ComboEditorBox datasource load when the form load funcrion .              *
         * WRITTEN BY   : PrabakaranG                                                                       *
         * DATE         : 20Mar15                                                                           *
         ****************************************************************************************************/
        private void Common_BaudRate_DataBinding(Infragistics.Win.UltraWinEditors.UltraComboEditor UCE_BaudRate_Control)
        {
            DataTable dt = clsCommon.getBaudRate();

            UCE_BaudRate_Control.ValueMember = "ValueText";
            UCE_BaudRate_Control.DisplayMember = "DisplayText";
            UCE_BaudRate_Control.DataSource = dt;
            UCE_BaudRate_Control.DataBind();
            UCE_BaudRate_Control.SelectedIndex = 0;
        }
        #endregion


        #region Common_Parity_DataBinding
        /****************************************************************************************************
         * NAME         : Common_Parity_DataBinding                                                         *
         * DESCRIPTION  : Initial ComboEditorBox datasource load when the form load funcrion .              *
         * WRITTEN BY   : PrabakaranG                                                                       *
         * DATE         : 20Mar15                                                                           *
         ****************************************************************************************************/
        private void Common_Parity_DataBinding(Infragistics.Win.UltraWinEditors.UltraComboEditor UCE_Parity_Control)
        {
            DataTable dt = clsCommon.getParity();

            UCE_Parity_Control.ValueMember = "ValueText";
            UCE_Parity_Control.DisplayMember = "DisplayText";
            UCE_Parity_Control.DataSource = dt;
            UCE_Parity_Control.DataBind();
            UCE_Parity_Control.SelectedIndex = 0;
        }
        #endregion

        #region Common_DataBits_DataBinding
        /****************************************************************************************************
         * NAME         : Common_DataBits_DataBinding                                                       *
         * DESCRIPTION  : Initial ComboEditorBox datasource load when the form load funcrion .              *
         * WRITTEN BY   : PrabakaranG                                                                       *
         * DATE         : 20Mar15                                                                           *
         ****************************************************************************************************/
        private void Common_DataBits_DataBinding(Infragistics.Win.UltraWinEditors.UltraComboEditor UCE_DataBits_Control)
        {
            DataTable dt = clsCommon.getDataBits();

            UCE_DataBits_Control.ValueMember = "ValueText";
            UCE_DataBits_Control.DisplayMember = "DisplayText";
            UCE_DataBits_Control.DataSource = dt;
            UCE_DataBits_Control.DataBind();
            UCE_DataBits_Control.SelectedIndex = 0;
        }
        #endregion

        #region Common_StopBits_DataBinding
        /****************************************************************************************************
         * NAME         : Common_StopBits_DataBinding                                                       *
         * DESCRIPTION  : Initial ComboEditorBox datasource load when the form load funcrion .              *
         * WRITTEN BY   : PrabakaranG                                                                       *
         * DATE         : 20Mar15                                                                           *
         ****************************************************************************************************/
        private void Common_StopBits_DataBinding(Infragistics.Win.UltraWinEditors.UltraComboEditor UCE_StopBits_Control)
        {
            DataTable dt = clsCommon.getStopBits();

            UCE_StopBits_Control.ValueMember = "ValueText";
            UCE_StopBits_Control.DisplayMember = "DisplayText";
            UCE_StopBits_Control.DataSource = dt;
            UCE_StopBits_Control.DataBind();
            UCE_StopBits_Control.SelectedIndex = 0;
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
            LBL_StationName.Text = (string)dicLanguageCaptions[string.Format("SettingslabelStationNameCaption_{0}", strLanguage)];
            LBL_UnitSettings.Text = (string)dicLanguageCaptions[string.Format("SettingslabelUnitSettingsCaption_{0}", strLanguage)];
            LBL_ModbuSlaveAddress.Text = (string)dicLanguageCaptions[string.Format("SettingslabelModbusslaveCaption_{0}", strLanguage)];

            LBL_SerialNumberAddress.Text = (string)dicLanguageCaptions[string.Format("SettingslabelSerialNumberCaption_{0}", strLanguage)];
            LBL_DataAddress.Text = (string)dicLanguageCaptions[string.Format("SettingslabelDataAddressCaption_{0}", strLanguage)];
            LBL_PrinterName.Text = (string)dicLanguageCaptions[string.Format("SettingslabelPrinterSettingsCaption_{0}", strLanguage)];

            grpCommunicationSettings.Text = (string)dicLanguageCaptions[string.Format("SettingsGroupboxCaption_{0}", strLanguage)];

            uBTN_Settings_OK.Text = (string)dicLanguageCaptions[string.Format("SettingsButtonSaveCaption_{0}", strLanguage)];
            uBTN_Settings_Cancel.Text = (string)dicLanguageCaptions[string.Format("SettingsbuttonCancelCaption_{0}", strLanguage)];
            utcAppSettingsWizard.Tabs[0].Text= (string)dicLanguageCaptions[string.Format("SettingsTabGeneralSettingsCaption_{0}", strLanguage)];
            utcAppSettingsWizard.Tabs[1].Text = (string)dicLanguageCaptions[string.Format("SettingsTabUsersCaption_{0}", strLanguage)];
        }
        #endregion

        #region utcAppSettingsWizard_SelectedTabChanged
        /****************************************************************************************************
         * NAME         : utcAppSettingsWizard_SelectedTabChanged                                                                  *
         * DESCRIPTION  : AppWizard Selected Change Event                                                     *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        private void utcAppSettingsWizard_SelectedTabChanged(object sender, IWIN.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            string strTabKey = e.Tab.Key.ToString();

            switch (strTabKey)
            {
                case "general":
                    GeneralSettings();
                    break;

                case "users":
                    uGrid_Users_Form_Load();
                    break;
               
            }
        }

        #endregion

        #region uBTN_Settings_OK_Click
        /****************************************************************************************************
         * NAME         : uBTN_Settings_OK_Click                                                            *
         * DESCRIPTION  : Click Event call from uBTN_Settings_OK control                                    *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 20Mar15                                                                           *
         ****************************************************************************************************/
        private void uBTN_Settings_OK_Click(object sender, EventArgs e)
        {                      
            try
            {
                /*Variable Declaration*/

                string strActiveTabKey = utcAppSettingsWizard.ActiveTab.Key;

                switch (strActiveTabKey)
                {
                    case "general":
                        uBTN_Settings_OK_Click_GeneralSettings();
                        break;
                    case "users":
                        uGrid_Users_Form_Load();
                        break;                    
                }
                clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsSaveSuccessCaption_{0}", strLanguage)]);
                clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "64");
                clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsMesageTitleCaption_{0}", strLanguage)]);
                clsCommon.commonGeneralDisplayMessageBox(0);

            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }
        }
        #endregion

        #region uBTN_Settings_OK_Click_GeneralSettings
        /****************************************************************************************************
         * NAME         : uBTN_Settings_OK_Click_GeneralSettings                                            *
         * DESCRIPTION  : General Settings validation for save the Details in Config File.                  *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        private bool uBTN_Settings_OK_Click_GeneralSettings()
        {

            /*Variable Declaration and Assign the Controls values*/
            string strTXT_StationName = TXT_StationName.Text,
               
                   strTxt_ModbusSlaveAddress = Txt_ModbusSlaveAddress.Text,
                   strTXT_SerialNumberAddress = TXT_SerialNumberAddress.Text,
                   strTXT_DataAddress = TXT_DataAddress.Text,
                    strTXT_PrinterName = UltraPrinter.Value.ToString(),
            struCBO_GS_StripeCom = uCBO_GS_StripeCom.Value.ToString(),
                   struCBO_GS_StripeBaudRate = uCBO_GS_StripeBaudRate.Value.ToString(),
                   struCBO_GS_StripeParity = uCBO_GS_StripeParity.Value.ToString(),
                   struCBO_GS_StripeDataBits = uCBO_GS_StripeDataBits.Value.ToString(),
                   struCBO_GS_StripeStopBits = uCBO_GS_StripeStopBits.Value.ToString();

            /*Validate TXT_StationName Controls*/
            if (!string.IsNullOrEmpty(strTXT_StationName))
            {
                clsCommon.SaveConfigSettingsValue("StationName", "GetSetGeneralSettings", "Settings", strTXT_StationName);
            }
            else
            {
                clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsStationNameCaption_{0}", strLanguage)]);
                clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "16");
                clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsMesageTitleCaption_{0}", strLanguage)]);

                clsCommon.commonGeneralDisplayMessageBox(0);                
                return false;
            }

          
            /*Validate Txt_ModbusSlaveAddress Controls*/
            if (!string.IsNullOrEmpty(strTxt_ModbusSlaveAddress))
            {
                clsCommon.SaveConfigSettingsValue("ModbusSlaveAddress", "GetSetGeneralSettings", "Settings", strTxt_ModbusSlaveAddress);
            }
            else
            {
                clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsModbusSlaveAddressCaption_{0}", strLanguage)]);
                clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "16");
                clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsMesageTitleCaption_{0}", strLanguage)]);
                clsCommon.commonGeneralDisplayMessageBox(0);
                return false;
            }

            /*Validate TXT_SerialNumberAddress Controls*/
            if (!string.IsNullOrEmpty(strTXT_SerialNumberAddress))
            {
                clsCommon.SaveConfigSettingsValue("SerialNumberAddress", "GetSetGeneralSettings", "Settings", strTXT_SerialNumberAddress);
            }
            else
            {
                clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsSerialNumberCaption_{0}", strLanguage)]);
                clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "16");
                clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsMesageTitleCaption_{0}", strLanguage)]);
                clsCommon.commonGeneralDisplayMessageBox(0);
                return false;
            }

            /*Validate TXT_DataAddress Controls*/
            if (!string.IsNullOrEmpty(strTXT_DataAddress))
            {
                clsCommon.SaveConfigSettingsValue("MACAddress", "GetSetGeneralSettings", "Settings", strTXT_DataAddress);
            }
            else
            {
                clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsDataAddressCaption_{0}", strLanguage)]);
                clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "16");
                clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsMesageTitleCaption_{0}", strLanguage)]);
                clsCommon.commonGeneralDisplayMessageBox(0);
                return false;
            }

            /*Validate TXT_PrinterName Controls*/
            if (!string.IsNullOrEmpty(strTXT_PrinterName))
            {
                clsCommon.SaveConfigSettingsValue("PrinterName", "GetSetGeneralSettings", "Settings", strTXT_PrinterName);
            }
            else
            {
                clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsPrinterSettingsCaption_{0}", strLanguage)]);
                clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "16");
                clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsMesageTitleCaption_{0}", strLanguage)]);
                clsCommon.commonGeneralDisplayMessageBox(0);
                return false;
            }

            /*Validate uCBO_GS_StripeCom Controls*/
            if (!string.IsNullOrEmpty(struCBO_GS_StripeCom))
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat("{0},{1},{2},{3},{4}", struCBO_GS_StripeCom, struCBO_GS_StripeBaudRate, struCBO_GS_StripeParity, struCBO_GS_StripeDataBits, struCBO_GS_StripeStopBits);
                clsCommon.SaveConfigSettingsValue("UnitSettings", "GetSetGeneralSettings", "Settings", strBuilder.ToString());
            }
            else
            {
                clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsMagneticStripeCaption_{0}", strLanguage)]);
                clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "16");
                clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("GeneralSettingsMesageTitleCaption_{0}", strLanguage)]);
                clsCommon.commonGeneralDisplayMessageBox(0);
                uCBO_GS_StripeCom.Focus();
                return false;
            }
            GeneralSettings();
            return true;
        }
        #endregion

        #region uBTN_Settings_Cancel_Click
        /****************************************************************************************************
         * NAME         : uBTN_Settings_Cancel_Click                                                        *
         * DESCRIPTION  : Click Event call from uBTN_Settings_Cancel control                                *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        private void uBTN_Settings_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #endregion

       
        // General Settings
        #region GeneralSettings
        /****************************************************************************************************
         * NAME         : GeneralSettings                                                                   *
         * DESCRIPTION  : General settings initial Value get the config value                               *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        private void GeneralSettings()
        {

         


            TXT_StationName.Text = clsCommon.ReadSingleConfigValue("StationName", "GetSetGeneralSettings", "Settings");            
            Txt_ModbusSlaveAddress.Text = clsCommon.ReadSingleConfigValue("ModbusSlaveAddress", "GetSetGeneralSettings", "Settings");
            TXT_SerialNumberAddress.Text = clsCommon.ReadSingleConfigValue("SerialNumberAddress", "GetSetGeneralSettings", "Settings");
            TXT_DataAddress.Text = clsCommon.ReadSingleConfigValue("MACAddress", "GetSetGeneralSettings", "Settings");
            GetPrinter();

            string[] strCOMSettings = clsCommon.ReadSingleConfigValue("UnitSettings", "GetSetGeneralSettings", "Settings").Split(',');
            uCBO_GS_StripeCom.Value = strCOMSettings[0];
            uCBO_GS_StripeBaudRate.Value = strCOMSettings[1];
            uCBO_GS_StripeParity.Value = strCOMSettings[2];
            uCBO_GS_StripeDataBits.Value = strCOMSettings[3];
            uCBO_GS_StripeStopBits.Value = strCOMSettings[4];
        }
        #endregion

        //User Grid

        #region uGrid_Users_Form_Load
        /****************************************************************************************************
         * NAME         : uGrid_Users_Form_Load                                                      *
         * DESCRIPTION  : Initial Grid datasource load when the form load funcrion                          *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        private void uGrid_Users_Form_Load()
        {
            try
            {
                uGrid_Users.DisplayLayout.AutoFitStyle = IWUG.AutoFitStyle.ResizeAllColumns;

                uGrid_Users.DataSource = clsSettingsBAL.getUsersDetails(4);
                uGrid_Users.DisplayLayout.Bands[0].Columns[1].Style = IWUG.ColumnStyle.DropDownList;
                uGrid_Users.DisplayLayout.Bands[0].Columns[1].ButtonDisplayStyle = IWUG.ButtonDisplayStyle.Always;
                uGrid_Users.DisplayLayout.Bands[0].Columns[1].ValueList = uGrid_Users_Roles();
            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }
        }
        #endregion

        #region uGrid_Users_Roles
        /****************************************************************************************************
         * NAME         : uGrid_Users_Roles                                                                 *
         * DESCRIPTION  : Creating ValueList Item for User Roles Detils Binding                             *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        private IWIN.ValueList uGrid_Users_Roles()
        {
            try
            {
                DataTable dt = clsCommonBAL.GetComboData(1);
                IWIN.ValueList usersRolesList = new IWIN.ValueList();
                foreach (DataRow row in dt.Rows)
                {
                    usersRolesList.ValueListItems.Add(row[0].ToString(), row[1].ToString());
                }
                return usersRolesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region uGrid_Users_InitializeLayout
        /****************************************************************************************************
         * NAME         : uGrid_Users_InitializeLayout                                                      *
         * DESCRIPTION  : InitializeLayout Event call from uGrid_Users_InitializeLayout control             *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        private void uGrid_Users_InitializeLayout(object sender, IWUG.InitializeLayoutEventArgs e)
        {
            foreach (IWUG.UltraGridColumn col in uGrid_Users.DisplayLayout.Bands[0].Columns)
            {
                // Here we "turn off" theming for the column header.
                col.Header.Appearance.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
                col.Header.Appearance.BackColor = Color.LightGray;
                col.Header.Appearance.ForeColor = Color.Black;
                col.Header.Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;

                col.Header.Caption = (string)dicLanguageCaptions[string.Format("{0}_{1}", col.Key,strLanguage)];
                
            }

            uGrid_Users.DisplayLayout.Bands[0].Columns[2].Hidden = true;
            uGrid_Users.DisplayLayout.Bands[0].Columns[3].Hidden = true;
           
            if ("0" == clsCommon.ReadSingleConfigValue("Default", "LanguageDirection", "LanguageSupport"))
            {
                uGrid_Users.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
                uGrid_Users.DisplayLayout.Bands[0].Override.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
            }
            else
            {
                uGrid_Users.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                uGrid_Users.DisplayLayout.Bands[0].Override.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            }


        }
        #endregion

        #region uGrid_Users_InitializeTemplateAddRow
        /****************************************************************************************************
         * NAME         : uGrid_Users_InitializeTemplateAddRow                                              *
         * DESCRIPTION  : uGrid_Users_InitializeTemplateAddRow Event call from uGrid_User control           *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        private void uGrid_Users_InitializeTemplateAddRow(object sender, IWUG.InitializeTemplateAddRowEventArgs e)
        {
            e.TemplateAddRow.Cells[0].Activation = IWUG.Activation.AllowEdit;
            e.TemplateAddRow.Cells[0].IgnoreRowColActivation = true;
        }
        #endregion

        #region uGrid_Users_Insert_Update_Delete
        /****************************************************************************************************
         * NAME         : uGrid_Users_Insert_Update_Delete                                                  *
         * DESCRIPTION  : Call BAL function                                                                 *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        private void uGrid_Users_Insert_Update_Delete(int intOperation, int intUserID, int intRoleID, string strRFID,int intKey)
        {
            clsSettingsBAL.setUsersDetails(intOperation, intUserID, intRoleID, strRFID, intKey);
        }
        #endregion

        #region uGrid_Users_CommonCellupdating
        /****************************************************************************************************
         * NAME         : uGrid_Users_CommonCellupdating                                                    *
         * DESCRIPTION  : Common update call from uGrid_Users grid Events control                  *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        private void uGrid_Users_CommonCellupdating()
        {
            try
            {
                /*Variable Declaration*/
                int intUserID = string.IsNullOrEmpty(uGrid_Users.ActiveRow.Cells[0].Value.ToString()) ? 0 : Convert.ToInt32(uGrid_Users.ActiveRow.Cells[0].Value.ToString());
                int intRoleID = string.IsNullOrEmpty(uGrid_Users.ActiveRow.Cells[1].Value.ToString()) ? 0 : Convert.ToInt32(uGrid_Users.ActiveRow.Cells[1].Value.ToString());
                string strRFID = string.IsNullOrEmpty(uGrid_Users.ActiveRow.Cells[2].Value.ToString()) ? null : uGrid_Users.ActiveRow.Cells[2].Value.ToString();
                int intKey = string.IsNullOrEmpty(uGrid_Users.ActiveRow.Cells[3].Value.ToString()) ? 0 : Convert.ToInt32(uGrid_Users.ActiveRow.Cells[3].Value.ToString());

                if (intUserID > 0 && intRoleID > 0)
                {
                    uGrid_Users_Insert_Update_Delete(2, intUserID, intRoleID, intUserID.ToString(), intKey);
                }

                else
                {
                    if (0 != uGrid_Users.ActiveRow.Index)
                    {
                        clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("SettingsGridUsersErrorMessageCaption_{0}", strLanguage)]);                        
                        clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "16");
                        clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", (string)dicLanguageCaptions[string.Format("SettingsGridUsersErrorMessageTitleCaption_{0}", strLanguage)]);
                        clsCommon.commonGeneralDisplayMessageBox(0);
                    }
                }
            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }
        }
        #endregion

        #region uGrid_Users_AfterCellUpdate
        /****************************************************************************************************
         * NAME         : uGrid_Users_AfterCellUpdate                                                       *
         * DESCRIPTION  : AfterCellUpdate Event call from uGrid_Users_AfterCellUpdate control               *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        private void uGrid_Users_AfterCellUpdate(object sender, IWUG.CellEventArgs e)
        {
            try
            {
                uGrid_Users_CommonCellupdating();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        #endregion

        #region uGrid_Users_BeforeRowsDeleted
        /****************************************************************************************************
         * NAME         : uGrid_Users_BeforeRowsDeleted                                                     *
         * DESCRIPTION  : BeforeRowsDeleted Event call from uGrid_Users_BeforeRowsDeleted control           *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        private void uGrid_Users_BeforeRowsDeleted(object sender, IWUG.BeforeRowsDeletedEventArgs e)
        {
            intuGrid_Users_DeleteRowId = string.IsNullOrEmpty(e.Rows[0].Cells[3].Value.ToString()) ? 0 : Convert.ToInt32(e.Rows[0].Cells[3].Value.ToString());
        }
        #endregion

        #region uGrid_Users_AfterRowsDeleted
        /****************************************************************************************************
         * NAME         : uGrid_Users_AfterRowsDeleted                                                      *
         * DESCRIPTION  : AfterRowsDeleted Event call from uGrid_Users_AfterRowsDeleted control             *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        private void uGrid_Users_AfterRowsDeleted(object sender, EventArgs e)
        {
            uGrid_Users_Insert_Update_Delete(3, 0, 0, null, intuGrid_Users_DeleteRowId);
            intuGrid_Users_DeleteRowId = 0;
        }
        #endregion

        #region call_uGrid_IsInEditMode
        /****************************************************************************************************
         * NAME         : call_uGrid_IsInEditMode                                                           *
         * DESCRIPTION  : Call the Trigger for Raise the ExitEditMode Event.                                *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        private void call_uGrid_IsInEditMode(IWUG.UltraGrid uGrid)
        {
            try
            {
                if (uGrid.ActiveCell != null)
                {
                    if (uGrid.ActiveCell.IsInEditMode)
                    {
                        uGrid.PerformAction(IWIN.UltraWinGrid.UltraGridAction.ExitEditMode);
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        #endregion

        
    }
}
