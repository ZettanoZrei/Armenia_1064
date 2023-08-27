using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    class DialogBackgroundManager 
    {       
        private DialogBackgroundView frontView;
        private DialogBackgroundView backView;

        public DialogBackgroundManager(IEnumerable<DialogBackgroundView> views)
        {
            frontView = views.First(x => x.dialogBackgroundViewType == DialogBackgroundView.DialogBackgroundViewType.Front);
            backView = views.First(x => x.dialogBackgroundViewType == DialogBackgroundView.DialogBackgroundViewType.Back);
        }
       
        public void SetBackSprites(Sprite front, Sprite back)
        {
            this.frontView.SetBackground(front);
            this.backView.SetBackground(back);
        }

    }
}
