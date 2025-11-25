using UnityEngine;

public class AgentController : MonoBehaviour
{
    private Movement _movement;
    private Weapon _weapon;
    public bool IsHandGun { get; set; }
    void Awake()
    {
        _movement = GetComponent<Movement>();
        _weapon = GetComponent<Weapon>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IsHandGun = !IsHandGun;
            if (IsHandGun)
            {
                _weapon.DrawHandGun();
            }
            else
            {
                _weapon.PutAwayHandGun();
            }

            return;
        }
    }
}