using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class VirtualButton : MonoBehaviour
{
    private ARTrackedImage trackedImage;
    [SerializeField]
    private UnityEvent onTrackedImageLimited;
    // Start is called before the first frame update
    void Start()
    {   
        trackedImage = transform.parent.GetComponent<ARTrackedImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedImage.trackingState == TrackingState.Limited)
        {
            onTrackedImageLimited.Invoke();
        }
    }
}
