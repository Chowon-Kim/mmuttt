using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Samsung.AppCommon.MVVM.AttachedProperties
{
    public class ImgFileProperties
    {
        #region [Normal]
        public static readonly DependencyProperty NormalProperty =
           DependencyProperty.RegisterAttached("Normal", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static void SetNormal(DependencyObject element, string value) { element.SetValue(NormalProperty, value); }
        public static string GetNormal(DependencyObject element) { return (string)element.GetValue(NormalProperty); }
        #endregion

        #region [Pressed]
        public static readonly DependencyProperty PressedProperty =
            DependencyProperty.RegisterAttached("Pressed", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetPressed(DependencyObject obj) { return (string)obj.GetValue(PressedProperty); }
        public static void SetPressed(DependencyObject obj, string value) { obj.SetValue(PressedProperty, value); }
        #endregion

        #region [Over]
        public static readonly DependencyProperty OverProperty =
            DependencyProperty.RegisterAttached("Over", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetOver(DependencyObject obj) { return (string)obj.GetValue(OverProperty); }
        public static void SetOver(DependencyObject obj, string value) { obj.SetValue(OverProperty, value); }
        #endregion

        #region [Disabled]
        public static readonly DependencyProperty DisabledProperty =
            DependencyProperty.RegisterAttached("Disabled", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetDisabled(DependencyObject obj) { return (string)obj.GetValue(DisabledProperty); }
        public static void SetDisabled(DependencyObject obj, string value) { obj.SetValue(DisabledProperty, value); }
        #endregion

        #region [Checked]
        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.RegisterAttached("Checked", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetChecked(DependencyObject obj) { return (string)obj.GetValue(CheckedProperty); }
        public static void SetChecked(DependencyObject obj, string value) { obj.SetValue(CheckedProperty, value); }
        #endregion

        #region [CheckedOver]
        public static readonly DependencyProperty CheckedOverProperty =
            DependencyProperty.RegisterAttached("CheckedOver", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetCheckedOver(DependencyObject obj) { return (string)obj.GetValue(CheckedOverProperty); }
        public static void SetCheckedOver(DependencyObject obj, string value) { obj.SetValue(CheckedOverProperty, value); }
        #endregion

        #region [CheckedPressed]
        public static readonly DependencyProperty CheckedPressedProperty =
            DependencyProperty.RegisterAttached("CheckedPressed", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetCheckedPressed(DependencyObject obj) { return (string)obj.GetValue(CheckedPressedProperty); }
        public static void SetCheckedPressed(DependencyObject obj, string value) { obj.SetValue(CheckedPressedProperty, value); }
        #endregion

        #region [CheckedDisabled]
        public static readonly DependencyProperty CheckedDisabledProperty =
            DependencyProperty.RegisterAttached("CheckedDisabled", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetCheckedDisabled(DependencyObject obj) { return (string)obj.GetValue(CheckedDisabledProperty); }
        public static void SetCheckedDisabled(DependencyObject obj, string value) { obj.SetValue(CheckedDisabledProperty, value); }

        #endregion

        #region [Unchecked]
        public static readonly DependencyProperty UncheckedProperty =
            DependencyProperty.RegisterAttached("Unchecked", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetUnchecked(DependencyObject obj) { return (string)obj.GetValue(UncheckedProperty); }
        public static void SetUnchecked(DependencyObject obj, string value) { obj.SetValue(UncheckedProperty, value); }
        #endregion

        #region [UncheckedOver]
        public static readonly DependencyProperty UncheckedOverProperty =
            DependencyProperty.RegisterAttached("UncheckedOver", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetUncheckedOver(DependencyObject obj) { return (string)obj.GetValue(UncheckedOverProperty); }
        public static void SetUncheckedOver(DependencyObject obj, string value) { obj.SetValue(UncheckedOverProperty, value); }
        #endregion

        #region [UncheckedPressed]
        public static readonly DependencyProperty UncheckedPressedProperty =
            DependencyProperty.RegisterAttached("UncheckedPressed", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetUncheckedPressed(DependencyObject obj) { return (string)obj.GetValue(UncheckedPressedProperty); }
        public static void SetUncheckedPressed(DependencyObject obj, string value) { obj.SetValue(UncheckedPressedProperty, value); }
        #endregion

        #region [UncheckedDisabled]
        public static readonly DependencyProperty UncheckedDisabledProperty =
            DependencyProperty.RegisterAttached("UncheckedDisabled", typeof(string), typeof(ImgFileProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static string GetUncheckedDisabled(DependencyObject obj) { return (string)obj.GetValue(UncheckedDisabledProperty); }
        public static void SetUncheckedDisabled(DependencyObject obj, string value) { obj.SetValue(UncheckedDisabledProperty, value); }
        #endregion

        #region [ImageWidth]
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.RegisterAttached("ImageWidth", typeof(double), typeof(ImgFileProperties), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));
        public static double GetImageWidth(DependencyObject obj) { return (double)obj.GetValue(ImageWidthProperty); }
        public static void SetImageWidth(DependencyObject obj, double value) { obj.SetValue(ImageWidthProperty, value); }
        #endregion

        #region [ImageHeight]
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.RegisterAttached("ImageHeight", typeof(double), typeof(ImgFileProperties), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));
        public static double GetImageHeight(DependencyObject obj) { return (double)obj.GetValue(ImageHeightProperty); }
        public static void SetImageHeight(DependencyObject obj, double value) { obj.SetValue(ImageHeightProperty, value); }
        #endregion
    }
}
