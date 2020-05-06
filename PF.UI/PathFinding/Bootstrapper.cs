using Caliburn.Micro;
using PathFinding.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PathFinding
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
