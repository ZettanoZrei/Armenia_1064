using Assets.Game.Configurations;
using Model.Types;
using System;
using Zenject;

namespace Assets.Game.Parameters.EndedParamSystem
{
    public class EndedParamRemovingPeopleHandler: IInitializable, ILateDisposable
    {
        private readonly EndedParamMechanics endedParamMechanics;
        private readonly ParametersManager parametersManager;

        public EndedParamRemovingPeopleHandler(EndedParamMechanics endedParamMechanics, ParametersManager parametersManager)
        {
            this.endedParamMechanics = endedParamMechanics;
            this.parametersManager = parametersManager;
        }

        void IInitializable.Initialize()
        {
            endedParamMechanics.OnRemovingPeople += HandlerRemovePeople;
        }
      
        void ILateDisposable.LateDispose()
        {
            endedParamMechanics.OnRemovingPeople -= HandlerRemovePeople;
        }

        private void HandlerRemovePeople(int arg2)
        {
            parametersManager.ChangePeople(arg2);
        }
    }
}
