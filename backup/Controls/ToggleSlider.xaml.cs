using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Samsung.SmartSearchApp.View.Controls
{
    /// <summary>
    /// ToggleSlider.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToggleSlider : UserControl
    {
        private bool _pressFlag = false;

        #region [Properties]
        #region [IsToggleOn]
        public bool IsToggleOn
        {
            get { return (bool)GetValue(IsToggleOnProperty); }
            set { SetValue(IsToggleOnProperty, value); }
        }

        public static readonly DependencyProperty IsToggleOnProperty =
            DependencyProperty.Register("IsToggleOn", typeof(bool), typeof(ToggleSlider), new PropertyMetadata(false));

        #endregion

        #region [DefaultThumbStatus]
        // Thumb의 최초 위치 (0 or 1) 설정을 위해 Binding mode "OneTime"으로 사용하는 Property
        public bool DefaultThumbStatus
        {
            get { return (bool)GetValue(DefaultThumbStatusProperty); }
            set { SetValue(DefaultThumbStatusProperty, value); }
        }

        public static readonly DependencyProperty DefaultThumbStatusProperty =
            DependencyProperty.Register("DefaultThumbStatus", typeof(bool), typeof(ToggleSlider), new PropertyMetadata(false, DefaultThumbStatusChangedCallback));

        private static void DefaultThumbStatusChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var owner = d as ToggleSlider;
            if ((bool)e.NewValue)
                owner.ThumbValue = 1.0;
            else
                owner.ThumbValue = 0.0;
        }
        #endregion

        #region [ThumbValue]
        public double ThumbValue
        {
            get { return (double)GetValue(ThumbValueProperty); }
            set { SetValue(ThumbValueProperty, value); }
        }

        public static readonly DependencyProperty ThumbValueProperty =
            DependencyProperty.Register("ThumbValue", typeof(double), typeof(ToggleSlider), new PropertyMetadata(0.0));
        #endregion
        #endregion

        public ToggleSlider()
        {
            InitializeComponent();
        }

        #region [Event handlers]
        #region [Mouse left button]
        private void part_toggle_slider_PreviewMouseLeftButtonDownUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                ToggleSliderThumbMoving();// ToggleSliderThumbDownMove?.Invoke(this, e);
            else
                ToggleSliderThumbStop();// ToggleSliderThumbUp?.Invoke(this, e);
        }

        //private void part_toggle_slider_PreviewMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //        ToggleSliderThumbMoving();
        //}
        #endregion

        #region [Keyboard arrow keyUp]
        private void part_toggle_slider_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down)
                ToggleSliderThumbStop();
        }
        #endregion

        #region [Touch]
        private void part_toggle_slider_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            ToggleSliderThumbStop();// ToggleSliderThumbUp?.Invoke(this, e);
        }

        private void part_toggle_slider_PreviewTouchDownMove(object sender, TouchEventArgs e)
        {
            ToggleSliderThumbMoving();
        }
        #endregion

        #region [Stylus]
        private void part_toggle_slider_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            ToggleSliderThumbMoving();//  ToggleSliderThumbDownMove?.Invoke(this, e);
        }

        //private void part_toggle_slider_PreviewStylusMove(object sender, StylusEventArgs e)
        //{
        //    if (e.InAir == false)
        //        ToggleSliderThumbMoving();
        //}

        private void part_toggle_slider_PreviewStylusUp(object sender, StylusEventArgs e)
        {
            ToggleSliderThumbStop();// ToggleSliderThumbUp?.Invoke(this, e);
        }
        #endregion

        #region [Dragging]
        private void Part_toggle_slider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if ((e.OriginalSource as Thumb).IsDragging)
                ToggleSliderThumbMoving();// ToggleSliderThumbDownMove?.Invoke(this, e);
        }
        #endregion
        #endregion

        private void ToggleSliderThumbMoving() // Thumb을 누른 상태에서 호출되는 함수
        {
            double thumbValue = ThumbValue;
            if (_pressFlag == false)
            {
                if (thumbValue == 0)
                    IsToggleOn = false;
                else if (thumbValue == 1)
                    IsToggleOn = true;
                _pressFlag = true;
            }
            else
            {
                if (thumbValue == 0)
                    IsToggleOn = false;
                else if (thumbValue == 1)
                    IsToggleOn = true;
            }
        }

        private void ToggleSliderThumbStop() // Thumb의 움직임을 멈췄을 때 호출되는 함수
        {
            ThumbValue = ThumbValue < 0.5 ? 0 : 1;
            IsToggleOn = ThumbValue < 0.5 ? false : true;
            _pressFlag = false;
        }
    }
}
