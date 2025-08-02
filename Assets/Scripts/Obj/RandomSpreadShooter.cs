using UnityEngine;

public class RandomSpreadShooter : MonoBehaviour
{
    [Header("발사체 설정")]
    [SerializeField] GameObject m_projectilePrefab = null;
    [SerializeField] Vector2 m_moveSpeed = Vector2.zero;

    [Header("발사 타입 (YSpread = 좌우, XSpread = 상하)")]
    [SerializeField] ShooterType.TYPE m_shooterType = ShooterType.TYPE.None;

    [Header("반전 플래그(true일때 YSpread = Left, XSpread = Down)")]
    [SerializeField] bool m_reverseFlag = false;
    [SerializeField] float m_maxShootTime = 0.0f;
    [SerializeField] float m_xSpread = 0.0f;
    [SerializeField] float m_ySpread = 0.0f;

    float m_curShootTime = 0.0f;

    Vector3 m_moveDir = Vector3.zero;
    Vector3 m_spawnPos = Vector3.zero;
    Vector3 m_spawnOffset = Vector3.zero;

    public void Tick()
    {
        if (!GManager.Instance.IsBoundaryBattleFlag) return;

        m_curShootTime -= Time.deltaTime;

        if (m_curShootTime <= 0.0f)
        {
            m_curShootTime = m_maxShootTime;
            RandomShoot();
        }
    }

    void RandomShoot()
    {
        switch (m_shooterType)
        {
            case ShooterType.TYPE.YSpread:
                m_moveDir = m_reverseFlag ? Vector3.right : Vector3.left;
                m_spawnOffset = new Vector3(0.0f, Random.Range(-m_ySpread * 0.5f, m_ySpread * 0.5f), 0.0f);
                break;
            case ShooterType.TYPE.UpSpread:
            case ShooterType.TYPE.DownSpread:
                m_moveDir = m_reverseFlag ? Vector3.up : Vector3.down;
                m_spawnOffset = new Vector3(Random.Range(-m_xSpread * 0.5f, m_xSpread * 0.5f), 0.0f, 0.0f);
                break;
        }

        m_spawnPos = transform.position + m_spawnOffset;

        GameObject _obj = PoolManager.Instance.Get(m_projectilePrefab);
        _obj.transform.position = m_spawnPos;
        _obj.GetComponent<Projectile>().Setting(m_moveDir, RandomSpeed(), BattleManager.Instance.GetRandomWord());
        BattleManager.Instance.IsProjectileList.Add(_obj);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        // 중심 위치
        Vector3 _center = transform.position;

        // 스폰 범위를 사각형으로 그리기
        Vector3 _size = new Vector3(m_xSpread, m_ySpread, 0.1f);
        Gizmos.DrawWireCube(_center, _size);

        // (선택) 중심 위치 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_center, 0.1f);
    }

    /// <summary>
    /// 속도 범위내에서 랜덤하게 설정
    /// </summary>
    /// <returns></returns>
    float RandomSpeed()
    {
        return Random.Range(m_moveSpeed.x, m_moveSpeed.y);
    }
}
