using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_0.After
{
    public class GameRulesDefault : IGameRules
    {
        public bool CalculateLiveStatus(Field field)
        {
            int livingNeighbours = field.GetLivingNeighbours();
            if (field.Live)
            {
                if (livingNeighbours < 2)
                {
                    return false;
                }
                else if (livingNeighbours <= 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return livingNeighbours == 3;
        }

        public bool NeedContinue(Map map)
        {
            return map.IsAnyLive;
        }
    }
}
