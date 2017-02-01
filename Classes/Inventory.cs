using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace ACSE
{
    public class Inventory
    {
        public InventorySlot[] InventorySlots = new InventorySlot[15];
        public Item[] Items = new Item[15];

        public Inventory(ushort[] inventoryData)
        {
            for(int i = 0; i < inventoryData.Length; i++)
            {
                Item item = new Item(inventoryData[i]);
                Items[i] = item;
                InventorySlots[i] = new InventorySlot(item, i + 1);
            }
        }

        public static Image getItemPic(int itemsize, int itemsPerRow, Item[] items)
        {
            int width = itemsize * itemsPerRow, height = itemsize * items.Length / itemsPerRow;
            height = height < 1 ? width : height;
            byte[] bmpData = new byte[4 * ((width) * (height))];
            for (int i = 0; i < items.Length; i++)
            {
                int X = i % itemsPerRow;
                int Y = i / itemsPerRow;
                Item item = items[i] == null ? new Item(0) : items[i];
                uint itemColor = ItemData.getItemColor(ItemData.getItemType(item.ItemID));

                for (int x = 0; x < itemsize * itemsize; x++)
                {
                    int dataPosition = (Y * itemsize + x % itemsize) * width * 4 + (X * itemsize + x / itemsize) * 4;
                    Buffer.BlockCopy(BitConverter.GetBytes(itemColor), 0, bmpData, dataPosition, 4);
                }
            }

            for (int i = 0; i < (width * height); i++)
                if ((i / itemsize > 0 && i % (itemsize * itemsPerRow) > 0 && i % (itemsize) == 0) || (i / (itemsize * itemsPerRow) > 0 && (i / (itemsize * itemsPerRow)) % (itemsize) == 0))
                    Buffer.BlockCopy(BitConverter.GetBytes(0x17000000), 0, bmpData,
                        ((i / (itemsize * itemsPerRow)) * width * 4) + ((i % (itemsize * itemsPerRow)) * 4), 4);

            Bitmap b = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bData = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(bmpData, 0, bData.Scan0, bmpData.Length);
            b.UnlockBits(bData);
            return b;
        }

        public ushort[] GetItemIDs()
        {
            ushort[] ids = new ushort[15];
            for (int i = 0; i < 15; i++)
                ids[i] = Items[i].ItemID;
            return ids;
        }
    }

    public class InventorySlot
    {
        public Item Item;
        public int SlotID;

        public InventorySlot(Item item, int slotId)
        {
            Item = item;
            SlotID = slotId;
        }
    }
}
