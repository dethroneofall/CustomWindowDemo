using CustomWindowDemo.HelperClass;
using CustomWindowDemo.ViewModel;
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

namespace CustomWindowDemo.View
{
    public partial class MainWindow : MyCustomControls.CustomWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            text.Visibility = Visibility.Collapsed;
            string name = ((string)((sender as Button).Content)).Replace(" ", "");
            if (name.Contains("Task5"))
                DataContext = new TaskFiveViewModel(new DialogService(), new HelixtoolKitService());
            else if (name.Contains("Task3"))
                DataContext = new TaskThreeViewModel(new DialogService(), new HelixtoolKitService(), false);
            else
                DataContext = new TaskThreeViewModel(new DialogService(), new HelixtoolKitService(), true);

            frame.Source = new Uri(name + ".xaml", UriKind.RelativeOrAbsolute);  
        }
    }
}
