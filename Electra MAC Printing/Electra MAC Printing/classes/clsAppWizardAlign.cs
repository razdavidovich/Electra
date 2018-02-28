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
        public void newBatchFormResize(Panel panelMainNewBatch, Panel panelLeftNewBatch, Panel panelRightNewBatch)
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
         
            //panelReprintbutton.Width = newBatchPanelWidth;
            //panelReprintbutton.Height = panelMainNewBatchHeight;


        }
        #endregion

    }
}
