using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilindroBehaviour : MonoBehaviour {

    public GameObject explosionEffect;

    public AudioClip impact;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Awake () {
		//gameObject.GetComponent<Collider>().isTrigger
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

    void OnCollisionEnter(Collision collision)
    {
        /*var main = explosionEffect.GetComponent<ParticleSystem>().main;
        main.loop = false;*/
        audioSource.PlayOneShot(impact, 0.7F);
        GameObject explosion = Instantiate(explosionEffect, collision.collider.gameObject.transform.position, transform.rotation);
        Destroy(collision.collider.gameObject);
        string dateAndTimeVar = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        Debug.Log("BOOM! " + dateAndTimeVar);
    }
}
