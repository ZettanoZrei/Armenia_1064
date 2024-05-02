using Assets.DialogSystem.Scripts.UI.ResponseUI;
using Assets.Game.HappeningSystem.Persons;
using PixelCrushers.DialogueSystem.ChatMapper;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Game.HappeningSystem;
using System;

public class MyUIMenuPanel : StandardUIMenuPanel
{
    protected override void SetResponseButton(StandardUIResponseButton button, Response response, Transform target, int buttonNumber)
    {
        base.SetResponseButton(button, response, target, buttonNumber);

        var database = Resources.Load<DialogueDatabase>("Dialogue Database"); //todo find way to get normal
        var personInfoCatalog = Resources.Load<DialogPersonPackCatalog>("Entities/DialogPersonCatalog"); //todo find way to get normal
        if (button is IMyResponseUI myResponse)
        {
            var conversation = database.actors.First(x => x.id == response.destinationEntry.ConversantID);
            if (!RelationManager.Instance.TryGetRelation(conversation.Name, out int relation))
                return;
            
            if(button is AdviceUIButton adviceUI) 
            {                
                var info = personInfoCatalog.GetPack(conversation.Name);
                adviceUI.SetRelation(relation);
                adviceUI.SetPortrait(info.Portret);
                var enabled = RelationManager.Instance.RelationNeededForAdvice < relation;
                myResponse.SetState(enabled);
            }
            else if(button is ResponseUIButton responseUI){
                var relationField = response.destinationEntry.fields.FirstOrDefault(x=>x.title=="Relations");
                if (relationField == null)
                    return;

                var enabled = Int32.Parse(relationField.value) < relation;
                myResponse.SetState(enabled);
            }
        }
    }
}
