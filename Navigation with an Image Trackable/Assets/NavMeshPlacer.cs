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
        if (Input.touchCount < 1) { return; }
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (raycastManager.Raycast(ray, hitResults, TrackableType.PlaneWithinPolygon)) {
                navmesh.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                navmesh.transform.position = hitResults[0].pose.position;   
                navmesh.SetActive(true);
                enabled = false;
            }
        }
    }

    public void TogglePlaneVisualizer() {
        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(!plane.gameObject.activeSelf);
        }
    }
}
