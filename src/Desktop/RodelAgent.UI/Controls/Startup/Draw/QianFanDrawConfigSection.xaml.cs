﻿// Copyright (c) Rodel. All rights reserved.

using System.Diagnostics;
using RodelAgent.UI.ViewModels.Items;
using RodelDraw.Models.Client;

namespace RodelAgent.UI.Controls.Startup;

/// <summary>
/// 千帆配置部分.
/// </summary>
public sealed partial class QianFanDrawConfigSection : DrawServiceConfigControlBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QianFanDrawConfigSection"/> class.
    /// </summary>
    public QianFanDrawConfigSection()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    /// <inheritdoc/>
    protected override void OnViewModelChanged(DependencyPropertyChangedEventArgs e)
    {
        var newVM = e.NewValue as DrawServiceItemViewModel;
        if (newVM == null)
        {
            return;
        }

        newVM.Config ??= new QianFanClientConfig();
        Debug.Assert(ViewModel.Config != null, "ViewModel.Config should not be null.");
        ViewModel.CheckCurrentConfig();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
        => SecretBox.Password = ((QianFanClientConfig)ViewModel.Config).Secret;

    private void OnSecretBoxTextChanged(object sender, RoutedEventArgs e)
    {
        ((QianFanClientConfig)ViewModel.Config).Secret = SecretBox.Password;
        ViewModel.CheckCurrentConfig();
    }
}
