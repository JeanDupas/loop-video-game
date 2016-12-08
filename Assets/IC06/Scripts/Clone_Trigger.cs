using UnityEngine;
using System.Collections;
namespace Loop
{
    public class Clone_Trigger : MonoBehaviour
    {
        // Endroit vers lequel afficher le clone
        public GameObject target;

        public bool inverted;

        private bool isSet;

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
            if (!isSet)
            {

                Instantiate(Resources.Load("Clone"), new Vector3(target.transform.position.x, target.transform.position.y), Quaternion.identity);
                isSet = true;
            }
            /*
            var newPos = new Vector3(col.transform.position.x, col.transform.position.y, col.transform.position.z);
            if (Axis_X)
                newPos.Set(target.transform.position.x, newPos.y, newPos.z);
            if (Axis_Y)
                newPos.Set(newPos.x, target.transform.position.y + (col.transform.position.y - this.transform.position.y), newPos.z);
            Debug.Log(col.transform.position.y - this.transform.position.y);
            col.transform.position = newPos;*/
        }

        public void setIsSet(bool set)
        {
            this.isSet = set;
        }
    }
}
