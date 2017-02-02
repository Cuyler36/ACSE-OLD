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
        private ushort currentlySelectedAcre = 0;
        private bool acreSelected = false;
        private PictureBox[] acreImages = new PictureBox[70];
        private Dictionary<int, Acre> currentAcreData;
        private ImageList imageList;
        private TreeView treeView1;
        private int[] treeViewIconIndex;

        void acreImage_Click(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (e.Button == MouseButtons.Right)
            {
                acreSelected = true;
                ushort id = ushort.Parse(p.Name);
                bool acreExists = AcreData.AcreImages.ContainsKey(id);
                pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (acreExists ? AcreData.AcreImages[id] : 99).ToString());
                label2.Text = acreExists ? AcreData.Acres[id] : "Unknown Acre";
                currentlySelectedAcre = id;
                label3.Text = "0x" + id.ToString("X4");
            }
            else if (e.Button == MouseButtons.Left && acreSelected)
            {
                int idx = Array.IndexOf(acreImages, p);
                if (idx > -1)
                {
                    p.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (AcreData.AcreImages.ContainsKey(currentlySelectedAcre) ? AcreData.AcreImages[currentlySelectedAcre].ToString() : "99"));
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
                acreSelected = true;
                ushort acreId = acre.Key;
                pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (AcreData.AcreImages[acreId]).ToString());
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
                acreNode.ImageIndex = Array.IndexOf(treeViewIconIndex, AcreData.AcreImages[acre.Key]);
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
                pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (acreDataExists ? (AcreData.AcreImages[acreId]).ToString() : "99"));
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
            int y = 1;
            int xOffset = 10;
            int yOffset = 42;
            int i = 0;
            foreach (KeyValuePair<int, Acre> data in currentAcreData)
            {
                int imageNumber = AcreData.AcreImages.ContainsKey(data.Value.AcreID) ? AcreData.AcreImages[data.Value.AcreID] : 99;
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

            DataConverter.WriteUShort(acreData, Form1.AcreTile_Offset);
            this.Close();
        }

    }
}
