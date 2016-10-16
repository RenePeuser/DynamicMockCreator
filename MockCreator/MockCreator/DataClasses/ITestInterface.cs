using System;

namespace MockCreator.DataClasses
{
    public interface ITestInterface
    {
        bool IsValid { get; set; }

        string Name { get; set; }

        string DoSomethingReturn();

        string DoSomethingReturn(int arg0);

        string DoSomethingReturn(int arg0, string arg2);

        string DoSomethingReturn(int arg0, string arg2, ICloneable arg3);

        void DoSomething();
    }
}
