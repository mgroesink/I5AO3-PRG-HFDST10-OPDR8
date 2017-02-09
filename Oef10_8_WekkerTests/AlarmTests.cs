using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlarmEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmEx.Tests
{
    [TestClass()]
    public class AlarmTests
    {
        [TestMethod()]
        public void NoAlarmSetTest()
        {
            Alarm alarm = new Alarm();

            Assert.AreEqual(alarm.AlarmTime, DateTime.MinValue.ToString());
        }

        [TestMethod()]
        public void AlarmPassed()
        {
            Alarm alarm = new Alarm();
            alarm.AlarmTime = DateTime.Now.AddSeconds(-30).ToString("hh:mm:ss");
            bool expected = alarm.IsAlarmPassed();
            Assert.AreEqual(true, expected);
        }

        [TestMethod()]
        public void AlarmNotPassed()
        {
            Alarm alarm = new Alarm();
            alarm.AlarmTime = DateTime.Now.AddSeconds(100).ToString("hh:mm:ss");
            bool expected = alarm.IsAlarmPassed();

            Assert.AreEqual(false, expected);
        }
    }
}