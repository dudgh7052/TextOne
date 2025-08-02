using TMPro;
using UnityEngine;

public class TargetWordProjectile : MonoBehaviour
{
    [SerializeField] TextMeshPro m_text = null;
    [SerializeField] float m_moveSpeed = 5.0f;

    Vector3 m_targetPos;
    bool m_activeFlag = false;

    /// <summary>
    /// 타겟 위치 설정 (외부에서 호출)
    /// </summary>
    public void Setting(Vector3 argTargetPos, float argSpeed, string argStr)
    {
        m_text.text = argStr;
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
                GManager.Instance.StartEndDialogue();
                BattleManager.Instance.InitBattle();
            }

            m_activeFlag = false;
            PoolManager.Instance.Return(gameObject);
        }
    }
}