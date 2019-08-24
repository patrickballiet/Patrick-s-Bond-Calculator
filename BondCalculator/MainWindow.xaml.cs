using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace BondCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double parValue, couponRate, annuallyInterestRate;
        int maturity;


       
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }



        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult choice = MessageBox.Show("Confirm if you want to exit", "Patrick's Bond Calculator", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (choice == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {

                e.Handled = false;
            }
        }

        

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            txtParValue.Visibility = txtMaturity.Visibility = txtCompounding.Visibility = txtCouponRate.Visibility = txtInterestRate.Visibility = Visibility.Visible;
            txtParValue.Text = txtMaturity.Text = txtCompounding.Text = txtCouponRate.Text = txtInterestRate.Text = "";
            lblParValue_Output.Visibility = lblMaturity_Output.Visibility = lblCompounding_Output.Visibility = lblCouponRate_Output.Visibility = lblInterestRate_Output.Visibility = Visibility.Hidden;
            btnRunCalculation.IsEnabled = true;
            txtResult.Document.Blocks.Clear();
            lblErrorFill.Visibility = Visibility.Hidden;
            imgLblErrorFill.Visibility = Visibility.Hidden;
            lblErrorFormat.Visibility = Visibility.Hidden;
            imgLblErrorFormat.Visibility = Visibility.Hidden;
            lblResetInfo.Visibility = Visibility.Hidden;

        }

        private void BtnRunCalculation_Click(object sender, RoutedEventArgs e)
        {
            lblErrorFill.Visibility = Visibility.Hidden;
            imgLblErrorFill.Visibility = Visibility.Hidden;
            lblErrorFormat.Visibility = Visibility.Hidden;
            imgLblErrorFormat.Visibility = Visibility.Hidden;

            if (txtParValue.Text == "" || txtMaturity.Text == "" || txtCompounding.Text == "" || txtCouponRate.Text == "" || txtInterestRate.Text == "")
            {
                lblErrorFill.Visibility = Visibility.Visible;
                imgLblErrorFill.Visibility = Visibility.Visible;
            }

            else
            {

                try
                {

                double inputParValue = Convert.ToDouble(txtParValue.Text), inputCouponRate = Convert.ToDouble(txtCouponRate.Text), inputInterestRate = Convert.ToDouble(txtInterestRate.Text);
                int inputMaturity = Convert.ToInt32(txtMaturity.Text);
                string inputCompounding = txtCompounding.Text;

               

               

                
                    txtParValue.Visibility = txtMaturity.Visibility = txtCompounding.Visibility = txtCouponRate.Visibility = txtInterestRate.Visibility = Visibility.Hidden;
                    lblParValue_Output.Visibility = lblMaturity_Output.Visibility = lblCompounding_Output.Visibility = lblCouponRate_Output.Visibility = lblInterestRate_Output.Visibility = Visibility.Visible;
                    btnRunCalculation.IsEnabled = false;
                    lblResetInfo.Visibility = Visibility.Visible;

                    // PAR VALUE OUTPUT

                    parValue = Convert.ToDouble(txtParValue.Text);
                    txtParValue.Text = String.Format("{0:C}", parValue);
                    lblParValue_Output.Content = txtParValue.Text;

                    //MATURITY OUTPUT

                    maturity = Convert.ToInt32(txtMaturity.Text);
                    lblMaturity_Output.Content = Convert.ToString(maturity);

                    //COUPON RATE

                    couponRate = Convert.ToDouble(txtCouponRate.Text);
                    lblCouponRate_Output.Content = Convert.ToString(couponRate);

                    // INTEREST RATE OUTPUT
                    annuallyInterestRate = Convert.ToDouble(txtInterestRate.Text);
                    lblInterestRate_Output.Content = Convert.ToString(annuallyInterestRate);


                    // COMPOUNDING
                    lblCompounding_Output.Content = Convert.ToString(txtCompounding.Text);

                    // RESULT

                    if (txtCompounding.Text == "Annually")
                    {

                        double CouponRate = Convert.ToDouble(txtCouponRate.Text) / 100;

                        double InterestRate = Convert.ToDouble(txtInterestRate.Text) / 100;

                        double AnnualCouponAmount = parValue * CouponRate;

                        double maturity = Convert.ToInt32(txtMaturity.Text);

                        double BondPrice = AnnualCouponAmount * ((1 - Math.Pow(1 + InterestRate, -maturity)) / InterestRate) + parValue / Math.Pow(1 + InterestRate, maturity);

                        double Yield = (AnnualCouponAmount / BondPrice) * 100;


                        txtResult.AppendText("\t" + "\t" + "       Patrick's Bond Calculator" + "\t"
                              + "--------------------------------------------------------" + "\t");
                        txtResult.AppendText("\n" + "               Par/Face Value : " + lblParValue_Output.Content + "\n");
                        txtResult.AppendText("\t" + "Annual Market Interest Rate: " + String.Format("{0:C}", lblInterestRate_Output.Content) + "%" + "\n");
                        txtResult.AppendText("\t" + "Annual Coupon Rate: " + String.Format("{0:C}", lblCouponRate_Output.Content) + "%" + "\n");
                        txtResult.AppendText("\t" + "Annual Coupon Payment: " + String.Format("{0:C}", AnnualCouponAmount) + "\n");
                        txtResult.AppendText("\t" + "No of annual payments: " + lblMaturity_Output.Content + "\n");
                        txtResult.AppendText("\t" + "Bond Price: " + String.Format("{0:C}", BondPrice) + "\n");
                        txtResult.AppendText("\t" + " Annual Yield: " + Math.Round(Yield, 2) + "%" + "\n");
                        txtResult.AppendText("       ---------------------      THANK YOU      ----------------------");



                    }

                    else if (txtCompounding.Text == "Semiannually")

                    {
                        double SemiannualCouponRate = (Convert.ToDouble(txtCouponRate.Text) / 100) / 2;

                        double SemiannualInterestRate = (Convert.ToDouble(txtInterestRate.Text) / 100) / 2;

                        double SemiannualCouponAmount = parValue * SemiannualCouponRate;

                        double maturity = Convert.ToInt32(txtMaturity.Text) * 2;

                        double BondPrice = SemiannualCouponAmount * ((1 - Math.Pow(1 + SemiannualInterestRate, -maturity)) / SemiannualInterestRate) + parValue / Math.Pow(1 + SemiannualInterestRate, maturity);

                        double Yield = (SemiannualCouponAmount / BondPrice) * 100;


                        txtResult.AppendText("\t" + "\t" + "       Patrick's Bond Calculator" + "\t"
                              + "--------------------------------------------------------" + "\t");
                        txtResult.AppendText("\n" + "               Par/Face Value : " + lblParValue_Output.Content + "\n");
                        txtResult.AppendText("\t" + "Semiannual Market Interest Rate: " + Math.Round(SemiannualInterestRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Semiannual Coupon Rate: " + Math.Round(SemiannualCouponRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Semiannual Coupon Payment: " + String.Format("{0:C}", SemiannualCouponAmount) + "\n");
                        txtResult.AppendText("\t" + "No of Semiannual payments: " + maturity + "\n");
                        txtResult.AppendText("\t" + "Bond Price: " + String.Format("{0:C}", BondPrice) + "\n");
                        txtResult.AppendText("\t" + "Semiannual Yield: " + Math.Round(Yield, 2) + "%" + "\n");
                        txtResult.AppendText("       ---------------------      THANK YOU      ----------------------");
                    }
                    else if (txtCompounding.Text == "Quarterly")
                    {
                        double QuarterlyCouponRate = (Convert.ToDouble(txtCouponRate.Text) / 100) / 4;

                        double QuarterlyInterestRate = (Convert.ToDouble(txtInterestRate.Text) / 100) / 4;

                        double QuarterlyCouponAmount = parValue * QuarterlyCouponRate;

                        double maturity = Convert.ToInt32(txtMaturity.Text) * 4;

                        double BondPrice = QuarterlyCouponAmount * ((1 - Math.Pow(1 + QuarterlyInterestRate, -maturity)) / QuarterlyInterestRate) + parValue / Math.Pow(1 + QuarterlyInterestRate, maturity);

                        double Yield = (QuarterlyCouponAmount / BondPrice) * 100;


                        txtResult.AppendText("\t" + "\t" + "       Patrick's Bond Calculator" + "\t"
                              + "--------------------------------------------------------" + "\t");
                        txtResult.AppendText("\n" + "               Par/Face Value : " + lblParValue_Output.Content + "\n");
                        txtResult.AppendText("\t" + "Quarterly Market Interest Rate: " + Math.Round(QuarterlyInterestRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Quarterly Coupon Rate: " + Math.Round(QuarterlyCouponRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Quaterly Coupon Payment: " + String.Format("{0:C}", QuarterlyCouponAmount) + "\n");
                        txtResult.AppendText("\t" + "No of Quarterly payments: " + maturity + "\n");
                        txtResult.AppendText("\t" + "Bond Price: " + String.Format("{0:C}", BondPrice) + "\n");
                        txtResult.AppendText("\t" + "Quarterly Yield: " + Math.Round(Yield, 2) + "%" + "\n");
                        txtResult.AppendText("       ---------------------      THANK YOU      ----------------------");
                    }

                    else if (txtCompounding.Text == "Monthly")
                    {
                        double MonthlyCouponRate = (Convert.ToDouble(txtCouponRate.Text) / 100) / 12;

                        double MonthlyInterestRate = (Convert.ToDouble(txtInterestRate.Text) / 100) / 12;

                        double MonthlyCouponAmount = parValue * MonthlyCouponRate;

                        double maturity = Convert.ToInt32(txtMaturity.Text) * 12;

                        double BondPrice = MonthlyCouponAmount * ((1 - Math.Pow(1 + MonthlyInterestRate, -maturity)) / MonthlyInterestRate) + parValue / Math.Pow(1 + MonthlyInterestRate, maturity);

                        double Yield = (MonthlyCouponAmount / BondPrice) * 100;


                        txtResult.AppendText("\t" + "\t" + "       Patrick's Bond Calculator" + "\t"
                              + "--------------------------------------------------------" + "\t");
                        txtResult.AppendText("\n" + "               Par/Face Value : " + lblParValue_Output.Content + "\n");
                        txtResult.AppendText("\t" + "Monthly Market Interest Rate: " + Math.Round(MonthlyInterestRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Monthly Coupon Rate: " + Math.Round(MonthlyCouponRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Monthly Coupon Payment: " + String.Format("{0:C}", MonthlyCouponAmount) + "\n");
                        txtResult.AppendText("\t" + "No of Semiannual payments: " + maturity + "\n");
                        txtResult.AppendText("\t" + "Bond Price: " + String.Format("{0:C}", BondPrice) + "\n");
                        txtResult.AppendText("\t" + " Monthly Yield: " + Math.Round(Yield, 2) + "%" + "\n");
                        txtResult.AppendText("       ---------------------      THANK YOU      ----------------------");
                    }

                    else if (txtCompounding.Text == "Weekly")
                    {
                        double WeeklyCouponRate = (Convert.ToDouble(txtCouponRate.Text) / 100) / 48;

                        double WeeklyInterestRate = (Convert.ToDouble(txtInterestRate.Text) / 100) / 48;

                        double WeeklyCouponAmount = parValue * WeeklyCouponRate;

                        double maturity = Convert.ToInt32(txtMaturity.Text) * 48;

                        double BondPrice = WeeklyCouponAmount * ((1 - Math.Pow(1 + WeeklyInterestRate, -maturity)) / WeeklyInterestRate) + parValue / Math.Pow(1 + WeeklyInterestRate, maturity);

                        double Yield = (WeeklyCouponAmount / BondPrice) * 100;


                        txtResult.AppendText("\t" + "\t" + "       Patrick's Bond Calculator" + "\t"
                              + "--------------------------------------------------------" + "\t");
                        txtResult.AppendText("\n" + "               Par/Face Value : " + lblParValue_Output.Content + "\n");
                        txtResult.AppendText("\t" + "Weekly Market Interest Rate: " + Math.Round(WeeklyInterestRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Weekly Coupon Rate: " + Math.Round(WeeklyCouponRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Weekly Coupon Payment: " + String.Format("{0:C}", WeeklyCouponAmount) + "\n");
                        txtResult.AppendText("\t" + "No of Weekly payments: " + maturity + "\n");
                        txtResult.AppendText("\t" + "Bond Price: " + String.Format("{0:C}", BondPrice) + "\n");
                        txtResult.AppendText("\t" + " Weekly Yield: " + Math.Round(Yield, 2) + "%" + "\n");
                        txtResult.AppendText("       ---------------------      THANK YOU      ----------------------");
                    }

                    else if (txtCompounding.Text == "Daily")
                    {
                        double DailyCouponRate = (Convert.ToDouble(txtCouponRate.Text) / 100) / 365;

                        double DailyInterestRate = (Convert.ToDouble(txtInterestRate.Text) / 100) / 365;

                        double DailyCouponAmount = parValue * DailyCouponRate;

                        double maturity = Convert.ToInt32(txtMaturity.Text) * 365;

                        double BondPrice = DailyCouponAmount * ((1 - Math.Pow(1 + DailyInterestRate, -maturity)) / DailyInterestRate) + parValue / Math.Pow(1 + DailyInterestRate, maturity);

                        double Yield = (DailyCouponAmount / BondPrice) * 100;


                        txtResult.AppendText("\t" + "\t" + "       Patrick's Bond Calculator" + "\t"
                              + "--------------------------------------------------------" + "\t");
                        txtResult.AppendText("\n" + "               Par/Face Value : " + lblParValue_Output.Content + "\n");
                        txtResult.AppendText("\t" + "Daily Market Interest Rate: " + Math.Round(DailyInterestRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Daily Coupon Rate: " + Math.Round(DailyCouponRate * 100, 2) + "%" + "\n");
                        txtResult.AppendText("\t" + "Daily Coupon Payment: " + String.Format("{0:C}", DailyCouponAmount) + "\n");
                        txtResult.AppendText("\t" + "No of Daily payments: " + maturity + "\n");
                        txtResult.AppendText("\t" + "Bond Price: " + String.Format("{0:C}", BondPrice) + "\n");
                        txtResult.AppendText("\t" + "Daily Yield: " + Math.Round(Yield, 2) + "%" + "\n");
                        txtResult.AppendText("       ---------------------      THANK YOU      ----------------------");
                    }

                }

                catch
                {
                    lblErrorFormat.Visibility = Visibility.Visible;
                    imgLblErrorFormat.Visibility = Visibility.Visible;
                }

            }

           
            
        }
    }
}
