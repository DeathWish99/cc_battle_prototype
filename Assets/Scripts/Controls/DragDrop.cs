using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    private Plane dragPlane;
    private Vector3 offset;

    private Transform transform;
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        transform = GetComponent<Transform>();
    }
    private void OnMouseDown()
    {
        dragPlane = new Plane(mainCamera.transform.forward, transform.position);
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
        Debug.Log("mouse down");
    }
    private void OnMouseDrag()
    {
        Debug.Log("dragging");
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        transform.position = camRay.GetPoint(planeDist) + offset;
    }

    private void OnMouseUp()
    {
        Debug.Log("mouse up");
    }
}
