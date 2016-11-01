namespace MockCreatorTest.Structs
{
    public struct StructWithCtor
    {
        public StructWithCtor(string name)
        {
            Name = name;
        }

        private string Name { get; set; }
    }
}
