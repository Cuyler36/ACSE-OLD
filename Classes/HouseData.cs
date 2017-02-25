using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACSE
{
    class HouseData
    {
        /*
         * New Discovery:
         *      Houses have four levels in them. These are layers you can store items on top of each other.
         *      This is how dressers work in the game. Each layer is 0x228 bytes away from the beginning of the previous one.
         *      So, a dresser with three items looks like this:
         *          Fourth Layer: Item 3
         *          Third Layer: Item 2
         *          Second Layer: Item 1
         *          Main Floor: Dresser
         *          
         *      This means that it's probably unnecessary to add "storage" to the inventory editor.
         */
        public static ushort[] House_Identifiers = new ushort[10]
        {
            0x0480, 0x2480, 0x4880, 0x24A0, 0x4890, 0x48A0, 0x6C90, 0x6C80, 0x7000, 0x0000 //StarterHouse, First Upgrade, Expanded Main Room (No Basement), First Upgrade + Basement, Expanded Room + Basement (From Basement), Expanded Room + Basement (From Expanded Room), 2nd Floor (From Expanded Room), 2nd Floor (From Basement), Statue (From Basement)
        };

        public static int[] House_Data_Sizes = new int[3]
        {
            0x8C, 0xF0, 0x114
        };

        public static int[] House_Data_Layer2_Sizes = new int[3]
        {
            0x68, 0xAC, 0xF0
        };

        //Rewrote all methods here to be significantly shorter. I originally wrote them when I had just started in C#.

        public static int ReadHouseSize(ushort[] houseBuffer, bool includesPadding = true)
        {
            int x;
            for (x = (includesPadding ? 0x11 : 0x0); x < houseBuffer.Length; x++)
                if (houseBuffer[x] == 0xFFFE)
                    break;
            return (x - (includesPadding ? 0x11 : 0x0));
        }

        public static Furniture[] ReadHouseData(ushort[] houseBuffer, int size = 0, bool includesPadding = true)
        {
            if (size == 0)
                size = ReadHouseSize(houseBuffer, includesPadding);
            Furniture[] Furniture_Array = new Furniture[size * size];

            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                    Furniture_Array[y * size + x] = new Furniture(houseBuffer[(includesPadding ? 0x11 : 0x0) + 0x10 * y + x]);

            return Furniture_Array;
        }

        public static void UpdateHouseData(Furniture[] houseItems, ushort[] houseBuffer, int size, bool includesPadding = true)
        {
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                    houseBuffer[(includesPadding ? 0x11 : 0x0) + 0x10 * y + x] = houseItems[y * size + x].ItemID;
        }
    }
}
