using System.Collections;
using UnityEngine;

public class DoubleRandomSpreadShooter : MonoBehaviour
{
    [Header("발사체 설정")]
    [SerializeField] GameObject m_projectilePrefab = null;
    [SerializeField] Vector2 m_randomMoveSpeed = Vector2.zero;

    [Header("스폰 범위 설정")]
    [SerializeField] float m_ySpread = 5.0f;
    [SerializeField] float m_xOffset = 3.0f;    

    [Header("발사 설정")]
    [SerializeField] float m_shootDelay = 0.3f;
    [SerializeField] Vector2Int m_randomShootCount = Vector2Int.zero;

    Vector3 m_moveDir = Vector3.zero;
    Vector3 m_spawnPos = Vector3.zero;

    Coroutine m_shootCoroutine = null;

    public void StartShooting()
    {
        if (m_shootCoroutine != null) StopCoroutine(m_shootCoroutine);

        m_shootCoroutine = StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        int _randomCount = Random.Range(m_randomShootCount.x, m_randomShootCount.y);

        for (int i = 0; i < _randomCount; i++)
        {
            if (!GManager.Instance.IsBoundaryBattleFlag) break;

            ShootProjectile();
            yield return new WaitForSeconds(m_shootDelay);
        }

        yield return new WaitForSeconds(1.0f);
        GManager.Instance.CreateWord();

        m_shootCoroutine = null;
    }

    void ShootProjectile()
    {
        // Y 위치를 랜덤하게 위나 아래에서 선택
        bool _spawnRightFlag = Random.value > 0.5f;

        // 생성할 x, y 위치 정하기
        float _spawnX = _spawnRightFlag ? transform.position.x + m_xOffset : transform.position.x - m_xOffset;
        float _randomY = Random.Range(-m_ySpread * 0.5f, m_ySpread * 0.5f);

        m_spawnPos = new Vector3(_spawnX, transform.position.y + _randomY, 0.0f);
        m_moveDir = _spawnRightFlag ? Vector3.left : Vector3.right;

        GameObject _obj = PoolManager.Instance.Get(m_projectilePrefab);
        _obj.transform.position = m_spawnPos;
        _obj.GetComponent<Projectile>().Setting(m_moveDir, RandomSpeed());

        BattleManager.Instance.IsSpawnedProjectileList.Add(_obj);
    }

    float RandomSpeed()
    {
        return Random.Range(m_randomMoveSpeed.x, m_randomMoveSpeed.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        // 왼쪽, 오른쪽 스폰 영역 중심 계산
        Vector3 _centerLeft = transform.position + Vector3.left * m_xOffset;
        Vector3 _centerRight = transform.position + Vector3.right * m_xOffset;

        // Y축으로 퍼지는 높이만큼 박스 크기 설정
        Vector3 _size = new Vector3(0.3f, m_ySpread * 2.0f, 0.1f);

        Gizmos.DrawWireCube(_centerLeft, _size);
        Gizmos.DrawWireCube(_centerRight, _size);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
