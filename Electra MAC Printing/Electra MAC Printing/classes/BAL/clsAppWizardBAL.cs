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

        #region getEletraLogBookDetails
        /****************************************************************************************************
         * NAME         : getEletraLogBookDetails                                                          *
         * DESCRIPTION  : Get the Marking Log Book Details.                                                 *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 17Feb18                                                                           *
         ****************************************************************************************************/
        public DataTable getEletraLogBookDetails(int intOperation, DateTime? dtFromDate = null, DateTime? dtToDate = null)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = clsAppWizardDAL.getEletraLogBookDetails(intOperation, dtFromDate, dtToDate).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SetEletraLogBookDetails
        /****************************************************************************************************
         * NAME         : SetEletraLogBookDetails                                                          *
         * DESCRIPTION  : Get the Marking Log Book Details.                                                 *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 17Feb18                                                                           *
         ****************************************************************************************************/
        public bool SetEletraLogBookDetails(int intOperation, DateTime MarkingDate, string vchMachine, int intUserId, string vchSerialNumber, string vchMACAddress)
        {
            try
            {
                return clsAppWizardDAL.SetEletraLogBookDetails(intOperation, MarkingDate, vchMachine, intUserId, vchSerialNumber, vchMACAddress);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getLanguageCapion
        /****************************************************************************************************
         * NAME         : getLanguageCapion                                                              *
         * DESCRIPTION  : Get Language Caption Details(SELECT).                                                   *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        public DataTable getLanguageCapion(int intOperation ,string vchLanguageCode)
        {

            try
            {
                DataTable dt = new DataTable();
                dt = clsAppWizardDAL.getLanguageCapion(intOperation, vchLanguageCode).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        #endregion
    }
}
