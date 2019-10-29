using Samsung.SmartSearchApp.SmartSearchCommon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Samsung.SmartSearchApp.View.Controls.TutorialPages
{
    /// <summary>
    /// Interaction logic for FileSearchGuide.xaml
    /// </summary>
    public partial class FileSearchGuide : UserControl
    {
        public FileSearchGuide()
        {
            InitializeComponent();
        }

        private const int ResultCount = 7;
        private const string Keyword = ConstValue.STR_TUTORIAL_KWD;

        public static DependencyProperty CustomStoryboardProperty = DependencyProperty.Register("CustomStoryboard", typeof(Storyboard), typeof(FileSearchGuide), new PropertyMetadata(null));
        public Storyboard CustomStoryboard
        {
            get { return (Storyboard)GetValue(CustomStoryboardProperty); }
            set { SetValue(CustomStoryboardProperty, value); }
        }

        public static DependencyProperty IsEnableAnimationProperty = DependencyProperty.Register("IsEnableAnimation", typeof(bool), typeof(FileSearchGuide), new PropertyMetadata(true));
        public bool IsEnableAnimation
        {
            get { return (bool)GetValue(IsEnableAnimationProperty); }
            set { SetValue(IsEnableAnimationProperty, value); }
        }

        private void GenerateAnimation()
        {
            // Create a storyboard to apply the animation.
            Storyboard tutorialStoryboard = new Storyboard();
            tutorialStoryboard.BeginTime = TimeSpan.FromMilliseconds(600);
            tutorialStoryboard.Duration = TimeSpan.FromMilliseconds(2500);

            DoubleAnimation objectAnimation = MakeOpacityAnimation("part_cursor_rect", 0, 1, 700);
            objectAnimation.RepeatBehavior = RepeatBehavior.Forever;
            tutorialStoryboard.Children.Add(objectAnimation);

            StringAnimationUsingKeyFrames typeAnimation = MakeTypewriteTextblock(Keyword, "part_typed_tbl");
            tutorialStoryboard.Children.Add(typeAnimation);

            DoubleAnimation guideTblAnimation = MakeOpacityAnimation("part_guide_tbl", 1, 0, 0);
            guideTblAnimation.BeginTime = TimeSpan.FromMilliseconds(700);
            tutorialStoryboard.Children.Add(guideTblAnimation);

            int delayMilliSec = 400;
            for (int i = 1; i <= ResultCount; i++)
            {
                DoubleAnimation objectAnimationForLvi = MakeOpacityAnimation("part_lvi_" + i, 0, 1, 200);
                objectAnimationForLvi.BeginTime = TimeSpan.FromMilliseconds(delayMilliSec + (100 * i));
                tutorialStoryboard.Children.Add(objectAnimationForLvi);
            }

            tutorialStoryboard.RepeatBehavior = RepeatBehavior.Forever;
            tutorialStoryboard.AutoReverse = true;
            CustomStoryboard = tutorialStoryboard;

            //// Start the storyboard after the rectangle loads.
            //rootGrid.Loaded += delegate (object sender, RoutedEventArgs e)
            //{
            //    translationStoryboard.Begin(this);
            //};
        }

        private DoubleAnimation MakeOpacityAnimation(string controlName, double from, double to, double duration)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = from;
            da.To = to;
            da.Duration = TimeSpan.FromMilliseconds(duration);

            PropertyPath opacityPath = new PropertyPath(OpacityProperty);
            // Set the animation to target the opacity property
            Storyboard.SetTargetProperty(da, opacityPath);
            Storyboard.SetTargetName(da, controlName);

            return da;
        }

        private StringAnimationUsingKeyFrames MakeTypewriteTextblock(string textToAnimate, string typeTextBlockName)
        {
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            stringAnimationUsingKeyFrames.FillBehavior = FillBehavior.HoldEnd;

            DiscreteStringKeyFrame discreteStringKeyFrame;
            string tmp = string.Empty;
            for (int i = 0; i <= textToAnimate.Length; i++)
            {
                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300 * i)); //KeyTime.Paced;
                discreteStringKeyFrame.Value = tmp;
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);

                if (i == textToAnimate.Length) break;
                tmp += textToAnimate[i];
            }

            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, typeTextBlockName);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath(TextBlock.TextProperty));

            return stringAnimationUsingKeyFrames;
        }

        private void Part_rootGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsEnableAnimation == false) return;
            if (e.NewValue is bool isVisible)
            {
                if (isVisible)
                {
                    if (CustomStoryboard == null)
                        GenerateAnimation();
                    CustomStoryboard.Begin();
                }
                else
                {
                    if (CustomStoryboard != null)
                    {
                        CustomStoryboard.Stop();
                        CustomStoryboard = null;
                    }
                }
            }
        }
    }
}
