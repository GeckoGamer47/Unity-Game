using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement8Dir : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed=5f;

    [Header("Dodge Settings")]
    [SerializeField]private float DodgeSpeed = 20f;
    [SerializeField]private float DodgeDuration=1f;
    [SerializeField]private float DodgeCooldown=2f;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;


    private bool _isDodgeing =false;
    private bool _canDodge=true;
    private Vector2 _DodgeDirection;
    private float _DodgeTimer =0f;
    private float _cooldownTimer=0f;

    private void Awake()
    {
        _rb=GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        _moveInput=value.Get<Vector2>();
    }

    public void OnDodge(InputValue value)
    {
        if (value.isPressed && _canDodge && _moveInput != Vector2.zero)
        {
            _isDodgeing=true;
            _canDodge=false;
            _DodgeDirection=_moveInput.normalized;
            _DodgeTimer = DodgeDuration;
            _cooldownTimer=DodgeCooldown;
        }
    }

    private void FixedUpdate()
    {
        if (_isDodgeing)
        {
            _rb.linearVelocity=_DodgeDirection*DodgeSpeed;
            _DodgeTimer-=Time.fixedDeltaTime;

            if (_DodgeTimer <= 0f)
            _isDodgeing = false;
        }
        else
        {
            {
                _rb.linearVelocity=_moveInput.normalized*moveSpeed;

                if (!_canDodge)
                {
                    _cooldownTimer-=Time.fixedDeltaTime;

                    if (_cooldownTimer<=0f)
                        _canDodge=true;
                }
            }
        }
    }
}