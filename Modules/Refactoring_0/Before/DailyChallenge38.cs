using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_0.Before
{
    public class DailyChallenge38
    {
        public class Field : IEquatable<Field>, IComparable<Field>
        {
            public int X;
            public int Y;
            public bool Live;
            public bool NextLive;
            public Field N;
            public Field W;
            public Field NW;
            public Field NE;
            public Field E;
            public Field S;
            public Field SE;
            public Field SW;

            public int CompareTo(Field other)
            {
                return X.CompareTo(other.X);
            }

            public bool Equals(Field other)
            {
                return other?.X == X && other.Y == Y;
            }
        }

        private Dictionary<int, List<Field>> _map = new Dictionary<int, List<Field>>();

        public DailyChallenge38(IEnumerable<Tuple<int, int>> lives)
        {
            foreach (var live in lives)
            {
                if (_map.ContainsKey(live.Item2) == false)
                {
                    _map[live.Item2] = new List<Field>();
                }
                Field field = new Field { X = live.Item1, Y = live.Item2, Live = true, NextLive = true };
                if (_map[live.Item2].Contains(field))
                {
                    continue;
                }
                _map[live.Item2].Add(field);
            }
            SortMap();
        }

        private void SortMap()
        {
            foreach (var set in _map.Values)
            {
                set.Sort();
            }
        }

        public void Proceed(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException();
            }
            Print();
            for (int i = 0; i < n; ++i)
            {
                AddNew();
                MakeStep();
                Print();
            }
        }

        private void AddNew()
        {
            // foreach add neighbours
            foreach (var pair in _map.ToList())
            {
                foreach (Field field in pair.Value.ToList())
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
            SortMap();
        }

        private void MakeStep()
        {
            foreach (var pair in _map)
            {
                foreach (Field f in pair.Value)
                {
                    int livingNeigh = 0;
                    livingNeigh += IsLive(f.N) ? 1 : 0;
                    livingNeigh += IsLive(f.S) ? 1 : 0;
                    livingNeigh += IsLive(f.W) ? 1 : 0;
                    livingNeigh += IsLive(f.E) ? 1 : 0;
                    livingNeigh += IsLive(f.NE) ? 1 : 0;
                    livingNeigh += IsLive(f.NW) ? 1 : 0;
                    livingNeigh += IsLive(f.SE) ? 1 : 0;
                    livingNeigh += IsLive(f.SW) ? 1 : 0;
                    if (f.Live)
                    {
                        if (livingNeigh < 2)
                        {
                            f.NextLive = false;
                        }
                        else if (livingNeigh <= 3)
                        {
                            f.NextLive = true;
                        }
                        else
                        {
                            f.NextLive = false;
                        }
                    }
                    else if (livingNeigh == 3)
                    {
                        f.NextLive = true;
                    }
                }
            }
            foreach (var pair in _map)
            {
                foreach (Field field in pair.Value)
                {
                    field.Live = field.NextLive;
                }
            }
        }

        private bool IsLive(Field field)
        {
            if (field == null)
            {
                return false;
            }
            return field.Live;
        }

        private void Print()
        {
            Debug.WriteLine("");
            Debug.WriteLine("");
            var list = _map.OrderBy(x => x.Key).ToList();
            if (list.Any(x => x.Value.Any(f => f.Live)) == false)
            {
                Debug.WriteLine("All dead!");
                return;
            }
            // we have sorted sets
            int minX = _map.Select(x => x.Value.FirstOrDefault(f => f.Live)).Where(x => x != null).OrderBy(x => x.X).First().X;
            int maxX = _map.Select(x => x.Value.LastOrDefault(f => f.Live)).Where(x => x != null).OrderBy(x => x.X).Last().X;
            int minY = list.FirstOrDefault(x => x.Value.Any(f => f.Live)).Key;
            int maxY = list.LastOrDefault(x => x.Value.Any(f => f.Live)).Key;
            foreach (var pair in _map.OrderBy(x => x.Key))
            {
                if (pair.Key < minY || pair.Key > maxY)
                {
                    continue;
                }
                StringBuilder sb = new StringBuilder();
                for (int i = minX; i <= maxX; ++i)
                {
                    Field field = pair.Value.FirstOrDefault(x => x.X == i);
                    if (field != null)
                    {
                        char ch = field.Live ? '*' : '.';
                        sb.Append($"({ch}) ");
                    }
                    else
                    {
                        // is death, cuz doesnt exist
                        sb.Append("(.) ");
                    }
                }
                Debug.WriteLine(sb.ToString());
            }
        }

        private Field AddNeighbour(int x, int y)
        {
            if (_map.ContainsKey(y) == false)
            {
                _map[y] = new List<Field>();
            }
            Field existing = _map[y].FirstOrDefault(f => f.X == x);
            if (existing != null)
            {
                return existing;
            }
            existing = new Field() { X = x, Y = y, Live = false, NextLive = false };
            _map[y].Add(existing);
            return existing;
        }
    }
}
