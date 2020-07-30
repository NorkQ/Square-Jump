using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _myRigidbody;

    [SerializeField]
    private ParticleSystem _myExplosionEffect;

    [SerializeField]
    private Material _shieldMat;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Shield")
        {
            StartCoroutine(ShieldEffect());
        }
    }

    private IEnumerator ShieldEffect()
    {
        _myExplosionEffect.gameObject.transform.parent = null;
        _shieldMat.SetInt("Boolean_9F77E8CC", 1);
        _myExplosionEffect.Play();
        _myRigidbody.velocity = Vector3.zero;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(0.3f);
        _shieldMat.SetInt("Boolean_9F77E8CC", 0);
        Destroy(gameObject);
    }
}
