using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsQuadraticEquation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            // Кнопка закрытия окна
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Выход - использование диалогового окна
            DialogResult result = MessageBox.Show(
                "Вы действительно хотите выйти?",
                "Выход",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void SolutionButton_Click(object sender, EventArgs e)
        {            
            using (StreamWriter sw = new StreamWriter("QuadraticEquation.txt", true))
            {
                sw.WriteLine(label1.Text);
                // Считывание введённых коэфициентов
                string userInput1 = KTextBox1.Text;
                string userInput2 = KTextBox2.Text;
                string userInput3 = KTextBox3.Text;                
                sw.Write(userInput1);
                sw.Write(label2.Text);
                sw.Write(userInput2);
                sw.Write(label3.Text);
                sw.Write(userInput3);
                sw.WriteLine(label4.Text);
                // Коэфициенты уравнения
                double k1, k2, k3;
                // Исключение некорректного ввода чисел
                if (double.TryParse(userInput1, out k1)
                    && double.TryParse(userInput2, out k2)
                    && double.TryParse(userInput3, out k3))
                {
                    k1 = Convert.ToDouble(userInput1);
                    k2 = Convert.ToDouble(userInput2);
                    k3 = Convert.ToDouble(userInput3);
                    double D = k2 * k2 - 4 * k1 * k3;  // Нахождение Дискриминанта
                    sw.WriteLine($"D = {D}");
                    sw.Write(label5.Text);
                    if (D > 0)                         // Д > 0 - уравнение имеет два корня
                    {
                        // Нахождение и вывод корней
                        double x1 = (-k2 + Math.Sqrt(D)) / (2 * k1);
                        double x2 = (-k2 - Math.Sqrt(D)) / (2 * k1);
                        AnswerTextBox.Text = $"x1 = {x1}; x2 = {x2}";
                        sw.WriteLine($"x1 = {x1}; x2 = {x2}");
                    }
                    if (D == 0)                       // Д = 0 - уравнение имеет один корень
                    {
                        // Нахождение и вывод корня 
                        double x = -k2 / (2 * k1);
                        AnswerTextBox.Text = $"x = {x}";
                        sw.WriteLine($"x = {x}");
                    }
                    if (D < 0)                         // Д < 0 - уравнение не имеет корней
                    {
                        // Вывод ответа
                        AnswerTextBox.Text = "Корней нет";
                        sw.WriteLine("Корней нет");
                    }                    
                }
                else
                {
                    // Вывод окна "Ошибка", очистка всех тектбоксов
                    MessageBox.Show(
                        "Некорректный ввод",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    KTextBox1.Clear();
                    KTextBox2.Clear();
                    KTextBox3.Clear();
                    AnswerTextBox.Clear();
                }
                sw.WriteLine();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            // Кнопка очистки текстбоксов
            KTextBox1.Clear();
            KTextBox2.Clear();
            KTextBox3.Clear();
            AnswerTextBox.Clear();
        }
    }
}
