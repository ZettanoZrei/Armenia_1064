using System;
using System.Linq;
using UnityEngine;

namespace PixelCrushers.DialogueSystem.VisualNovelFramework
{

    public class HandleBackgroundFields : MonoBehaviour
    {
        
        private BackgroundManager m_backgroundManager = null;
        private BackgroundManager backgroundManager
        {
            get
            {
                if (m_backgroundManager == null) m_backgroundManager = FindObjectOfType<BackgroundManager>();
                return m_backgroundManager;
            }
        }
        private SelectorLocationManager m_locationManager = null;
        private SelectorLocationManager locationManager
        {
            get
            {
                if (m_locationManager == null) m_locationManager = FindObjectOfType<SelectorLocationManager>();
                return m_locationManager;
            }
        }

        private Subtitle m_lastSubtitle = null;
        private int m_last_position_index = -1;
        private DialogueActor m_lastDialogueActor = null;

        public void FixedUpdate()
        {
            if (m_lastDialogueActor == null)
                return;
            
            var position = m_lastDialogueActor.customScenePosition;
            if (position != m_last_position_index)
            {
                Swap(m_lastSubtitle, m_lastDialogueActor);
                
            }
            m_lastDialogueActor.UpdateAnimation(CustomDialoguePosition.GetFlippedState());
                
        }

        public void Swap(Subtitle subtitle, DialogueActor dialogueActor = null)
        {
            subtitle = subtitle == null ? m_lastSubtitle : subtitle;
            if (subtitle == null || backgroundManager == null) return;
            
            
            var locationIndex = Field.LookupValue(subtitle.dialogueEntry.fields, "Location");
            var subAdress = "";
            dialogueActor = dialogueActor == null
                ? DialogueActor.GetDialogueActorComponent(subtitle.speakerInfo.transform) : dialogueActor;
            
            if (dialogueActor != null)
            {
                m_lastDialogueActor = dialogueActor;
                var position = dialogueActor.customScenePosition;
                if (position == -1)
                    return;
                m_last_position_index = position;
                if (position >= 0)
                    subAdress = position % 2 == 0 ? "" : "_back";
                //else
                //    subAdress = subtitle.speakerInfo.isPlayer ? "" : "_back";

                if (subAdress == "")
                    CustomDialoguePosition.Flip(false);
                else 
                    CustomDialoguePosition.Flip(true);
                
                var fade = FindAnyObjectByType<DialogueFadeController>();
                if (fade != null)
                    fade.Play();
                
                
            }

            if (!string.IsNullOrEmpty(locationIndex))
            {
                var background = DialogueLua.GetLocationFieldByIndex(Convert.ToInt16(locationIndex), "Background").asString;
                BackgroundManager.SetBackgroundImage(background + subAdress);
            }
            else 
            {
                var index = DialogueLua.GetActorField(subtitle.speakerInfo.nameInDatabase,"CurrentLocation").asInt;
                if (index > 0)
                {
                    var background = DialogueLua.GetLocationFieldByIndex(index, "Background").asString;
                    BackgroundManager.SetBackgroundImage(background + subAdress);
                }
                else
                {
                    if (locationManager != null)
                    {

                        var background = DialogueLua.GetLocationField(locationManager.CurrentLocation, "Background").asString;
                        BackgroundManager.SetBackgroundImage(background +  subAdress);
                    }
                }
            }
        }
        
        public void OnConversationResponseMenu(Response[] responses)
        {
            var first = responses.FirstOrDefault();
            if (first == null)
                return;
            
            if (m_lastSubtitle == null)
                return;

            var dialogueActor = m_lastSubtitle.speakerInfo.id == first.destinationEntry.ActorID
                ? DialogueActor.GetDialogueActorComponent(m_lastSubtitle.speakerInfo.transform)
                : DialogueActor.GetDialogueActorComponent(m_lastSubtitle.listenerInfo.transform);

            Swap(m_lastSubtitle, dialogueActor);
        }
        
        private void OnConversationLine(Subtitle subtitle)
        {
            m_lastSubtitle = subtitle;
            Swap(subtitle);
        }
    }
}