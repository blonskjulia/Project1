﻿using System;
using System.Runtime.Serialization;

namespace org
{
    internal class openqa
    {
        internal class selenium
        {
            [Serializable]
            internal class StaleElementReferenceException : Exception
            {
                public StaleElementReferenceException()
                {
                }

                public StaleElementReferenceException(string message) : base(message)
                {
                }

                public StaleElementReferenceException(string message, Exception innerException) : base(message, innerException)
                {
                }

                protected StaleElementReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
        }
    }
}