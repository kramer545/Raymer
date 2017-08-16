using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foamScroll : MonoBehaviour {

    public float scrollSpeed;
    float offset = 0;
    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        offset += Time.deltaTime * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(-offset,offset));
	}
}
