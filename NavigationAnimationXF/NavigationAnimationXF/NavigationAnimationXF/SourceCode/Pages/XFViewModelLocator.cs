using NavigationAnimationXF.SourceCode.Pages.HandleHardware;
using NavigationAnimationXF.SourceCode.Pages.HandleSoftware;

namespace NavigationAnimationXF.SourceCode.Pages
{
    public class XFViewModelLocator
    {
        public HandleHardwarePageViewModel HandleHardwarePageViewModel => new HandleHardwarePageViewModel();

        public HandleSoftwarePageViewModel HandleSoftwarePageViewModel => new HandleSoftwarePageViewModel();
    }
}
