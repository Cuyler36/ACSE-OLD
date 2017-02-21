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
    public partial class Villager_Editor : Form
    {
        private ComboBox[] VillagerBoxes = new ComboBox[16];
        private ComboBox[] Personalities = new ComboBox[16];
        private TextBox[] Catchphrases = new TextBox[16];
        private Label[] Indexes = new Label[16];
        private Panel[] House_Coords = new Panel[16];
        private TextBox[] House_Coord_Boxes = new TextBox[16 * 4];
        public Villager[] Villagers;
        private BindingSource bs;
        private ushort TownIdentifier = 0;

        private void Setup_Villager_Editor()
        {
            string[] Villager_Names = VillagerData.Villagers.Values.ToArray();
            for (int i = 0; i < 16; i++)
            {
                VillagerBoxes[i] = new ComboBox();
                VillagerBoxes[i].Items.AddRange(Villager_Names);
                VillagerBoxes[i].Text = Villagers[i].Name;
                VillagerBoxes[i].Name = Villagers[i].ID.ToString();
                VillagerBoxes[i].Size = new Size(200, 20);
                VillagerBoxes[i].Location = new Point(32, (i * 22) + 32);
                VillagerBoxes[i].DropDownStyle = ComboBoxStyle.DropDownList;
                Personalities[i] = new ComboBox();
                Personalities[i].Items.AddRange(VillagerData.Personalities);
                Personalities[i].Text = Villagers[i].Personality;
                Personalities[i].DropDownStyle = ComboBoxStyle.DropDownList;
                Personalities[i].Size = new Size(80, 20);
                Personalities[i].Location = new Point(236, (i * 22) + 32);
                Catchphrases[i] = new TextBox();
                Catchphrases[i].Size = new Size(120, 20);
                Catchphrases[i].Location = new Point(318, (i * 22) + 32);
                Catchphrases[i].Text = Villagers[i].Catchphrase;
                Catchphrases[i].MaxLength = 10;
                Indexes[i] = new Label();
                Indexes[i].Text = i == 15 ? "Isl." : (i + 1).ToString();
                Indexes[i].Location = new Point(10, (i * 22) + 35);
                Indexes[i].AutoSize = true;
                House_Coords[i] = new Panel()
                {
                    Size = new Size(180, 20),
                    Location = new Point(442, (i * 22) + 32),
                };
                for (int x = 0; x < 4; x++)
                {
                    int idx = i * 4 + x;
                    House_Coord_Boxes[idx] = new TextBox()
                    {
                        Size = new Size(25, 20),
                        Location = new Point(15 + x * 45, 0),
                        MaxLength = 2,
                        Text = Villagers[i].Exists ? Villagers[i].House_Coords[x].ToString() : "0"
                    };
                    House_Coord_Boxes[idx].TextChanged += new EventHandler(House_Position_Changed);
                    House_Coords[i].Controls.Add(House_Coord_Boxes[idx]);
                }
                Controls.Add(Indexes[i]);
                Controls.Add(VillagerBoxes[i]);
                Controls.Add(Personalities[i]);
                Controls.Add(Catchphrases[i]);
                Controls.Add(House_Coords[i]);
                VillagerBoxes[i].SelectedValueChanged += new EventHandler(Villager_Changed);
                Personalities[i].SelectedValueChanged += new EventHandler(Personality_Changed);
                Catchphrases[i].TextChanged += new EventHandler(Catchphrase_Changed);
            }
        }

        private void House_Position_Changed(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            int idx = Array.IndexOf(House_Coords, t.Parent);
            int coord = Array.IndexOf(House_Coord_Boxes, t);
            byte pos = 1;
            if (idx > -1 && coord > -1)
            {
                byte.TryParse(t.Text, out pos);
                int realCoord = coord % 4;
                Villagers[idx].House_Coords[realCoord] = pos; //Remember that X Positions were incremented
            }
        }

        private void Villager_Changed(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int x = Array.IndexOf(VillagerBoxes, c);
            if (c.SelectedIndex != -1 && x > -1)
            {
                Villager v = Villagers[x];
                if (c.SelectedIndex > 0)
                {
                    if (!v.Exists)
                    {
                        v.Personality = "Lazy";
                        v.PersonalityID = 0;
                        v.TownIdentifier = TownIdentifier;
                        v.Modified = true;
                        v.Shirt = new Item(0x2400);
                    }
                    if (Properties.Settings.Default.ModifyVillagerHouse && v.Index < 16)
                        v.Remove_House(); //Cleanup old house(s)
                    v.ID = VillagerData.GetVillagerID(c.Text);
                    v.Name = c.Text;
                    v.Name = v.ID.ToString();
                    if (Properties.Settings.Default.ModifyVillagerHouse && v.Index < 16)
                        v.Add_House();
                }
                else if (c.SelectedIndex == 0)
                    v.Delete();
            }
        }

        private void Personality_Changed(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int x = Array.IndexOf(VillagerBoxes, c);
            if (c.SelectedIndex != -1 && x > -1)
            {
                Villagers[x].Personality = c.Text;
                Villagers[x].PersonalityID = (byte)VillagerData.GetVillagerPersonalityID(c.Text);
            }
        }

        private void Catchphrase_Changed(object sender, EventArgs e)
        {
            TextBox b = (TextBox)sender;
            int maxBytes = StringUtil.StringToMaxChars(b.Text);
            if (b.Text.ToCharArray().Length > 10)
            {
                b.Text = b.Text.Substring(0, 10);
                b.SelectionStart = b.Text.Length;
                b.SelectionLength = 0;
            }
            if (Encoding.UTF8.GetBytes(b.Text.ToCharArray()).Length > maxBytes)
            {
                b.Text = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(b.Text), 0, maxBytes);
                b.SelectionStart = b.Text.Length;
                b.SelectionLength = 0;
            }
            if (b.Text.ToCharArray().Length < 11)
                Villagers[Array.IndexOf(Catchphrases, b)].Catchphrase = b.Text;
        }

        public Villager_Editor(Villager[] villagers)
        {
            InitializeComponent();
            bs = new BindingSource(VillagerData.VillagerDatabase, null);
            Villagers = villagers;
            TownIdentifier = DataConverter.ReadUShort(0x8);
            Setup_Villager_Editor();
            for (int i = 0; i < 16; i++)
                VillagerBoxes[i].SelectedValue = Villagers[i].ID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                Villagers[i].Write();
                MainForm.Villagers = Villagers;
            }
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
