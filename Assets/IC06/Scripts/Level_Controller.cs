using UnityEngine;
using System.Collections;

public class Level_Controller : MonoBehaviour {

    public static Level_Controller instance = null;

    private int level;

    private Animator groundAnimator;
    private Animator obstaclesAnimator;
    private Animator backgroundAnimator;

    public GameObject ground;
    public GameObject obstacles;
    public GameObject background;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        level = 1;

        groundAnimator = ground.GetComponent<Animator>();
        obstaclesAnimator = obstacles.GetComponent<Animator>();
        backgroundAnimator = background.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void nextLevel()
    {
        level++;
        backgroundAnimator.SetTrigger("resetDay");
        switch (level)
        {
            case 2:
                setLevel2();
                break;
        }
    }

    public void teleportTrigger()
    {
        Debug.Log("Teleport Trigger");
        Debug.Log("Level "+level);
        backgroundAnimator.SetTrigger("switchDay");
        switch (level)
        {
            case 2:
                teleportTriggerLevel2();
                break;
        }
    }

    private void setLevel2()
    {
        groundAnimator.SetInteger("level", 2);
        obstaclesAnimator.SetInteger("level", 2);
    }

    private void teleportTriggerLevel2()
    {
        Debug.Log("Teleport Trigger Level 2");
        obstaclesAnimator.SetTrigger("switchState");
    }


}
