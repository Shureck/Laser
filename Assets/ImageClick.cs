using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ImageClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool keyAction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("E key pressed");
        keyAction = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("E key released");
        keyAction = false;
    }
}