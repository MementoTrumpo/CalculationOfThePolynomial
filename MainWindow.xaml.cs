using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MatrixAndGauss;

namespace CalculationOfThePolynomial
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static List<Point> points = new List<Point>();

        public MainWindow()
        {
            InitializeComponent();
            //Отрисовка координатной сетки
            DrawingCoordinatePlate();
            

        }

        /// <summary>
        /// Отрисовка координатной сетки
        /// </summary>
        private void DrawingCoordinatePlate()
        {
            //Отрисовка вертикальных линий
            for (int coordinateX = 0; coordinateX < canvas.Width / 50; coordinateX++)//Рисуем вертикальные линии с шагом = 0,2
            {
                //Вертикальные линии сетки
                Line axies_X = new Line();
                axies_X.X1 = coordinateX * 50;
                axies_X.Y1 = 0;
                axies_X.X2 = coordinateX * 50;
                axies_X.Y2 = canvas.Height;
                axies_X.Stroke = Brushes.Black;

                if (coordinateX % 2 == 0)// проверка на целую клетку (2 клетки = 1)
                {
                    axies_X.StrokeThickness = 2;//Толщина основных линий
                }
                else
                {
                    axies_X.StrokeThickness = 0.5;//Толщина промежуточных линий
                }
                canvas.Children.Add(axies_X);

            }
            //Отрисовка горизонтальных линий
            for (int coordinateY = 0; coordinateY < canvas.Height / 50; coordinateY ++)
            {
                //Горизонтальные линии сетки
                Line axies_Y = new Line();

                axies_Y.X1 = 0;
                axies_Y.Y1 = coordinateY * 50;
                axies_Y.X2 = canvas.Width;
                axies_Y.Y2 = coordinateY * 50;
                axies_Y.Stroke = Brushes.Black;
                axies_Y.StrokeThickness = 2;//Толщина основных линий

                canvas.Children.Add(axies_Y);//Переносим линии на convas

            }

            //int count = 0;
            //for (int coordinateY = (int)canvas_1.Height - 70; coordinateY > 0; coordinateY -= 50)
            //{
            //    Label label = new Label();
            //    Point position = new Point(15, coordinateY);

            //    label.FontSize = 16;
            //    label.Foreground = Brushes.Black;
            //    label.Margin = new Thickness(position.X, position.Y, 0, 0);
            //    label.Content = $"{count}";
            //    canvas_1.Children.Add(label);
            //    count += 1;
            //}

            //count = 1;
            //for(int coordinateX = 90; coordinateX < canvas_1.Width; coordinateX += 100)
            //{
            //    Label label = new Label();
            //    Point position = new Point(coordinateX, canvas_1.Height - 50);

            //    label.FontSize = 16;
            //    label.Foreground = Brushes.Black;
            //    label.Margin = new Thickness(position.X, position.Y, 0, 0);
            //    label.Content = $"{count}";
            //    canvas_1.Children.Add(label);
            //    count += 1;
            //}
            

        }

        private List<double> ConvertingFromTextBoxToList()
        {
            double value;

            bool canParse;

            var textBoxes = this.grid_1.Children.OfType<TextBox>().ToList();

            List<double> coordinatesX = new List<double>();

            for (int i = 0; i < textBoxes.Count; i++)
            {
                canParse = double.TryParse(textBoxes[i].Text, out value);

                if(canParse == false)
                {
                    throw new ArgumentException("Невозможно привети к типу double");
                }
                if (i % 2 == 0)
                {
                    coordinatesX.Add(value);
                }
            }
            return coordinatesX;
        }
        /// <summary>
        /// Отрисовка цены деления на графике
        /// </summary>
        private void DrawingDivisions()
        {
                    
            List<double> xCoordinates = ConvertingFromTextBoxToList();

            double minValue = xCoordinates.Min();

            double maxValue = xCoordinates.Max();

            if(maxValue - minValue > 1)
            {
                MessageBox.Show("Разница между  максимальным и минимальным значениями не должна превышать 1!");
                ClearTextBoxesValues();
            }
            //Узнаем цену деления графика функции
            double delta = 0.2d;
            for (int coordinateX = 90; coordinateX < canvas_1.Width; coordinateX += 100)
            {
                Label label = new Label();
                Point position = new Point(coordinateX, canvas_1.Height - 50);

                label.FontSize = 16;
                label.Foreground = Brushes.Black;
                label.Margin = new Thickness(position.X, position.Y, 0, 0);
                label.Content = $"{minValue}";
                canvas_1.Children.Add(label);
                minValue += delta;
            }
            int count = 1;
            for (int coordinateY = (int)canvas_1.Height - 125; coordinateY > 0; coordinateY -= 50)
            {
                Label label = new Label();
                Point position = new Point(15, coordinateY);

                label.FontSize = 16;
                label.Foreground = Brushes.Black;
                label.Margin = new Thickness(position.X, position.Y, 0, 0);
                label.Content = $"{count}";
                canvas_1.Children.Add(label);
                count += 1;
            }
        }
        /// <summary>
        /// Очищает значения единичных отрезков на графике
        /// </summary>
        private void ClearDevisions()
        {
            canvas_1.Children.Clear();
        }
        /// <summary>
        /// Отрисовка всех точек на Canvas
        /// </summary>
        private void DrawingPoint()
        {
            for (int i = 0; i < points.Count; i++)
            {
                int R = 5;
                Ellipse ellipse = new Ellipse();
                ellipse.Width = R * 2;
                ellipse.Height = R * 2;
                ellipse.Fill = Brushes.Red;
                ellipse.Margin = new Thickness(i * 100 + 45, canvas.Height - points[i].Y * 50 - R, 0, 0);
                canvas.Children.Add(ellipse);
            }
        }
        /// <summary>
        /// Очищает значения всех TextBox
        /// </summary>
        private void ClearTextBoxesValues()
        {
            var textBoxes = this.grid_1.Children.OfType<TextBox>();
            foreach (var textBox in textBoxes)
            {
                textBox.Clear();
            }

        }

        /// <summary>
        /// Очистка значений всех TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_1_Click(object sender, RoutedEventArgs e)
        {
            //Очистка значений в textBox-ах
            ClearTextBoxesValues();
            //Очистка всего canvas
            canvas.Children.Clear();
            //Очистка labels
            ClearLabels();
            //Отрисовка кординатной сетки
            DrawingCoordinatePlate();
            //Очистка делений на графике
            ClearDevisions();

        }

        /// <summary>
        /// Проверка ввода данных пользователем (чтобы пользователь не смог вводить буквы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCurrencyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {           
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)) && (e.Text != ","))
            {
                { e.Handled = true; }
            }
            else if ((e.Text == ",") && ((tbne.Text.IndexOf(",") != -1) || (tbne.Text == "")))
            { e.Handled = true; }

          
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Calculate();
            AddCoordinatesToList();
            //Отрисовка делений на координатной оси
            DrawingDivisions();
            //Отрисовка точек
            DrawingPoint();
            //Отрисовка графиков аппроксимирующих функций
            DrawingPlot();
            //Добавление значений функций на labels
            AddLabels();
            //Очистка значениц списка точек
            points.Clear();
            
        }
        
        /// <summary>
        /// Добавление координат из TextBoxes в список List
        /// </summary>
        private void AddCoordinatesToList()
        {
            //Возможность спарсить данные
            bool canParse;
            //Значение, передаваемое при успешном переводе в тип double
            double value;
            //Список элементов textBox
            List<TextBox> textBoxes = this.grid_1.Children.OfType<TextBox>().ToList();
            //Список координат точек по оси X
            List<double> coordinatesX = new List<double>();
            //Список координат точек по оси Y
            List<double> coordinatesY = new List<double>();

            for(int i = 0; i < textBoxes.Count; i++)
            {
                
                canParse = double.TryParse(textBoxes[i].Text, out value);
                if (canParse == false)
                {
                    throw new ArgumentException("Невозможно преобразовать в тип double");
                }
                if(value > 9)
                {
                    MessageBox.Show("Значение должно находиться в пределах от 0 до 9");
                    return;
                }
                
                if(i % 2 == 0)
                {
                    coordinatesX.Add(value);
                }
                else
                {
                    coordinatesY.Add(value);
                }                
            }
            for(int i = 0; i < textBoxes.Count() / 2; i++)
            {
                Point point = new Point(coordinatesX[i], coordinatesY[i]);
                points.Add(point);
            }
        }

        /// <summary>
        /// Решает СЛАУ и возвращает коэффициенты линейной и пораболической аппроксимации в зависимости от выбранного параметра
        /// </summary>
        /// <param name="choice">true - в случае линейной аппроксимации, false - в случае квадратичной</param>
        /// <returns>Возвращает массив типа double, в котором хранится значения ответов</returns>
        private double[] SolveLinearApproximation(bool choice)
        {
            //Инициализируем переменные сумм
            double sum_1 = 0,//X
                   sum_2 = 0,//Y
                   sum_3 = 0,//X^2
                   sum_4 = 0,//X^3
                   sum_5 = 0,//X^4
                   sum_6 = 0,//XY
                   sum_7 = 0;//X^2Y
            double[] answerForParabol = new double[3];
            double[] answerForLine = new double[2];
            //находим необходимые суммы
            for(int i = 0; i < points.Count; i++)
            {
                LeastSquaresMethod leastSquaresMethod = new LeastSquaresMethod(points[i].X, points[i].Y);
                sum_1 += leastSquaresMethod.X;
                sum_2 += leastSquaresMethod.Y;
                sum_3 += leastSquaresMethod.X_2;
                sum_4 += leastSquaresMethod.X_3;
                sum_5 += leastSquaresMethod.X_4;
                sum_6 += leastSquaresMethod.XY;
                sum_7 += leastSquaresMethod.X_2Y;
            }
            if (choice == false)
            {
                //Создание объекта для решения СЛАУ
                GaussMethod solution = new GaussMethod(3, 3);
                //Запонение коэффициентов в уравнениях матрицы
                solution.Matrix[0][0] = sum_5;
                solution.Matrix[0][1] = sum_4;
                solution.Matrix[0][2] = sum_3;
                solution.Matrix[1][0] = sum_4;
                solution.Matrix[1][1] = sum_3;
                solution.Matrix[1][2] = sum_2;
                solution.Matrix[2][0] = sum_3;
                solution.Matrix[2][1] = sum_1;
                solution.Matrix[2][2] = points.Count;
                //Заполнение свободных членов 
                solution.RightPart[0] = sum_7;
                solution.RightPart[1] = sum_6;
                solution.RightPart[2] = sum_2;
                //Проверка на единственность решения
                int isCorrectAnswer = solution.SolveMatrix();
                if (isCorrectAnswer == 1)
                {
                    MessageBox.Show("Система не имеет решения!");
                    return null;
                }
                else if (isCorrectAnswer == 2)
                {
                    MessageBox.Show("Система имеет множество решений!");
                    return null;
                }
                //Заполнение массива ответов
                else
                {
                    for (int i = 0; i < answerForParabol.Length; i++)
                    {
                        answerForParabol[i] = solution.Answer[i];
                    }
                    return answerForParabol;
                }
            }
            else
            {
                //Создание объекта для решения СЛАУ
                GaussMethod solution = new GaussMethod(2, 2);
                //Запонение коэффициентов матрицы
                solution.Matrix[0][0] = sum_3;
                solution.Matrix[0][1] = sum_1;
                solution.Matrix[1][0] = sum_1;
                solution.Matrix[1][1] = points.Count;
                //Заполнение свободных членов 
                solution.RightPart[0] = sum_6;
                solution.RightPart[1] = sum_2;
                //Проверка на единственность решения
                int isCorrectAnswer = solution.SolveMatrix();
                if (isCorrectAnswer == 1)
                {
                    MessageBox.Show("Система не имеет решения!");
                    return null;
                }
                else if (isCorrectAnswer == 2)
                {
                    MessageBox.Show("Система имеет множество решений!");
                    return null;
                }
                //Заполнение массива ответов
                else
                {
                    for (int i = 0; i < answerForLine.Length; i++)
                    {
                        answerForLine[i] = solution.Answer[i];
                    }
                    return answerForLine;
                }   
            }
        }
        /// <summary>
        /// Отрисовка графика аппроксимирующей функции
        /// </summary>
        private void DrawingPlot()
        {
            double[] coefficientsForLine = SolveLinearApproximation(true);
            double[] coefficientsForPorabol = SolveLinearApproximation(false);
            //Проверка значений массива на null
            if(coefficientsForLine == null)
            {
                //throw new Exception("Массив не имеет значений");
                return;
            }
            if(coefficientsForPorabol == null)
            {
                //throw new Exception("Массив не имеет значений");
                return;
            }
            List<Point> P = new List<Point>();//Создаем список
            double x, y;//Создаем координаты
            x = 50; // Начинается с первого значения
            int i = 0;
            while (x <= 550)//заканчивается последним
            {
                
                y = coefficientsForLine[1] * 50 + coefficientsForLine[0] * (points[0].X + (x - 50) / 500) * 50;//Находим значение y

                P.Add(new Point(x, y));//Заполняем список точек
                x = x + 0.01;//Передвигаемся по оси X
                i++;
            }
            Polyline PL = new Polyline();//Создаем линию
            PointCollection PC = new PointCollection();//Создаем коллекцию точек
            foreach (Point p in P)//Для каждой точки
            {
                PC.Add(new Point(p.X, canvas.Height - p.Y));//Заполняем коллекцию точек с
            }

            PL.Points = PC;//Рисуем график по коллекции точкам
            PL.Stroke = Brushes.Blue;//Красим график в синий
            PL.StrokeThickness = 2;//толщина линии
            canvas.Children.Add(PL);//Переносим график на координатную плоскость


            List<Point> P1 = new List<Point>();//Создаем список

            double x1, y1;//Создаем координаты

            x1 = 50;// Начинается с первого значения

            i = 0;

            while (x1 <= 550)//заканчивается последним
            {
                y1 = coefficientsForPorabol[2] * 50 + coefficientsForPorabol[1] * ((points[0].X + (x1 - 50) / 500)) * 50 + coefficientsForPorabol[0] * (points[0].X
               + (x1 - 50) / 500) * (points[0].X + (x1 - 50) / 500) * 50;//Находим значение y для x
                P1.Add(new Point(x1, y1));//Заполняем список точек
                x1 = x1 + 0.01;//Передвигаемся по оси X
            }

            Polyline PL1 = new Polyline();//Создаем линию

            PointCollection PC1 = new PointCollection();//Создаем коллекцию точек

            foreach (Point p1 in P1)//Для каждой точки
            {
                PC1.Add(new Point(p1.X, canvas.Height - p1.Y));//Заполняем коллекцию

            }

            PL1.Points = PC1;//Рисуем график по коллекции точек
            PL1.Stroke = Brushes.DarkRed;//Красим график
            PL1.StrokeThickness = 2;//Толщина графика полинома второй степени
            canvas.Children.Add(PL1);//Переносим график на координатную плоскость
        }
        private void AddLabels()
        {
            double[] coefficientsForLine = SolveLinearApproximation(true);
            double[] coefficientsForPorabol = SolveLinearApproximation(false);
            //Заполняем тект label_1 для линейной аппроксимации
            if(coefficientsForLine[1] >= 0)
            {
                label_1.Content = $"{coefficientsForLine[0]} * x + {coefficientsForLine[1]}";
            }
            else
            {
                label_1.Content = $"{coefficientsForLine[0]} * x  {coefficientsForLine[1]}";
            }
            //Заполняем текст label_2 для пораболической аппроксимации
            //Если все коэффициенты положительные
            if (coefficientsForPorabol[1] >= 0 && coefficientsForPorabol[2] >= 0)
            {
                label_2.Content = $"{coefficientsForPorabol[0]} * x ^ 2 + {coefficientsForPorabol[1]} * x + {coefficientsForPorabol[2]}";
            }
            //Если второй коэффициент положительный, а третий отрицательный
            else if(coefficientsForPorabol[1] >= 0 && coefficientsForPorabol[2] < 0)
            {
                label_2.Content = $"{coefficientsForPorabol[0]} * x ^ 2 + {coefficientsForPorabol[1]} * x  {coefficientsForPorabol[2]}";
            }
            //Если второй коэффициент отрицательный, а третий положительный
            else if (coefficientsForPorabol[1] < 0 && coefficientsForPorabol[2] >= 0)
            {
                label_2.Content = $"{coefficientsForPorabol[0]} * x ^ 2  {coefficientsForPorabol[1]} * x + {coefficientsForPorabol[2]}";
            }
            //Если второй и третий коэффициенты отрицательны
            else
            {
                label_2.Content = $"{coefficientsForPorabol[0]} * x ^ 2  {coefficientsForPorabol[1]} * x  {coefficientsForPorabol[2]}";
            }
        }
        /// <summary>
        /// Очистка значений в Labels
        /// </summary>
        private void ClearLabels()
        {
            label_1.Content = "";
            label_2.Content = "";
        }


    }
}
