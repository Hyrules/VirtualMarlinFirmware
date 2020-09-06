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
using System.IO.Ports;
using System.Threading;
using System.Windows.Threading;
using VirtualRepRapFirmware.MainForm;

namespace VirtualRepRapFirmware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainFormViewModel _mainFormViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainFormViewModel = new MainFormViewModel();

        }

        private void tbTerminal_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTerminal.ScrollToEnd();
        }
    }
}
