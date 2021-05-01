using System.Collections;
using UnityEngine;

//This script is the one that holds the dialogue system together
namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        public GameObject player;
        public bool dialogueFinished;
        public PickableItems pickableItems;

        public void OnEnable()
        {
            StartCoroutine(dialogueSequence());
        }

        public IEnumerator dialogueSequence()
        {
            //As long as the dialogue is not finished, the code will loop the dialogue
            if (!dialogueFinished)
            {
                for (int i = 0; i < transform.childCount - 1; i++)
                {
                    Deactivate();
                    transform.GetChild(i).gameObject.SetActive(true);
                    yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
                }
            }

            //When dialogue has finished, the user can move to the next dialogue or close the dialogues
            else
            {
                int index = transform.childCount - 1;
                Deactivate();
                transform.GetChild(index).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(index).GetComponent<DialogueLine>().finished);
            }

            //Turns off the dialogues
            dialogueFinished = true;
            gameObject.SetActive(false);
        }

        //When calling this function, the dialogues will get deactivated
        public void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
