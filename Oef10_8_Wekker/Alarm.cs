using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmEx
{
    /// <summary>
    /// An alarm is used to set a time to remind the user to do something for example wake up
    /// </summary>
    public class Alarm
    {

        /// <summary>
        /// The alarm time
        /// </summary>
        private DateTime _alarmTime;


        /// <summary>
        /// The beep time
        /// </summary>
        private int _beepTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alarm"/> class.
        /// </summary>
        public Alarm()
        {
            _beepTime = 10;

            _alarmTime = DateTime.MinValue; // 1/1/0001 0:00:00  

        }

        /// <summary>
        /// Determines whether the alarm has passed].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is alarm passed]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAlarmPassed()
        {
            if (_alarmTime == DateTime.MinValue)
            {
                // No alarm time set by the user
                return false;
            }
            else
            {
                // Alarm is set so check if alarmtime has passed
                return DateTime.Now > _alarmTime;
            }
        }

        /// <summary>
        /// Should the beeping stop.
        /// </summary>
        /// <returns></returns>
        public bool ShouldStopBeeping()
        {
                return IsAlarmPassed() && _alarmTime.AddSeconds(_beepTime) < DateTime.Now;
        }

        /// <summary>
        /// Resets the alarm.
        /// </summary>
        public void Reset()
        {
            _alarmTime = DateTime.MinValue;
        }

        /// <summary>
        /// Sets the alarm time.
        /// </summary>
        /// <value>
        /// The alarm time.
        /// </value>
        public string AlarmTime
        {
            get
            {
                return _alarmTime.ToString();
            }
            set
            {
                string day = DateTime.Now.ToString("dd-MM-yyyy") + " " + value;
                _alarmTime = Convert.ToDateTime(day);

                if (_alarmTime.Hour < DateTime.Now.Hour && _alarmTime.Minute < DateTime.Now.Minute &&
                    _alarmTime.Second < DateTime.Now.Second)
                {
                    _alarmTime = _alarmTime.AddDays(1);
                }
            }
        }

        /// <summary>
        /// Gets or sets the beep time.
        /// </summary>
        /// <value>
        /// The beep time.
        /// </value>
        public int BeepTime
        {
            get { return _beepTime; }
            set { _beepTime = value; }
        }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <value>
        /// The current time.
        /// </value>
        public string CurrentTime
        {
            get { return DateTime.Now.ToLongTimeString(); }
        }


    }
}
