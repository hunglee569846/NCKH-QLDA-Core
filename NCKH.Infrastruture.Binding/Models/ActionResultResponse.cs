
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NCKH.Infrastruture.Binding.Models
{
    public class ActionResultReponese
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public long Code { get; set; }
        public ActionResultReponese() { }
        public ActionResultReponese(long code, string message = "", string title = "")
        {
            Code = code;
            Title = title;
            Message = message.ToString();
        }
    }
    public class ActionResultReponese<T> : ActionResultReponese
    {
        public T Data { get; set; }
        public ActionResultReponese()
        {
            Code = 1;
        }
        public ActionResultReponese(long code, string messge = "", string title = "", T data = default)
        {
            Code = code;
            Title = title;
            Message = messge.ToString();
            Data = data;
        }
        public ActionResultReponese(T data)
        {
            Code = 1;
            Data = data;
        }

    }
}
