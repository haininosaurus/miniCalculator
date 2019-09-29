using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MiniCalculator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        static int count = 0;
        public MainPage()
        {
            InitializeComponent();            
        }
        // nút số 1, 2, 3, 4, 5, 6, 7, 8, 9, 0
        private void numberClick(object sender, EventArgs e)
        {
            if (result.Text == "0")
            {
                result.Text = "";
            }           
            Button button = (Button)sender;
            string press = button.Text;
            result.Text += press;
        }
        //nút dấu +, -, x, /
        private void operatorClick(object sender, EventArgs e)
        {
            count += 1;
            if (count <= 1)
            {
                Button button = (Button)sender;
                string press = button.Text;
                if (press == "DIV") press = "/";
                result.Text += press;
            }
        }
        //xử lí thao tác với dấu chấm trong số thập phân
        private void dotClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string press = button.Text;
            int index = getIndex(result.Text);
            int check = 0;
            //xử lí dấu chấm sau toán tử tính toán cho không trùng lặp
            //dùng check kiểm tra nếu đã xuất hiện dấu chấm rồi thì check = 1
            if (index != 0)
            {
                for (int i = index + 1; i < result.Text.Length; i++)
                {
                    if (result.Text[i] == '.')
                    {
                        check = 1;
                        break;
                    }
                }
            }
            //tương tự xử lí dấu chấm trước toán tử tính toán
            else
            {
                for (int i = 0; i < result.Text.Length; i++)
                {
                    if (result.Text[i] == '.')
                    {
                        check = 1;
                        break;
                    }
                }
            }
            if (check == 0) result.Text += press;   //nếu thỏa mãn không trùng thì hiển thị lên text result
        }

        //xử lí khi ấn dấu =
        private void resultClick(object sender, EventArgs e)
        {
            count = 0;      //đưa count về 0 để thực hiện lần tính toán tiếp theo
            string str = "+-x/";
            int index = getIndex(result.Text);
            //chia text result thành 3 phần rồi xử lí
            if (index != 0 && str.Contains(result.Text[result.Text.Length - 1]) == false)
            {
                string str1 = result.Text.Substring(0, index);
                string stroperator = result.Text.Substring(index, 1);
                string str2 = result.Text.Substring(index + 1, result.Text.Length - index - 1);

                double val1 = Double.Parse(str1);
                double val2 = Double.Parse(str2);

                result.Text = calculate(val1, val2, stroperator);
            }

        }
        //hàm tính toán
        public string calculate(double a, double b, string s)
        {
            double dResult = 0;
            string sResult = "";
            switch (s)
            {
                case "+":
                    dResult = a + b;
                    break;
                case "-":
                    dResult = a - b;
                    break;
                case "x":
                    dResult = a * b;
                    break;
                case "/":
                    dResult = a / b;
                    break;
            }
            sResult = dResult.ToString();
            return sResult;
        }
        //thao tác reset
        private void cClick(object sender, EventArgs e)
        {
            result.Text = "0";
            count = 0;
        }
        //xử lí thêm dấu âm dương
        private void plusMinusClick(object sender, EventArgs e)
        {
            string str = "x/-+";
            string minus = "-";
            if (str.Contains(result.Text[result.Text.Length - 1]) == false)
            {
                for(int i = result.Text.Length-1; ; i--)
                {
                    if(str.Contains(result.Text[i]) && i!=0)
                    {
                        if(str.Contains(result.Text[i]) && str.Contains(result.Text[i-1]))
                            result.Text = result.Text.Remove(i, 1);
                        else result.Text = result.Text.Insert(i+1, minus);                       
                        break;
                    }
                    else if (i == 0)
                    {
                        if (str.Contains(result.Text[i]))
                            result.Text = result.Text.Remove(i, 1);
                        else result.Text = result.Text.Insert(i, minus);
                        break;
                    }
                }
            }

        }
        //xử lí với phần trăm
        private void percentClick(object sender, EventArgs e)
        {
            string str = "+-x/";
            int index = getIndex(result.Text);
            double num = 0;
            if (index == 0 && str.Contains(result.Text[result.Text.Length-1])==false)
            {
                num = Double.Parse(result.Text);
                num /= 100;
                result.Text = num.ToString();
            }
            else
            {
                num = Double.Parse(result.Text.Substring(index + 1, result.Text.Length - index-1));
                result.Text = result.Text.Remove(index + 1);
                num /= 100;
                result.Text += num.ToString(); 
            }
        }
        private int getIndex(string text)
        {
            string str = "+-x/";
            int index = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (str.Contains(text[i]) && i != 0)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }

}
