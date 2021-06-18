using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EdBindings
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public string VersionString { get; set; }
        public AboutWindow()
        {
            InitializeComponent();
            SetVersion();
            this.DataContext = this;
        }

        private void SetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var versionInfo = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            this.VersionString = $"v{versionInfo.InformationalVersion}";
        }
        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HyperlinkRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}
