using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject Journal;
    public GameObject Pages;
    private bool journalActive = false;
    private bool PagesActive = false;
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

        PagesActive = !PagesActive;
        SetJournalActive(PagesActive);
    }

    private void SetJournalActive(bool isActive)
    {
        Journal.SetActive(isActive);
        Pages.SetActive(isActive);
    }
}
