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
                Layers[i].Size = new Size(FirstFloorSize * 16 + 2, FirstFloorSize * 16 + 2);
                Layers[i].Location = new Point(10 + (i / 2) * FirstFloorSize * 16 - (i / 2), 60 + (i % 2) * FirstFloorSize * 16 - (i % 2) + (i % 2) * 2);
                Layers[i].BorderStyle = BorderStyle.FixedSingle;
                Layers[i].MouseClick += new MouseEventHandler(clickCustom);
                this.Controls.Add(Layers[i]);
            }
            for (int i = 2; i < 4; i++)
            {
                Layers[i] = new PictureBox();
                Layers[i].Size = new Size(6 * 16 + 2, 6 * 16 + 2);
                Layers[i].Location = new Point(Layers[0].Location.X + Layers[0].Size.Width - 1, 60 + (i % 2) * 6 * 16 - (i % 2) + (i % 2) * 2);
                Layers[i].BorderStyle = BorderStyle.FixedSingle;
                Layers[i].MouseClick += new MouseEventHandler(clickCustom);
                this.Controls.Add(Layers[i]);
            }
            for (int i = 4; i < 6; i++)
            {
                Layers[i] = new PictureBox();
                Layers[i].Size = new Size(8 * 16 + 2, 8 * 16 + 2);
                Layers[i].Location = new Point(Layers[2].Location.X + Layers[2].Size.Width - 1, 60 + (i % 2) * 8 * 16 - (i % 2) + (i % 2) * 2);
                Layers[i].BorderStyle = BorderStyle.FixedSingle;
                Layers[i].MouseClick += new MouseEventHandler(clickCustom);
                this.Controls.Add(Layers[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                Label l = new Label();
                l.AutoSize = false;
                l.Size = new Size(Layers[i * 2].Size.Width, 12);
                l.Text = i == 0 ? "First Floor" : i == 1 ? "Second Floor" : "Basement";
                l.Location = new Point(Layers[i * 2].Location.X, 42);
                l.TextAlign = ContentAlignment.MiddleCenter;
                this.Controls.Add(l);
            }

            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                    Items.Add(HouseData.GetHouseData(House_Data[i], Layers[i].Size.Width / 16));
                else
                {
                    int z = 0;
                    Items.Add(new Item[(Layers[i].Size.Width / 16) * (Layers[i].Size.Width / 16)]);
                    for (int x = 0; x < House_Data[i].Length; x++)
                        if (x % 16 < (Layers[i].Size.Width / 16) && z < Items[i].Length)
                        {
                            Items[i][z] = new Item(House_Data[i][x] == 0xFE1F ? (ushort)0xFFFF : House_Data[i][x]);
                            z++;
                        }
                }
            }
            for (int i = 0; i < 6; i++)
                Layers[i].Image = Inventory.getItemPic(16, Layers[i].Size.Width / 16, Items[i]);
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
                else if (comboBox1.SelectedValue != null) // Write
                {
                    if (idx % 2 == 1 && (ushort)comboBox1.SelectedValue != 0 && Items[idx - 1][index].ItemID == 0)
                        MessageBox.Show("Placing Furniture on top of nothing will result in it appearing on the floor. If you want this item to be on something, place an item in the above picturebox at the same location!", "Funriture Warning");
                        
                    Items[idx][index] = new Item((ushort)comboBox1.SelectedValue);
                    s.Image = Inventory.getItemPic(16, s.Size.Width / 16, Items[idx]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private int[] house_Data_Offsets = new int[6]
        {
            0, 0x24A, 0x8A8, 0xAF2, 0x1150, 0x139A
        };

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < House_Data.Count; i++)
            {
                HouseData.UpdateHouseData(Items[i], House_Data[i]);
                if (i % 2 == 0)
                    form.WriteUShort(House_Data[i], Form1.House_Addresses[0] + house_Data_Offsets[i]);
                else
                {
                    int pos = 0;
                    for (int idx = 0; idx < House_Data[i].Length; idx++)
                        if (idx % 16 < (Layers[i].Size.Width / 16))
                        {
                            House_Data[i][idx] = Items[i][pos].ItemID;
                            pos++;
                        }
                    form.WriteUShort(House_Data[i], Form1.House_Addresses[0] + house_Data_Offsets[i]);
                }
            }
            this.Hide();
        }
    }
}
