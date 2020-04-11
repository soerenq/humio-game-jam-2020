using System;
using System.Collections.Generic;
using UnityEngine;

namespace Humio
{
    public class Droppable : Interactable
    {
        [SerializeField] private List<Item> requires;
        [SerializeField] private bool orderMatters;
        private List<Item> _addedItems = new List<Item>();
        

        private void Awake()
        {
            if (requires == null)
            {
                Debug.LogError($"{name} requires item requirement");                
            }
        }

        public override void Interact()
        {
            base.Interact();
            // Select in inventory
            var selectedItem = Inventory.Instance.Selected;
            if (requires.Contains(selectedItem) && (!orderMatters || requires[_addedItems.Count] == selectedItem))
            {
                Debug.Log($"Successfully dropped {selectedItem} on {name}");
                Inventory.Instance.Remove(selectedItem);
                _addedItems.Add(selectedItem);
                if (_addedItems.Count == requires.Count)
                {
                    Trigger();
                }
            }
            else if(selectedItem != null)
            {
                Debug.Log($"Tried dropping {selectedItem} on {name} but it failed");                
            }


        }

        public virtual void Trigger()
        {
            Debug.Log("Triggered");
        }
        
    }
}