
using System;
using System.Collections.Generic;
using System.Linq;
using static Geo.Models.GeoRepository;

namespace Geo.Models
{
    public class Storage
    {
        private static readonly int queueLimit = 2;
        private Dictionary<Point, long> timeQueue = new();
        private Dictionary<Point, string> content = new();
        long curTime;

        private static Storage instance;
        private Storage() { }
        public static Storage GetInstance()
        {
            if (instance == null)
                instance = new Storage();
            return instance;
        }

#nullable enable
        public string? FindAndGetPoint(double latitude, double longitude)
        {
            var point = new Point(latitude, longitude);
            if (content.ContainsKey(point))
            {
                curTime = DateTime.Now.Ticks;
                timeQueue[point] = curTime;
                return content[point];
            }
            return null;
        }

        public void AddPoint(double latitude, double longitude, string answer)
        {
            var point = new Point(latitude, longitude);
            if (timeQueue.Count < queueLimit)
            {
                curTime = DateTime.Now.Ticks;
                content.Add(point, answer);
                timeQueue.Add(point, curTime);              
            }
            else
            {
                evictFromStorage(point, answer);               
            }
        }
        private void evictFromStorage(Point point, string answer)
        {
            var minKey = timeQueue.First(x => x.Value == timeQueue.Values.Min()).Key;
            content.Remove(minKey);
            timeQueue.Remove(minKey);
            content.Add(point, answer);
            timeQueue.Add(point, curTime);
        }
    }
}
