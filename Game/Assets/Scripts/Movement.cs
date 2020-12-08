using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    # region Variables
    [Header("Important")] // Stuff
    public Rigidbody rBody;
    public Transform tForm;
    private float horizontalMovement;
    private float verticalMovement;

    [Header("Statics")] // Some Items are going to add their multiplications to these.
    public float moveSpeed;
    public float dashSpeed;
    private float dashSpeedTemp;
    public int dashCooldown;
    public float jumpHeight = 1f;
    public float jumpMultiplier = 1f;
    public int descendSpeed = 500;
    public float gravity = -9.806650f;

    [Header("Boolean Checks")] // Some Items are going to alter these.
    public bool onGround;
    public bool hasJumped;
    public bool landingMoment;
    public bool ableToDash = true; // Will always start true, unless revoked or granted by an item.
    public bool nowDashing;

    [Header("Movement Related")]
    // For not carrying out the movement. Horrible way to do this since Unity can nullify these Vectors when calling functions but I can't be bothered.
    // When active it'll point to the movement direction WITH the speed attached as the Vector3 size.
    public Vector3 movementVector;
    private float xDirection;
    private float zDirection;
    # endregion
    private void Awake()
    {
        Cursor.visible = false; // too bad!!!
        rBody = GetComponent<Rigidbody>();
        rBody.interpolation = RigidbodyInterpolation.None; // We don't want jittery collision
        rBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // We want decent collision detection.
    }
    void Update()
    {
        xDirection = Input.GetAxis("Horizontal");
        zDirection = Input.GetAxis("Vertical");
        movementVector = transform.right * xDirection + transform.forward * zDirection; // xDirection and zDirection will 0 if not used.

        # region Boolean Checks
        if (onGround)
        {
            hasJumped = false;
        }

        if (!onGround && !hasJumped) {
            hasJumped = true; // To fix the collision detection errors that occur on 1 frame getkeys.
        }
        # endregion

        # region Other Checks
        if (!onGround)
        {
            movementVector.y += gravity * 2 * Time.deltaTime; // *2 to make it fall faster in default mode.
        }
        # endregion
        
        //Movement
        # region WASD
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
        # endregion
        
        //Special
        if (Input.GetKey(KeyCode.Space))
        {
            if (!hasJumped)
            {
                movementVector.y = Mathf.Sqrt(jumpHeight * -2f * gravity) * jumpMultiplier;
                hasJumped = true;
            }
            if (hasJumped && UnityEngine.Random.Range(0f,10f) == 9.806650) { // I like eggs
                Debug.LogWarning("Why are you trying to jump mid-air? Why? Have you EVER jumped mid-air? No... Stop. Jumping. Mid. Air.");
            }
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (hasJumped)
            {
                rBody.velocity += Vector3.down * descendSpeed * Time.deltaTime;
            }
        }
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && ableToDash)
        {
            dashSpeedTemp = dashSpeed;
            nowDashing = true;
        }
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector3(0,rBody.velocity.y,0) + movementVector * moveSpeed * Time.deltaTime;

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
