using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AnimatedButtons;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using NavigationAnimationXF.iOS.SourceCode;
using NavigationAnimationXF.SourceCode.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(XWellcareMenuFAB), typeof(XWellcareMenuFABRenderer))]

namespace NavigationAnimationXF.iOS.SourceCode
{
    [Preserve(AllMembers = true)]
    // ReSharper disable once InconsistentNaming
    public class XWellcareMenuFABRenderer : ViewRenderer<XWellcareMenuFAB, LiquidFloatingActionButton>
    {
        private LiquidFloatingActionButton _menuFab;

        /// <summary>
        /// render control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<XWellcareMenuFAB> e)
        {
            base.OnElementChanged(e);

            //init control
            if (Control == null)
            {
                //init menu
                var frame = new CGRect(0, 0, 50, 50);
                _menuFab = new LiquidFloatingActionButton(frame)
                {
                    AnimateStyle = AnimateStyle.Up,
                    EnableShadow = true,
                    Color = XInAppUtilities.Instance.GetColor((int) XColorKeys.MainColor).ToUIColor(),
                    Cells = Element.Children.Select(
                        btn =>
                            new LiquidFloatingCell(UIImage.FromBundle(btn.ImageName))
                            {
                                Responsible = true,
                                UserInteractionEnabled = true
                            }).ToList()
                };

                Element.Show = delegate { _menuFab.Open(); };
                Element.Hide = delegate { _menuFab.Close(); };
                Element.GetFabIsOpen = () => !_menuFab.IsClosed;
                
                //set native control
                SetNativeControl(_menuFab);
            }

            if (e.OldElement != null)
            {
                //unregister
                System.Diagnostics.Debug.Write("unregister cell selected");
                _menuFab.CellSelected -= MenuFabCellSelected;
            }

            if (e.NewElement != null)
            {
                //register
                System.Diagnostics.Debug.Write("register cell selected");
                _menuFab.CellSelected += MenuFabCellSelected;
            }
        }

        /// <summary>
        /// handle click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuFabCellSelected(object sender, CellSelectedEventArgs e)
        {
            //get item click index
            var selectedCellIndex = e.Index;
            Element.RaiseSelectIndexChanged(selectedCellIndex);

            //close fab menu
            _menuFab.Close();
        }

        /// <summary>
        /// refresh itemsource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == XWellcareMenuFAB.ChildrenProperty.PropertyName)
            {
                var cells =
                    Element.Children.Select(
                        btn => new LiquidFloatingCell(UIImage.FromBundle(btn.ImageName))
                            {Responsible = true}).ToList();
                _menuFab.Cells = cells;
            }
        }

        /// <summary>
        /// handle touch floating action button
        /// </summary>
        /// <param name="point"></param>
        /// <param name="uievent"></param>
        /// <returns></returns>
        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            if (_menuFab.Frame.Contains(point)) return _menuFab;

            foreach (var cell in _menuFab.Cells)
            {
                if (cell.Frame.Contains(point))
                    return cell;
            }

            return base.HitTest(point, uievent);
        }
    }
}