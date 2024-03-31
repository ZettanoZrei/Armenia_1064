using Assets.Game.HappeningSystem;
using Assets.Game.Parameters;
using Entities;
using Model.Entities.Answers;
using Model.Entities.Persons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DebugHappening : MonoBehaviour
{
    [SerializeField]
    private string happeningName;

    [Inject]
    SetupCampManager setupCamp;

    [Inject] ParametersManager parametersManager;

    public TextAsset file;



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) 
        {
            setupCamp.LeaveCamp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            setupCamp.SetupCamp();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            parametersManager.ChangeStamina(-20);
        }
    }
}
