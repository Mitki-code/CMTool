﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Services
{
    public class WindowsProviderService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowsProviderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Show<T>()
        where T : class
        {
            if (!typeof(Window).IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException($"The window class should be derived from {typeof(Window)}.");

            var windowInstance = _serviceProvider.GetService<T>() as Window;

            if (windowInstance == null)
                throw new InvalidOperationException("Window is not registered as service.");

            windowInstance.Owner = Application.Current.MainWindow;
            windowInstance.Show();
        }
    }
}
