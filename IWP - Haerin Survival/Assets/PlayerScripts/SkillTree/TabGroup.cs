using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButtons> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButtons SelectedTab;
    public List<GameObject> Objectstoswap;

    public void Subscribe(TabButtons button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButtons>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButtons button)
    {
        ResetTabs();
        if (SelectedTab == null || button != SelectedTab)
        {
           button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButtons button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButtons button)
    {
        SelectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < Objectstoswap.Count; i++)
        {
            if (i == index)
            {
                Objectstoswap[i].SetActive(true);
            }
            else
            {
                Objectstoswap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabButtons button in tabButtons)
        {
            if (SelectedTab != null && button == SelectedTab)
            {
                continue;
            }
            button.background.sprite = tabIdle;
        }

    }

}
