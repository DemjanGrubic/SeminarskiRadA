using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace TotalCommander
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int numberOfDirectoryViews = 2;
        private static DirectoryList[] directoryViews;

        internal static ListView listViewFocused;
        internal static DirectoryList directoryListFocused;
        internal static DirectoryList directoryListNotFocused;

        private static KeyPressedHandler keyPressedHandler;

        public MainWindow()
        {
            InitializeComponent();

            directoryViews = new DirectoryList[numberOfDirectoryViews];
            for (int i = 0; i < directoryViews.Length; ++i)
            {
                directoryViews[i] = new DirectoryList();
            }

            // binding
            listViewLeft.DataContext = directoryViews[0].ShownEntriesName;
            listViewRight.DataContext = directoryViews[1].ShownEntriesName;

            labelDirectoryLeft.DataContext = directoryViews[0];
            labelDirectoryRight.DataContext = directoryViews[1];
            
            // focus and default select
            listViewLeft.GotFocus += new RoutedEventHandler(ListBoxFocusChangeHandler);
            listViewRight.GotFocus += new RoutedEventHandler(ListBoxFocusChangeHandler);
            listViewLeft.Focus();

            // key pressed handles
            keyPressedHandler = new KeyPressedHandler(this);

            var driveList = DriveInfo.GetDrives();
            comboBoxLeft.ItemsSource = driveList;
            comboBoxRight.ItemsSource = driveList;

            comboBoxLeft.SelectionChanged += new SelectionChangedEventHandler(leftComboBoxSelectionChanged);
            comboBoxRight.SelectionChanged += new SelectionChangedEventHandler(rightComboBoxSelectionChanged);
        }

        private void rightComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            directoryViews[1].UpdateDirectoryEntries(e.AddedItems[0].ToString());
        }

        private void leftComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            directoryViews[0].UpdateDirectoryEntries(e.AddedItems[0].ToString());
        }

        private void ListBoxFocusChangeHandler(object sender, RoutedEventArgs e)
        {
            listViewFocused = (ListView)sender;
            if (listViewFocused.SelectedIndex == -1) listViewFocused.SelectedIndex = 0;
            listViewFocused.ScrollIntoView(listViewFocused.SelectedItem);

            if (listViewFocused == listViewLeft)
            {
                directoryListFocused = directoryViews[0];
                directoryListNotFocused = directoryViews[1];
            }
            else
            {
                directoryListFocused = directoryViews[1];
                directoryListNotFocused = directoryViews[0];
            }
        }

        internal static List<FileSystemInfo> GetSelectedItems()
        {
            List<FileSystemInfo> result = new List<FileSystemInfo>();

            foreach (string selectedItem in listViewFocused.SelectedItems)
            {
                FileSystemInfo element = directoryListFocused.FindFileSystemInfoWithName(selectedItem);
                if (element != null)
                {
                    result.Add(element);
                }
            }

            return result;
        }
    }
}
