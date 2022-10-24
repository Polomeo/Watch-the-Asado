using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

        }

    }

    public void GetBiten()
    {
        playerAudio.PlayOneShot(biteAudio);
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
