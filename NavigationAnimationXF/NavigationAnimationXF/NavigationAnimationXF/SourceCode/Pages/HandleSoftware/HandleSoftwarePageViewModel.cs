using NavigationAnimationXF.SourceCode.Pages.BasePage;
using Xamarin.Forms;

namespace NavigationAnimationXF.SourceCode.Pages.HandleSoftware
{
    public class HandleSoftwarePageViewModel : XFVNNavigationAnimationBasePageViewModel
    {
        public override void OnSoftBackButtonPressed()
        {
            var page = ((NavigationPage)Application.Current.MainPage).CurrentPage as HandleSoftwarePageView;
            page?.DisplayAlert("Ahihi",
                "This is hanlde software back button from ViewModel, do you want go to previous page", "OK", "Cancle");
        }
    }
}
