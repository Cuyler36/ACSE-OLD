using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACSE
{
    public partial class PatternNameForm : Form
    {
        public Pattern pattern;
        public PictureBox patternBox;
        public ToolTip tip;

        public PatternNameForm(Pattern p, PictureBox image, ToolTip t)
        {
            InitializeComponent();
            textBox1.Text = p.Name;
            pattern = p;
            patternBox = image;
            tip = t;
        }

        private void textBox1_HandleTextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            int maxBytes = StringUtil.StringToMaxChars(t.Text);
            if (t.Text.ToCharArray().Length > 16)
            {
                t.Text = t.Text.Substring(0, 16);
                t.SelectionStart = t.Text.Length;
                t.SelectionLength = 0;
            }
            if (Encoding.UTF8.GetBytes(t.Text.ToCharArray()).Length > maxBytes)
            {
                t.Text = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(t.Text), 0, maxBytes);
                t.SelectionStart = t.Text.Length;
                t.SelectionLength = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pattern.Name = textBox1.Text;
            patternBox.Name = pattern.Name;
            tip.SetToolTip(patternBox, patternBox.Name);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
