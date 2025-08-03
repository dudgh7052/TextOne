using UnityEngine;

public class TargetProjectile : MonoBehaviour
{
    Vector3 m_targetPos;
    float m_moveSpeed = 5.0f;
    bool m_activeFlag = false;

    /// <summary>
    /// 타겟 위치 설정 (외부에서 호출)
    /// </summary>
    public void Setting(Vector3 argTargetPos, float argSpeed)
    {
        m_targetPos = argTargetPos;
        m_moveSpeed = argSpeed;
        m_activeFlag = true;
    }

    void Update()
    {
        if (!m_activeFlag) return;

        transform.position = Vector2.MoveTowards(transform.position, m_targetPos, m_moveSpeed * Time.deltaTime);

        if (transform.position == m_targetPos)
        {
            m_activeFlag = false;
            PoolManager.Instance.Return(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GManager.Instance.IsBoundaryBattleFlag)
            {
                GManager.Instance.TakeDamage(1);
            }

            PoolManager.Instance.Return(gameObject);
        }
    }
}