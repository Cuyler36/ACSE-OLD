﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ACSE
{
    class AcreData
    {

        public static Dictionary<ushort, string> Acres = new Dictionary<ushort, string>()
        {
            {0x0000, "No Acre Data" },
            {0x0060, "River /w Islets & Stone Bridges (Left > Down) (Lower)" },
            {0x0061, "River /w Islets & Stone Bridges (Left > Down) (Upper)" },
            {0x006C, "Cliff (Upper Right Corner)" },
            {0x0071, "River (Upper Vertical) (3)" },
            {0x0084, "Cliff (Upper Horizontal) w/ Waterfall (1)" },
            {0x0088, "Ramp (Upper Horizontal)" },
            {0x0094, "Empty Acre (Lower) (1)" },
            {0x0095, "Empty Acre (Upper) (1)" },
            {0x0098, "Empty Acre (Lower) (2)" },
            {0x0099, "Empty Acre (Upper) (2)" },
            {0x009C, "Cliff (Upper Horizontal)" },
            {0x00A8, "Cliff (Left > Up) (Upper)" },
            {0x00B4, "Cliff (Upper Left Corner)" },
            {0x00C0, "Cliff (Upper Right Corner" },
            {0x00CC, "Cliff (Vertical) (Upper)" },
            {0x00D4, "Cliff (Upper Down > Right)" },
            {0x00E0, "River (Horizontal) (Lower)" },
            {0x00E4, "River /w Suspension Bridge (Down > Right) (Lower)" },
            {0x00E5, "River (Down > Right) (Upper)" },
            {0x00E8, "River (Lower Right > Down)" },
            {0x00EC, "River (Down > Left) (Lower)" },
            {0x00ED, "River(Down > Left) (Upper)" },
            {0x00F0, "River (Lower Left > Down)" },
            {0x0100, "River (Lower Vertical) w/ Bridge" },
            {0x0101, "River (Down) (Upper) /w Stone Bridge" },
            {0x0110, "River (Lower Down > Left) w/ Bridge" },
            {0x0114, "River w/ Stone Bridge (Left > Down) (Lower)" },
            {0x0115, "River (Left > Down) (Upper) w/ Stone Bridge" },
            {0x0119, "Dump (1)" },
            {0x011C, "Ramp (Upper Horizontal)" },
            {0x012C, "Ramp (Upper Right Corner)" },
            {0x0134, "Ramp (Down > Right) (Left Side) (Upper)" },
            {0x0138, "Cliff (Down > Right) (Upper) /w River" },
            {0x0155, "Train Station (Orange Roof)" },
            {0x015C, "Cliff (Horizontal)" },
            {0x0160, "Cliff (Upper Horizontal)" },
            {0x0164, "Cliff (Horizontal) (Upper)" },
            {0x016C, "Cliff (Upper Right > Up)" },
            {0x0178, "River (Lower Down > Right)" },
            {0x017D, "River (Right > Down() (Upper)" },
            {0x0180, "River (Left > Down) (Lower)" },
            {0x0184, "River (Lower Left > Down)" },
            {0x0185, "River (Upper, Left > Down)" },
            {0x0188, "Ramp (Left > Up) (Upper)" },
            {0x018C, "Ramp (Upper Horizontal)" },
            {0x0194, "Ramp (Upper Horizontal > Down)" },
            {0x01A4, "Cliff (Upper Left Corner)" },
            {0x01B0, "Cliff (Upper Left Corner)" },
            {0x01B4, "Cliff (Right > Down) (Upper)" },
            {0x01B8, "Cliff (Upper Vertical)" },
            {0x01C0, "River (Horizontal) (Lower)" },
            {0x01C4, "River (Upper Horizontal) w/ Cliff (Up > Right)" },
            {0x01CC, "River (Lower Vertical) w/ Cliff" },
            {0x01D0, "River (Lower Down > Left)" },
            {0x01D1, "River (Down > Left) (Upper)" },
            {0x01DC, "River (Lower Horizontal)" },
            {0x01E1, "River (Upper, Left)" },
            {0x01E8, "Cliff (Right Corner) (Upper)" },
            {0x01EC, "Cliff (Upper Up > Right)" },
            {0x01FD, "Lake (Straight) (Upper)" },
            {0x0200, "Cliff (Upper Horizontal) w/ Waterfall" },
            {0x0204, "Cliff (Horizontal) (Upper) w/ Waterfall" },
            {0x0210, "Cliff (Left > Up) w/ Waterfall" },
            {0x0214, "Cliff /w Waterfall (Horizontal > Down) (Upper)" },
            {0x021C, "Cliff (Upper Down > Right)" },
            {0x0220, "River (Down) (Lower)" },
            {0x022C, "River (Down > Right) (Lower)" },
            {0x022D, "Upper River (Left Corner)" },
            {0x0230, "River (Right > Down) (Lower)" },
            {0x0234, "River (Down > Left) (Lower)" },
            {0x024C, "River (Lower Vertical) w/ Bridge" },
            {0x024D, "River (Upper Vertical) w/ Bridge" },
            {0x0261, "River (Upper, Down > Left) /w Bridge" },
            {0x0264, "River (Lower Left > Down) w/ Bridge" },
            {0x0265, "River (Left > Down) (Upper) w/ Wooden Bridge" },
            {0x0269, "River (Upper Horizontal) /w Bridge" },
            {0x026D, "River (Right) (Upper) /w Bridge" },
            {0x0274, "Empty Acre (Lower) (3)" },
            {0x0275, "Empty Acre (Upper) (3)" },
            {0x0278, "Empty Acre (Upper) (4)" },
            {0x0279, "Empty Acre (Upper) (4)" },
            {0x027C, "Empty Acre (Lower) (5)" },
            {0x027D, "Empty Acre (Upper) (5)" },
            {0x0280, "Empty Acre (Lower) (6)" },
            {0x0281, "Empty Acre (Upper) (6)" },
            {0x0284, "Empty Acre (Lower) (7)" },
            {0x0285, "Empty Acre (Upper) (7)" },
            {0x028C, "Empty Acre (Lower) (8)" },
            {0x028D, "Empty Acre (Upper) (8)" },
            {0x0290, "Empty Acre (Lower) (9)" },
            {0x0291, "Empty Acre (Upper) (9)" },
            {0x0295, "Dump (2)" },
            {0x0299, "Dump (3)" },
            {0x02B9, "River (Upper Vertical) (2)" },
            {0x02BD, "River w/ Train Track Bridge (Upper) (2)" },
            {0x02C1, "River (Upper Vertical) (1)" },
            {0x02C5, "River (Upper Vertical) (2)" },
            {0x02C9, "Lake (Left > Right) (Upper)" },
            {0x02CC, "Lake (Lower Left > Left)" },
            {0x02E8, "Lake (Lower Down > Left)" },
            {0x02E9, "Lake (Down > Left) (Upper)" },
            {0x02EC, "Lake (Lower Left > Down)" },
            {0x02F1, "Train Station (Green Roof)" },
            {0x02F5, "Train Station (Blue Roof)" },
            {0x02FD, "Upper Lake (Right, Down"},
            {0x0320, "Ramp (Upper Horizontal)" },
            {0x0325, "Train Tracks" },
            {0x0329, "Train Track Bridge" },
            {0x032C, "Upper Left Border Wall (3)" },
            {0x032D, "Border Cliff (Upper Left)" },
            {0x0330, "Upper Left Border Wall (2)" },
            {0x0335, "Upper Left Border Wall (1)" },
            {0x0338, "Border Cliff (Lower Right)" },
            {0x0339, "Upper Right Border Wall (3)" },
            {0x033C, "Border Cliff (Right Upper > Lower)" },
            {0x0341, "Upper Right Border Wall (1)" },
            {0x0345, "Left Train Tunnel" },
            {0x0349, "Right Train Tunnel" },
            {0x034C, "Police Station (1)" },
            {0x0350, "Police Station (2)" },
            {0x0354, "Police Station (3)" },
            {0x0359, "Player Houses (1)" },
            {0x035D, "Player Houses (2)" },
            {0x0361, "Player Houses (3)" },
            {0x0364, "Wishing Well (1)" },
            {0x0368, "Wishing Well (2)"},
            {0x036C, "Wishing Well (3)" },
            {0x0371, "Post Office (1)" },
            {0x0375, "Nook's Acre (1)" },
            {0x0379, "Nook's Acre (2)" },
            {0x037D, "Nook's Acre (3)" },
            {0x0381, "Post Office (2)" },
            {0x0385, "Post Office (3)" },
            {0x03A0, "Beachfront (1)" },
            {0x03B0, "Beachfront w/ River (1)" },
            {0x03B4, "Beachfront Border Cliff (Left)" },
            {0x03B8, "Beachfront Border Cliff (Right)" },
            {0x03C0, "Beachfront w/ River (2)" },
            {0x03C8, "Beachfront w/ River (3)" },
            {0x03CC, "Beachfront w/ River (4)" },
            {0x03D0, "Beachfront River w/ Bridge (1)" },
            {0x03D4, "Beachfront River w/ Bridge (2)" },
            {0x03D8, "Beachfront River w/ Bridge (3)" },
            {0x03DC, "Ocean (1)" },
            {0x03E0, "Ocean (2)" },
            {0x03E4, "Ocean (3)" },
            {0x03E8, "Ocean (4)" },
            {0x03EC, "Ocean (5)" },
            {0x03F4, "Beachfront (2)" },
            {0x03F8, "Beachfront (3)" },
            {0x03FC, "Beachfront (4)" },
            {0x0400, "Beachfront (5)" },
            {0x0404, "Beachfront (6)" },
            {0x0408, "Beachfront (7)" },
            {0x040C, "Beachfront (8)" },
            {0x0410, "Ramp (Right > Up) (Upper)" },
            {0x0480, "Museum (1)" },
            {0x0484, "Museum (2)" },
            {0x0488, "Museum (3)" },
            {0x048C, "Tailor's Shop (1)"},
            {0x0490, "Tailor's Shop (2)" },
            {0x0494, "Tailor's Shop (3)" },
            {0x0498, "Beachfront w/ Dock (1)" },
            {0x049C, "Ocean (6)" },
            {0x04A0, "Island (Right) (1)" },
            {0x04A4, "Island (Left) (1)" },
            {0x04A8, "Ocean (7)" },
            {0x04AC, "Ocean (8)" },
            {0x04B0, "Ocean (9)" },
            {0x04B4, "Ocean (10)" },
            {0x04B8, "Ocean (11)" },
            {0x04BC, "Ocean (12)" },
            {0x04C0, "Ocean (13)" },
            {0x04C4, "Ocean (14)" },
            {0x04C8, "Ocean (15)" },
            {0x04CC, "Ocean (16)" },
            {0x04D0, "Ocean (17)" },
            {0x04D4, "Ocean (18)" },
            {0x04D8, "Ocean (19)" },
            {0x04DC, "Ocean (20)" },
            {0x0504, "Ocean (21)" },
            {0x0518, "Ocean (22)" },
            {0x051C, "Ocean (23)" },
            {0x0530, "Ocean (24)" },
            {0x0534, "Ocean (25)" },
            {0x0538, "Ocean (26)" },
            {0x053C, "Ocean (27)" },
            {0x0540, "Ocean (28)" },
            {0x0544, "Ocean (29)" },
            {0x0548, "Ocean (30)" },
            {0x054C, "Ocean (31)" },
            {0x0554, "Ocean (32)" },
            {0x0558, "Ocean (33)" },
            {0x055C, "Ocean (34)" },
            {0x0560, "Ocean (35)" },
            {0x0564, "Ocean (36)" },
            {0x0568, "Ocean (37)" },
            {0x056C, "Ocean (38)" },
            {0x0570, "Ocean (39)" },
            {0x0574, "Ocean (40)" },
            {0x0578, "Ocean (41)" },
            {0x057C, "Ocean (42)" },
            {0x0580, "Ocean (43)" },
            {0x0584, "Ocean (44)" },
            {0x0588, "Ocean (45)" },
            {0x058C, "Ocean (46)" },
            {0x0594, "Island (Right) (2)" },
            {0x0598, "Island (Left) (2)" },
            {0x059C, "Island (Right) (3)" },
            {0x05A0, "Island (Left) (3)" },
            {0x05A4, "Island (Right) (4)" },
            {0x05A8, "Island (Left) (4)" },
            {0x05AC, "Beachfront w/ Dock (2)" },
            {0x05B0, "Beachfront w/ Dock (3)" },
            {0x05B4, "Ocean (47)" },
            {0x05B8, "Ocean (48)" },
        };

        public Dictionary<int, Item> GetAcreData(ushort[] acreBuffer)
        {
            Dictionary<int, Item> acreData = new Dictionary<int, Item> { };
            int i = 0;
            Item EmptyItem = new Item(0);
            foreach (ushort cellData in acreBuffer)
            {
                acreData.Add(i, cellData == 0 ? EmptyItem : new Item(cellData));
                i++;
            }
            return acreData;
        }

        public static ushort[] ClearWeeds(ushort[] acreBuffer)
        {
            int WeedsCleared = 0;
            for (int i = 0; i < acreBuffer.Length; i++)
            {
                if (acreBuffer[i] >= 0x08 && acreBuffer[i] <= 0x0A)
                {
                    acreBuffer[i] = 0x00;
                    WeedsCleared++;
                }
            }
            MessageBox.Show("Weeds Cleared: " + WeedsCleared.ToString());
            return acreBuffer;
        }

        public static ushort[] ClearTown(ushort[] buffer)
        {
            int itemsCleared = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] < 0x4000)
                {
                    buffer[i] = 0x00;
                    itemsCleared++;
                }
            }
            MessageBox.Show("Items Cleared: " + itemsCleared.ToString());
            return buffer;
        }

        public static string GetAcreName(ushort acreId)
        {
            foreach (KeyValuePair<ushort, string> acreData in Acres)
            {
                if (acreId == acreData.Key)
                    return acreData.Value;
            }
            return "Unknown";
        }

        public static Dictionary<int, Acre> GetAcreTileData(ushort[] acreBuffer)
        {
            Dictionary<int, Acre> AcreTileData = new Dictionary<int, Acre> { };
            int i = 0;
            foreach (ushort acre in acreBuffer)
            {
                //MessageBox.Show(acre.ToString("X"));
                i++;
                AcreTileData.Add(i, new Acre(acre, i));
            }
            return AcreTileData;
        }
    }

    public class Acre
    {
        public ushort AcreID = 0;
        public int Index = 0;
        public string Name = "";

        public Acre(ushort acreId, int position)
        {
            AcreID = acreId;
            Index = position;
            Name = AcreData.GetAcreName(acreId);
        }
    }

    public class A_Acre : Acre
    {
        public WorldItem[] Acre_Items = new WorldItem[12 * 16];

        public A_Acre(ushort acreId, int position, ushort[] items = null) : base(acreId, position)
        {
            if (items != null)
                for (int i = 0; i < 192; i++)
                    Acre_Items[i] = new WorldItem(items[i], i);
        }
    }

    public class Normal_Acre : Acre
    {
        public WorldItem[] Acre_Items = new WorldItem[16 * 16];

        public Normal_Acre(ushort acreId, int position, ushort[] items = null, byte[] burriedItemData = null) : base(acreId, position)
        {
            if (items != null)
                for (int i = 0; i < 256; i++)
                {
                    Acre_Items[i] = new WorldItem(items[i], i);
                    if (Acre_Items[i].ItemID != 0 && Acre_Items[i].ItemID != 0xFFFF && burriedItemData != null)
                        SetBuried(Acre_Items[i], position - 1, burriedItemData);
                }
        }

        private int GetBuriedLocation(WorldItem item, int acre)
        {
            int worldPosition = (acre * 256) + item.Location.X % 8 + item.Location.Y * 16;
            int burriedDataOffset = worldPosition / 8;
            if (item.Location.X > 7)
                burriedDataOffset -= 1;
            return burriedDataOffset;
        }

        public void SetBuriedInMemory(WorldItem item, int acre, byte[] burriedItemData, bool buried)
        {
            int buriedLocation = GetBuriedLocation(item, acre);
            if (buriedLocation > -1)
            {
                int mask = 1 << (item.Location.X % 8);
                int isBuried = (burriedItemData[buriedLocation] >> item.Location.X % 8) & 1;
                if (isBuried == 0 && buried)
                {
                    burriedItemData[buriedLocation] = (burriedItemData[buriedLocation] |= (byte)mask);
                    item.Burried = true;
                }
                else if (isBuried == 1 && !buried)
                {
                    burriedItemData[buriedLocation] = (burriedItemData[buriedLocation] &= (byte)~mask);
                    item.Burried = false;
                }
            }
            else
            {
                item.Burried = false;
            }
        }

        private void SetBuried(WorldItem item, int acre, byte[] burriedItemData)
        {
            if (item.ItemID == 0 || item.ItemID == 0xFFFF)
                return;
            int burriedDataOffset = GetBuriedLocation(item, acre);
            if (burriedDataOffset > -1)
            {
                int burried = (burriedItemData[burriedDataOffset] >> item.Location.X % 8) & 1;
                item.Burried = burried == 1;
            }
        }
    }
}
