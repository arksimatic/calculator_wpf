using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalkulator
{
	public partial class MainWindow : Window
	{
		string text = "0";
		bool isOperator = false;
		bool isDot = false;
		bool isMinus = false;
		char whatOperator;

		public MainWindow()
		{
			InitializeComponent();
		}

		public void Update_Text()
		{
			Result.Text = text;
		}

		private void Pressed(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.NumPad0 || e.Key == Key.D0)
			{
				Number_Add("0");
			}
			if (e.Key == Key.NumPad1 || e.Key == Key.D1)
			{
				Number_Add("1");
			}
			if (e.Key == Key.NumPad2 || e.Key == Key.D2)
			{
				Number_Add("2");
			}
			if (e.Key == Key.NumPad3 || e.Key == Key.D3)
			{
				Number_Add("3");
			}
			if (e.Key == Key.NumPad4 || e.Key == Key.D4)
			{
				Number_Add("4");
			}
			if (e.Key == Key.NumPad5 || e.Key == Key.D5)
			{
				Number_Add("5");
			}
			if (e.Key == Key.NumPad6 || e.Key == Key.D6)
			{
				Number_Add("6");
			}
			if (e.Key == Key.NumPad7 || e.Key == Key.D7)
			{
				Number_Add("7");
			}
			if (e.Key == Key.NumPad8 || e.Key == Key.D8)
			{
				Number_Add("8");
			}
			if (e.Key == Key.NumPad9 || e.Key == Key.D9)
			{
				Number_Add("9");
			}
			if(e.Key == Key.Add)
			{
				Add();
			}
			if(e.Key == Key.Subtract)
			{
				Sub();
			}
			if(e.Key == Key.Multiply)
			{
				Mult();
			}
			if(e.Key == Key.Divide)
			{
				Div();
			}
			if(e.Key == Key.Enter)
			{
				Equal();
			}
			if(e.Key == Key.Back)
			{
				Back();
			}
			if(e.Key == Key.Decimal)
			{
				Dot();
			}
		}

		private void Add()
		{
			if (isOperator == false)
			{
				text += "+";
				isOperator = true;
				whatOperator = '+';
				isDot = false;
			}
			Update_Text();
		}
		private void Sub()
		{
			if (isOperator == false)
			{
				text += "-";
				isOperator = true;
				whatOperator = '-';
				isDot = false;
			}
			Update_Text();
		}
		private void Mult()
		{
			if (isOperator == false)
			{
				text += "*";
				isOperator = true;
				whatOperator = '*';
				isDot = false;
			}
			Update_Text();
		}
		private void Div()
		{
			if (isOperator == false)
			{
				text += "/";
				isOperator = true;
				whatOperator = '/';
				isDot = false;
			}
			Update_Text();
		}

		private void Equal()
		{
			if (isOperator == true && text[text.Length - 1] != '+' && text[text.Length - 1] != '-' && text[text.Length - 1] != '*' && text[text.Length - 1] != '/')
			{
				if(isMinus==true)
				{
					text = text.Remove(0, 1);
				}

				char[] spearator = { '+', '-', '*', '/' };
				String[] numbers = text.Split(spearator);
				double num1, num2;
				numbers[0] = numbers[0].Replace('.', ',');
				numbers[1] = numbers[1].Replace('.', ',');
				Double.TryParse(numbers[0], out num1);
				Double.TryParse(numbers[1], out num2);
				double? result = null;

				if(isMinus == true)
				{
					num1 = -num1;
				}

				switch (whatOperator)
				{
					case '+':
						{
							result = num1 + num2;
							break;
						}
					case '-':
						{
							result = num1 - num2;
							break;
						}
					case '*':
						{
							result = num1 * num2;
							break;
						}
					case '/':
						{
							if (num2 != 0)
								result = num1 / num2;
							break;
						}
				}

				if (result != null)
				{
					if (result < 0)
						isMinus = true;
					else
						isMinus = false;

					result = Math.Round(Convert.ToDouble(result), 5, MidpointRounding.ToEven);
					text = Convert.ToString(result);
					text = text.Replace(',', '.');
					isOperator = false;
					if (result % 1 != 0)
						isDot = true;
					else
						isDot = false;
					Update_Text();
				}
			}
		}

		private void Dot()
		{
			if (isDot == false)
			{
				if (text[text.Length - 1] != '+' && text[text.Length - 1] != '-' && text[text.Length - 1] != '*' && text[text.Length - 1] != '/')
				{
					text += ".";
					isDot = true;
					Update_Text();
				}
			}
		}

		private void Back()
		{
			if (text.Length == 0 || text.Length == 1)
			{
				text = "0";
			}
			else
			{
				if (text[text.Length - 1] == '+' || text[text.Length - 1] == '-' || text[text.Length - 1] == '*' || text[text.Length - 1] == '/')
					isOperator = false;
				if (text[text.Length - 1] == '.')
					isDot = false;
				text = text.Remove(text.Length - 1, 1);
			}
			Update_Text();
		}

		private void Number_Add(string number)
		{
			if (text == "0")
				text = number;
			else
			{
				if (isOperator == false)
				{
					if (text.Length < 14)
						text += number;
				}
				else
				{
					if (text.Length < 29)
						text += number;
				}
				//text += number;
			}
			Update_Text();

		}

		private void Number_Click(object sender, RoutedEventArgs e)
		{
			string number = (sender as Button).Content.ToString();
			Number_Add(number);
		}

		private void Button_Dot(object sender, RoutedEventArgs e)
		{
			Dot();
		}

		private void Button_OneDelete(object sender, RoutedEventArgs e)
		{
			Back();
		}

		private void Button_AllDelete(object sender, RoutedEventArgs e)
		{
			text = "0";
			isOperator = false;
			isDot = false;
			Update_Text();
		}


		private void Button_Add(object sender, RoutedEventArgs e)
		{
			Add();
		}

		private void Button_Sub(object sender, RoutedEventArgs e)
		{
			Sub();
		}

		private void Button_Mult(object sender, RoutedEventArgs e)
		{
			Mult();
		}

		private void Button_Div(object sender, RoutedEventArgs e)
		{
			Div();
		}

		private void Button_Equal(object sender, RoutedEventArgs e)
		{
			Equal();
		}

		private void Button_Change(object sender, RoutedEventArgs e)
		{
			if (isOperator == false)
			{
				if(isMinus==true)
				{
					text = text.Remove(0, 1);
					isMinus = false;
				}
				else
				{
					text = "-" + text;
					isMinus = true;
				}
				Update_Text();
			}
		}
	}
}
