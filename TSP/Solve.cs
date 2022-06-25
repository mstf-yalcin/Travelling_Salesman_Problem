using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    internal class Solve
    {

        private int N, start;
        private int[,] distance;
        private List<int> tour = new List<int>();
        private int minTourCost = 999;
        private bool solver = false;


        public Solve(int start, int[,] distance)
        {
            N = distance.GetLength(0);
            this.start = start;
            this.distance = distance;
        }

        public List<int> getTour()
        {
            if (!solver)
            {
                solve();
            }
            return tour;
        }

        public double getTourCost()
        {
            if (!solver)
            {
                solve();
            }
            return minTourCost;
        }
        public void solve()
        {
            if (solver) return;

            int END_STATE = (1 << N) - 1;

            int[,] array = new int[N, 1 << N];

            for (int end = 0; end < N; end++)
            {
                if (end == start) continue;
                    array[end, (1 << start) | (1 << end)] = distance[start, end];
            }

            for (int r = 3; r <= N; r++)
            {
                foreach (int item in combinations(r, N))
                {
                    if (notIn(start, item))
                        continue;

                    for (int next = 0; next < N; next++)
                    {
                        if (next == start || notIn(next, item)) 
                            continue;

                        int subsetWithoutNext = item ^ (1 << next);
                        int minDist = 999;
                        for (int end = 0; end < N; end++)
                        {
                            if (end == start || end == next || notIn(end, item)) 
                                continue;

                            int newDistance = array[end, subsetWithoutNext] + distance[end, next];
                            if (newDistance < minDist)
                            {
                                minDist = newDistance;
                            }
                        }

                        array[next, item] = minDist;
                    }
                }
            }

            for (int i = 0; i < N; i++)
            {
                if (i == start)
                    continue;

                int tourCost = array[i, END_STATE] + distance[i, start];
                if (tourCost < minTourCost)
                {
                    minTourCost = tourCost;
                }
            }

            int lastIndex = start;
            int state = END_STATE;
            tour.Add(start);

            for (int i = 1; i < N; i++)
            {
                int index = -1;
                for (int j = 0; j < N; j++)
                {
                    if (j == start || notIn(j, state)) 
                        continue;

                    if (index == -1) index = j;
                    double prevDist = array[index, state] + distance[index, lastIndex];
                    double newDist = array[j, state] + distance[j, lastIndex];
                    if (newDist < prevDist)
                    {
                        index = j;
                    }
                }

                tour.Add(index);
                state = state ^ (1 << index);
                lastIndex = index;
            }

            tour.Add(start);
            tour.Reverse();

            solver = true;
        }

        private static bool notIn(int elem, int subset)
        {
            return ((1 << elem) & subset) == 0;
        }

        public static List<int> combinations(int r, int n)
        {
            List<int> subsets = new List<int>();
            combinations(0, 0, r, n, subsets);
            return subsets;
        }
        private static void combinations(int set, int at, int r, int n, List<int> subsets)
        {
            int elementsLeftToPick = n - at;
            if (elementsLeftToPick < r) return;

            if (r == 0)
            {
                subsets.Add(set);
            }
            else
            {
                for (int i = at; i < n; i++)
                {
                    set |= 1 << i;

                    combinations(set, i + 1, r - 1, n, subsets);

                    set &= ~(1 << i);
                }
            }
        }


    }
}
