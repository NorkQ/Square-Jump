using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPosition : MonoBehaviour
{
    public Rigidbody playerRB;
    private void FixedUpdate()
    {
        transform.position = playerRB.position;
        

    }
}
