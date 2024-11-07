namespace DuckType.Core.DuckType
{
    public class DuckTypeOptions : IDuckTypeOptionsReader
    {
        private bool _useDefaultImplementations;
        public void UseDefaultImplementations()
        {
            _useDefaultImplementations = true;
        }

        bool IDuckTypeOptionsReader.UseDefaultImplementation => _useDefaultImplementations;
    }
}