using Electra_MAC_Printing.classes.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electra_MAC_Printing.classes.BAL
{
    class clsAppWizardBAL
    {

        clsAppWizardDAL clsAppWizardDAL = new clsAppWizardDAL();

        #region checkUserLogin
        /****************************************************************************************************
         * NAME         : checkUserLogin                                                                    *
         * DESCRIPTION  : Validate the User Login details.                                                  *
         * WRITTEN BY   : RajaSekar                                                                       *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        public Boolean checkUserLogin(string strLoginUserID)
        {
            try
            {
                /*Variable Declaration*/
                bool _return = false;
                DataTable dtUserDetails = new DataTable();

                dtUserDetails = clsAppWizardDAL.checkUserLogin(strLoginUserID).Tables[0];

                if (dtUserDetails.Rows.Count > 0)
                {
                    clsVariables.intLoginUserID = Convert.ToInt32(dtUserDetails.Rows[0]["intUserID"]);
                    clsVariables.intLoginRoleID = Convert.ToInt32(dtUserDetails.Rows[0]["intRoleID"]);
                    _return = true;
                }
                else
                {
                    _return = false;
                }


                return _return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
