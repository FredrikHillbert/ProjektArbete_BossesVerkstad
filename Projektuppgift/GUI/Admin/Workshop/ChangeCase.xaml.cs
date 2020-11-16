using System;
using GUI.Admin.Home;
using GUI.Admin.User;
using GUI.Login;
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
using GUI.Admin.Employer;
using Logic.Interface;
using Logic.Services;
using Logic.Entities;
using GUI.Tools;

namespace GUI.Admin.Workshop
{
    /// <summary>
    /// Interaction logic for ChangeCase.xaml
    /// </summary>
    public partial class ChangeCase : Page
    {
        Orders order = new Orders();
        List<string> listOfVehicles = new List<string>();
        List<string> listOfMechanics = new List<string>();
        public ChangeCase()
        {
            InitializeComponent();

        }
        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }
        private void Button_Start(object sender, RoutedEventArgs e)
        {
            HomePageAdmin homePageAdmin = new HomePageAdmin();
            this.NavigationService.Navigate(homePageAdmin);
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            AddCase addCase = new AddCase();
            this.NavigationService.Navigate(addCase);
        }
        private void Button_Workshop(object sender, RoutedEventArgs e) //Till CaseOptions (om man vill rensa)
        {
            CaseOptions caseOptions = new CaseOptions();
            this.NavigationService.Navigate(caseOptions);
        }

        private void Button_List(object sender, RoutedEventArgs e)
        {
            EmployerOptions employerOptions = new EmployerOptions();
            this.NavigationService.Navigate(employerOptions);
        }

        private void Button_Users(object sender, RoutedEventArgs e) //Dras til UserOptions. Kallas CaseOption..
        {
            CaseOption userOptions = new CaseOption();
            this.NavigationService.Navigate(userOptions);
        }

        private void Button_ClickSearchOrder(object sender, RoutedEventArgs e)
        {
            ILogic adminService = new AdminService();

            if ((adminService.ActivOrder(OrderIdSearch.Text))) 
            {
                List<Orders> listOfSpecificOrder = adminService.GetOrder(OrderIdSearch.Text);
                foreach (var item in listOfSpecificOrder)
                {
                    orderID.Text = OrderIdSearch.Text;
                    orderDesc.Text = item.OrderDescription;
                    ModelName.Text = item.ModelName;
                    RegNum.Text = item.RegNumber;
                    dateOfReg.Text = item.RegDate;
                    matare.Text = item.Matare;
                    specificQ.Text = item.SpecificQuestionAboutVehicle1;
                    specificQ2.Text = item.SpecificQuestionAboutVehicle2;
                    Motor.IsChecked = item.Engine;
                    Däck.IsChecked = item.Tire;
                    Vindrutor.IsChecked = item.BrokeWindow;
                    Bromsar.IsChecked = item.Brakes;
                    Kaross.IsChecked = item.Kaross;
                    cboType.SelectedItem = item.TypeOfVehicle;
                    statusYes.IsChecked = item.Status;
                    cbxMechanic.SelectedItem = item.Mechanic;
                    if (item.Status == false)
                    {
                        statusNo.IsChecked = true;
                    }

                    if (item.Fuel == "El")
                    {
                        el.IsChecked = true;
                    }
                    else if (item.Fuel == "Bensin")
                    {
                        gasoline.IsChecked = true;
                    }
                    else if (item.Fuel == "Etanol")
                    {
                        etanol.IsChecked = true;
                    }
                    else if (item.Fuel == "Diesel")
                    {
                        diesel.IsChecked = true;
                    }

                }
            }
            else
            {
                MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }


            cboType.Items.Refresh();
        }

        private void Bromsar_Checked(object sender, RoutedEventArgs e)
        {
            order.Brakes = true;
            string value = "Brakes";
            RefreshMethod(value);
        }

        private void Däck_Checked(object sender, RoutedEventArgs e)
        {
            order.Tire = true;
            string value = "Tire";
            RefreshMethod(value);
        }

        private void Vindruta_Checked(object sender, RoutedEventArgs e)
        {
            order.BrokeWindow = true;
            string value = "Window";
            RefreshMethod(value);
        }

        private void Motor_Checked(object sender, RoutedEventArgs e)
        {
            order.Engine = true;
            string value = "Engine";
            RefreshMethod(value);
        }

        private void Kaross_Checked(object sender, RoutedEventArgs e)
        {
            order.Kaross = true;
            string value = "Kaross";
            RefreshMethod(value);
        }


        private void ComboBoxVehicle_Loaded(object sender, RoutedEventArgs e)
        {

            ILogic adminService = new AdminService();
            listOfVehicles = adminService.GetVehicles();
            var combo = sender as ComboBox;
            combo.ItemsSource = listOfVehicles;
            if (order.TypeOfVehicle == "Bil")
                combo.SelectedIndex = 0;
            else if (order.TypeOfVehicle == "Buss")
                combo.SelectedIndex = 1;
            else if (order.TypeOfVehicle == "Lastbil")
                combo.SelectedIndex = 2;
            else if (order.TypeOfVehicle == "Motorcykel")
                combo.SelectedIndex = 3;
            cboType.ItemsSource = listOfVehicles;
            cbxMechanic.Items.Refresh();
        }



        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string whatTypeOfVehicle = order.TypeOfVehicle;



        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {

            List<string> orderLista = new List<string>();
            ILogic adminService = new AdminService();
            orderLista = adminService.GetKeyForOrder();
            var combo = sender as ComboBox;
            combo.ItemsSource = orderLista;
            combo.SelectedIndex = 0;




        }

        private void RefreshMethod(string value)
        {
            ILogic adminService = new AdminService();
            listOfMechanics = adminService.GetMechanicForTheJob(value);
            if (listOfMechanics.Count == 0)
            {
                listOfMechanics.Add("Finns inga mekaniker för jobbet.");

            }
            cbxMechanic.ItemsSource = listOfMechanics;
            cbxMechanic.Items.Refresh();
        }

        private void cbxMechanic_Loaded(object sender, RoutedEventArgs e)
        {
            cbxMechanic.ItemsSource = listOfMechanics;
            cbxMechanic.SelectedItem = order.Mechanic;

     
        }
    }
}