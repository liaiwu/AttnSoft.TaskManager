using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class Fm_QuartzCron : Form
    {
        public Fm_QuartzCron()
        {
            InitializeComponent();
        }

        Dictionary<string, int> dicWeek = new Dictionary<string, int>();
        private void Fm_QuartzCron_Load(object sender, EventArgs e)
        {
            numYearCS.Minimum = DateTime.Now.Year;
            numYearCO.Minimum = DateTime.Now.Year;
            numYearCS.Maximum = 2050;
            numYearCO.Maximum = 2050;
            txtWeek.Text = "?";
            txtYear.Text = string.Empty;
            dicWeek.Add("SUN", 1);
            dicWeek.Add("MON", 2);
            dicWeek.Add("TUES", 3);
            dicWeek.Add("WED", 4);
            dicWeek.Add("THUR", 5);
            dicWeek.Add("FRI", 6);
            dicWeek.Add("SAT", 7);
        }

        private void rbClick(object sender, EventArgs e)
        {
            RadioButton? rb = sender as RadioButton;
            if (rb == null) return;
            switch (rb.Name)
            {
                case "rbSecEvery":
                    txtSecond.Text = "*";
                    chkSec.Enabled = false;
                    numSecStart.Enabled = false;
                    numSecEvery.Enabled = false;
                    numSecCS.Enabled = false;
                    numSecCO.Enabled = false;
                    break;
                case "rbSecCycle":
                    txtSecond.Text = numSecCS.Value + "-" + numSecCO.Value;
                    chkSec.Enabled = false;
                    numSecStart.Enabled = false;
                    numSecEvery.Enabled = false;
                    numSecCS.Enabled = true;
                    numSecCO.Enabled = true;
                    break;
                case "rbSec":
                    txtSecond.Text = numSecStart.Value + "/" + numSecEvery.Value;
                    numSecStart.Enabled = true;
                    numSecEvery.Enabled = true;
                    numSecCS.Enabled = false;
                    numSecCO.Enabled = false;
                    chkSec.Enabled = false;
                    break;
                case "rbSecAppoint":
                    txtSecond.Text = "*";
                    chkSec.Enabled = true;
                    numSecStart.Enabled = false;
                    numSecEvery.Enabled = false;
                    numSecCS.Enabled = false;
                    numSecCO.Enabled = false;
                    break;
                case "rbMinEvery":
                    txtMinite.Text = "*";
                    chkMin.Enabled = false;
                    numMinStart.Enabled = false;
                    numMinEvery.Enabled = false;
                    numMinCS.Enabled = false;
                    numMinCO.Enabled = false;
                    break;
                case "rbMinCycle":
                    txtMinite.Text = numMinCS.Value + "-" + numMinCO.Value;
                    chkMin.Enabled = false;
                    numMinStart.Enabled = false;
                    numMinEvery.Enabled = false;
                    numMinCS.Enabled = true;
                    numMinCO.Enabled = true;
                    break;
                case "rbMin":
                    txtMinite.Text = numMinStart.Value + "/" + numMinEvery.Value;
                    numMinStart.Enabled = true;
                    numMinEvery.Enabled = true;
                    numMinCS.Enabled = false;
                    numMinCO.Enabled = false;
                    chkMin.Enabled = false;
                    break;
                case "rbMinAppoint":
                    txtMinite.Text = "*";
                    chkMin.Enabled = true;
                    numMinStart.Enabled = false;
                    numMinEvery.Enabled = false;
                    numMinCS.Enabled = false;
                    numMinCO.Enabled = false;
                    break;
                case "rbHourEvery":
                    txtHour.Text = "*";
                    chkHour.Enabled = false;
                    numHourStart.Enabled = false;
                    numHourEvery.Enabled = false;
                    numHourCS.Enabled = false;
                    numHourCO.Enabled = false;
                    break;
                case "rbHour":
                    txtHour.Text = numHourStart.Value + "/" + numHourEvery.Value;
                    chkHour.Enabled = false;
                    numHourStart.Enabled = true;
                    numHourEvery.Enabled = true;
                    numHourCS.Enabled = false;
                    numHourCO.Enabled = false;
                    break;
                case "rbHourCycle":
                    txtHour.Text = numHourCS.Value + "-" + numHourCO.Value;
                    chkHour.Enabled = false;
                    numHourStart.Enabled = false;
                    numHourEvery.Enabled = false;
                    numHourCS.Enabled = true;
                    numHourCO.Enabled = true;
                    break;
                case "rbHourAppoint":
                    txtHour.Text = "*";
                    chkHour.Enabled = true;
                    numHourStart.Enabled = false;
                    numHourEvery.Enabled = false;
                    numHourCS.Enabled = false;
                    numHourCO.Enabled = false;
                    break;
                case "rbDayEvery":
                case "rbDayNoAppoint":
                    txtDay.Text = rb.Name == "rbDayEvery" ? "*" : "?";
                    chkDay.Enabled = false;
                    numDayStart.Enabled = false;
                    numDayEvery.Enabled = false;
                    numDayCS.Enabled = false;
                    numDayCO.Enabled = false;
                    numDayW.Enabled = false;
                    break;
                case "rbDayCycle":
                    chkDay.Enabled = false;
                    txtDay.Text = numDayCS.Value + "-" + numDayCO.Value;
                    numDayStart.Enabled = false;
                    numDayEvery.Enabled = false;
                    numDayCS.Enabled = true;
                    numDayCO.Enabled = true;
                    numDayW.Enabled = false;
                    break;
                case "rbDay":
                    chkDay.Enabled = false;
                    txtDay.Text = numDayStart.Value + "/" + numDayEvery.Value;
                    numDayStart.Enabled = true;
                    numDayEvery.Enabled = true;
                    numDayCS.Enabled = false;
                    numDayCO.Enabled = false;
                    break;
                case "rbDayAppoint":
                    txtDay.Text = "*";
                    chkDay.Enabled = true;
                    numDayStart.Enabled = false;
                    numDayEvery.Enabled = false;
                    numDayCS.Enabled = false;
                    numDayCO.Enabled = false;
                    numDayW.Enabled = false;
                    break;
                case "rbDayW":
                    txtDay.Text = numDayW.Value + "W" ;
                    chkDay.Enabled = false;
                    numDayStart.Enabled = false;
                    numDayEvery.Enabled = false;
                    numDayCS.Enabled = false;
                    numDayCO.Enabled = false;
                    numDayW.Enabled = true;
                    break;
                case "rbDayL":
                    txtDay.Text = "L";
                    chkDay.Enabled = false;
                    numDayStart.Enabled = false;
                    numDayEvery.Enabled = false;
                    numDayCS.Enabled = false;
                    numDayCO.Enabled = false;
                    numDayW.Enabled = false;
                    break;
                case "rbMouthEvery":
                case "rbMouthNoAppoint":
                    txtMonth.Text = rbMouthEvery.Checked == true ? "*" : "?";
                    chkMouth.Enabled = false;
                    numMouthEvery.Enabled = false;
                    numMouthStart.Enabled = false;
                    numMouthCO.Enabled = false;
                    numMouthCS.Enabled = false;
                    break;
                case "rbMouth":
                    txtMonth.Text = numMouthStart.Value + "/" + numMouthEvery.Value;
                    chkMouth.Enabled = false;
                    numMouthEvery.Enabled = true;
                    numMouthStart.Enabled = true;
                    numMouthCO.Enabled = false;
                    numMouthCS.Enabled = false;
                    break;
                case "rbMouthAppoint":
                    chkMouth.Enabled = true;
                    numMouthEvery.Enabled = false;
                    numMouthStart.Enabled = false;
                    numMouthCO.Enabled = false;
                    numMouthCS.Enabled = false;
                    break;
                case "rbMouthCycle":
                    txtMonth.Text = numMouthCS.Value + "-" + numMouthCO.Value;
                    chkMouth.Enabled = false;
                    numMouthEvery.Enabled = false;
                    numMouthStart.Enabled = false;
                    numMouthCO.Enabled = true;
                    numMouthCS.Enabled = true;
                    break;
                case "rbWeek":
                case "rbWeekNoAppoint":
                    numWeek.Enabled = false;
                    numWeekCS.Enabled = false;
                    numWeekCO.Enabled = false;
                    txtWeek.Text = rb.Name == "rbWeek" ? "*" : "?";
                    chkWeek.Enabled = false;
                    break;
                case "rbWeekAppoint":
                    numWeek.Enabled = false;
                    numWeekCS.Enabled = false;
                    numWeekCO.Enabled = false;
                    txtWeek.Text = "*";
                    chkWeek.Enabled = true;
                    break;
                case "rbWeekNumIn":
                case "rbWeekLast":
                    numWeek.Enabled = true;
                    numWeekCS.Enabled = false;
                    numWeekCO.Enabled = false;
                    chkWeek.Enabled = true;
                    chkWeek.ClearSelected();
                    chkWeek.SelectedIndex = 0;
                    break;
                case "rbWeekCycle":
                    txtWeek.Text = numWeekCS.Value + "/" + numWeekCO.Value;
                    numWeek.Enabled = false;
                    numWeekCS.Enabled = true;
                    numWeekCO.Enabled = true;
                    chkWeek.Enabled = false;
                    break;
                case "rbYear":
                case "rbYearNoAppoint":
                    txtYear.Text = rbYear.Checked == true ? "*" : "";
                    numYearCS.Enabled = false;
                    numYearCO.Enabled = false;
                    break;
                case "rbYearAppoint":
                    txtYear.Text = numYearCS.Value + "-" + numYearCO.Value;
                    numYearCS.Enabled = true;
                    numYearCO.Enabled = true;
                    break;
            }
        }

        private void chk_SelectedValueChanged(object sender, EventArgs e)
        {
            if (rbWeekNumIn.Checked || rbWeekLast.Checked)
            {
                for (int i = 0; i < chkWeek.Items.Count; i++)
                {
                    chkWeek.SetItemCheckState(i, CheckState.Unchecked);
                }
                if (chkWeek.SelectedIndex > -1)
                    chkWeek.SetItemCheckState(chkWeek.SelectedIndex, CheckState.Checked);
            }
            CheckedListBox? chk = sender as CheckedListBox;
            if (chk == null) return;
            ArrayList al = new ArrayList();
            foreach (string? item in chk.CheckedItems)
            {
                if (string.IsNullOrEmpty(item)) continue;
                if (chk.Name == "chkWeek")
                {
                    al.Add(Convert.ToInt32(dicWeek[item!]));
                }
                else
                {
                    al.Add(Convert.ToInt32(item));
                }
            }
            if (al != null && al.Count > 0)
            {
                al.Sort();
                string str = string.Join(",", (int[])al.ToArray(typeof(int)));
                switch (chk.Name)
                {
                    case "chkSec":
                        txtSecond.Text = str;
                        break;
                    case "chkMin":
                        txtMinite.Text = str;
                        break;
                    case "chkHour":
                        txtHour.Text = str;
                        break;
                    case "chkDay":
                        txtDay.Text = str;
                        break;
                    case "chkMouth":
                        txtMonth.Text = str;
                        break;
                    case "chkWeek":
                        if (rbWeekNumIn.Checked)
                        {
                            txtWeek.Text = str + "#" + numWeek.Value;
                        }
                        else if (rbWeekLast.Checked)
                        {
                            txtWeek.Text = str + "L";
                        }
                        else
                        {
                            txtWeek.Text = str;
                        }
                        break;
                }
            }
        }

        private void num_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown? numUD = sender as NumericUpDown;
            if (numUD == null) return;
            switch (numUD.Tag?.ToString())
            {
                case "numSec":
                    txtSecond.Text = numSecStart.Value + "/" + numSecEvery.Value;
                    break;
                case "numSecC":
                    txtSecond.Text = numSecCS.Value + "-" + numSecCO.Value;
                    break;
                case "numMin":
                    txtMinite.Text = numMinStart.Value + "/" + numMinEvery.Value;
                    break;
                case "numMinC":
                    txtMinite.Text = numMinCS.Value + "-" + numMinCO.Value;
                    break;
                case "numHour":
                    txtHour.Text = numHourStart.Value + "/" + numHourEvery.Value;
                    break;
                case "numHourC":
                    txtHour.Text = numHourCS.Value + "-" + numHourCO.Value;
                    break;
                case "numDay":
                    txtDay.Text = numDayStart.Value + "/" + numDayEvery.Value;
                    break;
                case "numDayC":
                    txtDay.Text = numDayCS.Value + "-" + numDayCO.Value;
                    break;
                case "numDayW":
                    txtDay.Text = numDayW.Value + "W" ;
                    break;
                case "numMouth":
                    txtMonth.Text = numMouthStart.Value + "/" + numMouthEvery.Value;
                    break;
                case "numMouthC":
                    txtMonth.Text = numMouthCS.Value + "-" + numMouthCO.Value;
                    break;
                case "numWeekC":
                    txtWeek.Text = numWeekCS.Value + "/" + numWeekCO.Value;
                    break;
                case "numYearC":
                    txtYear.Text = numYearCS.Value + "-" + numYearCO.Value;
                    break;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel? ll = sender as LinkLabel;
            if (ll == null) return;
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = "iexplore.exe";
            myProcess.StartInfo.Arguments = ll.Text;
            myProcess.Start();
        }

        private void tabTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabTime.SelectedTab.Name == "tabIntro")
            {
                this.tabTime.Dock = System.Windows.Forms.DockStyle.Fill;
                gb1.Visible = false;
                gb2.Visible = false;
            }
            else
            {
                this.tabTime.Dock = System.Windows.Forms.DockStyle.Top;
                gb1.Visible = true;
                gb2.Visible = true;
            }
        }

        private void txtChanged(object sender, EventArgs e)
        {
            string second = txtSecond.Text.Trim();
            string minute = txtMinite.Text.Trim();
            string hour = txtHour.Text.Trim();
            string day = txtDay.Text.Trim();
            string mouth = txtMonth.Text.Trim();
            string week = txtWeek.Text.Trim();
            string year = txtYear.Text.Trim();
            txtCron.Text = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                string.IsNullOrEmpty(second) ? "*" : second,
                string.IsNullOrEmpty(minute) ? "*" : minute,
                string.IsNullOrEmpty(hour) ? "*" : hour,
                string.IsNullOrEmpty(day) ? "*" : day,
                string.IsNullOrEmpty(mouth) ? "*" : mouth,
                string.IsNullOrEmpty(week) ? "?" : week,
                string.IsNullOrEmpty(year) ? "" : year);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCron.Text))
                Clipboard.SetDataObject(txtCron.Text);
        }
        public string QuartzCronString
        {
            get { return this.txtCron.Text; }
        }

    }
}
