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
        public Villager[] Villagers;
        private BindingSource bs;
        private ushort TownIdentifier = 0;

        private void Setup_Villager_Editor()
        {
            for (int i = 0; i < 16; i++)
            {
                VillagerBoxes[i] = new ComboBox();
                foreach (KeyValuePair<ushort, string> v in VillagerData.Villagers)
                    VillagerBoxes[i].Items.Add(v.Value);
                VillagerBoxes[i].Text = Villagers[i].Name;
                VillagerBoxes[i].Name = Villagers[i].ID.ToString();
                VillagerBoxes[i].Size = new Size(200, 20);
                VillagerBoxes[i].Location = new Point(32, (i * 22) + 32);
                VillagerBoxes[i].DropDownStyle = ComboBoxStyle.DropDownList;
                Personalities[i] = new ComboBox();
                foreach (string v in VillagerData.Personalities)
                    Personalities[i].Items.Add(v);
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
                this.Controls.Add(Indexes[i]);
                this.Controls.Add(VillagerBoxes[i]);
                this.Controls.Add(Personalities[i]);
                this.Controls.Add(Catchphrases[i]);
                VillagerBoxes[i].SelectedValueChanged += delegate (object sender, EventArgs e)
                {
                    ComboBox c = (ComboBox)sender;
                    int x = Array.IndexOf(VillagerBoxes, c);
                    if (c.SelectedIndex != -1 && x > -1)
                    {
                        Villager v = Villagers[x];
                        if (!v.Exists)
                        {
                            v.TownIdentifier = TownIdentifier;
                            v.Exists = true;
                            v.Shirt = new Item(0x2400);
                        }
                        v.ID = VillagerData.GetVillagerID(c.Text);
                        v.Name = c.Text;
                        v.Name = v.ID.ToString();
                    }
                };

                Personalities[i].SelectedValueChanged += delegate (object sender, EventArgs e)
                {
                    ComboBox c = (ComboBox)sender;
                    int x = Array.IndexOf(VillagerBoxes, c);
                    if (c.SelectedIndex != -1 && x > -1)
                    {
                        Villagers[x].Personality = c.Text;
                        Villagers[x].PersonalityID = (byte)VillagerData.GetVillagerPersonalityID(c.Text);
                    }
                };

                Catchphrases[i].TextChanged += delegate (object sender, EventArgs e)
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
                };
            }
        }

        public Villager_Editor(Villager[] villagers)
        {
            InitializeComponent();
            bs = new BindingSource(VillagerData.VillagerDatabase, null);
            Villagers = villagers;
            TownIdentifier = DataConverter.ReadRawUShort(0x8, 2)[0];
            Setup_Villager_Editor();
            for (int i = 0; i < 16; i++)
                VillagerBoxes[i].SelectedValue = Villagers[i].ID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
                Villagers[i].Write();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
