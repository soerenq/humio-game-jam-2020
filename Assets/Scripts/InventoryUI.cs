using System;
using System.Collections.Generic;
using UnityEngine;

namespace Humio
{
    public class InventoryUI : MonoBehaviour
    {
        private Inventory _inventory;
        private InventorySlot[] _inventorySlots;
        private Dictionary<Item, InventorySlot> _itemToInventorySlot = new Dictionary<Item, InventorySlot>();

        private void Start()
        {
            _inventory = Inventory.Instance;
            _inventory.onItemAdd += AddItem;
            _inventory.onItemRemove += RemoveItem;
            _inventorySlots = GetComponentsInChildren<InventorySlot>();
            _inventory.Space = _inventorySlots.Length;
        }
        

        private void AddItem(Item item)
        {
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                if (!_inventorySlots[i].hasItem)
                {
                    _inventorySlots[i].AddItem(item);
                    _itemToInventorySlot.Add(item, _inventorySlots[i]);
                    return;
                }

            }
        }

        private void RemoveItem(Item item)
        {
            _itemToInventorySlot[item].RemoveItem();
            _itemToInventorySlot.Remove(item);
        }
    }
}