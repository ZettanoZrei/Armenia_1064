using Assets.Game.Camp;
using Assets.Game.HappeningSystem;
using Model.Entities.Answers;
using System;
using System.Linq;

class ConsequencesHandler
{
    public event Action<SingleQuestConsequences> OnQuestsConsequences;
    public event Action<SinglePersonConsequences> OnRelationConsequences;
    public event Action<SingleParamConsequences> OnParameterConsequences;
    public event Action<SingleHappeningConsequences> OnHappeningConsequences;

    private ConsequncesAccumulator consequncesAccumulator;

    //public Consequences Consequences => consequncesAccumulator.IsNotEmpty ? consequncesAccumulator.Consequences : null;

    public Consequences GetConsequences()
    {
        if(consequncesAccumulator.IsNotEmpty)
        {
            consequncesAccumulator.Consequences.PersonConsequences = consequncesAccumulator.Consequences.PersonConsequences.Where(x => x.Value != 0).ToList();
            return consequncesAccumulator.Consequences;
        }
        else
        {
            return null;
        }
    }
    public void Clear()
    {
        this.consequncesAccumulator = new ConsequncesAccumulator();
    }

    public void AccumulateConsequences(Answer answer)
    {
        if (answer.Consequences.IsPersonCons) //reaction has to be immediately
        {
            answer.Consequences.PersonConsequences.ForEach(x => consequncesAccumulator.AddPersonConsequences(x));
            answer.Consequences.PersonConsequences.ForEach(x => OnRelationConsequences?.Invoke(x));
        }

        if (answer.Consequences.IsQuestCons)
            answer.Consequences.QuestConsequences.ForEach(x => consequncesAccumulator.AddQuestConsequences(x));

        if (answer.Consequences.IsParamCons)
            answer.Consequences.ParamConsequences.ForEach(x => consequncesAccumulator.AddParamConsequences(x));

        if (answer.Consequences.IsHappenCons)
            answer.Consequences.HappenConsequences.ForEach(x => consequncesAccumulator.AddHappenConsequences(x));

        if (answer.Consequences.IsMessage)
            consequncesAccumulator.AddMessage(answer.Consequences.Message);
    }

    public void InvokeConsequencesEvents()
    {
        //consequncesAccumulator.Consequences.PersonConsequences.ForEach(x => OnRelationConsequences?.Invoke(x));
        consequncesAccumulator.Consequences.QuestConsequences.ForEach(x => OnQuestsConsequences?.Invoke(x));
        consequncesAccumulator.Consequences.ParamConsequences.ForEach(x => OnParameterConsequences?.Invoke(x));
        consequncesAccumulator.Consequences.HappenConsequences.ForEach(x => OnHappeningConsequences?.Invoke(x));
    }    
}


