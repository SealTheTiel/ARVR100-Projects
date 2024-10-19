using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeText : MonoBehaviour {
    void Start(){}

    void Update(){}

    public void ChangeTextValue(float newText) {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = (newText/100).ToString();
    }
}
