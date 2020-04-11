using System;
using System.Collections.Generic;
using UnityEngine;

namespace Humio
{
    public class Inventory : MonoBehaviour
    {
        private static Inventory _instance;

        private List<Item> _items = new List<Item>();
        private int _space = 10;
        

        public Action<Item> onItemAdd;
        public Action<Item> onItemRemove;
        private Item _selected;


        public static Inventory Instance => _instance;

        public int Space
        {
            get => _space;
            set => _space = value;
        }

        public Item Selected
        {
            get => _selected;
            set
            {
                if (_selected == value)
                {
                    Console.Instance.ReplaceText($"You put {value.Name} back in bag");
                    _selected = null;
                }
                else
                {
                    Console.Instance.ReplaceText($"You grab {value.Name} from your bag. By inspection, you describe it as: {value.Description}");
                    _selected = value;                    
                }
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public bool Add(Item item, bool producer = false)
        {
            if (_items.Count >= _space)
            {
                Console.Instance.ReplaceText($"Your bag is full!");
                Debug.Log("Insufficient space");
                return false;
            }

            if (!producer)
            {
                Console.Instance.ReplaceText($"You put {item.Name} in your bag");                
            }
            _items.Add(item);
            onItemAdd?.Invoke(item);
            return true;
        }
        
        public void Remove(Item item)
        {
            _items.Remove(item);
            if (_selected == item)
            {
                _selected = null;
            }
            onItemRemove?.Invoke(item);
        }
    }
}