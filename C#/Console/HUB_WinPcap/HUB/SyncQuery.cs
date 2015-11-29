using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HUB
{
    /// <summary>
    /// Интерфейс временного хранилища объектов
    /// </summary>
    public interface ISyncDelay
    {
        Object Dequeue();
        void Enqueue(Object value);
        int Count { get; }
    }

    /// <summary>
    /// Инкапсулирует стэк, реализуя ISyncDelay
    /// </summary>
    public class AsStack : ISyncDelay
    {
        Stack stack = new Stack();

        #region ISyncDelay Members

        public Object Dequeue() { return stack.Pop(); }

        public void Enqueue(Object value) { stack.Push(value); }

        public int Count { get { return stack.Count; } }

        #endregion
    }

    /// <summary>
    /// Инкапсулирует очередь, реализуя ISyncDelay
    /// </summary>
    public class AsQueue : ISyncDelay
    {
        Queue queue = new Queue();

        #region ISyncDelay Members

        public void Enqueue(Object value) { queue.Enqueue(value); }

        public Object Dequeue() { return queue.Dequeue(); }

        public int Count { get { return queue.Count; } }

        #endregion
    }

    /// <summary>
    /// Инкапсулирует сортированную очередь IComparable объектов
    /// </summary>
    public class AsComparable : ISyncDelay
    {
        /// <summary>
        /// Узел сортировочного дерева
        /// </summary>
        public class SortNode
        {
            public IComparable Value { get; private set; }
            /// <summary>
            /// Поддеревья
            /// </summary>
            SortNode left, right;
            /// <summary>
            /// К-во узлов в поддеревьях
            /// </summary>
            int lcount, rcount;
            /// <summary>
            /// Количество узлов в поддереве
            /// </summary>
            public int Count { get { return ((lcount <= rcount) ? lcount : rcount) + 1; } }
            /// <summary>
            /// Добавить узел
            /// </summary>
            /// <param name="node">Добавляемый узел</param>
            /// <returns>Ссылка ветви</returns>
            public SortNode Put(SortNode node)
            {
                if (node.Value.CompareTo(Value) < 0) return node.Put(this);
                else if (lcount <= rcount) { left = (left == null) ? node : left.Put(node); lcount = left.Count; }
                else { right = (right == null) ? node : right.Put(node); rcount = right.Count; }
                return this;
            }
            /// <summary>
            /// Извлечь значение
            /// </summary>
            /// <param name="ptr">Новая ссылка на корень ветви</param>
            /// <returns>"Минимальное значение"</returns>
            public Object Get(out SortNode ptr)
            {
                if (left == null) ptr = null;
                else if (right == null) ptr = left;
                else ptr = (left.Value.CompareTo(right.Value) < 0) ? left.Put(right) : right.Put(left);
                return Value;
            }
            /// <summary>
            /// Коструктор узла
            /// </summary>
            /// <param name="value">Хранимый объект</param>
            public SortNode(IComparable value) { Value = value; }
        }
        /// <summary>
        /// Корневой (минимальный) узел
        /// </summary>
        SortNode root;
        /// <summary>
        /// Количество хранимых объектов
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Добавить объект
        /// </summary>
        /// <param name="obj">Значение</param>
        public void Enqueue(object obj)
        {
            var node = new SortNode(obj as IComparable);
            root = (root == null) ? node : root.Put(node);
            Count++;
        }
        /// <summary>
        /// Извлечь "минимальный" объект (создает исключение, если пусто)
        /// </summary>
        /// <returns>Значение</returns>
        public object Dequeue()
        {
            var rv = root.Get(out root); Count--; return rv;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SyncQueue<T> : SyncDelay<T, AsQueue>
    {
        public SyncQueue() { }
        public SyncQueue(int maxCount) : base(maxCount) { }
    }

    public interface ISyncEnumerator<T>
    {
        T Get();
    }

    /// <summary>
    /// Threadsafe stack of T
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <typeparam name="Q">AsQueue / AsStack / AsComparable</typeparam>
    public class SyncDelay<T, Q> : IEnumerable<T>, ISyncEnumerator<T>, IDisposable
        where Q : class, ISyncDelay, new()
    {
        Q delay = new Q();
        /// <summary>
        /// Сигнализирует появление объектов в очереди
        /// </summary>
        public readonly Semaphore getSemaphore = new Semaphore(0, int.MaxValue);
        /// <summary>
        /// Обеспечивает требуемый размер очереди
        /// </summary>
        public readonly Semaphore putSemaphore;

        ManualResetEvent Closed = new ManualResetEvent(false);

        /// <summary>
        /// Создание очереди с putSemaphore
        /// </summary>
        /// <param name="minCount">Минимальный размер очереди</param>
        public SyncDelay(int minCount)
        {
            if (minCount >= 0) putSemaphore = new Semaphore(_minCount = minCount, int.MaxValue);
        }
        int _minCount;

        /// <summary>
        /// Создание очереди (putSemaphore не создается)
        /// </summary>
        public SyncDelay() : this(-1) { }

        /// <summary>
        /// Изменение максимального размера
        /// </summary>
        /// <param name="value">Величина изменения</param>
        public void AddMaxCount(int value)
        {
            lock (this) if ((_minCount += value) > delay.Count) putSemaphore.Release(_minCount - delay.Count);
        }

        /// <summary>
        /// Устанавливает признак закрытия очереди
        /// </summary>
        public void Close() { Closed.Set(); }
        /// <summary>
        /// Puts object into collection
        /// </summary>
        /// <param name="value">Object of type T</param>
        public T PutValue(T value)
        {
            //            if (value == null) throw new ArgumentNullException("Use Close method!");

            lock (this)
            {
                if (Closed.WaitOne(0) || WaitEmpty != null) throw new ObjectDisposedException(GetType().Name);

                delay.Enqueue(value); getSemaphore.Release();
            }

            return value;
        }
        /// <summary>
        /// Поместить объект с ожиданием putSemaphore
        /// </summary>
        /// <param name="value">Объект</param>
        /// <returns></returns>
        public T Put(T value)
        {
            if (putSemaphore != null) putSemaphore.WaitOne();

            return PutValue(value);
        }
        /// <summary>
        /// Проверка наличия объекта
        /// </summary>
        /// <param name="value">Возвращает оббьект</param>
        /// <returns>Есть/нет</returns>
        public bool Get(out T value)
        {
            try { value = Dequeue(false); return true; }
            catch { value = (new T[1])[0]; return false; }
        }
        /// <summary>
        /// Ожидать появление объекта
        /// <para>Если нет объектов и очередь закрыта - Exception</para>
        /// </summary>
        /// <returns>Объект</returns>
        public T Get() { return Dequeue(true); }
        /// <summary>
        /// Получить объект/исключение из очереди
        /// </summary>
        /// <param name="wait">true - с ожиданием</param>
        /// <returns>Объект</returns>
        T Dequeue(bool wait)
        {
            var handles = new WaitHandle[] { getSemaphore, Closed };

            switch (WaitHandle.WaitAny(handles, wait ? Timeout.Infinite : 0))
            {
                case 0: return GetValue();
                case 1: Dispose(); break;
            }
            throw new ObjectDisposedException(GetType().Name);
        }
        /// <summary>
        /// Просто выполняет Dequeue
        /// </summary>
        /// <returns>Объект иди Exception</returns>
        public T GetValue()
        {
            lock (this)
            {
                var value = delay.Dequeue();

                if (putSemaphore != null && delay.Count < _minCount) putSemaphore.Release();

                if (WaitEmpty != null && delay.Count == 0) WaitEmpty.Set();

                return (T)value;
            }
        }
        /// <summary>
        /// Get Count of objects waiting
        /// </summary>
        public int Count { get { lock (this) return delay.Count; } }

        AutoResetEvent WaitEmpty = null;
        /// <summary>
        /// Ожидать очистки очереди
        /// </summary>
        public void Wait()
        {
            lock (this)
            {
                if (delay.Count == 0) return;

                WaitEmpty = new AutoResetEvent(false);
            }

            using (WaitEmpty) WaitEmpty.WaitOne();

            WaitEmpty = null;
        }

        #region IDisposable Members

        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

        bool disposed;

        public void Dispose(bool disposing)
        {
            lock (this) if (disposed) return;
                else disposed = true;

            if (disposing) using (Closed) using (getSemaphore) if (putSemaphore != null) using (putSemaphore) { }
        }

        ~SyncDelay() { Dispose(false); }

        #endregion

        #region Члены IEnumerable<T>

        public IEnumerator<T> GetEnumerator() { return new SyncEnumerator<T>(this); }

        #endregion

        #region Члены IEnumerable

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #endregion

        public class SyncEnumerator<TData> : IEnumerator<TData>
        {
            ISyncEnumerator<TData> Queue;

            public SyncEnumerator(ISyncEnumerator<TData> queue) { Queue = queue; }

            #region IEnumerator<T> Members

            public TData Current { get; private set; }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current { get { return Current; } }

            public bool MoveNext() { try { Current = Queue.Get(); return true; } catch { return false; } }

            public void Reset() { throw new NotImplementedException(); }

            #endregion

            public void Dispose() { }
        }
    }
}
