using System.Collections;
using System.Collections.Generic;

namespace Astar
{
    public interface AstarNode
    {
        IEnumerable<AstarNode> Neighbors
        {
            get;
        }

        int costTo(AstarNode neighbor);

        int estimateTo(AstarNode destination);
    }
}