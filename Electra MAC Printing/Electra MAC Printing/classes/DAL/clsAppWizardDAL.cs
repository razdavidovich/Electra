
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
        #region getEletraLogBookDetails
        /****************************************************************************************************
         * NAME         : getEletraLogBookDetails                                                          *
         * DESCRIPTION  : Get the Marking Log Book Details.                                                 *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 17Feb18                                                                           *
         ****************************************************************************************************/
        public DataSet getEletraLogBookDetails(int intOperation, DateTime? dtFromDate = null, DateTime? dtToDate = null)
        {

            try
            {
                DataSet ds = new DataSet();
                using (DbCommand dbCommand = db.GetStoredProcCommand("PrintingLogBook_Sp"))
                {
                    db.AddInParameter(dbCommand, "Operation", DbType.Int32, intOperation);
                    db.AddInParameter(dbCommand, "datFromDate", DbType.DateTime, dtFromDate);
                    db.AddInParameter(dbCommand, "datToDate", DbType.DateTime, dtToDate);
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

        #region SetEletraLogBookDetails
        /****************************************************************************************************
         * NAME         : SetEletraLogBookDetails                                                          *
         * DESCRIPTION  : Get the Marking Log Book Details.                                                 *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 17Feb18                                                                           *
         ****************************************************************************************************/
        public bool SetEletraLogBookDetails(int intOperation, DateTime MarkingDate, string vchMachine, int intUserId, int intPartNumber, string vchVLMName, string vchParameter1, string vchParameter2, string vchParameter3, string vchParameter4, int intMachineStatus)
        {

            try
            {

                using (DbCommand dbCommand = db.GetStoredProcCommand("PrintingLogBook_Sp"))
                {
                    db.AddInParameter(dbCommand, "Operation", DbType.Int32, intOperation);
                    db.AddInParameter(dbCommand, "datPrintingDate", DbType.DateTime, MarkingDate);
                    db.AddInParameter(dbCommand, "vchMachine", DbType.String, vchMachine);
                    db.AddInParameter(dbCommand, "intUserID", DbType.Int32, intUserId);                  
                    db.AddInParameter(dbCommand, "vchSerialNumber", DbType.String, vchVLMName);
                    db.AddInParameter(dbCommand, "vchMACAddress", DbType.String, vchParameter1);                   
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
