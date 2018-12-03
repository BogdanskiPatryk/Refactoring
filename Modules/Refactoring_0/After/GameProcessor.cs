using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_0.After
{
    public class GameProcessor
    {
        private readonly Map _map;
        private readonly IMapPrinter _printer;
        private readonly IGameRules _gameRules;

        public GameProcessor(Map map, IGameRules gameRules, IMapPrinter printer = null)
        {
            _gameRules = gameRules ?? throw new ArgumentNullException(nameof(gameRules));
            _map = map ?? throw new ArgumentNullException(nameof(map));
            _printer = printer ?? new MapPrinterEmpty();
        }

        public void Proceed(int n)
        {
            if (n < 0)
            {
                return;
            }
            _printer.Print(_map);
            for (int i = 0; i < n; ++i)
            {
                if (_gameRules.NeedContinue(_map) == false)
                {
                    return;
                }
                MakeStep();
            }
        }

        private void MakeStep()
        {
            _map.AddNeighbours();
            Dictionary<Field, bool> nextLives = new Dictionary<Field, bool>();
            foreach (Field field in _map.Fields)
            {
                bool nextStatus = _gameRules.CalculateLiveStatus(field, _map.CalculateLivingNeighbours(field));
                nextLives.Add(field, nextStatus);
            }
            foreach (var pair in nextLives)
            {
                pair.Key.Live = pair.Value;
            }
            _printer.Print(_map);
        }
    }
}
