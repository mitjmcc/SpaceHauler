using UnityEngine;
using TeamUtility.IO;
using YeggQuest.NS_Spline;
using System.Collections;

enum MoveState {MOVING, STOPPED, END}

public class TruckController : MonoBehaviour {

    #region PublicVariables
    public PlayerID player;
    [Header("Camera Options")]
    public Vector2 lookXRestraints;
    public Vector2 lookYRestraints;
    [Range(0f, 5f)]
    public float lookSpeed;

    [Header("Physics Options")]
    public float maxMovementSpeed;
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
    Vector3 speed;
    SplineFollower follower;

    float lookX;
    float lookY;
    float x, y, z, t;
    #endregion

    #region Initialization
    // Use this for initialization
    void Start () {
        cam = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody>();
        follower = GetComponentInParent<YeggQuest.NS_Spline.SplineFollower>();
        follower.Playing(false);
    }
    #endregion

    #region Updates
    void Update() {
        switch (state)
        {
            case MoveState.END:
                // At end of level reset camera rotationn
                //cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation,
                //    Quaternion.Euler(0, 0, 0), .1f);
                break;
            default:
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
                break;
        }
    }

    void FixedUpdate() {
        // Get move inputs
        x = getHorizontal();
        y = getVetical();
        switch (state) {
            case MoveState.STOPPED:
                // Exit the intro when the player gives input
                if (InputManager.anyKey)
                {
                    follower.Playing(true);
                    state = MoveState.MOVING;
                }
                break;
            case MoveState.MOVING:
                // Calculate the motion direction vector and scale it by the moveForce
                speed = (x * transform.parent.right + y * transform.parent.up) * moveForce;
                // Clamp the speed
                speed = Vector3.ClampMagnitude(speed, maxMovementSpeed);
                // Add the player's speed to the rigidbody velocity relative to the parent
                body.AddRelativeForce(speed, ForceMode.VelocityChange);
                // Drag
                TCUtil.Drag(body, body.velocity, drag);
                // Adjust forward
                transform.localRotation = Quaternion.Lerp(transform.localRotation,
                    Quaternion.LookRotation(follower.getCurrentTangent()), .01f);
                break;
            case MoveState.END:

                break;
        }
    }
    #endregion

    #region Methods

    float getHorizontal()
    {
        return InputManager.GetAxisRaw("Horizontal", player);
    }

    float getVetical()
    {
        return InputManager.GetAxisRaw("Vertical", player);
    }

    public void shutdown()
    {
        state = MoveState.END;
        body.velocity = Vector3.zero;
        follower.enabled = false;
    }

    public void reverse(Vector3 normal)
    {
        body.AddForce(-normal * 300 + Vector3.forward * warpForce, ForceMode.Impulse);
    }

    #endregion
}
