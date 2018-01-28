using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDShake : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(ScheduleShake());
    }
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator ScheduleShake()
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        //yield return new WaitForSeconds(2f);
        //shouldShake = true;
        StartCoroutine(Shake());
        StartCoroutine(ScheduleShake());
    }

    IEnumerator Shake()
    {
        var rt = GetComponent<RectTransform>();
        for (var elapsedTime = 0f; elapsedTime < 0.5f; elapsedTime += Time.deltaTime)
        {
            var lerp = Mathf.PingPong(elapsedTime, 0.2f);
            var shakeOffset = Mathf.Lerp(0f, 20f, lerp);
            rt.offsetMin = rt.offsetMax = new Vector2(0f, shakeOffset);
            yield return null;
        }
    }
}
