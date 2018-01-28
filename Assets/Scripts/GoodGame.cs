using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class GoodGame : MonoBehaviour {

    public RadioPlayer radioPlayer;
    public Vision vision;
    public AudioSource loungPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!vision.GameOver && radioPlayer.AllTransmissionsDone && !loungPlayer.isPlaying)
        {
            GetComponent<Canvas>().enabled = true;
            loungPlayer.Play();
        }
	}
}
