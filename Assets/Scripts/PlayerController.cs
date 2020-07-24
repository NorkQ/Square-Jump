using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;

    private Transform activeBlock;
    private bool coolDown;

    private Vector3 offset;
    public Animator _animator;

    void Awake()
    {
        myTransform = transform;
        SwipeDetector.OnSwipe += OnMobileMove;
    }

    //----------------------------Kontroller-----------------------------

    //Input sisteminin eventlerini kullanarak kontrolleri sağlıyoruz.
    public void OnMobileMove(SwipeData data)
    {
        if(data.Direction == SwipeDirection.Up)
        {
            if (!coolDown)
            {
                offset = new Vector3(-2, 0, 0);
                StartCoroutine(SimulateProjectile(offset));
            }
        }
    }

    public void OnForward()
    {
        if (!coolDown)
        {
            offset = new Vector3(-2, 0, 0);
            StartCoroutine(SimulateProjectile(offset));
        }
    }

    public void OnBackward()
    {
        if (!coolDown)
        {
            offset = new Vector3(2, 0, 0);
            StartCoroutine(SimulateProjectile(offset));
        }
    }

    public void OnRight()
    {
        if (!coolDown)
        {
            offset = new Vector3(0, 0, 2);
            StartCoroutine(SimulateProjectile(offset));
        }
    }

    public void OnLeft()
    {
        if (!coolDown)
        {
            offset = new Vector3(0, 0, -2);
            StartCoroutine(SimulateProjectile(offset));
        }
    }
    
    //----------------------------------------------------------------------------

    private void Update()
    {
        
    }

    //Oyuncu hareket etmek istediğinde player objesinin parabolik şekilde ilerlemesini sağlayan fonksiyon.
    IEnumerator SimulateProjectile(Vector3 posOffset)
    {
        coolDown = true;
        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);
        Target = transform.position + posOffset;

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }

        //Karakter objesi atladığı küpün üzerinde kaymasın diye karakterin z ve x pozisyon değerlerini atladığı küpünkine eşitliyoruz.
        transform.position = new Vector3(activeBlock.transform.position.x, transform.position.y, activeBlock.transform.position.z);

        //Oyuncu ard arda hareket etmek istediğinde istenmeyen bir hareket olmaması için küp hareketini bitirene kadar ikinci bir hareket olmuyor.
        coolDown = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            //Doğru yer ile temas edilirse temas edilen yer aktif blok olarak kaydedilir.
            activeBlock = collision.gameObject.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Oka değilirse hasar alınır.
        if(other.gameObject.tag == "Arrow")
        {
            Debug.Log("Hasar alındı");
        }
    }
}
