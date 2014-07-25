using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M2E.Models
{
    public class ResponseModel<T>
    {
        public int Status;
        public string Message;
        public T Payload;
    }
}