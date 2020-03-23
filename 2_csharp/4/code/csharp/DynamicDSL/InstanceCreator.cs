namespace DynamicDSL
{
    class InstanceCreator
    {
        public dynamic Person
        {
            get
            {
                return new Person();
            }
        }
    }
}