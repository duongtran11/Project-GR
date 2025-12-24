using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    private Animator _anim;
    private NavMeshAgent _navMeshAgent;
    private AgentController _player;
    public string Name { get; }
    public float Health { get; set; }

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = PlayerManager.Instance.Player;
    }
    void Update()
    {
        if (_player != null)
        {
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {
        _navMeshAgent.destination = _player.transform.position;
    }
    public void OnDamage(float damageAmount)
    {
        Health -= damageAmount;
        Debug.Log("Get shot!");
    }
    void OnAnimatorMove()
    {
        _navMeshAgent.speed = (_anim.deltaPosition / Time.deltaTime).magnitude;
    }
}
