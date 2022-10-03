/*
*   Copyright (C) 2021 University of Central Florida, created by Dr. Ryan P. McMahan.
*
*   This program is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   This program is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with this program.  If not, see <http://www.gnu.org/licenses/>.
*
*   Primary Author Contact:  Dr. Ryan P. McMahan <rpm@ucf.edu>
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The BackgroundPlaneController provides a method for capturing, replaying, and resuming the background plane video.
public class BackgroundPlaneController : MonoBehaviour
{
    // Whether to capture the background after the start of the application.
    [SerializeField]
    [Tooltip("Whether to capture the background after the start of the application.")]
    private bool m_CaptureAfterStart;
    public bool CaptureAfterStart { get { return m_CaptureAfterStart; } set { m_CaptureAfterStart = value; } }

    // Whether to capture the background during the next update.
    public bool Capture { get; set; }

    // Whether to replay the capture.
    public bool Replay { get; set; }

    // Whether to resume the background.
    public bool Resume { get; set; }

    // Delay required before capturing after the start of the application (in seconds).
    private float m_Delay = 1.5f;
    // The amount of time that has passed since the start of the application (in seconds).
    private float m_TimePassed;
    // Whether the background was captured after the start of the application.
    private bool m_CapturedAfterStart;
    // Reference to the background GameObject.
    private GameObject m_BackgroundPlane;
    // Reference to the background MeshRenderer.
    private MeshRenderer m_BackgroundRenderer;
    // Reference to the background Material.
    private Material m_BackgroundMaterial;
    // Reference to the background Texture.
    private Texture m_BackgroundTexture;
    // Reference to the captured background Texture.
    private Texture m_BackgroundCapture;

    // Update is called once per frame
    void Update()
    {
        // Keep track of the time passed.
        m_TimePassed += Time.deltaTime;

        // If after the start delay.
        if (m_Delay < m_TimePassed)
        {
            // Check whether to capture after the start.
            if (m_CaptureAfterStart && !m_CapturedAfterStart)
            {
                m_CapturedAfterStart = CaptureBackground();
            }

            // Check whether to capture the background this frame.
            if (Capture)
            {
                CaptureBackground();
                Capture = false;
            }

            // Check whether to replay the capture.
            if (Replay)
            {
                ReplayBackground();
            }
            // Otherwise, resume the background.
            else
            {
                Resume = true;
            }

            // Check whether to resume the background.
            if (Resume)
            {
                ResumeBackground();
                Replay = Resume = false;
            }
        }
        // Otherwise, ignore any operations.
        else
        {
            Capture = Replay = Resume = false;
        }
    }

    // Function called to get the background plane's properties. Returns true if successful.
    private bool GetBackground()
    {
        // Return false if the delay has not passed.
        if (m_Delay > m_TimePassed) { return false; }

        // Get the background plane, if necessary.
        if (m_BackgroundPlane == null) { m_BackgroundPlane = GameObject.Find("BackgroundPlane"); }

        // If a background plane exists.
        if (m_BackgroundPlane != null)
        {
            // Get the background renderer, if necessary.
            if (m_BackgroundRenderer == null) { m_BackgroundRenderer = m_BackgroundPlane.GetComponent<MeshRenderer>(); }

            // If the background renderer exists.
            if (m_BackgroundRenderer != null)
            {
                // Get the background material, if necessary.
                if (m_BackgroundMaterial == null) { m_BackgroundMaterial = m_BackgroundRenderer.material; }

                // If the background material exists.
                if (m_BackgroundMaterial != null)
                {
                    // Get the background texture, if necessary.
                    if (m_BackgroundTexture == null) { m_BackgroundTexture = m_BackgroundMaterial.mainTexture; }

                    // If the background texture exists, return true.
                    if (m_BackgroundTexture != null) { return true; }
                }
            }
        }
        // Otherwise, return false.
        return false;
    }

    // Listener called to capture the current background image. Returns true if successful.
    public bool CaptureBackground()
    {
        // Ensure we have the background and texture.
        if (GetBackground() && m_BackgroundTexture != null)
        {
            // Capture the background texture.
            m_BackgroundCapture = Object.Instantiate(m_BackgroundTexture);
            return true;
        }
        // Otherwise, return false.
        return false;
    }

    // Listener called to replay the captured background image. Returns true if successful.
    public bool ReplayBackground()
    {
        // Ensure we have the background and capture texture.
        if (GetBackground() && m_BackgroundCapture != null)
        {
            // Replay the captured background texture.
            m_BackgroundMaterial.mainTexture = m_BackgroundCapture;
            return true;
        }
        // Otherwise, return false.
        return false;
    }

    // Listener called to resume the background image as normal. Returns true if successful.
    public bool ResumeBackground()
    {
        // Ensure we have the background and texture.
        if (GetBackground() && m_BackgroundTexture != null)
        {
            // Play the background texture as normal.
            m_BackgroundMaterial.mainTexture = m_BackgroundTexture;
            return true;
        }
        // Otherwise, return false.
        return false;
    }
}
