using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [SerializeField] ShooterType.TYPE m_curShooterType = ShooterType.TYPE.DoubleYSpread;

    [Header("�¿��� ��� ũ�� ������ �̵��ϴ� ����")]
    [SerializeField] RandomSpreadShooter m_leftShooter = null;

    [Header("���ʿ��� ������ ����")]
    [SerializeField] DoubleRandomSpreadShooter m_doubleSpreadShooter = null;

    [Header("��, �Ʒ����� �¿�� �����̸� ������ ����")]
    [SerializeField] SinRandomSpreadShooter m_upSinShooter = null;
    [SerializeField] SinRandomSpreadShooter m_downSinShooters = null;

    [Header("ĳ���� �ֺ����� �� ���·� ������ ����")]
    [SerializeField] CircularShooter m_circularShooter = null;

    List<string> m_spawnDatas = new List<string>();
    List<GameObject> m_spawnedProjectileList = new List<GameObject>();

    public List<GameObject> IsSpawnedProjectileList => m_spawnedProjectileList;

    void Awake()
    {
        if (BattleManager.Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    public void InitBattle()
    {
        if (m_spawnedProjectileList.Count > 0)
        {
            for (int i = 0; i < m_spawnedProjectileList.Count; i++)
            {
                PoolManager.Instance.Return(m_spawnedProjectileList[i]);
            }
        }

        m_spawnDatas.Clear(); // ����� ������(�ܾ�) ����Ʈ �ʱ�ȭ
        m_spawnedProjectileList.Clear(); // ���� ������ �߻�ü ����Ʈ �ʱ�ȭ
    }

    public void SettingBattle(List<WordData> argWordDataList, ShooterType.TYPE argShooterType)
    {
        m_curShooterType = argShooterType;

        m_spawnDatas.Clear();
        for (int i = 0; i < argWordDataList.Count; i++)
        {
            m_spawnDatas.Add(argWordDataList[i].IsWord);
        }

        StartPattern();
    }

    void StartPattern()
    {
        switch (m_curShooterType)
        {
            case ShooterType.TYPE.LeftYSpread:
                m_leftShooter.StartShooting();
                break;
            case ShooterType.TYPE.DoubleYSpread:
                m_doubleSpreadShooter.StartShooting();
                break;
            case ShooterType.TYPE.SinUpSpread:
                m_upSinShooter.StartShooting();
                break;
            case ShooterType.TYPE.SinDownSpread:
                m_downSinShooters.StartShooting();
                break;
            case ShooterType.TYPE.Circular:
                m_circularShooter.StartShooting();
                break;
        }
    }

    public string GetRandomWord()
    {
        return m_spawnDatas[Random.Range(0, m_spawnDatas.Count)];
    }
}