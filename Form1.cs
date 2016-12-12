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
        public byte[] SaveBuffer { get { return saveBuffer; } set { saveBuffer = value; } }

        public static int[] Player1_Pockets = new int[15] {
            0x88, 0x8A, 0x8C, 0x8E, 0x90,
            0x92, 0x94, 0x96, 0x98, 0x9A,
            0x9C, 0x9E, 0xA0, 0xA2, 0xA4
        };

        //ResourceManager rm = new ResourceManager("Animal_Crossing_GCN_Save_Editor.")

        public static int Date_Offset = 946684799; //Date at 12/31/1999 @ 11:59:59PM
        public static int Data_Start_Offset = 0x26040;
        public static int Town_Name_Offset = 0x9120;
        public static int Player1_Bells_Offset = 0xAC;
        public static int Player1_Town_Name = 0x28;
        static public int[] House_Addresses = new int[4] { 0x9D20, 0, 0, 0 };
        static public int House1_DataSize = 0x90;
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
        public static int IslandData_Offset = 0x22550;
        public static int IslandData_Length = 0x400;
        public static int Islander_Offset = 0x23440;
        public static int BurriedItems_Offset = 0x20F1D; //Actual Start: 0x46F5C (Each byte is 8 spaces. Stored in binary format (02) = 0000 00x0 (reversed)
        public static int BurriedItems_Length = 0x3C0; //Actual Size: 0x3C0
        //public static byte[] Blank_Villager = Properties.Resources.blank_Villager;
        
        static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private FileStream fs;
        private BinaryReader reader;
        private BinaryWriter writer;
        private string fileName;
        private ItemData itemData = new ItemData { };
        private HouseData houseData = new HouseData { };
        private AcreData acreData = new AcreData { };
        private TownEditor townEditorForm; // = new TownEditor { };
        private Inventory inventory;
        private AcreEditor editor;
        private Villager_Editor vEditor;
        private Inventory_Editor iEditor;
        List<KeyValuePair<ushort, string>> Shirts = new List<KeyValuePair<ushort, string>>();
        private bool CanSetData = false;

        public static DateTime DateFromTimestamp(long timestamp)
        {
            return _unixEpoch.AddSeconds(timestamp);
        }

        private void SaveData()
        {
            Checksum.Update(saveBuffer).CopyTo(saveBuffer, 0x12);
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

        private void ModifyData(int offset, byte[] data)
        {
            WriteData(offset, data);
        }

        private void ModifyString(int offset, byte[] data)
        {
            data.CopyTo(saveBuffer, offset);
        }

        private byte[] ReadData(int offset, int size)
        {
            byte[] data = new byte[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = saveBuffer[offset + i];
            }
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
            {
                data[i] = saveBuffer[offset + i];
            }
            return data;
        }

        public void WriteDataRaw(int offset, byte[] buffer)
        {
            buffer.CopyTo(saveBuffer, offset);
        }

        private string ReadString(int offset, int maxSize)
        {
            byte[] data = new byte[maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                data[i] = saveBuffer[offset + i];
            }
            return System.Text.Encoding.ASCII.GetString(data).Trim();
        }

        public void WriteString(int offset, string str, int maxSize)
        {
            if (str.Length <= maxSize)
            {
                byte[] strBytes = new byte[maxSize];
                Encoding.ASCII.GetBytes(str).CopyTo(strBytes, 0);
                if (str.Length < maxSize)
                {
                    for (int i = (str.Length); i <= maxSize - 1; i++)
                    {
                        strBytes[i] = 0x20;
                    }
                }
                ModifyString(offset, strBytes);
            }
        }

        public class ACString
        {
            Dictionary<byte, string> CharacterDictionary = new Dictionary<byte, string>()
            {
                {0x90, "–" },
                {0xCD, "\n" },
            };
            byte[] String_Bytes;
            string String = "";

            public ACString(byte[] stringBuffer)
            {
                String_Bytes = stringBuffer;
                foreach (byte b in stringBuffer)
                {
                    if (CharacterDictionary.ContainsKey(b))
                        String += CharacterDictionary.FirstOrDefault(o => o.Key == b).Value;
                    else
                        String += Encoding.ASCII.GetString(new byte[1] { b });
                }
            }

            public byte[] GetBytes()
            {
                byte[] stringBytes = Encoding.ASCII.GetBytes(String);
                for (int i = 0; i < String.Length; i++)
                    if (CharacterDictionary.ContainsValue(String[i].ToString()))
                        stringBytes[i] = CharacterDictionary.FirstOrDefault(o => o.Value == String[i].ToString()).Key;
                return stringBytes;
            }
        }

        private void SetBells(uint amount)
        {
            ModifyData(Player1_Bells_Offset, BitConverter.GetBytes(amount));
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
                townNameTextBox.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                GetSaveData(0x26040, 0x26000).CopyTo(saveBuffer, 0);
                townNameTextBox.Text = ReadString(Town_Name_Offset, 0x8);
                textBox1.Text = ReadString(0x20, 0x8);
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
                editor = new AcreEditor(this);
                reader.Close();
                writer.Close();
                fs.Close();
                if (!Checksum.Verify(saveBuffer))
                {
                    MessageBox.Show("The file's Checksum was invalid. The Checksum will be updated.");
                    SaveData();
                }
                //townEditorForm.Show();
                inventory = new Inventory(ReadRawUShort(Player1_Pockets[0], 0x1E));
                CanSetData = true;
                /*foreach (InventorySlot i in inventory.InventorySlots)
                {
                    MessageBox.Show(i.Item.ItemID.ToString("X") + " | " + i.Item.BaseItemID.ToString("X") + " | " + i.Item.Name);
                }*/
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
                if (text.Length > 0 && text.Length < 9)
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
                string text = textBox1.Text;
                if (text.Length > 0 && text.Length < 9)
                {
                    WriteString(0x20, text, 8);
                    //MessageBox.Show(ReadString(0x20, 8));
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (fs != null && textBox3.Text.Length > 0)
            {
                uint bells = string.IsNullOrEmpty(textBox2.Text) ? 0 : uint.Parse(textBox2.Text);
                if (bells >= 0 && bells < 100000)
                {
                    SetBells(bells);
                }
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
                //string[] pocketItemNames = GetNames(Player1_Pockets[0], 15);
                //for (int i = 0; i <= pocketItemNames.Length - 1; i++)
                //{
                //    MessageBox.Show("Pocket #" + (i + 1).ToString() + " Item: " + pocketItemNames[i]);
                //}
                if (iEditor == null || iEditor.IsDisposed)
                {
                    Item[] dresserItems = new Item[3];
                    for (int i = 0; i < 3; i++)
                        dresserItems[i] = new Item(ReadRawUShort(Player1_Dresser_Offsets[i], 2)[0]);
                    iEditor = new Inventory_Editor(ReadRawUShort(Player1_Pockets[0], 30), dresserItems, this);
                }
                iEditor.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fs != null)
            {
                ushort[] data = ReadRawUShort(House_Addresses[0], House1_DataSize);
                Dictionary<int, string> houseInfo = houseData.GetHouseData(data, 8);
                int i = -1;
                foreach (KeyValuePair<int, string> entry in houseInfo)
                {
                    i++;
                    MessageBox.Show("Item: " + entry.Value + " | X: " + ((i % 4) + 1).ToString() + " Y: " + (Math.Floor((decimal)i / 4) + 1));
                }
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
                if (townEditorForm == null || townEditorForm.IsDisposed)
                    townEditorForm = new TownEditor(acreRawData, islandRawData, acreTileData, burriedItemData, this);
                townEditorForm.Show();
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (fs != null)
            {
                ushort[] acreInfo = ReadRawUShort(AcreData_Offset, AcreData_Size);
                ushort[] newAcreData = acreData.ClearWeeds(acreInfo);
                if (acreInfo.Length == newAcreData.Length)
                    WriteUShort(newAcreData, AcreData_Offset);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fs != null)
                SaveData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (fs != null)
            {
                ushort[] acreInfo = ReadRawUShort(AcreData_Offset, AcreData_Size);
                ushort[] clearedAcreInfo = acreData.ClearTown(acreInfo);
                if (acreInfo.Length == clearedAcreInfo.Length)
                    WriteUShort(clearedAcreInfo, AcreData_Offset);
            }
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
                    ushort item = itemData.GetItemID(selectedItem);
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
                Dictionary<int, Acre> tileData = acreData.GetAcreTileData(acreTileData);
                editor.currentAcreData = tileData;
                editor.Show();
                /*foreach (KeyValuePair<int, Acre> acre in tileData)
                {
                    MessageBox.Show("AcreID: " + acre.Value.AcreID.ToString("X") + " | Name: " + acre.Value.Name);
                }*/
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
                {
                    Villagers[i] = new Villager(BitConverter.ToUInt16(new byte[2] { villagerData[(i * 0x988) + 1], villagerData[i * 0x988] }, 0), null, i + 1, villagerData[(i * 0x988) + 0xD],
                        ReadString(VillagerData_Offset + (i * 0x988) + 0x89D, 10));
                    //MessageBox.Show("Villager: " + Villagers[i].Name + " | Personality: " + Villagers[i].Personality + " | Index: " + Villagers[i].Index);
                }
                Villagers[15] = new Villager(BitConverter.ToUInt16(new byte[2] { islanderData[1], islanderData[0] }, 0), null, 16, islanderData[0xD], ReadString(Islander_Offset + 0x89D, 10));
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

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
