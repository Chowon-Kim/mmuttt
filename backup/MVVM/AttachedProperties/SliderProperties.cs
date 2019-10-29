using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Samsung.AppCommon.MVVM.AttachedProperties
{
    public class SliderProperties
    {
        #region [IsChecked]
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached("IsChecked", typeof(bool), typeof(SliderProperties), new PropertyMetadata(false));
        public static void SetIsChecked(DependencyObject element, bool value) { element.SetValue(IsCheckedProperty, value); }
        public static bool GetIsChecked(DependencyObject element) { return (bool)element.GetValue(IsCheckedProperty); }
        #endregion

        #region [IsValueChanging] - CustomControl ToggleSlider is using this property
        public static readonly DependencyProperty IsValueChangingProperty = DependencyProperty.RegisterAttached("IsValueChanging", typeof(bool), typeof(SliderProperties), new PropertyMetadata(false));
        public static void SetIsValueChanging(DependencyObject element, bool value) { element.SetValue(IsValueChangingProperty, value); }
        public static bool GetIsValueChanging(DependencyObject element) { return (bool)element.GetValue(IsValueChangingProperty); }
        #endregion
    }
}
