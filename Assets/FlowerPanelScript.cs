using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPanelScript : MonoBehaviour {

	public Xeriscape_intro_script script1;
    bool used = false;
    public hideRounds hider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void dismiss (bool addSeed) {
        hider.show();
		Cursor.lockState = CursorLockMode.Locked;
		script1.lookEnabled = true;
		script1.toggleInstructions(true);
        if(addSeed && !used)
        {
            script1.incrementSeeds();
            used = true;
        }
		this.gameObject.SetActive(false);
	}
}
