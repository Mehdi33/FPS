using UnityEngine;
using System.Collections;

public class GrenadeExplosion : MonoBehaviour {

    public int timer = 3;
    public GameObject ExplosionZone;
	// Use this for initialization
	void Start () {
        StartCoroutine(explosion());
    }
    IEnumerator explosion()
    {
        yield return new WaitForSeconds(timer);
        Appear();
    }

    // Update is called once per frame
    void Appear () {
        Instantiate(ExplosionZone, transform.position, transform.rotation);
        Destroy(gameObject);
	}
}
