using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawnPoint : MonoBehaviour
{
    public Transform player;
    public int degree;
    public Vector3 _offset;

    private void FixedUpdate()
    {
        //Spawn noktasını sürekli oyuncu küpünün etrafında tutuyorum.
        transform.position = new Vector3(player.position.x + _offset.x, player.position.y - 0.4f, player.position.z + _offset.z);
    }
}
