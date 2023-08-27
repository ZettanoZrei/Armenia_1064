using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game
{
    public class DialogBackgroundKeeper
    {
        private BackgroundState state;
        public string DialogFront => BackgroundState.Travel == state ? dialogFrontTravel : dialogFrontCamp;
        public string DialogBack => BackgroundState.Travel == state ? dialogBackTravel : dialogBackCamp;

        public BackgroundState State => state; 

        private string dialogFrontTravel;
        private string dialogBackTravel;
        private string dialogFrontCamp;
        private string dialogBackCamp;

        public BackgroundPack BackgroundPack { get; private set; }
        public void SetDialogBackground(BackgroundPack backgroundPack)
        {
            dialogFrontTravel = !string.IsNullOrEmpty(backgroundPack.dialogFrontTravel) ? backgroundPack.dialogFrontTravel : dialogFrontTravel;
            dialogBackTravel = !string.IsNullOrEmpty(backgroundPack.dialogBackTravel) ? backgroundPack.dialogBackTravel : dialogBackTravel;
            dialogFrontCamp = !string.IsNullOrEmpty(backgroundPack.dialogFrontCamp) ? backgroundPack.dialogFrontCamp : dialogFrontCamp;
            dialogBackCamp = !string.IsNullOrEmpty(backgroundPack.dialogBackCamp) ? backgroundPack.dialogBackCamp : dialogBackCamp;

            BackgroundPack = backgroundPack;    
        }

        public void ChangeState(BackgroundState state)
        {
            this.state = state;
        }

        public enum BackgroundState
        {
            Travel,
            Camp
        }
    }
}
