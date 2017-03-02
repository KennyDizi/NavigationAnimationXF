using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace NavigationAnimationXF.SourceCode.Controls
{
    // ReSharper disable once InconsistentNaming
    public class XWellcareMenuFAB : View
    {
        public XWellcareMenuFAB()
        {
            Children = new ObservableCollection<XWellcareFAB>();
            IsOpened = false;
        }

        private bool _isOpened;

        public bool IsOpened
        {
            get { return _isOpened; }
            set
            {
                _isOpened = value;
                OnPropertyChanged();
            }
        }

        public delegate void ShowHideDelegate(bool animate = true);

        public delegate bool GetOpen();

        public ShowHideDelegate Show { get; set; }
        public ShowHideDelegate Hide { get; set; }
        public GetOpen GetFabIsOpen { get; set; }

        public static readonly BindableProperty ChildrenProperty =
            BindableProperty.Create(nameof(Children), typeof(ObservableCollection<XWellcareFAB>),
                typeof(XWellcareMenuFAB));

        public ObservableCollection<XWellcareFAB> Children
        {
            get { return (ObservableCollection<XWellcareFAB>) GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        public static readonly BindableProperty DetailProperty =
            BindableProperty.Create(nameof(Detail), typeof(string), typeof(XWellcareMenuFAB), string.Empty);

        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }

        public event EventHandler<XViewEventArgs> SelectIndexChanged; 
        public void RaiseSelectIndexChanged(int index)
        {
            SelectIndexChanged?.Invoke(this, new XViewEventArgs("SelectIndexChanged", index));
        }
    }

    // ReSharper disable once InconsistentNaming
    public class XWellcareFAB : View
    {
        public static readonly BindableProperty DetailProperty =
            BindableProperty.Create(nameof(Detail), typeof(string), typeof(XWellcareFAB), string.Empty);

        public string Detail
        {
            get { return (string) GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }

        public static readonly BindableProperty ClickIdProperty =
            BindableProperty.Create(nameof(ClickId), typeof(int), typeof(XWellcareFAB), -1);

        public int ClickId
        {
            get { return (int) GetValue(ClickIdProperty); }
            set { SetValue(ClickIdProperty, value); }
        }

        public static readonly BindableProperty ImageNameProperty =
            BindableProperty.Create(nameof(ImageName), typeof(string), typeof(XWellcareFAB), string.Empty);

        public string ImageName
        {
            get { return (string) GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        public static readonly BindableProperty ColorNormalProperty =
            BindableProperty.Create(nameof(ColorNormal), typeof(Color), typeof(XWellcareFAB), Color.White);

        public Color ColorNormal
        {
            get { return (Color) GetValue(ColorNormalProperty); }
            set { SetValue(ColorNormalProperty, value); }
        }

        public static readonly BindableProperty ColorPressedProperty =
            BindableProperty.Create(nameof(ColorPressed), typeof(Color), typeof(XWellcareFAB), Color.White);

        public Color ColorPressed
        {
            get { return (Color) GetValue(ColorPressedProperty); }
            set { SetValue(ColorPressedProperty, value); }
        }

        public static readonly BindableProperty ColorRippleProperty =
            BindableProperty.Create(nameof(ColorRipple), typeof(Color), typeof(XWellcareFAB), Color.White);

        public Color ColorRipple
        {
            get { return (Color) GetValue(ColorRippleProperty); }
            set { SetValue(ColorRippleProperty, value); }
        }

        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size), typeof(FloatingActionButtonSize), typeof(XWellcareFAB),
                FloatingActionButtonSize.Normal);

        public FloatingActionButtonSize Size
        {
            get { return (FloatingActionButtonSize) GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(XWellcareFAB), true);

        public bool HasShadow
        {
            get { return (bool) GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }
    }

    public enum FloatingActionButtonSize
    {
        Normal,
        Mini
    }
}
