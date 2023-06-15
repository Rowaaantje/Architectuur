using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    int index = 2;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket) && transform.childCount > 1) // If ] is pressed, u will go to next dialogue
        {
            if (player1.dialogue)
            {
                transform.GetChild(index).gameObject.SetActive(true);
                index += 1;
                if (transform.childCount == index)
                {
                    index = 2;
                    player1.dialogue = false;
                }
            }

            else
            {
                //Destroy Clones If done with Dialogues
                var clones = GameObject.FindGameObjectsWithTag("clone");
                foreach (var clone in clones)
                {
                    Destroy(clone);
                }
                gameObject.SetActive(false);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftBracket) && transform.childCount > 1) // If [ is pressed, u will go to previous dialogue
        {
            if (player1.dialogue)
            {
                index += -1;
                transform.GetChild(index).gameObject.SetActive(false);
                if (transform.childCount == index)
                {
                    index = 2;
                    player1.dialogue = false;
                }
            }
        }
    }
}
