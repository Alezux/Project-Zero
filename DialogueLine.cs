using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Tämä koodi on dialogisysteemin pääkoodi, mikä vastaanottaa toisesta koodista toimintoja
namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;
        public DialogueHolderTwo DHT;

        [Header("Text Options")]
        [SerializeField] public string input;
        /*[SerializeField] private Color textColor;
        [SerializeField] private Font textFont;*/

        [Header("Time parameters")]
        [SerializeField] public float delay;
        [SerializeField] public float delayBetweenLines;

        private IEnumerator lineAppear;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }

                else
                {
                    finished = true;
                }
            }
        }

        //Etsii pelistä tekstin ja syöttää sen aloittaessa
        public void OnEnable()
        {
            ResetLine();
            lineAppear = WriteText(input, textHolder, delay, delayBetweenLines/*, textColor, textFont*/);
            StartCoroutine(lineAppear);
        }

        public void ResetLine()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";
            finished = false;
        }
    }
}