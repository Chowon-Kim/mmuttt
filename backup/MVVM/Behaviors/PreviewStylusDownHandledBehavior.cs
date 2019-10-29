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
    public class PreviewStylusDownHandledBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewStylusDown += AssociatedObject_PreviewStylusDown;
        }

        private void AssociatedObject_PreviewStylusDown(object sender, System.Windows.Input.StylusDownEventArgs e)
        {
            if ((e.StylusDevice.TabletDevice != null) && (e.StylusDevice.TabletDevice.Type != TabletDeviceType.Stylus))
            {
                //touch임, pen 아님 
                return; 
            }
            e.Handled = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewStylusDown -= AssociatedObject_PreviewStylusDown;
            base.OnDetaching();
        }
    }
}
