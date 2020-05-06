using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PathFinding.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {

        public void LoadDijkstra(object sender, RoutedEventArgs e)
        {
            ActivateItem(new DijkstrasViewModel());
        }

        public void LoadBellmanFord(object sender, RoutedEventArgs e)
        {
            ActivateItem(new BellmanFordViewModel());
        }

        public void LoadFloydWarshall(object sender, RoutedEventArgs e)
        {
            ActivateItem(new FloydWarshallViewModel());
        }
    }
}
