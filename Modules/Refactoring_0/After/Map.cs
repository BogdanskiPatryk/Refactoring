using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_0.After
{
    public class Map
    {
        private Dictionary<int, Dictionary<int, Field>> _map = new Dictionary<int, Dictionary<int, Field>>();

        public IEnumerable<Field> Fields
        {
            get
            {
                List<Field> fields = new List<Field>();
                foreach(var row in _map)
                {
                    fields.AddRange(row.Value.Values);
                }
                return fields;
            }
        }

        public bool IsAnyLive =>_map.Any(x => x.Value.Values.Any(f => f.Live));

        public Map(IEnumerable<Tuple<int, int>> lives = null)
        {
            if (lives != null)
            {
                foreach (var live in lives)
                {
                    if (_map.ContainsKey(live.Item2) == false)
                    {
                        _map[live.Item2] = new Dictionary<int, Field>();
                    }
                    Field field = new Field { X = live.Item1, Y = live.Item2, Live = true };
                    _map[live.Item2][live.Item1] = field;
                }
            }
        }

        public int CalculateLivingNeighbours(Field field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }
            int livingNeigh = 0;
            DoForAllNeighbours(field, (x, y) =>
            {
                livingNeigh += IsLive(x, y) ? 1 : 0;
            });
            return livingNeigh;
        }

        public void AddNeighbours()
        {
            foreach (Field field in Fields.Where(x => x.Live).ToList())
            {
                DoForAllNeighbours(field, (x, y) => AddField(x, y));
            }
        }

        private void DoForAllNeighbours(Field field, Action<int,int> action)
        {
            for (int x = field.X - 1; x <= field.X + 1; x++)
                for (int y = field.Y - 1; y <= field.Y + 1; y++)
                {
                    if (x == field.X && y == field.Y)
                    {
                        continue;
                    }
                    action(x, y);
                }
        }
        
        private void AddField(int x, int y)
        {
            if (_map.ContainsKey(y) == false)
            {
                _map[y] = new Dictionary<int, Field>();
            }
            if (_map[y].ContainsKey(x) == false)
            {
                _map[y][x] = new Field() { X = x, Y = y, Live = false };
            }
        }

        private bool IsLive(int x, int y)
        {
            if (_map.TryGetValue(y, out Dictionary<int,Field> row))
            {
                if (row.TryGetValue(x, out Field field))
                {
                    return field.Live;
                }
            }
            return false;
        }
    }
}
