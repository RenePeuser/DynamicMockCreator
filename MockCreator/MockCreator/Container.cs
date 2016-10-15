namespace MockCreator
{
    public class Container
    {
        public Container(ClassWithInterfaces classWithInterfaces,
            ClassWithPrimitiveTypes classWithPrimitiveTypes,
            ClassAbstract classAbstract
        )
        {
            ClassWithInterfaces = classWithInterfaces;
            ClassWithPrimitiveTypes = classWithPrimitiveTypes;
            ClassAbstract = classAbstract;
        }

        public ClassWithInterfaces ClassWithInterfaces { get; }
        public ClassWithPrimitiveTypes ClassWithPrimitiveTypes { get; }
        public ClassAbstract ClassAbstract { get; }
    }
}