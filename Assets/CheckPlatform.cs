using UnityEngine;

public class CheckPlatform : MonoBehaviour 
{ 

    private void Start()
    {
        string playerName = PlayerPrefs.GetString("Mode");
        Debug.Log(playerName);
        if (playerName.Equals("Comp"))
        {
            gameObject.SetActive(false); // ���������� �������
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}