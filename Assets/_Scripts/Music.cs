using UnityEngine;

public class Music : MonoBehaviour
{
    private bool playing = true;

    public void ToogleMusic()
    {
        if (!playing)
            GetComponent<AudioSource>().UnPause();
        else
            GetComponent<AudioSource>().Pause();
        playing = !playing; 
    }
}
