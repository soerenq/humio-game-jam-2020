using System;
using System.Collections.Generic;
using UnityEngine;

namespace Humio
{
    public class Droppable : Interactable
    {
        [SerializeField] private List<Item> requires;
        [SerializeField] private bool orderMatters;
        [SerializeField] private string droppableName;
        [SerializeField] private string description;
        
        private List<Item> _addedItems = new List<Item>();
        

        private void Awake()
        {
            if (requires == null)
            {
                Debug.LogError($"{droppableName} requires item requirement");                
            }
        }

        public override void Interact()
        {
            base.Interact();
            // Select in inventory
            var selectedItem = Inventory.Instance.Selected;
            if (requires.Contains(selectedItem) && (!orderMatters || requires[_addedItems.Count] == selectedItem))
            {
                Console.Instance.ReplaceText($"Successfully dropped {selectedItem} on {droppableName}");
                Inventory.Instance.Remove(selectedItem);
                _addedItems.Add(selectedItem);
                if (_addedItems.Count == requires.Count)
                {
                    Trigger();
                }
            }
            else if(selectedItem != null)
            {
                Console.Instance.ReplaceText($"Tried dropping {selectedItem} on {droppableName} but it failed");                
            }
            else
            {
                Console.Instance.ReplaceText($"It's a {droppableName}. {description}");
            }


        }

        public virtual void Trigger()
        {
            Debug.Log("Triggered");
        }
        
    }
}