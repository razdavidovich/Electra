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

namespace Electra_MAC_Printing
{
    public partial class frmAppWizard : Form
    {

        clsCommon clsCommon = new clsCommon();
        clsVariables clsVariables = new clsVariables();
        clsAppWizardAlign clsAppWizardAlign = new clsAppWizardAlign();
        clsAppWizardBAL clsappWizardBAL = new clsAppWizardBAL();
        //clsSettingsBAL clsSettingsBAL = new clsSettingsBAL();


        public frmAppWizard()
        {
            InitializeComponent();
           
            /*Settings For Log File path for Debug the Errors.*/
            clsApplicationLogFile.LogFilePath = clsCommon.ReadSingleConfigValue("LogFilePath", "LogSettings", "Settings");
            clsApplicationLogFile.LogFileName = clsCommon.ReadSingleConfigValue("LogFileName", "LogSettings", "Settings");
            clsApplicationLogFile.LogFileExtension = clsCommon.ReadSingleConfigValue("LogFileExtension", "LogSettings", "Settings");
            clsCommon.clsApplicationLogFileWriteLog(null, "Form Login Load : Success");
            clsVariables.variableClearSetDefaultValues();
        }

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

            //Adding_uGridWorkDetails_From_Form_Load();

            // RunStartMarking();
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
                    clsAppWizardAlign.newBatchFormResize(panelMainNewBatch, panelLeftNewBatch, panelRightNewBatch, panelReprintbutton);
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
                    //clearControls();
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
    }
}
