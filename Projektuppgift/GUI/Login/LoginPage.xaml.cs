using GUI.Admin.Home;
using GUI.Mechanics.Home;
using GUI.Tools;
using Logic.DAL;
using Logic.Entities;
using Logic.Interface;
using Logic.MyExceptions;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GUI.Login
{
    /// <summary>
    /// Interaction logic for LogginPage.xaml
    /// </summary>
    public partial class LogginPage : Page
    {

     //-------------------------------------------------------------------------------Lägg till knapp för att avsluta
        public LogginPage()
        {
            InitializeComponent();
         
        }
        /// <summary>
        /// If Textbox got focus = Emty string
        /// </summary>
        private void LoginUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginUserName.Text == "Användarnamn") {LoginUserName.Text = string.Empty; }
        }

        /// <summary>
        /// If Passwordbox got focus = Emty string
        /// </summary>   
        private void LoginPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginPassword.Password == "Password"){LoginPassword.Password = string.Empty; }
        }

        /// <summary>
        /// If Username and password is correct, iuserLogin.Login(loggin, password)==True.
        /// Else Username and password is incorrect, messagebox shows
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var loggin = LoginUserName.Text.ToLower();
            var password = LoginPassword.Password;
            ILogic adminService = new AdminService();
            if (adminService.Login(loggin, password) && Isadmin.IsChecked == true)
            {
                HomePageAdmin homePageAdmin = new HomePageAdmin();
                this.NavigationService.Navigate(homePageAdmin);
            }
            else if (adminService.LoginUser(loggin, password))
            {
                ILogicUser userService = new UserService();//------------------Håller koll på vem som loggar in!
                userService.SetUser(loggin);
        
                HomePageMechanic homePageMechanic = new HomePageMechanic();
                this.NavigationService.Navigate(homePageMechanic);
            }
            else
            {
                MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

            try
            {
                JsonSetFile jsonSetFile = new JsonSetFile();
                jsonSetFile.SetJson();
            }
            catch (ErrorException)
            {
                MessageBox.Show("Filen kunde inte Sparas korrekt!" +
                    "\n" +
                    "\n Avsluta och starta om programet!");
            }
            Application.Current.Shutdown();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Isadmin.IsChecked= true;
        }
    }
}
