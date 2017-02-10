using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
            int acreIdIndex = 8; //Start at first true acre
            for (int i = 0; i < 30; i++)
            {
                //MessageBox.Show(acreIds[acreIdIndex].ToString("X"));
                if (AcreData.CliffAcres.ContainsKey(acreIds[acreIdIndex]))
                    acreIdIndex += 3;
                else
                    acreIdIndex++;
                ushort[] itemsBuff = new ushort[256];
                //Array.ConstrainedCopy(items, i * 256, itemsBuff, 0, 256);
                Buffer.BlockCopy(items, i * 256 * 2, itemsBuff, 0, 256 * 2);
                Acres[i] = new Normal_Acre(acreIds[i], i + 1, itemsBuff, burriedItemData);
                Acres[i].Name = Y_Acre_Names[(i / 5) + 1] + " - " + X_Acre_Names[(i % 5) + 1];
                Acres[i].AcreID = acreIds[acreIdIndex - 1];
            }
            for (int i = 1; i > -1; i--)
            {
                ushort[] itemsBuff = new ushort[256];
                //Array.ConstrainedCopy(islandItems, i * 256, itemsBuff, 0, 256);
                Buffer.BlockCopy(islandItems, i * 256 * 2, itemsBuff, 0, 256 * 2);
                Island_Acres[i] = new Normal_Acre(acreIds[60 + i], i + 1, itemsBuff, islandBurriedItemData);
                Island_Acres[i].Name = "Island - " + (i == 0 ? "Left" : "Right");
                int acreImg = AcreData.AcreImages.ContainsKey(Island_Acres[i].AcreID) ? AcreData.AcreImages[Island_Acres[i].AcreID] : 99;
                islandImages[i] = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + acreImg.ToString());
                islandAcreImages[i] = new PictureBox();
                islandAcreImages[i].Size = new Size(130, 130);
                islandAcreImages[i].Location = new Point(968 - 25 - (130 * (2 - i)), 30);
                islandAcreImages[i].BackgroundImage = islandImages[i];
                islandAcreImages[i].BackgroundImageLayout = ImageLayout.Stretch;
                islandAcreImages[i].BorderStyle = BorderStyle.FixedSingle;
                islandAcreImages[i].MouseClick += new MouseEventHandler(Island_Mouse_Click);
                islandAcreImages[i].MouseMove += new MouseEventHandler(Mouse_Move);
                this.Controls.Add(islandAcreImages[i]);
            }
            for (int i = 0; i < 30; i++)
            {
                int acreImg = AcreData.AcreImages.ContainsKey(Acres[i].AcreID) ? AcreData.AcreImages[Acres[i].AcreID] : 99;
                images[i] = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + acreImg.ToString());
                acreImages[i] = new PictureBox();
                acreImages[i].Size = new Size(130, 130);
                acreImages[i].Location = new Point(10 + (130 * (i % 5)), 30 + (130 * (i / 5)));
                acreImages[i].BackgroundImage = images[i];
                acreImages[i].BackgroundImageLayout = ImageLayout.Stretch;
                acreImages[i].BorderStyle = BorderStyle.FixedSingle;
                acreImages[i].MouseClick += new MouseEventHandler(Mouse_Click);
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

        private void Mouse_Click(object sender, MouseEventArgs e)
        {
            int selectedAcre = Array.IndexOf(acreImages, sender as PictureBox);
            int X = e.X / (4 * scale);
            int Y = e.Y / (4 * scale);
            int index = X + Y * 16;
            //MessageBox.Show(Acres[selectedAcre].Name + " | " + Acres[selectedAcre].Acre_Items.Length.ToString() + " | " + Acres[selectedAcre].Acre_Items[index].Location);
            if (e.Button == MouseButtons.Left && !string.IsNullOrEmpty(comboBox1.Text))
            {
                //Dump Check
                if (((ushort)comboBox1.SelectedValue) == 0x583B && (!(Acres[selectedAcre].AcreID == 0x0295 || Acres[selectedAcre].AcreID == 0x0119) || (selectedAcre > 4)))
                {
                    DialogResult result = MessageBox.Show("Placing a Dump in a Non-Dump, Non-A Acre can break your game.\nWould you still like to place it?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No || result == DialogResult.Cancel)
                        return;
                }
                //Set Item
                Acres[selectedAcre].Acre_Items[index] = new WorldItem((ushort)comboBox1.SelectedValue, index);
                //Set Buried
                if ((ushort)comboBox1.SelectedValue >= 0x1000 && (ushort)comboBox1.SelectedValue < 0x5000) //Avoid burying world objects
                    Acres[selectedAcre].SetBuriedInMemory(Acres[selectedAcre].Acre_Items[index], selectedAcre, buriedData, checkBox1.Checked);
                //Redraw Picturebox
                acreImages[selectedAcre].Image = GenerateAcreItemsBitmap(Acres[selectedAcre].Acre_Items, selectedAcre);
            }
            else if (e.Button == MouseButtons.Right)
            {
                comboBox1.SelectedValue = Acres[selectedAcre].Acre_Items[index].ItemID;
                comboBox1.SelectionStart = comboBox1.Text.Length;
                comboBox1.SelectionLength = 0;
                checkBox1.Checked = Acres[selectedAcre].Acre_Items[index].Burried;
                //label1.Text = "0x" + Acres[selectedAcre].Acre_Items[index].ItemID.ToString("X4");
            }
            //MessageBox.Show("Acre: " + selectedAcre.ToString() + " | X: " + X.ToString() + " Y: " + Y.ToString() + " | Index: " + index.ToString() + " | Item: " + Acres[selectedAcre].Acre_Items[index].Name + " | ItemID: " + Acres[selectedAcre].Acre_Items[index].ItemID.ToString("X"));
        }

        private void Island_Mouse_Click(object sender, MouseEventArgs e)
        {
            int selectedAcre = Array.IndexOf(islandAcreImages, sender as PictureBox);
            int X = e.X / (4 * scale);
            int Y = e.Y / (4 * scale);
            int index = X + Y * 16;
            //MessageBox.Show(Acres[selectedAcre].Name + " | " + Acres[selectedAcre].Acre_Items.Length.ToString() + " | " + Acres[selectedAcre].Acre_Items[index].Location);
            if (e.Button == MouseButtons.Left && !string.IsNullOrEmpty(comboBox1.Text))
            {
                //Dump Check
                if (((ushort)comboBox1.SelectedValue) == 0x583B && (!(Acres[selectedAcre].AcreID == 0x0295 || Acres[selectedAcre].AcreID == 0x0119) || (selectedAcre > 4)))
                {
                    DialogResult result = MessageBox.Show("Placing a Dump in a Non-Dump, Non-A Acre can break your game.\nWould you still like to place it?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No || result == DialogResult.Cancel)
                        return;
                }
                //Set Item
                Island_Acres[selectedAcre].Acre_Items[index] = new WorldItem((ushort)comboBox1.SelectedValue, index);
                //Set Buried
                if ((ushort)comboBox1.SelectedValue >= 0x1000 && (ushort)comboBox1.SelectedValue < 0x5000) //Avoid burying world objects
                    Island_Acres[selectedAcre].SetBuriedInMemory(Island_Acres[selectedAcre].Acre_Items[index], selectedAcre, islandBuriedData, checkBox1.Checked); //change to add burried data
                                                                                                                                                                   //Redraw Picturebox
                islandAcreImages[selectedAcre].Image = GenerateAcreItemsBitmap(Island_Acres[selectedAcre].Acre_Items, selectedAcre);
            }
            else if (e.Button == MouseButtons.Right)
            {
                comboBox1.SelectedValue = Island_Acres[selectedAcre].Acre_Items[index].ItemID;
                checkBox1.Checked = Island_Acres[selectedAcre].Acre_Items[index].Burried;
                comboBox1.SelectedValue = Acres[selectedAcre].Acre_Items[index].ItemID;
                comboBox1.SelectionStart = comboBox1.Text.Length;
                comboBox1.SelectionLength = 0;
                //MessageBox.Show("ItemID: " + Island_Acres[selectedAcre].Acre_Items[index].ItemID.ToString("X"));
            }
            //MessageBox.Show("Acre: " + selectedAcre.ToString() + " | X: " + X.ToString() + " Y: " + Y.ToString() + " | Index: " + index.ToString() + " | Item: " + Acres[selectedAcre].Acre_Items[index].Name + " | ItemID: " + Acres[selectedAcre].Acre_Items[index].ItemID.ToString("X"));
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            PictureBox s = (PictureBox)sender;
            bool Island_Acre = Array.IndexOf(islandAcreImages, s) > -1;
            int Selected_Acre = Array.IndexOf(Island_Acre ? islandAcreImages : acreImages, s);
            int X = e.X / (4 * scale);
            int Y = e.Y / (4 * scale);
            int Item_Index = X + Y * 16;
            WorldItem Item = Island_Acre ? Island_Acres[Selected_Acre].Acre_Items[Item_Index] : Acres[Selected_Acre].Acre_Items[Item_Index];

            label2.Text = string.Format("Acre: {0} | X: {1} Y: {2}", Island_Acre ? Island_Acres[Selected_Acre].Name : Acres[Selected_Acre].Name, X + 1, Y + 1);
            if (Last_Hovered_Item != Item)
            {
                toolTip1.Show(Item.Burried ? Item.Name + " (Buried)" : Item.Name, this, s.Left + e.X + 10, s.Top + e.Y + 40, 100000);
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
                    DataConverter.WriteUShort(new ushort[1] { Acres[i].Acre_Items[x].ItemID }, MainForm.AcreData_Offset + (i * 512) + x * 2);
            for (int i = 0; i < 2; i++)
                for (int x = 0; x < 256; x++)
                    DataConverter.WriteUShort(new ushort[1] { Island_Acres[i].Acre_Items[x].ItemID }, MainForm.IslandData_Offset + (i * 512) + x * 2);
            DataConverter.WriteDataRaw(MainForm.BurriedItems_Offset, buriedData);
            DataConverter.WriteDataRaw(MainForm.IslandBurriedItems_Offset, islandBuriedData);
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
            Update_Pictureboxes();
        }
    }
}
