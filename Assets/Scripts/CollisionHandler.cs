using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CollisionHandler : MonoBehaviour
{
    // public GameObject current;
    public GameObject theObject;
    public GameObject counter;
    public GameObject redText;
    public GameObject greenText;
    public PhotonView photonView;
    private TextMeshPro mesh;
    private bool caught;
    private IncrementCount script;

    void Start()
    {
        mesh = counter.GetComponent<TextMeshPro>();
        Time.fixedDeltaTime = 0.001f;
        caught = false;
        script = greenText.GetComponent<IncrementCount>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (gameObject.GetComponent<Rigidbody>().SweepTest(gameObject.transform.forward * -1, out hit, 10))
        {
            if (hit.collider.gameObject == theObject)
            {
                if (!caught)
                {
                    caught = true;
                    UInt64 count = Convert.ToUInt64(mesh.text, 10);
                    mesh.text = (count + 1).ToString();
                    script.incrementGreen();
                }
                
            }
        }
        else
        {
            caught = false;
        }
    }

    //void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("HIT");
    //}

    //void OnTriggerExit(Collider col)
    //{
    //   if (col.gameObject == theObject)
    //    {
    //        UInt64 count = Convert.ToUInt64(mesh.text, 10);
    //        mesh.text = (count + 1).ToString();
    //    }
    //}

}
