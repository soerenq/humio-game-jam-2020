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

        public Sprite Icon => icon;

        public string Name => name;

        public string Description => description;
    }    
}

