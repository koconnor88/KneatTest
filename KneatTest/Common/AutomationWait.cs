using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
//using Automation.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace KneatTest.Common
{
    public class AutomationWait<TObjectToWaitOn>
    {
        private readonly TimeSpan _defaultSleepTimeout = TimeSpan.FromMilliseconds(1500);
        private TimeSpan _defaultTimeout;

        private readonly IClock _clock;

        private readonly TObjectToWaitOn _objectToWaitOn;

        private string _timeOutMessage = string.Empty;

        public static AutomationWait<TObjectToWaitOn> On(TObjectToWaitOn objectToWaitOn)
        {

            return new AutomationWait<TObjectToWaitOn>(objectToWaitOn, TimeSpan.FromSeconds(10));
        }

        private AutomationWait(TObjectToWaitOn objectToWaitOn, TimeSpan timeOut)
        {
            _objectToWaitOn = objectToWaitOn;
            _defaultTimeout = timeOut;
            _clock = new SystemClock();
        }

       

        public AutomationWait<TObjectToWaitOn> UpTo(TimeSpan timeSpan)
        {
            _defaultTimeout = timeSpan;
            return this;
        }

        public AutomationWait<TObjectToWaitOn> TimeoutMessage(string message)
        {
            _timeOutMessage = ": " + message;
            return this;
        }

        public TResult Until<TResult>(Func<TObjectToWaitOn, TResult> condition)
        {

            if (condition == null)
            {
                // ReSharper disable once LocalizableElement
                throw new ArgumentNullException("condition", "condition cannot be null");
            }

            var resultType = typeof(TResult);
            if ((resultType.IsValueType && resultType != typeof(bool)) || !resultType.IsSubclassOf(typeof(object)))
            {
                // ReSharper disable once LocalizableElement
                throw new ArgumentException("Can only wait on an object or boolean response, tried to use type: " + resultType, "condition");
            }

            WebDriverException lastException = null;
            var endTime = _clock.LaterBy(_defaultTimeout);
            while (_clock.IsNowBefore(endTime))
            {
                try
                {
                    var result = condition(_objectToWaitOn);
                    if (resultType == typeof(bool))
                    {
                        var boolResult = result as bool?;
                        if (boolResult.HasValue && boolResult.Value)
                        {
                            return result;
                        }
                    }
                    else
                    {
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
                catch (WebDriverException e)
                {
                    lastException = e;
                }

                Thread.Sleep(_defaultSleepTimeout);
            }

            throw new TimeoutException(string.Format(CultureInfo.InvariantCulture, "Timed out after {0} seconds {1}", _defaultTimeout.TotalSeconds, _timeOutMessage), lastException);
        }

        public TResult SilentUntil<TResult>(Func<TObjectToWaitOn, TResult> condition)
        {
            TResult result;

            try
            {
                result = Until(condition);
            }
            catch (TimeoutException)
            {
                result = default(TResult);
            }

            return result;
        }

        public IEnumerable<TResult> SilentUntilCollection<TResult>(Func<TObjectToWaitOn, IEnumerable<TResult>> condition)
        {
            IEnumerable<TResult> result;

            try
            {
                result = Until(condition);
            }
            catch (TimeoutException)
            {
                result = new List<TResult>();
            }

            return result;
        }
    }
}
