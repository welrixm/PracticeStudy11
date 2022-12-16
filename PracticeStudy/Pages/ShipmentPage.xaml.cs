using PracticeStudy.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace PracticeStudy.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShipmentPage.xaml
    /// </summary>
    public partial class ShipmentPage : Page
    {
        public ObservableCollection<Shipment> Shipments
        {
            get { return (ObservableCollection<Shipment>)GetValue(ShipmentsProperty); }
            set { SetValue(ShipmentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShipmentsProperty =
            DependencyProperty.Register("Orders", typeof(ObservableCollection<Shipment>), typeof(ShipmentPage));
        public ShipmentPage()
        {
            DBConnect.db.Shipment.Load();
            Shipments = DBConnect.db.Shipment.Local;
            InitializeComponent();
        }

        //private void BtnEditShip_Click(object sender, RoutedEventArgs e)
        //{
        //    var selShipment = (sender as Button).DataContext as Product;
        //    Navigation.NextPage(new Navig("Поставка", new EditShipPage(selShipment)));
        //}
    }
}
