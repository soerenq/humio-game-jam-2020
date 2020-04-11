using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spider : MonoBehaviour
{
    const float SPEED = 50f; // Per second

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
        rt.Translate(Time.deltaTime * SPEED * curVelo);
        curVelo *= Mathf.Pow(0.85f, Time.deltaTime * 50f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Spider hit {0}", other);
        var otherGO = other.gameObject;
        var player = other.GetComponent<BasementPlayer>();
        if (player != null)
        {
            player.OnTouchedBySpider();
        }
    }
}
