using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Humio
{
    [CreateAssetMenu(fileName = "Item", menuName = "GameJam/Item", order = 1)]    
    public class Item : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private List<Item> ingredients;
        // TODO could have action here, that will be triggered on click?

        public Sprite Icon => icon;

        public string Name => name;

        public string Description => description;

        public List<Item> Ingredients => ingredients;
    }    
}

