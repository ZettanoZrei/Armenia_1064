using PixelCrushers.DialogueSystem;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;



namespace Assets.DialogSystem.Scripts
{
    public class DialogAgent
    {
        private readonly Transform conversationActor;
        private readonly Transform storyActor;
        private readonly ActorEventObserver actorEventObserver;

        private bool isDialogFinish;
        public DialogAgent(ConversaionActor conversationActor, StoryActor storyActor, ActorEventObserver actorEventObserver) 
        {
            this.conversationActor = conversationActor.transform;
            this.storyActor = storyActor.transform;
            this.actorEventObserver = actorEventObserver;
        }


        public async Task LaunchDialog(string title, string actor)
        {
            actorEventObserver.DialogEndEvent += () => isDialogFinish = true;
            await Task.Delay(TimeSpan.FromSeconds(0.1));//todo if without delay then ui will not be showed
            switch (actor)
            {
                case "Conversation":
                    DialogueManager.StartConversation(title, conversationActor);
                    break;
                case "Story":
                    DialogueManager.StartConversation(title, storyActor);
                    break;
                default: throw new Exception($"Unknown actor: {actor}");
            }

            await CheckDialogFinish();
        }

        private async Task CheckDialogFinish()
        {
            while (!isDialogFinish)
            {
                await Task.Yield();
            }
            await Task.CompletedTask;
        }
    }
}
