using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using DevExpress.Xpf.NavBar;
using System.Windows.Media.Imaging;

namespace NavBarDataBinding {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
        }

        NavigationPaneView navigationPaneView;

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            navigationPaneView = new NavigationPaneView();
            navBar.View = navigationPaneView;

            navigationPaneView.ItemAdding += OnItemAdding;
            navigationPaneView.GroupAdding += OnGroupAdding;
            navigationPaneView.ItemSelected += OnItemSelected;

            navBar.GroupDescription = "Country";
            navBar.ItemsSource = EmployeesData.DataSource;


            Style style = (Style)Resources["EmployeeItemVisualStyle"];
            navigationPaneView.ItemVisualStyle = style;
        }

        void OnGroupAdding(object sender, GroupAddingEventArgs e) {
            if (e.Group == null || e.Group.DataContext == null)
                return;
            e.Group.Header = e.Group.DataContext.ToString();
        }

        void OnItemAdding(object sender, ItemAddingEventArgs e) {
            if (e.Item == null || e.Item.DataContext == null)
                return;
            Image image = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream stream = new MemoryStream(((Employee)e.Item.DataContext).Photo);
            bitmapImage.SetSource(stream);
            e.Item.ImageSource = bitmapImage;

            e.Item.Content = ((Employee)e.Item.DataContext).FirstName;
        }

        void OnItemSelected(object sender, NavBarItemSelectedEventArgs e) {
            //...
        }
    }


    [XmlRoot("Employees")]
    public class EmployeesData : List<Employee> {
        public static Stream GetDataStreamFromResources(string filename) {
            return typeof(EmployeesData).Assembly.GetManifestResourceStream("NavBarDataBinding." + filename);
        }
        public static IList DataSource {
            get {
                XmlSerializer s = new XmlSerializer(typeof(EmployeesData));
                return (IList)s.Deserialize(GetDataStreamFromResources("Employees.xml"));
            }
        }
    }


    public class Employee {
        int employeeID;
        public int EmployeeID {
            get { return employeeID; }
            set {
                if (employeeID == value)
                    return;
                employeeID = value;
            }
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public double Salary { get; set; }
        public bool OnVacation { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int ReportsTo { get; set; }
    }
}
