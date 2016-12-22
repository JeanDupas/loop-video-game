using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Loop;

public class Level_Controller : MonoBehaviour {

    public static Level_Controller instance = null;

    private int level;
    private bool day = true;
    private bool _isOpened = false;

    private Animator groundAnimator;
    private Animator obstaclesAnimator;
    private Animator backgroundAnimator;
    private Animator endDisplayAnimator;
    private Animator messageDisplayAnimator;

    public GameObject ground;
    public GameObject obstacles;
    public GameObject background;

    public GameObject endDisplay;
    public GameObject messageDisplay;
    public GameObject menuDisplay;

    public int Debuglevel;

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
        day = true;

        groundAnimator = ground.GetComponent<Animator>();
        obstaclesAnimator = obstacles.GetComponent<Animator>();
        backgroundAnimator = background.GetComponent<Animator>();
        endDisplayAnimator = endDisplay.GetComponent<Animator>();
        messageDisplayAnimator = messageDisplay.GetComponent<Animator>();

        level = Debuglevel;
        groundAnimator.SetInteger("level", Debuglevel);
        obstaclesAnimator.SetInteger("level", Debuglevel);

        endDisplayAnimator.SetTrigger("start");

        menuDisplay.SetActive(false);

        //endDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	    if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleMenu();
        }
	}

    public void ToggleMenu()
    {

        if (_isOpened)
            CloseMenu();
        else
            OpenMenu();

        _isOpened = !_isOpened;
    }

    public void OpenMenu()
    {
        menuDisplay.SetActive(true);
    }

    public void CloseMenu()
    {
        menuDisplay.SetActive(false);
    }


    void OnLevelWasLoaded(int level)
    {
        obstacles = GameObject.Find("Obstacles");
        ground = GameObject.Find("Ground");
        background = GameObject.Find("BackGround");
        endDisplay = GameObject.Find("Fade");
        messageDisplay = GameObject.Find("MessageDisplay");
        menuDisplay = GameObject.Find("Menu");

        menuDisplay.SetActive(false);

        groundAnimator = obstacles.GetComponent<Animator>();
        obstaclesAnimator = obstacles.GetComponent<Animator>();
        backgroundAnimator = background.GetComponent<Animator>();
        endDisplayAnimator = endDisplay.GetComponent<Animator>();
        messageDisplayAnimator = messageDisplay.GetComponent<Animator>();

        obstaclesAnimator.SetInteger("level", this.level);
        endDisplayAnimator.SetTrigger("start");
    }

    public void nextLevel()
    {
        level++;
        backgroundAnimator.SetTrigger("resetDay");
        day = true;
        Loop.SoundManager.instance.ChangeMusicPitch(day);
        switch (level)
        {
            case 2:
                setLevelDefault();
                break;
            case 3:
                setLevelDefault();
                break;
            case 4:
                setLevelDefault();
                break;
            case 5:
                setLevel5();
                break;
            case 6:
                setLevel6();
                break;
            case 7:
                setLevelDefault();
                break;
            case 8:
                setLevelDefault();
                break;
            case 9:
                setLevelDefault();
                break;
            case 10:
                setLevelDefault();
                break;
            case 11:
                setLevel9();
                break;
        }
    }

    public void teleportTrigger()
    {
        Debug.Log("Teleport Trigger");
        Debug.Log("Level "+level);
        backgroundAnimator.SetTrigger("switchDay");
        day = !day;
        Loop.SoundManager.instance.ChangeMusicPitch(day);
        switch (level)
        {
            case 2:
                teleportTriggerLevel2();
                break;
            case 3:
                teleportTriggerLevel2();
                break;
            case 4:
                teleportTriggerLevel2();
                break;
            case 5:
                teleportTriggerLevel2();
                break;
            case 6:
                teleportTriggerLevel2();
                break;
            case 7:
                teleportTriggerLevel2();
                break;
            case 8:
                teleportTriggerLevel8();
                break;
            case 9:
                teleportTriggerLevel2();
                break;
            case 10:
                teleportTriggerLevel2();
                break;
        }
    }

    private void setLevel6()
    {
        backgroundAnimator.SetTrigger("end");
        endDisplayAnimator.SetTrigger("end");
        Invoke("changeLevel", 5.0f);
    }

    private void setLevel9()
    {
        backgroundAnimator.SetTrigger("end");
        endDisplayAnimator.SetTrigger("end");
        Invoke("quitGame", 8.0f);
    }

    private void changeLevel()
    {
        SceneManager.LoadScene(2);
    }

    private void quitGame()
    {
        Application.Quit();
    }

    private void teleportTriggerLevel2()
    {
        Debug.Log("Teleport Trigger Level 2");
        obstaclesAnimator.SetTrigger("switchState");
    }

    private void teleportTriggerLevel8()
    {
        Debug.Log("Teleport Trigger Level 2");
        obstaclesAnimator.SetTrigger("switchState");
        GameObject obstacles = GameObject.Find("Obstacles");
        Transform level3 = obstacles.transform.Find("Level_03");
        Transform portalGreenDown = level3.transform.Find("Portal_Green_Down");
        Transform portalGreenUp = level3.transform.Find("Portal_Green_Up");
        Transform portalBlueDown = level3.transform.Find("Portal_Blue_Down");
        Transform portalBlueUp = level3.transform.Find("Portal_Blue_Up");
        Transform portalRedDown = level3.transform.Find("Portal_Red_Down");
        Transform portalRedUp = level3.transform.Find("Portal_Red_Up");

        if (day)
        {
            portalGreenDown.transform.Find("Portal_Green_Trigger_Source_Right").GetComponent<Loop.Teleport_To>().target = portalGreenUp.transform.Find("Portal_Green_Trigger_Target_Left").gameObject;
            portalGreenDown.transform.Find("Portal_Green_Trigger_Source_Left").GetComponent<Loop.Teleport_To>().target = portalGreenUp.transform.Find("Portal_Green_Trigger_Target_Right").gameObject;

            portalGreenUp.transform.Find("Portal_Green_Trigger_Source_Right").GetComponent<Loop.Teleport_To>().target = portalGreenDown.transform.Find("Portal_Green_Trigger_Target_Left").gameObject;
            portalGreenUp.transform.Find("Portal_Green_Trigger_Source_Left").GetComponent<Loop.Teleport_To>().target = portalGreenDown.transform.Find("Portal_Green_Trigger_Target_Right").gameObject;

            portalBlueDown.transform.Find("Portal_Green_Trigger_Source_Right").GetComponent<Loop.Teleport_To>().target = portalBlueUp.transform.Find("Portal_Green_Trigger_Target_Left").gameObject;
            portalBlueDown.transform.Find("Portal_Green_Trigger_Source_Left").GetComponent<Loop.Teleport_To>().target = portalBlueUp.transform.Find("Portal_Green_Trigger_Target_Right").gameObject;

            portalBlueUp.transform.Find("Portal_Green_Trigger_Source_Right").GetComponent<Loop.Teleport_To>().target = portalBlueDown.transform.Find("Portal_Green_Trigger_Target_Left").gameObject;
            portalBlueUp.transform.Find("Portal_Green_Trigger_Source_Left").GetComponent<Loop.Teleport_To>().target = portalBlueDown.transform.Find("Portal_Green_Trigger_Target_Right").gameObject;

            portalRedDown.gameObject.SetActive(true);
            portalRedUp.gameObject.SetActive(true);
        }
        else
        {
            portalGreenDown.transform.Find("Portal_Green_Trigger_Source_Right").GetComponent<Loop.Teleport_To>().target = portalBlueDown.transform.Find("Portal_Green_Trigger_Target_Left").gameObject;
            portalGreenDown.transform.Find("Portal_Green_Trigger_Source_Left").GetComponent<Loop.Teleport_To>().target = portalBlueDown.transform.Find("Portal_Green_Trigger_Target_Right").gameObject;

            portalBlueDown.transform.Find("Portal_Green_Trigger_Source_Right").GetComponent<Loop.Teleport_To>().target = portalGreenDown.transform.Find("Portal_Green_Trigger_Target_Left").gameObject;
            portalBlueDown.transform.Find("Portal_Green_Trigger_Source_Left").GetComponent<Loop.Teleport_To>().target = portalGreenDown.transform.Find("Portal_Green_Trigger_Target_Right").gameObject;

            portalGreenUp.transform.Find("Portal_Green_Trigger_Source_Right").GetComponent<Loop.Teleport_To>().target = portalBlueUp.transform.Find("Portal_Green_Trigger_Target_Left").gameObject;
            portalGreenUp.transform.Find("Portal_Green_Trigger_Source_Left").GetComponent<Loop.Teleport_To>().target = portalBlueUp.transform.Find("Portal_Green_Trigger_Target_Right").gameObject;

            portalBlueUp.transform.Find("Portal_Green_Trigger_Source_Right").GetComponent<Loop.Teleport_To>().target = portalGreenUp.transform.Find("Portal_Green_Trigger_Target_Left").gameObject;
            portalBlueUp.transform.Find("Portal_Green_Trigger_Source_Left").GetComponent<Loop.Teleport_To>().target = portalGreenUp.transform.Find("Portal_Green_Trigger_Target_Right").gameObject;

            portalRedDown.gameObject.SetActive(false);
            portalRedUp.gameObject.SetActive(false);
        }
    }

    private void setLevelDefault()
    {
        groundAnimator.SetInteger("level", level);
        obstaclesAnimator.SetInteger("level", level);
        messageDisplayAnimator.SetInteger("level", level);
    }

    private void setLevelDefaultClone()
    {
        groundAnimator.SetInteger("level", level);
        obstaclesAnimator.SetInteger("level", level);
        messageDisplayAnimator.SetInteger("level", level);

        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Clone_Spawn");

        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }

        foreach (GameObject spawn in spawns)
        {
            spawn.GetComponent<Clone_Trigger>().setIsSet(false);

        }
    }

    private void setLevel5()
    {
        endDisplay.SetActive(true);
        groundAnimator.SetInteger("level", 5);
        obstaclesAnimator.SetInteger("level", 5);
    }

}
