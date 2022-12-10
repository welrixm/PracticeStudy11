using PracticeStudy.Components;
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
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace PracticeStudy.Pages
{
    /// <summary>
    /// Логика взаимодействия для DecorationOrderPage.xaml
    /// </summary>
    public partial class DecorationOrderPage : Page
    {
        public ObservableCollection<Product> Products
        {
            get { return (ObservableCollection<Product>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("Products", typeof(ObservableCollection<Product>), typeof(OrderPage));

        public ObservableCollection<Product> ProductsAdd
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsAddOrderProperty); }
            set { SetValue(ProductsAddOrderProperty, value); }
        }

        public static readonly DependencyProperty ProductsAddOrderProperty =
            DependencyProperty.Register("ProductsAdd", typeof(ObservableCollection<Product>), typeof(OrderPage));
        public DecorationOrderPage()
        {

            DBConnect.db.Product.Load();
            Products = new ObservableCollection<Product>(DBConnect.db.Product.Local);
            ProductsAdd = new ObservableCollection<Product>();

            InitializeComponent();
        }

        private void ButtonAddProductOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem == null)
                return;

            Product product = ProductList.SelectedItem as Product;

            Products.Remove(product);
            ProductsAdd.Add(product);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            decimal totalCost = 0;

            foreach (var item in ProductsAdd)
                totalCost += (decimal)item.Cost;

            Order order = new Order()
            {
                UserId = 4,
                User1Id = Navigation.AuhtUser.Id,
                ExecutionStageId = 1,

                TotalCost = (int?)totalCost
            };

            DBConnect.db.Order.Add(order);

            foreach (var item in ProductsAdd)
            {
                ProductOrder product = new ProductOrder()
                {
                    OrderId = order.Id,
                    Product = item,
                    Quantity = item.Cost
                };

                DBConnect.db.ProductOrder.Add(product);
            }

            DBConnect.db.SaveChanges();
            Navigation.NextPage(new Navig("", new OrderPage()));
        }
    }
}
