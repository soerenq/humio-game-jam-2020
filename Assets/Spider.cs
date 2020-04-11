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

    private Vector3 curVelo = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        var rt = (RectTransform) this.transform;
        if (curVelo.magnitude < 0.15f)
        {
            // Find new velocity.
            var toTarget = (target.position - rt.position);
            if (toTarget.magnitude < 150)
            {
                // Close by...
                var direction = toTarget.normalized;
                curVelo = 4f * direction;
            }
            else
            {
                curVelo = 5f * Random.insideUnitSphere;
                curVelo.z = 0f;
            }
        }
        rt.Translate(curVelo);
        curVelo *= 0.85f;
    }
}
