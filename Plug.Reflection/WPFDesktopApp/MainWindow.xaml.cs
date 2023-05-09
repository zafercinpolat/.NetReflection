using SDK;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Plug> myList = new List<Plug>();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Form1_Load;
        }
     
        private void Form1_Load(object sender, RoutedEventArgs e)
        {
            myList = Kit.GetAllPlugins(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//plugins");
            foreach (Plug p in myList)
            {

                Button btnSubfolder = new Button();
                btnSubfolder.Content = p.pName;
                btnSubfolder.Margin = new Thickness(15, 15, 10, 0);
                btnSubfolder.Width = 200;
                btnSubfolder.Height = 150;
                btnSubfolder.Click += b_Click;
                btnSubfolder.HorizontalAlignment = HorizontalAlignment.Left;
                sp.Children.Add(btnSubfolder);
            }
        }
        private void b_Click(object sender, EventArgs e)
        {
            foreach (Plug p in myList)
            {
                if (p.pName == (sender as Button).Content.ToString())
                {
                    Run(p);
                }
            }
        }

        private void Run(Plug p)
        {
            IPlugin obj = (IPlugin)Kit.CreateObject(p);
            textBox1.Text = obj.action(textBox1.Text);
        }

    }
}
