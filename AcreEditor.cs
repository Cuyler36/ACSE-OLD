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

namespace ACSE
{
    public partial class AcreEditor : Form
    {
        ushort currentlySelectedAcre = 0;
        private PictureBox[] acreImages = new PictureBox[70];
        public Dictionary<int, Acre> currentAcreData;
        private Form1 form;
        private ImageList imageList;
        private TreeView treeView1;
        private int[] treeViewIconIndex;

        public static Dictionary<ushort, int> CliffAcres = new Dictionary<ushort, int>()
        {
            {0x0335, 71 },
            {0x0341, 71 },
            {0x0330, 73 },
            {0x0339, 71 },
            {0x032C, 72 },
            {0x033C, 73 }, //Cliff (Right Lower Acre)
            {0x03B4, 74 }, //Beachfront Cliff (Left Lower Acre)
            {0x03B8, 74 }, //Beachfront Cliff (Right)
            {0x0338, 72 }, //Border Cliff (Lower)
        };

        public static Dictionary<ushort, int> AcreImages = new Dictionary<ushort, int>()
        {
            {0x0345, 79 },
            {0x0325, 81 },
            {0x0329, 80 },
            {0x0349, 79 },
            {0x0335, 71 },
            {0x0385, 2 },
            {0x0295, 4 },
            {0x02F5, 3 },
            {0x02C1, 5 },
            {0x0375, 1 },
            {0x0341, 71 },
            {0x0330, 73 },
            {0x0160, 57 },
            {0x012C, 54 },
            {0x035D, 6 },
            {0x022D, 29 },
            {0x02FD, 16 },
            {0x0339, 71 },
            {0x032C, 72 },
            {0x0278, 10 },
            {0x01B8, 49 },
            {0x0185, 17 },
            {0x01E1, 20 },
            {0x0261, 27 },
            {0x0488, 7 },
            {0x01A4, 61 },
            {0x0084, 36 },
            {0x0088, 58 },
            {0x00C0, 53 },
            {0x0290, 10 }, //Lower empty acre
            {0x036C, 9 }, //lower wishing well
            {0x024C, 12 }, //Lower river
            {0x034C, 8 }, //lower police station
            {0x021C, 61 }, //Cliff, Down > Right
            {0x033C, 73 }, //Cliff (Right Lower Acre)
            {0x03B4, 74 }, //Beachfront Cliff (Left Lower Acre)
            {0x03F8, 63 }, //Beachfront Far Left
            {0x03FC, 63 }, //Beachfront Left
            {0x03B0, 65 }, //Beachfront w/ river (no bridge)
            {0x0494, 64 }, //Beachfront w/ Tailor shop
            {0x0498, 67 }, //Beachfront w/ Dock (Far Right)
            {0x03B8, 74 }, //Beachfront Cliff (Right)
            {0x0518, 70 }, //Ocean Border Left
            {0x0530, 70 }, //Ocean Far Left
            {0x0534, 70 }, //Ocean Left
            {0x0548, 70 }, //Ocean Middle
            {0x0570, 70 }, //Ocean Right
            {0x0574, 70 }, //Ocean Far Right
            {0x051C, 70 }, //Ocean Border Right
            {0x0381, 2 }, //Post Office
            {0x0155, 3 }, //Train Station (Orange Roof)
            {0x02B9, 5 }, //Train Bridge (2)
            {0x037D, 1 }, //Nook's Acre (2)
            {0x028D, 10 }, //Empty Acre Upper
            {0x0361, 6 }, //Player House Acre (2)
            {0x0269, 12 }, //River (Upper) Down w/ Bridge
            {0x0275, 10 }, //Empy Acre Upper (2)
            {0x0281, 10 }, //Empty Acre Upper (3)
            {0x01C4, 35 }, //River (Upper) Down w/ Cliff (Up > Right)
            {0x0194, 54 }, //Ramp (Upper > Middle) Straight > Down
            {0x0364, 9 }, //Wishing Well (Lower) (2)
            {0x00D4, 61 }, //Cliff (Upper > Lower) Down > Straight
            {0x015C, 57 }, //Cliff (Upper > Lower) Straight
            {0x0210, 37 }, //Cliff (Waterfall, Upper > Lower) Straight > Up
            {0x0184, 17 }, //River (Lower, Left > Down)
            {0x02CC, 25 }, //Lake (Lower, Straight > Straight)
            {0x0110, 27 }, //River (Lower, Down > Left) w/ Bridge
            {0x0338, 72 }, //Border Cliff (Lower)
            {0x03CC, 65 }, //Beachfront River (Down)
            {0x040C, 63 }, //Beachfront (2)
            {0x0490, 64 }, //Tailor Shop (2)
            {0x0558, 70 }, //Ocean
            {0x0544, 70 }, //Ocean
            {0x056C, 70 }, //Ocean
            {0x03DC, 70 }, //Empty (Ocean)
            {0x03E8, 70 }, //Empty (Ocean)
            {0x04B8, 70 }, //Ocean (Half)
            {0x0578, 70 }, //Ocean
            {0x04A4, 78 }, //Island (Left) (1)
            {0x04A0, 77 }, //Island (Right (1)
            {0x057C, 70 }, //Ocean
            {0x04D8, 70 }, //Ocean
            {0x04D4, 70 }, //Ocean
            {0x03E0, 70 }, //Empty (Ocean)
            {0x058C, 70 }, //Ocean
            {0x0588, 70 }, //Ocean
            {0x0584, 70 }, //Ocean
            {0x0580, 70 }, //Ocean
            {0x0371, 2 }, //Post Office (3)
            {0x032D, 71 }, //Cliff (Upper, Left Boundary)
            {0x01EC, 55 }, //Cliff (Upper, Up > Right)
            {0x011C, 58 }, //Cliff (Ramp Upper, Straight)
            {0x0200, 36 }, //Cliff (Waterfall, Straight)
            {0x009C, 57 }, //Cliff (Upper, Straight)
            {0x016C, 59 }, //Cliff (Upper, Right > Up)
            {0x00F0, 17 }, //River (Lower, Left > Down)
            {0x02E8, 28 }, //Lake (Lower, Down > Left)
            {0x0094, 10 }, //Empty Acre (Lower) (2)
            {0x0178, 29 }, //River (Lower, Down > Right)
            {0x01DC, 23 }, //River (Lower, Right)
            {0x00E8, 75 }, //River (Lower, Right > Down)
            {0x0274, 10 }, //Empty Acre (Lower) (3)
            {0x0350, 8 }, //Police Station (Lower) (2)
            {0x03D8, 66 }, //Beachfront (River) /w Bridge
            {0x0400, 63 }, //Beachfront
            {0x05AC, 67 }, //Beachfront w/ Dock
            {0x0564, 70 }, //Ocean
            {0x0538, 70 }, //Ocean
            {0x05B4, 70 }, //Ocean
            {0x03EC, 70 }, //Ocean (Empty)
            {0x04AC, 70 }, //Ocean (Half Empty)
            {0x0598, 78 }, //Island (Left) (2)
            {0x04D0, 70 }, //Ocean (Half Empty)
            {0x03E4, 70 }, //Ocean (Empty)
            {0x0000, 100 }, //No Data
            {0x04A8, 70 }, //Ocean
            {0x04CC, 70 }, //Ocean
            {0x04C8, 70 }, //Ocean
            {0x02C5, 5 }, //River (Upper Vertical) (2)
            {0x0285, 10 }, //Empty Acre (Upper) (4)
            {0x0279, 10 }, //Empty Acre (Upper) (5)
            {0x01B0, 55 }, //Cliff (Upper Left Corner)
            {0x006C, 34 }, //Cliff (Upper Right Corner) w/ Waterfall
            {0x0291, 10 }, //Empty Acre (Upper) (6)
            {0x01CC, 38 }, //River (Lower Horizontal) w/ Cliff
            {0x0320, 58 }, //Ramp (Upper Horizontal)
            {0x0264, 76 }, //River (Lower Left > Down) w/ Bridge
            {0x048C, 64 }, //Tailor's Shop (3)
            {0x03A0, 63 }, //Beachfront
            {0x0568, 70 }, //Ocean
            {0x049C, 70 }, //Ocean
            {0x04C0, 70 }, //Ocean
            {0x04BC, 70 }, //Ocean
            {0x0119, 4 }, //Dump (2)
            {0x02F1, 3 }, //Train Station (Green Roof)
            {0x0071, 5 }, //River (Upper Horizontal) (3)
            {0x0095, 10 }, //Empty Acre (Upper) (7)
            {0x024D, 12 }, //River (Upper Vertical) w/ Bridge
            {0x00B4, 55 }, //Cliff (Upper Left Corner)
            {0x018C, 58 }, //Ramp (Upper Horizontal)
            {0x0284, 10 }, //Empty Acre (Lower) (4)
            {0x0100, 12 }, //River (Lower Vertical) w/ Bridge
            {0x0354, 8 }, //Police Station (3)
            {0x02EC, 19 }, //Lake (Lower Left > Down)
            {0x01D0, 26 }, //River (Lower Down > Left)
            {0x0480, 7 }, //Museum (2)
            {0x0404, 63 }, //Beachfront
            {0x053C, 70 }, //Ocean
            {0x05A4, 77 }, //Island (Right) (2)
            {0x04DC, 70 }, //Ocean
            {0x04B0, 70 }, //Ocean
            {0x02E9, 28 },
            {0x026D, 24},
            {0x017D, 75 },
            {0x0164, 57 },
            {0x0204, 36 },
            {0x01E8, 53 },
            {0x0220, 11 },
            {0x03D4, 66 },
            {0x0560, 70 },
            {0x05A0, 78 },
            {0x0594, 77 },
            {0x0379, 1 },
            {0x00E5, 29 },
            {0x0359, 6 },
            {0x02C9, 22 },
            {0x0061, 15 },
            {0x0214, 34 },
            {0x0099, 10 },
            {0x0484, 7 },
            {0x05A8, 78 },
            {0x0115, 76},
            {0x01D1, 26 },
            {0x027C, 10 },
            {0x0138, 38 },
            {0x028C, 10 },
            {0x0234, 26 },
            {0x01B4, 53 },
            {0x0265, 76 },
            {0x00ED, 26 },
            {0x0134, 62 },
            {0x0410, 60 },
            {0x0098, 10 },
            {0x03C8, 65 },
            {0x03F4, 63 },
            {0x05B0, 67 },
            {0x0554, 70 },
            {0x0504, 70 },
            {0x05B8, 70 },
            {0x0101, 12 },
            {0x0180, 18 },
            {0x022C, 29 },
            {0x0280, 10 },
            {0x04C4, 70 },
            {0x01FD, 13 }, 
            {0x00CC, 49 },
            {0x00A8, 59 },
            {0x03D0, 66 },
            {0x055C, 70 },
            {0x059C, 77 },
            {0x0299, 4 },
            {0x027D, 10 },
            {0x0188, 60 },
            {0x0114, 76 },
            {0x03C0, 65 },
            {0x054C, 70 },
            {0x04B4, 70 },
            {0x02BD, 5 },
            {0x0368, 9},
            {0x00E0, 23 },
            {0x01C0, 23 },
            {0x00EC, 26 },
            {0x00E4, 30 },
            {0x0230, 75 },
            {0x0408, 63 },
            {0x0540, 70 },
            {0x0060, 15 },
        };

        void acreImage_Click(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (e.Button == MouseButtons.Right)
            {
                ushort id = ushort.Parse(p.Name);
                bool acreExists = AcreImages.ContainsKey(id);
                pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (acreExists ? AcreImages[id] : 99).ToString());
                label2.Text = acreExists ? AcreData.Acres[id] : "Unknown Acre";
                currentlySelectedAcre = id;
                label3.Text = "0x" + id.ToString("X4");
            }
            else if (e.Button == MouseButtons.Left && currentlySelectedAcre != 0)
            {
                int idx = Array.IndexOf(acreImages, p);
                if (idx > -1)
                {
                    p.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (AcreImages.ContainsKey(currentlySelectedAcre) ? AcreImages[currentlySelectedAcre].ToString() : "99"));
                    p.Name = currentlySelectedAcre.ToString();
                    currentAcreData[idx + 1] = new Acre(currentlySelectedAcre, currentAcreData[idx + 1].Index);
                }
            }
        }

        public AcreEditor(Form1 form)
        {
            this.form = form;
            InitializeComponent();
            imageList = new ImageList();
            imageList.ImageSize = new Size(24, 24);
            List<int> iconIndex = new List<int>();
            for (int i = 1; i < 68; i++)
            {
                imageList.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("_" + i));
                iconIndex.Add(i);
            }
            for (int i = 70; i < 82; i++)
            {
                imageList.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("_" + i));
                iconIndex.Add(i);
            }
            for (int i = 99; i < 101; i++)
            {
                imageList.Images.Add((Image)Properties.Resources.ResourceManager.GetObject("_" + i));
                iconIndex.Add(i);
            }

            treeViewIconIndex = iconIndex.ToArray();
            iconIndex.Clear();
            populate_TreeView();
        }

        private void setSelectedAcre(KeyValuePair<ushort, string> acre)
        {
            if (AcreData.Acres.Contains(acre))
            {
                ushort acreId = acre.Key;
                pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (AcreImages[acreId]).ToString());
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

        private void populate_TreeView()
        {
            treeView1 = new TreeView();
            treeView1.ItemHeight = 26;
            treeView1.Size = new Size(261, 329);
            treeView1.Location = new Point(250, 42);
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
            islandAcres.ImageIndex = 75;
            islandAcres.SelectedImageIndex = 75;
            TreeNode miscAcres = new TreeNode("Miscellaneous");
            miscAcres.ImageIndex = 80;
            miscAcres.SelectedImageIndex = 80;

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
                acreNode.ImageIndex = Array.IndexOf(treeViewIconIndex, AcreImages[acre.Key]);
                acreNode.SelectedImageIndex = acreNode.ImageIndex;
                acreNode.Tag = acre;

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
                pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (acreDataExists ? (AcreImages[acreId]).ToString() : "99"));
                string acreInfo = "Unknown Acre";
                if (acreDataExists)
                    AcreData.Acres.TryGetValue(acreId, out acreInfo);
                label2.Text = acreInfo;
                label3.Text = "0x" + acreId.ToString("X4");
                currentlySelectedAcre = acreId;
            }
        }

        private void AcreEditor_Shown(object sender, EventArgs e)
        {
            int y = 1;
            int xOffset = 10;
            int yOffset = 42;
            int i = 0;
            foreach (KeyValuePair<int, Acre> data in currentAcreData)
            {
                int imageNumber = AcreImages.ContainsKey(data.Value.AcreID) ? AcreImages[data.Value.AcreID] : 99;
                if (i % 7 == 0 && i > 0)
                    y = y + 33;
                acreImages[i] = new PictureBox();
                acreImages[i].SizeMode = PictureBoxSizeMode.StretchImage;
                acreImages[i].Size = new Size(32, 32);
                acreImages[i].Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (imageNumber).ToString());
                acreImages[i].Location = new Point(xOffset + (i % 7) * 33 + (1 + (i % 7)), y + yOffset);
                acreImages[i].Name = data.Value.AcreID.ToString();
                acreImages[i].MouseClick += new MouseEventHandler(acreImage_Click);
                this.Controls.Add(acreImages[i]);
                i++;
            }
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

            form.WriteUShort(acreData, Form1.AcreTile_Offset);
            this.Close();
        }

    }
}
