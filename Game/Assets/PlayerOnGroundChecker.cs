using UnityEngine;

public class PlayerOnGroundChecker : MonoBehaviour
{
    public GameObject playerObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            playerObject.GetComponent<Movement>().onGround = true;
            playerObject.GetComponent<Movement>().landingMoment = true; // Required to do ANYTHING at the moment of landing.
                                                                        // Other scripts will turn it false.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            playerObject.GetComponent<Movement>().onGround = false;
        }
    }
}
