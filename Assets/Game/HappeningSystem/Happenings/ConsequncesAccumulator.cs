using Model.Entities.Answers;
using System.Linq;

namespace Assets.Game.HappeningSystem
{
    public class ConsequncesAccumulator
    {
        private readonly Consequences consequences;
        public Consequences Consequences => consequences;
        public bool IsNotEmpty => consequences.IsHappenCons || consequences.IsQuestCons || consequences.IsPersonCons 
            || consequences.IsParamCons || consequences.IsMessage;
        public ConsequncesAccumulator()
        {
            this.consequences = new Consequences();
        }

        public void AddMessage(string message)
        {
            consequences.Message += $"{message} |";

            if(!consequences.IsMessage)
                consequences.IsMessage = true;
        }


        public void AddHappenConsequences(SingleHappeningConsequences happenConsequences)
        {
            consequences.HappenConsequences.Add(happenConsequences);

            if (!consequences.IsHappenCons)
                consequences.IsHappenCons = true;
        }
        public void AddParamConsequences(SingleParamConsequences paramConsequences)
        {
            if (consequences.ParamConsequences.Any(x => x.ParameterType == paramConsequences.ParameterType))
            {
                var param = consequences.ParamConsequences.First(x => x.ParameterType == paramConsequences.ParameterType);
                param.Value += paramConsequences.Value;
            }
            else
            {
                consequences.ParamConsequences.Add(paramConsequences);
            }

            if (!consequences.IsParamCons)
                consequences.IsParamCons = true;
        }

        public void AddPersonConsequences(SinglePersonConsequences personConsequences)
        {
            if (consequences.PersonConsequences.Any(x => x.PersonName.Equals(personConsequences.PersonName)))
            {
                var person = consequences.PersonConsequences.First(x => x.PersonName.Equals(personConsequences.PersonName));
                person.Value += personConsequences.Value;
            }
            else
            {
                consequences.PersonConsequences.Add(personConsequences);
            }

            if (!consequences.IsPersonCons) 
                consequences.IsPersonCons = true;
        }

        public void AddQuestConsequences(SingleQuestConsequences questConsequences)
        {
            if (consequences.QuestConsequences.Any(x => x.Quest == questConsequences.Quest))
            {
                var quest = consequences.QuestConsequences.First(x => x.Quest == questConsequences.Quest);
                quest.Happening = questConsequences.Happening;
            }
            else
            {
                consequences.QuestConsequences.Add(questConsequences);
            }

            if (!consequences.IsQuestCons)
                consequences.IsQuestCons = true;
        }
    }
}
