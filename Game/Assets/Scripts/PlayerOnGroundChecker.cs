using UnityEngine;

public class PlayerOnGroundChecker : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject TileSpawner;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            playerObject.GetComponent<Movement>().onGround = true;
            playerObject.GetComponent<Movement>().landingMoment = true; // Required to do ANYTHING at the moment of landing.
                                                                        // Other scripts will turn it false.
            if (other.transform.parent.transform.parent.gameObject!=TileSpawner.GetComponent<TileManager>().thisRoom && other.transform.parent.transform.parent.gameObject!=TileSpawner.GetComponent<TileManager>().LastRoom)
            {
                TileSpawner.GetComponent<TileManager>().currentroom=other.transform.parent.transform.parent.gameObject;
                TileSpawner.GetComponent<TileManager>().roomCleaner();
                
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            playerObject.GetComponent<Movement>().onGround = false;
        }
    }
}
