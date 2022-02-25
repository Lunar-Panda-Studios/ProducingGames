using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DialogueTrigger : MonoBehaviour
{
    public bool AlreadyShown = false;
    public float TimeToShowDialogue;
    public string CharacterNameToDisplay;
    public string TextToDisplay;
    public Text CharacterNameToShow;
    public Text TextToShow;
    public Image Fade;
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (AlreadyShown == false)
            {
                AlreadyShown = true;
                CharacterNameToShow.text = CharacterNameToDisplay;
                TextToShow.text = TextToDisplay;
                TextToShow.enabled = true;
                Fade.enabled = true;
                CharacterNameToShow.enabled = true;

                yield return new WaitForSeconds(TimeToShowDialogue);
                Fade.enabled = false;
                TextToShow.enabled = false;
                CharacterNameToShow.enabled = false;
            }
        }

    }
}