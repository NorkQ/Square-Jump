using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _myRigidbody;

    [SerializeField]
    private ParticleSystem _myExplosionEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Shield")
        {
            ShieldEffect();
        }
    }

    private void ShieldEffect()
    {
        _myExplosionEffect.gameObject.transform.parent = null;
        _myExplosionEffect.Play();
        _myRigidbody.velocity = Vector3.zero;
        Destroy(gameObject);
    }
}
