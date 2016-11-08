using System;
using System.Threading;

namespace ApplicationToMonitor
{
    /// <summary>
    /// a very simple implementation of a utility class to handle heartbeat generation
    /// 
    /// it's a logging feature, any call to methods of this class should not fail, event if it's misconfigured 
    /// </summary>
    public sealed class Heartbeat : IDisposable
    {
        private Timer _timer;

        /// <summary>
        /// use the interval specified in the Settings:
        /// SID.Monitoring.HeartbeatInterval
        /// </summary>
        public void Start()
        {
            var ts = new TimeSpan(0, 1, 0);
            if (ts == TimeSpan.Zero || ts.TotalMilliseconds <= 0)
                return;

            StartTimer(ts);
        }

        private void StartTimer(TimeSpan interval)
        {
            _timer_Elapsed(null);

            _timer = new System.Threading.Timer(_timer_Elapsed, null, 
                0, (int)interval.TotalMilliseconds);

            //_timer = new System.Threading.Timer(_timer_Elapsed);
            //_timer.Change((int)interval.TotalMilliseconds, (int)interval.TotalMilliseconds);
        }

        private void _timer_Elapsed(object sender)
        {
            Logger.Heartbeat();
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
