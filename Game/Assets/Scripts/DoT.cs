using System.Collections;
using UnityEngine;

public class DoT : MonoBehaviour
{
    private bool ActiveDamage = false;
    public float DoTDamage=5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("ignoreDoT"))
        {
            ActiveDamage = true;
            StartCoroutine(DealActiveDamage(other));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("ignoreDoT"))
        {
            ActiveDamage = false;
            StartCoroutine(DealDoT(other));
        }
    }
    IEnumerator DealDoT(Collider other)
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);
            if (!other.CompareTag("ignoreDoT"))
            {
                other.transform.parent.GetComponent<Health>().Hit(DoTDamage - i);
            }
        }
    }
    IEnumerator DealActiveDamage(Collider other)
    {
        while (ActiveDamage)
        {
            yield return new WaitForSeconds(0.1f);
            if (!other.CompareTag("ignoreDoT"))
            {
                other.transform.parent.GetComponent<Health>().Hit(1f);
            }
        }
    }
}
