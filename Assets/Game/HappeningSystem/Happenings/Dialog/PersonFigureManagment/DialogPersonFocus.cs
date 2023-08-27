using Model.Entities.Persons;
using Model.Entities.Phrases;
using Model.Types;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.HappeningSystem
{
    public class DialogPersonFocus : MonoBehaviour
    {
        private FigurePlaceFactory figurePlaceFactory;
        private DialogPersonKeeper dialogPersonKeeper;

        public event Action<LineView> OnSetLineView;

        [Inject]
        public void Construct(FigurePlaceFactory figurePlaceFactory, DialogPersonKeeper dialogPersonKeeper)
        {
            this.figurePlaceFactory = figurePlaceFactory;
            this.dialogPersonKeeper = dialogPersonKeeper;
        }

        public void FocusInvert(PersonName personName, bool IsShowFace) //if isShowCamera == false, flip the camera
        {
            var focusPlace = GetFocusPlaceInvert(personName, IsShowFace);
            foreach (var place in figurePlaceFactory)
            {
                if (focusPlace == place)
                    place.FocusCamera();
                else
                    place.UnfocusCamera();
            }
        }
        public void Focus(PersonName personName, bool IsShowFace, LineView currentLineView)
        {
            var focusPlace = GetFocusPlace(personName, IsShowFace, currentLineView);
            foreach (var place in figurePlaceFactory)
            {
                if (focusPlace == place)
                    place.FocusCamera();
                else
                    place.UnfocusCamera();
            }
        }
        private DialogBaseFigurePlace GetFocusPlaceInvert(PersonName personName, bool IsShowFace)
        {
            var positionType = dialogPersonKeeper.GetPerson(personName).Element<PersonModelInfoComponent>().PositionType;
            var frontFocusPlace = figurePlaceFactory.GetLinePlaces(personName);

            if (positionType == PositionType.Front)
            {
                OnSetLineView?.Invoke(LineView.FirstLine);
                return frontFocusPlace;
            }
            else
            {
                if(IsShowFace)
                {
                    OnSetLineView?.Invoke(LineView.SecondLine);
                    return frontFocusPlace.SecondLinePlace;
                }
                else
                {
                    OnSetLineView?.Invoke(LineView.FirstLine);
                    return frontFocusPlace;
                }               
            }

           
        }
        private DialogBaseFigurePlace GetFocusPlace(PersonName personName, bool IsShowFace, LineView currentLineView)
        {
            var positionType = dialogPersonKeeper.GetPerson(personName).Element<PersonModelInfoComponent>().PositionType;
            var frontFocusPlace = figurePlaceFactory.GetLinePlaces(personName);
            DialogBaseFigurePlace focusPlace = null;
            if (currentLineView == LineView.FirstLine)
            {
                if (IsShowFace)
                {
                    focusPlace = DefineIfShowFace(positionType, frontFocusPlace);
                }
                else
                {
                    focusPlace = frontFocusPlace;
                    OnSetLineView?.Invoke(LineView.FirstLine);
                }
            }
            else if (currentLineView == LineView.SecondLine)
            {
                if (IsShowFace)
                {
                    focusPlace = DefineIfShowFace(positionType, frontFocusPlace);
                }
                else
                {
                    focusPlace = frontFocusPlace.SecondLinePlace;
                    OnSetLineView?.Invoke(LineView.SecondLine);
                }
            }

            return focusPlace;
        }
        private DialogBaseFigurePlace DefineIfShowFace(PositionType positionType, DialogMainFigurePlace firstLineFocusPlace)
        {
            DialogBaseFigurePlace focusPlace = null;
            if (positionType == PositionType.Front)
            {
                focusPlace = firstLineFocusPlace;
                OnSetLineView?.Invoke(LineView.FirstLine);
            }
            else if (positionType == PositionType.Back)
            {
                focusPlace = firstLineFocusPlace.SecondLinePlace;
                OnSetLineView?.Invoke(LineView.SecondLine);
            }

            return focusPlace;
        }
    }
}
