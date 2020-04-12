using System;
using System.Collections.Generic;
using UnityEditor;
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
        
        [SerializeField] private SpriteRenderer floatingSpritePrefab;
        private SpriteRenderer _floatingSpriteInstance;
        private Camera _camera;
    
        private Dictionary<string, List<Item>> _externalItems = new Dictionary<string, List<Item>>();
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


        private void Update()
        {
            if (_selected != null)
            {
                if (_floatingSpriteInstance == null)
                {
                    _floatingSpriteInstance = Instantiate<SpriteRenderer>(floatingSpritePrefab);
                }

                _floatingSpriteInstance.transform.position =
                    Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10);
                _floatingSpriteInstance.sprite = _selected.Icon;
                
            } else if (_floatingSpriteInstance != null)
            {
                Destroy(_floatingSpriteInstance.gameObject);
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

        public void AddExternalItem(string key, Item item)
        {
            if(_externalItems.TryGetValue(key, out List<Item> value))
            {
                value.Add(item);
            }
            else
            {
                _externalItems[key] = new List<Item>(){item};
            }
        }

        public List<Item> GetExternalItems(string key)
        {
            if (_externalItems.TryGetValue(key, out List<Item> value))
            {
                return value;                
            }
            else
            {
                return new List<Item>();
            }

        }

    }
}