using UnityEngine;
using System.Collections;

namespace Loop
{
    public class Teleport_Trigger : MonoBehaviour
    {
        // Endroit vers lequel téléporter
        public Transform target;
        // Si on téléporte sur l'axe des X
        public bool Axis_X;
        // Si on téléporte sur l'axe des Y
        public bool Axis_Y;

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
            var newPos = new Vector3(col.transform.position.x, col.transform.position.y, col.transform.position.z);
            if (Axis_X)
                newPos.Set(target.position.x,newPos.y,newPos.z);
            if(Axis_Y)
                newPos.Set(newPos.x, target.position.y, newPos.z);
            col.transform.position = newPos;

            Level_Controller.instance.teleportTrigger();
        }
    }
}