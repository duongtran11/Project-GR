using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    private Animator _anim;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private AgentController _player;
    [SerializeField] private List<Transform> _waypointList = new();
    private Vector3 _currentDestination;
    private int _currentIndex;
    [SerializeField] private float _minReachDistance = 0.1f;
    public string Name { get; }
    public float Health { get; set; }

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentDestination = _waypointList[_currentIndex].position;
        _navMeshAgent.destination = _currentDestination;
        // _player = PlayerManager.Instance.Player;
    }
    void Update()
    {
        // if (_player != null)
        // {
        //     FollowPlayer();
        // }
        Patrol();
    }

    private void Patrol()
    {
        var distanceToDestination = (_currentDestination - transform.position).magnitude;
        if (distanceToDestination <= _minReachDistance)
        {
            if (_currentIndex < _waypointList.Count - 1)
            {
                _currentIndex++;
                _currentDestination = _waypointList[_currentIndex].position;
                _navMeshAgent.destination = _currentDestination;
            }
            else
            {
                ResetPath();
            }
        }
    }

    private void ResetPath()
    {
        _currentIndex = 0;
        for (int i = 0; i < _waypointList.Count / 2; i++)
        {
            var temp = _waypointList[i].position;
            _waypointList[i].position = _waypointList[_waypointList.Count - 1 - i].position;
            _waypointList[_waypointList.Count - 1 - i].position = temp;
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
