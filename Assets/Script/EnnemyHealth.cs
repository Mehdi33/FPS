using UnityEngine;
using System.Collections;

public class EnnemyHealth : MonoBehaviour {

    public int EnnemiHealth = 100;

    void  OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            EnnemiHealth -= 10;
        }
    }
	
	// Update is called once per frame
	void Update () {
	if(EnnemiHealth <= 0)
        {
            Destroy(gameObject);
        }
	}
}
