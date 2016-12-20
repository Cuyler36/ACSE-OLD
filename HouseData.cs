using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animal_Crossing_GCN_Save_Editor
{
    class HouseData
    {
        private ItemData itemData = new ItemData { };
        static public int[] House_Addresses = new int[4] { 0x2FD60, 0, 0, 0 };
        static public int House1_DataSize = 0xB0;
        static public ushort[] House_Identifiers = new ushort[10]
        {
            0x0480, 0x2480, 0x4880, 0x24A0, 0x4890, 0x48A0, 0x6C90, 0x6C80, 0x7000, 0x0000 //StarterHouse, First Upgrade, Expanded Main Room (No Basement), First Upgrade + Basement, Expanded Room + Basement (From Basement), Expanded Room + Basement (From Expanded Room), 2nd Floor (From Expanded Room), 2nd Floor (From Basement), Statue (From Basement)
        };

        static public int[] House_Data_Sizes = new int[8]
        {
            0, 0, 0, 0x8C, 0, 0xF0, 0, 0x114
        };

        static public int[] House_Data_Layer2_Sizes = new int[8]
        {
            0, 0, 0, 0x68, 0, 0xAC, 0, 0xF0
        };

        public static int GetHouseSize (ushort[] houseBuffer)
        {
            //Gonna stick with this for now. Haven't determined what bytes determine what house level you have.
            bool inbounds = false;
            int y = 0;
            int size = 0;
            bool sizeSet = false;
            for (int x = 0; x < houseBuffer.Length; x++)
            {
                if (houseBuffer[x] == 0xFFFE)
                {
                    if (inbounds && houseBuffer[x + 1] == 0)
                    {
                        y++;
                        inbounds = false;
                        sizeSet = (size == 0) ? false : true;
                        if (sizeSet)
                            return size;
                    }
                    else if (!inbounds)
                        inbounds = true;
                }
                else if (inbounds && y > 0)
                {
                    if (!sizeSet)
                        size++;
                }
            }
            return size;
        }

        public static Item[] GetHouseData(ushort[] houseBuffer, int size = 0)
        {
            List<Item> items = new List<Item>();
            bool inbounds = false;
            int y = 0;
            if (size == 0)
                size = GetHouseSize(houseBuffer);
            int pos = 0;
            for (int x = 0; x < houseBuffer.Length - 1; x++)
            {
                if (houseBuffer[x] == 0xFFFE)
                {
                    if (inbounds && x + 1 < houseBuffer.Length && houseBuffer[x + 1] == 0)
                    {
                        y++;
                        inbounds = false;
                    }
                    else if(!inbounds)
                        inbounds = true;
                }
                else if (inbounds && y > 0)
                {
                    pos++;
                    if (houseBuffer[x] == 0)
                        items.Add(new Item(0));
                    else if (houseBuffer[x] != 0xFFFE && houseBuffer[x] != 0xFE1F && houseBuffer[x] != 0xFE1B)
                        items.Add(new Item(houseBuffer[x]));
                    else if (houseBuffer[x] == 0xFE1F) //0xFE1F = Barrier/Occupied Space. Left by multispace furniture!
                    {
                        ushort itemId = 0xFFFF;
                        int index = pos - size;

                        if (x > 0 && houseBuffer[x - 1] != 0xFFFE && items.Count >= pos && items[pos - 1].ItemID != 0)
                            itemId = items[pos - 1].ItemID;
                        else if (items.Count > index && index >= 0 && items[index].ItemID != 0)
                            itemId = items[index].ItemID;
                        else if (index > 0 && items.Count >= index - 1 && items[index - 1].ItemID != 0)
                            itemId = items[index - 1].ItemID;
                        items.Add(new Item(itemId));
                    }
                }
            }
            return items.ToArray();
        }

        public static void UpdateHouseData(Item[] houseItems, ushort[] houseBuffer)
        {
            int pos = 0;
            bool inbounds = false;
            int y = 0;
            for (int x = 0; x < houseBuffer.Length; x++)
            {
                if (houseBuffer[x] == 0xFFFE)
                {
                    if (inbounds && x + 1 < houseBuffer.Length && houseBuffer[x + 1] == 0)
                    {
                        y++;
                        inbounds = false;
                    }
                    else if (!inbounds)
                        inbounds = true;
                }
                else if (inbounds && y > 0)
                    if (houseBuffer[x] != 0xFFFE && houseBuffer[x] != 0xFE1B)
                    {
                        houseBuffer[x] = houseItems[pos].ItemID;
                        pos++;
                    }
            }
        }
    }
}
