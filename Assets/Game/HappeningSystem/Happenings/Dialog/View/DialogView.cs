using Assets.Game;
using Assets.Game.HappeningSystem;
using Assets.Game.HappeningSystem.View.Advice;
using Assets.Game.HappeningSystem.View.Common;
using Entities;
using GameSystems;
using Model.Entities.Answers;
using Model.Entities.Happenings;
using Model.Entities.Nodes;
using Model.Entities.Persons;
using Model.Entities.Phrases;
using Model.Types;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

//Есть две линии, каждой позиции из первой линии соответсвует позиция из второй с обратным типо позиции. Ища 
//подходящую для персонажа позциию мы ориентируемся на тип позиции из первой линии. Затeм мы кидаем по парнo зад
//и перед персонажа на эти парные позиции
class DialogView : Popup, IInitializable
{
    [SerializeField] private OptionComponent optionView;
    [SerializeField] private TextScrollView textScrollView;
    [SerializeField] private DialogPortretsView portretsView;
    [SerializeField] private AdvicePopupView adviceView;
    [SerializeField] private ButtonNavigationComponent buttonNavigation;
    private FigurePersonManager figurePersonManager;
    [Inject] private DialogBackgroundManager backgroundManager;



    public event Action<int> OnDecitionMade
    {
        add { optionView.OnDecitionMade += value; }
        remove { optionView.OnDecitionMade -= value; }
    }

    public event Action<PersonName> OnClickPortrait
    {
        add { adviceView.OnClickPortrait += value; }
        remove { adviceView.OnClickPortrait -= value; }
    }

    public event Action OnClose
    {
        add { buttonNavigation.OnClose += value; }
        remove { buttonNavigation.OnClose -= value; }
    }
    public event Action OnNextPhrase
    {
        add { buttonNavigation.OnNextPhrase += value; }
        remove { buttonNavigation.OnNextPhrase -= value; }
    }


    private LineView currentLineView;
    private Person adviserPositon;

    [Inject]
    public void Construct(FigurePersonManager figurePersonManager, PortaitHeap portaitHeap)
    {
        this.figurePersonManager = figurePersonManager;
        adviceView.InitPortraitHeap(portaitHeap);
    }
    void IInitializable.Initialize()
    {
        figurePersonManager.OnSetLineView += (line) => currentLineView = line;
    }

    public void FocusCameraForStart(PersonName personName, bool isShowFace)
    {
        figurePersonManager.FocusInvert(personName, isShowFace);
    }
    public void UpdateText(Phrase phrase)
    {
        textScrollView.UpdateView(phrase);
    }
    public void UpdateOptions(List<Answer> answers)
    {
        optionView.UpdateOptions(answers);
    }

    public void ShowAdvicePopup(List<PortraitButton> portraits)
    {
        adviceView.ShowAdvicePanel(portraits);
    }

    public void PlayAdvice(AdvicePhrase advice)
    {
        CreatePersonFigure(new Person
        {
            Name = advice.PersonName,
            PositionType = adviserPositon.PositionType,
            XPosition = adviserPositon.XPosition
        });
        FocusPerson(advice.PersonName, true);
        textScrollView.UpdateView(advice);
        adviceView.HideAdvicePanel();
    }
    public void ShowCloseButton(bool value)
    {
        buttonNavigation.SetActiveCloseButton(value);
    }

    public void ShowNextTextButton(bool value)
    {
        buttonNavigation.SetActiveNextButton(value);
    }

    public void InitAdviserPosition(Person advisor)
    {
        adviserPositon = advisor;
    }
    public void InitBackground(Sprite front, Sprite back)
    {
        if (front == null || back == null)
            return;

        backgroundManager.SetBackSprites(front, back);
    }
    public void InitPersonModels(IEnumerable<Person> persons)
    {
        this.figurePersonManager.CreatePersonFigures(persons);
    }

    public void SetPortret(PersonName personName, int relationValue = 0)
    {
        portretsView.SetPersonPortret(personName, relationValue);
    }

    public void SetHeroPortret(bool leaveAnotherPortreit)
    {
        portretsView.SetHeroPortret(leaveAnotherPortreit);
    }

    public void FocusPerson(PersonName personName, bool IsShowFace)
    {
        this.figurePersonManager.Focus(personName, IsShowFace, currentLineView);
    }

    public void CreatePersonFigure(Person person)
    {
        this.figurePersonManager.CreatePersonFigure(person);
    }

    public void Finish()
    {
        this.figurePersonManager.Clean();
        this.textScrollView.Finish();
    }

    

    public void CloseAdvicePopup()
    {
        adviceView.HideAdvicePanel();
    }
}
