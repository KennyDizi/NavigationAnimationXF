using Xamarin.Forms;

namespace NavigationAnimationXF.SourceCode.Pages.BasePage
{
    public class XFVNNavigationAnimationBasePageView : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as XFVNNavigationAnimationBasePageViewModel;
            var result = bindingContext?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
            return result;
        }

        public void OnSoftBackButtonPressed()
        {
            var bindingContext = BindingContext as XFVNNavigationAnimationBasePageViewModel;
            bindingContext?.OnSoftBackButtonPressed();
        }

        public bool NeedOverrideSoftBackButton { get; set; } = false;
    }
}
