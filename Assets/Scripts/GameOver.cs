using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class GameOver : MonoBehaviour {

    public Vision vision;
    public AudioSource crashPlayer;

    private bool hasPlayed = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (vision.GameOver && !hasPlayed)
        {
            hasPlayed = true;
            GetComponent<Canvas>().enabled = true;
            crashPlayer.Play();
        }
	}
}
