using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSE
{
    public partial class Inventory_Editor : Form
    {
        ushort[] InventoryData;
        Form1 form;
        Inventory Pockets;
        Item[] Dresser;

        public Inventory_Editor(Inventory inventory, Form1 form1)
        {
            InitializeComponent();
            //InventoryData = inventoryData;
            form = form1;
            //Dresser = dresserItems;
            Pockets = inventory;
            pictureBox1.Image = Inventory.getItemPic(16, 5, Pockets.Items);
           // pictureBox2.Image = Inventory.getItemPic(16, 3, dresserItems);
            BindingSource bs = new BindingSource(ItemData.ItemDatabase, null);
            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            pictureBox1.MouseClick += new MouseEventHandler(clickCustom);
            pictureBox2.MouseClick += new MouseEventHandler(clickCustom);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
            bool pocket = s == pictureBox1;
            bool dresser = false;//s == pictureBox2;

            if (e.Button == MouseButtons.Right) // Read
            {
                if (pocket)
                    comboBox1.SelectedValue = Pockets.Items[index].ItemID;
                else if (dresser)
                    comboBox1.SelectedValue = Dresser[index].ItemID;
            }
            else // Write
            {
                if (pocket && comboBox1.SelectedValue != null)
                {
                    Pockets.Items[index] = new Item((ushort)comboBox1.SelectedValue);
                    Pockets.InventorySlots[index].Item = Pockets.Items[index];
                    pictureBox1.Image = Inventory.getItemPic(16, 5, Pockets.Items);
                }
                else if (dresser)
                {
                    Dresser[index] = new Item((ushort)comboBox1.SelectedValue);
                    pictureBox2.Image = Inventory.getItemPic(16, 3, Dresser);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 15; i++)
                //form.WriteUShort(new ushort[1] { Pockets.Items[i].ItemID }, Form1.Player1_Pockets + (i * 2));

            //Interesting note about dresser storage:
            //The limitation when storing furniture in it is created by the actual stored location of the items.
            //They're stored "ontop of" the dresser. Since they don't have 3D sprites themselves, they're invisible.
            //Using the editor to place furniture inside of the dresser causes the stacked item glitch used by many speed runners.

            //for (int i = 0; i < 3; i++)
                //form.WriteUShort(new ushort[1] { Dresser[i].ItemID }, Form1.Player1_Dresser_Offsets[i]);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
