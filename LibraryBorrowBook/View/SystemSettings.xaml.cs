using Library.Models;
using LibraryBorrowBook.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LibraryBorrowBook.View
{
    public partial class SystemSettings : Window
    {
        private readonly SystemConfigService _configService;
        private readonly User _currentUser;
        private List<SystemConfig> _configs;
        private Dictionary<SystemConfig, TextBox> _configTextBoxes;

        public SystemSettings(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _configService = new SystemConfigService();
            _configTextBoxes = new Dictionary<SystemConfig, TextBox>();
            LoadSettings();
        }

        private void LoadSettings()
        {
            _configs = _configService.GetAllConfigs();
            spSettings.Children.Clear();
            _configTextBoxes.Clear();

            foreach (var config in _configs)
            {
                StackPanel sp = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 0, 0, 15) };
                
                TextBlock tbKey = new TextBlock 
                { 
                    Text = config.ConfigKey, 
                    FontWeight = FontWeights.Bold, 
                    FontSize = 14, 
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333")) 
                };
                
                TextBlock tbDesc = new TextBlock 
                { 
                    Text = config.Description, 
                    FontSize = 12, 
                    Foreground = Brushes.Gray, 
                    Margin = new Thickness(0, 2, 0, 5),
                    TextWrapping = TextWrapping.Wrap
                };
                
                TextBox txtValue = new TextBox 
                { 
                    Text = config.ConfigValue, 
                    Height = 25, 
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(5,0,0,0)
                };

                _configTextBoxes.Add(config, txtValue);

                sp.Children.Add(tbKey);
                sp.Children.Add(tbDesc);
                sp.Children.Add(txtValue);

                spSettings.Children.Add(sp);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in _configTextBoxes)
                {
                    var config = item.Key;
                    var txtBox = item.Value;

                    if (config.ConfigValue != txtBox.Text)
                    {
                        config.ConfigValue = txtBox.Text;
                        _configService.UpdateConfig(config);
                    }
                }
                MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving settings: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(_currentUser);
            this.SwitchTo(main);
        }
    }
}

