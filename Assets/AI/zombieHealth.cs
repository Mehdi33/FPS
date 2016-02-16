using UnityEngine;
using System.Collections;
using System;

public class zombieHealth : MonoBehaviour {

    public int ZombieHealth = 100;
	// Use this for initialization
	void OnCollisionEnter (Collision col) {
	    if(col.gameObject.tag == "Bullet")
        {
            ZombieHealth -= 25;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(ZombieHealth <= 0)
        {
            GetComponent<Animator>().Play("back_fall");
            gameObject.GetComponent<zombieAI>().enabled = false;
            gameObject.GetComponent<CharacterController>().enabled = false;
            Dead();
        }
	}

    void Dead()
    {
        StartCoroutine(disparu());

    }
    IEnumerator disparu()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
