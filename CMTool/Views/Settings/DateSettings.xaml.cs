﻿using CMTool.ViewModels.Settings;
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

namespace CMTool.Views.Settings
{
    /// <summary>
    /// DateSettings.xaml 的交互逻辑
    /// </summary>
    public partial class DateSettings : Page
    {
        public DateSettingsViewModel ViewModel { get; }
        public DateSettings(DateSettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}
