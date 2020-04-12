using System;
using System.Collections;
using System.Collections.Generic;
using Humio;
using UnityEngine;

public class BoyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<Collider2D>().GetComponent<Interactable>();
        if (interactable != null)
        {
            Debug.Log($"Boy interacting with {interactable.name} | {interactable}");
            interactable.Interact();
        }
    }
}
