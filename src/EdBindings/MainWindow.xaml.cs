namespace EdBindings
{
    using EdBindings.Model;
    using EdBindings.Model.BindingsRaw;

    using Newtonsoft.Json;

    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string placeHolderText = "Filter...";

        private BindingFile BindingFile { get; set; }

        private DeviceMap DeviceMap { get; set; }

        private CollectionViewSource DataSource { get; set; }

        private ICollectionView KeyBindings { get; set; }

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
            var dataSource = justBindingGroups.Select(group => KeyBindingView.MakeKeyBindingView((EdBindings.Model.BindingsRaw.Bindings.BindingGroup)group, this.DeviceMap)).ToList();
            var filterable = new CollectionViewSource() { Source = new ObservableCollection<KeyBindingView>(dataSource) };
            this.KeyBindings = filterable.View;

            this.KeyBindingDataGrid.ItemsSource = this.KeyBindings;
            this.BindingFileStatusBar.Content = Path.GetFileName(this.BindingFile.FileName);
            this.KeyboardLayoutStatusBar.Content = this.BindingFile.KeyboardLayout;
            this.txtFilter.Text = placeHolderText;
        }

        private void TxtFilterKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var p = new Predicate<object>(item =>
            {
                var binding = (KeyBindingView)item;
                return binding.Action.Contains(this.txtFilter.Text, StringComparison.InvariantCultureIgnoreCase) 
                || binding.PrimaryKey.Contains(this.txtFilter.Text, StringComparison.InvariantCultureIgnoreCase)
                || (binding.SecondaryKey?.Contains(this.txtFilter.Text, StringComparison.InvariantCultureIgnoreCase) ?? false);
            });

            if(string.IsNullOrWhiteSpace(this.txtFilter.Text))
            {
                this.KeyBindings.Filter = null;
            }
            else
            {
                this.KeyBindings.Filter = p;
            }
        }

        private void TxtFilterGotFocus(object sender, RoutedEventArgs e)
        {
            if (this.txtFilter.Text == placeHolderText)
            {
                this.txtFilter.Text = string.Empty;
            }

        }

        private void TxtFilterLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtFilter.Text))
            {
                this.txtFilter.Text = placeHolderText;
            }

        }

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AboutWindow();
            dialog.Owner = this;
            dialog.ShowDialog();
        }
    }
}
