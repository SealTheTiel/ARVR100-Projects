using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //edit text mesh pro message
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Touch Phase: " + Input.GetTouch(0).phase.ToString();
    }
}
