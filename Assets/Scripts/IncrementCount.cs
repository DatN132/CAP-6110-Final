using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class IncrementCount : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;
    private TextMeshPro mesh;
    // Start is called before the first frame update
    void Start()
    {
        photonView = PhotonView.Get(this);
        mesh = gameObject.GetComponent<TextMeshPro>();
    }

    public void incrementRed()
    {
        photonView.RPC("IncrementColors", Photon.Pun.RpcTarget.AllBuffered);
    }

    public void incrementGreen()
    {
        photonView.RPC("IncrementColors", Photon.Pun.RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void IncrementColors()
    {
        UInt64 count = Convert.ToUInt64(mesh.text, 10);
        mesh.text = (count + 1).ToString();
    }
}
