using System.Collections;
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
    private float _jumpElapsedTime;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _jumpDuration = 1.5f;
    [SerializeField] private float _minReachDistance = 0.1f;
    private bool _isJumping;

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
        if (_navMeshAgent.isOnOffMeshLink && !_isJumping)
        {
            StartCoroutine(JumpWithAnimation());
        }
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

    private IEnumerator JumpWithAnimation()
    {
        _anim.SetBool("IsJump", true);
        _isJumping = true;
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
        var endPos = _navMeshAgent.currentOffMeshLinkData.endPos;
        var jumpDir = endPos - transform.position;
        jumpDir.y = 0f;
        transform.rotation = Quaternion.LookRotation(jumpDir);
        while (_isJumping)
        {
            yield return null;
        }
        _navMeshAgent.CompleteOffMeshLink();
        // transform.position = endPos;
        _navMeshAgent.nextPosition = transform.position;
        _navMeshAgent.updatePosition = true;
        _navMeshAgent.updateRotation = true;
    }

    private IEnumerator Jump()
    {
        _isJumping = true;
        var offMeshData = _navMeshAgent.currentOffMeshLinkData;
        var startPos = transform.position;
        var endPos = offMeshData.endPos;
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;

        while (_jumpElapsedTime < _jumpDuration)
        {
            var t = _jumpElapsedTime / _jumpDuration;
            var height = Mathf.Sin(Mathf.PI * t) * _jumpHeight;
            var pos = Vector3.Lerp(startPos, endPos, t);
            pos.y += height;

            transform.position = pos;
            _jumpElapsedTime += Time.deltaTime;
            yield return null;
        }

        _navMeshAgent.CompleteOffMeshLink();
        _navMeshAgent.updatePosition = true;
        _navMeshAgent.updateRotation = true;
        _isJumping = false;
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

    public void OnLanding()
    {
        _isJumping = false;
        _anim.SetBool("IsJump", false);
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
        if (_isJumping)
        {
            transform.position += _anim.deltaPosition;
            transform.rotation *= _anim.deltaRotation;
        }
    }
}
