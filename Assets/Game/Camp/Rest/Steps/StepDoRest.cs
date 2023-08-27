using Assets.Game.Message;
using Assets.Modules;
using Model.Entities.Answers;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Game.Camp
{
    //2
    public class StepDoRest : IRestStep
    {
        private readonly RestModel restModel;
        private readonly RestContext restContext;

        public StepDoRest(RestModel restModel, RestContext restContext)
        {
            this.restModel = restModel;
            this.restContext = restContext;
        }

        void IRestStep.Execute(ICallBack callBack)
        {
            var consequnces = restModel.Rest(restContext.ChosenParam);
            restContext.RestConsequences = consequnces;
            callBack.Return(true);
        }
    }
}
