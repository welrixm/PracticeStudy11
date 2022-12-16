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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {


        public ObservableCollection<Order> Orders
        {
            get { return (ObservableCollection<Order>)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(ObservableCollection<Order>), typeof(OrderPage));


        public OrderPage()
        {
            DBConnect.db.Order.Load();
            Orders = DBConnect.db.Order.Local;
            InitializeComponent();
        }

        //private void BtnEditOrder_Click(object sender, RoutedEventArgs e)
        //{
        //    var selOrder = (sender as Button).DataContext as Order;
        //    Navigation.NextPage(new Navig("Редактирование заказа", new OrderEditPage(selOrder)));
        //}
    }
}
