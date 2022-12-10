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
using System.Windows.Threading;
using PracticeStudy.Components;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace PracticeStudy.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        int actualPage = 0;
        
        public ProductPage()
        {
            DBConnect.db.Product.Load();
            Products = DBConnect.db.Product.Local;
            InitializeComponent();
            //ListProduct.Items.Clear();
            if (Navigation.AuthUser.RoleId == 2)
                AddNewProductBtn.Visibility = Visibility.Collapsed;
            else if (Navigation.AuthUser.RoleId == 4)
                OrdersBtn.Visibility = Visibility.Collapsed;
            //DispatcherTimer timer = new DispatcherTimer();


            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += UpdateData;
            //timer.Start();
            ListProduct.ItemsSource = DBConnect.db.Product.Where(x => x.IsActive != false).ToList();
            GeneralCount.Text = DBConnect.db.Product.Where(x => x.IsActive != false).Count().ToString();
        }
        //public void UpdateData(object sender, object e)
        //{
        //    var HistoryProduct = DBConnect.db.Product.ToList();
        //    ListProduct.ItemsSource = HistoryProduct;
        //    ListProduct.ItemsSource = DBConnect.db.Product.Where(x => x.Name.StartsWith(TxtSearch.Text) || x.Description.StartsWith(TxtSearch.Text)).ToList();
        //}
        public ObservableCollection<Product> Products
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register("Products", typeof(ObservableCollection<Product>), typeof(ProductPage));
        private void Refresh()
        {
            IEnumerable<Product> prodL = DBConnect.db.Product;
            ObservableCollection<Product> products = Products;
            {
                if (CbSort == null)
                return;
           
            if (CbSort.SelectedItem != null)
            {
                switch ((CbSort.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        //products = DBConnect.db.Product.Local;
                        prodL = DBConnect.db.Product;
                            break;
                    case "2":
                            //products = new ObservableCollection<Product>(Products.OrderBy(x => x.Name));
                            prodL = prodL.OrderBy(x => x.Name);
                            break;
                    case "3":
                            //products = new ObservableCollection<Product>(Products.OrderByDescending(x => x.Name));
                            prodL =prodL.OrderByDescending(x => x.Name);

                            break;
                    case "4":
                            //products = new ObservableCollection<Product>(Products.OrderBy(x => x.DateOfAddition));
                            prodL = prodL.OrderBy(x => x.DateOfAddition);
                            break;
                    case "5":
                        //products = new ObservableCollection<Product>(Products.OrderByDescending(x => x.DateOfAddition));
                            prodL = prodL.OrderByDescending(x => x.DateOfAddition);
                            break;

                }

            }
           // ListProduct.ItemsSource = products.ToList();

            if (TxtSearch == null)
                return;
            if (TxtSearch.Text.Length > 0)
            {
                    //products = new ObservableCollection<Product>(Products.Where(x => x.Name.ToLower().StartsWith(TxtSearch.Text.ToLower()) || x.Description.ToLower().StartsWith(TxtSearch.Text.ToLower())));
                    prodL = prodL.Where(x => x.Name.StartsWith(TxtSearch.Text) || x.Description.StartsWith(TxtSearch.Text));
            }
            //ListProduct.ItemsSource = products.ToList();

            if (CbFiltration == null)
                return;
            if(CbFiltration.SelectedItem != null)
            {
                switch((CbFiltration.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        //products = DBConnect.db.Product.Local;
                            prodL = DBConnect.db.Product;
                            break;
                    case "2":
                            //products = new ObservableCollection<Product>(Products.Where(x => x.UnitId == 2));
                            prodL = prodL.Where(x => x.UnitId == 2);
                            break;
                    case "3":
                            //products = new ObservableCollection<Product>(Products.Where(x => x.UnitId == 1));
                            prodL = prodL.Where(x => x.UnitId == 1);
                            break;
                }
            }
               // ListProduct.ItemsSource = products.ToList();
                if (CbCount.SelectedIndex > 0 && products.Count() > 0)
                {
                    int selCount = Convert.ToInt32((CbCount.SelectedItem as ComboBoxItem).Content);
                    products = new ObservableCollection<Product>(Products.Skip(selCount * actualPage).Take(selCount));
                    if (products.Count() == 0)
                    {
                        actualPage--;
                       
                    }
                }
                
                FoundCount.Text = products.Count().ToString() + " из ";
            }
            //ListProduct.ItemsSource = products.ToList();
            ListProduct.ItemsSource = prodL.ToList();

        }
        
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
            Navigation.NextPage(new Navig("Редактирование продукции", new EditPage(selProduct)));
        }

        

        

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void CbCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Refresh();
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage--;
            if (actualPage < 0)
                actualPage = 0;
            Refresh();
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage++;
            Refresh();
        }

        private void AddNewProductBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Navig("Добавление продукции", new EditPage(new Product())));
        }

        private void TxtSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void CbFiltration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Navig("Список заказов продукции", new OrderPage()));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
            if(MessageBox.Show("Вы точно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                selProduct.IsActive = false;
                MessageBox.Show("Запись удалена");
                DBConnect.db.SaveChanges();
                ListProduct.ItemsSource = DBConnect.db.Product.ToList();
            }
        }

        private void DecBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Navig("Оформление заказа", new DecorationOrderPage()));
        }
    }
}
