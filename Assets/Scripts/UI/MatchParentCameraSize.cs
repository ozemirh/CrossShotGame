using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MatchParentCameraSize : MonoBehaviour
{
    private Camera parentCamera;
    private Camera childCamera;

    void Start()
    {
        // Ensure that the parent camera is assigned
        parentCamera = transform.parent.GetComponent<Camera>();
        if (parentCamera == null)
        {
            Debug.LogError("Parent camera not found. Attach this script to a child camera with a parent camera.");
            return;
        }

        // Get the child camera component
        childCamera = GetComponent<Camera>();
        if (childCamera == null)
        {
            Debug.LogError("Child camera not found. Attach this script to a child camera with a parent camera.");
            return;
        }

        // Match the initial size of the child camera to the parent camera
        MatchSize();
    }

    void Update()
    {
        // Update the size of the child camera to match the parent camera
        MatchSize();
    }

    void MatchSize()
    {
        // Match the size of the child camera to the parent camera
        childCamera.orthographicSize = parentCamera.orthographicSize;
    }
}
