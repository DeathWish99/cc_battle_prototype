using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    public bool isInAttackMode = false;
    private Plane dragPlane;
    private Vector3 offset;

    private float downClickTime = 0.25F;
    private DateTime startTime;

    private Transform transform;
    private Camera mainCamera;

    private BaseUnit baseUnit;
    private void Awake()
    {
        baseUnit = GetComponent<BaseUnit>();
        mainCamera = Camera.main;
        transform = GetComponent<Transform>();
    }
    private void OnMouseDown()
    {
        if (!isInAttackMode)
        {
            startTime = DateTime.Now;
            dragPlane = new Plane(mainCamera.transform.forward, transform.position);
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDist;
            dragPlane.Raycast(camRay, out planeDist);
            offset = transform.position - camRay.GetPoint(planeDist);
            Debug.Log("mouse down");
        }
    }

    private void OnMouseDrag()
    {
        if (!isInAttackMode)
        {
            Debug.Log("dragging");
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDist;
            dragPlane.Raycast(camRay, out planeDist);
            transform.position = camRay.GetPoint(planeDist) + offset;
        }
    }

    private void OnMouseUp()
    {
        if (!isInAttackMode)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;

            if (elapsedTime.TotalSeconds < downClickTime)
            {
                baseUnit.SelectEvent();
            }
        }
    }
}

