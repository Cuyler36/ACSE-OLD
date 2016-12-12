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
        static public byte[] House_Type = new byte[6]
        {
            0x00, 0x24, 0x48, 0x00, 0x00, 0x00
        };

        public Dictionary<int, string> GetHouseData(ushort[] houseBuffer, int houseSize = 8)
        {
            //List<string> houseData = new List<string> { };
            Dictionary<int, string> data = new Dictionary<int, string> { };
            bool inbounds = false;
            int y = 0;
            int size = 0;
            int pos = 0;
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
                    }
                    else if(!inbounds)
                    {
                        inbounds = true;
                    }
                }
                else if (inbounds && y > 0)
                {
                    pos++;
                    if (!sizeSet)
                        size++;
                    if (houseBuffer[x] == 0)
                        data.Add(pos, "Empty");
                    else if (houseBuffer[x] != 0xFFFE && houseBuffer[x] != 0xFE1F && houseBuffer[x] != 0xFE1B)
                        data.Add(pos, ItemData.GetItemName(houseBuffer[x]));
                    else if (houseBuffer[x] == 0xFE1F) //0xFE1F = Barrier/Occupied Space. Left by multispace furniture!
                    {
                        string name = "Occupied";
                        int index = pos - size;
                        if (houseBuffer[x - 1] != 0xFFFE && data.ContainsKey(pos - 1) && data[pos - 1] != "Empty")
                            name = data[pos - 1];
                        else if (data.ContainsKey(index) && data[index] != "Empty")
                            name = data[index];
                        else if (data.ContainsKey(index - 1) && data[index - 1] != "Empty")
                            name = data[index - 1];
                        //MessageBox.Show(pos.ToString());
                        data.Add(pos, name);
                    }
                }
            }
            return data;
        }

        public void SetHouseData(ushort[] houseData, int offset, BinaryWriter writer)
        {
            writer.Seek(offset, SeekOrigin.Begin);
            foreach (ushort data in houseData)
            {
                byte[] us = BitConverter.GetBytes(data);
                Array.Reverse(us);
                writer.Write(us);
            }
        }
    }
}
