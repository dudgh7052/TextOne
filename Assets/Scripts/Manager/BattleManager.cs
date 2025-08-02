using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [SerializeField] ShooterType.TYPE m_curShooterType = ShooterType.TYPE.DoubleYSpread;

    [Header("좌에서 우로 크고 빠르게 이동하는 패턴")]
    [SerializeField] RandomSpreadShooter m_leftShooter = null;

    [Header("양쪽에서 나오는 패턴")]
    [SerializeField] DoubleRandomSpreadShooter m_doubleSpreadShooter = null;

    [Header("위, 아래에서 좌우로 움직이며 나오는 패턴")]
    [SerializeField] SinRandomSpreadShooter m_upSinShooter = null;
    [SerializeField] SinRandomSpreadShooter m_downSinShooters = null;

    [Header("캐릭터 주변으로 원 형태로 나오는 패턴")]
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

        m_spawnDatas.Clear(); // 날라올 데이터(단어) 리스트 초기화
        m_spawnedProjectileList.Clear(); // 현재 생성된 발사체 리스트 초기화
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