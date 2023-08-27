namespace Assets.Game.Camp
{
    class NightWorkAdapter
    {
        private NightWorkModel model;
        public void Init(NightWorkView view, NightWorkModel model)
        {
            this.model = model;

            view.SetOption(model.GetNightWorkOptions());
            view.OnMadeDicition += PerformExtraWork;
        }
        private void PerformExtraWork(int? type)
        {
            model.NightWorkChosen(type);
        }
    }
}
