using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;


public class EnemyHandler : MonoBehaviour
{
    [field:SerializeField] public Material[] Materials { get; private set; } = new Material[3];
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject Player;
    [SerializeField] internal int Power = 1;
    [SerializeField] private float SpeedMulti = 1.5f;
    [SerializeField] private float BaseSpeed = 1.5f;
    [SerializeField] private Renderer ballonmat;
    private int oldpower;
    private void UpdatePower()
    {
        if (Materials[Power - 1] != null)
        {
            ballonmat.materials[0] = Materials[Power - 1];
            agent.speed = BaseSpeed + SpeedMulti*Power;
        }
    }
    public IEnumerator DestroyEN()
    {
        if (gameObject.TryGetComponent<AudioSource>(out AudioSource audioSource))
        {
            agent.speed = 0;
            audioSource.Play();
            new WaitForSeconds(audioSource.clip.length);
        }
        Destroy(gameObject);
        yield return new WaitForSeconds(1);
    }
    private void Start()
    {
        oldpower = Power;
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        UpdatePower();
        if (Player == null)
        {
            GameObject temp = GameObject.Find("plr");
            Player = temp != null ? temp.gameObject : null;
        }
        agent.SetDestination(Player.transform.position); // not in update since that is not required , stationary
    }
    void Update()
    {
        if (agent.destination != Player.transform.position)
        {
            if (Player == null)
            {
                GameObject temp = GameObject.Find("plr");
                Player = temp != null ? temp.gameObject : null;
            }
            agent.SetDestination(Player.transform.position);
        }
        if (Power != oldpower)
        {
            oldpower = Power;
            UpdatePower();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider && collision.collider.gameObject == Player)
        {
            Destroy(gameObject);
        }
    }
}
