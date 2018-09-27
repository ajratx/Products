namespace Products.Client.ProductsBrowser.UserControls
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class DataGridUserControl : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(DataGridUserControl), new PropertyMetadata(null));

        public DataGridUserControl()
        {
            InitializeComponent();
        }

        public object ItemsSource
        {
            get => (object)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
    }
}
