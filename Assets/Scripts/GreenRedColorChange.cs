using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GreenRedColorChange : MonoBehaviourPunCallbacks
{
    public TextMeshPro greenMesh;
    public TextMeshPro redMesh;
    private Color color;
    private PhotonView photonView;




    // Start is called before the first frame update
    void Start()
    {
        Time.fixedDeltaTime = 0.001f;
        GetComponent<MeshRenderer>().material.color = Color.black;
        photonView = PhotonView.Get(this);
    }

    void FixedUpdate()
    {
        ChangeColor();
    }


    [PunRPC]
    public void ChangeColor()
    {
        UInt64 redCount = Convert.ToUInt64(redMesh.text, 10);
        UInt64 greenCount = Convert.ToUInt64(greenMesh.text, 10);
        if ((redCount != 0) || (greenCount != 0))
        {
            Material targetMaterial = GetComponent<MeshRenderer>().material;
            float percentRed = (float)redCount / (redCount + greenCount);
            color.r = Color.red.r * percentRed;
            color.g = Color.green.g * (1 - percentRed);

            targetMaterial.color = color;
        }
    }
}
