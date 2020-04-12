using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private List<string> _randomBadReplies = new List<string>(){"but it failed","but it failed miserably and gave you scars for life", "and nothing happened. What did you expect?", "but did so in vain", "but it fell short", "but it turned out to be a bad idea"};

        private void Awake()
        {
            if (requires == null)
            {
                Debug.LogError($"{droppableName} requires item requirement");                
            }
            // Initialize state
            _addedItems.AddRange(Inventory.Instance.GetExternalItems(droppableName));
        }

        public override void Interact()
        {
            base.Interact();
            // Select in inventory
            var selectedItem = Inventory.Instance.Selected;
            if (requires.Contains(selectedItem) && (!orderMatters || requires[_addedItems.Count] == selectedItem))
            {
                Console.Instance.ReplaceText($"Successfully dropped {selectedItem.Name} on {droppableName}");
                Inventory.Instance.AddExternalItem(droppableName, selectedItem);
                Inventory.Instance.Remove(selectedItem);
                _addedItems.Add(selectedItem);
                if (_addedItems.Count == requires.Count)
                {
                    Trigger();
                }
            }
            else if(selectedItem != null)
            {
                Console.Instance.ReplaceText($"You spent a minute trying to drop {selectedItem.Name} on {droppableName} {_randomBadReplies[Random.Range(0,_randomBadReplies.Count)]}");
                Counter.Instance.AddPenalty(60f);
                if (requires.Contains(selectedItem))
                {
                    Console.Instance.AddText("Maybe the order of adding the items matters");
                }
            }
            else
            {
                Console.Instance.ReplaceText($"It's a {droppableName}. {description}");
                if (_addedItems.Count > 0)
                {
                    Console.Instance.AddText($"You already added {_addedItems.Aggregate("", (current, next) => current + (current.Equals("")? "" : ", ") + next.Name)}");
                }
            }


        }

        public virtual void Trigger()
        {
            Debug.Log("Triggered");
        }
        
    }
}