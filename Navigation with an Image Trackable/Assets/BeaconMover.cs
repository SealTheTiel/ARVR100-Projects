using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.VisualScripting;

public class BeaconMover : MonoBehaviour
{
    [SerializeField]
    private GameObject beacon;
    [SerializeField]
    private GameObject navmesh;
    private ARTrackedImageManager trackedImageManager;
    
    // Start is called before the first frame update
    void Start()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        trackedImageManager.trackablesChanged.AddListener(OnImageAdded);
    }


    void OnEnable() {
        trackedImageManager.trackablesChanged.AddListener(OnImageAdded);
    }

    void OnDisable() {
        trackedImageManager.trackablesChanged.RemoveListener(OnImageAdded);
    }
    private void OnImageAdded(ARTrackablesChangedEventArgs<ARTrackedImage> args) {
        foreach (var image in args.added) {
            MoveBeacon(image);
        }
        foreach (var image in args.updated) {
            MoveBeacon(image);
        }
        // foreach (var image in args.removed) {
        //     DeactivateBeacon();
        // }
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    public void MoveBeacon(ARTrackedImage trackable) {
        beacon.SetActive(true);
        beacon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        beacon.transform.position = trackable.transform.position;//+ new Vector3(0, 3f, 0);
        Debug.Log("Beacon position " + beacon.transform.position);
        Debug.Log("Navmesh position " + navmesh.transform.position);

    }

    public void DeactivateBeacon() {
        beacon.SetActive(false);
    }
}
