using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

    // Endroit vers lequel téléporter
    public Transform target;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.transform.position = new Vector3(target.position.x, col.transform.position.y, col.transform.position.z);
            Level_Controller.instance.nextLevel();
        }
        
    }
}
