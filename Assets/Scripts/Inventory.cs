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

        public void Add(Item item)
        {
            if (_items.Count >= _space)
            {
                Debug.Log("Insufficient space");
                return;
            }
            _items.Add(item);
            onItemAdd?.Invoke(item);
        }
        
        public void Remove(Item item)
        {
            _items.Remove(item);
            onItemRemove?.Invoke(item);
        }
        
    }
}