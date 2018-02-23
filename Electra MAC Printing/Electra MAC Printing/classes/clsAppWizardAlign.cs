using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Electra_MAC_Printing.classes
{
    class clsAppWizardAlign
    {
        #region panelSharedHeaderDetailsLabelResize
        /****************************************************************************************************
         * NAME         : panelSharedHeaderDetailsLabelResize                                               *
         * DESCRIPTION  : Page Login page Label Resize function                                     *
         * WRITTEN BY   : RajaSekar                                                                         *
         * DATE         : 20Feb18                                                                           *
         ****************************************************************************************************/
        public void panelSharedCSNDetailsLabelResize(Panel panelloginMain, Label LBL_User, Label LBL_WorkOrder)
        {
            /*Variable Declaration.*/
            decimal panelloginMainWidth = panelloginMain.Width;
            decimal totalLBLWidth = LBL_User.Width + LBL_WorkOrder.Width;
            int leftValue = Convert.ToInt32(Math.Round((panelloginMainWidth - totalLBLWidth) / 3));

            LBL_WorkOrder.Left = LBL_User.Width + leftValue;
        }
        #endregion

        #region panelSharedHeaderDetailsLabelResize
        /****************************************************************************************************
         * NAME         : panelSharedHeaderDetailsLabelResize                                               *
         * DESCRIPTION  : Page Start Marking page Label Resize function                                     *
         * WRITTEN BY   : RajaSekar                                                                         *
         * DATE         : 20Feb18                                                                           *
         ****************************************************************************************************/
        public void panelSharedHeaderDetailsLabelResize(Panel panelSharedHeader, Label lblShared, Panel panelsharedImage)
        {
            int intpanelsharedLabel = (panelSharedHeader.Width - lblShared.Width) / 2;
            lblShared.Left = intpanelsharedLabel;
            panelsharedImage.Left = lblShared.Left - 75;
        }
        #endregion

        #region LoginFormResize
        /****************************************************************************************************
         * NAME         : LoginFormResize                                                                   *
         * DESCRIPTION  : LoginFor page panel resize function                                               *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 08Feb18                                                                           *
         ****************************************************************************************************/
        public void LoginFormResize(Panel pnlLoginMain, Panel pnlLogin, Label lblFormHead, PictureBox PictLoginLogo)
        {
            /*Variable Declaration.*/
            int intpnlLoginMainWidth = pnlLoginMain.Width; /*Get the panelNewBatchBody Width when page is Resize. */
            int intpnlLoginMainHeight = pnlLoginMain.Height; /*Get the panelNewBatchBody Height when page is Resize. */
            int intpnlLoginWidth = pnlLogin.Width; /*Get the panelNewBatchBody Width when page is Resize. */
            int intpnlLoginHeight = pnlLogin.Height;

            int intlblFormHeadWidth = lblFormHead.Width;
            int intPictLoginLogoWidth = PictLoginLogo.Width;
            //int intPicWidth = (intPictLoginLogoWidth) / 2;

            pnlLogin.Left = (intpnlLoginMainWidth - intpnlLoginWidth) / 2;
            pnlLogin.Top = ((intpnlLoginMainHeight - intpnlLoginHeight) / 2);

            lblFormHead.Left = ((intpnlLoginMainWidth - intlblFormHeadWidth) / 2) + 10;
            lblFormHead.Top = pnlLogin.Top - 100;
            PictLoginLogo.Top = pnlLogin.Bottom + 50;
            PictLoginLogo.Left = ((intpnlLoginMainWidth - intPictLoginLogoWidth) - 10) / 2;

            //int test = (intpnlLoginMainHeight - intpnlLoginHeight);
            //int test1 = intpnlLoginHeight / 2;
            //int res = test + test1;
            //int logo = PictLoginLogo.Height / 2;
            //int res1 = res - logo;
            //PictLoginLogo.Top = res1;
        }
        #endregion

        #region newBatchFormPageResize
        /****************************************************************************************************
         * NAME         : panelSharedCSNDetailsLabelResize                                                  *
         * DESCRIPTION  : New batch page panel resize function                                              *
         * WRITTEN BY   : RajaSekar                                                                         *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        public void newBatchFormResize(Panel panelMainNewBatch, Panel panelLeftNewBatch, Panel panelRightNewBatch, Panel panelReprintbutton)
        {
            int panelMainNewBatchWidth = panelMainNewBatch.Width; /*Get the panelNewBatchBody Width when page is Resize. */
            //int panelMainNewBatchHeight = panelMainNewBatch.Height;
            int panelMainNewBatchHeight = panelMainNewBatch.Height - 75; /*Get the panelNewBatchBody Height when page is Resize. */
            panelMainNewBatchHeight = (600 < panelMainNewBatchHeight) ? 576 : panelMainNewBatchHeight;
            int newBatchPanelWidth = (panelMainNewBatchWidth / 2) - 10;/*Set the panelLeftNewBatch & panelRightNewBatch Width when page is Resize. */
            int newBatchPanelLeft = (panelMainNewBatchWidth + 10) / 2;/*Set the panelRightNewBatch Left when page is Resize. */



            panelLeftNewBatch.Width = newBatchPanelWidth;
            panelLeftNewBatch.Height = panelMainNewBatchHeight;

            panelRightNewBatch.Width = newBatchPanelWidth;
            panelRightNewBatch.Height = panelMainNewBatchHeight;
            panelRightNewBatch.Left = newBatchPanelLeft;
         
            panelReprintbutton.Width = newBatchPanelWidth;
            panelReprintbutton.Height = panelMainNewBatchHeight;


        }
        #endregion

        #region panelMarkingLabelResize
        /****************************************************************************************************
         * NAME         : panelMarkingLabelResize                                                           *
         * DESCRIPTION  : Page Marking Label Resize function                                                *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 09Feb18                                                                           *
         ****************************************************************************************************/
        public void panelMarkingLabelResize(Panel panelMarking, Label LBL_MarkingText, Button btnMarkingCancel, Label LBL_BTN_MarkingText)
        {
            int panelMarkingWidth = panelMarking.Width;
            int LBL_MarkingTextWidth = LBL_MarkingText.Width;
            int uBTN_MarkingCancelWidth = btnMarkingCancel.Width;
            int LBL_BTN_MarkingTextWidth = LBL_BTN_MarkingText.Width;

            LBL_MarkingText.Left = (panelMarkingWidth - LBL_MarkingTextWidth) / 2;
            btnMarkingCancel.Left = (panelMarkingWidth - uBTN_MarkingCancelWidth) / 2;
            LBL_BTN_MarkingText.Left = (panelMarkingWidth - LBL_BTN_MarkingTextWidth) / 2;
        }
        #endregion

        #region panelStartBatchLabelResize
        /****************************************************************************************************
         * NAME         : panelStartBatchLabelResize                                                        *
         * DESCRIPTION  : Page Start Marking page Label Resize function                                     *
         * WRITTEN BY   : RajaSekar                                                                         *
         * DATE         : 10Feb18                                                                           *
         ****************************************************************************************************/
        public void panelStartBatchLabelResize(Panel panelStartBatch, Label lblBarCode, Label lblStartMarking, TextBox txtBarCode, Label lblStartBatchErroMessage, Panel panelStartBatchHeader)
        {
            int panelStartBatchWidth = panelStartBatch.Width;
            int lblBarCodeWidth = lblBarCode.Width;
            int lblStartMarkingWidth = lblStartMarking.Width;

            //lblBarCode.Left = (panelStartBatchWidth - lblBarCodeWidth) / 2;
            lblStartMarking.Left = (panelStartBatchWidth - lblStartMarkingWidth) / 2;
            lblStartBatchErroMessage.Left = (panelStartBatchHeader.Width - lblStartBatchErroMessage.Width) / 2;
        }
        #endregion

        #region panelStartMarkingLabelResize
        /****************************************************************************************************
         * NAME         : panelStartMarkingLabelResize                                                      *
         * DESCRIPTION  : Page Start Marking page Label Resize function                                     *
         * WRITTEN BY   : RajaSekar                                                                         *
         * DATE         : 09Feb18                                                                           *
         ****************************************************************************************************/
        public void panelStartMarkingLabelResize(Panel panelStartMarking, Label LBL_RM_ReadyMarking, Label LBL_RM_PressReady)
        {
            int panelStartMarkingWidth = panelStartMarking.Width;
            int LBL_RM_ReadyMarkingWidth = LBL_RM_ReadyMarking.Width;
            int LBL_PressReadyWidth = LBL_RM_PressReady.Width;

            LBL_RM_ReadyMarking.Left = (panelStartMarkingWidth - LBL_RM_ReadyMarkingWidth) / 2;
            LBL_RM_PressReady.Left = (panelStartMarkingWidth - LBL_PressReadyWidth) / 2;
        }
        #endregion
    }
}
