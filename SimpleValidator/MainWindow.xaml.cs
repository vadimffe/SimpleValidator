using SimpleValidator.ViewModels;
using System.Windows;

namespace SimpleValidator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel ViewModel { get; set; }

        public MainWindow()
        {
            DataContext = new ViewModel();
            InitializeComponent();
        }
    }
}
