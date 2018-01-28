using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class Vision : MonoBehaviour {

    public Image blackPanel;

    private bool isShown = false;
    private int currentVision;
    private bool showGui = false;
    private bool isAnswered = false;

    public bool GameOver
    {
        get; private set;
    }

    public IEnumerator ShowVision(int i)
    {
        currentVision = i;
        float elapsed = 1f;
        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            Color c = blackPanel.color;
            blackPanel.color = new Color(c.r, c.g, c.b, 1 - elapsed / 1f);
            yield return null;
        }
        isShown = true;
        GetComponent<Canvas>().enabled = true;
        elapsed = 1f;
        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            Color c = blackPanel.color;
            blackPanel.color = new Color(c.r, c.g, c.b, elapsed / 1f);
            yield return null;
        }
        while (isShown)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

	// Use this for initialization
	void Start () {
        GameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isShown && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ReturnFromVision());
        }
	}

    IEnumerator ReturnFromVision()
    {
        float elapsed = 1f;
        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            Color c = blackPanel.color;
            blackPanel.color = new Color(c.r, c.g, c.b, 1 - elapsed / 1f);
            yield return null;
        }
        GetComponent<Canvas>().enabled = false;
        showGui = true;
        while (!isAnswered)
        {
            yield return null;
        }
        elapsed = 1f;
        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            Color c = blackPanel.color;
            blackPanel.color = new Color(c.r, c.g, c.b, elapsed / 1f);
            yield return null;
        }
        isAnswered = false;
        showGui = false;
        isShown = false;
    }

    private Rect windowRect = new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2);
    void OnGUI()
    {
        if (showGui && !isAnswered)
        {
            string[] questions =
            {
                "How many bones were there at the scene?",
                "How many windows were there at the scene?",
            };
            GUI.Window(currentVision, windowRect, DoMyWindow, questions[currentVision]);
        }
    }
    void DoMyWindow(int windowID)
    {
        switch (windowID)
        {
            case 0:
                if (GUI.Button(new Rect(10, 20, 100, 20), "5"))
                {
                    isAnswered = true;
                }
                if (GUI.Button(new Rect(10, 50, 100, 20), "4"))
                {
                    isAnswered = true;
                    GameOver = true;
                }
                if (GUI.Button(new Rect(10, 80, 100, 20), "3"))
                {
                    isAnswered = true;
                    GameOver = true;
                }
                break;
            case 1:
                if (GUI.Button(new Rect(10, 20, 100, 20), "5"))
                {
                    isAnswered = true;
                    GameOver = true;
                }
                if (GUI.Button(new Rect(10, 50, 100, 20), "2"))
                {
                    isAnswered = true;
                    GameOver = true;
                }
                if (GUI.Button(new Rect(10, 80, 100, 20), "3"))
                {
                    isAnswered = true;
                }
                break;
        }
    }
}
