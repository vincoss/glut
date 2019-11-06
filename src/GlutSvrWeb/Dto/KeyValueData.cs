using System;



namespace GlutSvrWeb.Dto
{
    public class KeyValueData<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
    }
}
