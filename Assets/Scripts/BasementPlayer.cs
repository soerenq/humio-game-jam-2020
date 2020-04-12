using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementPlayer : MonoBehaviour
{
    const float SPEED = 50f; // Per second

    public bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        
        var rt = (RectTransform) this.transform;
        var dx = 0f;
        var dy = 0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dx = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dx = -1f;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            dy = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            dy = -1f;
        }

        if (Mathf.Abs(dx) == 0f && Mathf.Abs(dy) == 0f)
        {
            StopMoving();
        }
        else
        {
            rt.localRotation = Quaternion.EulerAngles(0f, 0f, Mathf.Atan2(dy, dx));
            MoveAhead(rt);
        }
    }


    private void MoveAhead(RectTransform rt)
    {
        var rb = GetComponent<Rigidbody2D>();
        var direction = rt.localRotation * Vector3.right;
        rb.velocity = SPEED * direction; 
    }

    private void StopMoving()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero; 
    }

    public void OnTouchedBySpider()
    {
        alive = false;
    }
    public void OnTouchingCrate()
    {
        //TODO
    }

}
