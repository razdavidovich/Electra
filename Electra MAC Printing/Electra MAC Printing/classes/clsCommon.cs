using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembly.Software.Utilities;
using System.Windows.Forms;
using System.Data;
using System.IO.Ports;

namespace Electra_MAC_Printing.classes
{
   public class clsCommon
    {
     
        #region ReadSingleConfigValue
        /****************************************************************************************************
         * NAME         : ReadSingleConfigValue                                                             *
         * DESCRIPTION  : Common Method for Read the Config Single Settings Value.                          *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 08Feb18                                                                      *
         ****************************************************************************************************/
        public static string ReadSingleConfigValue(string strItemName, string strConfigSection, string strConfigGroup)
        {
            return Config.get_ReadConfigValue(strItemName, strConfigSection, strConfigGroup);
        }
        #endregion

        #region FileFolderOpenFileDialog
        /****************************************************************************************************
         * NAME         : FileFolderOpenFileDialog                                                          *
         * DESCRIPTION  : Common File Folder Choosen function                                               *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        public static string[] FileFolderOpenFileDialog()
        {
            string[] _return = new string[2] { false.ToString(), null };
            try
            {
                FolderBrowserDialog FolderBrowserDialog = new FolderBrowserDialog();
                //FolderBrowserDialog.Description = "Custom Description"; //not mandatory

                if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    _return[0] = true.ToString();
                    _return[1] = FolderBrowserDialog.SelectedPath;

                    return _return;
                }
                else
                {
                    _return[0] = false.ToString();
                    _return[1] = null;

                    return _return;
                }
            }
            catch (Exception ex)
            {
                clsApplicationLogFileWriteLog(ex);

                return _return;
            }

        }
        #endregion

        #region clsApplicationLogFileWriteLog
        /****************************************************************************************************
         * NAME         : clsApplicationLogFileWriteLog                                                     *
         * DESCRIPTION  : Common Method for Write the Log file in the Folder.                               *
         * WRITTEN BY   : RajaSekar                                                                         *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        public static void clsApplicationLogFileWriteLog(Exception ex = null, string strMessages = null)
        {
            /*Variable Declaration*/
            string strLog = null;
            strMessages = string.IsNullOrEmpty(strMessages) ? "Null" : strMessages;

            if (null != ex)
            {
                string str = "User-ID        : {0}\r\nString-Messages: {1}\r\nError-Messages : {2}\r\nStack-Trace    : {3}";
                strLog = string.Format(str, clsVariables.intLoginUserID, strMessages, ex.Message.ToString(), ex.StackTrace.ToString());
            }
            else
            {
                string str = "User-ID        : {0}\r\nString-Messages: {1}";
                strLog = string.Format(str, clsVariables.intLoginUserID, strMessages);
            }

            clsApplicationLogFile.WriteLog(strLog);
        }
        #endregion
       
        #region ReadConfigGetSectionGroup
        /****************************************************************************************************
         * NAME         : ReadConfigGetSectionGroup                                                         *
         * DESCRIPTION  : Common Method for Read the Config Multi Settings Value.                           *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        public static NameValueCollection ReadConfigGetSectionGroup(string strSectionName)
        {
            return ConfigurationManager.GetSection(strSectionName) as NameValueCollection;
        }
        #endregion

        #region SaveConfigSettingsValue
        /****************************************************************************************************
         * NAME         : SaveConfigSettingsValue                                                           *
         * DESCRIPTION  : Common Method for Save the Config Settings Value.                                 *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                           *
         ****************************************************************************************************/
        public void SaveConfigSettingsValue(string strItemName, string strConfigSection, string strConfigGroup, string strValue)
        {
            Config.SaveConfigValue(strItemName, strConfigSection, strConfigGroup, strValue);
        }
        #endregion

        #region commonGeneralDisplayMessageBox
        /****************************************************************************************************
         * NAME         : commonGeneralDisplayMessageBox                                                    *
         * DESCRIPTION  : Common Method for display the messages.                                           *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 14Feb18                                                                           *
         ****************************************************************************************************/
        public static void commonGeneralDisplayMessageBox(int intMsgId)
        {
            General.DisplayMessageBoxLTR(intMsgId);
        }
        #endregion

        #region SetDisplayCommonErrorMessage
        /****************************************************************************************************
         * NAME         : SetDisplayCommonErrorMessage                                                      *
         * DESCRIPTION  : Get the details from the config and show the message                              *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 15Feb2018                                                                         *
         ****************************************************************************************************/
        public void SetDisplayCommonErrorMessage(string strTitle, string strType, string strMessage)
        {
            SaveConfigSettingsValue("MessageTitle", "ID_0", "Messages", strTitle);
            SaveConfigSettingsValue("MessageType", "ID_0", "Messages", strType);
            SaveConfigSettingsValue("MessageText", "ID_0", "Messages", strMessage);

            //clsCommon.commonGeneralDisplayMessageBox(0);
        }
        #endregion

        #region getSerialPort
        /****************************************************************************************************
         * NAME         : getSerialPort                                                                     *
         * DESCRIPTION  : Get the Serial Port details and Return the Details in to Datatables.              *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 26Feb18                                                                           *
         ****************************************************************************************************/
        public DataTable getSerialPort()
        {
            DataTable dtPorts = new DataTable();

            string[] ports = SerialPort.GetPortNames();

            dtPorts.Columns.Add("ValueText");
            dtPorts.Columns.Add("DisplayText");

            dtPorts.Rows.Add("", "None");
            foreach (string port in ports)
            {
                dtPorts.Rows.Add(port, port);
            }
            return dtPorts;
        }


        #endregion

        #region getBaudRate
        /****************************************************************************************************
         * NAME         : getBaudRate                                                                       *
         * DESCRIPTION  : Get the Baud Rate details and Return the Details in to Datatables.                *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 26Feb18                                                                           *
         ****************************************************************************************************/
        public DataTable getBaudRate()
        {
            DataTable dtBaudRate = new DataTable();
            dtBaudRate.Columns.Add("ValueText");
            dtBaudRate.Columns.Add("DisplayText");

            try
            {
                NameValueCollection columnDetails = clsCommon.ReadConfigGetSectionGroup("SerialPortSettings/SettingBaudRate");
                if (columnDetails != null)
                {
                    foreach (string key in columnDetails.AllKeys)
                    {
                        DataRow row = dtBaudRate.NewRow();
                        row[0] = key;
                        row[1] = columnDetails[key];
                        dtBaudRate.Rows.Add(row);
                    }
                }


            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }

            return dtBaudRate;
        }
        #endregion

        #region getParity
        /****************************************************************************************************
         * NAME         : getParity                                                                         *
         * DESCRIPTION  : Get the Parity details and Return the Details in to Datatables.                   *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 26Feb18                                                                           *
         ****************************************************************************************************/
        public DataTable getParity()
        {
            DataTable dtParity = new DataTable();
            string[] StrStopBits = Enum.GetNames(typeof(Parity));
            dtParity.Columns.Add("ValueText");
            dtParity.Columns.Add("DisplayText");
            foreach (string port in StrStopBits)
            {
                dtParity.Rows.Add(port, port);
            }
            return dtParity;

        }
        #endregion

        #region getDataBits
        /****************************************************************************************************
         * NAME         : getDataBits                                                                       *
         * DESCRIPTION  : Get the Data Bits details and Return the Details in to Datatables.                * 
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 26Feb18                                                                           *
         ****************************************************************************************************/
        public DataTable getDataBits()
        {
            DataTable dtDataBits = new DataTable();
            dtDataBits.Columns.Add("ValueText");
            dtDataBits.Columns.Add("DisplayText");

            try
            {
                NameValueCollection columnDetails = clsCommon.ReadConfigGetSectionGroup("SerialPortSettings/SettingDataBits");
                if (columnDetails != null)
                {
                    foreach (string key in columnDetails.AllKeys)
                    {
                        DataRow row = dtDataBits.NewRow();
                        row[0] = key;
                        row[1] = columnDetails[key];
                        dtDataBits.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                clsCommon.clsApplicationLogFileWriteLog(ex);
            }

            return dtDataBits;
        }
        #endregion

        #region getStopBits
        /****************************************************************************************************
         * NAME         : getStopBits                                                                       *
         * DESCRIPTION  : Get the Stop Bits details and Return the Details in to Datatables.                *
         * WRITTEN BY   : RajaSekar J                                                                       *
         * DATE         : 26Feb18                                                                           *
         ****************************************************************************************************/
        public DataTable getStopBits()
        {
            DataTable dtStopBits = new DataTable();
            string[] StrStopBits = Enum.GetNames(typeof(StopBits));
            dtStopBits.Columns.Add("ValueText");
            dtStopBits.Columns.Add("DisplayText");
            foreach (string port in StrStopBits)
            {
                dtStopBits.Rows.Add(port, port);
            }
            return dtStopBits;

        }
        #endregion
        
    }
}
