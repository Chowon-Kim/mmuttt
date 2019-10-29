using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Samsung.AppCommon.MVVM.AttachedProperties
{
    public class CommonProperties
    {
        #region [ToolTipText]
        public static readonly DependencyProperty ToolTipTextProperty = DependencyProperty.RegisterAttached("ToolTipText", typeof(string), typeof(CommonProperties), new PropertyMetadata(string.Empty));
        public static void SetToolTipText(DependencyObject element, string value) { element.SetValue(ToolTipTextProperty, value); }
        public static string GetToolTipText(DependencyObject element) { return (string)element.GetValue(ToolTipTextProperty); }
        #endregion

        #region [TextTrimming]
        public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.RegisterAttached("TextTrimming", typeof(TextTrimming), typeof(CommonProperties), new PropertyMetadata(TextTrimming.None));
        public static void SetTextTrimming(DependencyObject element, TextTrimming value) { element.SetValue(TextTrimmingProperty, value); }
        public static TextTrimming GetTextTrimming(DependencyObject element) { return (TextTrimming)element.GetValue(TextTrimmingProperty); }
        #endregion

        #region [TextWrapping]
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.RegisterAttached("TextWrapping", typeof(TextWrapping), typeof(CommonProperties), new PropertyMetadata(TextWrapping.NoWrap));
        public static void SetTextWrapping(DependencyObject element, TextWrapping value) { element.SetValue(TextWrappingProperty, value); }
        public static TextWrapping GetTextWrapping(DependencyObject element) { return (TextWrapping)element.GetValue(TextWrappingProperty); }
        #endregion

        #region [OverEffectBGBrush]
        public static readonly DependencyProperty OverEffectBGBrushProperty = DependencyProperty.RegisterAttached("OverEffectBGBrush", typeof(SolidColorBrush), typeof(CommonProperties), new PropertyMetadata(null));
        public static void SetOverEffectBGBrush(DependencyObject element, SolidColorBrush value) { element.SetValue(OverEffectBGBrushProperty, value); }
        public static SolidColorBrush GetOverEffectBGBrush(DependencyObject element) { return (SolidColorBrush)element.GetValue(OverEffectBGBrushProperty); }
        #endregion

        #region [PressEffectBGBrush]
        public static readonly DependencyProperty PressEffectBGBrushProperty = DependencyProperty.RegisterAttached("PressEffectBGBrush", typeof(SolidColorBrush), typeof(CommonProperties), new PropertyMetadata(null));
        public static void SetPressEffectBGBrush(DependencyObject element, SolidColorBrush value) { element.SetValue(PressEffectBGBrushProperty, value); }
        public static SolidColorBrush GetPressEffectBGBrush(DependencyObject element) { return (SolidColorBrush)element.GetValue(PressEffectBGBrushProperty); }
        #endregion

        #region [ShowEffectBG]
        public static readonly DependencyProperty ShowEffectBGProperty = DependencyProperty.RegisterAttached("ShowEffectBG", typeof(bool), typeof(CommonProperties), new PropertyMetadata(true));
        public static void SetShowEffectBG(DependencyObject element, bool value) { element.SetValue(ShowEffectBGProperty, value); }
        public static bool GetShowEffectBG(DependencyObject element) { return (bool)element.GetValue(ShowEffectBGProperty); }
        #endregion

        #region [IsChecked]
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached("IsChecked", typeof(bool), typeof(CommonProperties), new PropertyMetadata(false));
        public static void SetIsChecked(DependencyObject element, bool value) { element.SetValue(IsCheckedProperty, value); }
        public static bool GetIsChecked(DependencyObject element) { return (bool)element.GetValue(IsCheckedProperty); }
        #endregion

        #region [OnEffectBorderBrush]
        public static readonly DependencyProperty OnEffectBorderBrushProperty = DependencyProperty.RegisterAttached("OnEffectBorderBrush", typeof(SolidColorBrush), typeof(CommonProperties), new PropertyMetadata(null));
        public static void SetOnEffectBorderBrush(DependencyObject element, SolidColorBrush value) { element.SetValue(OnEffectBorderBrushProperty, value); }
        public static SolidColorBrush GetOnEffectBorderBrush(DependencyObject element) { return (SolidColorBrush)element.GetValue(OnEffectBorderBrushProperty); }
        #endregion

        #region [OffEffectBorderBrush]
        public static readonly DependencyProperty OffEffectBorderBrushProperty = DependencyProperty.RegisterAttached("OffEffectBorderBrush", typeof(SolidColorBrush), typeof(CommonProperties), new PropertyMetadata(null));
        public static void SetOffEffectBorderBrush(DependencyObject element, SolidColorBrush value) { element.SetValue(OffEffectBorderBrushProperty, value); }
        public static SolidColorBrush GetOffEffectBorderBrush(DependencyObject element) { return (SolidColorBrush)element.GetValue(OffEffectBorderBrushProperty); }
        #endregion

        #region [OverEffectBorderBrush]
        public static readonly DependencyProperty OverEffectBorderBrushProperty = DependencyProperty.RegisterAttached("OverEffectBorderBrush", typeof(SolidColorBrush), typeof(CommonProperties), new PropertyMetadata(null));
        public static void SetOverEffectBorderBrush(DependencyObject element, SolidColorBrush value) { element.SetValue(OverEffectBorderBrushProperty, value); }
        public static SolidColorBrush GetOverEffectBorderBrush(DependencyObject element) { return (SolidColorBrush)element.GetValue(OverEffectBorderBrushProperty); }
        #endregion
    }
}
