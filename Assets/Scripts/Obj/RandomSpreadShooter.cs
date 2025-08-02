using System.Collections;
using UnityEngine;

public class RandomSpreadShooter : MonoBehaviour
{
    [Header("발사 개수 설정")]
    [SerializeField] Vector2Int m_shootCount = Vector2Int.zero;

    [Header("발사체 설정")]
    [SerializeField] GameObject m_projectilePrefab = null;
    [SerializeField] Vector2 m_moveSpeed = Vector2.zero;

    [Header("좌우 반전 플래그(true: 왼쪽으로 발사)")]
    [SerializeField] bool m_reverseFlag = false;
    [SerializeField] float m_ySpread = 0.0f;
    [SerializeField] float m_shootDelay = 0.0f;

    Vector3 m_moveDir = Vector3.zero;
    Vector3 m_spawnPos = Vector3.zero;
    Vector3 m_spawnOffset = Vector3.zero;

    Coroutine m_shootCoroutine = null;

    public void StartShooting()
    {
        if (m_shootCoroutine != null) StopCoroutine(m_shootCoroutine);
        m_shootCoroutine = StartCoroutine(RandomShootCoroutine());
    }

    IEnumerator RandomShootCoroutine()
    {
        int _randomCount = Random.Range(m_shootCount.x, m_shootCount.y + 1);

        for (int i = 0; i < _randomCount; i++)
        {
            if (!GManager.Instance.IsBoundaryBattleFlag) break;

            RandomShoot();
            yield return new WaitForSeconds(m_shootDelay);
        }

        yield return new WaitForSeconds(1.0f);
        GManager.Instance.CreateWord();

        m_shootCoroutine = null;
    }

    void RandomShoot()
    {
        m_moveDir = m_reverseFlag ? Vector3.left : Vector3.right;
        m_spawnOffset = new Vector3(0.0f, Random.Range(-m_ySpread * 0.5f, m_ySpread * 0.5f), 0.0f);
        m_spawnPos = transform.position + m_spawnOffset;

        GameObject _obj = PoolManager.Instance.Get(m_projectilePrefab);
        _obj.transform.position = m_spawnPos;
        _obj.GetComponent<Projectile>().Setting(m_moveDir, RandomSpeed());

        BattleManager.Instance.IsSpawnedProjectileList.Add(_obj);
    }

    /// <summary>
    /// 속도 범위내에서 랜덤하게 설정
    /// </summary>
    /// <returns>속도</returns>
    float RandomSpeed()
    {
        return Random.Range(m_moveSpeed.x, m_moveSpeed.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector3 _center = transform.position;
        Vector3 _size = new Vector3(0.1f, m_ySpread, 0.1f);
        Gizmos.DrawWireCube(_center, _size);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_center, 0.1f);
    }
}
