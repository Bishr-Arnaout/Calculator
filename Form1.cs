using Guna.UI2.WinForms;
using System;
using System.Drawing.Text;
using System.Threading;
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
        private void CalculateAge(object sender, EventArgs e)
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
        private void ConvertToTheSameSystem(object sender)
        {
            txt2.Text = txt1.Text;
        }
        private void ConvertBinaryToOctal(object sender)
        {
            uint OctalNum = 0;
            string OctalString = "";
            int Power = 0;

            for (int i = 0; i < txt1.TextLength; i++)
            {
                if (Power == 3)
                {
                    Power = 0;
                    OctalString = OctalString.Insert(0, OctalNum.ToString());
                    OctalNum = 0;
                }

                if (txt1.Text[txt1.TextLength - 1 - i] == '1')
                    OctalNum += (uint)Math.Pow(2, Power);
                Power++;
            }
            OctalString = OctalString.Insert(0, OctalNum.ToString());

            txt2.Text = OctalString;
        }
        private void ConvertBinaryToDecimal(object sender)
        {
            uint DecimalNum = 0;

            for (int i = 0; i < txt1.TextLength; i++)
            {
                if (txt1.Text[txt1.TextLength - 1 - i] == '1')
                    DecimalNum += (uint)Math.Pow(2, i);
            }
            txt2.Text = DecimalNum.ToString();
        }
        private void ConvertBinaryToHexadecimal(object sender)
        {
            uint HexadecimalNum = 0;
            int Power = 0;
            string HexadecimalString = "";

            for (int i = 0; i < txt1.TextLength; i++)
            {
                if (Power == 4)
                {
                    Power = 0;
                    if (HexadecimalNum == 10) HexadecimalString = HexadecimalString.Insert(0, "A");
                    else if (HexadecimalNum == 11) HexadecimalString = HexadecimalString.Insert(0, "B");
                    else if (HexadecimalNum == 12) HexadecimalString = HexadecimalString.Insert(0, "C");
                    else if (HexadecimalNum == 13) HexadecimalString = HexadecimalString.Insert(0, "D");
                    else if (HexadecimalNum == 14) HexadecimalString = HexadecimalString.Insert(0, "E");
                    else if (HexadecimalNum == 15) HexadecimalString = HexadecimalString.Insert(0, "F");
                    else HexadecimalString = HexadecimalString.Insert(0, HexadecimalNum.ToString());
                    HexadecimalNum = 0;
                }

                if (txt1.Text[txt1.TextLength - 1 - i] == '1')
                    HexadecimalNum += (uint)Math.Pow(2, Power);
                Power++;
            }
            if (HexadecimalNum == 10) HexadecimalString = HexadecimalString.Insert(0, "A");
            else if (HexadecimalNum == 11) HexadecimalString = HexadecimalString.Insert(0, "B");
            else if (HexadecimalNum == 12) HexadecimalString = HexadecimalString.Insert(0, "C");
            else if (HexadecimalNum == 13) HexadecimalString = HexadecimalString.Insert(0, "D");
            else if (HexadecimalNum == 14) HexadecimalString = HexadecimalString.Insert(0, "E");
            else if (HexadecimalNum == 15) HexadecimalString = HexadecimalString.Insert(0, "F");
            else HexadecimalString = HexadecimalString.Insert(0, HexadecimalNum.ToString());

            txt2.Text = HexadecimalString;
        }
        private void ConvertOctalToBinary(object sender)
        {
            string BinaryString = "";
            for (int i = 0; i < txt1.TextLength; i++)
            {
                switch(txt1.Text[txt1.TextLength - 1 - i])
                {
                    case '0':
                        BinaryString = BinaryString.Insert(0, "000");
                        break;
                    case '1':
                        BinaryString = BinaryString.Insert(0, "001");
                        break;
                    case '2':
                        BinaryString = BinaryString.Insert(0, "010");
                        break;
                    case '3':
                        BinaryString = BinaryString.Insert(0, "011");
                        break;
                    case '4':
                        BinaryString = BinaryString.Insert(0, "100");
                        break;
                    case '5':
                        BinaryString = BinaryString.Insert(0, "101");
                        break;
                    case '6':
                        BinaryString = BinaryString.Insert(0, "101");
                        break;
                    case '7':
                        BinaryString = BinaryString.Insert(0, "111");
                        break;
                }
            }
            if (BinaryString == string.Empty) BinaryString = "0";
            else
                while (BinaryString[0] != '1') BinaryString = BinaryString.Substring(1);

            txt2.Text = BinaryString;
        }
        private void ConvertOctalToDecimal(object sender)
        {
            uint DecimalNumber = 0;

            for (int i = 0; i < txt1.TextLength; i++)
            {
                int CurrentDigit = Convert.ToInt32(txt1.Text[txt1.TextLength - 1 - i] - 48);
                DecimalNumber += (uint)(Math.Pow(8, i) * CurrentDigit);
            }
            txt2.Text = DecimalNumber.ToString();
        }
        private void ConvertOctalToHexadecimal(object sender)
        {
            string BinaryString = "";
            for (int i = 0; i < txt1.TextLength; i++)
            {
                switch (txt1.Text[txt1.TextLength - 1 - i])
                {
                    case '0':
                        BinaryString = BinaryString.Insert(0, "000");
                        break;
                    case '1':
                        BinaryString = BinaryString.Insert(0, "001");
                        break;
                    case '2':
                        BinaryString = BinaryString.Insert(0, "010");
                        break;
                    case '3':
                        BinaryString = BinaryString.Insert(0, "011");
                        break;
                    case '4':
                        BinaryString = BinaryString.Insert(0, "100");
                        break;
                    case '5':
                        BinaryString = BinaryString.Insert(0, "101");
                        break;
                    case '6':
                        BinaryString = BinaryString.Insert(0, "101");
                        break;
                    case '7':
                        BinaryString = BinaryString.Insert(0, "111");
                        break;
                }
            }
            if (BinaryString == string.Empty) BinaryString = "0";
            else
                while (BinaryString[0] != '1') BinaryString = BinaryString.Substring(1);

            uint HexadecimalNum = 0;
            string HexadecimalString = "";
            int Power = 0;

            for (int i = 0; i < BinaryString.Length; i++)
            {
                if (Power == 4)
                {
                    Power = 0;
                    if (HexadecimalNum == 10) HexadecimalString = HexadecimalString.Insert(0, "A");
                    else if (HexadecimalNum == 11) HexadecimalString = HexadecimalString.Insert(0, "B");
                    else if (HexadecimalNum == 12) HexadecimalString = HexadecimalString.Insert(0, "C");
                    else if (HexadecimalNum == 13) HexadecimalString = HexadecimalString.Insert(0, "D");
                    else if (HexadecimalNum == 14) HexadecimalString = HexadecimalString.Insert(0, "E");
                    else if (HexadecimalNum == 15) HexadecimalString = HexadecimalString.Insert(0, "F");
                    else HexadecimalString = HexadecimalString.Insert(0, HexadecimalNum.ToString());
                    HexadecimalNum = 0;
                }

                if (BinaryString[BinaryString.Length - 1 - i] == '1')
                    HexadecimalNum += (uint)Math.Pow(2, Power);
                Power++;
            }
            if (HexadecimalNum == 10) HexadecimalString = HexadecimalString.Insert(0, "A");
            else if (HexadecimalNum == 11) HexadecimalString = HexadecimalString.Insert(0, "B");
            else if (HexadecimalNum == 12) HexadecimalString = HexadecimalString.Insert(0, "C");
            else if (HexadecimalNum == 13) HexadecimalString = HexadecimalString.Insert(0, "D");
            else if (HexadecimalNum == 14) HexadecimalString = HexadecimalString.Insert(0, "E");
            else if (HexadecimalNum == 15) HexadecimalString = HexadecimalString.Insert(0, "F");
            else HexadecimalString = HexadecimalString.Insert(0, HexadecimalNum.ToString());

            txt2.Text = HexadecimalString;
        }
        private void ConvertDecimalToBinary(object sender)
        {
            if (txt1.Text == string.Empty)
            {
                txt2.Text = "0";
                return;
            }
            if (txt1.Text == "0")
            {
                txt2.Text = "0";
                return;
            }
            ulong NumberAfterDivision = Convert.ToUInt64(txt1.Text);
            string BinaryString = "";

            while (NumberAfterDivision != 0)
            {
                BinaryString = BinaryString.Insert(0, (NumberAfterDivision % 2).ToString());
                NumberAfterDivision /= 2;
            }
            txt2.Text = BinaryString;
        }
        private void ConvertDecimalToOctal(object sender)
        {
            if (txt1.Text == string.Empty)
            {
                txt2.Text = "0";
                return;
            }
            if (txt1.Text == "0")
            {
                txt2.Text = "0";
                return;
            }
            ulong NumberAfterDivision = Convert.ToUInt64(txt1.Text);
            string OctalString = "";

            while (NumberAfterDivision != 0)
            {
                OctalString = OctalString.Insert(0, (NumberAfterDivision % 8).ToString());
                NumberAfterDivision /= 8;
            }
            txt2.Text = OctalString;
        }
        private void ConvertDecimalToHexadecimal(object sender)
        {
            if (txt1.Text == string.Empty)
            {
                txt2.Text = "0";
                return;
            }
            if (txt1.Text == "0")
            {
                txt2.Text = "0";
                return;
            }
            ulong NumberAfterDivision = Convert.ToUInt64(txt1.Text);
            string HexadecimalString = "";

            while (NumberAfterDivision != 0)
            {
                if (NumberAfterDivision % 16 == 10) HexadecimalString = HexadecimalString.Insert(0, "A");
                else if (NumberAfterDivision % 16 == 11) HexadecimalString = HexadecimalString.Insert(0, "B");
                else if (NumberAfterDivision % 16 == 12) HexadecimalString = HexadecimalString.Insert(0, "C");
                else if (NumberAfterDivision % 16 == 13) HexadecimalString = HexadecimalString.Insert(0, "D");
                else if (NumberAfterDivision % 16 == 14) HexadecimalString = HexadecimalString.Insert(0, "E");
                else if (NumberAfterDivision % 16 == 15) HexadecimalString = HexadecimalString.Insert(0, "F");
                else HexadecimalString = HexadecimalString.Insert(0, (NumberAfterDivision % 16).ToString());
                NumberAfterDivision /= 16;
            }
            txt2.Text = HexadecimalString;
        }
        private void ConvertHexadecimalToBinary(object sender)
        {
            string BinaryString = "";

            for (int i = 0; i < txt1.TextLength; i++)
            {
                switch(txt1.Text[txt1.TextLength - 1 - i])
                {
                    case '0':
                        BinaryString = BinaryString.Insert(0, "0000");
                        break;
                    case '1':
                        BinaryString = BinaryString.Insert(0, "0001");
                        break;
                    case '2':
                        BinaryString = BinaryString.Insert(0, "0010");
                        break;
                    case '3':
                        BinaryString = BinaryString.Insert(0, "0011");
                        break;
                    case '4':
                        BinaryString = BinaryString.Insert(0, "0100");
                        break;
                    case '5':
                        BinaryString = BinaryString.Insert(0, "0101");
                        break;
                    case '6':
                        BinaryString = BinaryString.Insert(0, "0110");
                        break;
                    case '7':
                        BinaryString = BinaryString.Insert(0, "0111");
                        break;
                    case '8':
                        BinaryString = BinaryString.Insert(0, "1000");
                        break;
                    case '9':
                        BinaryString = BinaryString.Insert(0, "1001");
                        break;
                    case 'A':
                        BinaryString = BinaryString.Insert(0, "1010");
                        break;
                    case 'a':
                        BinaryString = BinaryString.Insert(0, "1010");
                        break;
                    case 'B':
                        BinaryString = BinaryString.Insert(0, "1011");
                        break;
                    case 'b':
                        BinaryString = BinaryString.Insert(0, "1011");
                        break;
                    case 'C':
                        BinaryString = BinaryString.Insert(0, "1100");
                        break;
                    case 'c':
                        BinaryString = BinaryString.Insert(0, "1100");
                        break;
                    case 'D':
                        BinaryString = BinaryString.Insert(0, "1101");
                        break;
                    case 'd':
                        BinaryString = BinaryString.Insert(0, "1101");
                        break;
                    case 'E':
                        BinaryString = BinaryString.Insert(0, "1110");
                        break;
                    case 'e':
                        BinaryString = BinaryString.Insert(0, "1110");
                        break;
                    case 'F':
                        BinaryString = BinaryString.Insert(0, "1111");
                        break;
                    case 'f':
                        BinaryString = BinaryString.Insert(0, "1111");
                        break;
                }
            }
            if (BinaryString == string.Empty) BinaryString = "0";
            else
                while (BinaryString[0] != '1') BinaryString = BinaryString.Substring(1);

            txt2.Text = BinaryString;
        }
        private void ConvertHexadecimalToOctal(object sender)
        {
            string BinaryString = "";

            for (int i = 0; i < txt1.TextLength; i++)
            {
                switch (txt1.Text[txt1.TextLength - 1 - i])
                {
                    case '0':
                        BinaryString = BinaryString.Insert(0, "0000");
                        break;
                    case '1':
                        BinaryString = BinaryString.Insert(0, "0001");
                        break;
                    case '2':
                        BinaryString = BinaryString.Insert(0, "0010");
                        break;
                    case '3':
                        BinaryString = BinaryString.Insert(0, "0011");
                        break;
                    case '4':
                        BinaryString = BinaryString.Insert(0, "0100");
                        break;
                    case '5':
                        BinaryString = BinaryString.Insert(0, "0101");
                        break;
                    case '6':
                        BinaryString = BinaryString.Insert(0, "0110");
                        break;
                    case '7':
                        BinaryString = BinaryString.Insert(0, "0111");
                        break;
                    case '8':
                        BinaryString = BinaryString.Insert(0, "1000");
                        break;
                    case '9':
                        BinaryString = BinaryString.Insert(0, "1001");
                        break;
                    case 'A':
                        BinaryString = BinaryString.Insert(0, "1010");
                        break;
                    case 'a':
                        BinaryString = BinaryString.Insert(0, "1010");
                        break;
                    case 'B':
                        BinaryString = BinaryString.Insert(0, "1011");
                        break;
                    case 'b':
                        BinaryString = BinaryString.Insert(0, "1011");
                        break;
                    case 'C':
                        BinaryString = BinaryString.Insert(0, "1100");
                        break;
                    case 'c':
                        BinaryString = BinaryString.Insert(0, "1100");
                        break;
                    case 'D':
                        BinaryString = BinaryString.Insert(0, "1101");
                        break;
                    case 'd':
                        BinaryString = BinaryString.Insert(0, "1101");
                        break;
                    case 'E':
                        BinaryString = BinaryString.Insert(0, "1110");
                        break;
                    case 'e':
                        BinaryString = BinaryString.Insert(0, "1110");
                        break;
                    case 'F':
                        BinaryString = BinaryString.Insert(0, "1111");
                        break;
                    case 'f':
                        BinaryString = BinaryString.Insert(0, "1111");
                        break;
                }
            }
            if (BinaryString == string.Empty) BinaryString = "0";
            else
                while (BinaryString[0] != '1') BinaryString = BinaryString.Substring(1);

            uint OctalNum = 0;
            string OctalString = "";
            int Power = 0;

            for (int i = 0; i < BinaryString.Length; i++)
            {
                if (Power == 3)
                {
                    Power = 0;
                    OctalString = OctalString.Insert(0, OctalNum.ToString());
                    OctalNum = 0;
                }

                if (BinaryString[BinaryString.Length - 1 - i] == '1')
                    OctalNum += (uint)Math.Pow(2, Power);
                Power++;
            }
            OctalString = OctalString.Insert(0, OctalNum.ToString());

            txt2.Text = OctalString;
        }
        private void ConvertHexadecimalToDecimal(object sender)
        {
            ulong DecimalNumber = 0;

            for (int i = 0; i < txt1.TextLength; i++)
            {
                int CurrentNumber = 0;
                switch (txt1.Text[txt1.TextLength - 1 - i])
                {
                    case '0':
                        CurrentNumber = 0;
                        break;
                    case '1':
                        CurrentNumber = 1;
                        break;
                    case '2':
                        CurrentNumber = 2;
                        break;
                    case '3':
                        CurrentNumber = 3;
                        break;
                    case '4':
                        CurrentNumber = 4;
                        break;
                    case '5':
                        CurrentNumber = 5;
                        break;
                    case '6':
                        CurrentNumber = 6;
                        break;
                    case '7':
                        CurrentNumber = 7;
                        break;
                    case '8':
                        CurrentNumber = 8;
                        break;
                    case '9':
                        CurrentNumber = 9;
                        break;
                    case 'A':
                        CurrentNumber = 10;
                        break;
                    case 'a':
                        CurrentNumber = 10;
                        break;
                    case 'B':
                        CurrentNumber = 11;
                        break;
                    case 'b':
                        CurrentNumber = 11;
                        break;
                    case 'C':
                        CurrentNumber = 12;
                        break;
                    case 'c':
                        CurrentNumber = 12;
                        break;
                    case 'D':
                        CurrentNumber = 13;
                        break;
                    case 'd':
                        CurrentNumber = 13;
                        break;
                    case 'E':
                        CurrentNumber = 14;
                        break;
                    case 'e':
                        CurrentNumber = 14;
                        break;
                    case 'F':
                        CurrentNumber = 15;
                        break;
                    case 'f':
                        CurrentNumber = 15;
                        break;
                }
                DecimalNumber += (ulong)(Math.Pow(16, i) * CurrentNumber);
            }

            txt2.Text = DecimalNumber.ToString();
        }
        private void ConvertNum(object sender, EventArgs e)
        {
            if (sender == cbFrom)
            {
                txt1.Text = "1";
                txt2.Text = "1";
                return;
            }
                switch (cbFrom.SelectedIndex)
                {
                    case 0:
                        switch (cbTo.SelectedIndex)
                        {
                            case 0:
                                ConvertToTheSameSystem(sender);
                                break;
                            case 1:
                                ConvertBinaryToOctal(sender);
                                break;
                            case 2:
                                ConvertBinaryToDecimal(sender);
                                break;
                            case 3:
                                ConvertBinaryToHexadecimal(sender);
                                break;
                        }
                        break;
                    case 1:
                        switch (cbTo.SelectedIndex)
                        {
                            case 0:
                                ConvertOctalToBinary(sender);
                                break;
                            case 1:
                                ConvertToTheSameSystem(sender);
                                break;
                            case 2:
                                ConvertOctalToDecimal(sender);
                                break;
                            case 3:
                                ConvertOctalToHexadecimal(sender);
                                break;
                        }
                        break;
                    case 2:
                        switch (cbTo.SelectedIndex)
                        {
                            case 0:
                                ConvertDecimalToBinary(sender);
                                break;
                            case 1:
                                ConvertDecimalToOctal(sender);
                                break;
                            case 2:
                                ConvertToTheSameSystem(sender);
                                break;
                            case 3:
                                ConvertDecimalToHexadecimal(sender);
                                break;
                        }
                        break;
                    case 3:
                        switch (cbTo.SelectedIndex)
                        {
                            case 0:
                                ConvertHexadecimalToBinary(sender);
                                break;
                            case 1:
                                ConvertHexadecimalToOctal(sender);
                                break;
                            case 2:
                                ConvertHexadecimalToDecimal(sender);
                                break;
                            case 3:
                                ConvertToTheSameSystem(sender);
                                break;
                        }
                        break;
                }
            }
        private void IsInputCorrect(object sender, KeyPressEventArgs e)
        {
            if (cbFrom.SelectedIndex == 0)
                e.Handled = !(e.KeyChar == '0' || e.KeyChar == '1' || e.KeyChar == (char)Keys.Back);
            else if (cbFrom.SelectedIndex == 1)
            {
                e.Handled = !(e.KeyChar >= '0' && e.KeyChar <= '7' || e.KeyChar == (char)Keys.Back);
            }
            else if (cbFrom.SelectedIndex == 2)
            {
                e.Handled = !(Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            }
            else
            {
                e.Handled = !((char.IsDigit(e.KeyChar) || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
                    || (e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar == (char)Keys.Back)));
            }
        }
        private void txt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txt2.Text);
        }
        private void copyBothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch(cbFrom.SelectedIndex)
            {
                case 0:
                    switch(cbTo.SelectedIndex)
                    {
                        case 0:
                            Clipboard.SetText("(2) " + txt1.Text + " = (2) " + txt2.Text);
                            break;
                        case 1:
                            Clipboard.SetText("(2) " + txt1.Text + " = (8) " + txt2.Text);
                            break;
                        case 2:
                            Clipboard.SetText("(2) " + txt1.Text + " = (10) " + txt2.Text);
                            break;
                        case 3:
                            Clipboard.SetText("(2) " + txt1.Text + " = (16) " + txt2.Text);
                            break;
                    }
                    break;
                case 1:
                    switch (cbTo.SelectedIndex)
                    {
                        case 0:
                            Clipboard.SetText("(8) " + txt1.Text + " = (2) " + txt2.Text);
                            break;
                        case 1:
                            Clipboard.SetText("(8) " + txt1.Text + " = (8) " + txt2.Text);
                            break;
                        case 2:
                            Clipboard.SetText("(8) " + txt1.Text + " = (10) " + txt2.Text);
                            break;
                        case 3:
                            Clipboard.SetText("(8) " + txt1.Text + " = (16) " + txt2.Text);
                            break;
                    }
                    break;
                case 2:
                    switch (cbTo.SelectedIndex)
                    {
                        case 0:
                            Clipboard.SetText("(10) " + txt1.Text + " = (2) " + txt2.Text);
                            break;
                        case 1:
                            Clipboard.SetText("(10) " + txt1.Text + " = (8) " + txt2.Text);
                            break;
                        case 2:
                            Clipboard.SetText("(10) " + txt1.Text + " = (10) " + txt2.Text);
                            break;
                        case 3:
                            Clipboard.SetText("(10) " + txt1.Text + " = (16) " + txt2.Text);
                            break;
                    }
                    break;
                case 3:
                    switch (cbTo.SelectedIndex)
                    {
                        case 0:
                            Clipboard.SetText("(16) " + txt1.Text + " = (2) " + txt2.Text);
                            break;
                        case 1:
                            Clipboard.SetText("(16) " + txt1.Text + " = (8) " + txt2.Text);
                            break;
                        case 2:
                            Clipboard.SetText("(16) " + txt1.Text + " = (10) " + txt2.Text);
                            break;
                        case 3:
                            Clipboard.SetText("(16) " + txt1.Text + " = (16) " + txt2.Text);
                            break;
                    }
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}