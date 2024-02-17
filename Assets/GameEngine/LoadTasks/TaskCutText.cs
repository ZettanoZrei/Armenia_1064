using Assets.Game.Core;
using Assets.Game.HappeningSystem;
using System;
using Zenject;

namespace Loader
{
    public class TaskCutText : IInitializable
    {
        private readonly HappeningCatalog catalog;


        public TaskCutText(HappeningCatalog catalog)
        {
            this.catalog = catalog;
        }


        void IInitializable.Initialize()
        {
            Begin();
        }
        public void Begin()
        {
            foreach (var happening in catalog)
            {
                foreach (var node in happening.Nodes)
                {
                    foreach (var phrase in node.Phrases)
                        phrase.Text = CutString(phrase.Text);

                    foreach (var answer in node.Answers)
                        answer.Text = CutString(answer.Text);

                    foreach (var advice in node.Advices)
                        advice.Text = CutString(advice.Text);
                }
            }
        }
        private string CutString(string text)
        {
            if (string.IsNullOrEmpty(text) || !text.Contains("\r\n"))
                return text;
            return text.Substring(0, text.Length - 2);
        }
    }
}
