using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ObjectiveTrigger : MonoBehaviour
{
    public bool AlreadyShown = false;
    public float TimeToShowDialogue;
    public string ObjectiveNumberToDisplay;
    public string TextToDisplay;
    public string JournalObjectiveText;
    public Text ObjectiveNumberToShow;
    public Text TextToShow;
    public Text ObjectiveForJournal;
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (AlreadyShown == false)
            {
                AlreadyShown = true;
                ObjectiveNumberToShow.text = ObjectiveNumberToDisplay;
                TextToShow.text = TextToDisplay;
                TextToShow.enabled = true;
                ObjectiveForJournal.text = JournalObjectiveText;
                ObjectiveNumberToShow.enabled = true;

                yield return new WaitForSeconds(TimeToShowDialogue);
                TextToShow.enabled = false;
                ObjectiveNumberToShow.enabled = false;
            }
        }

    }
}