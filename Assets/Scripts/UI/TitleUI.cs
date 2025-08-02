using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public void ButtonClick(int argBtnIndex)
    {
        switch (argBtnIndex)
        {
            case 0:
                SceneMoveManager.Instance.ChangeScene("Main");
                break;
            case 1:
                Application.Quit();
                break;
        }
    }
}
