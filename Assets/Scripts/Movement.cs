using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    
    [SerializeField] float thrustStrength;
    [SerializeField] float rotationStrength;

    [SerializeField] AudioClip mainEngine;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }


    void Update()
    {
        ProcessThurst();
        ProcessRotation();
    }


    private void ProcessThurst()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }     
        }
        else
        {
            audioSource.Stop();
        }
    }


    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotation.IsPressed())
        {
            //Debug.Log(rotationInput);
            if(rotationInput < 0)
            {
                ApplyRotation(rotationStrength);
            }

            else if(rotationInput > 0)
            {
                ApplyRotation(-rotationStrength);
            }
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}