using System;
using UnityEngine;

namespace Humio
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Interactable : MonoBehaviour
    {
        public virtual void Interact()
        {
            Debug.Log($"Interacted with: {name}");
        }
    }
}