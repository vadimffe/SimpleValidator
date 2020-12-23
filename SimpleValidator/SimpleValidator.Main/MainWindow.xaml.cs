using SimpleValidator.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
            ViewModel = new ViewModel();
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
