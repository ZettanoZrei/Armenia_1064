using System;
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

        /*
        private void OnConversationLine(Subtitle subtitle)
        {
            if (subtitle == null || backgroundManager == null) return;
            var background = Field.LookupValue(subtitle.dialogueEntry.fields, "Background");
            if (!string.IsNullOrEmpty(background))
            {
                BackgroundManager.SetBackgroundImage(background);
            }
            else 
            {
                background = DialogueLua.GetActorField(subtitle.speakerInfo.nameInDatabase, "Background").asString;
                if (!string.IsNullOrEmpty(background))
                {
                    BackgroundManager.SetBackgroundImage(background);
                }
            }
        }
        */
        private void OnConversationLine(Subtitle subtitle)
        {
            
            if (subtitle == null || backgroundManager == null) return;
            var locationIndex = Field.LookupValue(subtitle.dialogueEntry.fields, "Location");
            var subAdress = "";
            var dialogueActor = DialogueActor.GetDialogueActorComponent(subtitle.speakerInfo.transform);
            
            if (dialogueActor != null)
            {
                Debug.Log($"{subtitle.dialogueEntry.subtitleText}");
                var position = dialogueActor.customScenePosition;
                
                if (position > 0)
                    subAdress = position % 2 == 0 ? "" : "_back";
                else
                    subAdress = subtitle.speakerInfo.isPlayer ? "" : "_back";

                if (subAdress == "")
                    CustomDialoguePosition.Flip(0);
                else 
                    CustomDialoguePosition.Flip(180);
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
        

    }
}