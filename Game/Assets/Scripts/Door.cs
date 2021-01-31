using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        } else if (other.gameObject.tag == "deadground")
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
