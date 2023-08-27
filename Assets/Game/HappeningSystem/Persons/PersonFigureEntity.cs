using Entities;

namespace Assets.Game.HappeningSystem
{
    public class PersonFigureEntity : MonoEntity
    {
        public override bool Equals(object other)
        {
            if(other is PersonFigureEntity personModel)
            {
                var thisInfoComponent = this.Element<PersonModelInfoComponent>();
                var personModelInfoComponent = personModel.Element<PersonModelInfoComponent>();
                return thisInfoComponent.Name == personModelInfoComponent.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 0; ///?????
        }
    }
}
