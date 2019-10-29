using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsung.AppCommon.MVVM.Behaviors
{
    public class VisibilityChangedCommandBehavior : CommandBehaviorBase
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.IsVisibleChanged += AssociatedObject_IsVisibleChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.IsVisibleChanged -= AssociatedObject_IsVisibleChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (CommandParameter != null)
                Command?.Execute(CommandParameter);
            else
                Command?.Execute(e);
        }
    }
}
