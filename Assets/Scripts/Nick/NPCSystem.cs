using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCSystem : MonoBehaviour
{

    public GameObject d_template;
    public GameObject canva;
    bool player_detection = false;

    void Update()
    {
        if(player_detection && Input.GetKeyDown(KeyCode.F) && !player1.dialogue) // If F is pressed, Play Dialogue
        {
            canva.SetActive(true);
            player1.dialogue = true;
            CreateDialogue("Iedereen goed opgelet");
            CreateDialogue("Want dit is je grote kans.");
            CreateDialogue("Luister goed naar wat ik zeg");
            CreateDialogue("Want dit is de KabouterDans");
            canva.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void CreateDialogue(string text) // Clones the dialogue template and puts on a Text and Tag
    {
        GameObject template_clone = Instantiate(d_template, d_template.transform);
        template_clone.transform.parent = canva.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
        template_clone.tag = "clone";
    }

    private void OnTriggerEnter(Collider other) // If enters Trigger Area, Player will get detected
    {
        if(other.name == "PlayerBody")
        {
            player_detection = true;
        }
    }

    private void OnTriggerExit(Collider other) // If exits Trigger Area, Player will not be detected anymore and the clones will get destroyed 
    {
        player_detection = false;
        //Destroy Clones
        var clones = GameObject.FindGameObjectsWithTag("clone");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
        canva.SetActive(false);
    }
    
}
