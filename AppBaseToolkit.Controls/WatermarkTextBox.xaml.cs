using System.Windows;
using System.Windows.Input;
using AppBaseToolkit.Extensions;
using AppBaseToolkit.Mvvm;

namespace AppBaseToolkit.Controls
{
    /// <summary>
    /// TextBox-like control that displays hint (watermark) instead of text if text field is empty.
    /// When focused or text typed - watermark disappears.
    /// </summary>
    public partial class WatermarkTextBox
    {
        internal class Model : NotificationObject
        {
            #region WatermarkText property

            /// <summary>
            /// WatermarkText property
            /// </summary>
            public string? WatermarkText
            {
                get => _watermarkText;
                set
                {
                    if (SetProperty(ref _watermarkText, value))
                        WatermarkVisibility = Text.IsNullOrEmpty() ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            private string? _watermarkText;

            #endregion

            #region WatermarkVisibility property

            /// <summary>
            /// WatermarkVisibility property
            /// </summary>
            public Visibility WatermarkVisibility
            {
                get => _watermarkVisibility;
                set => SetProperty(ref _watermarkVisibility, value);
            }

            private Visibility _watermarkVisibility = Visibility.Collapsed;

            #endregion

            #region Text property

            /// <summary>
            /// Text property
            /// </summary>
            public string? Text
            {
                get => _text;
                set
                {
                    if (SetProperty(ref _text, value))
                        WatermarkVisibility = _text.IsNullOrEmpty() ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            private string? _text;

            #endregion

            public void UpdateWatermarkVisibility(bool focused)
            {
                if (focused)
                    WatermarkVisibility = Visibility.Collapsed;
                else if (Text.IsNullOrEmpty())
                    WatermarkVisibility = Visibility.Visible;
            }
        }

        private readonly Model _model = new ();

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="WatermarkTextBox"/>
        /// </summary>
        public WatermarkTextBox()
        {
            InitializeComponent();
            LayoutRoot.DataContext = _model;
            _model.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(Model.Text))
                    Text = _model.Text;
            };
        }

        #endregion

        #region WatermarkText dependency property

        /// <summary>
        /// registering <see cref="WatermarkText"/> dependency property
        /// </summary>
        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.Register(nameof(WatermarkText), typeof(string), typeof(WatermarkTextBox),
                new FrameworkPropertyMetadata(default(string?), WatermarkTextPropertyChanged));

        /// <summary>
        /// WatermarkText 
        /// This is dependency property
        /// </summary>
        public string? WatermarkText
        {
            get => (string?)GetValue(WatermarkTextProperty);
            set => SetValue(WatermarkTextProperty, value);
        }

        /// <summary>
        /// Handles changes of the <see cref="WatermarkTextProperty"/> dependency property.
        /// </summary>
        /// <param name="d">The currently processed owner of the property.</param>
        /// <param name="e">Provides information about the updated property.</param>
        private static void WatermarkTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var owner = (WatermarkTextBox)d;
            owner._model.WatermarkText = (string?)e.NewValue;
        }

        #endregion

        #region Text dependency property


        /// <summary>
        /// registering <see cref="Text"/> dependency property
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(WatermarkTextBox),
                new FrameworkPropertyMetadata(default(string?), TextPropertyChanged));

        /// <summary>
        /// Text 
        /// This is dependency property
        /// </summary>
        public string? Text
        {
            get => (string?)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Handles changes of the <see cref="TextProperty"/> dependency property.
        /// </summary>
        /// <param name="d">The currently processed owner of the property.</param>
        /// <param name="e">Provides information about the updated property.</param>
        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var owner = (WatermarkTextBox)d;
            owner._model.Text = (string?)e.NewValue;
        }

        #endregion

        #region TextBox focus events

        private void TextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            _model.UpdateWatermarkVisibility(true);
        }

        private void TextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _model.UpdateWatermarkVisibility(true);
        }

        private void TextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            _model.UpdateWatermarkVisibility(false);
        }

        private void TextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _model.UpdateWatermarkVisibility(false);
        }

        #endregion
    }
}
