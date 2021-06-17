namespace EdBindings
{
    using EdBindings.Model;
    using EdBindings.Model.BindingsRaw;

    using Newtonsoft.Json;

    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BindingFile BindingFile { get; set; }
        private DeviceMap DeviceMap { get; set; }
        private ObservableCollection<KeyBindingView> KeyBindings { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var deviceMappingFiles = Directory.GetFiles(@".\DeviceMappings");

            foreach(var deviceMappingFile in deviceMappingFiles)
            {

                var deviceMapping = JsonConvert.DeserializeObject<DeviceMap>(File.ReadAllText(deviceMappingFile));
                var menuItem = new MenuItem();
                menuItem.Header = deviceMapping.Name;
                menuItem.DataContext = deviceMapping;
                menuItem.Click += this.DeviceMapSelected;
                menuItem.IsCheckable = true;

                this.DeviceMappingMenu.Items.Add(menuItem);
            }
            this.SelectActiveDeviceMapping(ApplicationSettings.Default.DeviceMapSelection);
        }

        private void DeviceMapSelected(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.DeviceMappingMenu.Items.IndexOf(sender);
            this.SelectActiveDeviceMapping(selectedIndex);
        }

        private void SelectActiveDeviceMapping(int index)
        {
            var menuItem = (MenuItem)this.DeviceMappingMenu.Items[index];
            this.DeviceMap = (DeviceMap)menuItem.DataContext;
            foreach(var item in this.DeviceMappingMenu.Items)
            {
                ((MenuItem)item).IsChecked = false;
            }

            menuItem.IsChecked = true;
            if(index != ApplicationSettings.Default.DeviceMapSelection)
            {
                ApplicationSettings.Default.DeviceMapSelection = index;
                ApplicationSettings.Default.Save();
            }
            DeviceFileStatusBar.Content = $"Device Mapping: {menuItem.Header}";
            this.ProcessBindingFile();

        }

        private void FileExitMenuItemClick(object sender, RoutedEventArgs e) => this.Close();

        private void FileOpenBindingsMenuItemClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".binds";
            dialog.InitialDirectory = Environment.ExpandEnvironmentVariables(@"%localappdata%\Frontier Developments\Elite Dangerous\Options\Bindings");
            dialog.Filter = "Bindings (*.binds)|*.binds|All files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                this.BindingFile = BindingFile.Open(dialog.FileName);
                this.ProcessBindingFile();
            }
        }


        private void ProcessBindingFile()
        {
            if (this.BindingFile == null)
            {
                return;
            }

            var justBindingGroups = this.BindingFile.Bindings.Where(binding => binding is EdBindings.Model.BindingsRaw.Bindings.BindingGroup).ToList();

            this.KeyBindings = new ObservableCollection<KeyBindingView>(justBindingGroups.Select(group => KeyBindingView.MakeKeyBindingView((EdBindings.Model.BindingsRaw.Bindings.BindingGroup)group, this.DeviceMap)).ToList());

            this.KeyBindingDataGrid.ItemsSource = this.KeyBindings;
            this.BindingFileStatusBar.Content = $"Bindings: {this.BindingFile.FileName}";
            this.KeyboardLayoutStatusBar.Content = this.BindingFile.KeyboardLayout;
        }
    }
}
