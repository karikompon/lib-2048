using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048ForWpf;

public class GameHost
{
    public static IHost GetHost()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<GameWindow>();

                services.AddSingleton<IGridSettings, GridSettings>();
                services.AddSingleton<IBlockPositionWriter, BlockPositionWriter>();
                services.AddSingleton<IGridGenerator, GridGenerator>();
                services.AddSingleton<IBlockRepository, BlockRepository>();
                services.AddSingleton<IGameOutcomesHandler, GameOutcomesHandler>();
                services.AddSingleton<ICustomizer, Customizer>();
                services.AddSingleton<IUndoHandler, UndoHandler>();
            })
            .Build();
    }
}
