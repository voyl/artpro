/*****************************************************************************
 * Title: EventArgs.cs
 * Author: Avaya
 * Copyright: Copyright 2007 AVAYA, Inc.
 *****************************************************************************
 * Purpose: This file contains classes which are used as parameters while invoking
 * event handlers.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TeleCommuter
{
    /* These definitions list values of various lamp attributes */
    public enum Lamp
    {
        Conference=259,
        Transfer = 260,
        Hold = 261,
        CallAppearance = 263
    }
    public enum LampColor
    {
        Red = 1,
        Green = 3
    }
    public enum LampMode
    {
        Off = 2,
        Steady = 3,
        Flashing = 7,

    }
 
  
    /* These definitions list values of ringer attribute. */
    public enum RingPattern
    {
        RingerOff = 0,
        ManualSignal = 1,
        Intercom = 9,
        StandardRing = 11,
        PriorityRing = 13
    }

    /* Event Argument class for "lampModeEvent" */
    public class LampModeEventArgs
    {
        private int monitorCrosRefID = 0;
        private LampMode lampMode = 0;
        private LampColor lampColor = 0;
        private Lamp lamp = 0;

        public int MonitorCrosRefID
        {
            get { return monitorCrosRefID; }
        }
        public Lamp Lamp
        {
            get { return lamp; }
        }
        public LampMode LampMode
        {
            get { return lampMode; }
        }
        public LampColor LampColor
        {
            get { return lampColor; }
        }

       
        public LampModeEventArgs(XmlElement root)
        {
            XmlNodeList nodelist = root.ChildNodes;
            foreach (XmlNode node in nodelist)
            {
                switch (node.Name)
                {
                    case "monitorCrosRefID":
                        monitorCrosRefID = Int32.Parse(node.InnerText);
                        break;
                    case "lamp":
                        lamp = (Lamp)Int32.Parse(node.InnerText);
                        break;
                    case "lampMode":
                        lampMode = (LampMode)Int32.Parse(node.InnerText);
                        break;
                    case "lampColor":
                        lampColor = (LampColor)Int32.Parse(node.InnerText);
                        break;
                }

                
            }
        }
    }

    /* Event Argument class for "ringerStatusEvent" */
    public class RingerStatusEventArgs
    {
        private string device;
        private string ringMode;
        private RingPattern ringPattern;

        public string RingMode
        {
            get { return ringMode; }
        }
	    public RingPattern RingPattern
        {
            get { return ringPattern; }
        }	
        public string DeviceID
        {
            get { return device; }
        }
	
        public RingerStatusEventArgs(XmlNode root)
        {

            XmlNodeList nodelist = root.ChildNodes;
            foreach (XmlNode node in nodelist)
            {
                switch (node.Name)
                {
                    case "ringMode":
                        ringMode = node.InnerText;
                        break;
                    case "ringPattern":
                        ringPattern = (RingPattern)Int32.Parse(node.InnerText);
                        break;
                    case "device":
                        device = node.InnerText;
                        break;
                }
            }
        }
    }

    /* Event Argument class for "displayUpdatedEvent"*/
    public class DisplayUpdatedEventArgs
    {
        private string message;

        public string Message
        {
            get { return message; }
        }

        public DisplayUpdatedEventArgs(XmlNode root)
        {
            XmlNodeList nodelist = root.ChildNodes;
            foreach (XmlNode node in nodelist)
            {
                switch (node.Name)
                {
                    case "contentsOfDisplay":
                        message = node.InnerText;
                        break;
                }
            }
        }
    }

    /* Event Argument class for "TerminalUnregisteredEvent*/
    public class TerminalUnregisteredEventArgs
    {
        private string reason;

        public string Reason
        {
            get { return reason; }
        }

        public TerminalUnregisteredEventArgs(XmlElement root)
        {
            XmlNodeList nodelist = root.GetElementsByTagName("reason");
            if (nodelist != null && nodelist.Count > 0)
            {
                reason = nodelist[0].InnerText;
            }
        }
    }
    
    /* Event Argument class for exceptions*/
    public class ExceptionArgs
    {
        private string message;

        public string Message
        {
            get { return message; }
        }

        public ExceptionArgs(string expMessage)
        {
            message = expMessage;
        }
    }

}
