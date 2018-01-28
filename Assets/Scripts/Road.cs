using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    private Transform[] stripes;

	// Use this for initialization
	void Start () {
        stripes = GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var stripe in stripes)
        {
            var newZ = stripe.localPosition.z - 0.005f;
            if (newZ < 0)
            {
                newZ += 0.7f;
            }
            stripe.localPosition = new Vector3(stripe.localPosition.x, stripe.localPosition.y, newZ);
        }
	}
}
