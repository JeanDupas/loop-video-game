using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(1);
        }
    }
}
