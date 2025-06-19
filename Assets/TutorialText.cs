using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public GameObject fullText;
    public TMP_Text tutorialText;

    void Start()
    {
        StartCoroutine(Tutorial());
    }

    private IEnumerator Tutorial()
    {
        tutorialText.text = "You spin in circles!";
        yield return new WaitForSeconds(5);
        tutorialText.text = "Whenever you click, you shoot wherever the arrow points and turn the other direction";
        yield return new WaitForSeconds(10);
        tutorialText.text = "Kill the note block!";
        yield return new WaitForSeconds(5);
        tutorialText.text = "The yellow bar shows your super charge rate. Making a perfect combo of 30 (only in 10 in this level) will give you temporary unlimited bullets!";
        yield return new WaitForSeconds(15);
        tutorialText.text = "The rest is up to you! Good luck.";
        yield return new WaitForSeconds(5);
        fullText.SetActive(false);
    }
}
