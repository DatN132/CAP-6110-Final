using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if(UNITY_2018_3_OR_NEWER)
using UnityEngine.Android;
#endif

public class LoadAgora : MonoBehaviour
{
    static AgoraInterface app = null;
    // Start is called before the first frame update
    void Start()
    {
#if (UNITY_2018_3_OR_NEWER)
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            
        }
        else
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            
        }
        else
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
#endif
        // init agora engine
        if (ReferenceEquals(app, null))
        {
            app = new AgoraInterface();
            app.loadEngine();
        }

        // join channel
        app.joinChannel("Homework6");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
