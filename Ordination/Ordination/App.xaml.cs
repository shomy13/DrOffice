using Ordination.View;
using Ordination.View.Admin;
using Ordination.View.User;
using Ordination.ViewModel.Admin;
using Ordination.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Ordination
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
         protected override void OnStartup(StartupEventArgs e)
         {
              base.OnStartup(e);

              UserView userWindow = new UserView();
              AdminView adminWindow = new AdminView();
        
              var userViewModel = new UserViewModel();
              var adminViewModel = new AdminViewModel();
       

              EventHandler userHandler = null;
              EventHandler adminHandler = null;
              userHandler = delegate
              {
                  userViewModel.RequestClose -= userHandler;
                  userWindow.Close();
              };
              adminHandler = delegate
              {
                  adminViewModel.RequestClose -= adminHandler;
                  adminWindow.Close();
              };

              userViewModel.RequestClose += userHandler;
              adminViewModel.RequestClose += adminHandler;

              userWindow.DataContext = userViewModel;
              adminWindow.DataContext = adminViewModel;
        
            userWindow.Show();
           // adminWindow.Show();

          }

    }
}
