using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed = 0.0f;

    [Header("Interact Settings")]
    [SerializeField] float m_interactRadius = 0.0f;
    [SerializeField] LayerMask m_interactMask;

    Rigidbody2D m_rb = null;
    Animator m_animator = null;
    Vector2 m_input = Vector2.zero;

    readonly int m_moveFlagHash = Animator.StringToHash("MoveFlag");

    void Start()
    {
        Setting();
    }

    void Update()
    {
        if (GManager.Instance.IsBoundaryBattleFlag) return;

        HandleInput();
        HandleInteract();
    }

    void FixedUpdate()
    {
        if (GManager.Instance.IsBoundaryBattleFlag) return;

        Move();
    }

    void Setting()
    {
        m_rb = transform.GetComponent<Rigidbody2D>();
        m_animator = transform.Find("ViewObj").GetComponent<Animator>();
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
            m_animator.SetBool(m_moveFlagHash, false);
            m_rb.linearVelocity = Vector2.zero;
            return;
        }

        m_animator.SetBool(m_moveFlagHash, true);
        m_rb.linearVelocity = m_moveSpeed * m_input;
    }

    void HandleInteract()
    {
        if (GManager.Instance.IsBoundaryBattleFlag) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            // 레이어 체크해서 npc면 Interact 시작
            Collider2D _hit = Physics2D.OverlapCircle(transform.position, m_interactRadius, m_interactMask);
            if (_hit != null && _hit.TryGetComponent(out InteractNPC _sc))
            {
                GManager.Instance.BoundaryBattleStart(_sc.IsStageData);
            }
        }
    }
}