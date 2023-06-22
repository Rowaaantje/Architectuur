using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject Journal;
    private bool journalActive = false;
    private void Start()
    {
        SetJournalActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleJournalAndPages();
        }
    }
    private void ToggleJournalAndPages()
    {
        journalActive = !journalActive;
        SetJournalActive(journalActive);

    }
    private void SetJournalActive(bool isActive)
    {
        Journal.SetActive(isActive);
    }





}
