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

    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;


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
            StartThursting();

        }
        else
        {
            StopThursting();
        }
    }

    private void StartThursting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
    }

    private void StopThursting()
    {
        audioSource.Stop();
        mainBoosterParticle.Stop();
    }

    

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotation.IsPressed())
        {
            StartRotation(rotationInput);
        }
        else
        {
            StopRotation();
        }


    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    private void StartRotation(float rotationInput)
    {
        //Debug.Log(rotationInput);
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
            if (!rightBoosterParticle.isPlaying)
            {
                leftBoosterParticle.Stop();
                rightBoosterParticle.Play();
            }
        }

        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            if (!leftBoosterParticle.isPlaying)
            {
                rightBoosterParticle.Stop();
                leftBoosterParticle.Play();
            }
        }
    }
    
    private void StopRotation()
    {
        rightBoosterParticle.Stop();
        leftBoosterParticle.Stop();
    }

    

   
}