using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StepCounter
{
    public static class ListHelper
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> items)
        {
            var collection = new ObservableCollection<T>();
            foreach (var item in items)
                collection.Add(item);
            return collection;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            var collection = new ObservableCollection<T>();
            foreach (var item in items)
                collection.Add(item);
            return collection;
        }
    }
}