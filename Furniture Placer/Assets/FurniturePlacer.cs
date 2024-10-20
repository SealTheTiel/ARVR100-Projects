using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FurniturePlacer : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    [SerializeField] private GameObject furniturePrefab;
    [SerializeField] private float furnitureScale = 0.3f;
    [SerializeField] private Vector3 furnitureOffset = new Vector3(0, 0.05f, 0);
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isMoving = false;
    private GameObject selectedObject;
    private bool planeVisualizer = false;

    void Start() {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        planeManager.SetTrackablesActive(false);
    }

    void Update() {
        foreach (var plane in planeManager.trackables) {
            plane.gameObject.SetActive(planeVisualizer);
        }

        if (isMoving) {
            if (Input.touchCount == 0) {
                Ray r = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                if (raycastManager.Raycast(r, hits, TrackableType.PlaneWithinPolygon)) {
                    MoveFurniture(hits[0]);
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                isMoving = false;
                selectedObject = null;
                return;
            }
        }
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            if (EventSystem.current.IsPointerOverGameObject()) {return;}

            Ray r = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(r, out RaycastHit hit)) {
                if (hit.transform.name == "Furniture") {
                    isMoving = true;
                    selectedObject = hit.transform.gameObject;
                    return;
                }
            }

            if (raycastManager.Raycast(r, hits, TrackableType.PlaneWithinPolygon)) {
                PlaceFurniture(hits[0]);
                return;
            }
        }
    }

    public void PlaceFurniture(ARRaycastHit hit) {
        GameObject newAnchor = new GameObject("NewAnchor");
        newAnchor.transform.parent = null;
        newAnchor.transform.position = hit.pose.position;
        newAnchor.AddComponent<ARAnchor>();
        
        GameObject spawnedObject = Instantiate(furniturePrefab, newAnchor.transform);
        spawnedObject.name = "Furniture";
        spawnedObject.transform.localPosition = furnitureOffset;
        spawnedObject.transform.localScale = new Vector3(furnitureScale, furnitureScale, furnitureScale);

    }

    public void MoveFurniture(ARRaycastHit hit) {
        selectedObject.transform.position = hit.pose.position;
    }

    public void ChangeFurniture(GameObject furniture) {
        furniturePrefab = furniture;
    }

    public void TogglePlaneVisualizer() {
        planeVisualizer = !planeVisualizer;
        planeManager.SetTrackablesActive(planeVisualizer);
    }
}
