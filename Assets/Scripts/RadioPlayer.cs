using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadioPlayer : MonoBehaviour {

    public RadioTuner radioTuner;
    public AudioSource noisePlayer;
    public AudioSource stationPlayer;
    public AudioClip[] clips;
    public Vision vision;

    public bool AllTransmissionsDone
    {
        get; private set;
    }

    private int[] musicFreqs;
    private int[] transmissionFreqs;
    private int nextTransmission = 0;
    private int currentFreq;

	// Use this for initialization
	void Start ()
    {
        AllTransmissionsDone = false;
        var list = new List<int>();
        do
        {
            var newFreq = Random.Range(440, 540);
            list.Add(newFreq);
            list = list.Distinct().ToList();
        } while (list.Count < clips.Length);
        musicFreqs = list.Take(6).ToArray();
        transmissionFreqs = list.Skip(6).ToArray();
        currentFreq = radioTuner.initialFrequency;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (!AllTransmissionsDone && radioTuner.Frequency != currentFreq)
        {
            currentFreq = radioTuner.Frequency;
            if (currentFreq == transmissionFreqs[nextTransmission])
            {
                StartCoroutine(PlayNextTransmission());
                return;
            }
            for (int i = 0; i < musicFreqs.Length; i++)
            {
                if (currentFreq == musicFreqs[i])
                {
                    PlayMusic(i);
                    return;
                }
            }
            PlayNoise();
        }
	}

    void PlayMusic(int clip)
    {
        noisePlayer.volume = 0;
        stationPlayer.volume = 1;
        stationPlayer.loop = true;
        stationPlayer.clip = clips[clip];
        stationPlayer.time = Random.Range(15, 45);
        stationPlayer.Play();
    }

    void PlayNoise()
    {
        stationPlayer.Stop();
        stationPlayer.volume = 0;
        noisePlayer.volume = 1;
    }

    IEnumerator PlayNextTransmission()
    {
        radioTuner.TunerEnabled = false;
        noisePlayer.volume = 0;
        stationPlayer.volume = 1;
        stationPlayer.loop = false;
        var c = clips[6 + nextTransmission];
        stationPlayer.clip = c;
        stationPlayer.time = 0;
        stationPlayer.Play();
        yield return new WaitForSeconds(c.length);

        yield return vision.ShowVision(nextTransmission);
        if (++nextTransmission == (clips.Length - 6))
        {
            AllTransmissionsDone = true;
        }
        else if (!vision.GameOver)
        {
            radioTuner.TunerEnabled = true;
            PlayNoise();
        }
    }
}
