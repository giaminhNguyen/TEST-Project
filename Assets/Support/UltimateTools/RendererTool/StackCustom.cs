#if UNITY_EDITOR

using System;
using System.Collections.Generic;

namespace UltimateHelper
{
    public class StackCustom<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();
    
        private int _maxCount;
        private bool _isLimited;
        
        public StackCustom(int maxCount = 0)
        {
            _maxCount = maxCount;
            _isLimited = maxCount > 0;
        }
        
        // Push an element onto the stack
        
        public void ChangeSize(int newSize)
        {
            _maxCount = newSize;
            _isLimited = newSize > 0;
            if (_isLimited && _list.Count > _maxCount)
            {
                while (_list.Count > _maxCount)
                {
                    _list.RemoveFirst();
                }
            }
        }
        
        public void Push(T item)
        {
            if (_isLimited && _list.Count >= _maxCount)
                PopFirst();
            _list.AddLast(item);
        }
    
        // Pop an element from the stack
        public T Pop()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Stack is empty.");
            
            T value = _list.Last.Value;
            _list.RemoveLast();
            return value;
        }
    
        // Peek at the top element of the stack
        public T Peek()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Stack is empty.");
            
            return _list.Last.Value;
        }
    
        // Check if the stack is empty
        public bool IsEmpty()
        {
            return _list.Count == 0;
        }
    
        // Get the number of elements in the stack
        public int Count()
        {
            return _list.Count;
        }
    
        // Peek at the first element of the stack
        public T PeekFirst()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Stack is empty.");
            
            return _list.First.Value;
        }
    
        // Pop the first element of the stack
        public T PopFirst()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Stack is empty.");
            
            T value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }
    }
}
#endif