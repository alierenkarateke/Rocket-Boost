using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void OnEnable()
    {
        thrust.Enable();
    }

    void Update()
    {
        if(thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
    }
}
