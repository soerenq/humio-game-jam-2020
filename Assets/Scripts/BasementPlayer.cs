using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rt = (RectTransform) this.transform;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rt.localRotation = Quaternion.Euler(0f, 0f, 0f);
            rt.Translate(1f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rt.localRotation = Quaternion.Euler(0f, 0f, 180f);
            rt.Translate(1f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rt.localRotation = Quaternion.Euler(0f, 0f, 90f);
            rt.Translate(1f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rt.localRotation = Quaternion.Euler(0f, 0f, 270f);
            rt.Translate(1f, 0f, 0f);
        }
    }
}
