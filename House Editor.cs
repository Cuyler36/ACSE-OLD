using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Animal_Crossing_GCN_Save_Editor
{
    public partial class House_Editor : Form
    {
        PictureBox[] Layers = new PictureBox[6];
        Form1 form;
        int FirstFloorSize = 4;
        List<ushort[]> House_Data;
        List<Item[]> Items = new List<Item[]>();

        public House_Editor(List<ushort[]> houseData, Form1 form1)
        {
            InitializeComponent();
            House_Data = houseData;
            FirstFloorSize = HouseData.GetHouseSize(houseData[0]);
            form = form1;
            BindingSource bs = new BindingSource(ItemData.ItemDatabase, null);
            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            for (int i = 0; i < 2; i++)
            {
                Layers[i] = new PictureBox();
                Layers[i].Size = new Size(FirstFloorSize * 16, FirstFloorSize * 16);
                Layers[i].Location = new Point(10 + (i / 2) * FirstFloorSize * 16 - (i / 2), 40 + (i % 2) * FirstFloorSize * 16 - (i % 2));
                Layers[i].BorderStyle = BorderStyle.FixedSingle;
                Layers[i].MouseClick += new MouseEventHandler(clickCustom);
                this.Controls.Add(Layers[i]);
            }
            for (int i = 2; i < 4; i++)
            {
                Layers[i] = new PictureBox();
                Layers[i].Size = new Size(6 * 16, 6 * 16);
                Layers[i].Location = new Point(Layers[0].Location.X + Layers[0].Size.Width - 1, 40 + (i % 2) * 6 * 16 - (i % 2));
                Layers[i].BorderStyle = BorderStyle.FixedSingle;
                Layers[i].MouseClick += new MouseEventHandler(clickCustom);
                this.Controls.Add(Layers[i]);
            }
            for (int i = 4; i < 6; i++)
            {
                Layers[i] = new PictureBox();
                Layers[i].Size = new Size(8 * 16, 8 * 16);
                Layers[i].Location = new Point(Layers[2].Location.X +  Layers[2].Size.Width - 1, 40 + (i % 2) * 8 * 16 - (i % 2));
                Layers[i].BorderStyle = BorderStyle.FixedSingle;
                Layers[i].MouseClick += new MouseEventHandler(clickCustom);
                this.Controls.Add(Layers[i]);
            }
            for (int i = 0; i < 6; i++)
            {
                Items.Add(HouseData.GetHouseData(House_Data[i], Layers[i].Size.Width / 16));
            }
            for (int i = 0; i < 6; i++)
                Layers[i].Image = Inventory.getItemPic(16, Layers[i].Size.Width / 16, Items[i]);
        }

        private void House_Editor_Shown(object sender, EventArgs e)
        {
            
        }

        //Thanks NLSE!
        private void clickCustom(object sender, MouseEventArgs e)
        {
            int width = (sender as PictureBox).Width / 16; // 16pixels per item

            int X = e.X / (16);
            int Y = e.Y / (16);

            // Get Base Acre
            int index = width * Y + X;
            //MessageBox.Show("Index: " + index.ToString());

            var s = (sender as PictureBox);
            int idx = Array.IndexOf(Layers, s);
            if (Items.Count > idx && Items[idx].Length > index)
            {
                if (e.Button == MouseButtons.Right) // Read
                    comboBox1.SelectedValue = Items[idx][index].ItemID;
                else // Write
                {
                    Items[idx][index] = new Item((ushort)comboBox1.SelectedValue);
                    s.Image = Inventory.getItemPic(16, s.Size.Width / 16, Items[idx]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
