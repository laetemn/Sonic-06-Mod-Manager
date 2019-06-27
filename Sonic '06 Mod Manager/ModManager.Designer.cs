﻿namespace Sonic_06_Mod_Manager
{
    partial class ModManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModManager));
            this.playButton = new System.Windows.Forms.Button();
            this.group_Mods = new System.Windows.Forms.GroupBox();
            this.combo_Priority = new System.Windows.Forms.ComboBox();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.btn_DownerPriority = new System.Windows.Forms.Button();
            this.btn_UpperPriority = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.modList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.check_manUninstall = new System.Windows.Forms.CheckBox();
            this.combo_System = new System.Windows.Forms.ComboBox();
            this.lbl_System = new System.Windows.Forms.Label();
            this.check_FTP = new System.Windows.Forms.CheckBox();
            this.modsButton = new System.Windows.Forms.Button();
            this.lbl_ModsDirectory = new System.Windows.Forms.Label();
            this.modsBox = new System.Windows.Forms.TextBox();
            this.xeniaButton = new System.Windows.Forms.Button();
            this.lbl_XeniaExecutable = new System.Windows.Forms.Label();
            this.xeniaBox = new System.Windows.Forms.TextBox();
            this.s06PathButton = new System.Windows.Forms.Button();
            this.lbl_GameDirectory = new System.Windows.Forms.Label();
            this.s06PathBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tm_CreatorDisposal = new System.Windows.Forms.Timer(this.components);
            this.tool_Information = new System.Windows.Forms.ToolTip(this.components);
            this.lbl_Username = new System.Windows.Forms.Label();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.userField = new System.Windows.Forms.TextBox();
            this.passField = new System.Windows.Forms.TextBox();
            this.launchXeniaButton = new System.Windows.Forms.Button();
            this.group_Mods.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playButton.BackColor = System.Drawing.Color.LightGreen;
            this.playButton.FlatAppearance.BorderSize = 0;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Location = new System.Drawing.Point(106, 458);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(90, 23);
            this.playButton.TabIndex = 31;
            this.playButton.Text = "Install Mods";
            this.playButton.UseVisualStyleBackColor = false;
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // group_Mods
            // 
            this.group_Mods.Controls.Add(this.combo_Priority);
            this.group_Mods.Controls.Add(this.btn_DeselectAll);
            this.group_Mods.Controls.Add(this.btn_SelectAll);
            this.group_Mods.Controls.Add(this.btn_DownerPriority);
            this.group_Mods.Controls.Add(this.btn_UpperPriority);
            this.group_Mods.Controls.Add(this.button1);
            this.group_Mods.Controls.Add(this.createButton);
            this.group_Mods.Controls.Add(this.refreshButton);
            this.group_Mods.Controls.Add(this.modList);
            this.group_Mods.Location = new System.Drawing.Point(10, 4);
            this.group_Mods.Name = "group_Mods";
            this.group_Mods.Size = new System.Drawing.Size(375, 298);
            this.group_Mods.TabIndex = 37;
            this.group_Mods.TabStop = false;
            this.group_Mods.Text = "Mods";
            // 
            // combo_Priority
            // 
            this.combo_Priority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Priority.FormattingEnabled = true;
            this.combo_Priority.Items.AddRange(new object[] {
            "Top to Bottom",
            "Bottom to Top"});
            this.combo_Priority.Location = new System.Drawing.Point(95, 268);
            this.combo_Priority.Name = "combo_Priority";
            this.combo_Priority.Size = new System.Drawing.Size(120, 21);
            this.combo_Priority.TabIndex = 50;
            this.combo_Priority.SelectedIndexChanged += new System.EventHandler(this.Combo_Priority_SelectedIndexChanged);
            // 
            // btn_DeselectAll
            // 
            this.btn_DeselectAll.BackColor = System.Drawing.Color.Tomato;
            this.btn_DeselectAll.FlatAppearance.BorderSize = 0;
            this.btn_DeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeselectAll.Location = new System.Drawing.Point(6, 267);
            this.btn_DeselectAll.Name = "btn_DeselectAll";
            this.btn_DeselectAll.Size = new System.Drawing.Size(83, 23);
            this.btn_DeselectAll.TabIndex = 41;
            this.btn_DeselectAll.Text = "Deselect All";
            this.btn_DeselectAll.UseVisualStyleBackColor = false;
            this.btn_DeselectAll.Click += new System.EventHandler(this.Btn_DeselectAll_Click);
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_SelectAll.FlatAppearance.BorderSize = 0;
            this.btn_SelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SelectAll.Location = new System.Drawing.Point(6, 238);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(83, 23);
            this.btn_SelectAll.TabIndex = 40;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // btn_DownerPriority
            // 
            this.btn_DownerPriority.BackColor = System.Drawing.Color.White;
            this.btn_DownerPriority.Enabled = false;
            this.btn_DownerPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DownerPriority.Location = new System.Drawing.Point(125, 238);
            this.btn_DownerPriority.Name = "btn_DownerPriority";
            this.btn_DownerPriority.Size = new System.Drawing.Size(26, 23);
            this.btn_DownerPriority.TabIndex = 39;
            this.btn_DownerPriority.Text = "▼";
            this.btn_DownerPriority.UseVisualStyleBackColor = false;
            this.btn_DownerPriority.Click += new System.EventHandler(this.Btn_DownerPriority_Click);
            // 
            // btn_UpperPriority
            // 
            this.btn_UpperPriority.BackColor = System.Drawing.Color.White;
            this.btn_UpperPriority.Enabled = false;
            this.btn_UpperPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UpperPriority.Location = new System.Drawing.Point(95, 238);
            this.btn_UpperPriority.Name = "btn_UpperPriority";
            this.btn_UpperPriority.Size = new System.Drawing.Size(26, 23);
            this.btn_UpperPriority.TabIndex = 38;
            this.btn_UpperPriority.Text = "▲";
            this.btn_UpperPriority.UseVisualStyleBackColor = false;
            this.btn_UpperPriority.Click += new System.EventHandler(this.Btn_UpperPriority_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(221, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "Mod Info";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // createButton
            // 
            this.createButton.BackColor = System.Drawing.Color.LightGreen;
            this.createButton.FlatAppearance.BorderSize = 0;
            this.createButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createButton.Location = new System.Drawing.Point(249, 238);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(119, 23);
            this.createButton.TabIndex = 36;
            this.createButton.Text = "Create New Mod";
            this.createButton.UseVisualStyleBackColor = false;
            this.createButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.refreshButton.FlatAppearance.BorderSize = 0;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Location = new System.Drawing.Point(157, 238);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(86, 23);
            this.refreshButton.TabIndex = 35;
            this.refreshButton.Text = "Refresh Mods";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // modList
            // 
            this.modList.FormattingEnabled = true;
            this.modList.Location = new System.Drawing.Point(6, 17);
            this.modList.Name = "modList";
            this.modList.Size = new System.Drawing.Size(362, 214);
            this.modList.TabIndex = 34;
            this.modList.SelectedIndexChanged += new System.EventHandler(this.ModList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.check_manUninstall);
            this.groupBox1.Controls.Add(this.combo_System);
            this.groupBox1.Controls.Add(this.lbl_System);
            this.groupBox1.Controls.Add(this.check_FTP);
            this.groupBox1.Controls.Add(this.modsButton);
            this.groupBox1.Controls.Add(this.lbl_ModsDirectory);
            this.groupBox1.Controls.Add(this.modsBox);
            this.groupBox1.Controls.Add(this.xeniaButton);
            this.groupBox1.Controls.Add(this.lbl_XeniaExecutable);
            this.groupBox1.Controls.Add(this.xeniaBox);
            this.groupBox1.Controls.Add(this.s06PathButton);
            this.groupBox1.Controls.Add(this.lbl_GameDirectory);
            this.groupBox1.Controls.Add(this.s06PathBox);
            this.groupBox1.Location = new System.Drawing.Point(10, 304);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 121);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Directories";
            // 
            // check_manUninstall
            // 
            this.check_manUninstall.AutoSize = true;
            this.check_manUninstall.Location = new System.Drawing.Point(193, 96);
            this.check_manUninstall.Name = "check_manUninstall";
            this.check_manUninstall.Size = new System.Drawing.Size(102, 17);
            this.check_manUninstall.TabIndex = 49;
            this.check_manUninstall.Text = "Manual uninstall";
            this.tool_Information.SetToolTip(this.check_manUninstall, "This option will add an Uninstall Mods button for Xenia users to keep mods instal" +
        "led when closing Xenia.");
            this.check_manUninstall.UseVisualStyleBackColor = true;
            this.check_manUninstall.CheckedChanged += new System.EventHandler(this.Check_manUninstall_CheckedChanged);
            // 
            // combo_System
            // 
            this.combo_System.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_System.FormattingEnabled = true;
            this.combo_System.Items.AddRange(new object[] {
            "Xbox 360",
            "PlayStation 3"});
            this.combo_System.Location = new System.Drawing.Point(247, 93);
            this.combo_System.Name = "combo_System";
            this.combo_System.Size = new System.Drawing.Size(121, 21);
            this.combo_System.TabIndex = 48;
            this.combo_System.Visible = false;
            this.combo_System.SelectedIndexChanged += new System.EventHandler(this.Combo_System_SelectedIndexChanged);
            // 
            // lbl_System
            // 
            this.lbl_System.AutoSize = true;
            this.lbl_System.Location = new System.Drawing.Point(202, 97);
            this.lbl_System.Name = "lbl_System";
            this.lbl_System.Size = new System.Drawing.Size(44, 13);
            this.lbl_System.TabIndex = 47;
            this.lbl_System.Text = "System:";
            this.lbl_System.Visible = false;
            // 
            // check_FTP
            // 
            this.check_FTP.AutoSize = true;
            this.check_FTP.Location = new System.Drawing.Point(100, 96);
            this.check_FTP.Name = "check_FTP";
            this.check_FTP.Size = new System.Drawing.Size(86, 17);
            this.check_FTP.TabIndex = 46;
            this.check_FTP.Text = "FTP Server?";
            this.tool_Information.SetToolTip(this.check_FTP, "This option is for transferring mods to real hardware. Enable this to make Sonic " +
        "\'06 Mod Manager work with a modded Xbox 360 or PlayStation 3.");
            this.check_FTP.UseVisualStyleBackColor = true;
            this.check_FTP.CheckedChanged += new System.EventHandler(this.Check_FTP_CheckedChanged);
            // 
            // modsButton
            // 
            this.modsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.modsButton.FlatAppearance.BorderSize = 0;
            this.modsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modsButton.Location = new System.Drawing.Point(346, 17);
            this.modsButton.Name = "modsButton";
            this.modsButton.Size = new System.Drawing.Size(22, 20);
            this.modsButton.TabIndex = 45;
            this.modsButton.Text = "...";
            this.modsButton.UseVisualStyleBackColor = false;
            this.modsButton.Click += new System.EventHandler(this.ModsButton_Click);
            // 
            // lbl_ModsDirectory
            // 
            this.lbl_ModsDirectory.AutoSize = true;
            this.lbl_ModsDirectory.Location = new System.Drawing.Point(17, 20);
            this.lbl_ModsDirectory.Name = "lbl_ModsDirectory";
            this.lbl_ModsDirectory.Size = new System.Drawing.Size(81, 13);
            this.lbl_ModsDirectory.TabIndex = 44;
            this.lbl_ModsDirectory.Text = "Mods Directory:";
            this.tool_Information.SetToolTip(this.lbl_ModsDirectory, "Click here to open your Mods directory...");
            this.lbl_ModsDirectory.Click += new System.EventHandler(this.Lbl_ModsDirectory_Click);
            // 
            // modsBox
            // 
            this.modsBox.Location = new System.Drawing.Point(100, 17);
            this.modsBox.Name = "modsBox";
            this.modsBox.Size = new System.Drawing.Size(241, 20);
            this.modsBox.TabIndex = 43;
            this.modsBox.TextChanged += new System.EventHandler(this.ModsBox_TextChanged);
            // 
            // xeniaButton
            // 
            this.xeniaButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.xeniaButton.FlatAppearance.BorderSize = 0;
            this.xeniaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xeniaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xeniaButton.Location = new System.Drawing.Point(346, 69);
            this.xeniaButton.Name = "xeniaButton";
            this.xeniaButton.Size = new System.Drawing.Size(22, 20);
            this.xeniaButton.TabIndex = 42;
            this.xeniaButton.Text = "...";
            this.xeniaButton.UseVisualStyleBackColor = false;
            this.xeniaButton.Click += new System.EventHandler(this.XeniaButton_Click);
            // 
            // lbl_XeniaExecutable
            // 
            this.lbl_XeniaExecutable.AutoSize = true;
            this.lbl_XeniaExecutable.Location = new System.Drawing.Point(5, 72);
            this.lbl_XeniaExecutable.Name = "lbl_XeniaExecutable";
            this.lbl_XeniaExecutable.Size = new System.Drawing.Size(93, 13);
            this.lbl_XeniaExecutable.TabIndex = 41;
            this.lbl_XeniaExecutable.Text = "Xenia Executable:";
            this.tool_Information.SetToolTip(this.lbl_XeniaExecutable, "Click here to launch Xenia...");
            this.lbl_XeniaExecutable.Click += new System.EventHandler(this.Lbl_XeniaExecutable_Click);
            // 
            // xeniaBox
            // 
            this.xeniaBox.Location = new System.Drawing.Point(100, 69);
            this.xeniaBox.Name = "xeniaBox";
            this.xeniaBox.Size = new System.Drawing.Size(241, 20);
            this.xeniaBox.TabIndex = 40;
            this.xeniaBox.TextChanged += new System.EventHandler(this.XeniaBox_TextChanged);
            // 
            // s06PathButton
            // 
            this.s06PathButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.s06PathButton.FlatAppearance.BorderSize = 0;
            this.s06PathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.s06PathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s06PathButton.Location = new System.Drawing.Point(346, 43);
            this.s06PathButton.Name = "s06PathButton";
            this.s06PathButton.Size = new System.Drawing.Size(22, 20);
            this.s06PathButton.TabIndex = 39;
            this.s06PathButton.Text = "...";
            this.s06PathButton.UseVisualStyleBackColor = false;
            this.s06PathButton.Click += new System.EventHandler(this.S06PathButton_Click);
            // 
            // lbl_GameDirectory
            // 
            this.lbl_GameDirectory.AutoSize = true;
            this.lbl_GameDirectory.Location = new System.Drawing.Point(15, 46);
            this.lbl_GameDirectory.Name = "lbl_GameDirectory";
            this.lbl_GameDirectory.Size = new System.Drawing.Size(83, 13);
            this.lbl_GameDirectory.TabIndex = 38;
            this.lbl_GameDirectory.Text = "Game Directory:";
            this.tool_Information.SetToolTip(this.lbl_GameDirectory, "Click here to open your Game directory...");
            this.lbl_GameDirectory.Click += new System.EventHandler(this.Lbl_GameDirectory_Click);
            // 
            // s06PathBox
            // 
            this.s06PathBox.Location = new System.Drawing.Point(100, 43);
            this.s06PathBox.Name = "s06PathBox";
            this.s06PathBox.Size = new System.Drawing.Size(241, 20);
            this.s06PathBox.TabIndex = 37;
            this.s06PathBox.TextChanged += new System.EventHandler(this.S06PathBox_TextChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(298, 458);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 39;
            this.button2.Text = "About";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // tm_CreatorDisposal
            // 
            this.tm_CreatorDisposal.Tick += new System.EventHandler(this.Tm_CreatorDisposal_Tick);
            // 
            // tool_Information
            // 
            this.tool_Information.AutoPopDelay = 10000;
            this.tool_Information.InitialDelay = 500;
            this.tool_Information.ReshowDelay = 100;
            this.tool_Information.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tool_Information.ToolTipTitle = "Information";
            // 
            // lbl_Username
            // 
            this.lbl_Username.AutoSize = true;
            this.lbl_Username.Location = new System.Drawing.Point(11, 434);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(58, 13);
            this.lbl_Username.TabIndex = 43;
            this.lbl_Username.Text = "Username:";
            this.tool_Information.SetToolTip(this.lbl_Username, "This field is typically left empty for the PlayStation 3.");
            this.lbl_Username.Visible = false;
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Location = new System.Drawing.Point(201, 434);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(56, 13);
            this.lbl_Password.TabIndex = 45;
            this.lbl_Password.Text = "Password:";
            this.tool_Information.SetToolTip(this.lbl_Password, "This field is typically left empty for the PlayStation 3.");
            this.lbl_Password.Visible = false;
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.BackColor = System.Drawing.Color.Tomato;
            this.stopButton.FlatAppearance.BorderSize = 0;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Location = new System.Drawing.Point(202, 458);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(90, 23);
            this.stopButton.TabIndex = 40;
            this.stopButton.Text = "Uninstall Mods";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Visible = false;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // userField
            // 
            this.userField.Location = new System.Drawing.Point(72, 431);
            this.userField.Name = "userField";
            this.userField.Size = new System.Drawing.Size(124, 20);
            this.userField.TabIndex = 42;
            this.userField.Visible = false;
            this.userField.TextChanged += new System.EventHandler(this.UserField_TextChanged);
            // 
            // passField
            // 
            this.passField.Location = new System.Drawing.Point(260, 431);
            this.passField.Name = "passField";
            this.passField.PasswordChar = '*';
            this.passField.Size = new System.Drawing.Size(124, 20);
            this.passField.TabIndex = 44;
            this.passField.Visible = false;
            this.passField.TextChanged += new System.EventHandler(this.PassField_TextChanged);
            // 
            // launchXeniaButton
            // 
            this.launchXeniaButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.launchXeniaButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.launchXeniaButton.FlatAppearance.BorderSize = 0;
            this.launchXeniaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.launchXeniaButton.Location = new System.Drawing.Point(10, 458);
            this.launchXeniaButton.Name = "launchXeniaButton";
            this.launchXeniaButton.Size = new System.Drawing.Size(90, 23);
            this.launchXeniaButton.TabIndex = 46;
            this.launchXeniaButton.Text = "Play";
            this.launchXeniaButton.UseVisualStyleBackColor = false;
            this.launchXeniaButton.Visible = false;
            this.launchXeniaButton.Click += new System.EventHandler(this.LaunchXeniaButton_Click);
            // 
            // ModManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 490);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.passField);
            this.Controls.Add(this.lbl_Username);
            this.Controls.Add(this.userField);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.group_Mods);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.launchXeniaButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(410, 503);
            this.Name = "ModManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sonic \'06 Mod Manager";
            this.Load += new System.EventHandler(this.ModManager_Load);
            this.group_Mods.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.GroupBox group_Mods;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.CheckedListBox modList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button modsButton;
        private System.Windows.Forms.Label lbl_ModsDirectory;
        private System.Windows.Forms.TextBox modsBox;
        private System.Windows.Forms.Button xeniaButton;
        private System.Windows.Forms.Label lbl_XeniaExecutable;
        private System.Windows.Forms.TextBox xeniaBox;
        private System.Windows.Forms.Button s06PathButton;
        private System.Windows.Forms.Label lbl_GameDirectory;
        private System.Windows.Forms.TextBox s06PathBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer tm_CreatorDisposal;
        private System.Windows.Forms.ToolTip tool_Information;
        private System.Windows.Forms.CheckBox check_FTP;
        private System.Windows.Forms.ComboBox combo_System;
        private System.Windows.Forms.Label lbl_System;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button btn_DownerPriority;
        private System.Windows.Forms.Button btn_UpperPriority;
        private System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.ComboBox combo_Priority;
        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.TextBox userField;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.TextBox passField;
        private System.Windows.Forms.CheckBox check_manUninstall;
        private System.Windows.Forms.Button launchXeniaButton;
    }
}

