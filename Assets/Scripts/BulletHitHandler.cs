using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class BulletHitHandler : MonoBehaviour
{
    [SerializeField] private int Damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && (collision.gameObject.layer == 6 || collision.gameObject.layer == 7))
        {
            if (collision.gameObject.layer == 7 && collision.gameObject.TryGetComponent<EnemyHandler>(out EnemyHandler enemy))
            {
                if (enemy.Power > Damage)
                {
                    enemy.Power -= Damage;
                }
                else
                {
                    StartCoroutine(enemy.DestroyEN());
                    AudioMixer audi = Resources.Load<AudioMixer>("MainMixer");
                    if (audi != null)
                    {
                        audi.SetFloat("MDistort", 0.7f);
                    }
                }
            }
            if (gameObject.TryGetComponent<AudioSource>(out AudioSource source))
            {
                source.Play();
                Destroy(gameObject,0.1f);
            }
            else
            {
                Destroy(gameObject);
            }
        } 
    }
}
