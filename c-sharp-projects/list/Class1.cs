using System;
using System.Collections.Generic;
using System.Linq;

namespace VolatilePulse.Collection
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-5.0
    /// <summary>
    /// Represents a strongly typed list of objects that can be accessed by index. Provides methods to search, sort, and manipulate lists.
    /// </summary>
    public class List<T>
    {
        private const int CAPACITY_INCREASE_MIN = 4;
        private const int CAPACITY_INCREASE_MULTIPLIER = 2;
        private const double TRIM_THRESHOLD_PERCENT = 0.9;
        private T[] _items;
        private int _count;

        /// <summary>
        /// Initializes a new instance of the List<T> class that is empty and has the default initial capacity.
        /// </summary>
        public List()
        {
            _count = 0;
            _items = Array.Empty<T>();
        }

        /// <summary>
        /// Initializes a new instance of the List<T> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        public List(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), "Collection is null.");

            _items = collection.ToArray();
            _count = Capacity;
        }

        /// <summary>
        /// Initializes a new instance of the List<T> class that is empty and has the specified initial capacity.
        /// </summary>
        public List(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity is less than 0.");

            _items = new T[capacity];
            _count = 0;
        }

        /// <summary>
        /// Gets or sets the total number of elements the internal data structure can hold without resizing.
        /// </summary>
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value == Capacity)
                    return;
                if (value < _count)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Capacity is set to a value less than Count.");

                T[] newArray;
                try
                {
                    newArray = new T[value];
                }
                catch (OutOfMemoryException ex)
                {
                    throw new OutOfMemoryException("There is not enough memory available on the system.", ex);
                }
                Array.Copy(_items, newArray, _count);
                _items = newArray;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the List<T>.
        /// </summary>
        public int Count { get => _count; }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        public T this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index), index, "Index is less than 0.");
                if (index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index), index, "Index is out of range.");

                return _items[index];
            }
            set
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index), index, "Index is less than 0.");
                if (index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index), index, "Index is out of range.");

                _items[index] = value;
            }
        }

        /// <summary>
        /// Adds an object to the end of the List<T>.
        /// </summary>
        public void Add(T value)
        {
            if (_count == Capacity)
            {
                Capacity = CapacityIncrease();
            }
            _items[_count] = value;
            _count++;
        }

        private int CapacityIncrease()
        {
            var percentIncrease = Capacity * CAPACITY_INCREASE_MULTIPLIER;
            var minimumCapacity = Capacity + CAPACITY_INCREASE_MIN;
            var increase = Math.Max(percentIncrease, minimumCapacity);

            return increase;
        }

        /// <summary>
        /// Removes all elements from the List<T>.
        /// </summary>
        public void Clear()
        {
            _items = new T[Capacity];
            _count = 0;
        }

        /// <summary>
        /// Determines whether the List<T> contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        public bool Exists(Predicate<T> pred)
        {
            for (int i = 0; i < _count; i++)
            {
                if (pred(_items[i]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the List<T>.
        /// </summary>
        public bool Remove(T value)
        {
            bool found = false;
            var newArray = new T[Capacity];

            for (int i = 0; i < _count; i++)
            {
                if (!found && _items[i].Equals(value))
                {
                    found = true;
                    continue;
                }
                if (!found)
                    newArray[i] = _items[i];
                else
                    newArray[i - 1] = _items[i];
            }
            if (found)
                _items = newArray;
            return found;
        }

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        public int RemoveAll(Predicate<T> pred)
        {
            int dest = 0;
            int oldCount = _count;
            int found = 0;

            for (int i = 0; i < oldCount; i++)
            {
                if (pred(_items[i]))
                {
                    found++;
                    _count--;
                    _items[i] = default;
                    continue;
                }

                if (dest != i)
                {
                    _items[dest] = _items[i];
                    _items[i] = default;
                }

                dest++;
            }

            return found;
        }

        /// <summary>
        /// Removes the element at the specified index of the List<T>.
        /// </summary>
        public bool RemoveAt(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index is less than 0.");
            if (index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index is out of range.");

            _count--;
            Array.Copy(_items, index + 1, _items, index, _count - index);
            return false;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {

            var joinedString = string.Join(", ", _items.Take(Math.Min(10, _count)));
            var output = $"{{ {joinedString} }}";
            return output;
        }

        /// <summary>
        /// Copies the elements of the List<T> to a new array.
        /// </summary>
        public T[] ToArray()
        {
            var newArray = new T[_count];
            Array.Copy(_items, newArray, _count);
            return newArray;
        }

        /// <summary>
        /// Sets the capacity to the actual number of elements in the List<T>, if that number is less than a threshold value.
        /// </summary>
        public void TrimExcess()
        {
            if (_count / Capacity < TRIM_THRESHOLD_PERCENT)
            {
                Capacity = _count;
            }
        }

        /// <summary>
        /// Determines whether every element in the List<T> matches the conditions defined by the specified predicate.
        /// </summary>
        public bool TrueForAll(Predicate<T> pred)
        {
            for (int i = 0; i < _count; i++)
            {
                if (!pred(_items[i]))
                    return false;
            }

            return true;
        }
    }
}
