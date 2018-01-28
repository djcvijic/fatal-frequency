using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Text tutorialText;

    private bool textDisplayed = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (textDisplayed && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)))
        {
            StartCoroutine(HideText());
        }
    }

    IEnumerator HideText()
    {
        textDisplayed = false;
        yield return new WaitForSeconds(5f);
        var c = tutorialText.color;
        tutorialText.color = new Color(c.r, c.g, c.b, 0);
    }
}
