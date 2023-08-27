class SettingsCommand : IMenuCommand
{
    private readonly MySceneManager sceneManager;

    public SettingsCommand(MySceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
    }

    void IMenuCommand.Execute()
    {
        sceneManager.LoadSettings();
    }
}
