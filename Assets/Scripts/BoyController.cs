using System;
using Humio;
using UnityEngine;

public class BoyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D boy;
    [SerializeField] private int jumpHeight = 100;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip scream;
    [SerializeField] private BoxCollider2D coffeBeansCollider;
    
    private bool _isActive;
    private SpriteRenderer _spriteInstance;
    private Vector3 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _spriteInstance = gameObject.GetComponent<SpriteRenderer>();
        _initialPosition = _spriteInstance.transform.position;
        
        // here be dragons
        coffeBeansCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isActive)
        {
            _spriteInstance.transform.position = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(10);
        }
    }

    void OnMouseDown()
    {
        //Debug.Log($"Activated pantry controller. Current position = {_spriteInstance.transform.position}");
        _isActive = true;
    }

    private void OnMouseUp()
    {
        //Debug.Log($"Released pantry controller Current position = {_spriteInstance.transform.position}");
        _isActive = false;

        JumpTowardPoint();
        
        //Debug.Log($"Resetting position to position = {_initialPosition}");
        _spriteInstance.transform.position = _initialPosition;
    }
    
    void JumpTowardPoint()
    {
        var initialVelocity = CalculateJumpSpeed();
        var direction = CalculateDirection();
 
        boy.AddForce(initialVelocity * direction, ForceMode2D.Impulse);
        
        audioPlayer.PlayOneShot(scream);
    }

    private Vector3 CalculateDirection()
    {
        return _initialPosition - _spriteInstance.transform.position;
    }

    private float CalculateJumpSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude);
    }
}