using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    # region Variables
    [Header("Important")] // Stuff
    public Rigidbody rBody;

    [Header("Statics")] // Some Items are going to add their multiplications to these.
    public float moveSpeed = 16f;
    public float dashSpeed = 20f;
    private float dashSpeedTemp;
    public float dashCooldown = 5f;
    public float jumpMultiplier = 1f;
    public float jumpForce = 8f;
    public int descendSpeed = 100;

    [Header("Boolean Checks")] // Some Items are going to alter these.
    public bool onGround;
    public bool hasJumped;
    public bool landingMoment;
    public bool ableToDash = true; // Will always start true, unless revoked or granted by an item.
    public bool nowDashing;

    [Header("Movement Related")]
    private float xDirection;
    private float zDirection;
    # endregion

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.interpolation = RigidbodyInterpolation.None; // We don't want jittery collision
        rBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // We want decent collision detection.
    }
    void Update()
    {
        # region Common Checks
        if (onGround) { hasJumped = false; }

        if (!onGround && !hasJumped) { hasJumped = true; } // To fix the collision detection errors that occur on 1 frame getkeys.

        if (!onGround)
        {
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y - 0.1f * Time.deltaTime, rBody.velocity.z);
        }
        # endregion

        # region Movement
        xDirection = Input.GetAxisRaw("Horizontal") * moveSpeed;
        zDirection = Input.GetAxisRaw("Vertical") * moveSpeed;

        if (Input.GetKey(KeyCode.Space))
        {
            if (!hasJumped)
            {
                rBody.velocity = new Vector3(rBody.velocity.x, jumpForce * jumpMultiplier, rBody.velocity.z);
                hasJumped = true;
            }
            if (hasJumped && UnityEngine.Random.Range(0f, 10f) == 9.806650)
            { // I like eggs
                Debug.LogWarning("Why are you trying to jump mid-air? Why? Have you EVER jumped mid-air? No... Stop. Jumping. Mid. Air.");
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (hasJumped)
            {
                rBody.velocity += Vector3.down * descendSpeed;
            }
        }

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && ableToDash)
        {
            dashSpeedTemp = dashSpeed;
            nowDashing = true;
        }
        # endregion
    }

    private void FixedUpdate()
    {
        // Movement Vectors
        Vector3 movePosition = transform.right * xDirection + transform.forward * zDirection;
        Vector3 nextMovePosition = new Vector3(movePosition.x, rBody.velocity.y, movePosition.z);

        // Add vector to velocity. No need to Time.deltaTime because it's FixedUpdate.
        rBody.velocity = nextMovePosition;

        //KeyCode.Shift
        if (nowDashing)
        {
            ableToDash = false;
            rBody.velocity = new Vector3(nextMovePosition.x * dashSpeedTemp, rBody.velocity.y, nextMovePosition.z * dashSpeedTemp);
            StartCoroutine(waitForFrameDashEnd()); // So that it executes next Update. On very slow machines this will cause a slight drag.
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
