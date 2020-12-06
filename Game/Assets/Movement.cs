using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rBody;
    public Transform tForm;
    public float moveSpeed;
    public bool onFalloff = false;
    public float faloffSpeed;
    public bool onGround;
    public bool hasJumped;
    public bool landingMoment;
    public Vector3 movementVector;

    // Update is called once per frame
    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        //rBody.interpolation = RigidbodyInterpolation.Interpolate;
    }
    void Update()
    {
        movementVector = new Vector3(0,0,0);
        
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
            hasJumped = true;
            rBody.AddForce(0, moveSpeed * 10 * Time.deltaTime, 0, ForceMode.Impulse);
        }
        // Ati falloff ekle
    }

    private void FixedUpdate() {
        rBody.MovePosition(transform.position + movementVector * Time.deltaTime * moveSpeed);
    }
}