using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Samsung.AppCommon.MVVM.Behaviors
{
    public class ClickCommandBehavior : CommandBehaviorBase
    {
        #region [Private Fields]
        private bool isMouseLeftButtonDown = false;
        #endregion

        #region [Dependency Properties]
        /// <summary>
        /// it represnets whether it ignore a touch tap(click) or not
        /// </summary>
        public bool IgnoreTouchTap
        {
            get { return (bool)GetValue(IgnoreTouchInputProperty); }
            set { SetValue(IgnoreTouchInputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IgnoreTouchTap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IgnoreTouchInputProperty =
            DependencyProperty.Register("IgnoreTouchTap", typeof(bool), typeof(ClickCommandBehavior), new PropertyMetadata(false));
        #endregion

        #region [Override Methods]
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;

            base.OnDetaching();
        }
        #endregion

        #region [Event Handlers]
        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IgnoreTouchTap && IsTouchInput(e))
                return;

            AssociatedObject.CaptureMouse();
            isMouseLeftButtonDown = true;
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isMouseLeftButtonDown)
            {
                isMouseLeftButtonDown = false;
                AssociatedObject.ReleaseMouseCapture();

                Point pt = e.GetPosition(AssociatedObject);
                if (pt.X >= 0 && pt.X < AssociatedObject.ActualWidth && pt.Y >= 0 && pt.Y < AssociatedObject.ActualHeight)
                    Command?.Execute(CommandParameter);
            }
        }
        #endregion

        #region [Private Methods]
        private bool IsTouchInput(MouseEventArgs e)
        {
            if (e != null && e.StylusDevice != null && e.StylusDevice.TabletDevice != null &&
                e.StylusDevice.TabletDevice.Type == TabletDeviceType.Touch)
                return true;

            return false;
        }
        #endregion
    }
}
