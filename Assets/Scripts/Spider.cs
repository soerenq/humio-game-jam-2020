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

    // Update is called once per frame
    void Update()
    {
        var rt = (RectTransform) this.transform;
        var rb = GetComponent<Rigidbody2D>();
        if (rb.velocity.magnitude < 0.15f * SPEED)
        {
            Vector3 newVelo;
            // Find new velocity.
            var toTarget = (target.position - rt.position);
            if (toTarget.magnitude < 150)
            {
                // Close by...
                var direction = toTarget.normalized;
                newVelo = 4f * direction;
            }
            else
            {
                newVelo = 5f * Random.insideUnitSphere;
                newVelo.z = 0f;
            }

            rb.velocity = SPEED * newVelo;
        }
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
