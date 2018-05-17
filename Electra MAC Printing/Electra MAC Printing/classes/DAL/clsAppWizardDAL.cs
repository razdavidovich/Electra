
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

        #region clsAppWizardDAL
        /****************************************************************************************************
         * NAME         : clsAppWizardDAL                                                                    *
         * DESCRIPTION  : Get the DataBase Provider Factory                                                 *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 26Feb18                                                                           *
         ****************************************************************************************************/
        public clsAppWizardDAL()
        {
            factory = new DatabaseProviderFactory();
            db = factory.Create(clsVariables.strGetDefaultDatabase);
        }
        #endregion

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
         * NAME         : getEletraLogBookDetails                                                           *
         * DESCRIPTION  : Get the Electra MAC Log Book Details.                                             *
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
         * NAME         : SetEletraLogBookDetails                                                           *
         * DESCRIPTION  : Get the Electra MAC Log Book Details.                                             *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 17Feb18                                                                           *
         ****************************************************************************************************/
        public bool SetEletraLogBookDetails(int intOperation, DateTime MarkingDate, string vchMachine, int intUserId, string vchSerialNumber, string vchMACAddress)
        {

            try
            {

                using (DbCommand dbCommand = db.GetStoredProcCommand("PrintingLogBook_Sp"))
                {
                    db.AddInParameter(dbCommand, "Operation", DbType.Int32, intOperation);
                    db.AddInParameter(dbCommand, "datPrintingDate", DbType.DateTime, MarkingDate);
                    db.AddInParameter(dbCommand, "vchMachine", DbType.String, vchMachine);
                    db.AddInParameter(dbCommand, "intUserID", DbType.Int32, intUserId);                  
                    db.AddInParameter(dbCommand, "vchSerialNumber", DbType.String, vchSerialNumber);
                    db.AddInParameter(dbCommand, "vchMACAddress", DbType.String, vchMACAddress);                   
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

        #region getLanguageCapion
        /****************************************************************************************************
         * NAME         : getLanguageCapion                                                                 *
         * DESCRIPTION  : Get Language Caption Details(SELECT).                                             *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        public DataSet getLanguageCapion(int intOperation,string vchLanguageCode)
        {
            DataSet ds = new DataSet();
            try
            {
                using (DbCommand dbCommand = db.GetStoredProcCommand("TranslationGlossary_SP"))
                {
                    db.AddInParameter(dbCommand, "Operation", DbType.Int32, intOperation);
                    db.AddInParameter(dbCommand, "vchLanguageCode", DbType.String, vchLanguageCode);
                    db.AddInParameter(dbCommand, "vchValues", DbType.String, "");                    
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

        #region GetLabelDetails
        /****************************************************************************************************
         * NAME         : GetLabelDetails                                                                   *
         * DESCRIPTION  : Get the Label Details.                                                            *
         * WRITTEN BY   : Prabakaran G                                                                      *
         * DATE         : 17May2018                                                                         *
         ****************************************************************************************************/
        public DataSet GetLabelDetails(int intOperation, int intLabelID, string strLabelName)
        {

            try
            {
                DataSet ds = new DataSet();

                using (DbCommand dbCommand = db.GetStoredProcCommand("Label_Sp"))
                {
                    db.AddInParameter(dbCommand, "Operation", DbType.Int32, intOperation);
                    db.AddInParameter(dbCommand, "intLabelID", DbType.Int32, intLabelID);
                    db.AddInParameter(dbCommand, "vchLabelName", DbType.String, null);
                    db.AddInParameter(dbCommand, "vchZPL", DbType.String, null);
                    db.AddInParameter(dbCommand, "Key1", DbType.Int32, intLabelID);

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
