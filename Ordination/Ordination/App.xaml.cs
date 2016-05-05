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
        
              var userViewModel = new UserViewModel();
       

              EventHandler userHandler = null;
              userHandler = delegate
              {
                  userViewModel.RequestClose -= userHandler;
                  userWindow.Close();
              };
              
              userViewModel.RequestClose += userHandler;


              userWindow.DataContext = userViewModel;

        
            userWindow.Show();
           // adminWindow.Show();

          }

    }
}
