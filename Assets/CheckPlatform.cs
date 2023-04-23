using UnityEngine;

public class CheckPlatform : MonoBehaviour 
{
    public bool isMobile = false;
    private void Start()
    {
        string playerName = PlayerPrefs.GetString("Mode");
        Debug.Log(playerName);
        if (playerName.Equals("Comp"))
        {
            gameObject.SetActive(false); // выключение объекта
            isMobile = false;
        }
        else
        {
            gameObject.SetActive(true);
            isMobile = true;
        }
    }
}