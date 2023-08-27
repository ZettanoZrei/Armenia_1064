using Assets.Game.Configurations;
using Loader;
using System.Collections;
using System.Collections.Generic;

class NewGameCommand : IMenuCommand
{
    private readonly IEnumerable<ING_Task> tasks;

    public NewGameCommand(IEnumerable<ING_Task> tasks)
    {
        this.tasks = tasks;
    }

    void IMenuCommand.Execute()
    {
        foreach (var task in tasks) 
        {
            task.Execute();
        }
    }
}
