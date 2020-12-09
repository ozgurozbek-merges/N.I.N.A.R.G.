using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    # region Variables
    [Header("Important")] // Stuff
    public Rigidbody rBody;
<<<<<<< Updated upstream
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
=======

    // Statics. Some Items are going to add their multiplications to these.
    public float moveSpeed = 16f;
    public float dashSpeed = 20f;
    private float dashSpeedTemp;
    public float dashCooldown = 5f;
    public float jumpMultiplier = 1f;
    public float jumpForce = 8f;
    public int descendSpeed = 100;
>>>>>>> Stashed changes

    [Header("Boolean Checks")] // Some Items are going to alter these.
    public bool onGround;
    public bool hasJumped;
    public bool landingMoment;
    public bool ableToDash = true; // Will always start true, unless revoked or granted by an item.
    public bool nowDashing;

    [Header("Movement Related")]
    // For not carrying out the movement. Horrible way to do this since Unity can nullify these Vectors when calling functions but I can't be bothered.
    // When active it'll point to the movement direction WITH the speed attached as the Vector3 size.
<<<<<<< Updated upstream
    public Vector3 movementVector;
    private float xDirection;
    private float zDirection;
    # endregion
    private void Awake()
    {
        Cursor.visible = false; // too bad!!!
=======
    private float xDirection;
    private float yDirection;

    private void Awake()
    {
>>>>>>> Stashed changes
        rBody = GetComponent<Rigidbody>();
        rBody.interpolation = RigidbodyInterpolation.None; // We don't want jittery collision
        rBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // We want decent collision detection.
    }
    void Update()
    {
<<<<<<< Updated upstream
        xDirection = Input.GetAxis("Horizontal");
        zDirection = Input.GetAxis("Vertical");
        movementVector = transform.right * xDirection + transform.forward * zDirection; // xDirection and zDirection will 0 if not used.

        # region Boolean Checks
=======
>>>>>>> Stashed changes
        if (onGround)
        {
            hasJumped = false;
        }

        if (!onGround && !hasJumped) {
            hasJumped = true; // To fix the collision detection errors that occur on 1 frame getkeys.
        }
        # endregion

<<<<<<< Updated upstream
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
=======
        if (!onGround) {
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y - 0.1f * Time.deltaTime, rBody.velocity.z);
        }

        //Movement
        xDirection = Input.GetAxisRaw("Horizontal") * moveSpeed;
        yDirection = Input.GetAxisRaw("Vertical") * moveSpeed;

>>>>>>> Stashed changes
        if (Input.GetKey(KeyCode.Space))
        {
            if (!hasJumped)
            {
<<<<<<< Updated upstream
                movementVector.y = Mathf.Sqrt(jumpHeight * -2f * gravity) * jumpMultiplier;
=======
                rBody.velocity = new Vector3(rBody.velocity.x, jumpForce * jumpMultiplier, rBody.velocity.z);
>>>>>>> Stashed changes
                hasJumped = true;
            }
            if (hasJumped && UnityEngine.Random.Range(0f,10f) == 9.806650) { // I like eggs
                Debug.LogWarning("Why are you trying to jump mid-air? Why? Have you EVER jumped mid-air? No... Stop. Jumping. Mid. Air.");
            }
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
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
    }

    private void FixedUpdate()
    {
<<<<<<< Updated upstream
        rBody.velocity = new Vector3(0,rBody.velocity.y,0) + movementVector * moveSpeed * Time.deltaTime;
=======
        Vector3 movePosition = transform.right * xDirection + transform.forward * yDirection;
        Vector3 nextMovePosition = new Vector3(movePosition.x, rBody.velocity.y, movePosition.z);

        rBody.velocity = nextMovePosition;
>>>>>>> Stashed changes

        //KeyCode.Shift
        if (nowDashing)
        {
            ableToDash = false;
            rBody.velocity = new Vector3(nextMovePosition.x * dashSpeedTemp, rBody.velocity.y, nextMovePosition.z * dashSpeedTemp);
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
