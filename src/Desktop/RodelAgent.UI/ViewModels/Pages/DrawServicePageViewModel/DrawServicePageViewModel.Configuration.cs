﻿// Copyright (c) Rodel. All rights reserved.

using RodelDraw.Interfaces.Client;
using RodelDraw.Models.Client;

namespace RodelAgent.UI.ViewModels.Pages;

/// <summary>
/// 绘图服务页面视图模型.
/// </summary>
public sealed partial class DrawServicePageViewModel
{
    private void ResetDrawClientConfiguration()
    {
        var config = new DrawClientConfiguration();
        foreach (var service in AvailableServices)
        {
            var propertyName = service.ProviderType.ToString();
            var property = config.GetType().GetProperty(propertyName);
            if (property != null && property.PropertyType.IsSubclassOf(typeof(ClientConfigBase)))
            {
                var convertedConfig = Convert.ChangeType(service.Config, property.PropertyType);
                property.SetValue(config, convertedConfig);
            }
            else
            {
                _logger.LogDebug($"无法设置 {propertyName} 的配置.");
                continue;
            }
        }

        var chatProviderFactory = GlobalDependencies.ServiceProvider.GetRequiredService<IDrawProviderFactory>();
        chatProviderFactory.ResetConfiguration(config);
    }
}
