//using System;

//namespace App.Utilities
//{
//    public class PQueue<Element> where Element : IComparable<Element>
//    {
//        public Element[] elements;
//        private int numberElements = 0;

//        public int NumberElements { get => numberElements; set => numberElements = value; }

//        public PQueue()
//        {
//            elements = new Element[2];
//        }

//        public void Enqueue(Element element)
//        {
//            elements[++NumberElements] = element;
//            if (NumberElements == elements.Length - 1)
//            {
//                ChangeCapacityTo(2 * elements.Length);
//            }
//            Swim(NumberElements);
//        }
//        private void Swim(int pos)
//        {
//            while (pos > 1 && (elements[pos / 2].CompareTo(elements[pos]) > 0))
//            {
//                Swap(pos, pos / 2);
//                pos = pos / 2;
//            }
//        }
//        private void Swap(int left, int right)
//        {
//            Element tmp = elements[left];
//            elements[left] = elements[right];
//            elements[right] = tmp;
//        }

//        public Element Dequeue()
//        {
//            Element first = elements[1];
//            Swap(1, NumberElements--);
//            Sink(1);
//            if (NumberElements > 0 && NumberElements == elements.Length / 4)
//            {
//                ChangeCapacityTo(elements.Length / 2 + 1);
//            }
//            return first;
//        }
//        private void Sink(int i)
//        {
//            while (2 * i <= NumberElements)
//            {
//                int j = 2 * i;
//                if (j < NumberElements && (elements[j].CompareTo(elements[j + 1]) > 0))
//                {
//                    j++;
//                }
//                if (!(elements[i].CompareTo(elements[j]) > 0))
//                    break;
//                Swap(i, j);
//                i = j;
//            }
//        }
//        public bool IsEmpty()
//        {
//            return NumberElements == 0;
//        }
//        private void ChangeCapacityTo(int capacity)
//        {
//            Element[] tmp = new Element[capacity];
//            for (int i = 0; i <= NumberElements; i++)
//            {
//                tmp[i] = elements[i];
//            }
//            elements = tmp;
//        }
//        public void Clear()
//        {
//            elements = new Element[2];
//            numberElements = 0;
//        }
//        public bool Contains(Element element)
//        {
//            for (int i = 1; i <= NumberElements; i++)
//            {
//                if (elements[i].Equals(element))
//                    return true;

//            }
//            return false;
//        }

//    }
//}
using System;

public class PQueue<T> where T : IComparable<T>
{
    public T[] container;
    private int numberElements = 0;

    public int Count { get => numberElements; set => numberElements = value; }

    public PQueue()
    {
        container = new T[2];
    }

    public void Enqueue(T element)
    {
        container[++Count] = element;
        if (Count == container.Length - 1)
            Resize(2 * container.Length);
        Swim(Count);
    }
    private void Swim(int k)
    {
        while (k > 1 && Less(k / 2, k))
        {
            Swap(k, k / 2);
            k = k / 2;
        }
    }
    private void Swap(int i, int j)
    {
        T t = container[i];
        container[i] = container[j];
        container[j] = t;
    }

    public T Dequeue()
    {
        T top = container[1];
        Swap(1, Count--);
        Sink(1);
        if (Count > 0 && Count == container.Length / 4)
            Resize(container.Length / 2 + 1);
        return top;
    }
    private void Sink(int i)
    {
        while (2 * i <= Count)
        {
            int j = 2 * i;
            if (j < Count && Less(j, j + 1))
                j++;
            if (!Less(i, j))
                break;
            Swap(i, j);
            i = j;
        }
    }
    private bool Less(int i, int j)
    {
        return container[i].CompareTo(container[j]) > 0;
    }
    public bool IsEmpty()
    {
        return Count == 0;
    }
    private void Resize(int capacity)
    {
        T[] copy = new T[capacity];
        for (int i = 0; i <= Count; i++)
        {
            copy[i] = container[i];
        }
        container = copy;
    }
    public void Clear()
    {
        container = new T[2];
        numberElements = 0;
    }
    public bool Contains(T element)
    {
        for (int i = 1; i <= Count; i++)
        {
            if (container[i].Equals(element))
                return true;

        }
        return false;
    }

}