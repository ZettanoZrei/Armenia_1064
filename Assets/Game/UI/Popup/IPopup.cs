namespace Assets.Game
{
    interface IPopup
    {
        PopupType PopupType { get; }
        void Hide();
        void Activate();
    }
}
