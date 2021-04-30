using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Tämä koodi on dialogisysteemin pääkoodi, mikä vastaanottaa toisesta koodista toimintoja
namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; protected set; }

        //Saa dialogiin tekstin toiminnat
        protected IEnumerator WriteText(string input, Text textHolder, float delay, float delayBetweenLines/*, Color textColor, Font textFont, AudioClip sound*/)
        {
            //Vapaaehtoiset lisäykset ovat toistaiseksi kommentteina
            /*textHolder.color = textColor;*/
            /*textHolder.font = textFont;*/

            //Dialogin teksti kirjoittaa automaattisesti
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
            }

            /*yield return new WaitForSeconds(delayBetweenLines);*/
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            finished = true;
        }
    }
}
