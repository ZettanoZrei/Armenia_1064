using UnityEngine;
using Model.Entities.Persons;

namespace Assets.Game.HappeningSystem
{
    public class DialogMainFigurePlace : DialogBaseFigurePlace
    {
        public DialogBaseFigurePlace SecondLinePlace { get; set; }

        public PersonName PersonName { get; private set; } = new PersonName("");

        public void SetPerson(PersonFigureEntity personFigureEntity)
        {
            var positionComponent = personFigureEntity.Element<PersonModelPositionComponent>();
            var infoComponent = personFigureEntity.Element<PersonModelInfoComponent>();

            //if (PositionType == Model.Types.PositionType.Front)
            if (infoComponent.PositionType == Model.Types.PositionType.Front)
            {
                positionComponent.SetFrontViewPosition(transform.position);
                positionComponent.SetBackViewPosition(SecondLinePlace.transform.position);
            }
            else
            {
                positionComponent.SetFrontViewPosition(SecondLinePlace.transform.position);
                positionComponent.SetBackViewPosition(transform.position);
            }

            this.PersonName = infoComponent.Name;
        }

        public void Clean() 
        {
            PersonName = new PersonName(""); 
        }
    }
}
