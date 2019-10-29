using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace Samsung.SmartSearchApp.View.Controls
{
    public class CustomScrollViewer : ScrollViewer
    {
        #region [Private const variable]
        const int WM_MOUSEHWHEEL = 0x020E;
        #endregion

        #region [IsNewSearch]
        public static readonly DependencyProperty IsNewSearchProperty = DependencyProperty.Register("IsNewSearch", typeof(bool), typeof(CustomScrollViewer),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnIsNewSearchChanged)));
        public bool IsNewSearch
        {
            get { return (bool)GetValue(IsNewSearchProperty); }
            set { SetValue(IsNewSearchProperty, value); }
        }

        private static void OnIsNewSearchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null || (bool)e.NewValue == false) return;
            if (d != null && d is CustomScrollViewer sv)
            {
                sv.ScrollToTop();
                sv.ScrollToLeftEnd();
            }
        }
        #endregion

        // TouchPad에서 Two finger로 Horizontal scroll 기능을 지원하는 CustomScrollViewer
        public CustomScrollViewer()
        {
            Loaded += CustomScrollViewer_Loaded;
        }

        private void CustomScrollViewer_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var source = PresentationSource.FromVisual(this);
            if (source == null || source.RootVisual == null) return;

            ((HwndSource)PresentationSource.FromVisual(source.RootVisual))?.AddHook(Hook);
        }

        #region [Horizontal Scroll]
        private IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_MOUSEHWHEEL:
                    int tilt = (short)HIWORD(wParam);
                    OnMouseTilt(tilt);
                    return (IntPtr)1;
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// Gets high bits values of the pointer.
        /// </summary>
        private static long HIWORD(IntPtr ptr)
        {
            var val64 = 0L;
            try
            {
                val64 = ptr.ToInt64();
                val64 = (val64 >> 16) & 0xFFFF;
            }
            catch (Exception)
            {
                return 0;
            }

            return val64;
        }

        /// <summary>
        /// Gets low bits values of the pointer.
        /// </summary>
        private static long LOWORD(IntPtr ptr)
        {
            var val64 = 0L;
            try
            {
                val64 = ptr.ToInt64();
                val64 = val64 & 0xFFFF;
            }
            catch (Exception)
            {
                return 0;
            }

            return val64;
        }

        private void OnMouseTilt(int tilt)
        {
            //if (sv.IsVisible == false)
            //{
            //    sv = GetTemplateChild("part_sv") as ScrollViewer;
            //    if (sv == null) return;
            //}

            if (tilt > 0)
                LineRight();
            else
                LineLeft();
        }
        #endregion
    }
}
