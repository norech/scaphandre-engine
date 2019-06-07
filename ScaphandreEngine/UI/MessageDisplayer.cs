using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScaphandreEngine.UI
{
    public static class MessageDisplayer
    {
        /// <summary>
        /// Show an action reply (the message in the upper left, internally called "ErrorMessage" by Subnautica)
        /// </summary>
        /// <param name="kind">the kind of message</param>
        /// <param name="message">the message to show, supports Unity rich text</param>
        public static void ShowActionReply(MessageKind kind, string message)
        {
            switch (kind)
            {
                case MessageKind.Info:
                    ErrorMessage.AddMessage(message);
                    break;
                case MessageKind.Error:
                    ErrorMessage.AddError(message);
                    break;
                case MessageKind.Warning:
                    ErrorMessage.AddWarning(message);
                    break;
                default:
                    throw new Exception("invalid message kind");
            }
        }

        /// <summary>
        /// Show an action reply (the message in the upper left, internally called "ErrorMessage" by Subnautica)
        /// </summary>
        /// <param name="message">the message to show, supports Unity rich text</param>
        public static void ShowActionReply(string message)
        {
            ShowActionReply(MessageKind.Info, message);
        }

        private static GameObject notifGO;

        /// <summary>
        /// Show an action reply (the message in the upper left, internally called "ErrorMessage" by Subnautica)
        /// </summary>
        /// <param name="kind">the kind of message</param>
        /// <param name="message">the message to show, supports Unity rich text</param>
        public static void ShowPDANotication(string message, FMODAsset sound = null)
        {
            if(notifGO == null || !notifGO.activeInHierarchy)
            {
                notifGO = new GameObject();
            }

            PDANotification notification = notifGO.AddComponent<PDANotification>();

            notification.text = message;
            notification.sound = sound;

            notification.Play();
        }
    }
}
