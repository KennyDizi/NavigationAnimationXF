namespace NavigationAnimationXF.SourceCode.Pages.BasePage
{
    public class XFVNNavigationAnimationBasePageViewModel
    {
        /// <summary>
        /// //false is default value when system call back press
        /// </summary>
        /// <returns></returns>
        public virtual bool OnBackButtonPressed()
        {
            //false is default value when system call back press
            return false;
        }

        /// <summary>
        /// called when page need override soft back button
        /// </summary>
        public virtual void OnSoftBackButtonPressed() { }
    }
}
