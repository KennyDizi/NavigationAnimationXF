using NavigationAnimationXF.SourceCode.Pages.MainPage;
using Xamarin.Forms;

namespace NavigationAnimationXF
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPageView());
        }
    }
}
