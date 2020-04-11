using UnityEngine;
using UnityEngine.UI;

namespace Humio
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image icon;
        
        private Item _item;

        public bool hasItem => _item != null;
        
        public void AddItem(Item item)
        {
            _item = item;
            icon.sprite = _item.Icon;
            icon.enabled = true;
        }

        public void RemoveItem()
        {
            _item = null;
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}