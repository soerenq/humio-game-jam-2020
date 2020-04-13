using System;
using System.Collections;
using System.Collections.Generic;
using Humio;
using UnityEngine;

public class BoyBehaviour : MonoBehaviour
{
    private PlayerController _playerController;
    [SerializeField] private BoxCollider2D coffeBeansCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        // here be dragons
        _playerController = FindObjectOfType<PlayerController>();
        _playerController.gameObject.SetActive(false);
        coffeBeansCollider.enabled = true;
    }
    
    private void OnDestroy()
    {
        _playerController.gameObject.SetActive(true);
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
            interactable.Interact();
        }
    }
}
