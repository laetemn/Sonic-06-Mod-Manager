﻿using Unify.Messages;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

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

namespace Sonic_06_Mod_Manager.src
{
    public partial class AboutForm : Form
    {
        public AboutForm(string versionNumber)
        {
            InitializeComponent();
            lbl_versionNumber.Text = versionNumber;

            if (ModManager.debugMode) {
                lbl_Title.Text = SystemMessages.tl_DefaultTitle;
                pic_Logo.BackgroundImage = Properties.Resources.logo_aldi;
            }

            if (Properties.Settings.Default.theme)
            {
                pic_Logo.BackColor = Color.FromArgb(45, 45, 48);
                BackColor = Color.FromArgb(28, 28, 28);

                foreach (Control x in this.Controls)
                {
                    if (x is Label)
                    {
                        ((Label)x).ForeColor = SystemColors.Control;
                    }

                    if (x is LinkLabel)
                    {
                        ((LinkLabel)x).LinkColor = SystemColors.Control;
                    }
                }
            }
        }

        private void Link_Knuxfan24_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("https://github.com/Knuxfan24"); }

        private void Link_Hyper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("https://github.com/HyperPolygon64"); }

        private void Link_SuperSonic16_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("https://github.com/thesupersonic16"); }

        private void Link_SEGACarnival_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("https://www.segacarnival.com/forum/index.php"); }

        private void Link_Nonami_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("https://www.youtube.com/channel/UC35wsF1NUwoUWmw2DLz6uJg"); }

        private void Link_sharu6262_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("https://twitter.com/sharu6262"); }

        private void Link_Melpontro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("https://www.youtube.com/user/Melpontro"); }

        private void Link_acro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("https://gamebanana.com/members/1447045"); }
    }
}