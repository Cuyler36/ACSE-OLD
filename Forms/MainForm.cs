﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ACSE
{
    public partial class MainForm : Form
    {
        public static byte[] saveBuffer = new byte[0x26000];

        #region Offsets
        public static int Data_Start_Offset = 0x26040;
        public static int Town_Name_Offset = 0x9120;
        //House Carpet & Wallpaper offset: 0x8A0
        public static int AcreData_Offset = 0x137A8;
        public static int AcreData_Size = 0x3C00;
        public static int AcreTile_Offset = 0x173A8;
        public static int AcreTile_Size = 0x8C;
        //public static int[] Player1_Dresser_Offsets = new int[3] { 0x9F6E, 0xA196, 0xA3BE }; //0x228 away from each other
        public static int VillagerData_Offset = 0x17438;
        public static int VillagerData_Size = 0x8EF8;
        public static int IslandData_Offset = 0x22554;
        public static int IslandData_Length = 0x400;
        public static int Islander_Offset = 0x23440;
        public static int BurriedItems_Offset = 0x20F1C;  //Each byte is 8 spaces. Stored in binary format (02) = 0000 00x0 (reversed)
        public static int BurriedItems_Length = 0x3C0;
        public static int IslandBurriedItems_Offset = 0x23DC9;
        public static int IslandBurriedItems_Length = 0x40;
        public static int Nook_Items_Offset = 0x2040E;
        public static int Nook_Spent_Bells_Offset = 0x20468;
        public static int Nookingtons_Visitor_Flag = 0x2047F;
        public static int Town_Tune_Offset = 0x20F08;
        #endregion Offsets

        #region Variables
        public static readonly string Assembly_Location = Directory.GetCurrentDirectory();
        static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //static readonly int Date_Offset = 946684799; //Date at 12/31/1999 @ 11:59:59PM
        private FileStream fs;
        private BinaryReader reader;
        private BinaryWriter writer;
        public static string fileName;
        private TownEditor townEditorForm;
        private AcreEditor editor;
        private Villager_Editor vEditor;
        private Inventory_Editor iEditor;
        private House_Editor hEditor;
        private SettingsForm settingsForm = new SettingsForm();
        private NookEditor nEditor;
        List<KeyValuePair<ushort, string>> Shirts = new List<KeyValuePair<ushort, string>>();
        private bool CanSetData = false;
        private ACString TownName;
        private Player[] Players = new Player[4];
        private ComboBox[] Faces = new ComboBox[4];
        private string patternSaveLoc;
        private Bitmap currentPattern;
        private CancelEventHandler importHandler;
        public static byte[] SaveBuffer { get { return saveBuffer; } }
        public static Villager[] Villagers = new Villager[16];
        public static ACDate Town_Date;
        public static Dictionary<byte, string> Tune_Chart = new Dictionary<byte, string>()
        {
            { 0xE, "Zz.png" },
            { 0xF, "Empty.png" },
            { 0x0, "Blue_G.png" },
            { 0x1, "Blue_A.png" },
            { 0x2, "Blue_B.png" },
            { 0x3, "Green_C.png" },
            { 0x4, "Green_D.png" },
            { 0x5, "Green_E.png" },
            { 0x6, "Green_F.png" },
            { 0x7, "Green_G.png" },
            { 0x8, "Green_A.png" },
            { 0x9, "Yellow_B.png" },
            { 0xA, "Yellow_C.png" },
            { 0xB, "Orange_D.png" },
            { 0xC, "Orange_E.png" },
            { 0xD, "Random.png" }
        };
        #endregion Variables

        public static DateTime DateFromTimestamp(long timestamp)
        {
            return _unixEpoch.AddSeconds(timestamp);
        }

        private byte[] GetSaveData(int offset, int size)
        {
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            return reader.ReadBytes(size);
        }

        private void SaveData()
        {
            Checksum.Update(saveBuffer);
            fs = new FileStream(fileName, FileMode.Open);
            writer = new BinaryWriter(fs);
            writer.Seek(Data_Start_Offset, SeekOrigin.Begin);
            writer.Write(saveBuffer);
            if (Properties.Settings.Default.SecondSave)
            {
                writer.Seek(0x4C040, SeekOrigin.Begin);
                writer.Write(saveBuffer);
                MessageBox.Show("Wrote to Second Save!");
            }
            writer.Close();
            fs.Close();
        }

        public MainForm()
        {
            InitializeComponent();
            ItemData.AddVillagerHouses();
            ItemData.SetupItemDictionary();
            VillagerData.CreateVillagerDatabase();
            for (int i = 0; i < ItemData.Shirt_IDs.Length; i++)
                Shirts.Add(new KeyValuePair<ushort, string>(ItemData.Shirt_IDs[i], ItemData.Shirt_Names[i]));
            AcreData.Parse_Unique_Images();
            AcreData.Parse_Images();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void setControlsEnabled(bool val, Control container)
        {
            foreach (Control c in container.Controls)
                if (c is Panel || c is GroupBox)
                    setControlsEnabled(val, c);
                else
                    c.Enabled = true;
        }

        private void exportClick(object sender, EventArgs e)
        {
            MenuItem m = (MenuItem)sender;
            ContextMenu menu = (ContextMenu)m.Parent;
            PictureBox pattern = (PictureBox)menu.SourceControl;
            currentPattern = (Bitmap)pattern.Image;
            saveFileDialog1.FileName = pattern.Name;
            saveFileDialog1.ShowDialog();
        }

        private void renameClick(object sender, EventArgs e, Pattern p, ToolTip t)
        {
            MenuItem m = (MenuItem)sender;
            ContextMenu menu = (ContextMenu)m.Parent;
            PictureBox pattern = (PictureBox)menu.SourceControl;
            currentPattern = (Bitmap)pattern.Image;
            PatternNameForm f = new PatternNameForm(p, pattern, t);
            f.Show();
        }

        private void paletteClick(object sender, EventArgs e, Pattern p)
        {
            MenuItem m = (MenuItem)sender;
            ContextMenu menu = (ContextMenu)m.Parent;
            PictureBox pattern = (PictureBox)menu.SourceControl;
            PatternPaletteForm f = new PatternPaletteForm(p, pattern);
            f.Show();
        }

        private void importClick(object sender, EventArgs e, Pattern p, ToolTip t)
        {
            MenuItem m = (MenuItem)sender;
            ContextMenu menu = (ContextMenu)m.Parent;
            PictureBox pattern = (PictureBox)menu.SourceControl;
            importHandler = new CancelEventHandler((s, ex) => openFileDialog2_OK(s, ex, p, pattern, t));
            openFileDialog2.FileOk += importHandler;
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_OK (object sender, CancelEventArgs e, Pattern p, PictureBox box, ToolTip t)
        {
            openFileDialog2.FileOk -= importHandler;
            OpenFileDialog f = (OpenFileDialog)sender;
            if (f.FileName != null)
            {
                Image fileImage = Image.FromFile(f.FileName);
                if (fileImage.Width == 32 && fileImage.Height == 32 && (fileImage.PixelFormat == PixelFormat.Format24bppRgb
                    || fileImage.PixelFormat == PixelFormat.Format32bppRgb
                    || fileImage.PixelFormat == PixelFormat.Format32bppArgb))
                {
                    bool hasAlphaChannel = fileImage.PixelFormat != PixelFormat.Format24bppRgb;
                    byte[] bitmapBytes = new byte[hasAlphaChannel ? 4096 : 3072];
                    Buffer.BlockCopy((byte[])(new ImageConverter().ConvertTo((Bitmap)fileImage, typeof(byte[]))), 54, bitmapBytes, 0, hasAlphaChannel ?  4096 : 3072);
                    byte[] convertedBytes = new byte[512]; //32bit argb > 2 Nibble palette
                    byte[] reversedBuffer = new byte[512];
                    int x = 0;
                    for (int i = 0; i < bitmapBytes.Length; i += (hasAlphaChannel ? 8 : 6))
                    {
                        byte[] bufferLeft = new byte[hasAlphaChannel ? 8 : 6];
                        byte[] bufferRight = new byte[hasAlphaChannel ? 8 : 6];
                        Buffer.BlockCopy(bitmapBytes, i, bufferLeft, 0, bufferLeft.Length / 2);
                        Buffer.BlockCopy(bitmapBytes, i + (hasAlphaChannel ? 4 : 3), bufferRight, 0, bufferRight.Length / 2);
                        convertedBytes[x] = (byte)((PatternData.ClosestColor(BitConverter.ToUInt32(bufferLeft, 0), p.Palette) << 4)
                            + PatternData.ClosestColor(BitConverter.ToUInt32(bufferRight, 0), p.Palette));
                        x++;
                    }
                    for (int i = 0; i < 512; i += 16)
                        Buffer.BlockCopy(convertedBytes, 512 - i - 16, reversedBuffer, i, 16);
                    p.GeneratePatternBitmapFromImport(reversedBuffer);
                    string pathName = Path.GetFileNameWithoutExtension(f.FileName);
                    p.Name = pathName.Length > 16 ? pathName.Substring(0, 16) : pathName;
                    t.SetToolTip(box, p.Name);
                    box.Image = p.Pattern_Bitmap;
                }
                else
                    MessageBox.Show("Imported images can only be 32x32, 24-bit or 32-bit Bitmaps. The supported formats are 24bppRgb, 32bppArgb, and 32bppRgb.");
            }
        }

        private void saveFileDialog1_OK (object sender, CancelEventArgs e)
        {
            patternSaveLoc = ((SaveFileDialog)sender).FileName;
            if (currentPattern != null)
                currentPattern.Save(patternSaveLoc, ImageFormat.Bmp);
        }

        private void addPatternBoxes(Player p, Control playerBox)
        {
            for (int i = 0; i < 8; i++)
            {
                Pattern pattern = p.Patterns[i];
                PictureBox b = new PictureBox()
                {
                    Name = pattern.Name,
                    Image = pattern.Pattern_Bitmap,
                    Size = new Size(32, 32),
                    Location = new Point(playerBox.Location.X + 19 + 38 * (i % 4), playerBox.Location.Y + playerBox.Size.Height + 10 + 38 * (i / 4))
                };
                ToolTip t = new ToolTip();
                t.SetToolTip(b, pattern.Name);
                t.ShowAlways = true;
                ContextMenu cm = new ContextMenu();
                MenuItem rename = new MenuItem("Rename");
                MenuItem palette = new MenuItem("Set Palette");
                MenuItem import = new MenuItem("Import");
                MenuItem export = new MenuItem("Export");
                cm.MenuItems.Add(rename);
                cm.MenuItems.Add(palette);
                cm.MenuItems.Add(import);
                cm.MenuItems.Add(export);
                b.ContextMenu = cm;
                export.Click += exportClick;
                import.Click += new EventHandler((sender, e) => importClick(sender, e, pattern, t));
                rename.Click += new EventHandler((sender, e) => renameClick(sender, e, pattern, t));
                palette.Click += new EventHandler((sender, e) => paletteClick(sender, e, pattern));
                this.Controls.Add(b);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            CanSetData = false;
            if (fs != null)
            {
                reader.Close();
                writer.Close();
                fs.Close();
            }
            fileName = openFileDialog1.FileName;
            string extension = Path.GetExtension(fileName);
            fs = new FileStream(fileName, FileMode.Open);
            reader = new BinaryReader(fs);
            writer = new BinaryWriter(fs);
            if (extension == ".gcs")
            {
                reader.BaseStream.Seek(0x110, SeekOrigin.Begin); //.gcs has 0x110 additional bytes at the beginning, other than that, the save structure is exactly the same
                Data_Start_Offset = 0x26040 + 0x110;
            }
            else if (extension == ".gci")
                Data_Start_Offset = 0x26040;
            if (Encoding.UTF8.GetString(reader.ReadBytes(3)) == "GAF")
            {
                Text = "ACSE - " + Path.GetFileName(fileName);
                if (iEditor != null && !iEditor.IsDisposed)
                    iEditor.Dispose();
                if (vEditor != null && !vEditor.IsDisposed)
                    vEditor.Dispose();
                if (editor != null && !editor.IsDisposed)
                    editor.Dispose();
                if (hEditor != null && !hEditor.IsDisposed)
                    hEditor.Dispose();
                if (townEditorForm != null && !townEditorForm.IsDisposed)
                    townEditorForm.Dispose();
                setControlsEnabled(true, this);
                GetSaveData(Data_Start_Offset, 0x26000).CopyTo(saveBuffer, 0);
                TownName = DataConverter.ReadString(Town_Name_Offset, 0x8);
                Town_Date = new ACDate(DataConverter.ReadDataRaw(0xA, 8)); //Last Played Date
                townNameTextBox.Text = TownName.Trim();
                townDateLabel.Text = "Last Town Date: " + Town_Date.Date_Time_String;

                //Add Players
                Player Player1 = new Player(0);
                Player Player2 = new Player(1);
                Player Player3 = new Player(2);
                Player Player4 = new Player(3);

                //Add Villagers
                for (int i = 0; i < 16; i++)
                    Villagers[i] = new Villager(i + 1);

                //Pattern PictureBoxes
                List<PictureBox> temp = new List<PictureBox>();
                foreach (PictureBox p in Controls.OfType<PictureBox>())
                    temp.Add(p);
                foreach (PictureBox p in temp)
                {
                    Bitmap i = (Bitmap)p.Image;
                    p.Image = null;
                    Controls.Remove(p);
                    p.Dispose();
                    i.Dispose();
                }
                temp.Clear();

                addPatternBoxes(Player1, groupBox1);
                if (Player2.Exists)
                    addPatternBoxes(Player2, groupBox2);
                if (Player3.Exists)
                    addPatternBoxes(Player3, groupBox3);
                if (Player4.Exists)
                    addPatternBoxes(Player4, groupBox4);

                player1Name.Text = Player1.Name.Trim();
                player1Bells.Text = Player1.Bells.ToString();
                player1Debt.Text = Player1.Debt.ToString();
                player1Savings.Text = Player1.Savings.ToString();
                player1HeldItem.Text = Player1.Held_Item.ItemID == 0 ? "(None)" : Player2.Held_Item.Name;
                player1Shirt.DataSource = new BindingSource(Shirts, null);
                player1Shirt.ValueMember = "Key";
                player1Shirt.DisplayMember = "Value";
                player1Shirt.SelectedValue = Player1.Shirt.ItemID;
                player1Background.DataSource = new BindingSource(Shirts, null);
                player1Background.ValueMember = "Key";
                player1Background.DisplayMember = "Value";
                player1Background.SelectedValue = Player1.Inventory_Background.ItemID;
                player1Face.DataSource = Player1.Gender == 0 ? new BindingSource(Player.Male_Faces, null) : new BindingSource(Player.Female_Faces, null);
                player1Face.ValueMember = "Key";
                player1Face.DisplayMember = "Value";
                player1Face.SelectedValue = Player1.Face;
                player1Gender.Text = Player1.Gender == 0 ? "Male" : "Female";
                player1HeldItem.Text = Player1.Held_Item.ItemID == 0 ? "(None)" : Player1.Held_Item.Name;
                groupBox1.Text = "Player1 - " + Player1.Last_Played_Date.Date_Time_String;

                if (Player2.Exists)
                {
                    player2Shirt.DataSource = new BindingSource(Shirts, null);
                    player2Shirt.ValueMember = "Key";
                    player2Shirt.DisplayMember = "Value";
                    player2Shirt.SelectedValue = Player2.Shirt.ItemID;
                    player2Background.DataSource = new BindingSource(Shirts, null);
                    player2Background.ValueMember = "Key";
                    player2Background.DisplayMember = "Value";
                    player2Background.SelectedValue = Player2.Inventory_Background.ItemID;
                    player2Face.DataSource = Player2.Gender == 0 ? new BindingSource(Player.Male_Faces, null) : new BindingSource(Player.Female_Faces, null);
                    player2Face.ValueMember = "Key";
                    player2Face.DisplayMember = "Value";
                    player2Face.SelectedValue = Player2.Face;
                    player2Gender.Text = Player2.Gender == 0 ? "Male" : "Female";
                    player2Name.Text = Player2.Name;
                    player2Bells.Text = Player2.Bells.ToString();
                    player2Debt.Text = Player2.Debt.ToString();
                    player2Savings.Text = Player2.Savings.ToString();
                    player2HeldItem.Text = Player2.Held_Item.ItemID == 0 ? "(None)" : Player2.Held_Item.Name;
                    groupBox2.Text = "Player2 - " + Player2.Last_Played_Date.Date_Time_String;
                }
                else
                {
                    player2Shirt.Enabled = false;
                    player2Background.Enabled = false;
                    player2Name.Enabled = false;
                    player2Bells.Enabled = false;
                    player2Debt.Enabled = false;
                    player2Savings.Enabled = false;
                    player2HeldItem.Enabled = false;
                    player2Face.Enabled = false;
                    player2Gender.Enabled = false;
                }

                if (Player3.Exists)
                {
                    player3Shirt.DataSource = new BindingSource(Shirts, null);
                    player3Shirt.ValueMember = "Key";
                    player3Shirt.DisplayMember = "Value";
                    player3Shirt.SelectedValue = Player3.Shirt.ItemID;
                    player3Background.DataSource = new BindingSource(Shirts, null);
                    player3Background.ValueMember = "Key";
                    player3Background.DisplayMember = "Value";
                    player3Background.SelectedValue = Player3.Inventory_Background.ItemID;
                    player3Face.DataSource = Player3.Gender == 0 ? new BindingSource(Player.Male_Faces, null) : new BindingSource(Player.Female_Faces, null);
                    player3Face.ValueMember = "Key";
                    player3Face.DisplayMember = "Value";
                    player3Face.SelectedValue = Player3.Face;
                    player3Gender.Text = Player3.Gender == 0 ? "Male" : "Female";
                    player3Name.Text = Player3.Name;
                    player3Bells.Text = Player3.Bells.ToString();
                    player3Debt.Text = Player3.Debt.ToString();
                    player3Savings.Text = Player3.Savings.ToString();
                    player3HeldItem.Text = Player3.Held_Item.ItemID == 0 ? "(None)" : Player3.Held_Item.Name;
                    groupBox3.Text = "Player3 - " + Player3.Last_Played_Date.Date_Time_String;
                }
                else
                {
                    player3Shirt.Enabled = false;
                    player3Background.Enabled = false;
                    player3Name.Enabled = false;
                    player3Bells.Enabled = false;
                    player3Debt.Enabled = false;
                    player3Savings.Enabled = false;
                    player3HeldItem.Enabled = false;
                    player3Face.Enabled = false;
                    player3Gender.Enabled = false;
                }

                if (Player4.Exists)
                {
                    player4Shirt.DataSource = new BindingSource(Shirts, null);
                    player4Shirt.ValueMember = "Key";
                    player4Shirt.DisplayMember = "Value";
                    player4Shirt.SelectedValue = Player4.Shirt.ItemID;
                    player4Background.DataSource = new BindingSource(Shirts, null);
                    player4Background.ValueMember = "Key";
                    player4Background.DisplayMember = "Value";
                    player4Face.DataSource = Player4.Gender == 0 ? new BindingSource(Player.Male_Faces, null) : new BindingSource(Player.Female_Faces, null);
                    player4Face.ValueMember = "Key";
                    player4Face.DisplayMember = "Value";
                    player4Face.SelectedValue = Player4.Face;
                    player4Gender.Text = Player4.Gender == 0 ? "Male" : "Female";
                    player4Background.SelectedValue = Player4.Inventory_Background.ItemID;
                    player4Name.Text = Player4.Name;
                    player4Bells.Text = Player4.Bells.ToString();
                    player4Debt.Text = Player4.Debt.ToString();
                    player4Savings.Text = Player4.Savings.ToString();
                    player4HeldItem.Text = Player4.Held_Item.ItemID == 0 ? "(None)" : Player4.Held_Item.Name;
                    groupBox4.Text = "Player4 - " + Player4.Last_Played_Date.Date_Time_String;
                }
                else
                {
                    player4Shirt.Enabled = false;
                    player4Background.Enabled = false;
                    player4Name.Enabled = false;
                    player4Bells.Enabled = false;
                    player4Debt.Enabled = false;
                    player4Savings.Enabled = false;
                    player4HeldItem.Enabled = false;
                    player4Face.Enabled = false;
                    player4Gender.Enabled = false;
                }

                reader.Close();
                writer.Close();
                fs.Close();
                if (!Checksum.Verify(saveBuffer))
                {
                    DialogResult r = MessageBox.Show("The file's Checksum is invalid. Would you like it to be fixed?", "Invalid File Checksum", MessageBoxButtons.YesNo);
                    if (r == DialogResult.Yes)
                        SaveData();
                }
                Players[0] = Player1;
                Players[1] = Player2;
                Players[2] = Player3;
                Players[3] = Player4;
                Faces[0] = player1Face;
                Faces[1] = player2Face;
                Faces[2] = player3Face;
                Faces[3] = player4Face;
                player1Catchables.Click += new EventHandler(Catchables_Clicked);
                player2Catchables.Click += new EventHandler(Catchables_Clicked);
                player3Catchables.Click += new EventHandler(Catchables_Clicked);
                player4Catchables.Click += new EventHandler(Catchables_Clicked);
                CanSetData = true;
                if (Properties.Settings.Default.NookingtonsFlag)
                    DataConverter.WriteByte(Nookingtons_Visitor_Flag, 0x01);
                /*for (int i = 0; i < 15; i++)
                    new Messageboard_Post(0x912C + i * 0xC8);*/
                //Save Test = new Save("C:\\Users\\olsen\\Documents\\Project64 2.3\\Save\\DOUBUTSUNOMORI.fla");
                //Test.Write(0x26040, (byte)0x25, true);
                //Test.Flush();
            }
            else
            {
                reader.Close();
                writer.Close();
                fs.Close();
            }
        }

        private void townNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                string text = townNameTextBox.Text;
                if (text.Length > 0)
                {
                    DataConverter.WriteString(Town_Name_Offset, text, 8);
                    foreach (Player p in Players)
                        p.Town_Name = text.Trim();
                }
            }
        }

        private void townNameTextBox_HandleTextChanged(object sender, EventArgs e)
        {
            int maxBytes = StringUtil.StringToMaxChars(townNameTextBox.Text);
            if (townNameTextBox.Text.ToCharArray().Length > 8)
            {
                townNameTextBox.Text = townNameTextBox.Text.Substring(0, 8);
                townNameTextBox.SelectionStart = townNameTextBox.Text.Length;
                townNameTextBox.SelectionLength = 0;
            }
            if (Encoding.UTF8.GetBytes(townNameTextBox.Text.ToCharArray()).Length > maxBytes)
            {
                townNameTextBox.Text = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(townNameTextBox.Text), 0, maxBytes);
                townNameTextBox.SelectionStart = townNameTextBox.Text.Length;
                townNameTextBox.SelectionLength = 0;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                TextBox t = (TextBox)sender;
                int player = int.Parse(new string(t.Name.Where(char.IsDigit).ToArray()));
                if (t.Text.Length > 0)
                    Players[player - 1].Name = t.Text;
            }
        }

        private void textBox1_HandleTextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            int maxBytes = StringUtil.StringToMaxChars(t.Text);
            if (t.Text.ToCharArray().Length > 8)
            {
                t.Text = t.Text.Substring(0, 8);
                t.SelectionStart = t.Text.Length;
                t.SelectionLength = 0;
            }
            if (Encoding.UTF8.GetBytes(t.Text.ToCharArray()).Length > maxBytes)
            {
                t.Text = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(t.Text), 0, maxBytes);
                t.SelectionStart = t.Text.Length;
                t.SelectionLength = 0;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox senderComboBox = (TextBox)sender;
            if (CanSetData)
            {
                uint bells = senderComboBox.Text.Any(char.IsDigit) ? uint.Parse(senderComboBox.Text) : 0;
                int player = int.Parse(new string(senderComboBox.Name.Where(char.IsDigit).ToArray()));
                if (bells >= 0 && bells <= uint.MaxValue)
                    Players[player - 1].Bells = bells;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void savings_TextChanged(object sender, EventArgs e)
        {
            TextBox senderComboBox = (TextBox)sender;
            if (CanSetData)
            {
                uint savings = senderComboBox.Text.Any(char.IsDigit) ? uint.Parse(senderComboBox.Text) : 0;
                int player = int.Parse(new string(senderComboBox.Name.Where(char.IsDigit).ToArray()));
                if (savings >= 0 && savings <= uint.MaxValue)
                    Players[player - 1].Savings = savings;
            }
        }

        private void savings_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox senderComboBox = (TextBox)sender;
            if (CanSetData && player1Debt.Text.Length > 0)
            {
                int player = int.Parse(new string(senderComboBox.Name.Where(char.IsDigit).ToArray()));
                uint debt = senderComboBox.Text.Any(char.IsDigit) ? uint.Parse(senderComboBox.Text) : 0;
                if (debt >= 0 && debt <= uint.MaxValue)
                    Players[player - 1].Debt = debt;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void Catchables_Clicked(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                Button b = (Button)sender;
                int player = int.Parse(new string(b.Name.Where(char.IsDigit).ToArray()));
                if (Players[player - 1].Exists)
                    Players[player - 1].Fill_Catchables();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                Button b = (Button)sender;
                int player = int.Parse(new string(b.Name.Where(char.IsDigit).ToArray()));
                if (Players[player - 1].Exists)
                {
                    if (iEditor == null || iEditor.IsDisposed)
                        iEditor = new Inventory_Editor(Players[player - 1].Inventory);
                    iEditor.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                Button b = (Button)sender;
                int player = int.Parse(new string(b.Name.Where(char.IsDigit).ToArray()));
                if (Players[player - 1].Exists)
                {
                    if (hEditor == null || hEditor.IsDisposed)
                    {
                        Furniture[][] LayerData = new Furniture[12][];
                        for (int i = 0; i < 3; i++)
                            for (int x = 0; x < 4; x++)
                                LayerData[i * 4 + x] = Players[player - 1].House.Rooms[i].Layers[x].Furniture;
                        hEditor = new House_Editor(Players[player - 1].House, LayerData);
                    }
                    hEditor.Show();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                ushort[] acreRawData = DataConverter.ReadUShortArray(AcreData_Offset, AcreData_Size / 2);
                ushort[] islandRawData = DataConverter.ReadUShortArray(IslandData_Offset, IslandData_Length / 2);
                ushort[] acreTileData = DataConverter.ReadUShortArray(AcreTile_Offset, AcreTile_Size / 2);
                byte[] burriedItemData = DataConverter.ReadDataRaw(BurriedItems_Offset, BurriedItems_Length);
                byte[] islandBurriedItemData = DataConverter.ReadDataRaw(IslandBurriedItems_Offset, IslandBurriedItems_Length);
                if (townEditorForm == null || townEditorForm.IsDisposed)
                    townEditorForm = new TownEditor(acreRawData, islandRawData, acreTileData, burriedItemData, islandBurriedItemData);
                townEditorForm.Show();
                
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                foreach (Player p in Players)
                    if (p.Exists)
                        p.Write();
                SaveData();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                ComboBox senderComboBox = (ComboBox)sender;
                string selectedItem = senderComboBox.Text;
                int player = int.Parse(new string(senderComboBox.Name.Where(char.IsDigit).ToArray()));
                Players[player - 1].Held_Item = new Item(selectedItem == "(None)" ? (ushort)0 : ItemData.GetItemID(selectedItem));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                ushort[] acreTileData = DataConverter.ReadUShortArray(AcreTile_Offset, AcreTile_Size / 2);
                Dictionary<int, Acre> tileData = tileData = AcreData.GetAcreTileData(acreTileData);
                if (editor == null || editor.IsDisposed)
                    editor = new AcreEditor(tileData);
                editor.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                if (vEditor == null || vEditor.IsDisposed)
                    vEditor = new Villager_Editor();
                vEditor.Show();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                ComboBox c = (ComboBox)sender;
                ushort selectedShirt = 0;
                try { selectedShirt = (ushort)c.SelectedValue; }
                catch { }
                int player = -1;
                if (selectedShirt != 0)
                {
                    player = int.Parse(new string(c.Name.Where(char.IsDigit).ToArray()));
                    if (player > -1)
                        Players[player - 1].Shirt = new Item(selectedShirt);
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                ComboBox c = (ComboBox)sender;
                ushort selectedShirt = 0;
                try { selectedShirt = (ushort)c.SelectedValue; }
                catch { }
                int player = -1;
                if (selectedShirt != 0)
                {
                    player = int.Parse(new string(c.Name.Where(char.IsDigit).ToArray()));
                    if (player > -1)
                        Players[player - 1].Inventory_Background = new Item(selectedShirt);
                }
            }
        }

        private void gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                ComboBox c = (ComboBox)sender;
                int player = int.Parse(new string(c.Name.Where(char.IsDigit).ToArray()));
                Players[player - 1].Gender = c.Text == "Male" ? (byte)0 : (byte)1;
                Faces[player - 1].DataSource = c.Text == "Male" ? new BindingSource(Player.Male_Faces, null) : new BindingSource(Player.Female_Faces, null);
                Faces[player - 1].SelectedIndex = Players[player - 1].Face;
            }
        }

        private void face_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                ComboBox c = (ComboBox)sender;
                int player = int.Parse(new string(c.Name.Where(char.IsDigit).ToArray()));
                Players[player - 1].Face = (byte)c.SelectedValue;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (settingsForm == null || settingsForm.IsDisposed)
                settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (CanSetData)
                if (nEditor == null || nEditor.IsDisposed)
                {
                    nEditor = new NookEditor();
                    nEditor.Show();
                }
        }

        private void Catalog_Click(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                Button b = (Button)sender;
                int player = int.Parse(new string(b.Name.Where(char.IsDigit).ToArray()));
                if (Players[player - 1].Exists)
                    Players[player - 1].Fill_Catalog();
            }
        }
    }
}