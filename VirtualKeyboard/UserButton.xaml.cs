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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserInterface
{
    /// <summary>
    /// UserButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserButton : Button
    {
        #region CornerRadius

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                typeof(CornerRadius),
                typeof(UserButton),
                new PropertyMetadata(new CornerRadius(0)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register("Checked",
                typeof(bool),
                typeof(UserButton),
                new PropertyMetadata(false));

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        #endregion

        #region MouseOverBackgroundBrush

        public static readonly DependencyProperty MouseOverBackgroundBrushProperty =
            DependencyProperty.Register("MouseOverBackgroundBrush",
                typeof(Brush),
                typeof(UserButton));

        public Brush MouseOverBackgroundBrush
        {
            get { return (Brush)GetValue(MouseOverBackgroundBrushProperty); }
            set { SetValue(MouseOverBackgroundBrushProperty, value); }
        }

        #endregion

        #region MouseOverBorderBrush

        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.Register("MouseOverBorderBrush",
                typeof(Brush),
                typeof(UserButton));

        public Brush MouseOverBorderBrush
        {
            get { return (Brush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        #endregion

        #region PressedBackgroundBrush

        public static readonly DependencyProperty PressedBackgroundBrushProperty =
            DependencyProperty.Register("PressedBackgroundBrush",
                typeof(Brush),
                typeof(UserButton));

        public Brush PressedBackgroundBrush
        {
            get { return (Brush)GetValue(PressedBackgroundBrushProperty); }
            set { SetValue(PressedBackgroundBrushProperty, value); }
        }

        #endregion

        #region PressedBorderBrush

        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register("PressedBorderBrush",
                typeof(Brush),
                typeof(UserButton));

        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }

        #endregion



        public UserButton()
        {
            InitializeComponent();
        }
    }
}
