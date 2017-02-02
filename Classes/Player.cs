﻿using System;
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
        public uint Identifier;
        public int House_Number = 0;
        public int House_Data_Offset = 0;
        public uint Savings = 0;
        public Pattern[] Patterns = new Pattern[8];
        public bool Reset = false;
        public bool Exists = false;

        public Player(int idx)
        {
            Index = idx;
            Read();
        }

        //Town Identifier is: 0x30 0x??
        //Player Identifier is: 0xF0 0x??
        //Villager Identifier is: 0xE0 0x??
        //Can Look up resetti values, if wanted.
        //Documented ones: 0x250C | 0xAE8A | 0x85A6

        public void Read()
        {
            int offset = 0x20 + Index * Player_Length;
            Name = DataConverter.ReadString(offset + 0, 8).Trim();
            Town_Name = DataConverter.ReadString(offset + 0x8, 8).Trim();
            Identifier = BitConverter.ToUInt32(DataConverter.ReadData(offset + 0x10, 4), 0); //First two are player identifier bytes. Second two bytes are town identifier bytes.
            Gender = DataConverter.ReadData(offset + 0x14, 1)[0];
            Face = DataConverter.ReadData(offset + 0x15, 1)[0];
            Inventory = new Inventory(DataConverter.ReadRawUShort(offset + 0x68, 0x1E));
            Bells = BitConverter.ToUInt32(DataConverter.ReadData(offset + 0x8C, 4), 0);
            Debt = BitConverter.ToUInt32(DataConverter.ReadData(offset + 0x90, 4), 0);
            Held_Item = new Item(DataConverter.ReadRawUShort(offset + 0x4A4, 2)[0]);
            Inventory_Background = new Item(DataConverter.ReadRawUShort(offset + 0x1084, 2)[0]);
            Shirt = new Item(DataConverter.ReadRawUShort(offset + 0x1089 + 1, 2)[0]); //Research Patterns used as shirt.
            Reset = DataConverter.ReadRawUShort(offset + 0x10F6, 2)[0] > 0;
            Savings = BitConverter.ToUInt32(DataConverter.ReadData(offset + 0x122C, 4), 0);
            for (int i = 0; i < 8; i++)
                Patterns[i] = new Pattern(offset + 0x1240 + i * 0x220);
            House_Number = GetHouse();
            House_Data_Offset = 0x9CF8 + (House_Number - 1) * 0x26B0 + 0x28;
            Exists = Identifier != 0xFFFFFFFF;
        }

        public void Write()
        {
            int offset = 0x20 + Index * Player_Length;
            DataConverter.WriteString(offset + 0, Name, 8);
            DataConverter.WriteString(offset + 0x8, Town_Name, 8);
            DataConverter.WriteData(offset + 0x14, new byte[] { Gender });
            DataConverter.WriteData(offset + 0x15, new byte[] { Face });
            DataConverter.WriteUShort(Inventory.GetItemIDs(), offset + 0x68);
            DataConverter.WriteData(offset + 0x8C, BitConverter.GetBytes(Bells));
            DataConverter.WriteData(offset + 0x90, BitConverter.GetBytes(Debt));
            DataConverter.WriteUShort(new ushort[] { Held_Item.ItemID }, offset + 0x4A4);
            DataConverter.WriteUShort(new ushort[] { Inventory_Background.ItemID }, offset + 0x1084);
            DataConverter.WriteDataRaw(offset + 0x1089, new byte[] { (byte)(Shirt.ItemID & 0xFF), 0x24, (byte)(Shirt.ItemID & 0xFF) });

            if (Properties.Settings.Default.StopResetti)
                DataConverter.WriteUShort(new ushort[] { 0 }, offset + 0x10F6);

            DataConverter.WriteData(offset + 0x122C, BitConverter.GetBytes(Savings));

            foreach (Pattern p in Patterns)
                p.Write();
        }

        public int GetHouse()
        {
            for (int i = 0; i < 4; i++)
                if (Identifier != 0xFFFFFFFF && BitConverter.ToUInt32(DataConverter.ReadData(0x9CF8 + i * 0x26B0, 4), 0) == Identifier)
                    return i + 1;
            return 0;
        }
    }
}
