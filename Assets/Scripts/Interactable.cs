using System;
using UnityEngine;

namespace Humio
{
    public class Interactable : MonoBehaviour
    {
        public virtual void Interact()
        {
            Debug.Log($"Interacted with: {name}");
        }
    }
}