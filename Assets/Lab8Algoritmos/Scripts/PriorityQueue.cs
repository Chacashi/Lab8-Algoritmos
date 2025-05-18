using UnityEngine;

public class PriorityQueue<T>
{
    public class PriorityQueueNode
    {
        public T Value { get; private set; }
        public float Priority { get; private set; }
        public PriorityQueueNode Next { get; private set; }
        public PriorityQueueNode Prev { get; private set; }

        public PriorityQueueNode(T value, float priority)
        {
            Value = value;
            Priority = priority;
        }

        public void SetNext(PriorityQueueNode next) => Next = next;
        public void SetPrev(PriorityQueueNode prev) => Prev = prev;
    }

    private PriorityQueueNode head = null;
    private PriorityQueueNode last = null;
    private int count = 0;

    public int Count => count;

    public void Enqueue(T item, float prio)
    {
        var newNode = new PriorityQueueNode(item, prio);
        count++;

        if (head == null)
        {
            head = last = newNode;
            return;
        }

        if (prio < head.Priority)
        {
            newNode.SetNext(head);
            head.SetPrev(newNode);
            head = newNode;
            return;
        }

        if (prio >= last.Priority)
        {
            newNode.SetPrev(last);
            last.SetNext(newNode);
            last = newNode;
            return;
        }

        var current = head;
        while (current != null && current.Priority <= prio)
        {
            current = current.Next;
        }

        if (current != null)
        {
            var prev = current.Prev;
            newNode.SetPrev(prev);
            newNode.SetNext(current);
            if (prev != null) prev.SetNext(newNode);
            current.SetPrev(newNode);
        }
    }

    public T Dequeue()
    {
        if (count <= 0 || head == null)
        {
            count = 0;
            return default;
        }

        T value = head.Value;
        head = head.Next;

        if (head != null)
            head.SetPrev(null);
        else
            last = null;

        count--;
        return value;
    }

    public bool Contains(T item)
    {
        var current = head;
        while (current != null)
        {
            if (current.Value.Equals(item))
                return true;
            current = current.Next;
        }
        return false;
    }

    public void Clear()
    {
        head = last = null;
        count = 0;
    }
}