//using TMPro;
using UnityEngine;

public class WordProjectile : MonoBehaviour
{
    //[SerializeField] TextMeshPro m_text = null;
    [SerializeField] float m_moveSpeed = 5.0f;

    bool m_settingFlag = false;

    /// <summary>
    /// 프로젝타일 셋팅
    /// </summary>
    /// <param name="argDir"></param>
    public void Setting(string argStr)
    {
        //m_text.text = argStr;
        m_settingFlag = true;
    }

    void Update()
    {
        if (!m_settingFlag) return;

        transform.position = Vector2.MoveTowards(transform.position, GManager.Instance.IsBoundaryPlayerT.position, m_moveSpeed * Time.deltaTime);

        if (transform.position == GManager.Instance.IsBoundaryPlayerT.position)
        {
            GManager.Instance.StartEndDialogue();
            PoolManager.Instance.Return(gameObject);
        }
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GManager.Instance.IsBoundaryBattleFlag)
            {
                GManager.Instance.StartEndDialogue();
            }

            PoolManager.Instance.Return(gameObject);
        }
    }
    */
}