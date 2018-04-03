namespace Electra_MAC_Printing
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab(true);
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.utpcAppSettingsWizard_GeneralSettings = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panelGeneralSettings = new System.Windows.Forms.Panel();
            this.grpCommunicationSettings = new System.Windows.Forms.GroupBox();
            this.UltraPrinter = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.uCBO_GS_StripeStopBits = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.uCBO_GS_StripeParity = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.LBL_PrinterName = new System.Windows.Forms.Label();
            this.uCBO_GS_StripeBaudRate = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.TXT_DataAddress = new System.Windows.Forms.TextBox();
            this.uCBO_GS_StripeDataBits = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.LBL_DataAddress = new System.Windows.Forms.Label();
            this.uCBO_GS_StripeCom = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.TXT_SerialNumberAddress = new System.Windows.Forms.TextBox();
            this.LBL_SerialNumberAddress = new System.Windows.Forms.Label();
            this.Txt_ModbusSlaveAddress = new System.Windows.Forms.TextBox();
            this.LBL_ModbuSlaveAddress = new System.Windows.Forms.Label();
            this.LBL_UnitSettings = new System.Windows.Forms.Label();
            this.TXT_StationName = new System.Windows.Forms.TextBox();
            this.LBL_StationName = new System.Windows.Forms.Label();
            this.utpcAppSettingsWizard_Users = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panelUsersSettings = new System.Windows.Forms.Panel();
            this.uGrid_Users = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.uBTN_Settings_Cancel = new Infragistics.Win.Misc.UltraButton();
            this.uBTN_Settings_OK = new Infragistics.Win.Misc.UltraButton();
            this.utcAppSettingsWizard = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.utscpAppSettingsWizard_SharedControl = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.utpcAppSettingsWizard_GeneralSettings.SuspendLayout();
            this.panelGeneralSettings.SuspendLayout();
            this.grpCommunicationSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraPrinter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeStopBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeParity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeBaudRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeDataBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeCom)).BeginInit();
            this.utpcAppSettingsWizard_Users.SuspendLayout();
            this.panelUsersSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uGrid_Users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcAppSettingsWizard)).BeginInit();
            this.utcAppSettingsWizard.SuspendLayout();
            this.panelSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // utpcAppSettingsWizard_GeneralSettings
            // 
            this.utpcAppSettingsWizard_GeneralSettings.Controls.Add(this.panelGeneralSettings);
            this.utpcAppSettingsWizard_GeneralSettings.Location = new System.Drawing.Point(-10000, -10000);
            this.utpcAppSettingsWizard_GeneralSettings.Name = "utpcAppSettingsWizard_GeneralSettings";
            this.utpcAppSettingsWizard_GeneralSettings.Size = new System.Drawing.Size(659, 262);
            // 
            // panelGeneralSettings
            // 
            this.panelGeneralSettings.Controls.Add(this.grpCommunicationSettings);
            this.panelGeneralSettings.Controls.Add(this.TXT_StationName);
            this.panelGeneralSettings.Controls.Add(this.LBL_StationName);
            this.panelGeneralSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGeneralSettings.Location = new System.Drawing.Point(0, 0);
            this.panelGeneralSettings.Name = "panelGeneralSettings";
            this.panelGeneralSettings.Size = new System.Drawing.Size(659, 262);
            this.panelGeneralSettings.TabIndex = 0;
            // 
            // grpCommunicationSettings
            // 
            this.grpCommunicationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCommunicationSettings.Controls.Add(this.UltraPrinter);
            this.grpCommunicationSettings.Controls.Add(this.uCBO_GS_StripeStopBits);
            this.grpCommunicationSettings.Controls.Add(this.uCBO_GS_StripeParity);
            this.grpCommunicationSettings.Controls.Add(this.LBL_PrinterName);
            this.grpCommunicationSettings.Controls.Add(this.uCBO_GS_StripeBaudRate);
            this.grpCommunicationSettings.Controls.Add(this.TXT_DataAddress);
            this.grpCommunicationSettings.Controls.Add(this.uCBO_GS_StripeDataBits);
            this.grpCommunicationSettings.Controls.Add(this.LBL_DataAddress);
            this.grpCommunicationSettings.Controls.Add(this.uCBO_GS_StripeCom);
            this.grpCommunicationSettings.Controls.Add(this.TXT_SerialNumberAddress);
            this.grpCommunicationSettings.Controls.Add(this.LBL_SerialNumberAddress);
            this.grpCommunicationSettings.Controls.Add(this.Txt_ModbusSlaveAddress);
            this.grpCommunicationSettings.Controls.Add(this.LBL_ModbuSlaveAddress);
            this.grpCommunicationSettings.Controls.Add(this.LBL_UnitSettings);
            this.grpCommunicationSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpCommunicationSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCommunicationSettings.Location = new System.Drawing.Point(18, 87);
            this.grpCommunicationSettings.Name = "grpCommunicationSettings";
            this.grpCommunicationSettings.Size = new System.Drawing.Size(619, 348);
            this.grpCommunicationSettings.TabIndex = 15;
            this.grpCommunicationSettings.TabStop = false;
            this.grpCommunicationSettings.Text = "Communication Settings";
            // 
            // UltraPrinter
            // 
            this.UltraPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UltraPrinter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.UltraPrinter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UltraPrinter.Location = new System.Drawing.Point(266, 247);
            this.UltraPrinter.Name = "UltraPrinter";
            this.UltraPrinter.Size = new System.Drawing.Size(342, 32);
            this.UltraPrinter.TabIndex = 34;
            // 
            // uCBO_GS_StripeStopBits
            // 
            this.uCBO_GS_StripeStopBits.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.uCBO_GS_StripeStopBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uCBO_GS_StripeStopBits.Location = new System.Drawing.Point(781, 32);
            this.uCBO_GS_StripeStopBits.Name = "uCBO_GS_StripeStopBits";
            this.uCBO_GS_StripeStopBits.Size = new System.Drawing.Size(127, 32);
            this.uCBO_GS_StripeStopBits.TabIndex = 33;
            // 
            // uCBO_GS_StripeParity
            // 
            this.uCBO_GS_StripeParity.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.uCBO_GS_StripeParity.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uCBO_GS_StripeParity.Location = new System.Drawing.Point(524, 32);
            this.uCBO_GS_StripeParity.Name = "uCBO_GS_StripeParity";
            this.uCBO_GS_StripeParity.Size = new System.Drawing.Size(127, 32);
            this.uCBO_GS_StripeParity.TabIndex = 32;
            // 
            // LBL_PrinterName
            // 
            this.LBL_PrinterName.AutoSize = true;
            this.LBL_PrinterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_PrinterName.Location = new System.Drawing.Point(10, 251);
            this.LBL_PrinterName.Name = "LBL_PrinterName";
            this.LBL_PrinterName.Size = new System.Drawing.Size(135, 24);
            this.LBL_PrinterName.TabIndex = 27;
            this.LBL_PrinterName.Text = "Printer Settings";
            // 
            // uCBO_GS_StripeBaudRate
            // 
            this.uCBO_GS_StripeBaudRate.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.uCBO_GS_StripeBaudRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uCBO_GS_StripeBaudRate.Location = new System.Drawing.Point(395, 32);
            this.uCBO_GS_StripeBaudRate.Name = "uCBO_GS_StripeBaudRate";
            this.uCBO_GS_StripeBaudRate.Size = new System.Drawing.Size(127, 32);
            this.uCBO_GS_StripeBaudRate.TabIndex = 31;
            // 
            // TXT_DataAddress
            // 
            this.TXT_DataAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT_DataAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_DataAddress.Location = new System.Drawing.Point(266, 193);
            this.TXT_DataAddress.Name = "TXT_DataAddress";
            this.TXT_DataAddress.Size = new System.Drawing.Size(344, 29);
            this.TXT_DataAddress.TabIndex = 26;
            // 
            // uCBO_GS_StripeDataBits
            // 
            this.uCBO_GS_StripeDataBits.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.uCBO_GS_StripeDataBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uCBO_GS_StripeDataBits.Location = new System.Drawing.Point(653, 32);
            this.uCBO_GS_StripeDataBits.Name = "uCBO_GS_StripeDataBits";
            this.uCBO_GS_StripeDataBits.Size = new System.Drawing.Size(127, 32);
            this.uCBO_GS_StripeDataBits.TabIndex = 30;
            // 
            // LBL_DataAddress
            // 
            this.LBL_DataAddress.AutoSize = true;
            this.LBL_DataAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_DataAddress.Location = new System.Drawing.Point(10, 196);
            this.LBL_DataAddress.Name = "LBL_DataAddress";
            this.LBL_DataAddress.Size = new System.Drawing.Size(122, 24);
            this.LBL_DataAddress.TabIndex = 25;
            this.LBL_DataAddress.Text = "Data Address";
            // 
            // uCBO_GS_StripeCom
            // 
            this.uCBO_GS_StripeCom.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            this.uCBO_GS_StripeCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uCBO_GS_StripeCom.Location = new System.Drawing.Point(266, 32);
            this.uCBO_GS_StripeCom.Name = "uCBO_GS_StripeCom";
            this.uCBO_GS_StripeCom.Size = new System.Drawing.Size(127, 32);
            this.uCBO_GS_StripeCom.TabIndex = 29;
            // 
            // TXT_SerialNumberAddress
            // 
            this.TXT_SerialNumberAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT_SerialNumberAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_SerialNumberAddress.Location = new System.Drawing.Point(266, 138);
            this.TXT_SerialNumberAddress.Name = "TXT_SerialNumberAddress";
            this.TXT_SerialNumberAddress.Size = new System.Drawing.Size(344, 29);
            this.TXT_SerialNumberAddress.TabIndex = 24;
            // 
            // LBL_SerialNumberAddress
            // 
            this.LBL_SerialNumberAddress.AutoSize = true;
            this.LBL_SerialNumberAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_SerialNumberAddress.Location = new System.Drawing.Point(10, 141);
            this.LBL_SerialNumberAddress.Name = "LBL_SerialNumberAddress";
            this.LBL_SerialNumberAddress.Size = new System.Drawing.Size(206, 24);
            this.LBL_SerialNumberAddress.TabIndex = 23;
            this.LBL_SerialNumberAddress.Text = "Serial Number Address";
            // 
            // Txt_ModbusSlaveAddress
            // 
            this.Txt_ModbusSlaveAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_ModbusSlaveAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_ModbusSlaveAddress.Location = new System.Drawing.Point(266, 83);
            this.Txt_ModbusSlaveAddress.Name = "Txt_ModbusSlaveAddress";
            this.Txt_ModbusSlaveAddress.Size = new System.Drawing.Size(344, 29);
            this.Txt_ModbusSlaveAddress.TabIndex = 22;
            // 
            // LBL_ModbuSlaveAddress
            // 
            this.LBL_ModbuSlaveAddress.AutoSize = true;
            this.LBL_ModbuSlaveAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_ModbuSlaveAddress.Location = new System.Drawing.Point(10, 86);
            this.LBL_ModbuSlaveAddress.Name = "LBL_ModbuSlaveAddress";
            this.LBL_ModbuSlaveAddress.Size = new System.Drawing.Size(205, 24);
            this.LBL_ModbuSlaveAddress.TabIndex = 21;
            this.LBL_ModbuSlaveAddress.Text = "Modbus Slave Address";
            // 
            // LBL_UnitSettings
            // 
            this.LBL_UnitSettings.AutoSize = true;
            this.LBL_UnitSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_UnitSettings.Location = new System.Drawing.Point(10, 36);
            this.LBL_UnitSettings.Name = "LBL_UnitSettings";
            this.LBL_UnitSettings.Size = new System.Drawing.Size(113, 24);
            this.LBL_UnitSettings.TabIndex = 1;
            this.LBL_UnitSettings.Text = "Unit Settings";
            // 
            // TXT_StationName
            // 
            this.TXT_StationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT_StationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_StationName.Location = new System.Drawing.Point(288, 30);
            this.TXT_StationName.Name = "TXT_StationName";
            this.TXT_StationName.Size = new System.Drawing.Size(340, 29);
            this.TXT_StationName.TabIndex = 6;
            // 
            // LBL_StationName
            // 
            this.LBL_StationName.AutoSize = true;
            this.LBL_StationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_StationName.Location = new System.Drawing.Point(28, 33);
            this.LBL_StationName.Name = "LBL_StationName";
            this.LBL_StationName.Size = new System.Drawing.Size(122, 24);
            this.LBL_StationName.TabIndex = 1;
            this.LBL_StationName.Text = "Station Name";
            // 
            // utpcAppSettingsWizard_Users
            // 
            this.utpcAppSettingsWizard_Users.Controls.Add(this.panelUsersSettings);
            this.utpcAppSettingsWizard_Users.Location = new System.Drawing.Point(1, 25);
            this.utpcAppSettingsWizard_Users.Name = "utpcAppSettingsWizard_Users";
            this.utpcAppSettingsWizard_Users.Size = new System.Drawing.Size(659, 262);
            // 
            // panelUsersSettings
            // 
            this.panelUsersSettings.Controls.Add(this.uGrid_Users);
            this.panelUsersSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUsersSettings.Location = new System.Drawing.Point(0, 0);
            this.panelUsersSettings.Name = "panelUsersSettings";
            this.panelUsersSettings.Size = new System.Drawing.Size(659, 262);
            this.panelUsersSettings.TabIndex = 1;
            // 
            // uGrid_Users
            // 
            this.uGrid_Users.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.uGrid_Users.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.uGrid_Users.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.uGrid_Users.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.FixedAddRowOnTop;
            this.uGrid_Users.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.True;
            this.uGrid_Users.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.uGrid_Users.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.uGrid_Users.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uGrid_Users.Location = new System.Drawing.Point(0, 0);
            this.uGrid_Users.Margin = new System.Windows.Forms.Padding(5);
            this.uGrid_Users.Name = "uGrid_Users";
            this.uGrid_Users.Size = new System.Drawing.Size(659, 262);
            this.uGrid_Users.TabIndex = 1;
            this.uGrid_Users.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.uGrid_Users_AfterCellUpdate);
            this.uGrid_Users.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uGrid_Users_InitializeLayout);
            this.uGrid_Users.InitializeTemplateAddRow += new Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventHandler(this.uGrid_Users_InitializeTemplateAddRow);
            this.uGrid_Users.AfterRowsDeleted += new System.EventHandler(this.uGrid_Users_AfterRowsDeleted);
            this.uGrid_Users.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.uGrid_Users_BeforeRowsDeleted);
            // 
            // uBTN_Settings_Cancel
            // 
            this.uBTN_Settings_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.WhiteSmoke;
            appearance1.ImageHAlign = Infragistics.Win.HAlign.Left;
            this.uBTN_Settings_Cancel.Appearance = appearance1;
            this.uBTN_Settings_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uBTN_Settings_Cancel.ImageSize = new System.Drawing.Size(32, 32);
            this.uBTN_Settings_Cancel.Location = new System.Drawing.Point(501, 330);
            this.uBTN_Settings_Cancel.Name = "uBTN_Settings_Cancel";
            this.uBTN_Settings_Cancel.Size = new System.Drawing.Size(180, 45);
            this.uBTN_Settings_Cancel.TabIndex = 5;
            this.uBTN_Settings_Cancel.Text = "Close";
            this.uBTN_Settings_Cancel.Click += new System.EventHandler(this.uBTN_Settings_Cancel_Click);
            // 
            // uBTN_Settings_OK
            // 
            this.uBTN_Settings_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.WhiteSmoke;
            appearance2.ImageHAlign = Infragistics.Win.HAlign.Left;
            this.uBTN_Settings_OK.Appearance = appearance2;
            this.uBTN_Settings_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uBTN_Settings_OK.ImageSize = new System.Drawing.Size(35, 30);
            this.uBTN_Settings_OK.Location = new System.Drawing.Point(296, 330);
            this.uBTN_Settings_OK.Name = "uBTN_Settings_OK";
            this.uBTN_Settings_OK.Size = new System.Drawing.Size(180, 45);
            this.uBTN_Settings_OK.TabIndex = 4;
            this.uBTN_Settings_OK.Text = "Save";
            this.uBTN_Settings_OK.Click += new System.EventHandler(this.uBTN_Settings_OK_Click);
            // 
            // utcAppSettingsWizard
            // 
            appearance3.BackColor = System.Drawing.Color.White;
            this.utcAppSettingsWizard.ActiveTabAppearance = appearance3;
            this.utcAppSettingsWizard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance4.BackColor = System.Drawing.Color.White;
            this.utcAppSettingsWizard.ClientAreaAppearance = appearance4;
            this.utcAppSettingsWizard.Controls.Add(this.utscpAppSettingsWizard_SharedControl);
            this.utcAppSettingsWizard.Controls.Add(this.utpcAppSettingsWizard_GeneralSettings);
            this.utcAppSettingsWizard.Controls.Add(this.utpcAppSettingsWizard_Users);
            this.utcAppSettingsWizard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.utcAppSettingsWizard.Location = new System.Drawing.Point(20, 20);
            this.utcAppSettingsWizard.Name = "utcAppSettingsWizard";
            appearance5.BackColor = System.Drawing.Color.White;
            this.utcAppSettingsWizard.SelectedTabAppearance = appearance5;
            this.utcAppSettingsWizard.SharedControlsPage = this.utscpAppSettingsWizard_SharedControl;
            this.utcAppSettingsWizard.Size = new System.Drawing.Size(661, 288);
            this.utcAppSettingsWizard.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel;
            this.utcAppSettingsWizard.TabIndex = 0;
            appearance6.BackColor = System.Drawing.Color.White;
            appearance6.ForeColor = System.Drawing.Color.Black;
            ultraTab1.ActiveAppearance = appearance6;
            appearance7.BackColor = System.Drawing.Color.Gray;
            appearance7.ForeColor = System.Drawing.Color.Black;
            ultraTab1.Appearance = appearance7;
            ultraTab1.Key = "general";
            ultraTab1.TabPage = this.utpcAppSettingsWizard_GeneralSettings;
            ultraTab1.Text = "General Settings";
            appearance8.BackColor = System.Drawing.Color.Gray;
            ultraTab2.Appearance = appearance8;
            ultraTab2.Key = "users";
            ultraTab2.TabPage = this.utpcAppSettingsWizard_Users;
            ultraTab2.Text = "Users";
            this.utcAppSettingsWizard.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2});
            this.utcAppSettingsWizard.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.utcAppSettingsWizard.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(this.utcAppSettingsWizard_SelectedTabChanged);
            // 
            // utscpAppSettingsWizard_SharedControl
            // 
            this.utscpAppSettingsWizard_SharedControl.Location = new System.Drawing.Point(-10000, -10000);
            this.utscpAppSettingsWizard_SharedControl.Name = "utscpAppSettingsWizard_SharedControl";
            this.utscpAppSettingsWizard_SharedControl.Size = new System.Drawing.Size(659, 262);
            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.Color.White;
            this.panelSettings.Controls.Add(this.uBTN_Settings_Cancel);
            this.panelSettings.Controls.Add(this.uBTN_Settings_OK);
            this.panelSettings.Controls.Add(this.utcAppSettingsWizard);
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSettings.Location = new System.Drawing.Point(0, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(685, 390);
            this.panelSettings.TabIndex = 3;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 390);
            this.Controls.Add(this.panelSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "Electra MAC Print Settings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.utpcAppSettingsWizard_GeneralSettings.ResumeLayout(false);
            this.panelGeneralSettings.ResumeLayout(false);
            this.panelGeneralSettings.PerformLayout();
            this.grpCommunicationSettings.ResumeLayout(false);
            this.grpCommunicationSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraPrinter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeStopBits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeParity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeBaudRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeDataBits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uCBO_GS_StripeCom)).EndInit();
            this.utpcAppSettingsWizard_Users.ResumeLayout(false);
            this.panelUsersSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uGrid_Users)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcAppSettingsWizard)).EndInit();
            this.utcAppSettingsWizard.ResumeLayout(false);
            this.panelSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraButton uBTN_Settings_Cancel;
        private Infragistics.Win.Misc.UltraButton uBTN_Settings_OK;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl utcAppSettingsWizard;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage utscpAppSettingsWizard_SharedControl;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl utpcAppSettingsWizard_GeneralSettings;
        private System.Windows.Forms.Panel panelGeneralSettings;
        private System.Windows.Forms.GroupBox grpCommunicationSettings;
        private System.Windows.Forms.TextBox Txt_ModbusSlaveAddress;
        private System.Windows.Forms.Label LBL_ModbuSlaveAddress;
        private System.Windows.Forms.TextBox TXT_StationName;
        private System.Windows.Forms.Label LBL_StationName;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl utpcAppSettingsWizard_Users;
        private System.Windows.Forms.Panel panelUsersSettings;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Label LBL_PrinterName;
        private System.Windows.Forms.TextBox TXT_DataAddress;
        private System.Windows.Forms.Label LBL_DataAddress;
        private System.Windows.Forms.TextBox TXT_SerialNumberAddress;
        private System.Windows.Forms.Label LBL_SerialNumberAddress;
        private Infragistics.Win.UltraWinGrid.UltraGrid uGrid_Users;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor uCBO_GS_StripeStopBits;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor uCBO_GS_StripeParity;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor uCBO_GS_StripeBaudRate;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor uCBO_GS_StripeDataBits;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor uCBO_GS_StripeCom;
        private System.Windows.Forms.Label LBL_UnitSettings;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor UltraPrinter;
    }
}