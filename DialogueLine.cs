using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//This script works for the text lines in the dialogue system
namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;
        public DialogueHolderTwo DHT;

        [Header("Text Options")]
        [SerializeField] public string input;
        [Header("Time parameters")]
        [SerializeField] public float delay;
        [SerializeField] public float delayBetweenLines;

        private IEnumerator lineAppear;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //When user does not skip the dialogues, the dialogue will type automatically until all text is on the dialogue
                if (textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }

                //When clicking mouse button while the dialogue is going on, the dialogue's automatic typing can be skipped
                else
                {
                    finished = true;
                }
            }
        }

        //When calling this function, the lines will appear
        public void OnEnable()
        {
            ResetLine();
            lineAppear = WriteText(input, textHolder, delay, delayBetweenLines);
            StartCoroutine(lineAppear);
        }

        //When calling this function, the lines will reset
        public void ResetLine()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";
            finished = false;
        }
    }
}