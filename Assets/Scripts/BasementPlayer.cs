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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rt.localRotation = Quaternion.Euler(0f, 0f, 0f);
            MoveAhead(rt);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rt.localRotation = Quaternion.Euler(0f, 0f, 180f);
            MoveAhead(rt);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rt.localRotation = Quaternion.Euler(0f, 0f, 90f);
            MoveAhead(rt);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rt.localRotation = Quaternion.Euler(0f, 0f, 270f);
            MoveAhead(rt);
        }
    }

    private static void MoveAhead(RectTransform rt)
    {
        rt.Translate(SPEED * Time.deltaTime, 0f, 0f);
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
