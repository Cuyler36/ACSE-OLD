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
        Inventory Pockets;

        public Inventory_Editor(Inventory inventory)
        {
            InitializeComponent();
            Pockets = inventory;
            pictureBox1.Image = Inventory.GetItemPic(16, 5, Pockets.Items);
            comboBox1.DataSource = new BindingSource(ItemData.ItemDatabase, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            pictureBox1.MouseClick += new MouseEventHandler(clickCustom);
            pictureBox1.MouseMove += new MouseEventHandler(hover);

            //pictureBox2.MouseClick += new MouseEventHandler(clickCustom);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void hover(object sender, MouseEventArgs e)
        {
            int width = (sender as PictureBox).Width / 16;
            int X = e.X / (16);
            int Y = e.Y / (16);
            int index = width * Y + X;

            if (index > -1 && index < 15)
            {
                Item item = Pockets.Items[index];
                label1.Text = string.Format("0x{0} - {1}", item.ItemID.ToString("X4"), item.Name);
            }
            else
            {
                label1.Text = "0x0000 - Empty";
            }
        }

        //Thanks NLSE!
        private void clickCustom(object sender, MouseEventArgs e)
        {
            int width = (sender as PictureBox).Width / 16;

            int X = e.X / (16);
            int Y = e.Y / (16);
            int index = width * Y + X;

            var s = (sender as PictureBox);

            if (e.Button == MouseButtons.Right)
                comboBox1.SelectedValue = Pockets.Items[index].ItemID;
            else
            {
                if (index < Pockets.Items.Length)
                {
                    Pockets.Items[index] = new Item(string.IsNullOrEmpty(textBox1.Text) ? (ushort)comboBox1.SelectedValue : ushort.Parse(textBox1.Text, System.Globalization.NumberStyles.AllowHexSpecifier, null));
                    Pockets.InventorySlots[index].Item = Pockets.Items[index];
                    pictureBox1.Image = Inventory.GetItemPic(16, 5, Pockets.Items);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 15; i++)
                //form.WriteUShortArray(new ushort[1] { Pockets.Items[i].ItemID }, MainForm.Player1_Pockets + (i * 2));

            //Interesting note about dresser storage:
            //The limitation when storing furniture in it is created by the actual stored location of the items.
            //They're stored "ontop of" the dresser. Since they don't have 3D sprites themselves, they're invisible.
            //Using the editor to place furniture inside of the dresser causes the stacked item glitch used by many speed runners.

            //for (int i = 0; i < 3; i++)
                //form.WriteUShortArray(new ushort[1] { Dresser[i].ItemID }, MainForm.Player1_Dresser_Offsets[i]);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
