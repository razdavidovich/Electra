using Electra_MAC_Printing.classes.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electra_MAC_Printing.classes.BAL
{
    class clsCommonBAL
    {
        clsCommonDAL clsCommonDAL = new clsCommonDAL();

        #region GetComboData
        /****************************************************************************************************
         * NAME         : GetComboData                                                                      *
         * DESCRIPTION  : Get the Common Combo Details.                                                     *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 25Jun2015                                                                         *
         ****************************************************************************************************/
        public DataTable GetComboData(int intTable)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = clsCommonDAL.GetComboData(intTable).Tables[0];
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
