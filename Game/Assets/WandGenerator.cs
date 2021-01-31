using UnityEngine;

public class WandGenerator : MonoBehaviour
{
    //Saplar
    public GameObject[] Grips;
    //Stickler
    public GameObject[] Sticks;
    void Start()
    {
        //sap objesi = random sap
        GameObject g = Instantiate(Grips[Random.Range(0,Grips.Length)], transform.GetChild(0).transform.position, Quaternion.identity, transform);
        g.GetComponent<Renderer>().material.SetColor("Emission_Color", new Color(Random.Range(0,80),Random.Range(0,80),Random.Range(0,80),0.6f));

        //stick objesi = random stick
        g = Instantiate(Sticks[Random.Range(0,Sticks.Length)], transform.GetChild(1).transform.position, Quaternion.identity, transform);
        g.GetComponent<Renderer>().material.SetColor("Emission_Color", new Color(Random.Range(0,80),Random.Range(0,80),Random.Range(0,80),0.6f));
        

        //Destroy Empty Objects
        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(0).gameObject); //Because the first one is deleted, so second is first.
    }
}
