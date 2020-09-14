using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astar
{
    public static class Astar
    {
        private static List<AstarNode> closed;
        private static List<AstarNode> open;
        private static Dictionary<AstarNode, int> gScore;
        private static Dictionary<AstarNode, int> hScore;
        private static Dictionary<AstarNode, int> fScore;
        private static Dictionary<AstarNode, AstarNode> cameFrom;

        static Astar()
        {
            closed = new List<AstarNode>();
            open = new List<AstarNode>();
            cameFrom = new Dictionary<AstarNode, AstarNode>();
            gScore = new Dictionary<AstarNode, int>();
            hScore = new Dictionary<AstarNode, int>();
            fScore = new Dictionary<AstarNode, int>();
        }

        private class OpenListSorter: IComparer<AstarNode>
        {
            private Dictionary<AstarNode, int> fScore;

            public OpenListSorter(Dictionary<AstarNode, int> fScore)
            {
                this.fScore = fScore;
            }

            public int Compare(AstarNode x, AstarNode y)
            {
                if (x != null && y != null)
                {
                    return this.fScore[x].CompareTo(fScore[y]);
                }
                else
                {
                    return 0;
                }
            }
        }

        public static IList<AstarNode> getPath(AstarNode start, AstarNode end)
        {
            //precondition: either of them is null
            if (start == null || end == null)
            {
                return null;
            }
            //initialize 
            closed.Clear();
            open.Clear();
            //put the start to open list
            open.Add(start);
            cameFrom.Clear();
            gScore.Clear();
            hScore.Clear();
            fScore.Clear();
            //init g,h,f
            gScore.Add(start,0);
            hScore.Add(start,start.estimateTo(end));
            fScore.Add(start,hScore[start]);
            OpenListSorter openListSorter = new OpenListSorter(fScore);
            AstarNode current = null, 
                from = null;
            int tempGscore;
            bool isTempBetter;
            while (open.Count > 0)
            {
                //set the first node in open list as the current node also the node with min f
                current = open[0];
                //find the path
                if (current == end)
                {
                    return ReconstructPath(new List<AstarNode>(), cameFrom, end);//return path
                }
                //remove current node, put it to closed list
                open.Remove(current);
                closed.Add(current);
                if (current != start)
                {
                    from = cameFrom[current];
                }
                foreach (AstarNode each in current.Neighbors)
                {
                    if (!closed.Contains(each) && from!= each)
                    {
                        tempGscore = gScore[current] + current.costTo(each);
                        isTempBetter = true;
                        if (!open.Contains(each))
                        {
                            open.Add(each);
                        }
                        //if the gScore[each] is null, return false always
                        else if (tempGscore >= gScore[each])
                        {
                            isTempBetter = false;
                        }
                        if (isTempBetter)
                        {
                            gScore[each] = tempGscore;
                            cameFrom[each] = current;
                            hScore[each] = current.estimateTo(end);
                            fScore[each] = gScore[each] + hScore[each];
                        }
                    }
                }
                open.Sort(openListSorter);
            }

            return null;
        }
        private static IList<AstarNode> ReconstructPath(IList<AstarNode> path, Dictionary<AstarNode, AstarNode> cameFrom, AstarNode currentNode)
        {
            if (cameFrom.ContainsKey(currentNode))
            {
                ReconstructPath(path, cameFrom, cameFrom[currentNode]);
            }
            path.Add(currentNode);
            return path;
        }
    }
}