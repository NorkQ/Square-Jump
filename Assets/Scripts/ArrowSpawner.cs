using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject _arrow;
    public float spawnRate;
    public float _speed;
    

    public Transform _player;

    private float timer;
    public Transform[] _spawnPoints;
    public ParticleSystem _ps;
    public BoxCollider BoxColliderBow;

    private void Start()
    {
        timer = spawnRate;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        //Belirlenen saniyede bir çalışır. Örneğin 3 saniyede 1 kez.
        if(timer <= 0)
        {
            //Spawn noktalarından birini seçiyoruz.
            int randomNumber = Random.Range(0, _spawnPoints.Length);
            Transform spawnPoint = _spawnPoints[randomNumber];

            //Kopya oluşturuyoruz.
            GameObject _cloneArrow = Instantiate(_arrow, spawnPoint.position, Quaternion.identity);
            
            

            //Oyuncuya okun geldiğini belirtmek için particle system kullandım. Burada ok oluşturulduğu zaman particle system'ı oyuncunun olduğu yere taşıyoruz ve oynatıyoruz.
            _ps.transform.position = _player.position;
            _ps.Play();

            //Okun rotasyonunu karaktere dik olacak şekilde ayarlıyoruz.
            _cloneArrow.transform.rotation = Quaternion.Euler(new Vector3(0, spawnPoint.gameObject.GetComponent<ArrowSpawnPoint>().degree, 0));
            
            //Oku gönderiyoruz.
            _cloneArrow.GetComponent<Rigidbody>().velocity = spawnPoint.right * _speed;
            timer = spawnRate;
        }
    }

}
