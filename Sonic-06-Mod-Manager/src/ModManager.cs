﻿using System;
using System.IO;
using Unify.Tools;
using System.Text;
using System.Linq;
using Unify.Patcher;
using Ookii.Dialogs;
using Unify.Messages;
using System.Drawing;
using Microsoft.Win32;
using Unify.Networking;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

// Welcome to Project Unify!

// Project Unify is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2019 Knuxfan24 & HyperPolygon64

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Sonic_06_Mod_Manager
{
    public partial class ModManager : Form
    {
        public readonly string versionNumber = "Version 2.0-indev"; // Defines the version number to be used globally
        public static List<string> configs = new List<string>() { }; // Defines the configs list for 'mod.ini' files
        public static bool debugMode = false;

        public ModManager(string[] args) {
            InitializeComponent();
            ChangeAccentColours(); //Update colours from user settings on program launch.
            ChangeThemeColours(); //Select the theme that is being used from user settings on program launch.

            //Load settings from the Properties.
            #region Properties
            combo_Emulator_System.SelectedIndex = Properties.Settings.Default.emulatorSystem;
            combo_FTP_System.SelectedIndex = Properties.Settings.Default.ftpSystem;

            if (combo_Emulator_System.SelectedIndex == 0)
                text_EmulatorPath.Text = Properties.Settings.Default.xeniaPath;
            else
                text_EmulatorPath.Text = Properties.Settings.Default.RPCS3Path;

            combo_API.SelectedIndex = Properties.Settings.Default.API;
            text_GameDirectory.Text = Properties.Settings.Default.gameDirectory;
            text_ModsDirectory.Text = Properties.Settings.Default.modsDirectory;

            switch (Properties.Settings.Default.filter)
            {
                case 0:
                    radio_All.Checked = true;
                    break;
                case 1:
                    radio_Xbox360.Checked = true;
                    break;
                case 2:
                    radio_PlayStation3.Checked = true;
                    break;
            }

            check_RTV.Checked = Properties.Settings.Default.emulator_RTV;
            check_2xRes.Checked = Properties.Settings.Default.emulator_2xRes;
            check_VSync.Checked = Properties.Settings.Default.emulator_VSync;
            check_ProtectZero.Checked = Properties.Settings.Default.emulator_ProtectZero;
            check_Gamma.Checked = Properties.Settings.Default.emulator_Gamma;
            check_Debug.Checked = Properties.Settings.Default.emulator_Debug;
            check_Fullscreen.Checked = Properties.Settings.Default.emulator_Fullscreen;
            check_Discord.Checked = Properties.Settings.Default.emulator_Discord;
            combo_Reflections.SelectedIndex = Properties.Settings.Default.patches_Reflections;
            check_FTP.Checked = Properties.Settings.Default.FTP;
            check_ManualInstall.Checked = Properties.Settings.Default.manualInstall;
            nud_CameraDistance.Value = Properties.Settings.Default.patches_CameraDistance;
            nud_CameraHeight.Value = Properties.Settings.Default.patches_CameraHeight;
            check_ManualPatches.Checked = Properties.Settings.Default.manualPatches;

            switch (Properties.Settings.Default.priority)
            {
                case false:
                    btn_Priority.Text = "Priority: Top to Bottom";
                    break;
                case true:
                    btn_Priority.Text = "Priority: Bottom to Top";
                    break;
            }

            RegistryKey key = Registry.ClassesRoot.OpenSubKey(GB_Registry.protocol, false); // Open the Sonic '06 Mod Manager protocol key
            if (key == null) // If the key does not exist, keep the checkbox unchecked.
                check_GameBanana.Checked = false;
            else
                check_GameBanana.Checked = true;
            #endregion
        }

        public string Status { set { lbl_SetStatus.Text = value; } }
        private void ModManager_Shown(object sender, EventArgs e) { // Using the Shown method is cleaner, as it waits for the main window to appear before performing tasks
            GetMods(); // Basically just refreshes and clears up mods on launch
            if (Directory.Exists(Properties.Settings.Default.gameDirectory) && !check_ManualInstall.Checked) ARC.CleanupMods(); // Ensures manual install is disabled first
            if (!versionNumber.Contains("-indev")) Updater.CheckForUpdates(versionNumber, "https://segacarnival.com/hyper/updates/sonic-06-mod-manager/latest-master.exe", "https://segacarnival.com/hyper/updates/sonic-06-mod-manager/latest_master.txt", string.Empty);
        }

        #region Mods
        private void Btn_ModInfo_Click(object sender, EventArgs e) {
            Status = SystemMessages.msg_ModInfo;
            new src.ModInfo(Path.GetDirectoryName(configs[clb_ModsList.SelectedIndex])).ShowDialog();
            Status = SystemMessages.msg_DefaultStatus;
        }

        private void Btn_EditMod_Click(object sender, EventArgs e) {
            Status = SystemMessages.msg_EditMod;
            new src.ModCreator(Path.GetDirectoryName(configs[clb_ModsList.SelectedIndex]), true).ShowDialog();
            Status = SystemMessages.msg_DefaultStatus;
            GetMods();
        }

        private void Btn_SaveAndPlay_Click(object sender, EventArgs e) {
            if ((btn_SaveAndPlay.Text == "Save and Play" || btn_SaveAndPlay.Text == "Install Mods") && !check_FTP.Checked)
            {
                try
                {
                    ARC.CleanupMods();

                    if (Properties.Settings.Default.priority == false)
                    {
                        //Top to Bottom Priority
                        for (int i = clb_ModsList.Items.Count - 1; i >= 0; i--)
                        {
                            if (clb_ModsList.GetItemChecked(i))
                            {
                                Status = SystemMessages.msg_InstallingMod(clb_ModsList.Items[i].ToString());
                                ARC.InstallMods(Path.GetDirectoryName(configs[i]));
                                Status = SystemMessages.msg_DefaultStatus;
                            }
                        }
                    }
                    else
                    {
                        //Bottom to Top Priority
                        foreach (object mod in clb_ModsList.CheckedItems)
                        {
                            Status = SystemMessages.msg_InstallingMod(clb_ModsList.GetItemText(mod));
                            ARC.InstallMods(Path.GetDirectoryName(configs[clb_ModsList.Items.IndexOf(mod)]));
                            Status = SystemMessages.msg_DefaultStatus;
                        }
                    }

                    if (!check_ManualPatches.Checked) PatchAll();

                    //Show a MessageBox explaining what mods were skipped due to failing to copy.
                    if (ARC.skippedMods.ToString() != string.Empty)
                    {
                        StringBuilder getString = new StringBuilder();
                        foreach (var modName in ARC.skippedMods)
                            getString.Append(modName);

                        if (getString.Length > 0)
                            UnifyMessages.UnifyMessage.Show(ModsMessages.ex_SkippedModsTally(getString.ToString()), SystemMessages.tl_SuccessWarn, "OK", "Warning");
                    }
                    if (!check_ManualInstall.Checked)
                    {
                        if (combo_Emulator_System.SelectedIndex == 0)
                        {
                            Status = SystemMessages.msg_LaunchXenia;
                            LaunchXenia();
                            Status = SystemMessages.msg_DefaultStatus;
                        }
                        if (combo_Emulator_System.SelectedIndex == 1)
                        {
                            Status = SystemMessages.msg_LaunchRPCS3;
                            LaunchRPCS3();
                            Status = SystemMessages.msg_DefaultStatus;
                        }
                    }
                }
                catch (Exception ex)
                {
                    UnifyMessages.UnifyMessage.Show($"{ModsMessages.ex_ModInstallFailure}\n\n{ex}", SystemMessages.tl_FileError, "OK", "Warning");
                    unifytb_Main.SelectedIndex = 3;
                    ARC.CleanupMods();
                    Status = SystemMessages.msg_DefaultStatus;
                }
            }
            else if (btn_SaveAndPlay.Text == "Apply Patches") PatchAll();
        }


        private void PatchAll()
        {
            var files = Directory.GetFiles(Properties.Settings.Default.gameDirectory, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".arc"));

            foreach (var arc in files)
            {
                if (Path.GetFileName(arc) == "cache.arc")
                {
                    if (clb_PatchesList.GetItemChecked(3))
                    {
                        Status = SystemMessages.msg_PatchingRenderer;
                        if (!File.Exists($"{arc}_orig")) File.Copy(arc, $"{arc}_orig");
                        string unpack = ARC.UnpackARC(arc);
                        File.WriteAllBytes(Path.Combine(unpack, "cache\\xenon\\scripts\\render\\render_gamemode.lub"), Properties.Resources.vulkan_render_gamemode);
                        File.WriteAllBytes(Path.Combine(unpack, "cache\\xenon\\scripts\\render\\render_title.lub"), Properties.Resources.vulkan_render_title);
                        File.WriteAllBytes(Path.Combine(unpack, "cache\\xenon\\scripts\\render\\core\\render_main.lub"), Properties.Resources.vulkan_render_main);
                        ARC.RepackARC(unpack, arc);
                        Status = SystemMessages.msg_DefaultStatus;
                    }
                    else
                    {
                        Status = SystemMessages.msg_PatchingRenderer;
                        if (!File.Exists($"{arc}_orig")) File.Copy(arc, $"{arc}_orig");
                        string unpack = ARC.UnpackARC(arc);
                        File.WriteAllBytes(Path.Combine(unpack, "cache\\xenon\\scripts\\render\\render_gamemode.lub"), Properties.Resources.retail_render_gamemode);
                        File.WriteAllBytes(Path.Combine(unpack, "cache\\xenon\\scripts\\render\\render_title.lub"), Properties.Resources.retail_render_title);
                        File.WriteAllBytes(Path.Combine(unpack, "cache\\xenon\\scripts\\render\\core\\render_main.lub"), Properties.Resources.retail_render_main);
                        ARC.RepackARC(unpack, arc);
                        Status = SystemMessages.msg_DefaultStatus;
                    }

                    if (combo_Reflections.SelectedIndex != 1)
                    {
                        Status = SystemMessages.msg_PatchingReflections;
                        if (!File.Exists($"{arc}_orig")) File.Copy(arc, $"{arc}_orig");
                        string unpack = ARC.UnpackARC(arc);
                        Lua.Reflections(Path.Combine(unpack, "cache\\xenon\\scripts\\render\\core\\render_reflection.lub"), combo_Reflections.SelectedIndex);
                        ARC.RepackARC(unpack, arc);
                        Status = SystemMessages.msg_DefaultStatus;
                    }

                    Status = SystemMessages.msg_PatchingHUD;
                    if (!File.Exists($"{arc}_orig")) File.Copy(arc, $"{arc}_orig");
                    string disableHUD = ARC.UnpackARC(arc);
                    Lua.DisableHUD(Path.Combine(disableHUD, "cache\\xenon\\scripts\\render\\render_gamemode.lub"), !clb_PatchesList.GetItemChecked(0));
                    ARC.RepackARC(disableHUD, arc);
                    Status = SystemMessages.msg_DefaultStatus;

                    Status = SystemMessages.msg_PatchingShadows;
                    if (!File.Exists($"{arc}_orig")) File.Copy(arc, $"{arc}_orig");
                    string disableShadows = ARC.UnpackARC(arc);
                    Lua.DisableShadows(Path.Combine(disableShadows, "cache\\xenon\\scripts\\render\\render_gamemode.lub"), !clb_PatchesList.GetItemChecked(1));
                    ARC.RepackARC(disableShadows, arc);
                    Status = SystemMessages.msg_DefaultStatus;
                }
                else if (Path.GetFileName(arc) == "game.arc")
                {
                    if (nud_CameraDistance.Value != 650)
                    {
                        Status = SystemMessages.msg_PatchingCamera;
                        if (!File.Exists($"{arc}_orig")) File.Copy(arc, $"{arc}_orig");
                        string unpack = ARC.UnpackARC(arc);
                        Lua.CameraDistance(Path.Combine(unpack, "game\\xenon\\cameraparam.lub"), decimal.ToInt32(nud_CameraDistance.Value));
                        ARC.RepackARC(unpack, arc);
                        Status = SystemMessages.msg_DefaultStatus;
                    }
                    if (nud_CameraHeight.Value != 15)
                    {
                        Status = SystemMessages.msg_PatchingCamera;
                        if (!File.Exists($"{arc}_orig")) File.Copy(arc, $"{arc}_orig");
                        string unpack = ARC.UnpackARC(arc);
                        Lua.CameraHeight(Path.Combine(unpack, "game\\xenon\\cameraparam.lub"), decimal.ToInt32(nud_CameraHeight.Value));
                        ARC.RepackARC(unpack, arc);
                        Status = SystemMessages.msg_DefaultStatus;
                    }
                }
                else if (Path.GetFileName(arc) == "player_omega.arc")
                {
                    if (clb_PatchesList.GetItemChecked(2))
                    {
                        Status = SystemMessages.msg_PatchingOmega;
                        if (!File.Exists($"{arc}_orig")) File.Copy(arc, $"{arc}_orig");
                        string omegaBlurFix = ARC.UnpackARC(arc);
                        File.WriteAllBytes(Path.Combine(omegaBlurFix, "player_omega\\win32\\player\\omega\\omega_Root.xno"), Properties.Resources.omega_Root_Fix);
                        ARC.RepackARC(omegaBlurFix, arc);
                        Status = SystemMessages.msg_DefaultStatus;
                    }
                    else
                    {
                        Status = SystemMessages.msg_PatchingOmega;
                        if (!File.Exists($"{arc}_orig")) File.Copy(arc, $"{arc}_orig");
                        string omegaBlurFix = ARC.UnpackARC(arc);
                        File.WriteAllBytes(Path.Combine(omegaBlurFix, "player_omega\\win32\\player\\omega\\omega_Root.xno"), Properties.Resources.omega_Root_Retail);
                        ARC.RepackARC(omegaBlurFix, arc);
                        Status = SystemMessages.msg_DefaultStatus;
                    }
                }
            }
        }

        private void Btn_UninstallMods_Click(object sender, EventArgs e) {
            if (btn_UninstallMods.Text == "Restore Defaults")
                ARC.CleanupPatches();
            else
                ARC.CleanupMods();
        }

        private void Btn_RefreshMods_Click(object sender, EventArgs e) { GetMods(); }

        private void Btn_Save_Click(object sender, EventArgs e) {
            Properties.Settings.Default.emulator_RTV = check_RTV.Checked;
            Properties.Settings.Default.emulator_2xRes = check_2xRes.Checked;
            Properties.Settings.Default.emulator_VSync = check_VSync.Checked;
            Properties.Settings.Default.emulator_ProtectZero = check_ProtectZero.Checked;
            Properties.Settings.Default.emulator_Gamma = check_Gamma.Checked;
            Properties.Settings.Default.emulator_Debug = check_Debug.Checked;
            Properties.Settings.Default.emulator_Fullscreen = check_Fullscreen.Checked;
            Properties.Settings.Default.emulator_Discord = check_Discord.Checked;

            SaveChecks();
            GetMods();

            Properties.Settings.Default.Save();
        }

        private void Btn_Priority_Click(object sender, EventArgs e) { // Switches priority - cleaner than using a combo box in this case
            if (!Properties.Settings.Default.priority) {
                btn_Priority.Text = "Priority: Bottom to Top";
                Properties.Settings.Default.priority = true;
            } else {
                btn_Priority.Text = "Priority: Top to Bottom";
                Properties.Settings.Default.priority = false;
            }
            Properties.Settings.Default.Save();
        }

        private void GetMods() {
            clb_ModsList.Items.Clear();
            btn_UpperPriority.Enabled = false;
            btn_DownerPriority.Enabled = false;
            btn_ModInfo.Enabled = false;
            btn_EditMod.Enabled = false;

            //Ask the user for a mod directory if the textbox for it is empty/the specified path doesn't exist.
            if (text_ModsDirectory.Text == string.Empty || !Directory.Exists(text_ModsDirectory.Text)) {
                UnifyMessages.UnifyMessage.Show(ModsMessages.msg_NoModDirectory, SystemMessages.tl_DefaultTitle, "OK", "Information");

                VistaFolderBrowserDialog mods = new VistaFolderBrowserDialog {
                    Description = SettingsMessages.msg_LocateMods,
                    UseDescriptionForTitle = true,
                };

                if (mods.ShowDialog() == DialogResult.OK) {
                    text_ModsDirectory.Text = mods.SelectedPath;
                    Properties.Settings.Default.modsDirectory = mods.SelectedPath;
                    Properties.Settings.Default.Save();
                }
                else Application.Exit(); // Close the program if no mods directory is set. Why? Because it's redundant.
            }

            try {
                configs.Clear();
                foreach (var mod in Directory.GetFiles(Properties.Settings.Default.modsDirectory, "mod.ini", SearchOption.AllDirectories)) {
                    using (StreamReader configFile = new StreamReader(mod)) {
                        string line = string.Empty;
                        string entryValue = string.Empty;
                        string modName = string.Empty;

                        try {
                            while ((line = configFile.ReadLine()) != null) {
                                if (line.Contains("Title=\"")) {
                                    entryValue = line.Substring(line.IndexOf("=") + 2);
                                    modName = entryValue.Remove(entryValue.Length - 1); // Gets title directly from 'mod.ini'
                                }

                                //Handle Platforms based on Radio Buttons.
                                if (line.Contains("Platform=\"")) {
                                    entryValue = line.Substring(line.IndexOf("=") + 2);
                                    entryValue = entryValue.Remove(entryValue.Length - 1);

                                    if (!radio_PlayStation3.Checked && entryValue.Contains("Xbox 360")) {
                                        clb_ModsList.Items.Add(modName);
                                        configs.Add(mod);
                                    }

                                    if (!radio_Xbox360.Checked && entryValue.Contains("PlayStation 3")) {
                                        clb_ModsList.Items.Add(modName);
                                        configs.Add(mod);
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }

                GetModsChecks();
                GetPatchesChecks();
            }
            catch (Exception ex) { UnifyMessages.UnifyMessage.Show($"{ModsMessages.ex_ModListError}\n\n{ex}", SystemMessages.tl_ListError, "OK", "Error"); }
        }

        private void GetModsChecks()
        {
            string line = string.Empty; // Declare empty string for StreamReader

            using (StreamReader mods = new StreamReader(Path.Combine(Properties.Settings.Default.modsDirectory, "mods.ini"))) { // Read 'mods.ini'
                mods.ReadLine(); // Skip [Main] line
                while ((line = mods.ReadLine()) != null) { // Read all lines until null
                    if (clb_ModsList.Items.Contains(line)) { // If the mods list contains what's on the current line...
                        int checkedIndex = clb_ModsList.Items.IndexOf(line); // Get the index of the mod already in the mods list
                        string cachePath = configs[checkedIndex]; // Get the index of the mod in the configs list

                        clb_ModsList.Items.RemoveAt(checkedIndex); // Remove the mod already in the mods list
                        configs.Remove(cachePath); // Remove the config location from the configs list

                        clb_ModsList.Items.Insert(checkedIndex - checkedIndex, line); // Insert the mod by the name provided in 'mods.ini', given it's at least present in the list
                        configs.Insert(checkedIndex - checkedIndex, cachePath); // Insert the mod at the top of the configs list to re-arrange the information
                        clb_ModsList.SetItemChecked(checkedIndex - checkedIndex, true); // Set the new item to the checked state
                    }
                }
            }
        }

        private void GetPatchesChecks()
        {
            string line = string.Empty; // Declare empty string for StreamReader

            using (StreamReader patches = new StreamReader(Path.Combine(Properties.Settings.Default.modsDirectory, "patches.ini"))) { // Read 'patches.ini'
                patches.ReadLine(); // Skip [Main] line
                while ((line = patches.ReadLine()) != null) { // Read all lines until null
                    if (clb_PatchesList.Items.Contains(line)) { // If the mods list contains what's on the current line...
                        int checkedIndex = clb_PatchesList.Items.IndexOf(line); // Get the index of the mod already in the mods list
                        clb_PatchesList.SetItemChecked(checkedIndex, true); // Set the new item to the checked state
                    }
                }
            }
        }

        private void SaveChecks()
        {
            //Save the names of the selected mods and the indexes of the selected patches to their appropriate ini files
            string modCheckList = Path.Combine(text_ModsDirectory.Text, "mods.ini");
            string patchCheckList = Path.Combine(text_ModsDirectory.Text, "patches.ini");

            using (StreamWriter sw = File.CreateText(modCheckList))
                sw.WriteLine("[Main]"); //Header

            for (int i = clb_ModsList.Items.Count - 1; i >= 0; i--) { // Writes in reverse so the mods list writes it in it's preferred order
                if (clb_ModsList.GetItemChecked(i)) 
                    using (StreamWriter sw = File.AppendText(modCheckList)) 
                        sw.WriteLine(clb_ModsList.Items[i].ToString()); //Mod Name
            }

            using (StreamWriter sw = File.CreateText(patchCheckList))
                sw.WriteLine("[Main]"); //Header

            for (int i = clb_PatchesList.Items.Count - 1; i >= 0; i--) // Writes in reverse so the mods list writes it in it's preferred order
            {
                if (clb_PatchesList.GetItemChecked(i))
                    using (StreamWriter sw = File.AppendText(patchCheckList))
                        sw.WriteLine(clb_PatchesList.Items[i].ToString()); //Mod Name
            }
        }

        private void Radio_All_CheckedChanged(object sender, EventArgs e) { // Refreshes mods list based on All filter
            Properties.Settings.Default.filter = 0;
            Properties.Settings.Default.Save();
            GetMods();
        }

        private void Radio_Xbox360_CheckedChanged(object sender, EventArgs e) { // Refreshes mods list based on Xbox 360 filter
            Properties.Settings.Default.filter = 1;
            Properties.Settings.Default.Save();
            GetMods();
        }

        private void Radio_PlayStation3_CheckedChanged(object sender, EventArgs e) { // Refreshes mods list based on PlayStation 3 filter
            Properties.Settings.Default.filter = 2;
            Properties.Settings.Default.Save();
            GetMods();
        }

        private void Btn_Play_Click(object sender, EventArgs e) { // Launches the emulator respective to what system is chosen
            if (combo_Emulator_System.SelectedIndex == 0) {
                Status = SystemMessages.msg_LaunchXenia;
                LaunchXenia();
            }
            if (combo_Emulator_System.SelectedIndex == 1) {
                Status = SystemMessages.msg_LaunchRPCS3;
                LaunchRPCS3();
            }
        }

        //Checks all available checkboxes.
        private void Btn_SelectAll_Click(object sender, EventArgs e) { for (int i = 0; i < clb_ModsList.Items.Count; i++) clb_ModsList.SetItemChecked(i, true); }

        //Unchecks all available checkboxes.
        private void Btn_DeselectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_ModsList.Items.Count; i++) clb_ModsList.SetItemChecked(i, false);
            clb_ModsList.ClearSelected();
            btn_CreateNewMod.Width = 245;
            btn_EditMod.Visible = false;
        }

        private void Btn_UpperPriority_Click(object sender, EventArgs e) { // Moves selected checkbox up the list
            int selectedIndex = clb_ModsList.SelectedIndex; // Declares the selected index
            object selectedItem = clb_ModsList.SelectedItem; // Selected checkbox
            string cachePath = configs[selectedIndex]; // Info based on selectedIndex
            bool check = false; // Check state bool

            if (clb_ModsList.GetItemCheckState(selectedIndex) == CheckState.Checked) { check = true; } // Checks if the checkbox was checked

            clb_ModsList.Items.RemoveAt(selectedIndex); // Removes the selected checkbox
            configs.Remove(cachePath); // Remove the selected item's info from the configs list
            selectedIndex -= 1; // Move index up the list

            clb_ModsList.Items.Insert(selectedIndex, selectedItem); // Insert checkbox at selectedIndex
            configs.Insert(selectedIndex, cachePath); // Shifts the moved checkbox's info up the configs list
            clb_ModsList.SelectedIndex = selectedIndex; // Selects the recently moved checkbox
            clb_ModsList.SetItemChecked(selectedIndex, check); // Calls the 'check' bool and sets the checked state
        }

        private void Btn_DownerPriority_Click(object sender, EventArgs e) { // Moves selected checkbox down the list
            int selectedIndex = clb_ModsList.SelectedIndex; // Declares the selected index
            object selectedItem = clb_ModsList.SelectedItem; // Selected checkbox
            string cachePath = configs[selectedIndex]; // Info based on selectedIndex
            bool check = false; // Check state bool

            if (clb_ModsList.GetItemCheckState(selectedIndex) == CheckState.Checked) { check = true; } // Checks if the checkbox was checked

            clb_ModsList.Items.RemoveAt(selectedIndex); // Removes the selected checkbox
            configs.Remove(cachePath); // Remove the selected item's info from the configs list
            selectedIndex += 1; // Move index down the list

            clb_ModsList.Items.Insert(selectedIndex, selectedItem); // Insert checkbox at selectedIndex
            configs.Insert(selectedIndex, cachePath); // Shifts the moved checkbox's info up the configs list
            clb_ModsList.SelectedIndex = selectedIndex; // Selects the recently moved checkbox
            clb_ModsList.SetItemChecked(selectedIndex, check); // Calls the 'check' bool and sets the checked state
        }

        private void Clb_ModsList_SelectedIndexChanged(object sender, EventArgs e) {
            btn_ModInfo.Enabled = clb_ModsList.SelectedIndex >= 0; // Enables/disables the Mod Info button depending on if a checkbox is selected
            btn_EditMod.Visible = clb_ModsList.SelectedIndex >= 0; btn_EditMod.Enabled = clb_ModsList.SelectedIndex >= 0; // Enables/disables the Edit Mod button depending on if a checkbox is selected
            btn_CreateNewMod.Width = 120; // Sets Create New Mod button width to fit the Edit Mod button
            btn_UpperPriority.Enabled = clb_ModsList.SelectedIndex > 0; // Enables/disables the Upper Priority button depending on if a checkbox is selected
            btn_DownerPriority.Enabled = clb_ModsList.SelectedIndex >= 0 && clb_ModsList.SelectedIndex < clb_ModsList.Items.Count - 1; // Enables/disables the Downer Priority button depending on if a checkbox is selected
        }

        private void Btn_CreateNewMod_Click(object sender, EventArgs e) { // Opens the Mod Creator form - refreshes upon exit
            Status = SystemMessages.msg_CreateNewMod;
            new src.ModCreator(string.Empty, false).ShowDialog();
            Status = SystemMessages.msg_DefaultStatus;
            GetMods();
        }
        #endregion

        #region Emulator
        private void LaunchXenia() {
            if (text_GameDirectory.Text == string.Empty) {
                //Select game directory and save if we don't have one specified.
                VistaFolderBrowserDialog game = new VistaFolderBrowserDialog {
                    Description = EmulatorMessages.msg_LocateGame,
                    UseDescriptionForTitle = true,
                };

                if (game.ShowDialog() == DialogResult.OK) {
                    text_GameDirectory.Text = game.SelectedPath;
                    Properties.Settings.Default.gameDirectory = game.SelectedPath;
                    Properties.Settings.Default.Save();
                }
                else return;
            }

            if (text_EmulatorPath.Text != string.Empty) {
                ProcessStartInfo xeniaExec;
                List<string> xeniaParameters = new List<string>() { };

                if (File.Exists(Path.Combine(text_GameDirectory.Text, "default.xex"))) { xeniaParameters.Add($"\"{Path.Combine(text_GameDirectory.Text, "default.xex")}\""); }

                if (combo_API.SelectedIndex == 1) { //Make sure we're using DX12 before adding the D3D12 Parameters.
                    if (check_RTV.Checked) xeniaParameters.Add("--d3d12_edram_rov=false"); // Render Target Views
                    if (check_2xRes.Checked) xeniaParameters.Add("--d3d12_resolution_scale=2"); // 2x Resolution
                }
                if (combo_API.SelectedIndex == 0) xeniaParameters.Add("--gpu=vulkan"); // Vulkan
                if (!check_VSync.Checked) xeniaParameters.Add("--vsync=false"); // V-Sync
                if (!check_ProtectZero.Checked) xeniaParameters.Add("--protect_zero=false"); // Protect Zero
                if (check_Gamma.Checked) xeniaParameters.Add("--kernel_display_gamma_type=2"); // Enable Gamma
                if (check_Debug.Checked) xeniaParameters.Add("--debug"); // Debug
                if (check_Fullscreen.Checked) xeniaParameters.Add("--fullscreen"); // Launch in Fullscreen
                if (!check_Discord.Checked) xeniaParameters.Add("--discord=false"); // Discord Rich Presence

                // Saves selected settings
                Properties.Settings.Default.emulator_RTV = check_RTV.Checked;
                Properties.Settings.Default.emulator_2xRes = check_2xRes.Checked;
                Properties.Settings.Default.emulator_VSync = check_VSync.Checked;
                Properties.Settings.Default.emulator_ProtectZero = check_ProtectZero.Checked;
                Properties.Settings.Default.emulator_Gamma = check_Gamma.Checked;
                Properties.Settings.Default.emulator_Debug = check_Debug.Checked;
                Properties.Settings.Default.emulator_Fullscreen = check_Fullscreen.Checked;
                Properties.Settings.Default.emulator_Discord = check_Discord.Checked;
                Properties.Settings.Default.Save();

                // Checks if the array is populated before applying arguments
                if (xeniaParameters.ToArray().Length > 0) {
                    xeniaExec = new ProcessStartInfo(text_EmulatorPath.Text) {
                        WorkingDirectory = Path.GetDirectoryName(text_EmulatorPath.Text),
                        Arguments = string.Join(" ", xeniaParameters.ToArray())
                    };
                }
                else {
                    xeniaExec = new ProcessStartInfo(text_EmulatorPath.Text)
                    { WorkingDirectory = Path.GetDirectoryName(text_EmulatorPath.Text) };
                }

                var xenia = Process.Start(xeniaExec); // Launch Xenia
                Status = SystemMessages.msg_XeniaExitCall;
                xenia.WaitForExit(); // Wait for Xenia to exit
                if (!check_ManualInstall.Checked) ARC.CleanupMods();
                Status = SystemMessages.msg_DefaultStatus;
            }
            else {
                text_EmulatorPath.Text = Locations.LocateEmulator(); // Relocate Xenia if the emulator path is not set
                LaunchXenia();
            }
        }

        private void LaunchRPCS3()
        {
            if (text_GameDirectory.Text == string.Empty) {
                //Select game directory and save if we don't have one specified.
                VistaFolderBrowserDialog game = new VistaFolderBrowserDialog {
                    Description = EmulatorMessages.msg_LocateGame,
                    UseDescriptionForTitle = true,
                };

                if (game.ShowDialog() == DialogResult.OK) {
                    text_GameDirectory.Text = game.SelectedPath;
                    Properties.Settings.Default.gameDirectory = game.SelectedPath;
                    Properties.Settings.Default.Save();
                }
                else return;
            }

            if (text_EmulatorPath.Text != string.Empty) {
                ProcessStartInfo RPCS3Exec;
                List<string> RPCS3Parameters = new List<string>() { };

                if (File.Exists(Path.Combine(text_GameDirectory.Text, "EBOOT.BIN"))) { RPCS3Parameters.Add($"\"{Path.Combine(text_GameDirectory.Text, "EBOOT.BIN")}\""); }

                // Checks if the array is populated before applying arguments
                if (RPCS3Parameters.ToArray().Length > 0) {
                    RPCS3Exec = new ProcessStartInfo(text_EmulatorPath.Text) {
                        WorkingDirectory = Path.GetDirectoryName(text_EmulatorPath.Text),
                        Arguments = string.Join(" ", RPCS3Parameters.ToArray())
                    };
                }
                else {
                    RPCS3Exec = new ProcessStartInfo(text_EmulatorPath.Text)
                    { WorkingDirectory = Path.GetDirectoryName(text_EmulatorPath.Text) };
                }

                var RPCS3 = Process.Start(RPCS3Exec); // Launch RPCS3
                Status = SystemMessages.msg_RPCS3ExitCall;
                RPCS3.WaitForExit(); // Wait for RPCS3 to exit
                if (!check_ManualInstall.Checked) ARC.CleanupMods();
                Status = SystemMessages.msg_DefaultStatus;
            }
            else {
                text_EmulatorPath.Text = Locations.LocateEmulator(); // Relocate RPCS3 if the emulator path is not set
                LaunchRPCS3();
            }
        }

        private void Btn_EmulatorPath_Click(object sender, EventArgs e) { text_EmulatorPath.Text = Locations.LocateEmulator(); } // Locate the emulator of choice

        private void Combo_Emulator_System_SelectedIndexChanged(object sender, EventArgs e) {
            //Depending on the selected system and theme, change text to disabled colour.
            if (combo_Emulator_System.SelectedIndex == 0) {
                group_Settings.Enabled = true;
                combo_API.Enabled = true;

                if (!Properties.Settings.Default.theme) {
                    lbl_API.ForeColor = SystemColors.ControlText;
                    lbl_ForceRTV.ForeColor = SystemColors.ControlText;
                    lbl_2xResolution.ForeColor = SystemColors.ControlText;
                    lbl_VSync.ForeColor = SystemColors.ControlText;
                    lbl_ProtectZero.ForeColor = SystemColors.ControlText;
                    lbl_EnableGamma.ForeColor = SystemColors.ControlText;
                    lbl_Fullscreen.ForeColor = SystemColors.ControlText;
                    lbl_Discord.ForeColor = SystemColors.ControlText;
                    lbl_Debug.ForeColor = SystemColors.ControlText;
                    lbl_SettingsOverlay.ForeColor = SystemColors.ControlText;
                }
                else {
                    lbl_API.ForeColor = SystemColors.Control;
                    lbl_ForceRTV.ForeColor = SystemColors.Control;
                    lbl_2xResolution.ForeColor = SystemColors.Control;
                    lbl_VSync.ForeColor = SystemColors.Control;
                    lbl_ProtectZero.ForeColor = SystemColors.Control;
                    lbl_EnableGamma.ForeColor = SystemColors.Control;
                    lbl_Fullscreen.ForeColor = SystemColors.Control;
                    lbl_Discord.ForeColor = SystemColors.Control;
                    lbl_Debug.ForeColor = SystemColors.Control;
                    lbl_SettingsOverlay.ForeColor = SystemColors.Control;
                }
                text_EmulatorPath.Text = Properties.Settings.Default.xeniaPath;
            }
            else {
                //Disable Xenia specific objects when RPCS3 is selected
                group_Settings.Enabled = false;
                combo_API.Enabled = false;
                lbl_API.ForeColor = SystemColors.GrayText;
                lbl_ForceRTV.ForeColor = SystemColors.GrayText;
                lbl_2xResolution.ForeColor = SystemColors.GrayText;
                lbl_VSync.ForeColor = SystemColors.GrayText;
                lbl_ProtectZero.ForeColor = SystemColors.GrayText;
                lbl_EnableGamma.ForeColor = SystemColors.GrayText;
                lbl_Fullscreen.ForeColor = SystemColors.GrayText;
                lbl_Discord.ForeColor = SystemColors.GrayText;
                lbl_Debug.ForeColor = SystemColors.GrayText;
                lbl_SettingsOverlay.ForeColor = SystemColors.GrayText;

                text_EmulatorPath.Text = Properties.Settings.Default.RPCS3Path;
            }
            Properties.Settings.Default.emulatorSystem = combo_Emulator_System.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void Combo_API_SelectedIndexChanged(object sender, EventArgs e) {
            //Depending on the selected API and theme, change text to disabled colour.
            if (combo_API.SelectedIndex == 0) {
                check_RTV.Enabled = false;
                check_2xRes.Enabled = false;
                lbl_ForceRTV.ForeColor = SystemColors.GrayText;
                lbl_2xResolution.ForeColor = SystemColors.GrayText;
            }
            else {
                check_RTV.Enabled = true;
                check_2xRes.Enabled = true;
                
                if (!Properties.Settings.Default.theme) {
                    if (combo_Emulator_System.SelectedIndex == 1) {
                        lbl_ForceRTV.ForeColor = SystemColors.GrayText;
                        lbl_2xResolution.ForeColor = SystemColors.GrayText;
                    }
                    else {
                        lbl_ForceRTV.ForeColor = SystemColors.ControlText;
                        lbl_2xResolution.ForeColor = SystemColors.ControlText;
                    }
                }
                else {
                    if (combo_Emulator_System.SelectedIndex == 1) {
                        lbl_ForceRTV.ForeColor = SystemColors.GrayText;
                        lbl_2xResolution.ForeColor = SystemColors.GrayText;
                    }
                    else {
                        lbl_ForceRTV.ForeColor = SystemColors.Control;
                        lbl_2xResolution.ForeColor = SystemColors.Control;
                    }
                }
            }
            Properties.Settings.Default.API = combo_API.SelectedIndex;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Patches
        private void Combo_Reflections_SelectedIndexChanged(object sender, EventArgs e) { // Save Reflections value
            Properties.Settings.Default.patches_Reflections = combo_Reflections.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void Btn_ResetReflections_Click(object sender, EventArgs e) { // Default Reflections to Quarter
            combo_Reflections.SelectedIndex = 1;
            Properties.Settings.Default.patches_Reflections = 1;
            Properties.Settings.Default.Save();
        }

        private void Btn_ResetCameraDistance_Click(object sender, EventArgs e) { // Default Camera Distance to 650
            nud_CameraDistance.Value = 650;
            Properties.Settings.Default.patches_CameraDistance = 650;
            Properties.Settings.Default.Save();
        }

        private void Btn_ResetCameraHeight_Click(object sender, EventArgs e) { // Default Camera Height to 15
            nud_CameraHeight.Value = 15;
            Properties.Settings.Default.patches_CameraHeight = 15;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Settings
        private void Check_ManualPatches_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.manualPatches = check_ManualPatches.Checked;
            Properties.Settings.Default.Save();
        }

        private void Btn_Update_Click(object sender, EventArgs e) { Updater.CheckForUpdates(versionNumber, "https://segacarnival.com/hyper/updates/sonic-06-mod-manager/latest-master.exe", "https://segacarnival.com/hyper/updates/sonic-06-mod-manager/latest_master.txt", "user"); }

        private void Combo_FTP_System_SelectedIndexChanged(object sender, EventArgs e) { // Save FTP value
            Properties.Settings.Default.ftpSystem = combo_FTP_System.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void Btn_ModsFolder_Click(object sender, EventArgs e) { text_ModsDirectory.Text = Locations.LocateMods(); } // Locate Mods folder

        private void Btn_GameFolder_Click(object sender, EventArgs e) { text_GameDirectory.Text = Locations.LocateGame(); } // Locate Game folder

        private void Btn_ColourPicker_Click(object sender, System.EventArgs e) {
            //Create the Colour Picker, with the Custom Colours menu open and the colour set to the one from settings.
            ColorDialog accentPicker = new ColorDialog {
                FullOpen = true,
                Color = Properties.Settings.Default.accentColour
            };

            if (accentPicker.ShowDialog() == DialogResult.OK)
                Properties.Settings.Default.accentColour = accentPicker.Color;

            Properties.Settings.Default.Save();
            ChangeAccentColours();
        }

        private void ChangeAccentColours() {
            btn_ColourPicker.BackColor = Properties.Settings.Default.accentColour; //Change the colour of the selector button.
            unifytb_Main.ActiveColor = Properties.Settings.Default.accentColour; //Colour the selected tab is highlighted in.
            unifytb_Main.HorizontalLineColor = Properties.Settings.Default.accentColour; //Colour the line at the top is.
            unifytb_Main.Refresh(); //Refresh user control to remove software rendering leftovers.
        }

        private void Unifytb_Main_SelectedIndexChanged(object sender, System.EventArgs e) {
            clb_ModsList.ClearSelected();
            clb_PatchesList.ClearSelected();

            if (check_ManualInstall.Checked)
            {
                Properties.Settings.Default.manualInstall = true;
                check_FTP.Enabled = false;
                lbl_FTP.ForeColor = SystemColors.GrayText;
                btn_SaveAndPlay.Text = "Install Mods";
                btn_SaveAndPlay.Width = 120;
                btn_UninstallMods.Visible = true;
            }
            else
            {
                Properties.Settings.Default.manualInstall = false;
                check_FTP.Enabled = true;
                if (!Properties.Settings.Default.theme)
                    lbl_FTP.ForeColor = SystemColors.ControlText;
                else
                    lbl_FTP.ForeColor = SystemColors.Control;
                btn_SaveAndPlay.Text = "Save and Play";
                btn_SaveAndPlay.Width = 245;
                btn_UninstallMods.Visible = false;
            }
            if (unifytb_Main.SelectedIndex == 2) {
                btn_SaveAndPlay.Text = "Apply Patches";
                btn_SaveAndPlay.Width = 120;
                btn_UninstallMods.Text = "Restore Defaults";
                btn_UninstallMods.Visible = true;
            }
            else {
                if (check_FTP.Checked || check_ManualInstall.Checked) {
                    btn_SaveAndPlay.Text = "Install Mods";
                    btn_SaveAndPlay.Width = 120;
                    btn_UninstallMods.Text = "Uninstall Mods";
                    btn_UninstallMods.Visible = true;
                }
                else {
                    btn_SaveAndPlay.Text = "Save and Play";
                    btn_SaveAndPlay.Width = 245;
                    btn_UninstallMods.Text = "Uninstall Mods";
                    btn_UninstallMods.Visible = false;
                }
            }
            btn_CreateNewMod.Width = 245; // Reset Create New Mod button size from Edit Mod compacted size
            btn_EditMod.Visible = false;
            unifytb_Main.Refresh(); //Refresh user control to remove software rendering leftovers.
        }

        private void Btn_ColourPicker_Default_Click(object sender, System.EventArgs e) { // Default Accent Colour to RGB: (186, 0, 0)
            Properties.Settings.Default.accentColour = Color.FromArgb(186, 0, 0);
            Properties.Settings.Default.Save();
            ChangeAccentColours();
        }

        private void Btn_Theme_Click(object sender, System.EventArgs e) {
            if (Properties.Settings.Default.theme) //Dark to Light
                Properties.Settings.Default.theme = false;
            else //Light to Dark
                Properties.Settings.Default.theme = true;

            Properties.Settings.Default.Save();
            ChangeThemeColours();
        }

        private void ChangeThemeColours() {
            //You get the idea. =P
            if (!Properties.Settings.Default.theme) {
                btn_Theme.Text = "Theme: Light";

                unifytb_Main.BackTabColor = SystemColors.Control;
                unifytb_Main.BorderColor = SystemColors.Control;
                unifytb_Main.HeaderColor = SystemColors.ControlLightLight;
                unifytb_Main.TextColor = SystemColors.ControlText;
                status_Main.BackColor = SystemColors.Control;
                BackColor = SystemColors.ControlLight;

                lbl_AccentColour.ForeColor = SystemColors.ControlText;
                lbl_CameraDistance.ForeColor = SystemColors.ControlText;
                lbl_CameraHeight.ForeColor = SystemColors.ControlText;
                lbl_FTPLocation.ForeColor = SystemColors.ControlText;
                lbl_GameDirectory.ForeColor = SystemColors.ControlText;
                lbl_ModsDirectory.ForeColor = SystemColors.ControlText;
                lbl_Password.ForeColor = SystemColors.ControlText;
                lbl_Reflections.ForeColor = SystemColors.ControlText;
                lbl_System.ForeColor = SystemColors.ControlText;
                sonic06mm_Aldi.BackColor = SystemColors.ControlLightLight;
                lbl_Username.ForeColor = SystemColors.ControlText;
                statuslbl_Status.ForeColor = SystemColors.ControlText;
                lbl_GameBanana.ForeColor = SystemColors.ControlText;
                lbl_SetStatus.ForeColor = SystemColors.ControlText; lbl_SetStatus.BackColor = SystemColors.Control;
                lbl_ManualPatches.ForeColor = SystemColors.ControlText;
                if (check_FTP.Checked)
                    lbl_ManualInstall.ForeColor = SystemColors.GrayText;
                else
                    lbl_ManualInstall.ForeColor = SystemColors.ControlText;
                if (check_ManualInstall.Checked)
                    lbl_FTP.ForeColor = SystemColors.GrayText;
                else
                    lbl_FTP.ForeColor = SystemColors.ControlText;

                lbl_EmulatorEXE.ForeColor = SystemColors.ControlText;
                lbl_Emulator_System.ForeColor = SystemColors.ControlText;
                if (combo_Emulator_System.SelectedIndex == 0) { //Depending on the selected system, change text to disabled colour.
                    lbl_API.ForeColor = SystemColors.ControlText;
                    if (combo_API.SelectedIndex == 0) {
                        lbl_ForceRTV.ForeColor = SystemColors.GrayText;
                        lbl_2xResolution.ForeColor = SystemColors.GrayText;
                    }
                    else {
                        lbl_ForceRTV.ForeColor = SystemColors.ControlText;
                        lbl_2xResolution.ForeColor = SystemColors.ControlText;
                    }
                    lbl_VSync.ForeColor = SystemColors.ControlText;
                    lbl_ProtectZero.ForeColor = SystemColors.ControlText;
                    lbl_EnableGamma.ForeColor = SystemColors.ControlText;
                    lbl_Fullscreen.ForeColor = SystemColors.ControlText;
                    lbl_Discord.ForeColor = SystemColors.ControlText;
                    lbl_Debug.ForeColor = SystemColors.ControlText;
                }
                else {
                    lbl_API.ForeColor = SystemColors.GrayText;
                    lbl_ForceRTV.ForeColor = SystemColors.GrayText;
                    lbl_2xResolution.ForeColor = SystemColors.GrayText;
                    lbl_VSync.ForeColor = SystemColors.GrayText;
                    lbl_ProtectZero.ForeColor = SystemColors.GrayText;
                    lbl_EnableGamma.ForeColor = SystemColors.GrayText;
                    lbl_Fullscreen.ForeColor = SystemColors.GrayText;
                    lbl_Discord.ForeColor = SystemColors.GrayText;
                    lbl_Debug.ForeColor = SystemColors.GrayText;
                }

                group_Directories.ForeColor = SystemColors.ControlText;
                group_FTP.ForeColor = SystemColors.ControlText;
                group_Options.ForeColor = SystemColors.ControlText;
                group_Tweaks.ForeColor = SystemColors.ControlText;
                group_Setup.ForeColor = SystemColors.ControlText;
                group_Settings.ForeColor = SystemColors.ControlText;

                check_FTP.ForeColor = SystemColors.ControlText;
                check_ManualInstall.ForeColor = SystemColors.ControlText;

                text_FTPLocation.BackColor = SystemColors.ControlLightLight; text_FTPLocation.ForeColor = SystemColors.ControlText;
                text_GameDirectory.BackColor = SystemColors.ControlLightLight; text_GameDirectory.ForeColor = SystemColors.ControlText;
                text_ModsDirectory.BackColor = SystemColors.ControlLightLight; text_ModsDirectory.ForeColor = SystemColors.ControlText;
                text_Password.BackColor = SystemColors.ControlLightLight; text_Password.ForeColor = SystemColors.ControlText;
                text_Username.BackColor = SystemColors.ControlLightLight; text_Username.ForeColor = SystemColors.ControlText;
                text_EmulatorPath.BackColor = SystemColors.ControlLightLight; text_EmulatorPath.ForeColor = SystemColors.ControlText;

                clb_ModsList.BackColor = SystemColors.ControlLightLight; clb_ModsList.ForeColor = SystemColors.ControlText;
                clb_PatchesList.BackColor = SystemColors.ControlLightLight; clb_PatchesList.ForeColor = SystemColors.ControlText;

                radio_All.ForeColor = SystemColors.ControlText; radio_All.BackColor = SystemColors.ControlLightLight;
                radio_Xbox360.ForeColor = SystemColors.ControlText; radio_Xbox360.BackColor = SystemColors.ControlLightLight;
                radio_PlayStation3.ForeColor = SystemColors.ControlText; radio_PlayStation3.BackColor = SystemColors.ControlLightLight;

                btn_CreateNewMod.Width = 245;
                btn_EditMod.Visible = false;
            }
            else {
                btn_Theme.Text = "Theme: Dark";

                unifytb_Main.BackTabColor = Color.FromArgb(28, 28, 28);
                unifytb_Main.BorderColor = Color.FromArgb(30, 30, 30);
                unifytb_Main.HeaderColor = Color.FromArgb(45, 45, 48);
                unifytb_Main.TextColor = Color.FromArgb(255, 255, 255);
                status_Main.BackColor = Color.FromArgb(28, 28, 28);
                BackColor = Color.FromArgb(45, 45, 48);

                lbl_AccentColour.ForeColor = SystemColors.Control;
                lbl_CameraDistance.ForeColor = SystemColors.Control;
                lbl_CameraHeight.ForeColor = SystemColors.Control;
                lbl_FTPLocation.ForeColor = SystemColors.Control;
                lbl_GameDirectory.ForeColor = SystemColors.Control;
                lbl_ModsDirectory.ForeColor = SystemColors.Control;
                lbl_Password.ForeColor = SystemColors.Control;
                lbl_Reflections.ForeColor = SystemColors.Control;
                lbl_System.ForeColor = SystemColors.Control;
                lbl_Username.ForeColor = SystemColors.Control;
                statuslbl_Status.ForeColor = SystemColors.Control;
                lbl_GameBanana.ForeColor = SystemColors.Control;
                sonic06mm_Aldi.BackColor = Color.FromArgb(45, 45, 48);
                lbl_SetStatus.ForeColor = SystemColors.Control; lbl_SetStatus.BackColor = Color.FromArgb(28, 28, 28);
                lbl_ManualPatches.ForeColor = SystemColors.Control;
                if (check_FTP.Checked)
                    lbl_ManualInstall.ForeColor = SystemColors.GrayText;
                else
                    lbl_ManualInstall.ForeColor = SystemColors.Control;
                if (check_ManualInstall.Checked)
                    lbl_FTP.ForeColor = SystemColors.GrayText;
                else
                    lbl_FTP.ForeColor = SystemColors.Control;

                lbl_EmulatorEXE.ForeColor = SystemColors.Control;
                lbl_Emulator_System.ForeColor = SystemColors.Control;
                if (combo_Emulator_System.SelectedIndex == 0) { //Depending on the selected system, change text to disabled colour.
                    lbl_API.ForeColor = SystemColors.Control;
                    if (combo_API.SelectedIndex == 0) {
                        lbl_ForceRTV.ForeColor = SystemColors.GrayText;
                        lbl_2xResolution.ForeColor = SystemColors.GrayText;
                    }
                    else {
                        lbl_ForceRTV.ForeColor = SystemColors.Control;
                        lbl_2xResolution.ForeColor = SystemColors.Control;
                    }
                    lbl_VSync.ForeColor = SystemColors.Control;
                    lbl_ProtectZero.ForeColor = SystemColors.Control;
                    lbl_EnableGamma.ForeColor = SystemColors.Control;
                    lbl_Fullscreen.ForeColor = SystemColors.Control;
                    lbl_Discord.ForeColor = SystemColors.Control;
                    lbl_Debug.ForeColor = SystemColors.Control;
                }
                else {
                    lbl_API.ForeColor = SystemColors.GrayText;
                    lbl_ForceRTV.ForeColor = SystemColors.GrayText;
                    lbl_2xResolution.ForeColor = SystemColors.GrayText;
                    lbl_VSync.ForeColor = SystemColors.GrayText;
                    lbl_ProtectZero.ForeColor = SystemColors.GrayText;
                    lbl_EnableGamma.ForeColor = SystemColors.GrayText;
                    lbl_Fullscreen.ForeColor = SystemColors.GrayText;
                    lbl_Discord.ForeColor = SystemColors.GrayText;
                    lbl_Debug.ForeColor = SystemColors.GrayText;
                }

                group_Directories.ForeColor = SystemColors.Control;
                group_FTP.ForeColor = SystemColors.Control;
                group_Options.ForeColor = SystemColors.Control;
                group_Tweaks.ForeColor = SystemColors.Control;
                group_Setup.ForeColor = SystemColors.Control;
                group_Settings.ForeColor = SystemColors.Control;

                check_FTP.ForeColor = SystemColors.Control;
                check_ManualInstall.ForeColor = SystemColors.Control;

                text_FTPLocation.BackColor = Color.FromArgb(45, 45, 48); text_FTPLocation.ForeColor = SystemColors.Control;
                text_GameDirectory.BackColor = Color.FromArgb(45, 45, 48); text_GameDirectory.ForeColor = SystemColors.Control;
                text_ModsDirectory.BackColor = Color.FromArgb(45, 45, 48); text_ModsDirectory.ForeColor = SystemColors.Control;
                text_Password.BackColor = Color.FromArgb(45, 45, 48); text_Password.ForeColor = SystemColors.Control;
                text_Username.BackColor = Color.FromArgb(45, 45, 48); text_Username.ForeColor = SystemColors.Control;
                text_EmulatorPath.BackColor = Color.FromArgb(45, 45, 48); text_EmulatorPath.ForeColor = SystemColors.Control;

                clb_ModsList.BackColor = Color.FromArgb(45, 45, 48); clb_ModsList.ForeColor = SystemColors.Control;
                clb_PatchesList.BackColor = Color.FromArgb(45, 45, 48); clb_PatchesList.ForeColor = SystemColors.Control;

                radio_All.ForeColor = SystemColors.Control; radio_All.BackColor = Color.FromArgb(45, 45, 48);
                radio_Xbox360.ForeColor = SystemColors.Control; radio_Xbox360.BackColor = Color.FromArgb(45, 45, 48);
                radio_PlayStation3.ForeColor = SystemColors.Control; radio_PlayStation3.BackColor = Color.FromArgb(45, 45, 48);

                btn_CreateNewMod.Width = 245;
                btn_EditMod.Visible = false;
            }

            unifytb_Main.Refresh(); //Refresh user control to remove software rendering leftovers.
        }

        //Navigate to the GitHub page in the default web browser.
        private void Btn_GitHub_Click(object sender, EventArgs e) { Process.Start("https://github.com/Knuxfan24/Sonic-06-Mod-Manager"); }

        //Show About Form, passing the Version Number in to be displayed.
        private void Btn_About_Click(object sender, EventArgs e) { new src.AboutForm(versionNumber).ShowDialog(); }

        private void Btn_Reset_Click(object sender, EventArgs e) {
            //Read the message box text if you're confused.
            string resetConfirmation = UnifyMessages.UnifyMessage.Show(SettingsMessages.msg_Reset, SystemMessages.tl_DefaultTitle, "YesNo", "Warning");

            if (resetConfirmation == "Yes") {
                Properties.Settings.Default.Reset();
                Application.Restart();
            }
        }

        private void Btn_ReportBug_Click(object sender, EventArgs e) { Process.Start("https://github.com/Knuxfan24/Sonic-06-Mod-Manager/issues"); }

        private void Lbl_ModsDirectory_Click(object sender, EventArgs e) { Process.Start(Properties.Settings.Default.modsDirectory); } // Open Mods directory shortcut

        private void Lbl_GameDirectory_Click(object sender, EventArgs e) { Process.Start(Properties.Settings.Default.gameDirectory); } // Open Game directory shortcut

        private void Check_ManualInstall_CheckedChanged(object sender, EventArgs e) {
            if (check_ManualInstall.Checked) {
                Properties.Settings.Default.manualInstall = true;
                check_FTP.Enabled = false;
                lbl_FTP.ForeColor = SystemColors.GrayText;
                btn_SaveAndPlay.Text = "Install Mods";
                btn_SaveAndPlay.Width = 120;
                btn_UninstallMods.Visible = true;
            } else {
                Properties.Settings.Default.manualInstall = false;
                check_FTP.Enabled = true;
                if (!Properties.Settings.Default.theme)
                    lbl_FTP.ForeColor = SystemColors.ControlText;
                else
                    lbl_FTP.ForeColor = SystemColors.Control;
                btn_SaveAndPlay.Text = "Save and Play";
                btn_SaveAndPlay.Width = 245;
                btn_UninstallMods.Visible = false;
            }
            Properties.Settings.Default.Save();
        }

        private void Check_FTP_CheckedChanged(object sender, EventArgs e)
        {
            if (check_FTP.Checked) {
                Properties.Settings.Default.FTP = true;
                check_ManualInstall.Enabled = false;
                lbl_ManualInstall.ForeColor = SystemColors.GrayText;
                btn_SaveAndPlay.Text = "Install Mods";
                btn_SaveAndPlay.Width = 120;
                btn_UninstallMods.Visible = true;
            } else {
                Properties.Settings.Default.FTP = false;
                check_ManualInstall.Enabled = true;
                if (!Properties.Settings.Default.theme)
                    lbl_ManualInstall.ForeColor = SystemColors.ControlText;
                else
                    lbl_ManualInstall.ForeColor = SystemColors.Control;
                btn_SaveAndPlay.Text = "Save and Play";
                btn_SaveAndPlay.Width = 245;
                btn_UninstallMods.Visible = false;
            }
            Properties.Settings.Default.Save();
        }

        private void Check_GameBanana_CheckedChanged(object sender, EventArgs e) {
            if (check_GameBanana.Checked) {
                try {
                    var Protocol = "sonic06mm";
                    var key = Registry.ClassesRoot.OpenSubKey($"{Protocol}\\shell\\open\\command");

                    if (key == null) {
                        if (!Program.RunningAsAdmin()) {
                            string registry = UnifyMessages.UnifyMessage.Show(SystemMessages.msg_GameBananaRegistry, SystemMessages.tl_DefaultTitle, "YesNo", "Warning");

                            switch (registry) {
                                case "Yes":
                                    var runAsAdmin = new ProcessStartInfo(Application.ExecutablePath, "-registry_add");
                                    runAsAdmin.Verb = "runas";
                                    if (Process.Start(runAsAdmin) != null) { Application.Exit(); }
                                    break;
                                case "No":
                                    check_GameBanana.Checked = false;
                                    break;
                            }
                        }
                        else GB_Registry.AddRegistry();
                    }
                }
                catch { }
            } else {
                try {
                    var Protocol = "sonic06mm";
                    var key = Registry.ClassesRoot.OpenSubKey(Protocol);

                    if (key != null) {
                        if (!Program.RunningAsAdmin()) {
                            string registry = UnifyMessages.UnifyMessage.Show(SystemMessages.msg_GameBananaRegistryUninstall, SystemMessages.tl_DefaultTitle, "YesNo", "Warning");

                            switch (registry) {
                                case "Yes":
                                    var runAsAdmin = new ProcessStartInfo(Application.ExecutablePath, "-registry_remove");
                                    runAsAdmin.Verb = "runas";
                                    if (Process.Start(runAsAdmin) != null) { Application.Exit(); }
                                    break;
                                case "No":
                                    check_GameBanana.Checked = true;
                                    break;
                            }
                        }
                        else GB_Registry.RemoveRegistry();
                    }
                }
                catch { }
            }
        }
        #endregion

        //Debug Button to allow us to check random bits & bobs
        //private void Debug_Click(object sender, EventArgs e)
        //{
        //    //Lua Decompilation Test
        //    //OpenFileDialog openLua = new OpenFileDialog();
        //    //openLua.Filter = "SONIC THE HEDGEHOG (2006) Lua (*.lub)|*.lub";

        //    //if (openLua.ShowDialog() == DialogResult.OK)
        //    //{
        //    //    Lua.Decompile(openLua.FileName);
        //    //    Lua.CameraDistance(openLua.FileName, 250);
        //    //}

        //    //MessageBox Text Test
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.msg_Prereq_Newtonsoft, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.msg_Prereq_Ookii, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.msg_GameBananaRegistry, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.msg_GameBananaRegistryUninstall, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.msg_NoUpdates, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.ex_UpdateFailedUnknown, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.warn_CloseProcesses, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.msg_UpdateComplete, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.ex_Prereq_Newtonsoft_WriteFailure(null), "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.ex_Prereq_Ookii_WriteFailure(null), "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SystemMessages.msg_UpdateAvailable("Version 4.20", "lol"), "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.msg_NoModDirectory, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_ModListError, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.msg_LocateARCs, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.msg_ThumbnailDeleteError, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_ModInstallFailure, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.msg_CancelDownloading, "Test", "OK", "Information");
              #region //UnifyMessages.UnifyMessage.Show(ModsMessages.msg_LoSInstalled, "Test", "OK", "Information");
        private void Aldi(object sender, EventArgs e)
        {
            Text = "Aldi Mod Manager";
            Icon = Properties.Resources.icon_aldi;
            debugMode = true;
            SystemMessages.tl_DefaultTitle = "Aldi Mod Manager";
            btn_About.Text = "About Aldi Mod Manager";
        }

        private void Debug_Click(object sender, EventArgs e)
        {

        }
              #endregion
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_GitHubTimeout, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_GameBananaTimeout, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_ExtractFailNoApp, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_GBExtractFailed("lol"), "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.msg_GBInstalled("lol"), "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_SkippedMod("lol", "filename"), "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_SkippedModsTally("these mods are ded"), "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_IncorrectTarget("lol", "69lol"), "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(ModsMessages.ex_ModExists("noob"), "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(EmulatorMessages.msg_LocateGame, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(EmulatorMessages.msg_LocateXenia, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(EmulatorMessages.msg_LocateRPCS3, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SettingsMessages.msg_LocateMods, "Test", "OK", "Information");
        //    //UnifyMessages.UnifyMessage.Show(SettingsMessages.msg_Reset, "Test", "OK", "Information");
        //}


    }
}
