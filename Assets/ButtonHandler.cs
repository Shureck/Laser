using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ButtonHandler : MonoBehaviour
{
    public void OnButtonClick(UnityEngine.UI.Button button)
    {
        if (button != null)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                PlayerPrefs.SetString("Mode", buttonText.text);
                SceneManager.LoadScene("Cave");
            }
        }
    }
}