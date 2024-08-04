using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boostpowerup : MonoBehaviour
{
    private PipeSpawner pipespawner;

    // Update is called once per frame
    void Update()
    {
        pipespawner = FindAnyObjectByType<PipeSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pipespawner.speedup = true;
            Destroy(gameObject);
        }
    }
}
