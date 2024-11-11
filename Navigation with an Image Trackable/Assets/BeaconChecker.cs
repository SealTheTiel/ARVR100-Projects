using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class BeaconChecker : MonoBehaviour
{
    private GameObject trackables;
    [SerializeField]
    private AgentManager agentManager;
    // Start is called before the first frame update
    void Start()
    {
        trackables = GameObject.Find("XR Origin").GetNamedChild("Trackables");

    }

    // Update is called once per frame
    void Update() {
        ARTrackedImage image = trackables.GetComponentInChildren<ARTrackedImage>();
        if (image != null) {
            agentManager.SetBeacon(image.gameObject);
        }
    }
}
 