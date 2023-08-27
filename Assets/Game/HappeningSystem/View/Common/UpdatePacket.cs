using Model.Entities.Answers;
using Model.Entities.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.HappeningSystem.View.Common
{
    internal class UpdatePacket
    {
        public Phrase Phrase { get; set; }
        public bool IsFinish { get; set; }
        public List<Phrase> Advices { get; set; }
        public List<Answer> Answers { get; set; }
        public bool IsFocus { get; set; }
        public bool IsShowFace { get; set; }
    }
}
