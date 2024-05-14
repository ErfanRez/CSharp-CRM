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
using System.Windows.Forms;
using System.Windows.Media.Effects;
using CRMPracticeProject.Forms;
using BE;
using BLL;

namespace CRMPracticeProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void OpenWinForm(Form f)
        {
            Window g = this.FindName("Main") as Window;
            BlurBitmapEffect blurBitmapEffect = new BlurBitmapEffect();
            blurBitmapEffect.Radius = 20;

            g.BitmapEffect = blurBitmapEffect;
            
                f.ShowDialog();

            blurBitmapEffect.Radius = 0;
            g.BitmapEffect = blurBitmapEffect;
        }

        public void RefreshPage()
        {
            UserNameTxt.Text = LoggedInUser.Username;
            PersonNameTxt.Text = LoggedInUser.Name;
            ReminderCountTxt.Text = dashBoard.UserReminderCount(LoggedInUser);
            SellsCountTxt.Text = dashBoard.SellsCount();
            CustomerCountTxt.Text = dashBoard.CustomerCount();
            int a = 0;
            foreach (var item in dashBoard.GetReminders(LoggedInUser))
            {
                if (a < 7)
                {
                    ReminderUC ru = new ReminderUC();
                    ru.ReminderTitleTxt.Text = item.Title;
                    ru.ReminderInfoTxt.Text = item.ReminderInfo;
                    Grid.SetRow(ru, 5 + a);
                    Grid.SetColumn(ru, 6);
                    Grid.SetColumnSpan(ru, 6);
                    MainGrid.Children.Add(ru);
                    a++;
                }
                
            }
            
        }

        public User LoggedInUser = new User();
        MsBoxClass ms = new MsBoxClass();
        UserBLL ubll = new UserBLL();
        DashBoardBLL dashBoard = new DashBoardBLL();
        

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult res = ms.MsShow("Exit", "Do you wish to exit the app?", "", true, false);
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }     
        }

        private void WrapPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(LoggedInUser,"Customers Part",1))
            {
                CustomerForm f = new CustomerForm();

                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to enter this section", "", false, true);
            }
            
        }

        private void WrapPanel_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(LoggedInUser, "Products Part", 1))
            {
                ProductForm f = new ProductForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to enter this section", "", false, true);
            }
            
        }

        private void WrapPanel_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(LoggedInUser, "Invoices Part", 1))
            {
                InvoiceForm f = new InvoiceForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to enter this section", "", false, true);
            }
            
        }

        private void WrapPanel_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(LoggedInUser,"Activities Part", 1))
            {
                ActivityForm f = new ActivityForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to enter this section", "", false, true);
            }
            
        }

        private void WrapPanel_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(LoggedInUser, "Reminders Part", 1))
            {
                ReminderForm f = new ReminderForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to enter this section", "", false, true);
            }
            
        }

        private void WrapPanel_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(LoggedInUser, "Sms Part", 1))
            {
                SmsForm f = new SmsForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to enter this section", "", false, true);
            }
            
        }

        private void WrapPanel_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(LoggedInUser,"Users Part", 1))
            {
                UserForm f = new UserForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to enter this section", "", false, true);
            }
            
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            MsBoxClass m = new MsBoxClass();
            m.MsShow("Registration Is Done", "User's info is registerd", "", false, true);
        }

        private void WrapPanel_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(LoggedInUser, "Settings", 1))
            {
                ActivityCategoryForm f = new ActivityCategoryForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to enter this section", "", false, true);
            }
            
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            LoginForm f = new LoginForm();
            OpenWinForm(f);
            RefreshPage();
        }

        private void WrapPanel_MouseLeftButtonDown_9(object sender, MouseButtonEventArgs e)
        {
            if (ubll.Access(LoggedInUser, "Reports Part", 1))
            {
                ReportForm f = new ReportForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                ms.MsShow("Access Denied", "You don't have premission to enter this section", "", false, true);
            }
            
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            DialogResult rs = ms.MsShow("Logout", "Do you wish to logout of your account?", "", true, false);
            if (rs == System.Windows.Forms.DialogResult.Yes)
            {
                LoginForm f = new LoginForm();
                OpenWinForm(f);
                RefreshPage();
            }
        }
    }
}
