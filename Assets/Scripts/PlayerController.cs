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
                Debug.Log($"Shooting at {Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10)}");
                var hit = Physics2D.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10), Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
                    var interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        Debug.Log("We hit interactable");
                        interactable.Interact();                        
                    }

                }
            }
        }
    }
}