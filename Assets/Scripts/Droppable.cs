using System;
using UnityEngine;

namespace Humio
{
    public class Droppable : Interactable
    {
        [SerializeField] private Item requires;

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
            if (requires.Equals(selectedItem))
            {
                Debug.Log($"Successfully dropped {selectedItem} on {name}");
                Inventory.Instance.Remove(selectedItem);
                Trigger();
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