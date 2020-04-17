using Ocean_DEMO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_DEMO
{
    /// <summary>
    /// Океан состоит из двухмерного масива
    /// </summary>
    class Ocean : IOcean, IOceanView
    {

       


        private ICell[,] _cells = null;
        private readonly int _columns;
        private readonly int _rows;
        private bool[,] _processed=null;

        public Ocean(int cols, int rows)
        {
            _columns = cols;
            _rows = rows;
            _cells = new ICell[_columns, _rows];
            _processed = new bool[_columns, _rows];
        }

        public void Initialize(int prey, int predators, int obstacles)
        {
            

            int[] entities = RandomGenerator.GenerateIntUniqueSeries(0, _rows * _columns, prey + predators + obstacles);


            int startIndex = entities.Length - 1;
            int stopIndex = predators + obstacles;
            int col;
            int row;

            for (int i = startIndex; i >= stopIndex; i--)
            {
                IndexToCoordinate(entities[i], out col, out row);
                _cells[col, row] =
                    new Fish(
                        ocean: this,
                        pos: new Coordinate() { X = col, Y = row },
                        curTimeToReproduce: RandomGenerator.GetIntRandom(1, DefaulConst.FISH_TIME_TO_REPRODUCE + 1),
                        timeToReproduce: DefaulConst.FISH_TIME_TO_REPRODUCE);
            }

            startIndex = stopIndex - 1;
            stopIndex = obstacles + predators / 2;    // хищники

            for (int i = startIndex; i >= stopIndex; i--)
            {
                IndexToCoordinate(entities[i], out col, out row);
                _cells[col, row] =
                    new Shark(
                        ocean: this,
                        pos: new Coordinate() { X = col, Y = row },
                        curTimeToReproduce: RandomGenerator.GetIntRandom(1, DefaulConst.SHARK_TIME_TO_REPRODUCE + 1),
                        curTimeToFeed: RandomGenerator.GetIntRandom(1, DefaulConst.SHARK_TIME_TO_FEED + 1),
                        timeToReproduce: DefaulConst.SHARK_TIME_TO_REPRODUCE,
                        timeToFeed: DefaulConst.SHARK_TIME_TO_FEED);
            }

            startIndex = stopIndex - 1;
            stopIndex = obstacles;

            for (int i = startIndex; i >= stopIndex; i--)
            {
                IndexToCoordinate(entities[i], out col, out row);
                _cells[col, row] =
                    new Tuna(
                        ocean: this,
                        pos: new Coordinate() { X = col, Y = row },
                        curTimeToReproduce: RandomGenerator.GetIntRandom(1, DefaulConst.TUNA_TIME_TO_REPRODUCE + 1),
                        curTimeToFeed: RandomGenerator.GetIntRandom(1, DefaulConst.TUNA_TIME_TO_FEED + 1),
                        timeToReproduce: DefaulConst.TUNA_TIME_TO_REPRODUCE,
                        timeToFeed: DefaulConst.TUNA_TIME_TO_FEED);
            }



            startIndex = stopIndex - 1;
            stopIndex = 0;

            for (int i = startIndex; i >= stopIndex; i--)
            {
                IndexToCoordinate(entities[i], out col, out row);
                _cells[col, row] = new Obstacle(new Coordinate() { X = col, Y = row });
            }
        }

        private void IndexToCoordinate(int index, out int column, out int row)
        {
            if (_rows > _columns)
            {
                row = index % _rows;
                column = index / _rows;
            }
            else
            {
                column = index % _columns;
                row = index / _columns;
            }
        }
        public void Run()
        {
            for (int i = 0; i < _processed.GetLength(0); i++)
            {
                for (int j = 0; j < _processed.GetLength(1); j++)
                {
                    _processed[i, j] = false;
                }
            }

            foreach (ICell item in this)
            {
                if (!_processed[item.Position.X, item.Position.Y]) 
                {
                    item.Process();
                    _processed[item.Position.X, item.Position.Y] = true;
                }
            }
        }

        #region IOcean Members

        /// <summary>
        /// Вернуть случайную пустую соседнюю ячейку
        /// </summary>
        /// <param name="position"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool GetEmptyNeighbour(Coordinate position, out Coordinate result)
        {
            bool success = false;
            result = new Coordinate() { X = -1, Y = -1 };
            int neighbourAreaSize = 3;

            // (left, top) = верхний левый угол анализируемой области
            int left = position.X - 1;
            int top = position.Y - 1;

            // Чтобы вернуть случайного соседа, начинаем перебор со случайного смещения (dX, dY)
            int dX = RandomGenerator.GetIntRandom(0, neighbourAreaSize);
            int dY = RandomGenerator.GetIntRandom(0, neighbourAreaSize);

            for (int xStep = 0; xStep < neighbourAreaSize; xStep++)
            {
                for (int yStep = 0; yStep < neighbourAreaSize; yStep++)
                {
                    int testX = left + dX;
                    int testY = top + dY;

                    if (!(dX == 1 && dY == 1 // сама себе ячейка не "сосед"
                        || testX < 0        // дальше - проверки индексов на выход за пределы массива
                        || testY < 0
                        || testX >= _columns
                        || testY >= _rows)
                        && _cells[testX, testY] == null
                        )
                    {
                        result = new Coordinate(testX, testY);
                        success = true;
                        break;
                    }

                    if (success)
                    {
                        break;
                    }
                    dY = (dY + 1) % neighbourAreaSize;
                }

                dX = (dX + 1) % neighbourAreaSize;
            }

            return success;
        }

        /// <summary>
        /// Вернуть случайную соседнюю-добычу
        /// </summary>
        /// <param name="c"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool GetPreyNeighbour(ICell c, out Coordinate result)
        {
            bool success = false;
            result = new Coordinate() { X = -1, Y = -1 };
            int neighbourAreaSize = 3;

            // (left, top) = верхний левый угол анализируемой области
            int left = c.Position.X - 1;
            int top = c.Position.Y - 1;

            // Чтобы вернуть случайного соседа, начинаем перебор со случайного смещения (dX, dY)
            int dX = RandomGenerator.GetIntRandom(0, neighbourAreaSize);
            int dY = RandomGenerator.GetIntRandom(0, neighbourAreaSize);

            for (int xStep = 0; xStep < neighbourAreaSize; xStep++)
            {
                for (int yStep = 0; yStep < neighbourAreaSize; yStep++)
                {
                    int testX = left + dX;
                    int testY = top + dY;

                    if (!(dX == 1 && dY == 1 // сама себе ячейка не "сосед"
                        || testX < 0        // дальше - проверки индексов на выход за пределы массива
                        || testY < 0
                        || testX >= _columns
                        || testY >= _rows)
                        && _cells[testX, testY] != null
                        && c.CanDisplace(_cells[testX, testY])  // переданная ячейка может захватить выбранного соседа
                        )
                    {
                        result = _cells[testX, testY].Position;
                        success = true;
                        break;
                    }

                    dY = (dY + 1) % neighbourAreaSize;
                }

                if (success)
                {
                    break;
                }
                dX = (dX + 1) % neighbourAreaSize;
            }

            return success;
        }

        /// <summary>
        /// Изменить расположение заданной ячейки
        /// </summary>
        /// <param name="oldPosition"></param>
        /// <param name="newPosition"></param>
        /// <returns>Операция прошла удачно</returns>
        public bool SetNewPosition(Coordinate oldPosition, Coordinate newPosition)
        {
            if (_cells[newPosition.X, newPosition.Y] != null)
            {
                return false;
            }

            ICell mover = _cells[oldPosition.X, oldPosition.Y];
            _cells[oldPosition.X, oldPosition.Y] = null;
            mover.Position = newPosition;
            _cells[newPosition.X, newPosition.Y] = mover;

            return true;
        }

        /// <summary>
        /// Заместить одну ячейку другой. Например, хищник поедает добычу
        /// </summary>
        /// <param name="oldPosition"></param>
        /// <param name="newPosition"></param>
        /// <returns>Операция прошла удачно</returns>
        public bool DislaceAtNewPosition(Coordinate oldPosition, Coordinate newPosition)
        {
            ICell displacer = _cells[oldPosition.X, oldPosition.Y];

            if (!displacer.CanDisplace(_cells[newPosition.X, newPosition.Y]))
            {
                return false;
            }

            _cells[oldPosition.X, oldPosition.Y] = null;
            displacer.Position = newPosition;
            _cells[newPosition.X, newPosition.Y] = displacer;

            return true;
        }

        /// <summary>
        /// Поместить новую ячейку в указанную координату
        /// </summary>
        /// <param name="c"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Add(ICell c, Coordinate position)
        {
            if (_cells[position.X, position.Y] != null)
            {
                return false;
            }

            ICell member = (ICell)c.Clone();
            member.Position = position;
            _cells[position.X, position.Y] = member;

            return true;
        }

        /// <summary>
        /// Удалить ячейку
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public void Delete(ICell c)
        {
            if (c == _cells[c.Position.X, c.Position.Y])
            {
                _cells[c.Position.X, c.Position.Y] = null;
            }
            else
            {
                throw new InvalidOperationException("В указанной позиции находится ячейка, отличная от удаляемой");
            }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return new CellEnumerator(this);
        }

        #endregion

        #region CellEnumerator

        struct CellEnumerator : IEnumerator
        {
            public CellEnumerator(Ocean oc)
            {
                _c = oc;
                _posCurrentItem = -1;
            }

            public object Current
            {
                get
                {
                    int col;
                    int row;
                    _c.IndexToCoordinate(_posCurrentItem, out col, out row);
                    return _c._cells[col, row];
                }
            }

            public bool MoveNext()
            {
                bool ok = false;
                int lastCell = _c._columns * _c._rows - 1;
                int columnCount = _c._columns;

                while (_posCurrentItem < lastCell)
                {
                    int col;
                    int row;

                    _c.IndexToCoordinate(++_posCurrentItem, out col, out row);
                    if (_c._cells[col, row] != null)
                    {
                        // Если достигли не пустой ячейки, то выходим
                        ok = true;
                        break;
                    }
                }

                return ok;
            }

            public void Reset()
            {
                _posCurrentItem = -1;
            }

            private int _posCurrentItem;
            private Ocean _c;
        }

        #endregion

        #region IOceanView Members

        public int Width
        {
            get { return _columns; }
        }

        public int Height
        {
            get { return _rows; }
        }

        #endregion



      
    }
}
