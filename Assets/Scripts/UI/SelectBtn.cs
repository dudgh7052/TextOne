using UnityEngine;
using UnityEngine.EventSystems;

public class SelectBtn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int m_btnIndex = 0;

    public void Setting(int argBtnIndex)
    {
        m_btnIndex = argBtnIndex;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked {gameObject.name} Index = {m_btnIndex}");
    }
}