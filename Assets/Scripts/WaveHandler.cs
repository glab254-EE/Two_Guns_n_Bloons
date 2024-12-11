using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WaveHandler : MonoBehaviour
{
    [field:SerializeField] public Wave[] Waves { get; private set; }
    [SerializeField] private int CurrentWave = 0;
    [SerializeField] private GameObject PrefabEnemy;
    public AudioMixer AudioMixer;
    [SerializeField] float _nextwavetime = 0;
    private GameObject getrandompoint()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Respawn");
        int random = UnityEngine.Random.Range(0, objects.Length);
        return objects[random];
    }
    private IEnumerator spawnenemies(Wave wave)
    {
        if (wave != null)
        {
            for (int i = 0; i < wave.EnemiesToSpawn.Count; i++)
            {
                GameObject point = getrandompoint();
                GameObject enemy = Instantiate(PrefabEnemy, point.transform.position,Quaternion.identity);
                enemy.GetComponent<EnemyHandler>().Power = wave.EnemiesToSpawn[i]; // there will be only one enmy with that class.
                yield return new WaitForSeconds(wave.Cooldown);
            }
        }
    }
    void spawnwave()
    {
        Wave wave = Waves[CurrentWave];
        if (wave!= null)
        {
            StartCoroutine(spawnenemies(wave));
            CurrentWave = Math.Clamp(CurrentWave + 1, 0, wave.EnemiesToSpawn.Count-1);
            _nextwavetime = wave.NextWaveTime;
        }
    }
    void Start()
    {
        CurrentWave = 0;
    }

    void Update()
    {
        if (AudioMixer.GetFloat("MDistort", out float temp))
        {
            if (temp > 0)
            {
                AudioMixer.SetFloat("MDistort", temp - Time.deltaTime);
            }
            else
            {
                AudioMixer.SetFloat("MDistort", 0);
            }
        }
        if (_nextwavetime < 0)
        {
            spawnwave();
        } else
        {
           _nextwavetime -= Time.deltaTime;
        }
    }
}
[Serializable]
public class Wave 
{
    public float Cooldown = 1;
    public List<int> EnemiesToSpawn = new List<int>();
    public float NextWaveTime = 10;
}
