using Assets.Game.HappeningSystem;
using Assets.Modules;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assets.DialogSystem.Scripts
{
    //управляет последовательностью дествий после завершения диалога
    internal class DialogSequenceManager : ICallBack
    {
        private readonly Queue<IDialogSequenceAction> actions = new Queue<IDialogSequenceAction>();

        public void StartDialog(string title, Actor actor)
        {
            this.title = title;
            switch (actor.Name)
            {
                case "Conversation":
                    currentActor = conversationActor;
                    break;
                case "Story":
                    currentActor = storyActor;
                    break;
                default: throw new Exception($"Unknown actor: {actor.Name}");
            }
        }

        public void Execute()
        {
            (this as ICallBack).Return(null);
        }

        void ICallBack.Return(object _)
        {
            if (!actions.Any()) return;
            var action = actions.Dequeue();
            action.Execute(this);
        }
    }

    public interface IDialogSequenceAction
    {
        void Execute(ICallBack callBack);
    }

    class DialogStarter : IDialogSequenceAction
    {
        public DialogStarter(string title, Actor actor) 
        {

        }
        void IDialogSequenceAction.Execute(ICallBack callBack)
        {
            throw new NotImplementedException();
        }
    }
}
