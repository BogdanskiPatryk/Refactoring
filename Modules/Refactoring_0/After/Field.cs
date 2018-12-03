using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_0.After
{
    public class Field : IEquatable<Field>
    {
        public int X;
        public int Y;
        public bool Live;
        public Field N;
        public Field W;
        public Field NW;
        public Field NE;
        public Field E;
        public Field S;
        public Field SE;
        public Field SW;

        public int GetLivingNeighbours()
        {
            int livingNeigh = 0;
            livingNeigh += IsLive(N) ? 1 : 0;
            livingNeigh += IsLive(S) ? 1 : 0;
            livingNeigh += IsLive(W) ? 1 : 0;
            livingNeigh += IsLive(E) ? 1 : 0;
            livingNeigh += IsLive(NE) ? 1 : 0;
            livingNeigh += IsLive(NW) ? 1 : 0;
            livingNeigh += IsLive(SE) ? 1 : 0;
            livingNeigh += IsLive(SW) ? 1 : 0;
            return livingNeigh;
        }

        public override int GetHashCode()
        {
            int hashcode = 23;
            hashcode = (hashcode * 37) + X;
            hashcode = (hashcode * 37) + Y;
            return hashcode;
        }

        public bool Equals(Field other)
        {
            return other.GetHashCode() == GetHashCode();
        }

        private bool IsLive(Field field)
        {
            if (field == null)
            {
                return false;
            }
            return field.Live;
        }
    }
}
