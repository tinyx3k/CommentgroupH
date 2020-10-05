using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commentgroup
{
    public static class Common
    {
        public static List<T> RandomList<T>(this IList<T> list, int number, List<int> listIgnore)
        {
            var listNew = new List<T>();
            Random rng = new Random();
            if (number < list.Count())
            {
                while (number > 0)
                {
                    int n = list.Count;
                    int k = rng.Next(0, n);
                    var dem = 0;
                    while (dem < 200)
                    {
                        if (!listIgnore.Contains(k))
                        {
                            listNew.Add(list[k]);
                            list.Remove(list[k]);
                            break;
                        }
                        dem++;
                    }
                    number--;
                }
            }
            return listNew;
        }

        public static int RandomValue(int valueFrom, int valueTo)
        {
            int randomTime = (new Random()).Next(valueFrom, valueTo);
            return randomTime;
        }
    }
}
