using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RadioTuner : MonoBehaviour {

    public Text displayText;

    [Range(440, 540)]
    public int initialFrequency = 490;

    public int Frequency
    {
        get; private set;
    }

    public bool TunerEnabled
    {
        get; set;
    }

    private RectTransform rt;
    private float initialXMin, initialXMax;
    private float xMin, xMax;

    private float timeUntilNextTune = 0;

	// Use this for initialization
	void Start ()
    {
        Frequency = initialFrequency;
        TunerEnabled = true;
        rt = GetComponent<RectTransform>();
        xMin = initialXMin = rt.anchorMin.x;
        xMax = initialXMax = rt.anchorMax.x;
        updateFrequency(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (timeUntilNextTune > 0)
        {
            timeUntilNextTune -= Time.deltaTime;
        }
        else if (TunerEnabled)
        {
            timeUntilNextTune = 0.1f;
            if (Input.GetKey(KeyCode.Q))
            {
                updateFrequency(-1);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                updateFrequency(+1);
            }
        }
    }

    void updateFrequency(int offset)
    {
        Frequency = Mathf.Clamp(Frequency + offset, 440, 540);
        xMin = initialXMin + (Frequency - initialFrequency) * 0.0016f;
        rt.anchorMin = new Vector2(xMin, rt.anchorMin.y);
        xMax = initialXMax + (Frequency - initialFrequency) * 0.0016f;
        rt.anchorMax = new Vector2(xMax, rt.anchorMax.y);
        displayText.text = (Frequency * 0.2f).ToString("##0.0");
    }
}
