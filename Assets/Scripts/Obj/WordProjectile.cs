using TMPro;
using UnityEngine;

public class WordProjectile : MonoBehaviour
{
    [SerializeField] TextMeshPro m_text = null;

    float m_moveSpeed = 0.0f;
    Vector3 m_moveDir = Vector3.zero;

    /// <summary>
    /// 프로젝타일 셋팅
    /// </summary>
    /// <param name="argDir"></param>
    public void Setting(Vector3 argDir, float argSpeed, string argStr)
    {
        m_moveDir = argDir;
        m_moveSpeed = argSpeed;
        m_text.text = argStr;
    }

    void Update()
    {
        transform.position += m_moveSpeed * Time.deltaTime * m_moveDir;
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

            PoolManager.Instance.Return(gameObject);
        }
    }
}