using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PracticeStudy.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        public AuthPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Login != null)
                LoginTb.Text = Properties.Settings.Default.Login;
            if (Properties.Settings.Default.Password != null)
                PasswordTb.Password = Properties.Settings.Default.Password;

        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            int TimeAuth = Properties.Settings.Default.TimeAuth;
            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Password.Trim();
            if(TimeAuth < 3)
            {
                if (login.Length == 0 && password.Length == 0)
                {
                    MessageBox.Show("Пусто! Пожалуйста заполните поля.");
                }
                else
                {
                    Navigation.AuthUser = DBConnect.db.User.ToList().Find(x => x.Login == login && x.Password == password);
                    if (Navigation.AuthUser == null)
                    {
                        MessageBox.Show("Извините, такого пользователя не существует!");
                        TimeAuth += 1;
                        Properties.Settings.Default.TimeAuth = TimeAuth;
                    }
                    else
                    {
                        if (SaveCb.IsChecked == true)
                        {
                            Properties.Settings.Default.Login = LoginTb.Text;
                            Properties.Settings.Default.Password = PasswordTb.Password;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.Login = null;
                            Properties.Settings.Default.Password = null;
                            Properties.Settings.Default.Save();
                        }
                        TimeAuth = 0;
                        Navigation.isAuth = true;


                    }
                }
            }
            else
            {
                MessageBox.Show("Пароль введен неправильно. Вы заблокированы в течение 1 минуты!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                TimeAuth = 0;
                AuthBtn.IsEnabled = false;
                RegistBtn.IsEnabled = false;
                timer.Interval = new TimeSpan(0, 0, 60);
                timer.Tick += new EventHandler(IsVisibleBtn);
                timer.Start();
            }
            
            //if(LoginTb.Text != "123" && LoginTb.Enabled)
            //{
            //    K++;
            //    if(K > 3)
            //    {
            //        ed = DateTime.Now.AddSeconds((K + 1)60);

            //    }
            //}
            Navigation.NextPage(new Navig("", new ProductPage()));

        }
        //int K = 0;
        //DateTime ed;

        private void IsVisibleBtn(object sender, EventArgs e)
        {
            AuthBtn.IsEnabled = true;
            RegistBtn.IsEnabled = true;
            timer.Stop();
        }
        private void RegistBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Navig("Регистрация", new RegistrationPage()));
            
        }
        //public class LoginTb : TextBox
        //{
        //    public int ErrorInputCount { get; set; }
        //}
        //private void LoginTb_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var tb = sender as LoginTb; 
        //    if(tb.Text != "123")
        //    {
        //        tb.IsEnabled = false;
        //        tb.ErrorInputCount++;
        //        switch (tb.ErrorInputCount)
        //        {
        //            case 1:
        //                break;
        //            case 2:
        //                await Task.Delay(1000);
        //        break;;

        //        }
        //    }
        //}
    }

}
