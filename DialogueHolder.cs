using System.Collections;
using UnityEngine;

//Tämä koodi on dialogisysteemin pääkoodi, mikä vastaanottaa toisesta koodista toimintoja
namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        public GameObject player;
        public bool dialogueFinished;
        public PickableItems pickableItems;

        //Herätessä käynnistää dialogisysteemin
        public void OnEnable()
        {
            StartCoroutine(dialogueSequence());
        }

        //Aloittaa dialogin toiminnan
        public IEnumerator dialogueSequence()
        {
            //Jos dialogi ei ole päättynyt, dialogi jatkaa toimintaa
            if (!dialogueFinished)
            {
                for (int i = 0; i < transform.childCount - 1; i++)
                {
                    Deactivate();
                    transform.GetChild(i).gameObject.SetActive(true);
                    yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
                }
            }

            //Jos dialogi on päättynyt, voit vaihtaa seuraavaan dialogiin
            else
            {
                int index = transform.childCount - 1;
                Deactivate();
                transform.GetChild(index).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(index).GetComponent<DialogueLine>().finished);
            }

            //player.GetComponent<PlayerMovement>().enabled = true;
            dialogueFinished = true;
            gameObject.SetActive(false);
        }

        //Sulkee dialogin
        public void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
