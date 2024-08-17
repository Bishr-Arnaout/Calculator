using Guna.UI2.WinForms;
using System;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private double _Result = 0;
        private char _Operation = ' ';
        private double _Number1 = 0;
        private double _Number2 = 0;
        public Form1()
        {
            InitializeComponent();

            ResetAgeScreen();
        }
        private void ResetAgeScreen()
        {
            dtpToday.Value = DateTime.Now;
        }
        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtOperation.Text == string.Empty && lblResult.Text == "0") return;

            if (txtOperation.Text == string.Empty && lblResult.Text.Length == 2 && lblResult.Text[0] == '-')
            {
                lblResult.Text = "0";
                _Result = 0;
            }
            else if (txtOperation.Text == string.Empty && lblResult.Text.Length > 1)
            {
                lblResult.Text = lblResult.Text.Substring(0, lblResult.Text.Length - 1);
                _Result = Convert.ToDouble(lblResult.Text);
            }
            else if (txtOperation.Text == string.Empty && lblResult.Text.Length == 1)
            {
                lblResult.Text = "0";
                _Result = 0;
            }
            else txtOperation.Text = txtOperation.Text.Substring(0, txtOperation.Text.Length - 1);
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOperation.Text = string.Empty;
            lblOperation.Text = string.Empty;
            lblResult.Text = "0";
            _Result = 0;
            _Number1 = 0;
            _Number2 = 0;
            _Operation = ' ';
        }
        private void btnResult_Click(object sender, EventArgs e)
        {
            if (_Operation != '%')
                if (txtOperation.Text == string.Empty) return;
                else _Number2 = Convert.ToDouble(txtOperation.Text);

            switch (_Operation)
            {
                case ' ':
                    break;
                case '+':
                    _Result = _Number1 + _Number2;
                    break;
                case '-':
                    _Result = _Number1 - _Number2;
                    break;
                case 'x':
                    _Result = _Number1 * _Number2;
                    break;
                case '/':
                    _Result = _Number1 / _Number2;
                    break;
                case '%':
                    _Result = _Number1 / 100;
                    break;
                case '^':
                    _Result = Math.Pow(_Number1, _Number2);
                    break;
            }
            txtOperation.Text = "";
            lblOperation.Text = "";
            _Number1 = 0;
            _Number2 = 0;
            _Operation = ' ';
            lblResult.Text = _Result.ToString();
        }
        private void NumberClicked(object sender, EventArgs e)
        {
            if (txtOperation.Text == string.Empty && ((Guna2Button)sender).Text == "0")
                return;

            if (txtOperation.Text == string.Empty && ((Guna2Button)sender).Text == ".") txtOperation.Text = "0.";
            else txtOperation.Text += ((Guna2Button)sender).Text;
        }
        private void OperationClicked(object sender, EventArgs e)
        {
            lblOperation.Text = ((Guna2Button)sender).Text;

            if (txtOperation.Text != "" && lblOperation.Text == "%")
            {
                _Number1 = Convert.ToDouble(txtOperation.Text);
                _Operation = Convert.ToChar(((Guna2Button)sender).Text);
                btnResult_Click(sender, e);
                return;
            }
            else if (lblResult.Text != "" && lblOperation.Text == "%")
            {
                _Number1 = Convert.ToDouble(lblResult.Text);
                _Operation = Convert.ToChar(((Guna2Button)sender).Text);
                btnResult_Click(sender, e);
                return;
            }
            if (txtOperation.Text != "") _Number1 = Convert.ToDouble(txtOperation.Text);
            else _Number1 = _Result;

            lblResult.Text = _Number1.ToString();

            txtOperation.Text = "";
            _Operation = Convert.ToChar(((Guna2Button)sender).Text);
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lblResult.Text != "0") Clipboard.SetText(lblResult.Text);

            else MessageBox.Show("You haven't done any operation!", "It's Empty!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (double.TryParse(Clipboard.GetText(), out double TempNumber)) lblResult.Text = TempNumber.ToString();

            else MessageBox.Show("You cannot paste any type exept numbers!", "Wrong data type!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnClear_Click(sender, e);
        }
        private bool IsDateWrong()
        {
            if (dtpBirthday.Value.CompareTo(dtpToday.Value) == 1)
            {
                dtpBirthday.Value = dtpToday.Value;
                lblYears.Text = "0";
                lblYearstxt.Text = "Year";
                lblMonths.Text = "0 Month | ";
                lblDays.Text = "0 Day";
                return true;
            }
            return false;
        }
        private void GetDiffrencesBetweenYears()
        {
            if (dtpBirthday.Value.Year.CompareTo(dtpToday.Value.Year) == 0)
            {
                lblYears.Text = "0";
                lblYearstxt.Text = "Year";
            }
            else
            {
                if (dtpToday.Value.Year - dtpBirthday.Value.Year == 1) lblYearstxt.Text = "Year";
                else lblYearstxt.Text = "Years";

                lblYears.Text = (dtpToday.Value.Year - dtpBirthday.Value.Year).ToString();
            }
        }
        private void GetDiffrencesBetweenMonths()
        {
            if (dtpBirthday.Value.Month.CompareTo(dtpToday.Value.Month) == 0) lblMonths.Text = "0";
            else if (dtpBirthday.Value.Month.CompareTo(dtpToday.Value.Month) == 1)
            {
                lblMonths.Text = (12 - (dtpBirthday.Value.Month - dtpToday.Value.Month)).ToString();

                lblYears.Text = (Convert.ToInt32(lblYears.Text) - 1).ToString();
            }
            else lblMonths.Text = (dtpToday.Value.Month - dtpBirthday.Value.Month).ToString();

            if (Convert.ToInt32(lblMonths.Text) <= 1)
                lblMonthstxt.Text = "Month |";
            else lblMonthstxt.Text = "Months |";
        }
        private void GetDiffrencesBetweenDays()
        {
            if (dtpBirthday.Value.Day.CompareTo(dtpToday.Value.Day) == 0) lblDays.Text = "0";
            else if (dtpBirthday.Value.Day.CompareTo(dtpToday.Value.Day) == 1)
            {
                if (lblMonths.Text == "0") lblYears.Text = (Convert.ToInt32(lblYears.Text) - 1).ToString();

                if (dtpBirthday.Value.Month.CompareTo(dtpToday.Value.Month) == 0)
                {
                    lblMonths.Text = "11";
                    lblMonthstxt.Text = "Months |";
                }
                else lblMonths.Text = (Convert.ToInt32(lblMonths.Text) - 1).ToString();
                if (lblMonths.Text == "0" || lblMonths.Text == "1") lblMonthstxt.Text = "Month |";


                if ((dtpToday.Value.Month == 2) || (dtpToday.Value.Month == 4) || (dtpToday.Value.Month == 6)
                    || (dtpToday.Value.Month == 9) || (dtpToday.Value.Month == 11))
                    lblDays.Text = (31 - (dtpBirthday.Value.Day - dtpToday.Value.Day)).ToString();
                else lblDays.Text = (30 - (dtpBirthday.Value.Day - dtpToday.Value.Day)).ToString();
            }
            else lblDays.Text = (dtpToday.Value.Day - dtpBirthday.Value.Day).ToString();

            if (Convert.ToInt32(lblDays.Text) <= 1)
                lblDaystxt.Text = " Day";
            else lblDaystxt.Text = " Days";
        }
        private void GetTheDiffrenceBetweenDates()
        {
            if (IsDateWrong()) return;

            GetDiffrencesBetweenYears();
            GetDiffrencesBetweenMonths();
            GetDiffrencesBetweenDays();
        }
        private void dtpBirthday_ValueChanged(object sender, EventArgs e)
        {
            GetTheDiffrenceBetweenDates();
            GetNextBirthday();
            GetSummary();
        }
        private void dtpToday_ValueChanged(object sender, EventArgs e)
        {
            GetTheDiffrenceBetweenDates();
            GetNextBirthday();
            GetSummary();
        }
        private bool IsBirthdayHappend()
        {
            DateTime Now = dtpToday.Value;
            DateTime ThisBirthday = new DateTime(dtpToday.Value.Year, dtpBirthday.Value.Month, dtpBirthday.Value.Day);

            if (ThisBirthday.CompareTo(Now) == 1) return true;
            return false;
        }
        private void GetNextBirthday()
        {
            if (dtpBirthday.Value == dtpToday.Value) lblNextBirthday.Text = "12 Months | 0 Day";
            else
            {
                lblNextBirthday.Text = (12 - (Convert.ToInt32(lblMonths.Text) + 1)).ToString();
                if (Convert.ToInt32(lblNextBirthday.Text) <= 1) lblNextBirthday.Text += " Month | ";
                else lblNextBirthday.Text += " Months | ";

                if ((dtpToday.Value.Month == 2) || (dtpToday.Value.Month == 4) || (dtpToday.Value.Month == 6)
                    || (dtpToday.Value.Month == 9) || (dtpToday.Value.Month == 11))
                    lblNextBirthday.Text += (31 - Convert.ToInt32(lblDays.Text)).ToString();
                else lblNextBirthday.Text += (30 - Convert.ToInt32(lblDays.Text)).ToString();

                if ((dtpToday.Value.Month == 2) || (dtpToday.Value.Month == 4) || (dtpToday.Value.Month == 6)
                    || (dtpToday.Value.Month == 9) || (dtpToday.Value.Month == 11))
                {
                    if (31 - Convert.ToInt32(lblDays.Text) <= 1) lblNextBirthday.Text += " Day";
                    else lblNextBirthday.Text += " Days";
                }
                else
                {
                    if (30 - Convert.ToInt32(lblDays.Text) <= 1) lblNextBirthday.Text += " Day";
                    else lblNextBirthday.Text += " Days";
                }
            }

            DateTime NextBirthday;
            if (IsBirthdayHappend()) NextBirthday = new DateTime(dtpToday.Value.Year + 1, dtpBirthday.Value.Month, dtpBirthday.Value.Day);
            else NextBirthday = new DateTime(dtpToday.Value.Year, dtpBirthday.Value.Month, dtpBirthday.Value.Day);

            lblNextBirthdaytxt.Text = NextBirthday.DayOfWeek.ToString();
        }
        private void CalculateTotalYears()
        {
            lblTotalYears.Text = lblYears.Text;
        }
        private void CalculateTotalMonths()
        {
            lblTotalMonths.Text = (Convert.ToInt32(lblMonths.Text) + (Convert.ToInt32(lblYears.Text) * 12)).ToString();
        }
        private void CalculateTotalWeeks()
        {
            lblTotalWeeks.Text = ((Convert.ToInt32(lblYears.Text) * 52) + (Convert.ToInt32(lblMonths.Text) * 4) + (Convert.ToInt32(lblDays.Text) / 7)).ToString();
        }
        private void CalculateTotalDays()
        {
            long TotalDays = (Int64)((Convert.ToInt64(lblYears.Text) * 365.25) + (Convert.ToInt64(lblDays.Text)));
            lblTotalDays.Text = TotalDays.ToString();
        }
        private void CalculateTotalHours()
        {
            lblTotalHours.Text = (Convert.ToInt64(lblTotalDays.Text) * 24).ToString();
        }
        private void CalculateTotalMinutes()
        {
            lblTotalMinutes.Text = (Convert.ToInt64(lblTotalHours.Text) * 60).ToString();
        }
        private void GetSummary()
        {
            CalculateTotalYears();
            CalculateTotalMonths();
            CalculateTotalWeeks();
            CalculateTotalDays();
            CalculateTotalHours();
            CalculateTotalMinutes();
        }
    }
}