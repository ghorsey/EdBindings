namespace EdBindings
{
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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string bindingsFile;

        public MainWindow() => InitializeComponent();

        private void FileExitMenuItemClick(object sender, RoutedEventArgs e) => this.Close();

        private void FileOpenBindingsMenuItemClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".binds";
            dialog.InitialDirectory = Environment.ExpandEnvironmentVariables(@"%localappdata%\Frontier Developments\Elite Dangerous\Options\Bindings");
            dialog.Filter = "Bindings (*.binds)|*.binds|All files (*.*)|*.*";

            if(dialog.ShowDialog() == true)
            {
                this.bindingsFile = dialog.FileName;
            }
        }
    }
}
