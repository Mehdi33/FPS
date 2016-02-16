using UnityEngine;
using System.Collections;

public class ExplosionZone : MonoBehaviour {

    public int timer = 2;
    public int dammageGrenade = 50;
    // Use this for initialization
    void Start () {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        StartCoroutine(explosion());

    }
    IEnumerator explosion()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider hit) {
	if(hit.gameObject.tag == "Ennemi")
        {
            hit.gameObject.GetComponent<EnnemyHealth>().EnnemiHealth -= dammageGrenade;
        }
	}
}
