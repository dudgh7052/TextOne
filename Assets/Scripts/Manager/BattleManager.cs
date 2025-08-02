using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [SerializeField] ShooterType.TYPE m_curShooterType = ShooterType.TYPE.DoubleYSpread;

    [Header("Shooters")]
    [SerializeField] List<RandomSpreadShooter> m_ySpreadShooters = null;
    [SerializeField] RandomSpreadShooter m_upSpreadShooters = null;
    [SerializeField] RandomSpreadShooter m_downSpreadShooters = null;
    [SerializeField] CircularProjectileShooter m_circularProjectileShooter = null;

    bool m_settingFlag = false;
    List<string> m_spawnDatas = new List<string>();
    List<GameObject> m_spawnedProjectiles = new List<GameObject>();

    public List<GameObject> IsProjectileList => m_spawnedProjectiles;

    void Awake()
    {
        if (BattleManager.Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        if (!m_settingFlag || !GManager.Instance.IsBoundaryBattleFlag) return;

        // 여기서 날아오는 타입에 따라서 분기
        UpdateShooter();
    }

    public void InitBattle()
    {
        if (m_spawnedProjectiles.Count > 0)
        {
            for (int i = 0; i < m_spawnedProjectiles.Count; i++)
            {
                PoolManager.Instance.Return(m_spawnedProjectiles[i]);
            }
        }

        m_spawnDatas.Clear(); // 날라올 데이터(단어) 리스트 초기화
        m_spawnedProjectiles.Clear(); // 현재 생성된 발사체 리스트 초기화

        m_settingFlag = false;
    }

    public void SettingBattle(List<WordData> argWordDataList, ShooterType.TYPE argShooterType)
    {
        m_curShooterType = argShooterType;

        m_spawnDatas.Clear();

        for (int i = 0; i < argWordDataList.Count; i++)
        {
            m_spawnDatas.Add(argWordDataList[i].IsWord);
        }

        m_settingFlag = true;
    }

    void UpdateShooter()
    {
        switch (m_curShooterType)
        {
            case ShooterType.TYPE.DoubleYSpread:
                foreach (RandomSpreadShooter _sc in m_ySpreadShooters) _sc.Tick();
                break;
            case ShooterType.TYPE.UpSpread:
                m_upSpreadShooters?.Tick();
                break;
            case ShooterType.TYPE.DownSpread:
                m_downSpreadShooters?.Tick();
                break;
            case ShooterType.TYPE.Circular:
                m_circularProjectileShooter?.Tick();
                break;
        }
    }

    public string GetRandomWord()
    {
        return m_spawnDatas[Random.Range(0, m_spawnDatas.Count)];
    }
}