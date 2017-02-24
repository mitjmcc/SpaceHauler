using UnityEngine;
using TeamUtility.IO;
using System.Collections;

enum MoveState {MOVING, STOPPED}

public class TruckController : MonoBehaviour {

    #region PublicVariables
    public PlayerID player;
    [Header("Camera Options")]
    public Vector2 lookXRestraints;
    public Vector2 lookYRestraints;
    [Range(0f, 5f)]
    public float lookSpeed;

    [Header("Physics Options")]
    public Vector2 truckAltitudeRestraints;
    [Range(0f, 1f)]
    public float maxRotationSpeed;
    [Range(0f, 70f)]
    public float warpForce;
    [Range(0f, 200f)]
    public float moveForce;
    [Range(0f, 1f)]
    public float drag = .1f;
    #endregion

    #region PrivateVariables
    MoveState state = MoveState.STOPPED;
    Rigidbody body;
    Camera cam;
    Animator anim;
    Vector3 position;
    Vector3 polarSpeed;
    Vector3 polarPosition;

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
        polarPosition = new Vector3(0, 90, 0);
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
        cam.transform.localRotation = rotation;
    }

    void FixedUpdate() {
        // Get move inputs
        x = InputManager.GetAxis("Horizontal", player);
        y = InputManager.GetAxis("Vertical", player);
        switch (state) {
            case MoveState.STOPPED:
                if (InputManager.anyKey)
                {
                    state = MoveState.MOVING;
                    // Initial forward velocity
                    //body.velocity = new Vector3(0, 0, warpForce);
                }
                break;
            case MoveState.MOVING:
                //TODO: Fix speed and up normal
                polarSpeed.y = -x / 2;

                polarSpeed.x = y;

                polarSpeed *= moveForce * Time.fixedDeltaTime;

                polarSpeed.y = Mathf.Clamp(polarSpeed.y, -maxRotationSpeed, maxRotationSpeed);

                polarPosition += polarSpeed;

                polarPosition.x = Mathf.Clamp(polarPosition.x, truckAltitudeRestraints.x, truckAltitudeRestraints.y);

                position = TCUtil.PolarToCartesion(polarPosition, Vector3.forward * 25);

                transform.localRotation = Quaternion.LookRotation(Vector3.forward, position.normalized);
                transform.localPosition = position ;

                // Calculate the motion direction vector and scale it by the moveForce
                //speed = (x * body.transform.up + z * body.transform.right) * moveForce;
                // Clamp the speed
                //Vector3.ClampMagnitude(speed, maxMovementSpeed);
                // Add the player's speed to the rigidbody velocity
                //body.velocity += speed;
                // Drag
                TCUtil.HorizontalDrag(body, body.velocity, drag);
                break;
        }
    }
    #endregion

    #region Methods

    public void shutdown()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<TruckController>().enabled = false;
    }

    public void reverse(Vector3 normal)
    {
        body.AddForce(-normal * 300 + Vector3.forward * warpForce, ForceMode.Impulse);
    }

    #endregion
}
