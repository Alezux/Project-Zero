using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//This is the main code of the dialog system, which will receive the actions from other scripts
namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; protected set; }

        //This will make the dialog text system to work and gives it features
        protected IEnumerator WriteText(string input, Text textHolder, float delay, float delayBetweenLines/)
        {

            //As a loop the dialog text will type automatically
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
            }

            //When text has finished typing, the loop will stop until user continues
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            finished = true;
        }
    }
}
