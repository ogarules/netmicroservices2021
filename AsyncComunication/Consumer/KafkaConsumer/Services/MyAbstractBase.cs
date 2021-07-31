namespace KafkaConsumer.Services
{
    public abstract class MyAbstractBase
    {
        public int Calculate(int val1, int val2)
        {
            return this.ExecuteOperation(SetDefaultValue(val1), SetDefaultValue(val2));
        }

        protected abstract int ExecuteOperation(int val1, int val2);

        protected virtual int SetDefaultValue(int val)
        {
            if (val <= 0)
            {
                return 100;
            }
            else
            {
                return val;
            }
        }
    }
}