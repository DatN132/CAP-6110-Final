using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using agora_gaming_rtc;
public class AgoraInterface : MonoBehaviour
{
    public IRtcEngine mRtcEngine;
    public uint mRemotePeer;

    public void loadEngine()
    {
        string appId = "13d63ff963be4b54a4c6f018cbbe6953";
        // start SDK
        Debug.Log("Initializing Engine");
        if (mRtcEngine != null)
        {
            Debug.Log("Engine already exists. Please unload it first");
            return;
        }

        // init RTC engine
        mRtcEngine = IRtcEngine.GetEngine(appId);

        // set log level
        mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG);
    }

    public void joinChannel(string channelName)
    {
        Debug.Log("Joining channel: " + channelName);

        if (mRtcEngine == null)
        {
            Debug.Log("Engine needs to be initialized before joining a channel");
            return;
        }

        // set callbacks
        mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccess;
        mRtcEngine.OnUserJoined = OnUserJoined;
        mRtcEngine.OnUserOffline = OnUserOffline;
        // enable video
        mRtcEngine.EnableVideo();
        mRtcEngine.DisableAudio();

        // allow camera input callback
        mRtcEngine.EnableVideoObserver();
//#if UNITY_ANDROID
//        mRtcEngine.SwitchCamera();
//#endif

        // join the channel
        mRtcEngine.JoinChannel(channelName, null, 0);
    }

    public void leaveChannel()
    {
        Debug.Log("Leaving channel");
        if (mRtcEngine == null)
        {
            Debug.Log("Engine needs to be initialized before leaving a channel");
            return;
        }

        // leave channel
        mRtcEngine.LeaveChannel();

        // remove video observer
        mRtcEngine.DisableVideoObserver();
    }

    public void unloadEngine()
    {
        Debug.Log("Unloading Agora Engine");
        if (mRtcEngine != null)
        {
            IRtcEngine.Destroy();
            mRtcEngine = null;
        }
    }

    public string getSdkVersion()
    {
        return IRtcEngine.GetSdkVersion();
    }

    //implement callbacks
    private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Successfully joined channel " + channelName + " with id " + uid);
        //GameObject versionText = GameObject.Find("VersionText");
        //versionText.GetComponent<Text>().text = "Version: " + getSdkVersion();
    }

    private void OnUserJoined(uint uid, int elapsed)
    {
        Debug.Log("New User has joined channel with id: " + uid);

        // add remote stream to scene

        // create game object
        GameObject go = GameObject.Find("screen");
        go.name = uid.ToString();

        // configure video surface
        VideoSurface o = go.AddComponent<VideoSurface>();
        o.SetForUser(uid);
        o.SetEnable(true);


        mRemotePeer = uid;
    }

    private void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        Debug.Log("User with id: " + uid + " has left the channel");

        // remove the game object from the scene
        GameObject go = GameObject.Find(uid.ToString());
        if (!ReferenceEquals(go, null))
        {
            Destroy(go);
        }
    }
}
