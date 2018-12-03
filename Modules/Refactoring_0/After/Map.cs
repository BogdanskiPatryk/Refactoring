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
        private Dictionary<int, Map_Row> _map = new Dictionary<int, Map_Row>();

        public IEnumerable<Field> Fields
        {
            get
            {
                List<Field> fields = new List<Field>();
                foreach(var row in _map)
                {
                    fields.AddRange(row.Value.Fields);
                }
                return fields;
            }
        }

        public bool IsAnyLive =>_map.Any(x => x.Value.IsAnyLive);

        public Map(IEnumerable<Tuple<int, int>> lives)
        {
            if (lives != null)
            {
                foreach (var live in lives)
                {
                    if (_map.ContainsKey(live.Item2) == false)
                    {
                        _map[live.Item2] = new Map_Row();
                    }
                    Field field = new Field { X = live.Item1, Y = live.Item2, Live = true };
                    _map[live.Item2].AddField(field);
                }
            }
        }

        public void AddNeighbours()
        {
            foreach (var pair in _map.ToList())
            {
                foreach (Field field in pair.Value.Fields.ToList())
                {
                    if (field.N == null)
                    {
                        field.N = AddNeighbour(field.X, field.Y - 1);
                        field.N.S = field;
                    }
                    if (field.S == null)
                    {
                        field.S = AddNeighbour(field.X, field.Y + 1);
                        field.S.N = field;
                    }
                    if (field.E == null)
                    {
                        field.E = AddNeighbour(field.X + 1, field.Y);
                        field.E.W = field;
                    }
                    if (field.W == null)
                    {
                        field.W = AddNeighbour(field.X - 1, field.Y);
                        field.W.E = field;
                    }
                    if (field.NE == null)
                    {
                        field.NE = AddNeighbour(field.X + 1, field.Y - 1);
                        field.NE.SW = field;
                    }
                    if (field.SE == null)
                    {
                        field.SE = AddNeighbour(field.X + 1, field.Y + 1);
                        field.SE.NW = field;
                    }
                    if (field.NW == null)
                    {
                        field.NW = AddNeighbour(field.X - 1, field.Y - 1);
                        field.NW.SE = field;
                    }
                    if (field.SW == null)
                    {
                        field.SW = AddNeighbour(field.X - 1, field.Y + 1);
                        field.SW.NE = field;
                    }
                }
            }
        }
        
        public Field AddNeighbour(int x, int y)
        {
            if (_map.ContainsKey(y) == false)
            {
                _map[y] = new Map_Row();
            }
            Field existing = _map[y].Fields.FirstOrDefault(f => f.X == x);
            if (existing != null)
            {
                return existing;
            }
            existing = new Field() { X = x, Y = y, Live = false};
            _map[y].AddField(existing);
            return existing;
        }

        private class Map_Row
        {
            private HashSet<Field> _fields = new HashSet<Field>();

            public bool IsAnyLive =>_fields.Any(x => x.Live);

            public IEnumerable<Field> Fields => _fields;

            public void AddField(Field field)
            {
                if (_fields.Add(field) == false)
                {
                    throw new ArgumentException($"Field '({field.X},{field.Y})' already exist");
                }
            }
        }
    }
}
