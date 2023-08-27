using System;

namespace Assets.Game.Configurations
{
    [Serializable]
    public class PopupConfig
    {
        public FlyinngPopup flyinngPopup;
        public ReactionPopupConfig reactionPopupConfig;
        public BlackPanelConfig blackPanelConfig;

        [Serializable]
        public class FlyinngPopup
        {
            public float upLimit;
            public float speed;
            public float fadeSpeed;
        }

        [Serializable]
        public class ReactionPopupConfig
        {
            public float stayTime;
            public float appearSpeed;
        }

        [Serializable]
        public class BlackPanelConfig
        {
            public float darkUpSpeed;
            public float darkDownSpeed;
            public float delayAfterDark;
        }
    }
}
