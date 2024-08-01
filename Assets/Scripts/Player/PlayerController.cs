using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;

    [Space]
    [SerializeField] private float _speedMove;

    public bool CanMove { get; private set; } = true;

    private const string NAME_ANIM_PARAM_WALK = "speedMove";

    private void Awake()
    {

    }

    public void Start()
    {

    }

    private void FixedUpdate()
    {
        UpdateAnim();

        if (CanMove == false || _joystick.Direction == Vector2.zero) { return; }

        SetMove();
    }

    private void SetMove()
    {
        _rigidbody.velocity = new Vector3(_joystick.Direction.x * _speedMove, _rigidbody.velocity.y, _joystick.Direction.y * _speedMove);

        Vector3 direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _rigidbody.rotation = targetRotation;
        }
    }

    private void UpdateAnim()
    {
        float speedX = Mathf.Abs(_rigidbody.velocity.x);
        float speedZ = Mathf.Abs(_rigidbody.velocity.z);

        float speed = Mathf.Max(speedX, speedZ);

        _animator.SetFloat(NAME_ANIM_PARAM_WALK, speed / 2);
    }
}
