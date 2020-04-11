using System;
using UnityEngine;

namespace Humio
{
    public class PlayerController : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var hit = Physics2D.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10), Vector2.zero);
                if (hit.collider != null)
                {
                    var interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.Interact();                        
                    }

                }
            }
        }
    }
}