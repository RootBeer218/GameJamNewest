using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Budcollect : MonoBehaviour
{
    private bool canchase = false;
    private GameObject chaserbf;
    public float distance;
    public float speed;
    public Vector2 followpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canchase = true;
            chaserbf = collision.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        followpoint = new Vector2(chaserbf.transform.position.x - 1.5f, chaserbf.transform.position.y);
        if (canchase == true)
        {
            distance = Vector2.Distance(transform.position, chaserbf.transform.position);
            Vector2 direction = chaserbf.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position, followpoint, speed * Time.deltaTime);
        }
    }
}
