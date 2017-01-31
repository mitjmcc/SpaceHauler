using UnityEngine;
using TeamUtility.IO;
using System.Collections;

enum MoveState {MOVING, STOPPED}

public class TruckController : MonoBehaviour {

    #region PublicVariables
    public Vector2 lookXRestraints;
    public Vector2 lookYRestraints;
    public PlayerID player;

    [Range(0f, 70f)]
    public float maxMovementSpeed;
    [Range(0f, 70f)]
    public float warpForce;
    [Range(0f, 200f)]
    public float moveForce;
    [Range(0f, 5f)]
    public float lookSpeed;
    [Range(0f, 1f)]
    public float drag = .1f;
    #endregion

    #region PrivateVariables
    MoveState state = MoveState.STOPPED;
    Rigidbody body;
    Camera cam;
    Animator anim;
    Vector3 speed;
    Vector3 forward;

    float lookX;
    float lookY;
    float x, y, z;
    #endregion

    #region Initialization
    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody>();
    }
    #endregion

    #region Updates
    void Update() {
        // Get inputs from the camera inputs
        lookX += InputManager.GetAxis("LookHorizontal", player) * lookSpeed;
        lookY += InputManager.GetAxis("LookVertical", player) * lookSpeed;
        // Limit the look axises
        lookX = Mathf.Clamp(lookX, lookXRestraints.x, lookXRestraints.y);
        lookY = Mathf.Clamp(lookY, lookYRestraints.x, lookYRestraints.y);
        // Calculate the rotation using Euler angles,
        Quaternion rotation = Quaternion.Euler(lookY, lookX, 0);
        // Set the camera's new rotation
        cam.transform.rotation = rotation;
    }

    void FixedUpdate() {
        // Get move inputs
        x = InputManager.GetAxis("Vertical", player);
        z = InputManager.GetAxis("Horizontal", player);
        switch (state) {
            case MoveState.STOPPED:
                if (InputManager.anyKey)
                {
                    state = MoveState.MOVING;
                    // Initial forward velocity
                    body.velocity = new Vector3(0, 0, warpForce);
                }
                break;
            case MoveState.MOVING:

                // Calculate the motion direction vector and scale it by the moveForce
                speed = (x * body.transform.up + z * body.transform.right) * moveForce;
                // Clamp the speed
                Vector3.ClampMagnitude(speed, maxMovementSpeed);
                // Add the player's speed to the rigidbody velocity
                body.velocity += speed;
                // Drag
                TCUtil.HorizontalDrag(body, body.velocity, drag);
                break;
        }
    }
    #endregion
}
