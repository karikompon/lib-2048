using System.Linq;
using System.Windows;

namespace _2048ForWpf;

public interface IGameOutcomesHandler
{
    bool TryWin();

    bool TryLose();
}

public class GameOutcomesHandler : IGameOutcomesHandler
{
    private readonly IBlockCollection _blocks;
    private readonly IGridSettings _gridSettings;

    private bool _gameIsWon;

    public GameOutcomesHandler(IBlockCollection blocks, IGridSettings gridSettings)
    {
        _blocks = blocks;
        _gameIsWon = false;
        _gridSettings = gridSettings;
    }

    public bool TryWin()
    {
        if (!_gameIsWon && _blocks.Any(block => block.Number == 2048))
        {
            MessageBox.Show("You win!", "", MessageBoxButton.OK);
            _gameIsWon = true;

            return true;
        }

        return false;
    }

    public bool TryLose()
    {
        if (_blocks.Count() >= _gridSettings.VerticalBlockCount * _gridSettings.HorizontalBlockCount)
        {
            var result = MessageBox.Show("You lose!", "", MessageBoxButton.OK);
            Application.Current.Shutdown();

            return true;
        }

        return false;
    }
}