using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class expCtrller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] public int level;
    public float CurrentExp;
    [SerializeField] private float TargetExp;
    [SerializeField] private Image expProgressBar;

    
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CurrentExp += 12;
        }

        expText.text = CurrentExp + " / " + TargetExp;

        ExpController();
    }



    public void ExpController()
    {
        lvlText.text = "Level: " + level.ToString();
        expProgressBar.fillAmount = CurrentExp / TargetExp;



        if (CurrentExp >= TargetExp)
        {
            CurrentExp = CurrentExp - TargetExp;
            level++;
            TargetExp += 50; 
        }
    }



}
