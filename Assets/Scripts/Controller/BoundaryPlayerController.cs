using UnityEngine;

public class BoundaryPlayerController : MonoBehaviour
{
    [Header("이동속도")]
    [SerializeField] float m_moveSpeed = 0.0f;

    Rigidbody2D m_rb = null;
    //Animator m_animator = null;
    Vector2 m_input = Vector2.zero;

    //readonly int m_moveFlagHash = Animator.StringToHash("MoveFlag");

    void OnEnable()
    {
        transform.localPosition = Vector3.zero;
    }

    void Start()
    {
        Setting();
    }

    void Update()
    {
        if (!GManager.Instance.IsBoundaryBattleFlag) return;

        HandleInput();
    }

    void FixedUpdate()
    {
        if (!GManager.Instance.IsBoundaryBattleFlag) return;

        Move();
    }

    void Setting()
    {
        m_rb = transform.GetComponent<Rigidbody2D>();
        //m_animator = transform.Find("ViewObj").GetComponent<Animator>();
    }

    void HandleInput()
    {
        m_input.x = Input.GetAxisRaw("Horizontal");
        m_input.y = Input.GetAxisRaw("Vertical");
        m_input.Normalize();
    }

    void Move()
    {
        if (m_input == Vector2.zero)
        {
            //m_animator.SetBool(m_moveFlagHash, false);
            m_rb.linearVelocity = Vector2.zero;
            return;
        }

        //m_animator.SetBool(m_moveFlagHash, true);
        m_rb.linearVelocity = m_moveSpeed * m_input;
    }
}
