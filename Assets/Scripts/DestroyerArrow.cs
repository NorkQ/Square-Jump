using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerArrow : MonoBehaviour
{
    public Material material;
    public float hexagonscale=0f;
    public bool collisiontry = false;
    public Rigidbody rb;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (collisiontry == true)
        {
            material.SetFloat("Vector1_42AE453", hexagonscale);     
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Shield")
        {
            hexagonscale = 1.5f;
            collisiontry = true;
            Destroy(gameObject, 0.3f) ;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        material.SetFloat("Vector1_42AE453", 0);
        Debug.Log("Deneme");
    }
}
