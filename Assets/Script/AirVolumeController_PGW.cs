using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVolumeController_PGW : MonoBehaviour
{
    [SerializeField] private AudioSource theAudio = null;
    [SerializeField] private float maxVolume = 0;
    [SerializeField] private float minVolume = 0;

    private IEnumerator IncreaseVolume()
    {

        while (theAudio.volume < maxVolume)
        {
            theAudio.volume = Mathf.Lerp(theAudio.volume, maxVolume, 0.05f);
            if (theAudio.volume > maxVolume - 0.05f)
            {
                theAudio.volume = maxVolume;
            }
            yield return null;

        }

    }
    private IEnumerator DecreaseVolume()
    {
        while (theAudio.volume > minVolume)
        {
            theAudio.volume = Mathf.Lerp(theAudio.volume, minVolume, 0.05f);

            if (theAudio.volume < minVolume + 0.05f)
            {
                theAudio.volume = minVolume;
            }
            yield return null;


        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            StopCoroutine("IncreaseVolume");
            StartCoroutine("DecreaseVolume");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            StopCoroutine("DecreaseVolume");
            StartCoroutine("IncreaseVolume");
        }
    }

}
