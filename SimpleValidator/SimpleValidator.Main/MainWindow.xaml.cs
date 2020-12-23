using System.Windows;
using SimpleValidator.ViewModels;

namespace SimpleValidator.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel ViewModel { get; set; }

        public MainWindow()
        {
            this.ViewModel = new ViewModel();
            this.DataContext = this.ViewModel;
            InitializeComponent();
        }
    }
}
