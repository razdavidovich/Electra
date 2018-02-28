using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electra_MAC_Printing.classes.DAL
{
    class clsSettingsDAL
    {
        #region Variable Declaration
        private DatabaseProviderFactory factory;
        private Database db;
        #endregion
        public clsSettingsDAL()
        {
            factory = new DatabaseProviderFactory();
            db = factory.Create(clsVariables.strGetDefaultDatabase);
        }

        #region getUsersDetails
        /****************************************************************************************************
         * NAME         : getUsersDetails                                                                   *
         * DESCRIPTION  : Get User Details(SELECT).                                                         *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        public DataSet getUsersDetails(int intOperation)
        {
            DataSet ds = new DataSet();
            try
            {
                using (DbCommand dbCommand = db.GetStoredProcCommand("BT_Users_Sp"))
                {
                    db.AddInParameter(dbCommand, "Operation", DbType.String, intOperation);
                    db.AddInParameter(dbCommand, "intUserID", DbType.String, null);
                    db.AddInParameter(dbCommand, "intRoleID", DbType.String, null);
                    db.AddInParameter(dbCommand, "vchRFID", DbType.String, null);

                    ds = db.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        #region setUsersDetails
        /****************************************************************************************************
         * NAME         : setUsersDetails                                                                   *
         * DESCRIPTION  : set User Details(INSERT,UPDATE,DELETE).                                           *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        public Boolean setUsersDetails(int intOperation, int intUserID = 0, int intRoleID = 0, string strRFID = null, int intKey = 0)
        {
            try
            {
                using (DbCommand dbCommand = db.GetStoredProcCommand("BT_Users_Sp"))
                {
                    db.AddInParameter(dbCommand, "Operation", DbType.String, intOperation);
                    db.AddInParameter(dbCommand, "intUserID", DbType.String, intUserID);
                    db.AddInParameter(dbCommand, "intRoleID", DbType.String, intRoleID);
                    db.AddInParameter(dbCommand, "vchRFID", DbType.String, strRFID);
                    db.ExecuteNonQuery(dbCommand);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
