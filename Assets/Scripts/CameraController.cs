using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public PlayerMovement playerPosition;
    private Vector3 startingPosition;
    
    void Start()
    {
        startingPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(startingPosition.x + playerPosition.playerLocation.x, startingPosition.y + playerPosition.playerLocation.y, startingPosition.z);
    }
}
