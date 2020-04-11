using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Humio
{
    public class Droppable : Interactable
    {
        [SerializeField] private List<Item> requires;
        [SerializeField] private bool orderMatters;
        [SerializeField] private string droppableName;
        [SerializeField] private string description;
        
        private List<Item> _addedItems = new List<Item>();
        
        private List<string> _randomBadReplies = new List<string>(){"but it failed","but it failed miserably and gave you scars for life", "and nothing happened. What did you expect?"};

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
                Console.Instance.ReplaceText($"Successfully dropped {selectedItem.Name} on {droppableName}");
                Inventory.Instance.Remove(selectedItem);
                _addedItems.Add(selectedItem);
                if (_addedItems.Count == requires.Count)
                {
                    Trigger();
                }
            }
            else if(selectedItem != null)
            {
                Console.Instance.ReplaceText($"Tried dropping {selectedItem.Name} on {droppableName} {_randomBadReplies[Random.Range(0,_randomBadReplies.Count)]}");                
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