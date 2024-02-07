using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlignUpgrade : MonoBehaviour
{ 
    public AlignMutagen alignMutagen;
    public SelectionType selectionType;

    private bool isActive;

    public void Update()
    {
        // Set button to be interactable
        GetComponent<ActionButton>().UIButton.interactable = isActive;
        GetComponent<ActionButton>().SetButtonOpacity(isActive ? 1.0f : 0.3f);

        if (alignMutagen.selectionType != selectionType)
        {
            isActive = true;
        }
    }

    public void AssignSelectionType()
    {
        alignMutagen.selectionType = selectionType;
        isActive = false;
    }
}
