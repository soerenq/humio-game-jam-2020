using UnityEngine;
using UnityEngine.UI;

namespace Humio
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button _button;
        
        private Item _item;

        public bool hasItem => _item != null;
        
        public void AddItem(Item item)
        {
            _item = item;
            icon.sprite = _item.Icon;
            icon.enabled = true;
            _button.onClick.AddListener(() => Inventory.Instance.Selected = item);
        }

        public void RemoveItem()
        {
            _item = null;
            icon.sprite = null;
            icon.enabled = false;
            _button.onClick.RemoveAllListeners();
        }
    }
}