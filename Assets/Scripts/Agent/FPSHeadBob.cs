using UnityEngine;

public class FPSHeadBob : MonoBehaviour
{
    [SerializeField] private float _frequency;
    [SerializeField] private float _amplitude;
    private Vector3 _startPos;
    private AgentController _agentController;
    void Awake()
    {
        _agentController = GetComponentInParent<AgentController>();
        _startPos = transform.localPosition;
    }
    void Update()
    {
        if (!_agentController.IsMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, Time.deltaTime);
            return;
        }
        var pos = Vector3.zero;
        pos.y = Mathf.Lerp(pos.y, -Mathf.Sin(Time.time * _frequency) * _amplitude, Time.deltaTime);
        pos.x = Mathf.Lerp(pos.x, Mathf.Cos(Time.time * _frequency / 2) * _amplitude, Time.deltaTime);
        
        transform.localPosition += pos;
    }
}
