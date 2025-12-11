using System;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay;

    [SerializeField] AudioClip crashSound; 
    [SerializeField] AudioClip succsessSound; 
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem succsessParticle;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(!isControllable || !isCollidable){return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
            //Debug.Log("Friendly");
            break;

            case "Fuel":
            //Debug.Log("Fuel");
            break;
            
            case "Finish":
            StartSuccessSequence();
            break;

            default:
            StartCrashSequence();
            break;
        }
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        succsessParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(succsessSound);
        GetComponent<Movement>().enabled = false;  
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        crashParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",2f);
    }

    void ReloadLevel()
    {
        int currentScene =  SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    
    void LoadNextLevel()
    {
        int currentScene =  SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.isPressed)
        {
            LoadNextLevel();
            
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
            Debug.Log("c key pressed ");
        }
    }
}
