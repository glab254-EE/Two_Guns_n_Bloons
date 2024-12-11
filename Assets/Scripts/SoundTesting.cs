using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundTesting : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private Slider M_slider;
    [SerializeField] private Slider SFX_slider;
    [SerializeField] private Slider OST_slider;
    [SerializeField] private GameObject ShootSource;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private float MoveSpeed = 1.0f;
    [SerializeField] private float ShootStrenghts = 25;
    [SerializeField] private float MaxOffset = 1.5f;
    [SerializeField] private float MaxDistort = 1f;
    [SerializeField] private float MinDistort =0.5f;
    [SerializeField] private float ChangeSpeed = 0.5f;

    private Vector3 currentpos;
    private float distortion = 0.5f; // MDistort
    private void Shoot()
    {
        if (Projectile != null)
        {
            if (ShootSource.TryGetComponent<AudioSource>(out AudioSource audioSource))
            {
                float pan = Math.Clamp(currentpos.z / MaxOffset, -1, 1);
                audioSource.panStereo = pan;
                audioSource.Play();
            }
            GameObject newobject = Instantiate(Projectile, ShootSource.transform.position, ShootSource.transform.rotation);
            newobject.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0) * ShootStrenghts, ForceMode.VelocityChange);

            Destroy(newobject, 5f);
        }
    }
    public void TemporaryDistort()
    {
        distortion = MaxDistort;
    }
    void Start()
    {
        currentpos = ShootSource.transform.parent.position;
    }

    void Update()
    {
        if (distortion > MinDistort)
        {
            distortion = Math.Clamp(distortion-Time.deltaTime*ChangeSpeed, MinDistort, MaxDistort);
        }
        if (AudioMixer != null)
        {
            AudioMixer.SetFloat("MVolume",Math.Clamp(M_slider.value, -80f, 0f));
            AudioMixer.SetFloat("MusicVolume", Math.Clamp(OST_slider.value, -80f, 0f));
            AudioMixer.SetFloat("SFXVolume", Math.Clamp(SFX_slider.value, -80f, 0f));
            AudioMixer.SetFloat("MDistort", distortion);
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentpos.z = Math.Clamp(currentpos.z - MoveSpeed * Time.deltaTime, -MaxOffset, MaxOffset);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentpos.z = Math.Clamp(currentpos.z + MoveSpeed * Time.deltaTime, -MaxOffset, MaxOffset);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        ShootSource.transform.parent.position = currentpos;
    }
}
