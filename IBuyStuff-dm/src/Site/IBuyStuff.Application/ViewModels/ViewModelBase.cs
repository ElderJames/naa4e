using System;

namespace IBuyStuff.Application.ViewModels
{
    public class ViewModelBase
    {
        public ViewModelBase()
        {
            Dino = SocialInterface.NewForDino();
            Andrea = SocialInterface.NewForAndrea();
        }
        public String Title { get; set; }
        public SocialInterface Dino { get; set; }
        public SocialInterface Andrea { get; set; }
    }
}
