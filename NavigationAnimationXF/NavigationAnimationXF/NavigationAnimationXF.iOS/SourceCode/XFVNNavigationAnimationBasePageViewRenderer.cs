using NavigationAnimationXF.iOS.SourceCode;
using NavigationAnimationXF.SourceCode.Pages.BasePage;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(XFVNNavigationAnimationBasePageView), typeof(XFVNNavigationAnimationBasePageViewRenderer))]

namespace NavigationAnimationXF.iOS.SourceCode
{
    public class XFVNNavigationAnimationBasePageViewRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var page = Element as XFVNNavigationAnimationBasePageView;
            if (page == null) return;

            #region for soft back button

            var root = NavigationController.TopViewController;
            if (!page.NeedOverrideSoftBackButton) return;
            var title = NavigationPage.GetBackButtonTitle(Element);

            root.NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem(title, UIBarButtonItemStyle.Plain, (sender, args) =>
                {
                    page.OnSoftBackButtonPressed();
                }), true);

            #endregion
        }
    }
}
