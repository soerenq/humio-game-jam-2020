using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public RectTransform target;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rt = (RectTransform) this.transform;
        var direction = (target.position - rt.position).normalized;
        rt.Translate(0.8f * direction);
    }
}
