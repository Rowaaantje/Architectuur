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
        if(player_detection && Input.GetKeyDown(KeyCode.F) && !player1.dialogue)
        {
            canva.SetActive(true);
            player1.dialogue = true;
            NewDialogue("Iedereen goed opgelet");
            NewDialogue("Want dit is je grote kans.");
            NewDialogue("Luister goed naar wat ik zeg");
            NewDialogue("Want dit is de KabouterDans");
            canva.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void NewDialogue(string text)
    {
        GameObject template_clone = Instantiate(d_template, d_template.transform);
        template_clone.transform.parent = canva.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
        template_clone.tag = "clone";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "PlayerBody")
        {
            player_detection = true;
        }
    }

    private void OnTriggerExit(Collider other)
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
