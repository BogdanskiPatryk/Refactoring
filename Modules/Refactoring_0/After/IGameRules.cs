using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_0.After
{
    public interface IGameRules
    {
        bool NeedContinue(Map map);
        bool CalculateLiveStatus(Field field, int livingNeighbours);
    }
}
