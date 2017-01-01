namespace ObjectCreatorTest.Structs
{
    public struct StructWithCtor
    {
        public StructWithCtor(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
