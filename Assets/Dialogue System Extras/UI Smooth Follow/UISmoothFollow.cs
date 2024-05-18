﻿using UnityEngine;

namespace PixelCrushers.DialogueSystem.Extras
{

    /// <summary>
    /// Add to a GameObject, and assign a screen space UI panel.
    /// If the GameObject is involved in a conversation, or if 
    /// the follow variable is true, the UI will follow the GameObject.
    /// </summary>
    public class UISmoothFollow : MonoBehaviour
    {
        public RectTransform ui;
        public Vector3 offset = Vector3.zero;
        public float smoothTime = 0.2f;
        public bool follow;
        public bool deactivateOnConversationEnd = true;

        private Vector2 m_currentVelocity;
        private Canvas canvas;
        private RectTransform canvasRT;
        private bool needToSnap = false;
        private bool isInvisible = false;

        private void Awake()
        {
            if (ui == null)
            {
                Debug.LogError($"Assign a UI to UISmoothFollow on {name}.", this);
                enabled = false;
            }
        }

        private void Start()
        {
            if (DialogueManager.instance == null)
            {
                Debug.LogError($"No Dialogue Manager found. Can't register UISmoothFollow on {name}.", this);
                enabled = false;
            }
            else
            {
                ui.gameObject.SetActive(false);
                var dialogueSystemEvents = DialogueManager.instance.GetComponent<DialogueSystemEvents>() ??
                    DialogueManager.instance.gameObject.AddComponent<DialogueSystemEvents>();
                dialogueSystemEvents.conversationEvents.onConversationEnd.AddListener(HandleConversationEnd);
                dialogueSystemEvents.conversationEvents.onConversationLine.AddListener(HandleConversationLine);
            }
        }

        private void OnDestroy()
        {
            if (DialogueManager.instance == null) return;
            var dialogueSystemEvents = DialogueManager.instance.GetComponent<DialogueSystemEvents>();
            if (dialogueSystemEvents != null)
            {
                dialogueSystemEvents.conversationEvents.onConversationEnd.RemoveListener(HandleConversationEnd);
                dialogueSystemEvents.conversationEvents.onConversationLine.RemoveListener(HandleConversationLine);
            }
        }

        private void HandleConversationEnd(Transform actor)
        {
            follow = false;
            if (deactivateOnConversationEnd) ui.gameObject.SetActive(false);
        }

        private void HandleConversationLine(Subtitle subtitle)
        {
            if (subtitle.speakerInfo.transform == this.transform)
            {
                if (follow == false) MoveUI(true);
                follow = true;
            }
        }

        private void LateUpdate()
        {
            if (follow) MoveUI(false);
        }

        private void MoveUI(bool snap)
        {
            if (canvas == null)
            {
                canvas = ui.GetComponentInParent<Canvas>();
                canvasRT = canvas.GetComponent<RectTransform>();
            }
            if (ui.rect.width == 0)
            {
                needToSnap = true;
                SetInvisible(true);
            }
            else if (isInvisible)
            {
                SetInvisible(false);
            }
            var vectorToCamera = (Camera.main.transform.position - transform.position).normalized;
            var vectorToOffset = Quaternion.Euler(0, -90, 0) * vectorToCamera;
            var ofs = new Vector3(vectorToOffset.x * offset.x, offset.y, vectorToOffset.z);
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position + ofs) / canvasRT.localScale.x;
            ui.gameObject.SetActive(viewPos.z > 0);
            var newPosition = new Vector2((viewPos.x * canvas.pixelRect.width) - (ui.rect.width / 2), viewPos.y * canvas.pixelRect.height);
            newPosition.x += ((ui.sizeDelta.x / ui.localScale.x) / 2);
            if (snap || smoothTime == 0 || needToSnap)
            {
                ui.anchoredPosition = newPosition;
                m_currentVelocity = Vector2.zero;
                if (ui.rect.width > 0) needToSnap = false;
            }
            else
            {
                ui.anchoredPosition = Vector2.SmoothDamp(ui.anchoredPosition, newPosition, ref m_currentVelocity, smoothTime);
            }
        }

        private void SetInvisible(bool value)
        {
            isInvisible = value;
            var canvasGroup = ui.GetComponent<CanvasGroup>();
            if (canvasGroup != null) canvasGroup = ui.gameObject.AddComponent<CanvasGroup>();
            if (canvasGroup != null) canvasGroup.alpha = value ? 0 : 1;
        }
    }
}