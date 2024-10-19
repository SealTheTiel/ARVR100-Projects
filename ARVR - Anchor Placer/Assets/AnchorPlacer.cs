using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AnchorPlacer : MonoBehaviour
{
    private ARAnchorManager anchorManager;
    private ARPointCloudManager pointCloudManager;
    [SerializeField] private GameObject prefabToAnchor;
    [SerializeField] private float forwardOffset = 1f;
    private List<GameObject> anchors = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();
        pointCloudManager.SetTrackablesActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            if (EventSystem.current.IsPointerOverGameObject()) {return;}
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                ARAnchor anchor = hit.transform.GetComponentInParent<ARAnchor>();
                if (anchor != null) {
                    Destroy(anchor.gameObject);
                    return;
                }
            }
            Vector3 spawnPos = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).GetPoint(forwardOffset); 
            AnchorObject(spawnPos);   
        }
    }

    public void AnchorObject(Vector3 position) {
        GameObject newAnchor = new GameObject("NewAnchor");
        newAnchor.transform.parent = null;
        newAnchor.transform.position = position;
        newAnchor.AddComponent<ARAnchor>();
        
        GameObject spawnedObject = Instantiate(prefabToAnchor, newAnchor.transform);
        spawnedObject.transform.localPosition = Vector3.zero;
        anchors.Add(newAnchor);
    }

    public void ChangeAnchor(GameObject anchor) {
        prefabToAnchor = anchor;
    }

    public void ChangeDistance(float distance) {
        forwardOffset = distance/100;
    }

    public void ClearAnchors() {
        foreach (var anchor in anchors) {
            Destroy(anchor);
        }
        foreach (var anchor in anchorManager.trackables) {
            Destroy(anchor);
        }
    }

}