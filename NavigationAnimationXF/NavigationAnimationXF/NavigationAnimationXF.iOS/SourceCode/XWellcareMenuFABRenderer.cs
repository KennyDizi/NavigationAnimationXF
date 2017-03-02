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

        protected override void OnElementChanged(ElementChangedEventArgs<XWellcareMenuFAB> e)
        {
            base.OnElementChanged(e);

            //init control
            if (Control == null)
            {
                //init menu
                var height = UIScreen.MainScreen.Bounds.Height;
                var width = UIScreen.MainScreen.Bounds.Width;
                var frame2 = new CGRect(x: width - 56 - 16, y: height - 56 - 16, width: 56, height: 56);
                var cslayer = FrameToCaShapeLayer(frame2);
                var frame = new CGRect(0, 0, 50, 50);
                _menuFab = new LiquidFloatingActionButton(frame)
                {
                    AnimateStyle = AnimateStyle.Up,
                    EnableShadow = true,
                    Color = Color.Blue.ToUIColor(),
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

        private void MenuFabCellSelected(object sender, CellSelectedEventArgs e)
        {
            //get item click index
            var selectedCellIndex = e.Index;
            Element.RaiseSelectIndexChanged(selectedCellIndex);

            //close fab menu
            _menuFab.Close();
        }

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
        
        private static CAShapeLayer FrameToCaShapeLayer(CGRect frame)
        {
            var plusLayer = new CAShapeLayer
            {
                LineCap = new NSString("kCALineCapRound"),
                StrokeColor = UIColor.White.CGColor,
                LineWidth = 3.0f
            };

            var w = frame.Width;
            var h = frame.Height;

            var points = new Dictionary<CGPoint, CGPoint>
            {
                {new CGPoint(x: w * 0.25, y: h * 0.35), new CGPoint(x: w * 0.75, y: h * 0.35)},
                {new CGPoint(x: w * 0.25, y: h * 0.5), new CGPoint(x: w * 0.75, y: h * 0.5)},
                {new CGPoint(x: w * 0.25, y: h * 0.65), new CGPoint(x: w * 0.75, y: h * 0.65)}
            };

            var path = new UIBezierPath();

            foreach (var point in points)
            {
                path.MoveTo(point.Key);
                path.AddLineTo(point.Value);
            }

            plusLayer.Path = path.CGPath;

            return plusLayer;
        }
    }
}