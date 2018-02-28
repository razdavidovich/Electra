using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electra_MAC_Printing.classes
{
    class clsVariables
    {
        private static int _intLoginUserID, _intLoginRoleID;
        private static string _strFrmAppWizardActiveTabKey, _strFrmSettingsActiveTabKey;
        private static bool _blnToolBarSettingsVisiable;

        #region Static String Variables


        /*Default Database Property*/
        public static string strGetDefaultDatabase { get { return clsCommon.ReadSingleConfigValue("DefaultDatabase", "DataBase", "Settings"); } }
        /*Form AppWizard Active Tab Key Property*/
        public static string strFrmAppWizardActiveTabKey { get { return _strFrmAppWizardActiveTabKey; } set { _strFrmAppWizardActiveTabKey = value; } }
        /*Form Settings Active Tab Key Property*/
        public static string strFrmSettingsActiveTabKey { get { return _strFrmSettingsActiveTabKey; } set { _strFrmSettingsActiveTabKey = value; } }

        #endregion

        /*Non-Static Variable  Declaration*/
        private string _strImageFileName;

        public void DefaultInitDataMatrixValuesClear()
        {
        }

        #region Static Int Variables
        /*Login UserId Property*/
        public static int intLoginUserID { get { return _intLoginUserID; } set { _intLoginUserID = value; } }
        /*Login RoleId Property*/
        public static int intLoginRoleID { get { return _intLoginRoleID; } set { _intLoginRoleID = value; } }
        #endregion

        #region Static bool Variables
        /*Tool bar Settings Visiable True/False Property*/
        public static bool blnToolBarSettingsVisiable { get { return _blnToolBarSettingsVisiable; } set { _blnToolBarSettingsVisiable = value; } }
        /*Tool bar Settings Visiable True/False Property*/

        #endregion

        #region Non-Static String Variables
        /*Image and File comman name get/set from work order details. */
        public string strImageFileName { get { return _strImageFileName; } set { _strImageFileName = value; } }

        public string strSharedImageFileName { get { return _strImageFileName; } set { _strImageFileName = value; } }

        #endregion

        public void variableClearSetDefaultValues()
        {
            _intLoginUserID = 0;
            _intLoginRoleID = 0;

            _strFrmAppWizardActiveTabKey = clsCommon.ReadSingleConfigValue("FrmAppWizardActiveTabKey", "OtherSettings", "Settings");
            _blnToolBarSettingsVisiable = Convert.ToBoolean(clsCommon.ReadSingleConfigValue("ToolBarSettingsVisiable", "OtherSettings", "Settings"));
        }
    }
    
}
