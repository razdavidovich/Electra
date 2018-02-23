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


namespace Electra_MAC_Printing
{
    public partial class frmSettings : Form
    {
        clsCommon clsCommon = new clsCommon();
        clsAppWizardAlign clsAppWizardAlign = new clsAppWizardAlign();
       // clsSerialPort clsSerialPort = new clsSerialPort();
        clsSettingsBAL clsSettingsBAL = new clsSettingsBAL();
        clsCommonBAL clsCommonBAL = new clsCommonBAL();

        /*Variable Declaration*/
        private int intuGrid_Users_DeleteRowId = 0;
        private int intuGrid_ParameterstoPartNumbers_DeleteRowId = 0;
        private int intuGrid_ParameterstoPartNumbers_DeleteParameterID = 0;
        private int intGrid_PartNumbers_DeleteRowId = 0;

        private int intuGrid_Parameters = 0;


        public frmSettings()
        {
            InitializeComponent();
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
            
            /*General Settings*/
            GeneralSettings();
            //uGrid_Parameters_Load();
            //uGrid_ParameterstoPartNumbers_Load();

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

                clsCommon.commonGeneralDisplayMessageBox(7);

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
                strTxt_UnitSettings = Txt_UnitSettings.Text,
                   strTxt_ModbusSlaveAddress = Txt_ModbusSlaveAddress.Text,
                   strTXT_SerialNumberAddress = TXT_SerialNumberAddress.Text,
                   strTXT_DataAddress = TXT_DataAddress.Text,
                    strTXT_PrinterSettings = TXT_PrinterSettings.Text;

            /*Validate TXT_StationName Controls*/
            if (!string.IsNullOrEmpty(strTXT_StationName))
            {
                clsCommon.SaveConfigSettingsValue("StationName", "GetSetGeneralSettings", "Settings", strTXT_StationName);
            }
            else
            {
                clsCommon.commonGeneralDisplayMessageBox(3);
                return false;
            }

            /*Validate Txt_UnitSettings Controls*/
            if (!string.IsNullOrEmpty(strTxt_UnitSettings))
            {
                clsCommon.SaveConfigSettingsValue("UnitSettings", "GetSetGeneralSettings", "Settings", strTxt_UnitSettings);
            }
            else
            {
                clsCommon.commonGeneralDisplayMessageBox(4);
                return false;
            }

            /*Validate Txt_ModbusSlaveAddress Controls*/
            if (!string.IsNullOrEmpty(strTxt_ModbusSlaveAddress))
            {
                clsCommon.SaveConfigSettingsValue("ModbusSlaveAddress", "GetSetGeneralSettings", "Settings", strTxt_ModbusSlaveAddress);
            }
            else
            {
                clsCommon.commonGeneralDisplayMessageBox(5);
                return false;
            }

            /*Validate TXT_SerialNumberAddress Controls*/
            if (!string.IsNullOrEmpty(strTXT_SerialNumberAddress))
            {
                clsCommon.SaveConfigSettingsValue("SerialNumberAddress", "GetSetGeneralSettings", "Settings", strTXT_SerialNumberAddress);
            }
            else
            {
                clsCommon.commonGeneralDisplayMessageBox(6);
                return false;
            }

            /*Validate TXT_DataAddress Controls*/
            if (!string.IsNullOrEmpty(strTXT_DataAddress))
            {
                clsCommon.SaveConfigSettingsValue("DataAddress", "GetSetGeneralSettings", "Settings", strTXT_DataAddress);
            }
            else
            {
                clsCommon.commonGeneralDisplayMessageBox(6);
                return false;
            }

            /*Validate TXT_PrinterSettings Controls*/
            if (!string.IsNullOrEmpty(strTXT_PrinterSettings))
            {
                clsCommon.SaveConfigSettingsValue("PrinterSettings", "GetSetGeneralSettings", "Settings", strTXT_PrinterSettings);
            }
            else
            {
                clsCommon.commonGeneralDisplayMessageBox(6);
                return false;
            }

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
            Txt_UnitSettings.Text = clsCommon.ReadSingleConfigValue("UnitSettings", "GetSetGeneralSettings", "Settings");
            Txt_ModbusSlaveAddress.Text = clsCommon.ReadSingleConfigValue("ModbusSlaveAddress", "GetSetGeneralSettings", "Settings");
            TXT_SerialNumberAddress.Text = clsCommon.ReadSingleConfigValue("SerialNumberAddress", "GetSetGeneralSettings", "Settings");
            TXT_DataAddress.Text = clsCommon.ReadSingleConfigValue("DataAddress", "GetSetGeneralSettings", "Settings");
            TXT_PrinterSettings.Text = clsCommon.ReadSingleConfigValue("PrinterSettings", "GetSetGeneralSettings", "Settings");
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

                uGrid_Users.DataSource = clsSettingsBAL.getUsersDetails(1);
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
                col.Header.Caption = clsCommon.ReadSingleConfigValue(col.ToString(), "uGrid_Users_HeaderCaption", "Settings");

            }

            e.Layout.Bands[0].Columns[0].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

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
        private void uGrid_Users_Insert_Update_Delete(int intOperation, int intUserID, int intRoleID, string strRFID)
        {
            clsSettingsBAL.setUsersDetails(intOperation, intUserID, intRoleID, strRFID);
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

                if (intUserID > 0 && intRoleID > 0 && !string.IsNullOrEmpty(strRFID))
                {
                    uGrid_Users_Insert_Update_Delete(2, intUserID, intRoleID, strRFID);
                }

                else
                {

                    if (0 != uGrid_Users.ActiveRow.Index)
                    {
                        clsCommon.SaveConfigSettingsValue("MessageText", "ID_0", "Messages", "Column Should not be Null or Empty");
                        clsCommon.SaveConfigSettingsValue("MessageType", "ID_0", "Messages", "16");
                        clsCommon.SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", "Invalied Data");

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
            intuGrid_Users_DeleteRowId = string.IsNullOrEmpty(e.Rows[0].Cells[0].Value.ToString()) ? 0 : Convert.ToInt32(e.Rows[0].Cells[0].Value.ToString());
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
            uGrid_Users_Insert_Update_Delete(3, intuGrid_Users_DeleteRowId, 0, null);
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
