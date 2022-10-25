using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    [Header("Shooting")]

    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject projectile;
    
    float boneSpeed = 40f;

    [Header("Audio")]

    AudioSource playerAudio;
    [SerializeField] AudioClip trowBoneAudio;
    [SerializeField] public AudioClip biteAudio;
    [SerializeField] public AudioClip gameOverAudio;



    private void Start()
    {
        // Audio
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Player Input
        if (MainManager.Instance.IsGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
        {
                Shoot(boneSpeed, projectile);
        }
            if (Input.GetKeyDown(KeyCode.P))
            {

                // DEBUG test to reset score
                MainManager.Instance.SaveHighScore(0, "Test");
                MainManager.Instance.LoadHighScore();

                Debug.Log("Highscore reset. HS: " + MainManager.Instance.HighScore + ", Name: " + MainManager.Instance.HighScoreName);
            }

        }

    }

    public void GetBiten()
    {
        playerAudio.PlayOneShot(biteAudio);
        gameManager.PlayerHit();
    }

    public void PlayAudioOnce(AudioClip clip)
    {
        playerAudio.PlayOneShot(clip);
    }

    private void Shoot(float shootSpeed, GameObject projectile)
    {
        // Instanciates the bullet and saves it in a variable
        GameObject bullet = Instantiate(projectile, shootPoint.position, shootPoint.rotation);

        // Adds velocity to such instantiated projectile RigidBody
        bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * shootSpeed;

        // Sound FX - Trow bone
        PlayAudioOnce(trowBoneAudio);
    }
}
