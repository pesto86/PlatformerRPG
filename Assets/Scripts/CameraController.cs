using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public PlayerMovement playerPosition;
    private Vector3 startingPosition;
    private Vector3 previousFramePos;
    private float camFollowSpeed = 1f;
    private float t;
    private float offsetX = 3f;
    private float offsetY = 1.5f;
    
    void Start()
    {
        startingPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        previousFramePos = startingPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        t = camFollowSpeed;
        cam.transform.position = new Vector3(Mathf.Lerp(previousFramePos.x, playerPosition.playerLocation.x, t) + offsetX, Mathf.Lerp(previousFramePos.y, playerPosition.playerLocation.y, t) + offsetY, startingPosition.z);
        previousFramePos = cam.transform.position; 
    }
}
