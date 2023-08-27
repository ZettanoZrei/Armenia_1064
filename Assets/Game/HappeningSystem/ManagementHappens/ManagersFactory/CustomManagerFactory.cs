using ModestTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Game.HappeningSystem.ManagementHappens
{
    class CustomManagerFactory : IFactory<IHappeningManager>
    {
        private readonly DialogManager.Factory dialogFactory;
        private readonly AccidentManager.Factory accidentFactory;
        private readonly ManagerTypeResolver managerTypeResolver;
        public CustomManagerFactory(ManagerTypeResolver managerTypeResolver, DialogManager.Factory dialogFactory, 
            AccidentManager.Factory accidentFactory)
        {
            this.dialogFactory = dialogFactory;
            this.accidentFactory = accidentFactory;
            this.managerTypeResolver = managerTypeResolver;
        }

        public IHappeningManager Create()
        {
            if (managerTypeResolver.HappenType == PopupType.DialogPopup)
            {
                return dialogFactory.Create();
            }
            else
            {
                return accidentFactory.Create();
            }            
        }
    }
}
