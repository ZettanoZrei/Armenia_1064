using Assets.Game.HappeningSystem;
using Assets.Modules;
using Model.Entities.Answers;

namespace Assets.Game.Message
{
    internal class MessageAdapter
    {
        private readonly MessageView view;
        private readonly Consequences consequences;
        private readonly ICallBack managerCallBack;
        public MessageAdapter(IPopup view, Consequences consequences, ICallBack callBack)
        {
            this.view = (MessageView)view;
            this.consequences = consequences;
            this.managerCallBack = callBack;

            this.view.OnClose += CallBackAboutClosePopup;
        }

        private void CallBackAboutClosePopup()
        {
            managerCallBack.Return(null);
        }

        public void Perform()
        {
            SetMessageView();
            SetParamsView();
            SetPersonRealationView();
        }

        private void SetPersonRealationView()
        {
            foreach (var person in consequences.PersonConsequences)
            {
                view.SetPersons(person.PersonName.Name, person.Value);
            }            
        }

        private void SetParamsView()
        {
            foreach (var param in consequences.ParamConsequences)
            {
                SetParameter(param);
            }
        }

        private void SetMessageView()
        {
            if (consequences.IsMessage)
            {
                var messages = consequences.Message.Split('|');
                foreach (var message in messages)
                    view.SetMessage(message);
            }
        }

        private void SetParameter(SingleParamConsequences param)
        {
            switch (param.ParameterType)
            {
                case Model.Types.ParameterType.Food:
                    view.SetFood(param.Value);
                    break;
                case Model.Types.ParameterType.Spirit:
                    view.SetSpirit(param.Value);
                    break;
                case Model.Types.ParameterType.Stamina:
                    view.SetStamina(param.Value);
                    break;
                case Model.Types.ParameterType.People:
                    view.SetPeople(param.Value);
                    break;
            }
        }
    }
}
