using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; private set; } = null;

    [SerializeField] GameObject m_nonBattleObjs = null;
    [SerializeField] GameObject m_battleObjs = null;

    /// <summary>
    /// 인터렉팅 플래그
    /// </summary>
    [field:SerializeField] public bool IsBoundaryBattleFlag { get; set; } = false;

    void Awake()
    {
        if (GManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// 범위 배틀 시작
    /// </summary>
    public void BoundaryBattleStart()
    {
        IsBoundaryBattleFlag = true;

        m_nonBattleObjs.SetActive(false);
        m_battleObjs.SetActive(true);
    }

    /// <summary>
    /// 범위 배틀 종료
    /// </summary>
    public void BoundaryBattleEnd()
    {
        IsBoundaryBattleFlag = false;

        m_battleObjs.SetActive(false);
        m_nonBattleObjs.SetActive(true);
    }
}