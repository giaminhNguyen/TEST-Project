using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#if USE_FIREBASE_ANALYTICS
using Firebase.Analytics;
#endif
using UnityEngine;

namespace UltimateHelper
{
    public class FirebaseUtilities
    {
        #region Analytics
        

        public static void LogEvent(string eventName)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                Debug.LogWarning("eventName IsNullOrEmpty");
                return;
            }
            #if USE_FIREBASE_ANALYTICS && USE_FIREBASE_LOG_EVENT
            FirebaseAnalytics.LogEvent(eventName);
            #endif
        }

        public static void LogEvent(string eventName, string parameterName, string parameterValue)
        {
            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(parameterName))
            {
                Debug.LogWarning("eventName or parameterName IsNullOrEmpty");
                return;
            }
            #if USE_FIREBASE_ANALYTICS && USE_FIREBASE_LOG_EVENT
            FirebaseAnalytics.LogEvent(eventName, parameterName, parameterValue);
            #endif
        }
        
        public static void LogEvent(string eventName, string parameterName, int parameterValue)
        {
            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(parameterName))
            {
                Debug.LogWarning("eventName or parameterName IsNullOrEmpty");
                return;
            }
            #if USE_FIREBASE_ANALYTICS && USE_FIREBASE_LOG_EVENT
            FirebaseAnalytics.LogEvent(eventName, parameterName, parameterValue);
            #endif
        }
        
        public static void LogEvent(string eventName, string parameterName, long parameterValue)
        {
            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(parameterName))
            {
                Debug.LogWarning("eventName or parameterName IsNullOrEmpty");
                return;
            }
            #if USE_FIREBASE_ANALYTICS && USE_FIREBASE_LOG_EVENT
            FirebaseAnalytics.LogEvent(eventName, parameterName, parameterValue);
            #endif
        }
        
        public static void LogEvent(string eventName, string parameterName, float parameterValue)
        {
            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(parameterName))
            {
                Debug.LogWarning("eventName or parameterName IsNullOrEmpty");
                return;
            }
            #if USE_FIREBASE_ANALYTICS && USE_FIREBASE_LOG_EVENT
            FirebaseAnalytics.LogEvent(eventName, parameterName, parameterValue);
            #endif
        }
        
        public static void LogEvent(string eventName, string parameterName, double parameterValue)
        {
            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(parameterName))
            {
                Debug.LogWarning("eventName or parameterName IsNullOrEmpty");
                return;
            }
            #if USE_FIREBASE_ANALYTICS && USE_FIREBASE_LOG_EVENT
            FirebaseAnalytics.LogEvent(eventName, parameterName, parameterValue);
            #endif
        }

#if USE_FIREBASE_ANALYTICS
        public static void LogEvent(string eventName, Parameter[] parameter)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                Debug.LogWarning("eventName IsNullOrEmpty");
                return;
            }
            if(parameter == null)
            {
                Debug.LogWarning("parameter IsNull");
                return;
            }
            #if USE_FIREBASE_ANALYTICS && USE_FIREBASE_LOG_EVENT
            FirebaseAnalytics.LogEvent(eventName, parameter);
            #endif
        }
#endif
        public static void LogEvent(string eventName, Dictionary<string, object> dictionary = null)
        {
            #if USE_FIREBASE_ANALYTICS
            try
            {
                if (string.IsNullOrEmpty(eventName))
                {
                    Debug.LogWarning("eventName IsNullOrEmpty");
                    return;
                }

                if (eventName.Length >= 32)
                    eventName = eventName[..32];
                eventName = eventName.ToLower();

                if (dictionary != null)
                {
                    var param = dictionary.Select(x =>
                    {
                        return x.Value switch
                        {
                                float value => new Parameter(x.Key.ToLower(), value),
                                long value  => new Parameter(x.Key.ToLower(), value),
                                int value   => new Parameter(x.Key.ToLower(), value),
                                string => new Parameter(x.Key.ToLower(),
                                        !x.Value.ToString().Contains("_")
                                                ? Regex.Replace(x.Value.ToString(), @"\B[A-Z]", m => "_" + m.ToString())
                                                       .ToLower()
                                                : x.Value.ToString()),
                                _ => new Parameter(x.Key.ToLower(), "")
                        };
                    }).ToArray();

                    LogEvent(eventName, param);
                }
                else
                {
                    LogEvent(eventName);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("[Firebase] LogEvent: " + ex.Message);
            }
            #endif
        }
        
        public static void LogSpendVirtualCurrency(int value, string name)
        {
            #if USE_FIREBASE_ANALYTICS
            FirebaseAnalytics.LogEvent(
                    FirebaseAnalytics.EventSpendVirtualCurrency,
                    new Parameter[]
                    {
                            new Parameter(FirebaseAnalytics.ParameterValue, value),
                            new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, name)
                    });
            #endif
        }
        
        public static void LogEarnVirtualCurrency(int value, string name)
        {
            #if USE_FIREBASE_ANALYTICS
            FirebaseAnalytics.LogEvent(
                    FirebaseAnalytics.EventEarnVirtualCurrency,
                    new Parameter[]
                    {
                            new Parameter(FirebaseAnalytics.ParameterValue, value),
                            new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, name)
                    });
            #endif
        }
        
        public static void LogLevelUp(string character, string level)
        {
            #if USE_FIREBASE_ANALYTICS
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(level))
                return;

            SetUserProperty("Level", level);

            FirebaseAnalytics.LogEvent(
                    FirebaseAnalytics.EventLevelUp,
                    new Parameter[] {
                            new Parameter( FirebaseAnalytics.ParameterCharacter, character),
                            new Parameter(  FirebaseAnalytics.ParameterLevel, level),
                    }
            );
            #endif
        }
        
        public static void SetUserId(string userId)
        {
            #if USE_FIREBASE_ANALYTICS
            FirebaseAnalytics.SetUserId(userId);
            #endif
        }
        
        public static void SetUserProperty(string propertyName, object propertyValue)
        {
            #if USE_FIREBASE_ANALYTICS
            try
            {
                if (propertyValue == null)
                    return;
                FirebaseAnalytics.SetUserProperty(propertyName, propertyValue.ToString());
            }
            catch (Exception ex)
            {
                Debug.LogError("[Firebase] SetUser: " + ex.Message);
            }
            #endif
        }

        #endregion
    }
}