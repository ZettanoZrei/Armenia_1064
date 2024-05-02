using Assets.DialogSystem.Scripts.UI;
using Model.Types;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using System.Linq;

namespace Assets.DialogSystem.Scripts
{
    public class DialogConclusionAgent //todo message about ending
    {
        private readonly ConclusionDialogueUI conclusionDialogueUI;
        public DialogConclusionAgent(ConclusionDialogueUI conclusionDialogueUI)
        {
            this.conclusionDialogueUI = conclusionDialogueUI;
        }


        public void LaunchConclusion(DialogConclusion dialogConclusion) //todo доделать
        {           
            DialogueManager.conversationView.dialogueUI = conclusionDialogueUI;
            conclusionDialogueUI.SetParameters(new Dictionary<ParameterType, int> 
            {
                { ParameterType.Spirit, dialogConclusion.blessing },
                { ParameterType.Food, dialogConclusion.food },
                { ParameterType.Stamina, dialogConclusion.stamina },
                { ParameterType.People, dialogConclusion.people }
            });
            conclusionDialogueUI.SetRelations(dialogConclusion.persons.ToDictionary(x => x.actor, y => y.relations.Sum()));
        }
    }
}
