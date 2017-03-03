using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSE
{
    class Player
    {
        public static Dictionary<byte, string> Male_Faces = new Dictionary<byte, string>()
        {
            {0x00, "Male Eyes w/ Eyelashes" },
            {0x01, "Male Circle Eyes w/ Eyebrows" },
            {0x02, "Male Eyes w/ Purple Bags" },
            {0x03, "Male Circle Dot Eyes" },
            {0x04, "Male Large Oval Eyes" },
            {0x05, "Male Half Oval Eyes" },
            {0x06, "Male Eyes w/ Flushed Cheeks" },
            {0x07, "Male Blue Circle Eyes" },
            {0x08, "Female Full Black Eyes (Pink Hair)" },
            {0x09, "Female Black Squinty Eyes (Purple Hair)" },
            {0x0A, "Female Eyes w/ Flushed Cheeks (Brown Hair)" },
            {0x0B, "Female Eyes w/ Bags (Blue Hair)" },
            {0x0C, "Female Oval Eyes w/ Eyelashes (Pink Hair)" },
            {0x0D, "Female Half Oval Eyes (Ginger Hair)" },
            {0x0E, "Female Blue Eyes (Dark Blue Hair)" },
            {0x0F, "Female Circle Eyes /w Eyelashes (Red Hair)" },
            {0x10, "Male Bee Sting (1)" },
            {0x11, "Male Bee Sting (2)" },
            {0x12, "Male Bee Sting (3)" },
            {0x13, "Male Bee Sting (4)" },
            {0x14, "Male Bee Sting (5)" },
            {0x15, "Male Bee Sting (6)" },
            {0x16, "Male Bee Sting (7)" },
            {0x17, "Male Bee Sting (8)" },
            {0x18, "Female Bee Sting (1)" },
            {0x19, "Female Bee Sting (2)" },
            {0x1A, "Female Bee Sting (3)" },
            {0x1B, "Female Bee Sting (4)" },
            {0x1C, "Female Bee Sting (5)" },
            {0x1D, "Female Bee Sting (6)" },
            {0x1E, "Female Bee Sting (7)" },
            {0x1F, "Female Bee Sting (8)" },
            {0x20, "Lloyd Face" }
        };

        public static Dictionary<byte, string> Female_Faces = new Dictionary<byte, string>() //Can switch these to simple arrays, if wanted.
        {
            {0x00, "Female Full Black Eyes (Pink Hair)" },
            {0x01, "Female Black Squinty Eyes (Purple Hair)" },
            {0x02, "Female Eyes w/ Flushed Cheeks (Brown Hair)" },
            {0x03, "Female Eyes w/ Bags (Blue Hair)" },
            {0x04, "Female Oval Eyes w/ Eyelashes (Pink Hair)" },
            {0x05, "Female Half Oval Eyes (Ginger Hair)" },
            {0x06, "Female Blue Eyes (Dark Blue Hair)" },
            {0x07, "Female Circle Eyes /w Eyelashes (Red Hair)" },
            {0x08, "Male Eyes w/ Eyelashes" },
            {0x09, "Male Circle Eyes w/ Eyebrows" },
            {0x0A, "Male Eyes w/ Purple Bags" },
            {0x0B, "Male Circle Dot Eyes" },
            {0x0C, "Male Large Oval Eyes" },
            {0x0D, "Male Half Oval Eyes" },
            {0x0E, "Male Eyes w/ Flushed Cheeks" },
            {0x0F, "Male Blue Circle Eyes" },
            {0x10, "Female Bee Sting (1)" },
            {0x11, "Female Bee Sting (2)" },
            {0x12, "Female Bee Sting (3)" },
            {0x13, "Female Bee Sting (4)" },
            {0x14, "Female Bee Sting (5)" },
            {0x15, "Female Bee Sting (6)" },
            {0x16, "Female Bee Sting (7)" },
            {0x17, "Female Bee Sting (8)" },
            {0x18, "Male Bee Sting (1)" },
            {0x19, "Male Bee Sting (2)" },
            {0x1A, "Male Bee Sting (3)" },
            {0x1B, "Male Bee Sting (4)" },
            {0x1C, "Male Bee Sting (5)" },
            {0x1D, "Male Bee Sting (6)" },
            {0x1E, "Male Bee Sting (7)" },
            {0x1F, "Male Bee Sting (8)" },
            {0x20, "Lloyd Face" }
        };
        static int Player_Length = 0x2440;

        public int Index = 0;
        public string Name;
        public string Town_Name;
        public Inventory Inventory;
        public uint Bells = 0;
        public uint Debt = 0;
        public Item Held_Item;
        public Item Shirt;
        public Item Inventory_Background;
        //public Item[] Stored_Items;
        public byte Face;
        public byte Gender;
        public byte Tan;
        public uint Identifier;
        public int House_Number = 0;
        public House House;
        public uint Savings = 0;
        public byte[] Bugs_and_Fish_Caught = new byte[11]; //Contains some furntiure set as well
        public Pattern[] Patterns = new Pattern[8];
        public Mail[] Letters = new Mail[10];
        public bool Reset = false;
        public bool Exists = false;
        public ACDate Last_Played_Date;

        public Player(int idx)
        {
            Index = idx;
            Read();
        }

        //Town Identifier is: 0x30 0x??
        //Player Identifier is: 0xF0 0x??
        //Villager Model Identifier is: 0xD0 ??
        //Villager Identifier is: 0xE0 0x??
        //Can Look up resetti values, if wanted.
        //Documented ones: 0x250C | 0xAE8A | 0x85A6

        public void Read()
        {
            int offset = 0x20 + Index * Player_Length;
            Name = DataConverter.ReadString(offset + 0, 8).Trim();
            Town_Name = DataConverter.ReadString(offset + 0x8, 8).Trim();
            Identifier = DataConverter.ReadUInt(offset + 0x10); //First two are player identifier bytes. Second two bytes are town identifier bytes.
            Gender = DataConverter.ReadData(offset + 0x14, 1)[0];
            Face = DataConverter.ReadData(offset + 0x15, 1)[0];
            Inventory = new Inventory(DataConverter.ReadUShortArray(offset + 0x68, 0x1E / 2));
            Bells = DataConverter.ReadUInt(offset + 0x8C);
            Debt = DataConverter.ReadUInt(offset + 0x90);
            Held_Item = new Item(DataConverter.ReadUShort(offset + 0x4A4));
            Inventory_Background = new Item(DataConverter.ReadUShort(offset + 0x1084));
            Shirt = new Item(DataConverter.ReadUShort(offset + 0x1089 + 1)); //Research Patterns used as shirt.
            Reset = DataConverter.ReadUInt(offset + 0x10F4) > 0;
            Savings = DataConverter.ReadUInt(offset + 0x122C);
            Exists = Identifier != 0xFFFFFFFF;

            for (int i = 0; i < 8; i++)
                Patterns[i] = new Pattern(offset + 0x1240 + i * 0x220);
            if (Exists)
            {
                House_Number = GetHouse();
                House = new House(0x9CE8 + (House_Number - 1) * 0x26B0);
                Last_Played_Date = new ACDate(Exists ? DataConverter.ReadDataRaw(House.Offset + 0x2640, 8) : new byte[8]);
            }
        }

        public void WriteName()
        {
            int offset = 0x20 + Index * Player_Length;
            DataConverter.WriteString(offset, Name, 8);
            DataConverter.WriteString(0x9CF8 + (House_Number - 1) * 0x26B0 - 0x10, Name, 8); //House Name
            foreach (Villager v in MainForm.Villagers)
                if (v.Exists)
                    for (int i = 0; i < v.Villager_Player_Entries.Length; i++)
                        if (v.Villager_Player_Entries[i] != null && v.Villager_Player_Entries[i].Exists)
                            if (v.Villager_Player_Entries[i].Player_ID == Identifier)
                            {
                                v.Villager_Player_Entries[i].Player_Name = Name;
                                DataConverter.WriteString(v.Offset + 0x10 + (i * 0x138), Name, 8); //Update name in save
                            }
        }

        public void Write()
        {
            int offset = 0x20 + Index * Player_Length;
            //DataConverter.WriteString(offset + 0, Name, 8);
            WriteName();
            DataConverter.WriteString(offset + 0x8, Town_Name, 8);
            DataConverter.Write(offset + 0x14, Gender);
            DataConverter.Write(offset + 0x15, Face);
            DataConverter.Write(offset + 0x68, Inventory.GetItemIDs());
            DataConverter.Write(offset + 0x8C, Bells);
            DataConverter.Write(offset + 0x90, Debt);
            DataConverter.Write(offset + 0x4A4, Held_Item.ItemID);
            DataConverter.Write(offset + 0x1084, Inventory_Background.ItemID);
            DataConverter.WriteByteArray(offset + 0x1089, new byte[] { (byte)(Shirt.ItemID & 0xFF), 0x24, (byte)(Shirt.ItemID & 0xFF) }, false);

            if (Properties.Settings.Default.StopResetti)
                DataConverter.Write(offset + 0x10F4, 0);

            DataConverter.Write(offset + 0x122C, Savings);

            foreach (Pattern p in Patterns)
                p.Write();
        }

        public int GetHouse()
        {
            for (int i = 0; i < 4; i++)
                if (Identifier != 0xFFFFFFFF && DataConverter.ReadUInt(0x9CF8 + i * 0x26B0) == Identifier)
                    return i + 1;
            return 0;
        }

        public void Fill_Catchables()
        {
            //This will add some items to Nook's Catalog as well (They're stored in binary again for space saving)
            int offset = 0x20 + Index * Player_Length;
            DataConverter.WriteByteArray(offset + 0x1164, new byte[] { 0xFF, 0xFF }, false);
            DataConverter.WriteByteArray(offset + 0x1168, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, false);
            DataConverter.WriteByte(offset + 0x1173, 0xFF);
        }

        public void Fill_Catalog()
        {
            int offset = 0x20 + Index * Player_Length + 0x10F0;
            for (int i = 0; i < 0x4; i++)
                DataConverter.WriteByte(offset + i, 0xFF);
            for (int i = 0; i < 0xB0; i++)
                DataConverter.WriteByte(offset + 0x8 + i, 0xFF);
            for (int i = 0; i < 0x28; i++)
                DataConverter.WriteByte(offset + 0xC4 + i, 0xFF);
        }
    }
}
