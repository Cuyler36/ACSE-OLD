﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Resources;

namespace Animal_Crossing_GCN_Save_Editor
{
    public partial class Form1 : Form
    {

        public byte[] saveBuffer = new byte[0x26000];

        public static int Date_Offset = 946684799; //Date at 12/31/1999 @ 11:59:59PM
        public static int Data_Start_Offset = 0x26040;
        public static int Town_Name_Offset = 0x9120;
        public static int Player1_Bells_Offset = 0xAC;
        public static int Player1_Town_Name = 0x28;
        public static int Player1_Pockets = 0x88;
        public static int[] House_Addresses = new int[4] { 0x9D20, 0, 0, 0 }; //House Carpet & Wallpaper offset: 0x8A0
        public static int AcreData_Offset = 0x137A8;
        public static int AcreData_Size = 0x3C00;
        public static int AcreTile_Offset = 0x173A8;
        public static int AcreTile_Size = 0x8C;
        public static int Player1_Held_Item_Offset = 0x4C4;
        public static int Player1_Inventory_Background_Offset = 0x10A4;
        public static int Player1_Debt_Offset = 0xB0;
        public static int Player1_House_Size_Offset = 0x9D12;
        public static int Player1_Shirt_Offset = 0x10A9;
        public static int[] Player1_Dresser_Offsets = new int[3] { 0x9F6E, 0xA196, 0xA3BE }; //0x228 away from each other
        public static int VillagerData_Offset = 0x17438;
        public static int VillagerData_Size = 0x8EF8;
        public static int IslandData_Offset = 0x22554;
        public static int IslandData_Length = 0x400;
        public static int Islander_Offset = 0x23440;
        public static int BurriedItems_Offset = 0x20F1D;  //(Each byte is 8 spaces. Stored in binary format (02) = 0000 00x0 (reversed)
        public static int BurriedItems_Length = 0x3C0;
        public static int IslandBurriedItems_Offset = 0x23DC9;
        public static int IslandBurriedItems_Length = 0x40;
        public static int Nook_Items_Offset = 0x2040E;
        //public static byte[] Blank_Villager = Properties.Resources.blank_Villager;

        static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private FileStream fs;
        private BinaryReader reader;
        private BinaryWriter writer;
        private string fileName;
        private TownEditor townEditorForm;
        private Inventory inventory;
        private AcreEditor editor;
        private Villager_Editor vEditor;
        private Inventory_Editor iEditor;
        private House_Editor hEditor;
        List<KeyValuePair<ushort, string>> Shirts = new List<KeyValuePair<ushort, string>>();
        private bool CanSetData = false;
        private ACString TownName;
        private Player[] Players = new Player[4];
        private ComboBox[] Faces = new ComboBox[4];
        private string patternSaveLoc;
        private Bitmap currentPattern;
        //Temp
        private PictureBox[] Player1Patterns = new PictureBox[8];

        public static DateTime DateFromTimestamp(long timestamp)
        {
            return _unixEpoch.AddSeconds(timestamp);
        }

        private void SaveData()
        {
            Checksum.Update(saveBuffer);
            fs = new FileStream(fileName, FileMode.Open);
            writer = new BinaryWriter(fs);
            writer.Seek(Data_Start_Offset, SeekOrigin.Begin);
            writer.Write(saveBuffer);
            writer.Close();
            fs.Close();
        }

        public void WriteData(int offset, byte[] data)
        {
            Array.Reverse(data);
            data.CopyTo(saveBuffer, offset);
        }

        public byte[] ReadData(int offset, int size)
        {
            byte[] data = new byte[size];
            for (int i = 0; i < size; i++)
                data[i] = saveBuffer[offset + i];
            Array.Reverse(data);
            return data;
        }

        public ushort[] ReadUShort(int offset, int size)
        {
            ushort[] data = new ushort[size];
            byte[] byteData = ReadData(offset, size);
            for (int i = 0; i < byteData.Length; i += 2)
            {
                ushort item = BitConverter.ToUInt16(byteData, i);
                data[i / 2] = item;
            }
            return data;
        }

        public ushort[] ReadRawUShort(int offset, int size)
        {
            ushort[] data = new ushort[(int)Math.Ceiling((decimal)size / 2)];
            byte[] rawData = ReadDataRaw(offset, size);
            for (int i = 0; i < rawData.Length; i += 2)
            {
                byte[] udata = new byte[2] { rawData[i], rawData[i + 1] };
                Array.Reverse(udata);
                data[i / 2] = BitConverter.ToUInt16(udata, 0);
            }
            return data;
        }

        public void WriteUShort(ushort[] buffer, int offset)
        {
            for (int i = 0; i < buffer.Length; i ++)
            {
                byte[] ushortBytes = BitConverter.GetBytes(buffer[i]);
                Array.Reverse(ushortBytes);
                ushortBytes.CopyTo(saveBuffer, offset + i * 2);
            }
        }

        private byte[] GetSaveData(int offset, int size)
        {
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            return reader.ReadBytes(size);
        }

        public byte[] ReadDataRaw(int offset, int size)
        {
            byte[] data = new byte[size];
            for (int i = 0; i < size; i++)
                data[i] = saveBuffer[offset + i];
            return data;
        }

        public void WriteDataRaw(int offset, byte[] buffer)
        {
            buffer.CopyTo(saveBuffer, offset);
        }

        public ACString ReadString(int offset, int maxSize)
        {
            byte[] data = new byte[maxSize];
            for (int i = 0; i < maxSize; i++)
                data[i] = saveBuffer[offset + i];
            return new ACString(data);
        }

        public void WriteString(int offset, string str, int maxSize)
        {
            byte[] strBytes = new byte[maxSize];
            byte[] ACStringBytes = ACString.GetBytes(str, maxSize);
            if (ACStringBytes.Length <= maxSize)
            {
                ACStringBytes.CopyTo(strBytes, 0);
                if (str.Length < maxSize)
                    for (int i = (str.Length); i <= maxSize - 1; i++)
                        strBytes[i] = 0x20;
                strBytes.CopyTo(saveBuffer, offset);
            }
        }

        private void SetBells(uint amount)
        {
            WriteData(Player1_Bells_Offset, BitConverter.GetBytes(amount));
        }

        private string[] GetNames(int offset, int count)
        {
            string[] names = new string[count];
            for (int i = 0; i < count; i++)
            {
                ushort itemID = BitConverter.ToUInt16(ReadData(offset + (i * 2), 2), 0);
                names[i] = ItemData.GetItemName(itemID);
            }
            return names;
        }

        public Form1()
        {
            InitializeComponent();
            ItemData.AddVillagerHouses();
            ItemData.SetupItemDictionary();
            VillagerData.CreateVillagerDatabase();
            for (int i = 0; i < ItemData.Shirt_IDs.Length; i++)
                Shirts.Add(new KeyValuePair<ushort, string>(ItemData.Shirt_IDs[i], ItemData.Shirt_Names[i]));
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

        private void saveFileDialog1_OK (object sender, CancelEventArgs e)
        {
            patternSaveLoc = ((SaveFileDialog)sender).FileName;
            if (currentPattern != null)
                currentPattern.Save(patternSaveLoc, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private void addPatternBoxes(Player p, Control playerBox)
        {
            for (int i = 0; i < 8; i++)
            {
                Pattern pattern = p.Patterns[i];
                PictureBox b = new PictureBox();
                b.Name = pattern.Name; //string.Format("player{0}Pattern{1}", p.Index, i);
                b.Image = pattern.Pattern_Bitmap;
                b.Size = new Size(32, 32);
                b.Location = new Point(playerBox.Location.X + 19 + 38 * (i % 4), playerBox.Location.Y + playerBox.Size.Height + 10 + 38 * (i / 4));
                ToolTip t = new ToolTip();
                t.SetToolTip(b, pattern.Name);
                t.ShowAlways = true;
                ContextMenu cm = new ContextMenu();
                MenuItem rename = new MenuItem("Rename");
                MenuItem palette = new MenuItem("Set Palette");
                MenuItem import = new MenuItem("Import (WIP)");
                MenuItem export = new MenuItem("Export");
                cm.MenuItems.Add(rename);
                cm.MenuItems.Add(palette);
                cm.MenuItems.Add(import);
                cm.MenuItems.Add(export);
                b.ContextMenu = cm;
                export.Click += exportClick;
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
            string Game_ID = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(3));
            if (Game_ID == "GAF")
            {
                this.Text = "ACSE - " + Path.GetFileName(fileName);
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
                TownName = ReadString(Town_Name_Offset, 0x8);
                Player Player1 = new Player(0, this);
                Player Player2 = new Player(1, this);
                Player Player3 = new Player(2, this);
                Player Player4 = new Player(3, this);

                List<PictureBox> temp = new List<PictureBox>();
                foreach (PictureBox p in this.Controls.OfType<PictureBox>())
                    temp.Add(p);
                foreach (PictureBox p in temp)
                {
                    Bitmap i = (Bitmap)p.Image;
                    p.Image = null;
                    this.Controls.Remove(p);
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

                townNameTextBox.Text = TownName.Trim();

                player1Name.Text = Player1.Name.Trim();
                player1Bells.Text = Player1.Bells.ToString();
                try { player1Debt.Text = Player1.Debt.ToString(); } catch { } //This line throws a null exception for some reason????
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
                player2HeldItem.Text = Player2.Held_Item.ItemID == 0 ? "(None)" : Player2.Held_Item.Name;
                if (!Player2.Exists)
                {
                    player2Shirt.Enabled = false;
                    player2Background.Enabled = false;
                    player2Name.Enabled = false;
                    player2Bells.Enabled = false;
                    player2Debt.Enabled = false;
                    player2HeldItem.Enabled = false;
                    player2Face.Enabled = false;
                    player2Gender.Enabled = false;
                }

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
                player3HeldItem.Text = Player3.Held_Item.ItemID == 0 ? "(None)" : Player3.Held_Item.Name;
                if (!Player3.Exists)
                {
                    player3Shirt.Enabled = false;
                    player3Background.Enabled = false;
                    player3Name.Enabled = false;
                    player3Bells.Enabled = false;
                    player3Debt.Enabled = false;
                    player3HeldItem.Enabled = false;
                    player3Face.Enabled = false;
                    player3Gender.Enabled = false;
                }

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
                player4HeldItem.Text = Player4.Held_Item.ItemID == 0 ? "(None)" : Player4.Held_Item.Name;
                if (!Player4.Exists)
                {
                    player4Shirt.Enabled = false;
                    player4Background.Enabled = false;
                    player4Name.Enabled = false;
                    player4Bells.Enabled = false;
                    player4Debt.Enabled = false;
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
                inventory = new Inventory(ReadRawUShort(Player1_Pockets, 0x1E));
                Players[0] = Player1;
                Players[1] = Player2;
                Players[2] = Player3;
                Players[3] = Player4;
                Faces[0] = player1Face;
                Faces[1] = player2Face;
                Faces[2] = player3Face;
                Faces[3] = player4Face;
                CanSetData = true;
                //MessageBox.Show(ReadString(0x95DC, 0xC0).String); //0x96A4
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
            if (fs != null)
            {
                string text = townNameTextBox.Text;
                if (text.Length > 0)
                {
                    WriteString(Town_Name_Offset, text, 8);
                    foreach (Player p in Players)
                        p.Town_Name = text.Trim();
                }
                //Pattern testPattern = new Pattern(0x18C0, this); //0x1260
                //pictureBox1.Image = testPattern.Pattern_Bitmap;
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
            if (fs != null)
            {
                TextBox t = (TextBox)sender;
                int player = int.Parse(new string(t.Name.Where(Char.IsDigit).ToArray()));
                if (t.Text.Length > 0)
                    Players[player - 1].Name = t.Text;
                    //WriteString(0x20, text, 8);
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
            if (fs != null && player1Debt.Text.Length > 0)
            {
                uint bells = string.IsNullOrEmpty(player1Bells.Text) ? 0 : uint.Parse(player1Bells.Text);
                int player = int.Parse(new string(senderComboBox.Name.Where(Char.IsDigit).ToArray()));
                if (bells >= 0 && bells <= uint.MaxValue)
                    Players[player - 1].Bells = bells;
                //SetBells(bells);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox senderComboBox = (TextBox)sender;
            if (fs != null && player1Debt.Text.Length > 0)
            {
                int player = int.Parse(new string(senderComboBox.Name.Where(Char.IsDigit).ToArray()));
                uint debt = string.IsNullOrEmpty(senderComboBox.Text) ? 0 : uint.Parse(senderComboBox.Text);
                if (debt >= 0 && debt <= uint.MaxValue)
                    Players[player - 1].Debt = debt;
                    //WriteData(Player1_Debt_Offset, BitConverter.GetBytes(debt));
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fs != null)
            {
                Button b = (Button)sender;
                int player = int.Parse(new string(b.Name.Where(Char.IsDigit).ToArray()));
                if (Players[player - 1].Exists)
                {
                    if (iEditor == null || iEditor.IsDisposed)
                    {
                        /*Item[] dresserItems = new Item[3];
                        for (int i = 0; i < 3; i++)
                            dresserItems[i] = new Item(ReadRawUShort(Player1_Dresser_Offsets[i], 2)[0]);*/
                        iEditor = new Inventory_Editor(Players[player - 1].Inventory, this);
                    }
                    iEditor.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fs != null)
            {
                Button b = (Button)sender;
                int player = int.Parse(new string(b.Name.Where(Char.IsDigit).ToArray()));
                if (Players[player - 1].Exists)
                {
                    int firstFloorSize = HouseData.GetHouseSize(ReadRawUShort(Players[player - 1].House_Data_Offset, 0x114));
                    ushort[] firstFloorLayer1 = ReadRawUShort(Players[player - 1].House_Data_Offset, HouseData.House_Data_Sizes[firstFloorSize - 1]);
                    ushort[] firstFloorLayer2 = ReadRawUShort(Players[player - 1].House_Data_Offset + 0x24A, HouseData.House_Data_Layer2_Sizes[firstFloorSize - 1]);
                    ushort[] secondFloorLayer1 = ReadRawUShort(Players[player - 1].House_Data_Offset + 0x8A8, 0xF0);
                    ushort[] secondFloorLayer2 = ReadRawUShort(Players[player - 1].House_Data_Offset + 0xAF2, 0xAC);
                    ushort[] basementLayer1 = ReadRawUShort(Players[player - 1].House_Data_Offset + 0x1150, 0x114);
                    ushort[] basementLayer2 = ReadRawUShort(Players[player - 1].House_Data_Offset + 0x139A, 0xF0);

                    if (hEditor == null || hEditor.IsDisposed)
                        hEditor = new House_Editor(new List<ushort[]>() { firstFloorLayer1, firstFloorLayer2, secondFloorLayer1, secondFloorLayer2, basementLayer1, basementLayer2 }, Players[player - 1].House_Data_Offset, this);
                    hEditor.Show();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fs != null)
            {
                ushort[] acreRawData = ReadRawUShort(AcreData_Offset, AcreData_Size);
                ushort[] islandRawData = ReadRawUShort(IslandData_Offset, IslandData_Length);
                ushort[] acreTileData = ReadRawUShort(AcreTile_Offset, AcreTile_Size);
                byte[] burriedItemData = ReadDataRaw(BurriedItems_Offset, BurriedItems_Length);
                byte[] islandBurriedItemData = ReadDataRaw(IslandBurriedItems_Offset, IslandBurriedItems_Length);
                if (townEditorForm == null || townEditorForm.IsDisposed)
                    townEditorForm = new TownEditor(acreRawData, islandRawData, acreTileData, burriedItemData, islandBurriedItemData, this);
                townEditorForm.Show();
                
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fs != null)
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
                int player = int.Parse(new string(senderComboBox.Name.Where(Char.IsDigit).ToArray()));
                Players[player - 1].Held_Item = new Item(selectedItem == "(None)" ? (ushort)0 : ItemData.GetItemID(selectedItem));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (fs != null)
            {
                ushort[] acreTileData = ReadRawUShort(AcreTile_Offset, AcreTile_Size);
                Dictionary<int, Acre> tileData = AcreData.GetAcreTileData(acreTileData);
                if (editor == null || editor.IsDisposed)
                    editor = new AcreEditor(this);
                editor.currentAcreData = tileData;
                editor.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (saveBuffer != null)
            {
                byte[] villagerData = ReadDataRaw(VillagerData_Offset, VillagerData_Size);
                byte[] islanderData = ReadDataRaw(Islander_Offset, VillagerData_Size / 15);
                Villager[] Villagers = new Villager[16];
                for (int i = 0; i < 15; i++)
                    Villagers[i] = new Villager(BitConverter.ToUInt16(new byte[2] { villagerData[(i * 0x988) + 1], villagerData[i * 0x988] }, 0), null, i + 1, villagerData[(i * 0x988) + 0xD], ReadString(VillagerData_Offset + (i * 0x988) + 0x89D, 10).Trim());
                Villagers[15] = new Villager(BitConverter.ToUInt16(new byte[2] { islanderData[1], islanderData[0] }, 0), null, 16, islanderData[0xD], ReadString(Islander_Offset + 0x89D, 10).Trim()); //0x89D + 0x3D = Villager Shirt
                if (vEditor == null || vEditor.IsDisposed)
                    vEditor = new Villager_Editor(Villagers, this);
                vEditor.Show();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (saveBuffer != null && CanSetData)
            {
                ComboBox c = (ComboBox)sender;
                ushort selectedShirt = 0;
                try { selectedShirt = (ushort)c.SelectedValue; }
                catch { }
                int player = -1;
                if (selectedShirt != 0)
                {
                    player = int.Parse(new string(c.Name.Where(Char.IsDigit).ToArray()));
                    if (player > -1)
                        Players[player - 1].Shirt = new Item(selectedShirt);
                }
                    //WriteDataRaw(Player1_Shirt_Offset, new byte[3] { (byte)(selectedShirt & 0xFF), 0x24, (byte)(selectedShirt & 0xFF) });
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (saveBuffer != null && CanSetData)
            {
                ComboBox c = (ComboBox)sender;
                ushort selectedShirt = 0;
                try { selectedShirt = (ushort)c.SelectedValue; }
                catch { }
                int player = -1;
                if (selectedShirt != 0)
                {
                    player = int.Parse(new string(c.Name.Where(Char.IsDigit).ToArray()));
                    if (player > -1)
                        Players[player - 1].Inventory_Background = new Item(selectedShirt);
                }
                //WriteUShort(new ushort[1] { selectedShirt }, Player1_Inventory_Background_Offset);
            }
        }

        private void gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                ComboBox c = (ComboBox)sender;
                int player = int.Parse(new string(c.Name.Where(Char.IsDigit).ToArray()));
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
                int player = int.Parse(new string(c.Name.Where(Char.IsDigit).ToArray()));
                Players[player - 1].Face = (byte)c.SelectedValue;
            }
        }
    }
}
