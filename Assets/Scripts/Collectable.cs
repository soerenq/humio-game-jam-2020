using JetBrains.Annotations;
using UnityEngine;

namespace Humio
{
    public class Collectable : Interactable
    {
        [SerializeField]
        private Item _item;

        public override void Interact()
        {
            base.Interact();
            if (_item == null)
            {
                Debug.LogError($"No item supplied in {name}!");
                return;
            }

            if (Inventory.Instance.Add(_item))
            {
                Destroy(gameObject);                
            }
            else
            {
                Debug.Log("Unable to pickup item");
            }
        }
    }
}