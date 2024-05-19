﻿using CMTool.Module;
using CMTool.ViewModels.Pages;
using CMTool.ViewModels.Settings;
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
using Wpf.Ui.Controls;

namespace CMTool.Views.Settings
{
    /// <summary>
    /// MoreSettings.xaml 的交互逻辑
    /// </summary>
    public partial class MoreSettings : Page
    {
        public MoreSettingsViewModel ViewModel { get; }
        public MoreSettings(MoreSettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
            CheckAutoStart();
            CheckProtect();
        }

        private void CheckAutoStart()
        {
            if (PowerStartManger.IsAutoStart()) { PowerStartButton.IsChecked = true; }
            else { PowerStartButton.IsChecked = false; }
        }
        private void CheckProtect()
        {
            if (FileIO.GetData("Settings")["Safe"].ToString() == "true")
                ProtectButton.IsChecked = true;
        }
    }
}
