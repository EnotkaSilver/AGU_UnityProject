using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    private Camera _camera;
    private Vector3 mousePos;
    private bool cheak;
    private DistanceJoint2D distanceJoint;
    private LineRenderer lineRenderer;
    private Vector3 tempPos;
    [SerializeField]
    LayerMask grappleMask;
    void Start()
    {
        _camera = Camera.main;
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint.enabled = false;
        cheak = true;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        GetMouse();
        RaycastHit2D hit2D = Physics2D.Raycast(_camera.transform.position, mousePos, Mathf.Infinity, grappleMask);
        
        if(Input.GetMouseButtonDown(0)&& cheak && hit2D)
        {
            distanceJoint.enabled = true;
            distanceJoint.connectedAnchor = mousePos;
            lineRenderer.positionCount = 2;
            tempPos = mousePos;
            cheak = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            distanceJoint.enabled = false;
            lineRenderer.positionCount = 0;
            cheak = true;
        }
        DrawLine();
    }

    private void DrawLine()
    {
        if (lineRenderer.positionCount <= 0) return;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, tempPos);
    }

    private void GetMouse()
    {
        mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
