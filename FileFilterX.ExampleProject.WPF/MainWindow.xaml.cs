using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FileFilterX.Library;
using Microsoft.Win32;

namespace FileFilterX.ExampleProject.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog(false);
        }

        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                NotifyChanged(nameof(FilePath));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog(true);
        }

        private void OpenFileDialog(bool useCommonFilters)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = useCommonFilters ?
                    Library.FileFilterX.FilterBuilder(true, new Filter(CommonFilters.JPG),
                                                            new Filter(CommonFilters.PNG))
                    : Library.FileFilterX.FilterBuilder(true, new Filter("JPG", "jpg", "jpeg"),
                                                                new Filter("PNG", "png")),
                Title = "Select a file",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (dialog.ShowDialog() is bool result && result)
            {
                FilePath = dialog.FileName;
            }
        }
    }
}
