using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public Rigidbody rBody;
    public Transform tForm;

    // Statics. Some Items are going to add their multiplications to these.
    public float moveSpeed;
    public float dashSpeed;
    private float dashSpeedTemp;
    public int dashCooldown;

    // Boolean status checkers. Some Items are going to alter these.
    public bool onGround;
    public bool hasJumped;
    public bool landingMoment;
    public bool ableToDash = true; // Will always start true, unless revoked or granted by an item.
    public bool nowDashing;

    // For not carrying out the movement. Horrible way to do this since Unity can nullify these Vectors when calling functions but I can't be bothered.
    // When active it'll point to the movement direction WITH the speed attached as the Vector3 size.
    public Vector3 movementVector;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.interpolation = RigidbodyInterpolation.None; // We don't want jittery collision
        rBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // We want decent collision detection.
    }
    void Update()
    {
        movementVector = Vector3.zero;

        if (onGround)
        {
            hasJumped = false;
        }

        //Movement
        if (Input.GetKey(KeyCode.D))
        {
            movementVector += transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector += -transform.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementVector += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector += -transform.forward;
        }
        if (Input.GetKey(KeyCode.Space) && !hasJumped)
        {
            rBody.AddForce(0, moveSpeed * 10 * Time.deltaTime, 0, ForceMode.Impulse); //This causes bug in FixedUpdate if computer is super low to register update after registering fixedupdate twice!
            hasJumped = true;
        }
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && ableToDash)
        {
            dashSpeedTemp = dashSpeed;
            nowDashing = true;
        }
    }

    private void FixedUpdate()
    {
        rBody.MovePosition(transform.position + movementVector * Time.deltaTime * moveSpeed);

        //KeyCode.Shift
        if (nowDashing)
        {
            ableToDash = false;
            rBody.velocity = movementVector * dashSpeedTemp;
            StartCoroutine(waitForFrameDashEnd()); // So that it executes next Update. On very slow machines this will cause a slight falloff.
        }
    }

    IEnumerator waitForFrameDashEnd()
    {
        //returning 0 will make it wait 1 frame
        yield return 0;

        rBody.velocity = Vector3.zero;
        nowDashing = false;
        StartCoroutine(waitForDashCooldown());
    }

    IEnumerator waitForDashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        ableToDash = true;
    }
}
