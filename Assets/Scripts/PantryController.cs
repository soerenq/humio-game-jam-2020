using UnityEngine;

public class PantryController : MonoBehaviour
{
    private bool _isActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            _isActive = false;
        }

        if(_isActive)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
            // if you want to smooth movement then lerp it
        }
    }
    
    void OnMouseDown()
    {
        _isActive = true;
    }
}