using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal_Crossing_GCN_Save_Editor
{
    class Player
    {
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

        public Player(string name, ushort[] inventory, int bells, int debt, ushort heldItem, ushort shirt, ushort inventoryBackground, ushort[] storedItems = null)
        {
            Name = name;
            Inventory = new Inventory(inventory);
            Bells = bells;
            Debt = debt;
            Held_Item = new Item(heldItem);
            Shirt = new Item(shirt);
            Inventory_Background = new Item(inventoryBackground);
            //Implement Stored Items
        }
    }
}
