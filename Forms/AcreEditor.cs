using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ACSE
{
    public partial class AcreEditor : Form
    {
        //TODO: Create "Generate Random Town" method
        private ushort currentlySelectedAcre = 0;
        private bool acreSelected = false;
        private PictureBox[] acreImages = new PictureBox[70];
        private Dictionary<int, Acre> currentAcreData;
        private ImageList imageList;
        private TreeView treeView1;
        private int[] treeViewIconIndex;
        private List<PictureBox> Villager_Houses = new List<PictureBox>();
        private CancelEventHandler Selected_Handler;

        void acreImage_Click(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (e.Button == MouseButtons.Right)
            {
                acreSelected = true;
                ushort id = ushort.Parse(p.Name);
                bool acreExists = AcreData.Acres.ContainsKey(id);
                pictureBox1.Image = AcreData.ToAcrePicture(id);
                label2.Text = acreExists ? AcreData.Acres[id] : "Unknown Acre";
                currentlySelectedAcre = id;
                label3.Text = "0x" + id.ToString("X4");
                //treeView1.UpdateLayout();
                foreach (TreeNode container in treeView1.Nodes)
                {
                    TreeNode[] Matches = container.Nodes.Find(id.ToString(), true);
                    if (Matches.Length > 0)
                    {
                        container.Toggle();
                        treeView1.SelectedNode = Matches[0];
                        treeView1.Focus();
                        break;
                    }
                }
            }
            else if (e.Button == MouseButtons.Left && acreSelected)
            {
                int idx = Array.IndexOf(acreImages, p);
                if (idx > -1)
                {
                    p.Image = AcreData.ToAcrePicture(currentlySelectedAcre);
                    p.Name = currentlySelectedAcre.ToString();
                    currentAcreData[idx + 1] = new Acre(currentlySelectedAcre, currentAcreData[idx + 1].Index);
                }
            }
        }

        public AcreEditor(Dictionary<int, Acre> acreData)
        {
            currentAcreData = acreData;
            InitializeComponent();
            imageList = new ImageList();
            imageList.ImageSize = new Size(24, 24);
            List<int> iconIndex = new List<int>();
            for (int i = 1; i < 68; i++)
            {
                imageList.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("_" + i));
                iconIndex.Add(i);
            }
            for (int i = 70; i < 95; i++)
            {
                imageList.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("_" + i));
                iconIndex.Add(i);
            }
            for (int i = 99; i < 101; i++)
            {
                imageList.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("_" + i));
                iconIndex.Add(i);
            }
            foreach (ushort key in AcreData.Unique_Acre_Images.Keys)
            {
                imageList.Images.Add(AcreData.Unique_Acre_Images[key]);
                iconIndex.Add(key);
            }
            treeViewIconIndex = iconIndex.ToArray();
            iconIndex.Clear();
            populate_TreeView();
        }

        public void Populate_Villager_Houses()
        {   
            foreach (Villager Villager in MainForm.Villagers)
                if (Villager.Exists)
                {
                    int X_Acre = Villager.House_Coords[0];
                    int Y_Acre = Villager.House_Coords[1];
                    int X_Position = Villager.House_Coords[2];
                    int Y_Position = Villager.House_Coords[3];
                    int Acre_Index = 7 * Y_Acre + X_Acre;
                    if (X_Acre >= 1 && X_Acre <= 5 && Y_Acre >= 1 && Y_Acre <= 6 && X_Position > 0 && X_Position < 16 && Y_Position > 0 && Y_Position < 15)
                    {
                        Acre House_Acre = currentAcreData.ElementAt(Acre_Index).Value;
                        int Z_Position = House_Acre.Name.Contains("Upper") ? 0 : House_Acre.Name.Contains("Middle") ? 1 : 2;
                        //Add Z-Position logic
                        /*ColorPalette Bitmap_Palette = House_Image.Palette;
                        for (int i = 0; i < Bitmap_Palette.Entries.Length; i++)
                        {
                            if (Bitmap_Palette.Entries[i] == Color.White)
                                Bitmap_Palette.Entries[i] = Color.FromArgb(90, 90, 225);
                            else if (Bitmap_Palette.Entries[i] == Color.Black)
                                Bitmap_Palette.Entries[i] = Color.White;
                        }
                        House_Image.Palette = Bitmap_Palette;*/
                        PictureBox Acre_Picturebox = acreImages[Acre_Index];
                        PictureBox House_Picturebox = new PictureBox();
                        House_Picturebox.Size = new Size(16, 16);
                        House_Picturebox.Location = new Point(Math.Min(X_Position * 4 - 8, 56), Math.Min(Y_Position * 4 - 8, 56));
                        House_Picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
                        House_Picturebox.BackColor = Color.Transparent;
                        House_Picturebox.Image = (Image)Properties.Resources.ResourceManager.GetObject("_VillagerHouse");
                        Acre_Picturebox.Controls.Add(House_Picturebox);
                        House_Picturebox.BringToFront();
                        Villager_Houses.Add(House_Picturebox);
                    }
                }
        }

        private void setSelectedAcre(KeyValuePair<ushort, string> acre)
        {
            if (AcreData.Acres.Contains(acre))
            {
                acreSelected = true;
                ushort acreId = acre.Key;
                pictureBox1.Image = AcreData.ToAcrePicture(acreId);
                currentlySelectedAcre = acreId;
                label2.Text = acre.Value;
                label3.Text = "0x" + acreId.ToString("X4");
            }
        }

        private void treeNode_Selected(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;

            if (node != null && treeView1.Nodes.IndexOf(node) == -1)
            {
                setSelectedAcre((KeyValuePair<ushort, string>)node.Tag);
            }
        }

        private void Import_Acre_Map(object sender, CancelEventArgs e)
        {
            FileStream Map = new FileStream(((OpenFileDialog)sender).FileName, FileMode.Open);
            BinaryReader Map_Reader = new BinaryReader(Map);
            ushort[] Map_Buffer = new ushort[70];
            for (int i = 0; i < 0x8C; i += 2)
            {
                byte[] Acre_Bytes = Map_Reader.ReadBytes(2);
                Array.Reverse(Acre_Bytes);
                Map_Buffer[i / 2] = BitConverter.ToUInt16(Acre_Bytes, 0);
            }
            for (int x = 0; x < 70; x++)
            {
                acreImages[x].Image = AcreData.ToAcrePicture(Map_Buffer[x]);
                acreImages[x].Name = Map_Buffer[x].ToString();
                currentAcreData[x + 1] = new Acre(Map_Buffer[x], currentAcreData[x + 1].Index);
            }
            Map_Reader.Close();
            Map.Close();
            (sender as OpenFileDialog).FileOk -= Selected_Handler;
            (sender as OpenFileDialog).Dispose();
        }

        private void Import_Select_File()
        {
            OpenFileDialog Map_Selector = new OpenFileDialog() {
                InitialDirectory = Path.GetDirectoryName(MainForm.fileName),
                DefaultExt = ".acmap",
                Filter = "Animal Crossing Map (*.acmap) | *.acmap"
            };

            Selected_Handler = new CancelEventHandler(Import_Acre_Map);
            Map_Selector.FileOk += Selected_Handler;
            Map_Selector.ShowDialog();
        }

        private void Export_Acre_Map()
        {
            ushort[] Acre_Data_Buffer = new ushort[70];
            int i = 0;
            foreach (KeyValuePair<int, Acre> Acre in currentAcreData)
            {
                Acre_Data_Buffer[i] = Acre.Value.AcreID;
                i++;
            }

            FileStream Map_File = new FileStream(Path.GetDirectoryName(MainForm.fileName) + "\\" + Path.GetFileNameWithoutExtension(MainForm.fileName) + ".acmap", FileMode.OpenOrCreate);
            if (Map_File.CanWrite)
            {
                BinaryWriter Map_Writer = new BinaryWriter(Map_File);
                foreach (ushort Acre in Acre_Data_Buffer)
                    Map_Writer.Write(new byte[] { BitConverter.GetBytes(Acre)[1], BitConverter.GetBytes(Acre)[0] });
                Map_File.Flush();
                Map_Writer.Close();
            }
            Map_File.Close();
        }

        private void populate_TreeView()
        {
            treeView1 = new TreeView();
            treeView1.ItemHeight = 26;
            treeView1.Size = new Size(261, 329);
            treeView1.Location = new Point(Size.Width - 281, 43);
            this.Controls.Add(treeView1);

            treeView1.BeginUpdate();
            treeView1.ImageList = imageList;
            treeView1.Nodes.Clear();

            TreeNode borderAcres = new TreeNode("Border Acres");
            borderAcres.ImageIndex = 68;
            borderAcres.SelectedImageIndex = 68;
            TreeNode grassAcres = new TreeNode("Grass Acres");
            grassAcres.ImageIndex = 9;
            grassAcres.SelectedImageIndex = 9;
            TreeNode cliffAcres = new TreeNode("Cliff/Ramp Acres");
            cliffAcres.ImageIndex = 57;
            cliffAcres.SelectedImageIndex = 57;
            TreeNode riverAcres = new TreeNode("River/Lake Acres");
            riverAcres.ImageIndex = 10;
            riverAcres.SelectedImageIndex = 10;
            TreeNode specialAcres = new TreeNode("Special Acres");
            specialAcres.ImageIndex = 8;
            specialAcres.SelectedImageIndex = 8;
            TreeNode beachAcres = new TreeNode("Beachfront Acres");
            beachAcres.ImageIndex = 62;
            beachAcres.SelectedImageIndex = 62;
            TreeNode oceanAcres = new TreeNode("Ocean Acres");
            oceanAcres.ImageIndex = 67;
            oceanAcres.SelectedImageIndex = 67;
            TreeNode islandAcres = new TreeNode("Island Acres");
            islandAcres.ImageIndex = 80;
            islandAcres.SelectedImageIndex = 80;
            TreeNode miscAcres = new TreeNode("Miscellaneous");
            miscAcres.ImageIndex = 93;
            miscAcres.SelectedImageIndex = 93;

            treeView1.Nodes.Add(borderAcres);
            treeView1.Nodes.Add(grassAcres);
            treeView1.Nodes.Add(cliffAcres);
            treeView1.Nodes.Add(riverAcres);
            treeView1.Nodes.Add(specialAcres);
            treeView1.Nodes.Add(beachAcres);
            treeView1.Nodes.Add(oceanAcres);
            treeView1.Nodes.Add(islandAcres);
            treeView1.Nodes.Add(miscAcres);

            foreach (KeyValuePair<ushort, string> acre in AcreData.Acres)
            {
                TreeNode acreNode = new TreeNode(acre.Value);
                int idx = Array.IndexOf(treeViewIconIndex, AcreData.ToAcrePictureID(acre.Key));
                acreNode.ImageIndex = idx > -1 ? idx : 0;
                acreNode.SelectedImageIndex = acreNode.ImageIndex;
                acreNode.Tag = acre;
                acreNode.Name = acre.Key.ToString();

                if (acre.Value.Contains("Border"))
                    treeView1.Nodes[0].Nodes.Add(acreNode);
                else if (acre.Value.Contains("Empty"))
                    treeView1.Nodes[1].Nodes.Add(acreNode);
                else if (acre.Value.Contains("Cliff") || acre.Value.Contains("Ramp"))
                    treeView1.Nodes[2].Nodes.Add(acreNode);
                else if ((acre.Value.Contains("River") || acre.Value.Contains("Lake")) && !acre.Value.Contains("Beachfront"))
                    treeView1.Nodes[3].Nodes.Add(acreNode);
                else if (acre.Value.Contains("Nook's Acre") || acre.Value.Contains("Post Office") || acre.Value.Contains("Dump") || acre.Value.Contains("Train Station") || acre.Value.Contains("Tailor's Shop") || acre.Value.Contains("Museum") || acre.Value.Contains("Police Station") || acre.Value.Contains("Wishing Well") || acre.Value.Contains("Player Houses"))
                    treeView1.Nodes[4].Nodes.Add(acreNode);
                else if (acre.Value.Contains("Beachfront"))
                    treeView1.Nodes[5].Nodes.Add(acreNode);
                else if (acre.Value.Contains("Ocean"))
                    treeView1.Nodes[6].Nodes.Add(acreNode);
                else if (acre.Value.Contains("Island"))
                    treeView1.Nodes[7].Nodes.Add(acreNode);
                else
                    treeView1.Nodes[8].Nodes.Add(acreNode);
            }

            treeView1.AfterSelect += new TreeViewEventHandler(treeNode_Selected);

            treeView1.EndUpdate();
        }

        //Upcoming Feature. WIP currently.
        private void Generate_Random_Town(bool Normal_Rules = true)
        {
            Random Number_Generator = new Random();
            ushort[] Acre_Buffer = new ushort[70];
            List<byte> A_Acres = new List<byte>() { 1, 2, 3, 4, 5 };
            int Town_Layer_Count = Number_Generator.Next(Normal_Rules ? 2 : 1, Normal_Rules ? 3 : 4);
            int Town_Layers = Town_Layer_Count - 1;
            int River_Start_Acre = Number_Generator.Next(1, 5);
            int River_Turn_Count = Town_Layer_Count == 4 ? 0 : Number_Generator.Next(1, 3);
            River_Turn_Count = River_Turn_Count + River_Turn_Count % 2; //Must turn an even amount to get back to valid connected acres
            A_Acres.Remove((byte)River_Start_Acre);
            Acre_Buffer[0] = (ushort)(0x344 + Town_Layers);
            //Acre_Buffer[River_Start_Acre - 1] = 0; //Make this method generate acres in rows of 7 would be a lot easier
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox1_FocusLost(sender, null);
        }

        private void textBox1_FocusLost(object sender, EventArgs e)
        {
            TextBox s = (TextBox)sender;
            ushort acreId = 0;
            if (s.Text.Contains("0x"))
                s.Text.Replace("0x", "");
            ushort.TryParse(s.Text, NumberStyles.AllowHexSpecifier, null, out acreId);
            if (acreId < 0x600)
            {
                bool acreDataExists = AcreData.Acres.ContainsKey(acreId);
                pictureBox1.Image = AcreData.ToAcrePicture(acreId);
                string acreInfo = "Unknown Acre";
                if (acreDataExists)
                    AcreData.Acres.TryGetValue(acreId, out acreInfo);
                label2.Text = acreInfo;
                label3.Text = "0x" + acreId.ToString("X4");
                currentlySelectedAcre = acreId;
                acreSelected = true;
            }
        }

        private void AcreEditor_Shown(object sender, EventArgs e)
        {
            int xOffset = 10;
            int yOffset = 42;
            int i = 0;
            foreach (KeyValuePair<int, Acre> data in currentAcreData)
            {
                acreImages[i] = new PictureBox();
                acreImages[i].SizeMode = PictureBoxSizeMode.StretchImage;
                acreImages[i].Size = new Size(64, 64);
                acreImages[i].Image = AcreData.ToAcrePicture(data.Value.AcreID);
                acreImages[i].Location = new Point(xOffset + (i % 7) * 65 + (1 + (i % 7)), (i / 7) * 65 + yOffset);
                acreImages[i].Name = data.Value.AcreID.ToString();
                acreImages[i].MouseClick += new MouseEventHandler(acreImage_Click);
                this.Controls.Add(acreImages[i]);
                i++;
            }
            Populate_Villager_Houses();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ushort[] acreData = new ushort[currentAcreData.Count];
            foreach (KeyValuePair<int, Acre> acre in currentAcreData)
                acreData[acre.Key - 1] = acre.Value.AcreID;

            DataConverter.WriteUShortArray(acreData, MainForm.AcreTile_Offset);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Export_Acre_Map();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Import_Select_File();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (PictureBox House in Villager_Houses)
                House.Visible = checkBox1.Checked;
        }
    }
}
