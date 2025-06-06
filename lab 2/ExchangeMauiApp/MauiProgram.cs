﻿using Microsoft.Extensions.Logging;
using ExchangeMauiApp.Core;
using SQLitePCL;

namespace ExchangeMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        Batteries.Init();

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<ExchangeRatesService>();
        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
	}
}
