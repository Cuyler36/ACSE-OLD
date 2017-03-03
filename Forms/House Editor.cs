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
    public partial class House_Editor : Form
    {
        PictureBox[] Layers = new PictureBox[12];
        int FirstFloorSize = 4;
        Furniture[][] Items;
        House House;

        public House_Editor(House Player_House, Furniture[][] Furniture)
        {
            InitializeComponent();
            House = Player_House;
            FirstFloorSize = Player_House.Rooms[0].Room_Size;
            Items = Furniture;
            comboBox1.DataSource = new BindingSource(ItemData.ItemDatabase, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            //Nest this as one loop later
            for (int i = 0; i < 4; i++)
            {
                Layers[i] = new PictureBox();
                Layers[i].Size = new Size(FirstFloorSize * 16 + 2, FirstFloorSize * 16 + 2);
                Layers[i].Location = new Point(10, 60 + (i % 4) * FirstFloorSize * 16 - (i % 4) + (i % 4) * 2);
                Layers[i].BorderStyle = BorderStyle.FixedSingle;
                Layers[i].MouseClick += new MouseEventHandler(clickCustom);
                this.Controls.Add(Layers[i]);
            }
            for (int i = 4; i < 8; i++)
            {
                Layers[i] = new PictureBox();
                Layers[i].Size = new Size(6 * 16 + 2, 6 * 16 + 2);
                Layers[i].Location = new Point(Layers[0].Location.X + Layers[0].Size.Width - 1, 60 + (i % 4) * 6 * 16 - (i % 4) + (i % 4) * 2);
                Layers[i].BorderStyle = BorderStyle.FixedSingle;
                Layers[i].MouseClick += new MouseEventHandler(clickCustom);
                this.Controls.Add(Layers[i]);
            }
            for (int i = 8; i < 12; i++)
            {
                Layers[i] = new PictureBox();
                Layers[i].Size = new Size(8 * 16 + 2, 8 * 16 + 2);
                Layers[i].Location = new Point(Layers[4].Location.X + Layers[4].Size.Width - 1, 60 + (i % 4) * 8 * 16 - (i % 4) + (i % 4) * 2);
                Layers[i].BorderStyle = BorderStyle.FixedSingle;
                Layers[i].MouseClick += new MouseEventHandler(clickCustom);
                this.Controls.Add(Layers[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                Label l = new Label();
                l.AutoSize = false;
                l.Size = new Size(Layers[i * 4].Size.Width, 12);
                l.Text = i == 0 ? "First Floor" : i == 1 ? "Second Floor" : "Basement";
                l.Location = new Point(Layers[i * 4].Location.X, 42);
                l.TextAlign = ContentAlignment.MiddleCenter;
                this.Controls.Add(l);
            }

            for (int i = 0; i < 12; i++)
                Layers[i].Image = Inventory.GetItemPic(16, Layers[i].Size.Width / 16, Items[i]);
        }

        //Thanks NLSE!
        private void clickCustom(object sender, MouseEventArgs e)
        {
            int width = (sender as PictureBox).Width / 16; // 16pixels per item

            int X = e.X / 16;
            int Y = e.Y / 16;
            int index = width * Y + X;

            PictureBox s = sender as PictureBox;
            int idx = Array.IndexOf(Layers, s);
            if (Items.Length > idx && Items[idx].Length > index)
            {
                if (e.Button == MouseButtons.Right) // Read
                {
                    comboBox1.SelectedValue = Items[idx][index].IsFurniture ? Items[idx][index].BaseItemID : Items[idx][index].ItemID;
                    label1.Text = "0x" + (Items[idx][index].IsFurniture ? Items[idx][index].BaseItemID : Items[idx][index].ItemID).ToString("X4");
                }
                else if (comboBox1.SelectedValue != null) // Write
                {
                    if (idx % 4 > 0 && (ushort)comboBox1.SelectedValue != 0 && Items[idx - 1][index].ItemID == 0)
                        MessageBox.Show("Placing Furniture on top of nothing will result in it appearing on the floor. If you want this item to be on something," +
                            " place an item in the above layer at the same location!", "Funriture Warning");

                    Items[idx][index] = new Furniture((ushort)comboBox1.SelectedValue);
                    s.Image = Inventory.GetItemPic(16, s.Size.Width / 16, Items[idx]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int x = 0; x < 4; x++)
                    DataConverter.Write(House.Offset + i * 0x8A8 + x * 0x228, House.Rooms[i].Layers[x].Get_Data(Items[i * 4 + x]));
            Close();
        }
    }
}
