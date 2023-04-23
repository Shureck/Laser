using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraControllerPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed = false;
    public bool isMobile = true;
    public float rotateSpeed = 5f;
    private Vector2 lastPosition;

    public float delta_x;
    public float delta_y;

    private int fingerId;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            pressed = true;
            fingerId = eventData.pointerId;

            // также нужно использовать API Input System для получения позиции указателя на экране
            lastPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            // также нужно использовать API Input System для получения позиции указателя на экране
            Vector2 currentPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 delta = currentPosition - lastPosition;
            lastPosition = currentPosition;


            delta_x = delta.x/10;
            delta_y = -delta.y/10;
        }
    }
}