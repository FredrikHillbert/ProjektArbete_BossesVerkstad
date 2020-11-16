using GUI.Admin.Home;
using GUI.Admin.User;
using GUI.Admin.Workshop;
using GUI.Login;
using GUI.Tools;
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

namespace GUI.Admin.Employer
{
    /// <summary>
    /// Interaction logic for ChangeEmployer.xaml
    /// </summary>
    public partial class ChangeEmployer : Page
    {

        Mechanic mechanic = new Mechanic();
        string whichMechanic;

        public ChangeEmployer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePageAdmin homePageAdmin = new HomePageAdmin();
            this.NavigationService.Navigate(homePageAdmin);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //--------------------------------------------------------------Nyheter För framtid
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CaseOptions caseOptions = new CaseOptions();
            this.NavigationService.Navigate(caseOptions);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            EmployerOptions employerOptions = new EmployerOptions();
            this.NavigationService.Navigate(employerOptions);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            CaseOption userOptions = new CaseOption();
            this.NavigationService.Navigate(userOptions);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            AddEmployer addEmployer = new AddEmployer();
            this.NavigationService.Navigate(addEmployer);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

            ILogic adminService = new AdminService();

            if ((adminService.ActivUser(employerIdSearch.Text)))
            {
                List<Mechanic> DeklareraLista = adminService.GetMechanic(employerIdSearch.Text);
                foreach (var item in DeklareraLista)
                {
                    firstName.Text = item.FirstNameOfMechanic;
                    lastname.Text = item.LastNameOfMechanic;
                    DateTime Birth = item.BirthdayOfMechanic;
                    DateTime employ = item.DateOfEmploymentOfMechanic;
                    employerId.Text = employerIdSearch.Text;
                    Motor.IsChecked= item.Engine;
                    Däck.IsChecked = item.Tire;
                    vindrutor.IsChecked = item.Window;
                    Bromsar.IsChecked = item.Brakes;
                    Kaross.IsChecked = item.Kaross;

                    dateOfBirth.Text = Birth.ToString("yyyy-MM-dd");
                    dateOfEmployment.Text = employ.ToString("yyyy-MM-dd");
            
                }
            }
            else
            {
                MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
        //----------------------------------------------------------------Sök efter Anställd, koppla till konto eller ta bort, koppla till textbo samt koppla kompetens till radiobuttom.


        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ILogic adminService = new AdminService();

            if ((adminService.ActivUser(employerIdSearch.Text)))
            {

                if (MessageBox.Show("Är du säker på att du vill ta bort anställd!", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    adminService.DeleteMechanic(employerIdSearch.Text);   
                }
            }
            else
            {
                MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
             
            }
            ChangeEmployer changeEmployer = new ChangeEmployer();
            this.NavigationService.Navigate(changeEmployer);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            List<Mechanic> mechanics = new List<Mechanic>();
            ILogic adminService = new AdminService();


            if (adminService.ValidMechanic(firstName.Text, lastname.Text, dateOfBirth.Text, dateOfEmployment.Text, employerId.Text))
            {

                if ((adminService.ActivUser(employerIdSearch.Text)))
                {

                    adminService.DeleteMechanic(employerIdSearch.Text);

                    mechanics.Add(new Mechanic(firstName.Text, lastname.Text,
                                   DateTime.Parse(dateOfBirth.Text), DateTime.Parse(dateOfEmployment.Text),
                                    (bool)Motor.IsChecked, (bool)Däck.IsChecked, (bool)vindrutor.IsChecked,
                                    (bool)Bromsar.IsChecked, (bool)Kaross.IsChecked,mechanic.ActiveOrders));



                    string id = employerId.Text;
                    adminService.NewMechanic(id, mechanics);
                    MessageBox.Show("Mekaniker är nu ändrad!", "", MessageBoxButton.OK);

                }
                else 
                {
                    MessageBox.Show("Kontrollera att allt är ifyllt korrekt!\n Datum ska anges i formatet(yyyy-mm-dd)", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else
            {
                MessageBox.Show("Kontrollera att allt är ifyllt korrekt!\n Datum ska anges i formatet(yyyy-mm-dd)", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            ChangeEmployer changeEmployer = new ChangeEmployer();
            this.NavigationService.Navigate(changeEmployer);
        }

       
        private void employerIdSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (employerIdSearch.Text == "Anställnings-ID")
            {
                employerIdSearch.Text = string.Empty;
            }
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> DeklareraLista = new List<string>();
            ILogic adminService = new AdminService();
            DeklareraLista = adminService.GetKey();
            var combo = sender as ComboBox;
            combo.ItemsSource = DeklareraLista;
            combo.SelectedIndex = 0;
           
        }

        private void Bromsar_Checked(object sender, RoutedEventArgs e)
        {

            mechanic.Brakes = true;

        }

        private void Tire_Checked(object sender, RoutedEventArgs e)
        {
            mechanic.Tire = true;
        }

        private void vindrutor_Checked(object sender, RoutedEventArgs e)
        {
            mechanic.Window = true;
        }

        private void Motor_Checked(object sender, RoutedEventArgs e)
        {
            mechanic.Engine = true;
        }

        private void Kaross_Checked(object sender, RoutedEventArgs e)
        {
            mechanic.Kaross = true;
        }

     
    }
 }

