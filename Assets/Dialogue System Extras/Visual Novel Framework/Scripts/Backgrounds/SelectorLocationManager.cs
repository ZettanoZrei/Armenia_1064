
using PixelCrushers.DialogueSystem.VisualNovelFramework;
using UnityEngine;
using System;

namespace PixelCrushers.DialogueSystem
{
    public class SelectorLocationManager : MonoBehaviour
    {
        [LocationPopup(true)] public string CurrentLocation;
    }

}
