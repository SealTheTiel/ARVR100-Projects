using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class NavMeshPlacer : MonoBehaviour
{
    [SerializeField] private GameObject navmeshPrefab;
    private GameObject navmesh;
    private ARPlaneManager planeManager;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    private bool planesVisible = true;
    // Start is called before the first frame update
    void Start()
    {
        navmesh = Instantiate(navmeshPrefab);
        navmesh.SetActive(false);
        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount < 1) { return; }
        //if (Input.GetTouch(0).phase == TouchPhase.Began) {
        if (navmesh.activeSelf) { return; }
        if (Input.GetMouseButtonDown(0)) {
            // Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (raycastManager.Raycast(ray, hitResults, TrackableType.PlaneWithinPolygon)) {
                navmesh.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                navmesh.transform.position = hitResults[0].pose.position;// + new Vector3(0, 0.2f, 0);   
                navmesh.SetActive(true);
            }
        }
    }

    public void TogglePlaneVisualizer() {
        planesVisible = !planesVisible;
        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(planesVisible);
        }
    }
}
