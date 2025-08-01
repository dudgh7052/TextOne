using UnityEngine;
using System.Collections.Generic;

public class RandomSpreadShooter : MonoBehaviour
{
    [Header("�߻�ü ����")]
    [SerializeField] GameObject m_projectilePrefab = null;
    [SerializeField] Vector2 m_moveSpeed = Vector2.zero;

    [Header("���� ����")]
    [SerializeField] bool m_xSpreadFlag = false;
    [SerializeField] bool m_rightMoveFlag = false;
    [SerializeField] float m_maxShootTime = 0.0f;
    [SerializeField] float m_xSpread = 0.0f;
    [SerializeField] float m_ySpread = 0.0f;

    float m_curShootTime = 0.0f;

    Vector3 m_spawnPos = Vector3.zero;
    Vector3 m_spawnOffset = Vector3.zero;

    void Update()
    {
        if (!GManager.Instance.IsBoundaryBattleFlag) return;

        // ���߿� �Ѳ����� ����
        Tick();
    }

    void Tick()
    {
        m_curShootTime -= Time.deltaTime;

        if (m_curShootTime <= 0.0f)
        {
            m_curShootTime = m_maxShootTime;
            RandomShoot();
        }
    }

    void RandomShoot()
    {
        if (m_xSpreadFlag) m_spawnOffset = new Vector3(Random.Range(-m_xSpread * 0.5f, m_xSpread * 0.5f), 0.0f, 0.0f);
        else m_spawnOffset = new Vector3(0.0f, Random.Range(-m_ySpread * 0.5f, m_ySpread * 0.5f), 0.0f);

        m_spawnPos = transform.position + m_spawnOffset;

        GameObject _obj = PoolManager.Instance.Get(m_projectilePrefab);

        _obj.transform.position = m_spawnPos;
        _obj.GetComponent<Projectile>().Setting(m_rightMoveFlag ? Vector3.right : Vector3.left, RandomSpeed(), "Testt");
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

    float RandomSpeed()
    {
        return Random.Range(m_moveSpeed.x, m_moveSpeed.y);
    }
}
