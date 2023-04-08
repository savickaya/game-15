using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15
{
    class Game
    {
        int size;
        int[,] pole;
        int space_x, space_y;
        static Random rand = new Random();

        public Game(int size)//размер поля (4 на 4)
        {
            if (size < 2) size = 2;
            if (size > 5) size = 5;
            this.size = size;
            
            pole = new int[size, size];//координаты
        }

        public void start()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    pole[x, y] = coords_to_position(x, y) + 1;
            space_x = size - 1;
            space_y = size - 1;
            pole[space_x, space_y] = 0;//последний элемент
        }

        public void shift (int position)
        {
            int x, y;
            position_to_coords(position, out x, out y);
            if (Math.Abs(space_x - x) + Math.Abs(space_y - y) != 1)//стоят ли рядом с пустой
                return;
            pole[space_x, space_y] = pole[x, y];
            pole[x, y] = 0;
            space_x = x;
            space_y = y;
        }

        public void shift_random()
        {
        
            int a = rand.Next(0, 4);
            int x = space_x;
            int y = space_y;
            switch(a)
            {
                case 0: x--; break;
                case 1: x++; break;
                case 2: y--; break;
                case 3: y++; break;
            }
            shift(coords_to_position(x, y)); 

        }

        public bool chek_numbers ()
        {
            if (!(space_x == size - 1 && space_y == size - 1))
                return false;
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (x == size - 1 && y == size - 1)
                        return true;
                    else 
                    if (pole[x, y] != coords_to_position(x, y) + 1)
                        return false;
            return true;
        }

        public int get_number (int position)
        {
            int x, y;
            position_to_coords(position, out x, out y);
            if (x < 0 || x >= size) return 0;
            if (y < 0 || y >= size) return 0; //чтобы не было переполнения
            return pole[x, y];
        }
        private int coords_to_position (int x, int y)//перевод координат в позицию
        {
            if (x < 0) x = 0;
            if (x > size - 1) x = size - 1;
            if (y < 0) y = 0;
            if (y > size - 1) y = size - 1;

            return y * size + x;//вычисление координаты (x,y)

            /* 0.0 1.0 2.0 3.0
               0.1 1.1 2.1 3.1
               0.2 1.2 2.2 3.2
               0.3 1.3 2.3 3.3 */
        }

        private void position_to_coords (int position, out int x, out int y)//позиция в координаты
        {
            if (position < 0) position = 0;
            if (position > size * size - 1) position = size * size - 1;
            x = position % size;
            y = position / size;
        }
    }
}
