using Assets.DialogSystem.Scripts.UI;
using PixelCrushers.DialogueSystem;



namespace Assets.DialogSystem.Scripts
{
    public class DialogConclusionAgent //todo message about ending
    {
        private readonly ConclusionDialogueUI conclusionDialogueUI;
        public DialogConclusionAgent(ConclusionDialogueUI conclusionDialogueUI)
        {
            this.conclusionDialogueUI = conclusionDialogueUI;
        }

        public void LaunchConclusion()
        {
            DialogueManager.conversationView.dialogueUI = conclusionDialogueUI;
        }

        public void LaunchConclusion(DialogConclusion dialogConclusion) //todo доделать
        {
            DialogueManager.conversationView.dialogueUI = conclusionDialogueUI;
        }
    }
}
