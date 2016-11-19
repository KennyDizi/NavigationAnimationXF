using System;
using NavigationAnimationXF.SourceCode.Pages.HandleHardware;
using NavigationAnimationXF.SourceCode.Pages.HandleSoftware;

namespace NavigationAnimationXF.SourceCode.Pages.MainPage
{
    public partial class MainPageView
    {
        public MainPageView()
        {
            InitializeComponent();
        }

        private void OnGotoHardwarePage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HandleHardwarePageView(), true);
        }

        private void OnGotoSoftwarePage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HandleSoftwarePageView(), true);
        }
    }
}
