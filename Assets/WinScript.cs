using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public LogicScript logic;
    private AudioSource audioSource;
    // Start is called before the first frame update

    private IEnumerator WaitForSongToEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        
        logic.WinGame();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        StartCoroutine(WaitForSongToEnd());
    }
}
