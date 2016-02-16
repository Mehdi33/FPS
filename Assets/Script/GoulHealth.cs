using UnityEngine;
using System.Collections;

public class GoulHealth : MonoBehaviour {

    public int ZombieHealth = 100;
    // Use this for initialization
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            ZombieHealth -= 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ZombieHealth <= 0)
        {
            GetComponent<Animator>().Play("die");
            gameObject.GetComponent<GouleAI>().enabled = false;
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
