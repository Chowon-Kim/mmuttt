using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Samsung.AppCommon.MVVM.Behaviors
{
    public class HandledRoutedEventBehavior : Behavior<UIElement>
    {
        #region [ Dependency Properties ]

        #region [ Event Name ]

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register("EventName", typeof(string), typeof(HandledRoutedEventBehavior), new PropertyMetadata(string.Empty));

        #endregion

        #region [ Key Value ]

        public Key KeyValue
        {
            get { return (Key)GetValue(KeyValueProperty); }
            set { SetValue(KeyValueProperty, value); }
        }
        public static readonly DependencyProperty KeyValueProperty = DependencyProperty.Register("KeyValue", typeof(Key), typeof(HandledRoutedEventBehavior), new PropertyMetadata(Key.None));

        #endregion

        #region [ Command ]

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(HandledRoutedEventBehavior), new PropertyMetadata(null));

        #endregion

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            if (string.Equals(EventName, "MouseLeftButtonDown", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseButtonEvent;
            }
            else if (string.Equals(EventName, "MouseLeftButtonUp", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseButtonEvent;
            }
            else if (string.Equals(EventName, "KeyDown", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.KeyDown += AssociatedObject_KeyEvent;
            }
            else if (string.Equals(EventName, "KeyUp", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.KeyUp += AssociatedObject_KeyEvent;
            }
            else if (string.Equals(EventName, "TextChanged", StringComparison.CurrentCultureIgnoreCase))
            {
                if (AssociatedObject is TextBoxBase textBoxBaseObject)
                {
                    textBoxBaseObject.TextChanged += AssociatedObject_TextChanged;
                }
            }
            else if (string.Equals(EventName, "SubmenuOpened", StringComparison.CurrentCultureIgnoreCase))
            {
                if (AssociatedObject is MenuItem menuItemObject)
                {
                    menuItemObject.SubmenuOpened += AssociatedObject_SubmenuOpened;
                }
            }
            else if (string.Equals(EventName, "Opened", StringComparison.CurrentCultureIgnoreCase))
            {
                if (AssociatedObject is ContextMenu contextMenuObject)
                {
                    contextMenuObject.Opened += AssociatedObject_Opened;
                }
            }
            else if (string.Equals(EventName, "SelectionChanged", StringComparison.CurrentCultureIgnoreCase))
            {
                if (AssociatedObject is Selector selectorObject)
                {
                    selectorObject.SelectionChanged += AssociatedObject_SelectionChanged;
                }
            }
            else if (string.Equals(EventName, "MouseMove", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            }
            else if (string.Equals(EventName, "PreviewKeyDown", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
            }
            else if (string.Equals(EventName, "GotFocus", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.GotFocus += AssociatedObject_GotFocus;
            }
            else if (string.Equals(EventName, "LostFocus", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.LostFocus += AssociatedObject_LostFocus;
            }
            else if (string.Equals(EventName, "TouchDown", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.TouchDown += AssociatedObject_TouchDown;
            }
            else if (string.Equals(EventName, "StylusUp", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.StylusUp += AssociatedObject_StylusUp;
            }
        }
    

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (string.Equals(EventName, "MouseLeftButtonDown", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseButtonEvent;
            }
            else if (string.Equals(EventName, "MouseLeftButtonUp", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseButtonEvent;
            }
            else if (string.Equals(EventName, "KeyDown", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.KeyDown -= AssociatedObject_KeyEvent;
            }
            else if (string.Equals(EventName, "KeyUp", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.KeyUp -= AssociatedObject_KeyEvent;
            }
            else if (string.Equals(EventName, "TextChanged", StringComparison.CurrentCultureIgnoreCase))
            {
                if (AssociatedObject is TextBoxBase textBoxBaseObject)
                {
                    textBoxBaseObject.TextChanged -= AssociatedObject_TextChanged;
                }
            }
            else if (string.Equals(EventName, "SubmenuOpened", StringComparison.CurrentCultureIgnoreCase))
            {
                if (AssociatedObject is MenuItem menuItemObject)
                {
                    menuItemObject.SubmenuOpened -= AssociatedObject_SubmenuOpened;
                }
            }
            else if (string.Equals(EventName, "Opened", StringComparison.CurrentCultureIgnoreCase))
            {
                if (AssociatedObject is ContextMenu contextMenuObject)
                {
                    contextMenuObject.Opened -= AssociatedObject_Opened;
                }
            }
            else if (string.Equals(EventName, "SelectionChanged", StringComparison.CurrentCultureIgnoreCase))
            {
                if (AssociatedObject is Selector selectorObject)
                {
                    selectorObject.SelectionChanged -= AssociatedObject_SelectionChanged;
                }
            }
            else if (string.Equals(EventName, "MouseMove", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            }
            else if (string.Equals(EventName, "PreviewKeyDown", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
            }
            else if (string.Equals(EventName, "GotFocus", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
            }
            else if (string.Equals(EventName, "LostFocus", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.LostFocus -= AssociatedObject_LostFocus;
            }
            else if (string.Equals(EventName, "TouchDown", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.TouchDown -= AssociatedObject_TouchDown;
            }
            else if (string.Equals(EventName, "StylusUp", StringComparison.CurrentCultureIgnoreCase))
            {
                AssociatedObject.StylusUp -= AssociatedObject_StylusUp;
            }
        }

        private void AssociatedObject_MouseButtonEvent(object sender, MouseButtonEventArgs e)
        {
            Command?.Execute(e);
        }

        private void AssociatedObject_KeyEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == KeyValue)
            {
                Command?.Execute(e);
            }
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            Command?.Execute(e);
        }

        private void AssociatedObject_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            Command?.Execute(e);
        }

        private void AssociatedObject_Opened(object sender, RoutedEventArgs e)
        {
            Command?.Execute(e);
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Command?.Execute(e);
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            Command?.Execute(e);
        }

        private void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Command?.Execute(e);
        }

        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            Command?.Execute(e);
        }

        private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            Command?.Execute(e);
        }

        private void AssociatedObject_TouchDown(object sender, TouchEventArgs e)
        {
            Command?.Execute(e);
        }


        private void AssociatedObject_StylusUp(object sender, StylusEventArgs e)
        {
            Command?.Execute(e);
        }
    }
}
