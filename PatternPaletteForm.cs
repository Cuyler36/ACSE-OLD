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
    public partial class PatternPaletteForm : Form
    {
        Pattern Pattern;
        PictureBox Box;

        public PatternPaletteForm(Pattern pattern, PictureBox p)
        {
            InitializeComponent();
            Pattern = pattern;
            Box = p;
            if (Pattern.Palette < 0x10)
                comboBox1.SelectedIndex = Pattern.Palette;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pattern.Palette = (comboBox1.SelectedIndex > -1 && comboBox1.SelectedIndex < 0x10) ? (byte)comboBox1.SelectedIndex : Pattern.Palette;
            Pattern.AdjustPalette();
            Box.Image = Pattern.Pattern_Bitmap;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
