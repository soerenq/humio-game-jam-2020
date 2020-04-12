using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Crate touched by {0}", other);
        var otherGO = other.gameObject;
        var player = other.GetComponent<BasementPlayer>();
        if (player != null)
        {
            player.OnTouchingCrate();
            gameObject.SetActive(false);
        }
    }

}
