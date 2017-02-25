using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSE
{
    public partial class TownEditor : Form
    {
        const int scale = 2;
        PictureBox[] acreImages;
        PictureBox[] islandAcreImages;
        private Normal_Acre[] Acres;
        private Normal_Acre[] Island_Acres;
        Bitmap[] images = new Bitmap[30];
        Bitmap[] islandImages = new Bitmap[2];
        byte[] buriedData;
        byte[] islandBuriedData;
        WorldItem Last_Hovered_Item;

        public string[] X_Acre_Names =
        {
            "0", "1", "2", "3", "4", "5", "6"
        };

        public string[] Y_Acre_Names =
        {
            "0", "A", "B", "C", "D", "E", "F", "G", "H", "I"
        };

        public string[] Island_Acre_Names =
        {
            "Left Acre", "Right Acre"
        };
        //Add messagebox to ask if they want the other half of the sign placed down
        public TownEditor(ushort[] items, ushort[] islandItems, ushort[] acreIds, byte[] burriedItemData, byte[] islandBurriedItemData)
        {
            buriedData = burriedItemData;
            islandBuriedData = islandBurriedItemData;
            acreImages = new PictureBox[30];
            islandAcreImages = new PictureBox[2];
            Acres = new Normal_Acre[30];
            Island_Acres = new Normal_Acre[2];
            for (int i = 0; i < 30; i++)
            {
                ushort[] itemsBuff = new ushort[256];
                Buffer.BlockCopy(items, i * 256 * 2, itemsBuff, 0, 256 * 2);
                Acres[i] = new Normal_Acre(acreIds[i], i + 1, itemsBuff, burriedItemData);
                Acres[i].Name = Y_Acre_Names[(i / 5) + 1] + " - " + X_Acre_Names[(i % 5) + 1];
            }
            for (int i = 0; i < 6; i++)
                for (int x = 0; x < 5; x++)
                    Acres[x + i * 5].AcreID = acreIds[8 + i * 7 + x];
            for (int i = 1; i > -1; i--)
            {
                ushort[] itemsBuff = new ushort[256];
                Buffer.BlockCopy(islandItems, i * 256 * 2, itemsBuff, 0, 256 * 2);
                Island_Acres[i] = new Normal_Acre(acreIds[60 + i], i + 1, itemsBuff, islandBurriedItemData);
                Island_Acres[i].Name = "Island - " + (i == 0 ? "Left" : "Right");
                islandImages[i] = AcreData.ToAcrePicture(Island_Acres[i].AcreID);
                islandAcreImages[i] = new PictureBox();
                islandAcreImages[i].Size = new Size(130, 130);
                islandAcreImages[i].Location = new Point(968 - 25 - (130 * (2 - i)), 30);
                islandAcreImages[i].BackgroundImage = islandImages[i];
                islandAcreImages[i].BackgroundImageLayout = ImageLayout.Stretch;
                islandAcreImages[i].BorderStyle = BorderStyle.FixedSingle;
                islandAcreImages[i].MouseDown += new MouseEventHandler(Mouse_Down);
                islandAcreImages[i].MouseMove += new MouseEventHandler(Mouse_Move);
                this.Controls.Add(islandAcreImages[i]);
            }
            for (int i = 0; i < 30; i++)
            {
                images[i] = AcreData.ToAcrePicture(Acres[i].AcreID);
                acreImages[i] = new PictureBox();
                acreImages[i].Size = new Size(130, 130);
                acreImages[i].Location = new Point(10 + (130 * (i % 5)), 30 + (130 * (i / 5)));
                acreImages[i].BackgroundImage = images[i];
                acreImages[i].BackgroundImageLayout = ImageLayout.Stretch;
                acreImages[i].BorderStyle = BorderStyle.FixedSingle;
                acreImages[i].MouseDown += new MouseEventHandler(Mouse_Down);
                acreImages[i].MouseMove += new MouseEventHandler(Mouse_Move);
                this.Controls.Add(acreImages[i]);
            }
            BindingSource bs = new BindingSource(ItemData.ItemDatabase, null);
            MouseEnter += new EventHandler(Hide_Tip);

            InitializeComponent();
            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void Hide_Tip(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
            Last_Hovered_Item = null;
        }

        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            Mouse_Click(sender, e);
        }

        private void Mouse_Click(object sender, MouseEventArgs e)
        {
            PictureBox s = (PictureBox)sender;
            s.Capture = false;
            bool Island_Hovered = Array.IndexOf(islandAcreImages, s) > -1;
            int selectedAcreIdx = Island_Hovered ? Array.IndexOf(islandAcreImages, sender as PictureBox) : Array.IndexOf(acreImages, s);
            Normal_Acre Selected_Acre = Island_Hovered ? Island_Acres[selectedAcreIdx] : Acres[selectedAcreIdx];
            int X = e.X / (4 * scale);
            int Y = e.Y / (4 * scale);
            int index = X + Y * 16;
            ushort Item_ID = 0;

            if (!string.IsNullOrEmpty(textBox1.Text))
                ushort.TryParse(textBox1.Text, NumberStyles.AllowHexSpecifier, null, out Item_ID);
            else if (comboBox1.SelectedValue != null)
                Item_ID = (ushort)comboBox1.SelectedValue;

            if (index < 0 || index > 255 || Item_ID == Selected_Acre.Acre_Items[index].ItemID)
                return;
            if (e.Button == MouseButtons.Left)
            {
                //Dump Check
                ushort Base_Acre_ID = (ushort)(Selected_Acre.AcreID - Selected_Acre.AcreID % 4);
                if (Item_ID == 0x583B && (!(Base_Acre_ID == 0x0294 || Base_Acre_ID == 0x0118 || Base_Acre_ID == 0x0298) || (selectedAcreIdx > 4 && !Island_Hovered)))
                {
                    DialogResult result = MessageBox.Show("Placing a Dump in a Non-Dump, Non-A Acre can break your game.\nWould you still like to place it?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No || result == DialogResult.Cancel)
                        return;
                }
                if (Item_ID == 0x583B)
                    ItemData.Place_Structure("Dump", index, ref Selected_Acre.Acre_Items);
                else if (Item_ID == 0x584A)
                    ItemData.Place_Structure("Museum", index, ref Selected_Acre.Acre_Items);
                else if (Item_ID == 0x584D)
                    ItemData.Place_Structure("Tailor's Shop", index, ref Selected_Acre.Acre_Items);
                else if (Item_ID == 0x5825)
                    ItemData.Place_Structure("Wishing Well", index, ref Selected_Acre.Acre_Items);
                else if (Item_ID == 0x580C)
                    ItemData.Place_Structure("Police Station", index, ref Selected_Acre.Acre_Items);
                else if (Item_ID == 0x5808)
                    ItemData.Place_Structure("Post Office", index, ref Selected_Acre.Acre_Items);
                else if (Item_ID == 0x5844)
                    ItemData.Place_Structure("Lighthouse", index, ref Selected_Acre.Acre_Items);
                //Set Item
                Selected_Acre.Acre_Items[index] = new WorldItem(Item_ID, index);
                //Set Buried
                if (Item_ID >= 0x1000 && Item_ID < 0x5000) //Avoid burying world objects
                    Selected_Acre.SetBuriedInMemory(Selected_Acre.Acre_Items[index], selectedAcreIdx, Island_Hovered ?  islandBuriedData : buriedData, checkBox1.Checked);
                //Redraw Picturebox
                if (!Island_Hovered)
                    acreImages[selectedAcreIdx].Image = GenerateAcreItemsBitmap(Selected_Acre.Acre_Items, selectedAcreIdx);
                else
                    islandAcreImages[selectedAcreIdx].Image = GenerateAcreItemsBitmap(Selected_Acre.Acre_Items, selectedAcreIdx);
            }
            else if (e.Button == MouseButtons.Right)
            {
                comboBox1.SelectedValue = Selected_Acre.Acre_Items[index].ItemID;
                comboBox1.SelectionStart = comboBox1.Text.Length;
                comboBox1.SelectionLength = 0;
                checkBox1.Checked = Selected_Acre.Acre_Items[index].Burried;
            }
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            PictureBox s = (PictureBox)sender;
            s.Capture = false;
            bool Island_Acre = Array.IndexOf(islandAcreImages, s) > -1;
            int Selected_Acre = Array.IndexOf(Island_Acre ? islandAcreImages : acreImages, s);
            int X = e.X / (4 * scale);
            int Y = e.Y / (4 * scale);
            int Item_Index = X + Y * 16;
            //Merge with Mouse_Click to prevent duplicate code
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
                Mouse_Click(sender, e);
            if (Item_Index < 0 || Item_Index > 255)
                return;
            WorldItem Item = Island_Acre ? Island_Acres[Selected_Acre].Acre_Items[Item_Index] : Acres[Selected_Acre].Acre_Items[Item_Index];

            label2.Text = string.Format("Acre: {0} | X: {1} Y: {2}", Island_Acre ? Island_Acres[Selected_Acre].Name : Acres[Selected_Acre].Name, X + 1, Y + 1);
            if (Last_Hovered_Item != Item)
            {
                if (!string.IsNullOrEmpty(Item.Name))
                    toolTip1.Show(Item.Burried ? Item.Name + " (Buried)" + string.Format(" - 0x{0}", Item.ItemID.ToString("X4"))
                        : Item.Name + string.Format(" - 0x{0}", Item.ItemID.ToString("X4")), this, s.Left + e.X + 15, s.Top + e.Y + 45, 100000);
                else
                    toolTip1.Show(Item.Burried ? Item.ItemID.ToString("X4") + " (Buried)" : Item.ItemID.ToString("X4"), this, s.Left + e.X + 15, s.Top + e.Y + 45, 100000);
                Last_Hovered_Item = Item;
            }
        }

        private void Update_Pictureboxes()
        {
            int x = 0;
            foreach (PictureBox p in acreImages)
            {
                if (p.Image != null)
                {
                    Bitmap old = (Bitmap)p.Image;
                    p.Image = null;
                    old.Dispose();
                    old = null;
                }
                Bitmap acreImage = GenerateAcreItemsBitmap(Acres[x].Acre_Items, x);
                p.Image = acreImage;
                x++;
            }
            for (int i = 0; i < 2; i++)
            {
                if (islandAcreImages[i].Image != null)
                {
                    Bitmap old = (Bitmap)islandAcreImages[i].Image;
                    islandAcreImages[i].Image = null;
                    old.Dispose();
                    old = null;
                }
                islandAcreImages[i].Image = GenerateAcreItemsBitmap(Island_Acres[i].Acre_Items, i);
            }
        }

        private void TownEditor_Shown(object sender, EventArgs e)
        {
            Update_Pictureboxes();
        }

        //Code from NLSE. Thanks!
        private Bitmap GenerateAcreItemsBitmap(WorldItem[] items, int acre)
        {
            const int itemSize = 4 * scale;
            const int acreSize = 64 * scale;
            byte[] bitmapBuffer = new byte[4 * (acreSize * acreSize)];
            for(int i = 0; i < 256; i++)
            {
                WorldItem item = items[i];
                uint itemColor = ItemData.GetItemColor(ItemData.GetItemType(item.ItemID));
                for (int x = 0; x < itemSize * itemSize; x++)
                {
                    Buffer.BlockCopy(BitConverter.GetBytes(itemColor), 0, bitmapBuffer, ((item.Location.Y * itemSize + x / itemSize) * acreSize * 4) + ((item.Location.X * itemSize + x % itemSize) * 4), 4);
                }
                // Buried
                if (item.Burried)
                {
                    for (int z = 2; z < itemSize - 1; z++)
                    {
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bitmapBuffer,
                            ((item.Location.Y * itemSize + z) * acreSize * 4) + ((item.Location.X * itemSize + z) * 4), 4);
                        Buffer.BlockCopy(BitConverter.GetBytes(0xFF000000), 0, bitmapBuffer,
                            ((item.Location.Y * itemSize + z) * acreSize * 4) + ((item.Location.X * itemSize + itemSize - z) * 4), 4);
                    }
                }
            }
            for (int i = 0; i < acreSize * acreSize; i++)
                if (i % itemSize == 0 || (i / (16 * itemSize)) % itemSize == 0)
                    Buffer.BlockCopy(BitConverter.GetBytes(0x41444444), 0, bitmapBuffer, ((i / (16 * itemSize)) * acreSize * 4) + ((i % (16 * itemSize)) * 4), 4);
            Bitmap acreBitmap = new Bitmap(acreSize, acreSize, PixelFormat.Format32bppArgb);
            BitmapData bitmapData = acreBitmap.LockBits(new Rectangle(0, 0, acreSize, acreSize), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(bitmapBuffer, 0, bitmapData.Scan0, bitmapBuffer.Length);
            acreBitmap.UnlockBits(bitmapData);
            return acreBitmap;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 30; i++)
                acreImages[i].BackgroundImage = checkBox2.Checked ? images[i] : null;
            for (int i = 0; i < 2; i++)
                islandAcreImages[i].BackgroundImage = checkBox2.Checked ? islandImages[i] : null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Acres.Length; i++)
                for (int x = 0; x < 256; x++)
                    DataConverter.Write(MainForm.AcreData_Offset + (i * 512) + x * 2, Acres[i].Acre_Items[x].ItemID);
            for (int i = 0; i < 2; i++)
                for (int x = 0; x < 256; x++)
                    DataConverter.Write(MainForm.IslandData_Offset + (i * 512) + x * 2, Island_Acres[i].Acre_Items[x].ItemID);
            DataConverter.WriteByteArray(MainForm.BurriedItems_Offset, buriedData, false);
            DataConverter.WriteByteArray(MainForm.IslandBurriedItems_Offset, islandBuriedData, false);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int Cleared_Weeds = 0;
            for (int i = 0; i < 30; i++)
                foreach (WorldItem item in Acres[i].Acre_Items.Where(o => o.Name == "Weed"))
                {
                    item.ItemID = 0;
                    item.Name = "Empty";
                    Cleared_Weeds++;
                }
            Update_Pictureboxes();
            MessageBox.Show("Weeds Cleared: " + Cleared_Weeds.ToString(), "Weeds Cleared");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 30; i++)
                foreach (WorldItem item in Acres[i].Acre_Items.Where(o => o.ItemID < 0x5000))
                {
                    item.ItemID = 0;
                    item.Name = "Empty";
                    Acres[i].SetBuriedInMemory(item, i, buriedData, false);
                }

            for (int i = 0; i < 2; i++)
                foreach (WorldItem item in Island_Acres[i].Acre_Items.Where(o => o.ItemID < 0x5000))
                {
                    item.ItemID = 0;
                    item.Name = "Empty";
                    Island_Acres[i].SetBuriedInMemory(item, i, islandBuriedData, false);
                }
            Update_Pictureboxes();
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            TextBox s = (TextBox)sender;
            ushort ItemID = 0;
            s.Text.Replace("0x", "");
            ushort.TryParse(s.Text, NumberStyles.AllowHexSpecifier, null, out ItemID);
            if (ItemID < 0x6000)
                comboBox1.SelectedValue = ItemID;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox1_LostFocus(sender, null);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                textBox2.Text.Replace("0x", "");
                textBox3.Text.Replace("0x", "");
                ushort Replaced_Item_ID = 0;
                ushort Replacing_Item_ID = 0;
                ushort.TryParse(textBox2.Text, NumberStyles.AllowHexSpecifier, null, out Replaced_Item_ID);
                ushort.TryParse(textBox3.Text, NumberStyles.AllowHexSpecifier, null, out Replacing_Item_ID);
                if (Replaced_Item_ID == Replacing_Item_ID)
                    return;
                string Item_Name = ItemData.GetItemName(Replacing_Item_ID);

                int Number_Replaced = 0;
                for (int i = 0; i < 30; i++)
                    foreach (WorldItem item in Acres[i].Acre_Items.Where(o => o.ItemID == Replaced_Item_ID))
                    {
                        item.ItemID = Replacing_Item_ID;
                        item.Name = Item_Name;
                        Acres[i].SetBuriedInMemory(item, i, buriedData, checkBox1.Checked);
                        Number_Replaced++;
                    }

                for (int i = 0; i < 2; i++)
                    foreach (WorldItem item in Island_Acres[i].Acre_Items.Where(o => o.ItemID == Replaced_Item_ID))
                    {
                        item.ItemID = Replacing_Item_ID;
                        item.Name = Item_Name;
                        Island_Acres[i].SetBuriedInMemory(item, i, islandBuriedData, checkBox1.Checked);
                        Number_Replaced++;
                    }
                Update_Pictureboxes();
                MessageBox.Show(string.Format("{0} occurrences replaced!", Number_Replaced));
            }
        }
    }
}
