using Swack.Logic;
using Swack.Logic.Models;
using Swack.UI.ViewModels;
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

namespace Swack.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var currentUser = new User
            {
                Username = "gedlbauer",
                ProfileUrl = "https://robohash.org/gedlbauer?size=150x150"
            };
            MainViewModel mainViewModel = new MainViewModel(new SimulatedMessagingLogic(currentUser));
            DataContext = mainViewModel;
            this.Loaded += async (s, e) => await mainViewModel.InitializeAsync();
        }
    }
}
