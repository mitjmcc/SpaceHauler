using UnityEngine;
using System.Collections;

//Player Controller Utilities
public class TCUtil {

    public static Vector3 XZPlane = new Vector3(1, 0, 1);

    public static void AdjustRigidbodyForward(Rigidbody body, Vector3 newForward, Vector3 camForward, float speed)
    {
        //Only rotate the body when there is motion
        if (newForward.magnitude > 0)
        {
            //The direction of movement
            Vector3 moveForward = new Vector3(newForward.x, 0, newForward.z).normalized, forward;
            if (Vector3.Dot(moveForward, camForward) > 2)
                //If the body is moving in the direction of the camera is pointing
                forward = camForward;
            else
                //Or it is not going in the direction of the camera
                forward = moveForward;
            //Smooth the direction the body is facing to the correct direction
            body.transform.forward = Vector3.Slerp(body.transform.forward, forward, speed * Time.fixedDeltaTime);
        }
    }

    public static void HorizontalDrag(Rigidbody body, Vector3 velocity, float drag)
    {
        velocity.x *= 1 - drag;
        velocity.y *= 1 - drag;
        body.velocity = velocity;
    }
}
