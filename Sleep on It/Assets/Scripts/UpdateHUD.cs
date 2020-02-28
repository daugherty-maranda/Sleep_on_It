using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateHUD : MonoBehaviour
{
    TextMeshProUGUI livesDisplay;
    TextMeshProUGUI scoreDisplay;

    private void Awake()
    {
        TextMeshProUGUI[] textMesh = GetComponentsInChildren<TextMeshProUGUI>();
        livesDisplay = textMesh[0];
        scoreDisplay = textMesh[1];
    }

    public void update(string type, int toSet)
    {
        if (type == "lives")
        {
            livesDisplay.SetText(toSet.ToString());
        }
        else if (type == "score")
        {
            scoreDisplay.SetText(toSet.ToString());
        }
    }
}
