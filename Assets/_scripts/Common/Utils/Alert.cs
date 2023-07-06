using UnityEngine;

namespace _scripts.Common.Utils
{
    public static class Alert
    {
        private const bool Enabled = true;
    
        public static void Message(object message)
        {
            if (Enabled)
            {
                Debug.Log(message);
            }
        }

        public static void Warning(object message)
        {
            if (Enabled)
            {
                Debug.LogWarning(message);
            }
        }

        public static void Error(object message)
        {
            if (Enabled)
            {
                Debug.LogError(message);
            }
        }
    }
}