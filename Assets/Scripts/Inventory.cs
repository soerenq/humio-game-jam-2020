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

        
        public static Inventory Instance => _instance;

        public int Space
        {
            get => _space;
            set => _space = value;
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

        public bool Add(Item item)
        {
            if (_items.Count >= _space)
            {
                Debug.Log("Insufficient space");
                return false;
            }
            _items.Add(item);
            onItemAdd?.Invoke(item);
            return true;
        }
        
        public void Remove(Item item)
        {
            _items.Remove(item);
            onItemRemove?.Invoke(item);
        }
        
    }
}