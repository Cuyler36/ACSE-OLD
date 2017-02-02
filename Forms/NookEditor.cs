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
    public partial class NookEditor : Form
    {
        int Shop_Size = 1;
        int Item_Count = 0;
        Furniture[] Shop_Selection;

        public NookEditor()
        {
            InitializeComponent();
            GetShopSize();
            int itemsPerRow = Shop_Size == 1 ? 5 : Shop_Size == 2 ? 4 : Shop_Size == 3 ? 6 : 7;
            int rows = Shop_Size == 2 ? 4 : Shop_Size + 1;
            pictureBox1.Size = new Size(itemsPerRow * 16, rows * 16);
            pictureBox1.Image = Inventory.getItemPic(16, itemsPerRow, Shop_Selection);
            BindingSource bs = new BindingSource(ItemData.ItemDatabase, null);
            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

            pictureBox1.MouseMove += new MouseEventHandler(PictureBox_Hover);
            pictureBox1.MouseClick += new MouseEventHandler(PictureBox_Click);
            textBox1.Text = BitConverter.ToUInt32(DataConverter.ReadData(MainForm.Nook_Spent_Bells_Offset, 4), 0).ToString();
            label3.Text = "Shop: " + (Shop_Size == 1 ? "Nook's Cranny" : Shop_Size == 2 ? "Nook 'n Go" : Shop_Size == 3 ? "Nookway" : "Nookington's");
        }

        private void GetShopSize()
        {
            ushort[] items = DataConverter.ReadRawUShort(MainForm.Nook_Items_Offset, 0x46);
            Item_Count = items.Length;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == 0)
                {
                    Item_Count = i;
                    break;
                }
            }
            if (Item_Count > 24)
                Shop_Size = 4;
            else if (Item_Count > 16)
                Shop_Size = 3;
            else if (Item_Count > 10)
                Shop_Size = 2;
            Shop_Selection = new Furniture[Item_Count];
            for (int i = 0; i < Item_Count; i++)
                Shop_Selection[i] = new Furniture(items[i]);
        }

        private void PictureBox_Hover(object sender, MouseEventArgs e)
        {
            int width = (sender as PictureBox).Width / 16;
            int X = e.X / (16);
            int Y = e.Y / (16);
            int index = width * Y + X;

            if (index > -1 && index < Shop_Selection.Length)
            {
                Furniture item = Shop_Selection[index];
                label1.Text = string.Format("0x{0} - {1}", item.ItemID.ToString("X4"), item.Name);
            }
            else
                label1.Text = "0x0000 - Empty";
        }

        private void PictureBox_Click(object sender, MouseEventArgs e)
        {
            int width = (sender as PictureBox).Width / 16;
            int X = e.X / (16);
            int Y = e.Y / (16);
            int index = width * Y + X;

            if (e.Button == MouseButtons.Right)
                    comboBox1.SelectedValue = Shop_Selection[index].ItemID;
            else
            {
                if (comboBox1.SelectedValue != null)
                {
                    Shop_Selection[index] = new Furniture((ushort)comboBox1.SelectedValue);
                    pictureBox1.Image = Inventory.getItemPic(16, Shop_Size == 1 ? 5 : Shop_Size == 2 ? 4 : Shop_Size == 3 ? 6 : 7, Shop_Selection);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Shop_Selection.Length; i++)
                DataConverter.WriteUShort(new ushort[] { Shop_Selection[i].ItemID }, MainForm.Nook_Items_Offset + i * 2);
            if (textBox1.Text.Length > 0)
            {
                int spentBells = -1;
                int.TryParse(textBox1.Text, out spentBells);
                if (spentBells > 1)
                    DataConverter.WriteData(MainForm.Nook_Spent_Bells_Offset, BitConverter.GetBytes(spentBells));
            }
            Close();
        }
    }
}
