using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animal_Crossing_GCN_Save_Editor
{
    public partial class AcreEditor : Form
    {

        int mode = 0;
        ushort currentlySelectedAcre = 0;
        PictureBox[] acreImages = new PictureBox[72];
        public Dictionary<int, Acre> currentAcreData;
        Form1 form;

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
            {0x0345, 10 },
            {0x0325, 10 },
            {0x0329, 10 },
            {0x0349, 10 },
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
            {0x04A4, 70 }, //Island (Left) (1)
            {0x04A0, 70 }, //Island (Right (1)
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
            {0x0000, 10 }, //No Data
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
        };

        void acreImage_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (mode == 0)
            {
                foreach (KeyValuePair<ushort, int> data in AcreImages)
                {
                    if (data.Key == ushort.Parse(p.Name))
                    {
                        pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (data.Value).ToString());
                        currentlySelectedAcre = data.Key;
                        label2.Text = AcreData.Acres[data.Key];
                        break;
                    }
                }
            }
            else if (mode == 1 && currentlySelectedAcre != 0)
            {
                for (int i = 0; i < acreImages.Length; i++)
                {
                    if (p == acreImages[i])
                    {
                        p.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + AcreImages[currentlySelectedAcre].ToString());
                        //currentAcreData[i + 1].AcreID = currentlySelectedAcre;
                        p.Name = currentlySelectedAcre.ToString();
                        currentAcreData[i + 1] = new Acre(currentlySelectedAcre, currentAcreData[i + 1].Index);
                        break;
                    }
                }
            }
        }

        public AcreEditor(Form1 form)
        {
            this.form = form;
            InitializeComponent();
            foreach (KeyValuePair<ushort, string> data in AcreData.Acres)
            {
                listBox1.Items.Add(data.Value);
            }    
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string acreName = listBox1.SelectedItem.ToString();
            foreach (KeyValuePair<ushort, string> data in AcreData.Acres)
            {
                if (data.Value == acreName)
                {
                    pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + (AcreImages[data.Key]).ToString());
                    currentlySelectedAcre = data.Key;
                    label2.Text = data.Value;
                    break;
                }
            }
        }

        private void AcreEditor_Load(object sender, EventArgs e)
        {
            
        }

        private void AcreEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
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
                acreImages[i].Click += new EventHandler(acreImage_Click);
                this.Controls.Add(acreImages[i]);
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mode = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ushort[] acreData = new ushort[currentAcreData.Count];
            foreach (KeyValuePair<int, Acre> acre in currentAcreData)
            {
                acreData[acre.Key - 1] = acre.Value.AcreID;
            }
            form.WriteUShort(acreData, Form1.AcreTile_Offset);
            this.Hide();
        }

    }
}
