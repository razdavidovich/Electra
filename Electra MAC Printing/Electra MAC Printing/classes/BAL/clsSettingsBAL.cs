using Electra_MAC_Printing.classes.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electra_MAC_Printing.classes.BAL
{
    class clsSettingsBAL
    {
        clsSettingsDAL clsSettingsDAL = new clsSettingsDAL();

        #region getUsersDetails
        /****************************************************************************************************
         * NAME         : getUsersDetails                                                                   *
         * DESCRIPTION  : Get User Details(SELECT).                                                         *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        public DataTable getUsersDetails(int intOperation)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = clsSettingsDAL.getUsersDetails(intOperation).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region setUsersDetails
        /****************************************************************************************************
         * NAME         : setUsersDetails                                                                   *
         * DESCRIPTION  : set User Details(INSERT,UPDATE,DELETE).                                           *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        public Boolean setUsersDetails(int intOperation, int intUserID = 0, int intRoleID = 0, string strRFID = null)
        {
            try
            {
                return clsSettingsDAL.setUsersDetails(intOperation, intUserID, intRoleID, strRFID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
