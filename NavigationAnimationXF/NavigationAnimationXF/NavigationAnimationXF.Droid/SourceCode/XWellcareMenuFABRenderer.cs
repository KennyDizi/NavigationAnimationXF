using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using Clans.Fab;
using NavigationAnimationXF.Droid.SourceCode;
using NavigationAnimationXF.SourceCode.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XWellcareMenuFAB), typeof(XWellcareMenuFABRenderer))]

namespace NavigationAnimationXF.Droid.SourceCode
{
    // ReSharper disable once InconsistentNaming
    public class XWellcareMenuFABRenderer :
        ViewRenderer<XWellcareMenuFAB, FloatingActionMenu>, FloatingActionMenu.IOnMenuToggleListener, Android.Views.View.IOnClickListener
    {
        private readonly Context _context;
        private FloatingActionMenu _menuFab;

        public XWellcareMenuFABRenderer()
        {
            _context = Xamarin.Forms.Forms.Context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<XWellcareMenuFAB> e)
        {
            base.OnElementChanged(e);            

            //init control
            if (Control == null)
            {
                //init menu
                var inflater = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);
                var view = inflater.Inflate(Resource.Layout.menufab, null);
                var menuFab = view.FindViewById<FloatingActionMenu>(Resource.Id.fabmenures);
                menuFab.MenuButtonLabelText = Element.Detail;
                menuFab.SetOnMenuToggleListener(this);
                menuFab.SetClosedOnTouchOutside(false);                
                _menuFab = menuFab;
                Element.Show = delegate(bool animate)
                {
                    if (!_menuFab.IsOpened)
                        _menuFab.Toggle(animate);
                };

                Element.Hide = delegate(bool animate)
                {
                    if (_menuFab.IsOpened)
                        _menuFab.Toggle(animate);
                };

                Element.GetFabIsOpen = () => Element.IsOpened;

                //init items
                _menuFab.RemoveAllMenuButtons();
                var listFab = Element.Children;
                if (listFab.Count == 0) return;
                foreach (var btn in listFab)
                {
                    var inflateritem = (LayoutInflater) _context.GetSystemService(Context.LayoutInflaterService);
                    var viewitem = inflateritem.Inflate(Resource.Layout.itemfab, null);
                    var fab = viewitem.FindViewById<Clans.Fab.FloatingActionButton>(Resource.Id.fabitemres);
                    fab.SetShowShadow(btn.HasShadow);
                    fab.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    fab.LabelText = btn.Detail;
                    if (!string.IsNullOrEmpty(btn.ImageName))
                        SetFabImage(fab, btn.ImageName);
                    fab.Id = btn.ClickId;
                    fab.Clickable = true;
                    fab.SetOnClickListener(this);

                    _menuFab.AddMenuButton(fab);
                }
                //set native control
                SetNativeControl(_menuFab);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            //handler some property when changed
            if (e.PropertyName == XWellcareMenuFAB.ChildrenProperty.PropertyName)
            {
                _menuFab.RemoveAllMenuButtons();
                var listFab = Element.Children;
                if (listFab.Count == 0) return;
                foreach (var btn in listFab)
                {
                    var inflater = (LayoutInflater) _context.GetSystemService(Context.LayoutInflaterService);
                    var view = inflater.Inflate(Resource.Layout.itemfab, null);
                    var fab = view.FindViewById<Clans.Fab.FloatingActionButton>(Resource.Id.fabitemres);
                    fab.SetShowShadow(btn.HasShadow);
                    fab.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    fab.LabelText = btn.Detail;
                    fab.Id = btn.ClickId;
                    fab.Clickable = true;
                    fab.SetOnClickListener(this);
                    if(!string.IsNullOrEmpty(btn.ImageName))
                        SetFabImage(fab, btn.ImageName);
                    _menuFab.AddMenuButton(fab);
                }
            }
            else if (e.PropertyName == XWellcareMenuFAB.DetailProperty.PropertyName)
            {
                _menuFab.MenuButtonLabelText = Element.Detail;
            }
        }

        /// <summary>
        /// OnMenuToggle - listent togggle click
        /// </summary>
        /// <param name="opened"></param>
        public void OnMenuToggle(bool opened)
        {
            Element.IsOpened = opened;
        }

        /// <summary>
        /// OnClick - listent item click
        /// </summary>
        /// <param name="v"></param>
        public void OnClick(Android.Views.View v)
        {
            var fab = v as Clans.Fab.FloatingActionButton;
            _menuFab.Toggle(animate: true);
            if (fab != null)
            {
                Element.RaiseSelectIndexChanged(fab.Id);
            }
        }

        /// <summary>
        /// SetFabImage
        /// </summary>
        /// <param name="fab"></param>
        /// <param name="imageName"></param>
        private void SetFabImage(Clans.Fab.FloatingActionButton fab, string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
            {
                try
                {
                    Task.Run(async () =>
                    {
                        var drawableNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(imageName);
                        var resources = _context.Resources;
                        var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable",
                            _context.PackageName);
                        var bitmap = await BitmapFactory.DecodeResourceAsync(_context.Resources,
                            imageResourceName);

                        var activity = Context as Activity;
                        activity?.RunOnUiThread(() =>
                        {
                            fab.SetScaleType(ImageView.ScaleType.FitCenter);
                            fab.SetImageBitmap(bitmap);
                        });
                    });
                }
                catch (Exception ex)
                {
#if DEBUG
                    throw new FileNotFoundException("There was no Android Drawable by that name.", ex);
#else
                    System.Diagnostics.Debug.Write(ex.Message);
#endif
                }
            }
        }

        public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            float height = options.OutHeight;
            float width = options.OutWidth;
            double inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = (int)(height / 2);
                int halfWidth = (int)(width / 2);

                // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
                while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return (int)inSampleSize;
        }
    }

    //behaviour
    [Register("XFMiniMobileApplication.Droid.SourceCode.Controls.XWellcare.FloatingActionMenuBehavior")]
    public class FloatingActionMenuBehavior : CoordinatorLayout.Behavior
    {
        private float _mTranslationY;

        public FloatingActionMenuBehavior(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, Android.Views.View dependency)
        {
            return IsInstanceOf<Snackbar.SnackbarLayout>(dependency);
        }

        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, Android.Views.View dependency)
        {
            if (IsInstanceOf<FloatingActionMenu>(child) && IsInstanceOf<Snackbar.SnackbarLayout>(dependency))
            {
                UpdateTranslation(parent, (Android.Views.View)child, dependency);
            }

            return false;
        }

        public override void OnDependentViewRemoved(CoordinatorLayout parent, Java.Lang.Object child, Android.Views.View dependency)
        {
            if (IsInstanceOf<FloatingActionMenu>(child) && IsInstanceOf<Snackbar.SnackbarLayout>(dependency))
            {
                UpdateTranslation(parent, (Android.Views.View)child, dependency);
            }
        }

        private void UpdateTranslation(CoordinatorLayout parent, Android.Views.View child, Android.Views.View dependency)
        {
            var translationY = GetTranslationY(parent, child);
            if (Math.Abs(translationY - _mTranslationY) > double.Epsilon)
            {
                ViewCompat.Animate(child).Cancel();

                if (Math.Abs(Math.Abs(translationY - _mTranslationY) - dependency.Height) < double.Epsilon)
                {
                    ViewCompat.Animate(child)
                        .TranslationY(translationY)
                        .SetListener(null);
                }
                else
                {
                    ViewCompat.SetTranslationY(child, translationY);
                }

                _mTranslationY = translationY;
            }
        }

        private static float GetTranslationY(CoordinatorLayout parent, Android.Views.View child)
        {
            var minOffset = 0.0F;
            var dependencies = parent.GetDependencies(child);
            var i = 0;

            for (var z = dependencies.Count; i < z; ++i)
            {
                var view = dependencies[i];
                if (IsInstanceOf<Snackbar.SnackbarLayout>(view) && parent.DoViewsOverlap(child, view))
                {
                    minOffset = Math.Min(minOffset, ViewCompat.GetTranslationY(view) - view.Height);
                }
            }

            return minOffset;
        }

        private static bool IsInstanceOf<T>(object instance)
        {
            return instance.GetType().IsAssignableFrom(typeof(T));
        }
    }
}