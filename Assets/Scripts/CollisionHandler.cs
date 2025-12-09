using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay;
    [SerializeField] AudioClip crashSound; 
    [SerializeField] AudioClip succsessSound; 

    AudioSource audioSource;

    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(!isControllable){return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("Friendly");
            break;

            case "Fuel":
            Debug.Log("Fuel");
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
        audioSource.Stop();
        audioSource.PlayOneShot(succsessSound);
        GetComponent<Movement>().enabled = false;  
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isControllable = false;
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
}
