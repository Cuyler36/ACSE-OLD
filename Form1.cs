using System;
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
        private ACString Player1Name;

        public static DateTime DateFromTimestamp(long timestamp)
        {
            return _unixEpoch.AddSeconds(timestamp);
        }

        private void SaveData()
        {
            Checksum.Update(saveBuffer);
            fs = new FileStream(fileName, FileMode.Open);
            writer = new BinaryWriter(fs);
            writer.Seek(0x26040, SeekOrigin.Begin);
            writer.Write(saveBuffer);
            writer.Close();
            fs.Close();
        }

        private void WriteData(int offset, byte[] data)
        {
            Array.Reverse(data);
            data.CopyTo(saveBuffer, offset);
        }

        private byte[] ReadData(int offset, int size)
        {
            byte[] data = new byte[size];
            for (int i = 0; i < size; i++)
                data[i] = saveBuffer[offset + i];
            Array.Reverse(data);
            return data;
        }

        private ushort[] ReadUShort(int offset, int size)
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

        private ACString ReadString(int offset, int maxSize)
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
            fs = new FileStream(fileName, FileMode.Open);
            reader = new BinaryReader(fs);
            writer = new BinaryWriter(fs);
            string Game_ID = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(3));
            if (Game_ID == "GAF")
            {
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
                townNameTextBox.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                GetSaveData(0x26040, 0x26000).CopyTo(saveBuffer, 0);
                TownName = ReadString(Town_Name_Offset, 0x8);
                Player1Name = ReadString(0x20, 0x8);
                townNameTextBox.Text = TownName.Trim();
                textBox1.Text = Player1Name.Trim();
                textBox2.Text = BitConverter.ToUInt32(ReadData(Player1_Bells_Offset, 4), 0).ToString();
                textBox3.Text = BitConverter.ToUInt32(ReadData(Player1_Debt_Offset, 4), 0).ToString();
                string heldItemName = ItemData.GetItemName(ReadRawUShort(Player1_Held_Item_Offset, 2)[0]);
                comboBox1.Text = string.IsNullOrEmpty(heldItemName) ? "(None)" : heldItemName;
                comboBox2.DataSource = new BindingSource(Shirts, null);
                comboBox2.ValueMember = "Key";
                comboBox2.DisplayMember = "Value";
                comboBox2.SelectedValue = ReadRawUShort(Player1_Shirt_Offset + 1, 2)[0];
                comboBox3.DataSource = new BindingSource(Shirts, null);
                comboBox3.ValueMember = "Key";
                comboBox3.DisplayMember = "Value";
                comboBox3.SelectedValue = ReadRawUShort(Player1_Inventory_Background_Offset, 2)[0];
                reader.Close();
                writer.Close();
                fs.Close();
                if (!Checksum.Verify(saveBuffer))
                {
                    MessageBox.Show("The file's Checksum was invalid. The Checksum will be updated.");
                    SaveData();
                }
                inventory = new Inventory(ReadRawUShort(Player1_Pockets, 0x1E));
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
                int maxBytes = StringUtil.StringToMaxChars(townNameTextBox.Text);
                if (Encoding.UTF8.GetBytes(townNameTextBox.Text.ToCharArray()).Length > maxBytes)
                    townNameTextBox.Text = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(textBox1.Text), 0, maxBytes);

                string text = townNameTextBox.Text;
                if (text.Length > 0)
                {
                    WriteString(Town_Name_Offset, text, 8);
                    WriteString(Player1_Town_Name, text, 8);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (fs != null)
            {
                int maxBytes = StringUtil.StringToMaxChars(textBox1.Text);
                if (Encoding.UTF8.GetBytes(textBox1.Text.ToCharArray()).Length > maxBytes)
                    textBox1.Text = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(textBox1.Text), 0, maxBytes);
                
                string text = textBox1.Text;
                if (text.Length > 0)
                    WriteString(0x20, text, 8);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (fs != null && textBox3.Text.Length > 0)
            {
                uint bells = string.IsNullOrEmpty(textBox2.Text) ? 0 : uint.Parse(textBox2.Text);
                if (bells >= 0 && bells <= uint.MaxValue)
                    SetBells(bells);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (fs != null && textBox3.Text.Length > 0)
            {
                uint debt = string.IsNullOrEmpty(textBox3.Text) ? 0 : uint.Parse(textBox3.Text);
                if (debt >= 0 && debt <= uint.MaxValue)
                    WriteData(Player1_Debt_Offset, BitConverter.GetBytes(debt));
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
                if (iEditor == null || iEditor.IsDisposed)
                {
                    Item[] dresserItems = new Item[3];
                    for (int i = 0; i < 3; i++)
                        dresserItems[i] = new Item(ReadRawUShort(Player1_Dresser_Offsets[i], 2)[0]);
                    iEditor = new Inventory_Editor(ReadRawUShort(Player1_Pockets, 30), dresserItems, this);
                }
                iEditor.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fs != null)
            {
                int firstFloorSize = HouseData.GetHouseSize(ReadRawUShort(House_Addresses[0], 0x114));
                ushort[] firstFloorLayer1 = ReadRawUShort(House_Addresses[0], HouseData.House_Data_Sizes[firstFloorSize - 1]);
                ushort[] firstFloorLayer2 = ReadRawUShort(House_Addresses[0] + 0x228, HouseData.House_Data_Sizes[firstFloorSize - 1]);
                ushort[] secondFloorLayer1 = ReadRawUShort(House_Addresses[0] + 0x8A8, 0xF0);
                ushort[] secondFloorLayer2 = new ushort[0xF0]; //Temp
                ushort[] basementLayer1 = ReadRawUShort(House_Addresses[0] + 0x1150, 0x114);
                ushort[] basementLayer2 = new ushort[0x114];

                if (hEditor == null || hEditor.IsDisposed)
                    hEditor = new House_Editor(new List<ushort[]>() { firstFloorLayer1, firstFloorLayer2, secondFloorLayer1, secondFloorLayer2, basementLayer1, basementLayer2 }, this);
                hEditor.Show();
            }
        }

        private static string[] Acre_Names = new string[6]
        {
            "A", "B", "C", "D", "E", "F"
        };

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
                SaveData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanSetData)
            {
                ComboBox senderComboBox = (ComboBox)sender;
                string selectedItem = senderComboBox.Text;
                if (selectedItem == "(None)")
                    WriteUShort(new ushort[1] { 0 }, Player1_Held_Item_Offset);
                else
                {
                    ushort item = ItemData.GetItemID(selectedItem);
                    if (item != 0)
                        WriteUShort(new ushort[1] { item }, Player1_Held_Item_Offset);
                }
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
                ushort selectedShirt = 0;
                try { selectedShirt = (ushort)comboBox2.SelectedValue; }
                catch { }
                if (selectedShirt != 0)
                    WriteDataRaw(Player1_Shirt_Offset, new byte[3] { (byte)(selectedShirt & 0xFF), 0x24, (byte)(selectedShirt & 0xFF) });
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (saveBuffer != null && CanSetData)
            {
                ushort selectedShirt = 0;
                try { selectedShirt = (ushort)comboBox3.SelectedValue; }
                catch { }
                if (selectedShirt != 0)
                    WriteUShort(new ushort[1] { selectedShirt }, Player1_Inventory_Background_Offset);
            }
        }
    }
}
