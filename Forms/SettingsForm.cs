﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACSE
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            checkBox1.Checked = Properties.Settings.Default.SecondSave;
            checkBox2.Checked = Properties.Settings.Default.StopResetti;
            checkBox3.Checked = Properties.Settings.Default.NookingtonsFlag;
            checkBox4.Checked = Properties.Settings.Default.ModifyVillagerHouse;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SecondSave = checkBox1.Checked;
            Properties.Settings.Default.StopResetti = checkBox2.Checked;
            Properties.Settings.Default.NookingtonsFlag = checkBox3.Checked;
            Properties.Settings.Default.ModifyVillagerHouse = checkBox4.Checked;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
