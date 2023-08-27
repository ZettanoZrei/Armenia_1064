using System.Collections.Generic;
using Zenject;

class TravelSceneNavigatorController : IInitializable
{
    private readonly IEnumerable<NextTravelSceneTrigger> sceneTriggers;
    private readonly TravelSceneNavigator travelSceneNavigator;

    public TravelSceneNavigatorController(IEnumerable<NextTravelSceneTrigger> sceneTriggers, TravelSceneNavigator travelSceneNavigator)
    {
        this.sceneTriggers = sceneTriggers;
        this.travelSceneNavigator = travelSceneNavigator;
    }

    void IInitializable.Initialize()
    {
        travelSceneNavigator.SetTriggers(sceneTriggers);
    }
}
