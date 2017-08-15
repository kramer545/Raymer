using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerCollider : MonoBehaviour {

    public GameObject[] colliders;
    public OnEnableFlower[] flowers;
	public OnEnableFlower[] npcRounds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        for(int x = 0;x<flowers.Length;x++)
        {
            if (other.name == colliders[x].name)
            {
                flowers[x].EnableImage();
				if(colliders [x].name == "npc_collide")
				{
					for(int y = 0;y<npcRounds.Length;y++)
					{
						npcRounds [y].EnableImage ();
					}
				}	
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int x = 0; x < flowers.Length; x++)
        {
            if (other.name == colliders[x].name)
            {
                flowers[x].DisableImage();
				if(colliders [x].name == "npc_collide")
				{
					for(int y = 0;y<npcRounds.Length;y++)
					{
						npcRounds [y].DisableImage ();
					}
				}	
            }
        }
    }

}
