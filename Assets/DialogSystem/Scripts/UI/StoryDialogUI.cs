using PixelCrushers.DialogueSystem.Wrappers;
using UnityEngine;

namespace Assets.DialogSystem.Scripts.UI
{
    public class StoryDialogUI : StandardDialogueUI
    {
        [SerializeField] private AdvicePopupView advicePopupView;
        private ActorEventObserver actorEventObserver;
        public override void Awake()
        {
            base.Awake();
            actorEventObserver = FindObjectOfType<ActorEventObserver>();
        }
        public override void OnEnable()
        {
            base.OnEnable();
            actorEventObserver.ShowResponseMenuEvent += ShowAdvicePanel;
            actorEventObserver.ShowSubtitleEvent += TryHideAdvicePanel;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            actorEventObserver.ShowResponseMenuEvent -= ShowAdvicePanel;
            actorEventObserver.ShowSubtitleEvent -= TryHideAdvicePanel;
        }

        private void TryHideAdvicePanel(PixelCrushers.DialogueSystem.Subtitle obj)
        {
            if(advicePopupView.gameObject.activeSelf)
                advicePopupView.HideAdvicePanel();
        }

        private void ShowAdvicePanel(PixelCrushers.DialogueSystem.Response[] obj)
        {
            advicePopupView.ActiveAdvicePanel();
        }
    }
    
}
