using UnityEngine;
using System.Collections;

namespace Loop
{
    public class Clone_Trigger_Delete : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "Player")
            {
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
        }
    }
}
