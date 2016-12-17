using System;

namespace ObjectCreatorTest.DataClasses
{
    public class ImmutableActionAndFunc
    {
        public Action<string> Action { get; }
        public Func<string, int> Func { get; }

        public ImmutableActionAndFunc(Action<string> action, Func<string, int> func)
        {
            Action = action;
            Func = func;
        }
    }

    public class ActionAndFunc
    {
        public Action<string> Action { get; set; }
        public Func<string, int> Func { get; set; }
    }
}
