using UnityEngine;

public class SinProjectile : MonoBehaviour
{
    [Header("파동 진폭")]
    [SerializeField] float m_waveAmplitude = 1.0f;

    [Header("파동 주기(높을수록 잦게 흔들림)")]
    [SerializeField] float m_waveFrequency = 2.0f;

    float m_moveSpeed;
    float m_time;

    Vector3 m_offset = Vector3.zero;
    Vector3 m_startPos = Vector3.zero;
    Vector3 m_direction = Vector3.zero;

    bool m_settingFlag = false;

    public void Setting(Vector3 argDirection, float argSpeed)
    {
        m_time = 0.0f;
        m_moveSpeed = argSpeed;
        m_startPos = transform.position;
        m_direction = argDirection.normalized;

        m_settingFlag = true;
    }

    void Update()
    {
        if (!m_settingFlag) return;

        m_time += Time.deltaTime;

        if (Mathf.Abs(m_direction.x) > 0.5f) // 좌우로 직진 시, Y축에 sin 파형
        {
            m_offset = m_direction * m_moveSpeed * m_time;
            m_offset.y += Mathf.Sin(m_time * m_waveFrequency) * m_waveAmplitude;
        }
        else if (Mathf.Abs(m_direction.y) > 0.5f) // 위아래 직진 시, X축에 sin 파형
        {
            m_offset = m_direction * m_moveSpeed * m_time;
            m_offset.x += Mathf.Sin(m_time * m_waveFrequency) * m_waveAmplitude;
        }

        transform.position = m_startPos + m_offset;
    }
}