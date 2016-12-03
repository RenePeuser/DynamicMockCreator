using System;

namespace ObjectCreatorTest.DataClasses
{
    public class ClassWithActionAndFunc
    {
        public Action<string> Action { get; set; }
        public Func<string, int> Func { get; set; }

        public ClassWithActionAndFunc(Action<string> action, Func<string, int> func)
        {
            Action = action;
            Func = func;
        }
    }
}
