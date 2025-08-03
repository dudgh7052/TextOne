using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed = 0.0f;

    [Header("Interact Settings")]
    [SerializeField] float m_interactRadius = 0.0f;
    [SerializeField] LayerMask m_interactMask;

    [SerializeField] CapsuleCollider2D m_boundaryCollider; //충돌범위콜라이더
    Bounds m_bounds; 

    bool m_facingRightFlag = true;

    Rigidbody2D m_rb = null;
    Animator m_animator = null;
    Vector2 m_input = Vector2.zero;

    readonly int m_moveFlagHash = Animator.StringToHash("MoveFlag");
    readonly int m_moveXHash = Animator.StringToHash("MoveX");
    readonly int m_moveYHash = Animator.StringToHash("MoveY");

    void Start()
    {
        Setting();
    }

    void Update()
    {
        if (GManager.Instance.IsBoundaryBattleFlag || GManager.Instance.IsGameOverFlag) return;

        HandleInput();
        HandleInteract();
        HandleFlip();
    }

    void FixedUpdate()
    {
        if (GManager.Instance.IsBoundaryBattleFlag || GManager.Instance.IsGameOverFlag) return;

        Move();
    }

    void Setting()
    {
        GManager.Instance.IsPlayerT = transform;
        GManager.Instance.IsPlayerSc = this;

        m_rb = transform.GetComponent<Rigidbody2D>();
        m_animator = transform.Find("ViewObj").GetComponent<Animator>();

        if(m_boundaryCollider != null)//바운더리 설정
        {
            m_bounds = m_boundaryCollider.bounds;
        }
    }

    void HandleInput()
    {
        m_input.x = Input.GetAxisRaw("Horizontal");
        m_input.y = Input.GetAxisRaw("Vertical");
        m_input.Normalize();

        if (m_input == Vector2.zero)
        {
            m_animator.SetBool(m_moveFlagHash, false);
        }
        else
        {
            m_animator.SetBool(m_moveFlagHash, true);
            m_animator.SetFloat(m_moveXHash, m_input.x);
            m_animator.SetFloat(m_moveYHash, m_input.y);
        }
    }

    void Move()
    {
        if (m_input == Vector2.zero)
        {
            m_rb.linearVelocity = Vector2.zero;
            return;
        }
        //캡슐 콜라이더 내 위치 조정
        Vector2 nextPos = m_rb.position + m_moveSpeed * Time.fixedDeltaTime * m_input;
        float clampedX = Mathf.Clamp(nextPos.x, m_bounds.min.x, m_bounds.max.x);
        float clampedY = Mathf.Clamp(nextPos.y, m_bounds.min.y, m_bounds.max.y);
        Vector2 clampedPos = new Vector2(clampedX, clampedY);

        m_rb.MovePosition(clampedPos);
    }

    void HandleFlip()
    {
        if (m_input.x == 0.0f) return;

        if (m_input.x < 0.0f && m_facingRightFlag) Flip();
        else if (m_input.x > 0.0f && !m_facingRightFlag) Flip();
    }

    void Flip()
    {
        m_facingRightFlag = !m_facingRightFlag;
        transform.rotation = Quaternion.Euler(0.0f, m_facingRightFlag ? 0.0f : 180.0f, 0.0f);
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

    public void PlayDeathAnimation()
    {
        m_animator.SetTrigger("Death");
    }
}