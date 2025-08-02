using UnityEngine;

public class RandomSpreadShooter : MonoBehaviour
{
    [Header("�߻�ü ����")]
    [SerializeField] GameObject m_projectilePrefab = null;
    [SerializeField] Vector2 m_moveSpeed = Vector2.zero;

    [Header("�߻� Ÿ�� (YSpread = �¿�, XSpread = ����)")]
    [SerializeField] ShooterType.TYPE m_shooterType = ShooterType.TYPE.None;

    [Header("���� �÷���(true�϶� YSpread = Left, XSpread = Down)")]
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

        // �߽� ��ġ
        Vector3 _center = transform.position;

        // ���� ������ �簢������ �׸���
        Vector3 _size = new Vector3(m_xSpread, m_ySpread, 0.1f);
        Gizmos.DrawWireCube(_center, _size);

        // (����) �߽� ��ġ ǥ��
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_center, 0.1f);
    }

    /// <summary>
    /// �ӵ� ���������� �����ϰ� ����
    /// </summary>
    /// <returns></returns>
    float RandomSpeed()
    {
        return Random.Range(m_moveSpeed.x, m_moveSpeed.y);
    }
}
