using Model.Entities.Happenings;

namespace Assets.Game.HappeningSystem
{
    public class ManagerTypeResolver
    {
        public PopupType HappenType { get; private set; }

        public void DefineHappenType(Happening happening)
        {
            if (happening is DialogHappening)
            {
                HappenType = PopupType.DialogPopup;
            }
            else if (happening is StoryHappening)
            {
                HappenType = PopupType.StoryPopup;
            }
            else if (happening is Happening)
            {
                HappenType = PopupType.AccidentPopup;
            }
        }
    }
}
