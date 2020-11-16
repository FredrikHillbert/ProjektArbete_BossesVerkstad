using GUI.Login;
using GUI.Mechanics.Home;
using GUI.Mechanics.User;
using Logic.Entities;
using Logic.Interface;
using Logic.Services;
using System;
using System.Collections.Generic;
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

namespace GUI.Mechanics.Workshop
{
    /// <summary>
    /// Interaction logic for CaseOptionMechanic.xaml
    /// </summary>
    public partial class CaseOptionMechanic : Page
    {
        Mechanic mechanic = new Mechanic();
        ILogicUser userService = new UserService();
        public CaseOptionMechanic()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePageMechanic homePageMechanic = new HomePageMechanic();
            this.NavigationService.Navigate(homePageMechanic);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            this.NavigationService.Navigate(profile);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //------Information om Pågående Arbeten, samt ändringar
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //------------Till ny sida, med information om alla avslutade arbeten?
        }
    }
}
