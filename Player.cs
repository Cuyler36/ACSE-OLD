using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal_Crossing_GCN_Save_Editor
{
    class Player
    {
        public static Dictionary<string, int> Data_Offsets = new Dictionary<string, int>()
        {
            {"Name", 0x0 },
            {"Town_Name", 0x8 },
            {"Pockets", 0x68 },
            {"Bells", 0x8C },
            {"Debt", 0x90 },
            {"Held_Item", 0x4A4 },
            {"Inventory_Background", 0x1084 },
            {"Shirt", 0x1089 },
        };

        int Index = 0;
        string Name;
        Inventory Inventory;
        int Bells;
        int Debt;
        Item Held_Item;
        Item Shirt;
        Item Inventory_Background;
        Item[] Stored_Items;
        //TODO: Research Face Data

        public Player(int idx, string name, ushort[] inventory, int bells, int debt, ushort heldItem, ushort shirt, ushort inventoryBackground, ushort[] storedItems = null)
        {
            Index = idx;
            Name = name;
            Inventory = new Inventory(inventory);
            Bells = bells;
            Debt = debt;
            Held_Item = new Item(heldItem);
            Shirt = new Item(shirt);
            Inventory_Background = new Item(inventoryBackground);
            if (storedItems != null)
            {
                Stored_Items = new Item[3];
                for (int i = 0; i < storedItems.Length; i++)
                    Stored_Items[i] = new Item(storedItems[i]);
            }
        }

        public void Write()
        {
            
        }
    }
}
