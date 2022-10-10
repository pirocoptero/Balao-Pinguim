using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.transform.position.x);
        if (col.gameObject.name == "Baita")
        {
            if (col.transform.position.x < 0)
            {
                col.transform.position = new Vector3(2.7f, col.transform.position.y, col.transform.position.z);
            } else {
                col.transform.position = new Vector3(-2.7f, col.transform.position.y, col.transform.position.z);
            }
        }
    }


}