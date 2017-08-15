using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleBush : MonoBehaviour
{

    public GameObject org;
    public bool active = false;
    public float radius = 0.8F;
    //public int amount = 75;
    public float maxScale;
    public float minScale;
    public float yPos = 0;
    public bool debug = false;

    // Use this for initialization
    void Start()
    {
        if (active)
        {
            active = false;
            float xPos = transform.position.x;
            float zPos = transform.position.z;
            for(int y = 1;y<6;y++)
            {
                float h = radius / y;
                for (int x = 0; x < 10*y; x++)
                {
                    float angle = ((2 * Mathf.PI) / (10 * y)) * x;
                    GameObject clone = Instantiate(org);
                    clone.transform.position = new Vector3(xPos + h * Mathf.Sin(angle), yPos, zPos + h * Mathf.Cos(angle));
                    clone.transform.eulerAngles = new Vector3(Random.Range(-15F, 15F), Random.Range(0F, 360F), Random.Range(-15F, 15F));
                    float scale = maxScale - (((maxScale - minScale) / 5) * (6 - y));
                    clone.transform.localScale = new Vector3(scale, scale, scale);
                    if (debug)
                        Debug.Log(Vector3.Distance(clone.transform.position, transform.position));
                    if (Vector3.Distance(clone.transform.position, transform.position)+0.2F >= radius)
                        Destroy(clone);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
