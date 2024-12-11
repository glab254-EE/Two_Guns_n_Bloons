using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] private float maxspread = 0.5f;
    [SerializeField] private GameObject shootorgin;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float ForcePower = 15f;
    [SerializeField] private bool automatic = false;
    [SerializeField] private float CoolDownTime = 0.25f;

    private float _ctcd = 0;
    private void Fire()
    {
        if (shootorgin != null)
        {
            if (shootorgin.TryGetComponent<AudioSource>(out AudioSource source))
            {
                source.Play();
            }
            if (projectile != null)
            {
                GameObject proj = Instantiate(projectile,shootorgin.transform.position, shootorgin.transform.rotation);
                if (proj.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                {
                    rigidbody.velocity = shootorgin.transform.right * ForcePower + new Vector3(UnityEngine.Random.Range(-maxspread, maxspread), UnityEngine.Random.Range(-maxspread, maxspread), UnityEngine.Random.Range(-maxspread, maxspread))*(ForcePower/2);
                }
            }
        }
    }
    void Update()
    {
        if (_ctcd > 0)
        {
            _ctcd -= Time.deltaTime;
        } else
        {
            _ctcd = 0;
        }
        if (automatic && Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if (_ctcd <= 0 && Time.timeScale>0)
            {
                _ctcd = CoolDownTime;
                Fire();
            }
        }
    }
}
