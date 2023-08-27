using Model.Entities.Answers;
using Model.Types;


namespace Assets.Game.Camp
{
    public class NightWorkOptionInfo
    {
        public ParameterType ParameterTypePlus { get; set; }
        public int PlusValue { get; set; }
        public ParameterType ParameterTypeMinus { get; set; }
        public int MinusValue { get; set; }
        public Answer Answer { get; set; }
    }
}
