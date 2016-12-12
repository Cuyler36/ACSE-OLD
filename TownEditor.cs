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

namespace Animal_Crossing_GCN_Save_Editor
{
    public partial class TownEditor : Form
    {

        const int scale = 2;
        PictureBox[] acreImages;
        private Normal_Acre[] Acres;
        Bitmap[] images = new Bitmap[30];
        byte[] buriedData;
        Form1 form;

        public string[] X_Acre_Names =
        {
            "1", "2", "3", "4", "5"
        };

        public string[] Y_Acre_Names =
        {
            "A", "B", "C", "D", "E", "F"
        };

        public string[] Island_Acre_Names =
        {
            "Left Acre", "Right Acre"
        };

        private DrawGrid drawGrid = new DrawGrid { };

        public TownEditor(ushort[] items, ushort[] islandItems, ushort[] acreIds, byte[] burriedItemData, Form1 form1)
        {
            form = form1;
            buriedData = burriedItemData;
            acreImages = new PictureBox[30];
            Acres = new Normal_Acre[30];
            Island_Acre[] Island_Acres = new Island_Acre[2];
            int acreIdIndex = 8; //Start at first true acre
            for (int i = 0; i < 30; i++)
            {
                //MessageBox.Show(acreIds[acreIdIndex].ToString("X"));
                if (AcreEditor.CliffAcres.ContainsKey(acreIds[acreIdIndex]))
                    acreIdIndex += 3;
                else
                    acreIdIndex++;
                ushort[] itemsBuff = new ushort[256];
                Array.ConstrainedCopy(items, i * 256, itemsBuff, 0, 256);
                Acres[i] = new Normal_Acre(acreIds[i], i + 1, itemsBuff, burriedItemData);
                Acres[i].Name = Y_Acre_Names[i / 5] + " - " + X_Acre_Names[i % 5];
                Acres[i].AcreID = acreIds[acreIdIndex - 1];
            }
            for (int i = 0; i < 2; i++)
            {
                ushort[] itemsBuff = new ushort[256];
                Array.ConstrainedCopy(islandItems, i * 256, itemsBuff, 0, 256);
                Island_Acres[i] = new Island_Acre(0, i + 1, itemsBuff);
            }
            for (int i = 0; i < 30; i++)
            {
                images[i] = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (AcreEditor.AcreImages[Acres[i].AcreID]).ToString());
                acreImages[i] = new PictureBox();
                acreImages[i].Size = new Size(128, 128);
                acreImages[i].Location = new Point(10 + (128 * (i % 5)), 30 + (128 * (i / 5)));
                acreImages[i].SizeMode = PictureBoxSizeMode.StretchImage;
                acreImages[i].BackgroundImage = images[i];
                acreImages[i].BackgroundImageLayout = ImageLayout.Stretch;
                acreImages[i].MouseClick += delegate (object sender, MouseEventArgs e)
                {
                    int selectedAcre = Array.IndexOf(acreImages, sender as PictureBox);
                    int X = e.X / (4 * scale);
                    int Y = e.Y / (4 * scale);
                    int index = X + Y * 16;
                    //MessageBox.Show(Acres[selectedAcre].Name + " | " + Acres[selectedAcre].Acre_Items.Length.ToString() + " | " + Acres[selectedAcre].Acre_Items[index].Location);
                    if (e.Button == MouseButtons.Left)
                    {
                        //Set Item
                        Acres[selectedAcre].Acre_Items[index] = new WorldItem((ushort)comboBox1.SelectedValue, index);
                        //Set Buried
                        if ((ushort)comboBox1.SelectedValue >= 0x1000 && (ushort)comboBox1.SelectedValue < 0x5000) //Avoid burying world objects
                            Acres[selectedAcre].SetBuriedInMemory(Acres[selectedAcre].Acre_Items[index], selectedAcre, buriedData, checkBox1.Checked);
                        //Redraw Picturebox
                        acreImages[selectedAcre].Image = GenerateAcreItemsBitmap(Acres[selectedAcre].Acre_Items, selectedAcre);
                    } else if (e.Button == MouseButtons.Right)
                    {
                        comboBox1.SelectedValue = Acres[selectedAcre].Acre_Items[index].ItemID;
                        checkBox1.Checked = Acres[selectedAcre].Acre_Items[index].Burried;
                    }
                    //MessageBox.Show("Acre: " + selectedAcre.ToString() + " | X: " + X.ToString() + " Y: " + Y.ToString() + " | Index: " + index.ToString() + " | Item: " + Acres[selectedAcre].Acre_Items[index].Name + " | ItemID: " + Acres[selectedAcre].Acre_Items[index].ItemID.ToString("X"));
                };
                //MessageBox.Show(acreImages[i].Location.ToString());
                this.Controls.Add(acreImages[i]);
            }
            BindingSource bs = new BindingSource(ItemData.ItemDatabase, null);

            InitializeComponent();
            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void TownEditor_Shown(object sender, EventArgs e)
        {
            int x = 0;
            foreach (PictureBox p in acreImages)
            {
                Bitmap acreImage = GenerateAcreItemsBitmap(Acres[x].Acre_Items, x);
                //MessageBox.Show((p == null).ToString());
                p.Image = acreImage;
                p.BringToFront();
                x++;
            }
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
                uint itemColor = ItemData.getItemColor(ItemData.getItemType(item.ItemID));
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

        private void TownEditor_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            drawGrid.MakeGrid(80, 92, e);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                for (int i = 0; i < 30; i++)
                    acreImages[i].BackgroundImage = images[i];
            else
                for (int i = 0; i < 30; i++)
                    acreImages[i].BackgroundImage = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Acres.Length; i++)
                for (int x = 0; x < 256; x++)
                    form.WriteUShort(new ushort[1] { Acres[i].Acre_Items[x].ItemID }, Form1.AcreData_Offset + (i * 512) + x * 2);
            form.WriteDataRaw(Form1.BurriedItems_Offset, buriedData);
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(((ComboBox)sender).SelectedValue.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
