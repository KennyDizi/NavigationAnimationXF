using NavigationAnimationXF.SourceCode.Pages.BasePage;
using Xamarin.Forms;

namespace NavigationAnimationXF.SourceCode.Pages.HandleHardware
{
    public class HandleHardwarePageViewModel : XFVNNavigationAnimationBasePageViewModel
    {
        public override bool OnBackButtonPressed()
        {
            var page = ((NavigationPage) Application.Current.MainPage).CurrentPage as HandleHardwarePageView;
            if(page == null) return false;
            var result = page.DisplayAlert("Ahihi",
                "This is handle hardware back button from ViewModel, do you want go to previous page", "OK", "Cancel");
            return true;
        }
    }
}
