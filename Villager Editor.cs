using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animal_Crossing_GCN_Save_Editor
{
    public partial class Villager_Editor : Form
    {
        private ComboBox[] VillagerBoxes = new ComboBox[16];
        private ComboBox[] Personalities = new ComboBox[16];
        private TextBox[] Catchphrases = new TextBox[16];
        private Label[] Indexes = new Label[16];
        private Form1 form;
        public Villager[] Villagers;
        private BindingSource bs;

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
                    if (c.SelectedIndex != -1)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            if (VillagerBoxes[x] == c)
                            {
                                Villagers[x].ID = VillagerData.GetVillagerID(c.Text);
                                Villagers[x].Name = c.Text;
                                VillagerBoxes[x].Name = Villagers[x].ID.ToString();
                                break;
                            }
                        }
                    }
                };

                Personalities[i].SelectedValueChanged += delegate (object sender, EventArgs e)
                {
                    ComboBox c = (ComboBox)sender;
                    if (c.SelectedIndex != -1)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            if (Personalities[x] == c)
                            {
                                Villagers[x].Personality = c.Text;
                                Villagers[x].PersonalityID = (byte)VillagerData.GetVillagerPersonalityID(c.Text);
                                break;
                            }
                        }
                    }
                };

                Catchphrases[i].TextChanged += delegate (object sender, EventArgs e)
                {
                    TextBox b = (TextBox)sender;
                    int maxBytes = StringUtil.StringToMaxChars(b.Text);
                    if (b.Text.ToCharArray().Length > 8)
                    {
                        b.Text = b.Text.Substring(0, 8);
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

        public Villager_Editor(Villager[] villagers, Form1 form1)
        {
            form = form1;
            InitializeComponent();
            bs = new BindingSource(VillagerData.VillagerDatabase, null);
            Villagers = villagers;
            Setup_Villager_Editor();
            for (int i = 0; i < 16; i++)
            {
                KeyValuePair<ushort, string> t = VillagerData.VillagerDatabase.FirstOrDefault(o => o.Key == Villagers[i].ID);
                int idx = VillagerData.VillagerDatabase.IndexOf(t);
                //MessageBox.Show("VillagerID: " + Villagers[i].ID + " | Name:" + Villagers[i].Name + " | KeyValuePair<ushort, string>: " + t.ToString() + " | Index: " + idx);
                VillagerBoxes[i].SelectedValue = Villagers[i].ID;
            }
        }

        private void Villager_Editor_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ushort[] VillagerIds = new ushort[16];
            byte[] vPersonalities = new byte[16];
            string[] catchphrases = new string[16];

            foreach (Villager v in Villagers)
            {
                VillagerIds[v.Index - 1] = v.ID;
                vPersonalities[v.Index - 1] = v.PersonalityID;
                catchphrases[v.Index - 1] = v.Catchphrase;
            }

            for (int i = 0; i < 15; i++)
            {
                if(Villagers[i].ID > 0 && Villagers[i].isSet == false)
                {
                    MessageBox.Show("Villager wasn't set. Adding blank data! | " + Villagers[i].ID.ToString("X"));
                    //form.WriteDataRaw(Form1.VillagerData_Offset + (i * 0x988), Form1.Blank_Villager); //FIX THIS
                }

                form.WriteUShort(new ushort[] { VillagerIds[i] }, Form1.VillagerData_Offset + (i * 0x988));
                form.WriteDataRaw(Form1.VillagerData_Offset + (i * 0x988) + 0xD, new byte[] { vPersonalities[i] });
                form.WriteString(Form1.VillagerData_Offset + (i * 0x988) + 0x89D, catchphrases[i], 10);
            }

            form.WriteUShort(new ushort[] { VillagerIds[15] }, Form1.Islander_Offset);
            form.WriteDataRaw(Form1.Islander_Offset + 0xD, new byte[] { vPersonalities[15] });
            form.WriteString(Form1.Islander_Offset + 0x89D, catchphrases[15], 10);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
