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
    class clsCommonDAL
    {
        #region Variable Declaration
        private DatabaseProviderFactory factory;
        private Database db;
        #endregion
        public clsCommonDAL()
        {
            factory = new DatabaseProviderFactory();
            db = factory.Create(clsVariables.strGetDefaultDatabase);
        }

        clsCommon clsCommon = new clsCommon();

        #region GetComboData
        /****************************************************************************************************
         * NAME         : GetComboData                                                                      *
         * DESCRIPTION  : Get the Common Combo Details.                                                     *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                          *
         ****************************************************************************************************/
        public DataSet GetComboData(int intTable)
        {
            try
            {
                DataSet ds = new DataSet();
                using (DbCommand dbCommand = db.GetStoredProcCommand("GetComboData_Sp"))
                {
                    db.AddInParameter(dbCommand, "intTable", DbType.String, intTable);

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
