using Assets.Modules;
using PixelCrushers.DialogueSystem;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;



namespace Assets.DialogSystem.Scripts
{
    public class DialogStarter : ISequenceAction
    {
        private readonly string title;
        private readonly string actor;
        private readonly DialogAgent dialogAgent;
        private ICallBack sequenceManagerCallBack;
        public DialogStarter(string title, string actor, DialogAgent dialogAgent)
        {
            this.title = title;
            this.actor = actor;
            this.dialogAgent = dialogAgent;
        }
        async Task ISequenceAction.Execute(ICallBack callBack)
        {
            sequenceManagerCallBack = callBack;
            await dialogAgent.LaunchDialog(title, actor);
            sequenceManagerCallBack.Return(null);
        }

        public class Factory : PlaceholderFactory<string, string, DialogStarter>
        {
        }
    }
}
