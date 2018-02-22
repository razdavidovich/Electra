
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
    class clsAppWizardDAL
    {
        #region Variable Declaration
        private DatabaseProviderFactory factory;
        private Database db;
        #endregion
        public clsAppWizardDAL()
        {
            factory = new DatabaseProviderFactory();
            db = factory.Create(clsVariables.strGetDefaultDatabase);
        }
        #region checkUserLogin
        /****************************************************************************************************
         * NAME         : checkUserLogin                                                                    *
         * DESCRIPTION  : Validate the User Login details.                                                  *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        public DataSet checkUserLogin(string strLoginUserID)
        {
            try
            {
                /*Variable Declaration*/
                DataSet ds = new DataSet();
                using (DbCommand dbCommand = db.GetStoredProcCommand("Login_sp"))
                {
                    db.AddInParameter(dbCommand, "vchRFID", DbType.String, strLoginUserID);

                    ds = db.ExecuteDataSet(dbCommand);
                }
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        #endregion
    }
}
