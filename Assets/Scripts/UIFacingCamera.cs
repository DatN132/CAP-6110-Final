using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFacingCamera : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(gameObject.transform.position - Camera.main.transform.position);
    }
}
