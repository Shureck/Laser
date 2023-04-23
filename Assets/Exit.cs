using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit: MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName); // перейти в другую сцену
        }
    }
}